    public class UserTabControl<T> : TabPage
        where T : UserControl, new ()
    {
        private T _userControl;
        public T UserControl 
        { 
            get{ return _userControl;}
            set
            {          
                _userControl = value;
                OnUserControlChanged(EventArgs.Empty);
            }
        }
        public event EventHandler UserControlChanged;
        protected virtual void OnUserControlChanged(EventArgs e)
        {
            //add user control docked to tabpage
            this.Controls.Clear();      
            UserControl.Dock = DockStyle.Fill;
            this.Controls.Add(UserControl);
            if (UserControlChanged != null)
            {
                UserControlChanged(this, e);
            }
        }
        public UserTabControl() : this("UserTabControl")
        {
        }
        public UserTabControl(string text) 
            : this( new T(),text )
        {
        }
        public UserTabControl(T userControl) 
            : this(userControl, userControl.Name)
        {
        }
        public UserTabControl(T userControl, string tabtext)
            : base(tabtext)
        {
            InitializeComponent();
            UserControl = userControl;  
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UserTabControl
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Padding = new System.Windows.Forms.Padding(3);
            this.UseVisualStyleBackColor = true;
            this.ResumeLayout(false);
        }
    }
