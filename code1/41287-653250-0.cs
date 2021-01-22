        string assName = Assembly.GetExecutingAssembly().GetName().Name;
        StringBuilder sb = new StringBuilder();
        sb.Append("<DataTemplate ");
        sb.Append("xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' ");
        sb.Append("xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' ");
        sb.Append("xmlns:src='clr-namespace:WpfToolkitDataGridTester;assembly=" + assName + "' >");
        sb.Append("<DataTemplate.Resources>");
        sb.Append("<src:FooConverter x:Key='fooConverter' />");
        sb.Append("</DataTemplate.Resources>");
        sb.Append("<TextBlock ");
        sb.Append("Foreground='{Binding Candidates[" + i + "].CandidateType,Converter={StaticResource fooConverter}}' ");
        sb.Append("Text='{Binding Candidates[" + i + @"].Name}' />");
        sb.Append("</DataTemplate>");
        var template = (DataTemplate)XamlReader.Parse(sb.ToString());