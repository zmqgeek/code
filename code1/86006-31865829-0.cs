    public partial class VerticalLabel_UserControl : UserControl
    {
        private IComponentChangeService _changeService;
        private string strPropertyText = "Vertical Text";
        public VerticalLabel_UserControl()
        {
            InitializeComponent();
        }
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text { get { return base.Text; } set { base.Text = value; this.Invalidate(); } }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            strPropertyText =  this.Text;
        }
        private void GenerateTexture()
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.EllipsisCharacter;
            Bitmap img = new Bitmap(this.Height, this.Width);
            Graphics G = Graphics.FromImage(img);
            G.Clear(this.BackColor);
            SolidBrush brush_text = new SolidBrush(this.ForeColor);
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            G.DrawString(this.strPropertyText, this.Font, brush_text, new Rectangle(0, 0, img.Width, img.Height), format);
            brush_text.Dispose();
            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            this.BackgroundImage = img;
        }
        public override System.ComponentModel.ISite Site
        {
            get
            {
                return base.Site;
            }
            set
            {
                _changeService =         (IComponentChangeService)GetService(typeof(IComponentChangeService));
                if (_changeService != null)
                    _changeService.ComponentChanged -= new ComponentChangedEventHandler(OnComponentChanged);
                    base.Site = value;
                    if (!DesignMode)
                        return;
                    _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
                if (_changeService != null)
                    _changeService.ComponentChanged += new ComponentChangedEventHandler(OnComponentChanged);
            }
        }
        private void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
        {
            VerticalLabel_UserControl label = ce.Component as VerticalLabel_UserControl;
            if (label == null || !label.DesignMode)
                return;
            if (((IComponent)ce.Component).Site == null || ce.Member == null || ce.Member.Name != "Text")
                return;
            label.Text = strPropertyText;
            GenerateTexture();
        }
    }
