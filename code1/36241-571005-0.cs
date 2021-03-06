    public class TvShowsDataSource
    {
    	public ObservableCollection<Show> Shows { get; set; }
    	
    	public void AddItems(IEnumberable<Show> shows)
    	{
    		Shows.AddRange(shows);
    	}
    	
    	public void LoadShowsAsync(Dispatcher dispatcher)
    	{
    		new Thread(() => LoadShows(dispatcher)).Start();
    	}
    	
    	private void LoadShows(Dispatcher dispatcher)
    	{
    		if (dispatcher == null)
    			throw new ArgumentNullException("dispatcher");
    	
    		using (var context = new Data.TVShowDataContext())
    		{
    		    var list = from show in context.Shows
    		               select show;
    		
    		    dispatcher.Invoke(AddItems(list));
    		}
    	}
    }
    
    public class UserControl1
    {
    	private readonly TvShowsDataSource tvShowsDataSource;
    	public UserControl1() : this(new TvShowsDataSource()) {}
    	
    	public UserControl1(TvShowsDataSource tvShowsDataSource )
    	{
    		this.tvShowsDataSource = tvShowsDataSource;
    		listShow.ItemsSource = tvShowsDataSource.Shows;
    		this.Loaded += UserControl1_Loaded;
    	}
    	
    	public void UserControl1_Loaded(object sender, RoutedEventArgs e)
    	{
    		if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
    		{
    			tvShowsDataSource.LoadAsync(this.Dispatcher);
    		}
    	}
    }
