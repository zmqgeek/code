    if (this.DialogResult == DialogResult.Cancel)
            {
            }
            else
            {
                switch (e.CloseReason)
                {
                    case CloseReason.UserClosing:
                        e.Cancel = true;
                        break;
                }
            }
