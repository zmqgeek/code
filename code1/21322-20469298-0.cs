    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    
    namespace WindowsFormsApplication2
    {
        public partial class Form1 : Form
        {
            public Form1()
            {
                InitializeComponent();
            }
    
            private void Form1_Load(object sender, EventArgs e)
            {
                foreach (FontFamily fam in FontFamily.Families)
                {
                    listBox1.Items.Add(fam.Name);
                }
                listBox1.DrawMode = DrawMode.OwnerDrawFixed; // 属性里设置
    
            }
    
            private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
            {
                e.DrawBackground();
                e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), new Font(listBox1.Items[e.Index].ToString(), listBox1.Font.Size), Brushes.Black, e.Bounds);
                //e.DrawFocusRectangle();
            }
        }
    }
    
    ![enter image description here][1]
    
    
      [1]: http://i.stack.imgur.com/HsBKz.png`enter code here`
