    this.toolStrip1 = new System.Windows.Forms.ToolStrip();
    this.toolStrip1.Location = new System.Drawing.Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new System.Drawing.Size(444, 25);
    this.toolStrip1.TabIndex = 0;
    this.toolStrip1.Text = "toolStrip1";
    object O = global::WindowsFormsApplication1.Properties.Resources.ResourceManager.GetObject("best_robust_ghost");
    ToolStripButton btn = new ToolStripButton("m1");
    btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
    btn.Image = (Image)O;
    this.toolStrip1.Items.Add(btn);