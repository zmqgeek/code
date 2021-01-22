            public bool NextTab(){...}
            public bool PreviousTab(){...}
            protected override bool ProcessCmdKey(ref Message message, Keys keys)
            {
                switch (keys)
                {
                    case Keys.Control | Keys.Tab:
                        NextTab();
                        return true;
                    case Keys.Control | Keys.Shift | Keys.Tab:
                        PreviousTab();
                        return true;
                }
                return base.ProcessCmdKey(ref message, keys);
            }
        }
        public class mySecondForm : System.Windows.Forms.Form
        {
            ...
        }
    }
.