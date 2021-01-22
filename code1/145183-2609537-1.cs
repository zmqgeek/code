    public partial class CustomLabel : Label
    {
        private TextRenderingHint _hint = TextRenderingHint.SystemDefault;   
        public TextRenderingHint TextRenderingHint
        {
            get { return this._hint; }
            set { this._hint = value; }
        }        
    
        protected override void OnPaint(PaintEventArgs pe)
        {            
            pe.Graphics.TextRenderingHint = TextRenderingHint;
            base.OnPaint(pe);
        }
    }
