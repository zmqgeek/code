    public class TreeItem : INotifyPropertyChanged
    {
        public string Name { get; set; }
        // ...
    }
    public class Folder : TreeItem
    {
        public ObservableCollection<TreeItem> Items { get; private set; }
        public Folder()
        {
            this.Items = new ObservableCollection<TreeItem>();
        }
        // ...
    }
