    public MainWindow()
            {
                InitializeComponent();
                this.CommandBindings.Add(
                    new CommandBinding(TestControl.IncrementCommand, new ExecutedRoutedEventHandler(TestControl.IncrementMin)));
                this.CommandBindings.Add(
                    new CommandBinding(TestControl.DecrementCommand, new ExecutedRoutedEventHandler(TestControl.DecrementMin)));            
            }
