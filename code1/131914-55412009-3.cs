       public void ReduceImageSize(double scaleFactor, String sourcePath, string targetPath, string Image_name)
    {
        using (var image = System.Drawing.Image.FromFile(sourcePath))
               {
      
            //var newWidth = (int)(image.Width  * scaleFactor);
            //var newHeight = (int)(image.Height * scaleFactor);
            var newWidth = (int)(500 * scaleFactor);
            var newHeight = (int)(500 * scaleFactor);
            var thumbnailImg = new Bitmap(newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new System.Drawing.RectangleF (0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            MemoryStream s = new MemoryStream();
            thumbnailImg.Save(s, System.Drawing.Imaging.ImageFormat.Png);
            s.Position = 0;
            byte[] image2 = new byte[525000];// 512kb =525000  
            s.Read(image2, 0, image2.Length);
            Guid guid = Guid.NewGuid();
            string Server_MapPath = Server.MapPath("~/Image Compress/" + Image_name + guid.ToString() + ".PNG");//Your Compressor Image Save Path
            System.IO.FileStream fs = new System.IO.FileStream(Server_MapPath, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            fs.Write(image2, 0, image2.Length);
          
        }
    }
