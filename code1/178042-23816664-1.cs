    byte[] bytesPDF = System.IO.File.ReadAllBytes(@"C:\sample.pdf");
            if (bytesPDF != null)
            {
                Response.AddHeader("content-disposition", "attachment;filename= DownloadSample.pdf");
                Response.ContentType = "application/octectstream";
                Response.BinaryWrite(bytesPDF);
                Response.End();
            }
