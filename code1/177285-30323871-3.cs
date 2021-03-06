    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    
    namespace ListBoxReorderDemo
    {
        public class Item
        {
            public string Name { get; set; }
            public Item(string name)
            {
                this.Name = name;
            }
        }
    
        public partial class Window1 : Window
        {
            private Point _dragStartPoint;
            
            private T FindVisualParent<T>(DependencyObject child)
                where T : DependencyObject
            {
                var parentObject = VisualTreeHelper.GetParent(child);
                if (parentObject == null)
                    return null;
                T parent = parentObject as T;
                if (parent != null)
                    return parent;
                return FindVisualParent<T>(parentObject);
            }
      
            private IList<Item> _items = new ObservableCollection<Item>();
    
            public Window1()
            {
                InitializeComponent();
                
                _items.Add(new Item("1"));
                _items.Add(new Item("2"));
                _items.Add(new Item("3"));
                _items.Add(new Item("4"));
                _items.Add(new Item("5"));
                _items.Add(new Item("6"));
    
                listBox.DisplayMemberPath = "Name";
                listBox.ItemsSource = _items;
         
                listBox.PreviewMouseMove += ListBox_PreviewMouseMove;
    
                var style = new Style(typeof(ListBoxItem));
                style.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
                style.Setters.Add(
                    new EventSetter(
                        ListBoxItem.PreviewMouseLeftButtonDownEvent,
                        new MouseButtonEventHandler(ListBoxItem_PreviewMouseLeftButtonDown)));
                style.Setters.Add(
                        new EventSetter(
                            ListBoxItem.DropEvent, 
                            new DragEventHandler(ListBoxItem_Drop)));
                listBox.ItemContainerStyle = style;
            }
    
            private void ListBox_PreviewMouseMove(object sender, MouseEventArgs e)
            {
                Point point = e.GetPosition(null);
                Vector diff = _dragStartPoint - point;
                if (e.LeftButton == MouseButtonState.Pressed &&
                    (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                        Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                {
                    var lb = sender as ListBox;
                    var lbi = FindVisualParent<ListBoxItem>(((DependencyObject)e.OriginalSource));
                    if (lbi != null)
                    {
                        DragDrop.DoDragDrop(lbi, lbi.DataContext, DragDropEffects.Move);
                    }
                }
            }
            private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                _dragStartPoint = e.GetPosition(null);
            }
    
            private void ListBoxItem_Drop(object sender, DragEventArgs e)
            {
                if (sender is ListBoxItem)
                {
                    var source = e.Data.GetData(typeof(Item)) as Item;
                    var target = ((ListBoxItem)(sender)).DataContext as Item;
            
                    int sourceIndex = listBox.Items.IndexOf(source);
                    int targetIndex = listBox.Items.IndexOf(target);
    
                    Move(source, sourceIndex, targetIndex);
                }
            }
            
            private void Move(Item source, int sourceIndex, int targetIndex)
            {
                if (sourceIndex < targetIndex)
                {
                    _items.Insert(targetIndex + 1, source);
                    _items.RemoveAt(sourceIndex);
                }
                else
                {
                    int removeIndex = sourceIndex + 1;
                    if (_items.Count + 1 > removeIndex)
                    {
                        _items.Insert(targetIndex, source);
                        _items.RemoveAt(removeIndex);
                    }
                }
            }
        }
    }
