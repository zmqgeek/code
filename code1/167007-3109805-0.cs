    using System;
    using System.Data;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    
    namespace Whatever
    {
     /// 
     /// This class provides a method to write a dataset to the HttpResponse as
     /// an excel file. 
     /// 
     public class ExcelExport
     {
      public static void ExportDataSetToExcel(DataSet ds, string filename)
      {
       HttpResponse response = HttpContext.Current.Response;
       
       // first let's clean up the response.object
       response.Clear();
       response.Charset = "";
       
       // set the response mime type for excel
       response.ContentType = "application/vnd.ms-excel";
       response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
       
       // create a string writer
       using (StringWriter sw = new StringWriter())
       {
        using (HtmlTextWriter htw = new HtmlTextWriter(sw))
        {
         // instantiate a datagrid
         DataGrid dg = new DataGrid();
         dg.DataSource = ds.Tables[0];
         dg.DataBind();
         dg.RenderControl(htw);
         response.Write(sw.ToString());
         response.End(); 
        }
       }
      }
     }
    }
