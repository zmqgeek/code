    TextBlock tb = new TextBlock();
    tb.Inlines.Add(new Run("Background indicates packet repeat status:"));
    tb.Inlines.Add(new LineBreak());
    tb.Inlines.Add(new LineBreak());
    Run r = new Run("White");
    r.Background = Brushes.White;
    r.ToolTip = "This word has a White background";
    tb.Inlines.Add(r);
    tb.Inlines.Add(new Run("\t= Identical Packet received at this time."));
    tb.Inlines.Add(new LineBreak());
    r = new Run("SkyBlue");
    r.ToolTip = "This word has a SkyBlue background";
    r.Background = new SolidColorBrush(Colors.SkyBlue);
    tb.Inlines.Add(r);
    tb.Inlines.Add(new Run("\t= Original Packet received at this time."));
    myControl.Content = tb;
