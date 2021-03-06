    private byte[] PopulatePersonPDF(Person obj)
    {
       MemoryStream ms = new MemoryStream();
       PdfStamper Stamper = null;
    
       try
       {
          PdfCopyFields Copier = new PdfCopyFields(ms);
          Copier.AddDocument(GetReader("Person.pdf"));
          Copier.Close();
    
          PdfReader docReader = new PdfReader(ms.ToArray());
          ms = new MemoryStream();
          Stamper = new PdfStamper(docReader, ms);
          AcroFields Fields = Stamper.AcroFields;
          Fields.SetField("FirstName", obj.FirstName);
       }
       finally
       {
          if (Stamper != null)
          {
             Stamper.Close();
          }
       }
       return ms.ToArray();
    }
