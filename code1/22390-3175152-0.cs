    // Convert the image in resources to a Stream
    Stream ms = new MemoryStream()
    Properties.Resources.MyImage.Save(ms,ImageFormat.Png);
    
    // Create a BitmapImage with the Stream.
    BitmapImage bitmap = new BitmapImage();
    bitmap.BeginInit();
    bitmap.StreamSource = ms;
    bitmap.EndInit();
    
    // Set as Source
    Source = bitmap;
