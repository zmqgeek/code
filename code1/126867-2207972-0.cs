    public static class Extensions
    {
        public static void MoveUp(this TreeNode node)
        {
            TreeNode parent = node.Parent;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index - 1, node);
    
                    // bw : add this line to restore the originally selected node as selected
                    node.TreeView.SelectedNode = node;
                }
            }
        }
    
        public static void MoveDown(this TreeNode node)
        {
            TreeNode parent = node.Parent;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index < parent.Nodes.Count - 1)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index + 1, node);
    
                    // bw : add this line to restore the originally selected node as selected
                    node.TreeView.SelectedNode = node;
                }
            }
        }
    }
