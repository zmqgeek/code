    private const int SPI_SETDESKWALLPAPER = 20;
    private const int SPIF_UPDATEINIFILE = 0x01;
    private const int SPIF_SENDWININICHANGE = 0x02;
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
    public enum Style : int
    {
        Tiled,
        Centered,
        Stretched
    }
    /// <summary>
    /// Loops numFrames times, animating the desktop background as the given gif.
    /// Remember this will sorta bog down your computer, and probably isn't best to be running 24/7.
    /// If numFrames is negative this will loop forever
    /// </summary>
    /// <param name="gifPath">The gif to be animated</param>
    /// <param name="transparencyReplace">If the gif has transparency, it will be "replaced" with this color.</param>
    /// <param name="framesPerSecond">How many frames to play per second. This is a max: most likely it will be a little slower than this especially at first.</param>
    /// <param name="style">Whether to tile, center, or stretch each gif frame as it's played.</param>
    /// <param name="numFrames">The number of frames to play. If negative, this method will loop forever.</param>
    public static void SetDesktopBackgroundAsGifAsync(string gifPath, System.Drawing.Color transparencyReplace, int framesPerSecond, Style style, int numFrames)
    {
        Thread workerThread = new Thread(() => SetDesktopBackgroundAsGif(gifPath, transparencyReplace, framesPerSecond, style, numFrames));
        workerThread.Start();
    }
    /// <summary>
    /// Loops numFrames times, animating the desktop background as the given gif.
    /// Remember this will sorta bog down your computer, and probably isn't best to be running 24/7.
    /// If num frames is negative this will loop forever. 
    //// <summary>
    /// <param name="gifPath">The gif to be animated</param>
    /// <param name="backgroundImage">Image to render the gif on top of (because of transparency)</param>
    /// <param name="framesPerSecond">How many frames to play per second. This is a max: most likely it will be a little slower than this.</param>
    /// <param name="style">Whether to tile, center, or stretch each gif frame as it's played.</param>
    /// <param name="numFrames">The number of frames to play. If negative, this method will loop forever.</param>
    public static void SetDesktopBackgroundAsGifAsync(string gifPath, System.Drawing.Image backgroundImage, int framesPerSecond, Style style, int numFrames)
    {
        Thread workerThread = new Thread(() => SetDesktopBackgroundAsGif(gifPath, backgroundImage, framesPerSecond, style, numFrames));
        workerThread.Start();
    }
    /// <summary>
    /// Loops numFrames times, animating the desktop background as the given gif.
    /// Remember this will sorta bog down your computer, and probably isn't best to be running 24/7. 
    /// if numFrames is negative this will loop forever
    /// </summary>
    /// <param name="gifPath">The gif to be animated</param>
    /// <param name="transparencyReplace">If the gif has transparency, it will be "replaced" with this color.</param>
    /// <param name="framesPerSecond">How many frames to play per second. This is a max: most likely it will be a little slower than this.</param>
    /// <param name="style">Whether to tile, center, or stretch each gif frame as it's played.</param>
    /// <param name="numFrames">The number of frames to play. If negative, this method will loop forever.</param>
    public static void SetDesktopBackgroundAsGif(string gifPath, System.Drawing.Color transparencyReplace, int framesPerSecond, Style style, int numFrames)
    {
        if (!File.Exists(gifPath))
            throw new Exception("Given gif: '" + gifPath + "' not found");
        FileStream gifFile = new FileStream(gifPath, FileMode.Open);
        GifBitmapDecoder gifDecoder = new GifBitmapDecoder(gifFile, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
        if (gifDecoder.Frames.Count == 0)
            throw new Exception("No frames in given gif");
        Bitmap backgroundImage = new Bitmap(gifDecoder.Frames[0].PixelWidth, gifDecoder.Frames[0].PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            
        using(Graphics g = Graphics.FromImage(backgroundImage))
        {
            g.FillRectangle(new System.Drawing.SolidBrush(transparencyReplace), 0, 0, gifDecoder.Frames[0].PixelWidth, gifDecoder.Frames[0].PixelHeight);
        }
        
        gifFile.Close();
        
        SetDesktopBackgroundAsGif(gifPath, backgroundImage, framesPerSecond, style, numFrames);
    }
    /// <summary>
    /// Loops infinitely, animating the desktop background as the given gif.
    /// Remember this will sorta bog down your computer, and probably isn't best to be running 24/7. 
    /// </summary>
    /// <param name="gifPath">The gif to be animated</param>
    /// <param name="backgroundImage">Image to render the gif on top of (because of transparency)</param>
    /// <param name="framesPerSecond">How many frames to play per second. This is a max: most likely it will be a little slower than this.</param>
    /// <param name="style">Whether to tile, center, or stretch each gif frame as it's played.</param>
    /// <param name="numFrames">The number of frames to play. If negative, this method will loop forever.</param>
    private static void SetDesktopBackgroundAsGif(string gifPath, System.Drawing.Image backgroundImage, int framesPerSecond, Style style, int numFrames)
    {
        if (!File.Exists(gifPath))
            throw new Exception("Given gif: '" + gifPath + "' not found");
        FileStream gifFile = new FileStream(gifPath, FileMode.Open);
            
        GifBitmapDecoder gifDecoder = new GifBitmapDecoder(gifFile, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
        if (gifDecoder.Frames.Count == 0)
            throw new Exception("No frames in given gif");
        Console.WriteLine("Saving frames to temporary files:");
        int numFramesSoFar = 0;
        for (int i = 0; i < gifDecoder.Frames.Count; i++)
        {
            BitmapFrame gifFrame = gifDecoder.Frames[i];
            PngBitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(gifFrame);
            MemoryStream pngStream = new MemoryStream();
            pngEncoder.Save(pngStream);
            Image frameImage = Image.FromStream(pngStream);
            Bitmap bmp = new Bitmap(frameImage.Width, frameImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(backgroundImage, 0, 0);
                g.DrawImageUnscaled(frameImage, 0, 0);
            }
            string tempPath = Path.Combine(Path.GetTempPath(), gifPath + i + ".bmp");
            bmp.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);
            Console.WriteLine("Saved frame " + i);
            numFramesSoFar++;
            if (numFrames >= 0 && numFramesSoFar >= numFrames) break;
        }
        Console.WriteLine("Setting frames to desktop background at about " + framesPerSecond + " FPS");
        // 1.0/... to convert to seconds per frame (instead of frames per second)
        // * 1000 to convert to milliseconds per frame
        // * 1000 to convert to microseconds per frame
        // * 10 to convert to 0.1s of microseconds per frame = 100s of nanoseconds per frame
        long ticksBetweenFrames = (long)Math.Round(1.0 / framesPerSecond) * 1000*1000*10;
        Stopwatch timer = new Stopwatch();
        timer.Start();
        numFramesSoFar = 0;
        while(numFrames < 0 || numFramesSoFar < numFrames)
        {
            for (int i = 0; i < gifDecoder.Frames.Count; i++)
            {
                // Sleep until we're at the desired frame rate, if needed.
                if(ticksBetweenFrames > timer.ElapsedTicks)
                    Thread.Sleep(new TimeSpan(Math.Max(0, ticksBetweenFrames - timer.ElapsedTicks)));
                timer.Restart();
                // From http://stackoverflow.com/a/1061682/2924421
                string filePath = Path.Combine(Path.GetTempPath(), "wallpaper" + i + ".bmp");
                    
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
                if (style == Style.Stretched)
                {
                    key.SetValue(@"WallpaperStyle", 2.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                }
                if (style == Style.Centered)
                {
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                }
                if (style == Style.Tiled)
                {
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 1.ToString());
                }
                SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0,
                    filePath,
                    SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
                
                numFramesSoFar++;
                if (numFrames >= 0 && numFramesSoFar >= numFrames) break;
            }
        }
        
        gifFile.Close();
    }
