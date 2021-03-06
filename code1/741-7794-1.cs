        protected override void OnPreviewTextInput(System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = AreAllValidNumericChars(e.Text);
            base.OnPreviewTextInput(e);
        }
        private bool AreAllValidNumericChars(string str)
        {
            foreach(char c in str)
            {
                if(!Char.IsNumber(ch)) return false;
            }
            return true;
        }
