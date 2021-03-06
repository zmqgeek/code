    public static class TbHelper
    {
        // Method for use
        public static void SelectAllTextOnEnter(TextBox Tb)
        {
            Tb.Enter += new EventHandler(Tb_Enter);
            Tb.Click += new EventHandler(Tb_Click);
        }
        private static TextBox LastTb;
        private static void Tb_Enter(object sender, EventArgs e)
        {
            var Tb = (TextBox)sender;
            Tb.SelectAll();
            LastTb = Tb;
        }
        private static void Tb_Click(object sender, EventArgs e)
        {
            var Tb = (TextBox)sender;
            if (LastTb == Tb)
            {
                Tb.SelectAll();
                LastTb = null;
            }
        }
    }
