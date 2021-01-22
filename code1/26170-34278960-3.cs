    public static class TreeViewSelectedItemExBehavior
    {
        private static List<TreeView> isRegisteredToSelectionChanged = new List<TreeView>();
        public static readonly DependencyProperty SelectedItemExProperty =
            DependencyProperty.RegisterAttached("SelectedItemEx",
                typeof(object),
                typeof(TreeViewSelectedItemExBehavior),
                new FrameworkPropertyMetadata(new object(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedItemExChanged, null));
        #region SelectedItemEx
        public static object GetSelectedItemEx(TreeView target)
        {
            return target.GetValue(SelectedItemExProperty);
        }
        public static void SetSelectedItemEx(TreeView target, object value)
        {
            target.SetValue(SelectedItemExProperty, value);
            var treeViewItemToSelect = GetTreeViewItem(target, value);
            if (treeViewItemToSelect == null)
            {
                if (target.SelectedItem == null)
                    return;
                var treeViewItemToUnSelect = GetTreeViewItem(target, target.SelectedItem);
                treeViewItemToUnSelect.IsSelected = false;
            }
            else
                treeViewItemToSelect.IsSelected = true;
        }
        public static void OnSelectedItemExChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var treeView = depObj as TreeView;
            if (treeView == null)
                return;
            if (!isRegisteredToSelectionChanged.Contains(treeView))
            {
                treeView.SelectedItemChanged += TreeView_SelectedItemChanged;
                isRegisteredToSelectionChanged.Add(treeView);
            }
        }
        
        #endregion
        private static void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var treeView = (TreeView)sender;
            SetSelectedItemEx(treeView, e.NewValue);
        }
        #region Helper Structures & Methods
        public class MyVirtualizingStackPanel : VirtualizingStackPanel
        {
            /// <summary>
            /// Publically expose BringIndexIntoView.
            /// </summary>
            public void BringIntoView(int index)
            {
                BringIndexIntoView(index);
            }
        }
        /// <summary>Recursively search for an item in this subtree.</summary>
        /// <param name="container">The parent ItemsControl. This can be a TreeView or a TreeViewItem.</param>
        /// <param name="item">The item to search for.</param>
        /// <returns>The TreeViewItem that contains the specified item.</returns>
        private static TreeViewItem GetTreeViewItem(ItemsControl container, object item)
        {
            if (container != null)
            {
                if (container.DataContext == item)
                {
                    return container as TreeViewItem;
                }
                // Expand the current container
                if (container is TreeViewItem && !((TreeViewItem)container).IsExpanded)
                {
                    container.SetValue(TreeViewItem.IsExpandedProperty, true);
                }
                // Try to generate the ItemsPresenter and the ItemsPanel.
                // by calling ApplyTemplate.  Note that in the 
                // virtualizing case even if the item is marked 
                // expanded we still need to do this step in order to 
                // regenerate the visuals because they may have been virtualized away.
                container.ApplyTemplate();
                ItemsPresenter itemsPresenter =
                    (ItemsPresenter)container.Template.FindName("ItemsHost", container);
                if (itemsPresenter != null)
                {
                    itemsPresenter.ApplyTemplate();
                }
                else
                {
                    // The Tree template has not named the ItemsPresenter, 
                    // so walk the descendents and find the child.
                    itemsPresenter = FindVisualChild<ItemsPresenter>(container);
                    if (itemsPresenter == null)
                    {
                        container.UpdateLayout();
                        itemsPresenter = FindVisualChild<ItemsPresenter>(container);
                    }
                }
                Panel itemsHostPanel = (Panel)VisualTreeHelper.GetChild(itemsPresenter, 0);
                // Ensure that the generator for this panel has been created.
                UIElementCollection children = itemsHostPanel.Children;
                MyVirtualizingStackPanel virtualizingPanel =
                    itemsHostPanel as MyVirtualizingStackPanel;
                for (int i = 0, count = container.Items.Count; i < count; i++)
                {
                    TreeViewItem subContainer;
                    if (virtualizingPanel != null)
                    {
                        // Bring the item into view so 
                        // that the container will be generated.
                        virtualizingPanel.BringIntoView(i);
                        subContainer =
                            (TreeViewItem)container.ItemContainerGenerator.
                            ContainerFromIndex(i);
                    }
                    else
                    {
                        subContainer =
                            (TreeViewItem)container.ItemContainerGenerator.
                            ContainerFromIndex(i);
                        // Bring the item into view to maintain the 
                        // same behavior as with a virtualizing panel.
                        subContainer.BringIntoView();
                    }
                    if (subContainer != null)
                    {
                        // Search the next level for the object.
                        TreeViewItem resultContainer = GetTreeViewItem(subContainer, item);
                        if (resultContainer != null)
                        {
                            return resultContainer;
                        }
                        else
                        {
                            // The object is not under this TreeViewItem
                            // so collapse it.
                            subContainer.IsExpanded = false;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>Search for an element of a certain type in the visual tree.</summary>
        /// <typeparam name="T">The type of element to find.</typeparam>
        /// <param name="visual">The parent element.</param>
        /// <returns></returns>
        private static T FindVisualChild<T>(Visual visual) where T : Visual
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                Visual child = (Visual)VisualTreeHelper.GetChild(visual, i);
                if (child != null)
                {
                    T correctlyTyped = child as T;
                    if (correctlyTyped != null)
                    {
                        return correctlyTyped;
                    }
                    T descendent = FindVisualChild<T>(child);
                    if (descendent != null)
                    {
                        return descendent;
                    }
                }
            }
            return null;
        }
        
        #endregion
    }
