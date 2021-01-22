	// needs (F4): System.Windows.dll, System.Windows.Forms.dll, WindowsFormsIntegration.dll
	void Main()
	{		
		var wfHost1 = new System.Windows.Forms.Integration.WindowsFormsHost();
		wfHost1.Height=175; wfHost1.Width=175; wfHost1.Name="Picturebox1";
		wfHost1.HorizontalAlignment=System.Windows.HorizontalAlignment.Left;
		wfHost1.VerticalAlignment=System.Windows.VerticalAlignment.Top;
		System.Windows.Forms.PictureBox pBox1 = new System.Windows.Forms.PictureBox();
		wfHost1.Child = pBox1;
		pBox1.Paint += new System.Windows.Forms.PaintEventHandler(picturebox1_Paint);
		PanelManager.StackWpfElement(wfHost1, "Picture");
	} 
	
	// Define other methods and classes here
	public void picturebox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
	{
		// http://stackoverflow.com/a/14143574/1016343
		var path = @"C:\Users\Public\Pictures\Sample Pictures\Tulips.jpg";
		System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(path);
		System.Drawing.Point ulPoint = new System.Drawing.Point(0, 0);
		e.Graphics.DrawImage(bmp, ulPoint.X, ulPoint.Y, 175, 175);
	}
	
