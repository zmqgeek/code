    protected void Page_Load(object sender, EventArgs e)
    {
        MemoryStream ms;
        
        using (ms = new MemoryStream())
        {
           PdfWriter writer = PdfWriter.GetInstance(myPdfDoc, ms);
        
           Response.ContentType = "application/pdf";
           Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
           Response.OutputStream.Flush();
           Response.OutputStream.Close();
    
        }
    }
