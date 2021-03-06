    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    
    namespace DemoWindowApp
    {
        public partial class frmDemo : Form
        {
            Rectangle rec;
            bool isInsideRec;
            public frmDemo()
            {
                InitializeComponent();
            }
    
            private void frmDemo_Load(object sender, EventArgs e)
            {
                isInsideRec = false;
                rec = new Rectangle(150,100,100,100);
            }
    
            private void frmDemo_Paint(object sender, PaintEventArgs e)
            {
                
                Pen p = new Pen(Color.Blue);
                Graphics g = e.Graphics;
    
                g.DrawRectangle(p,rec);
            }
    
            private void frmDemo_MouseMove(object sender, MouseEventArgs e)
            {
                if(
                    e.X> rec.Left 
                    && e.X< rec.Right
                    && e.Y > rec.Top
                    && e.Y < rec.Bottom
                    )
                {
                    isInsideRec = true;
                    Cursor = Cursors.Cross;
                }else
                {
                    isInsideRec = false;
                    Cursor = Cursors.Default;
                }
            }
    
            private void frmDemo_Click(object sender, EventArgs e)
            {   
                if(isInsideRec)
                {
                    //mouse position adjust for window postion and border size.
                    //you may have to adjust the borders depending your
                    //windows theme
                    int x = MousePosition.X - this.Left -  4;
                    int y = MousePosition.Y - this.Top  - 29;
    
                    Graphics g = this.CreateGraphics();
                    Pen p = new Pen(Color.Black);
                    Point p1 = new Point(x-10,y-10);
                    Point p2 = new Point(x+10,y+10);
                    Point p3 = new Point(x-10,y+10);
                    Point p4 = new Point(x+10,y-10);
    
                    g.DrawLines(p,new Point[]{p1,p2});
                    g.DrawLines(p,new Point[]{p3,p4});
                }
            }
        }
    }
