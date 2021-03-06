        public void selectTreeNodeFromPath(string path)
        {
            // set up some delimters to split our path on.
            char[] delimiters = new char[] { '\\' };
            // split the array and store the values inside a string array.
            string[] pathArray = path.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            // clean up this array.
            ensurePathArrayAccuracy(ref pathArray);
            // a simple celing variable.
            int numberOfLvlsToProbe = pathArray.Length;
            // a variable for to keep an eye on the level of the TreeNode.
            int currentLevel = 0;
            // this collection always get re-populated each iteration.
            TreeNodeCollection globalTreeNCollection = treeDrives.Nodes;
            do
            {
                // start iterating through the global tree node collection.
                foreach (TreeNode rootNode in globalTreeNCollection)
                {
                    // only set the level if the node level is less!
                    if (rootNode.Level < pathArray.Length)
                    {
                        currentLevel = rootNode.Level;
                        // the 'currentLevel' variable can also be used to help index the 'pathArray' to make comparisons straightforward with current node.
                        if (rootNode.Text == pathArray[currentLevel])
                        {
                            // update our control variables and start again, the next level down.
                            globalTreeNCollection = rootNode.Nodes;
                            // once we have found the node then ...
                            break;
                        }                       
                    }
                    else // this portion of code means we are at the end of the 'pathArray.'
                    { 
                        treeDrives.SelectedNode = rootNode;
                        //treeDrives.SelectedNode.EnsureVisible();
                        // to make sure the loop ends ... we need to update the currentLevel counter
                        // to allow the loop to end.
                        currentLevel = numberOfLvlsToProbe;
                        break;             
                    }
                }
            }
            // if the 'currentLevel' is less than the 'numberOfLvlsToProbe' then we need
            // to keep on looping till we get to the end.
            while (currentLevel < numberOfLvlsToProbe);
