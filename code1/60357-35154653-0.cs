    	public class MdiClientPanel : Panel
	{
		private MdiClient _ctlClient = new MdiClient();
		// Callback event when a child is activated
		public delegate void ActivateHandler(object sender, MdiClientForm child);
		public event ActivateHandler OnChildActivated;
		/// <summary>
		/// The current active child form
		/// </summary>
		public Form ActiveMDIWnd
		{
			get;
			set;
		}
		/// <summary>
		/// List of available forms
		/// </summary>
		public List<MdiClientForm> ChildForms = new List<MdiClientForm>();
		/// <summary>
		/// Std constructor
		/// </summary>
		public MdiClientPanel()
		{
			base.Controls.Add(_ctlClient);
		}
		private Form _mdiForm = null;
		public Form MdiForm
		{
			get
			{
				if (_mdiForm == null)
				{
					_mdiForm = new Form();
					// Set the hidden _ctlClient field which is used to determine if the form is an MDI form
					System.Reflection.FieldInfo field = typeof(Form).GetField("ctlClient", 
						System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
					field.SetValue(_mdiForm, _ctlClient);
				}
				return _mdiForm;
			}
		}
		private void InitializeComponent()
		{
			SuspendLayout();
			ResumeLayout(false);
		}
		/// <summary>
		/// Add this Form to our list of children and set the MDI relationship up
		/// </summary>
		/// <param name="child">The new kid</param>
		/// <returns>The new kid</returns>
		public MdiClientForm AddChild(MdiClientForm child)
		{
			child.MyMdiContainer = this;
			child.MdiParent = MdiForm;
			ChildForms.Add(child);
			return child;
		}
		/// <summary>
		/// The user sent a "restore" command, so issue it to all children
		/// </summary>
		public void RestoreChildForms()
		{
			foreach (DataTableForm child in ChildForms)
			{
				child.WindowState = FormWindowState.Normal;
				child.MaximizeBox = true;
				child.MinimizeBox = true;
			}
		}
		/// <summary>
		/// Send the Activated message to the owner of this panel (if they are listening)
		/// </summary>
		/// <param name="child">The child that was just activated</param>
		public void ChildActivated(MdiClientForm child)
		{
			ActiveMDIWnd = child;
			if (OnChildActivated != null)
				OnChildActivated(this, child);
		}
		/// <summary>
		/// The child closed so remove them from our available form list
		/// </summary>
		/// <param name="child">The child that closed</param>
		public void ChildClosed(MdiClientForm child)
		{
			ChildForms.Remove(child);
		}
	}
	/// <summary>
	/// A wrapper class for any form wanting to be an MDI child of an MDI Panel
	/// </summary>
	public class MdiClientForm : Form
	{
		/// <summary>
		/// My parent MDI container
		/// </summary>
		public MdiClientPanel MyMdiContainer { get; set; }
		/// <summary>
		/// Standard Constructor
		/// </summary>
		public MdiClientForm()
		{
			Activated += OnFormActivated;
			FormClosed += OnFormClosed;
		}
		/// <summary>
		/// Reports back to the container when we close
		/// </summary>
		void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			MyMdiContainer.ChildClosed(this);
		}
		/// <summary>
		/// Reports back to the parent container when we are activated
		/// </summary>
		private void OnFormActivated(object sender, EventArgs e)
		{
			MyMdiContainer.ChildActivated(this);
		}
	}
