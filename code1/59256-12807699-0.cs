    namespace MyNamespace
    {
        using System.Collections.Generic;
        using System.Configuration;
        /// <summary>
        /// Persistent store for my parameters.
        /// </summary>
        public class MySettings : ApplicationSettingsBase
        {
            /// <summary>
            /// The instance lock.
            /// </summary>
            private static readonly object InstanceLock = new object();
            /// <summary>
            /// The instance.
            /// </summary>
            private static MySettings instance;
            /// <summary>
            /// Prevents a default instance of the <see cref="MySettings"/> class 
            /// from being created.
            /// </summary>
            private MySettings()
            {
                // don't need to do anything
            }
            /// <summary>
            /// Gets the singleton.
            /// </summary>
            public static MySettings Instance
            {
                get
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new MySettings();
                        }
                    }
    
                    return instance;
                }
            }
            /// <summary>
            /// Gets or sets the parameters.
            /// </summary>
            [UserScopedSetting]
            [SettingsSerializeAs(SettingsSerializeAs.Binary)]
            public Dictionary<string, string> Parameters
            {
                get
                {
                    return (Dictionary<string, string>)this["Parameters"];
                }
    
                set
                {
                    this["Parameters"] = value;
                }
            }
        }
    }
