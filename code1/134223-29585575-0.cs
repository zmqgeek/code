    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing.Drawing2D;
    namespace shapes
    {
        public partial class Form1 : Form
        {
            public Form1()
            {
                InitializeComponent();
            }
            private void Form1_Load(object sender, EventArgs e)
            {
                GraphicsPath gp = new GraphicsPath();
                gp.AddEllipse(0, 0, 100, 100);
                this.Region = new Region(gp);
            }
        }
    }
