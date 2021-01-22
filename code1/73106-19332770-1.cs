    private static void PopulateTreeView(TreeView treeView, string[] paths, char pathSeparator)
            {
                TreeNode lastNode = null;
                string subPathAgg;
                foreach (string path in paths)
                {
                    subPathAgg = string.Empty;
                    foreach (string subPath in path.Split(pathSeparator))
                    {
                        subPathAgg += subPath + pathSeparator;
                        TreeNode[] nodes = treeView.Nodes.Find(subPathAgg, true);
                        if (nodes.Length == 0)
                            if (lastNode == null)
                                lastNode = treeView.Nodes.Add(subPathAgg, subPath);
                            else
                                lastNode = lastNode.Nodes.Add(subPathAgg, subPath);
                        else
                            lastNode = nodes[0];
                    }
                    lastNode = null; // This is the place code was changed
    
                }
            }
