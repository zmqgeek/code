    namespace MyProject.Core
    {
    	/// <remarks>
        /// You can create partial class with the same name in another file to add custom properties
        /// </remarks>
    	public static partial class SiteSettings 
    	{
    		/// <summary>
            /// Static constructor to initialize properties
            /// </summary>
    		static SiteSettings()
    		{
    			var settings = System.Configuration.ConfigurationManager.AppSettings;
    			PageSize = Convert.ToInt32( settings["PageSize"] );
    			CurrentTheme = ( settings["CurrentTheme"] );
    			IsShowSomething = Convert.ToBoolean( settings["IsShowSomething"] );
    		}
    		
    		/// <summary>
    		/// PageSize configuration value
    		/// </summary>
    		public static int PageSize { get; private set; }
    
    		/// <summary>
    		/// CurrentTheme configuration value
    		/// </summary>
    		public static string CurrentTheme { get; private set; }
    
    		/// <summary>
    		/// IsShowSomething configuration value
    		/// </summary>
    		public static bool IsShowSomething { get; private set; }
    
    	}
    }
