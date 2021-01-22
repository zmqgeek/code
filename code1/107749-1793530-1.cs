    %h2= ViewData.Model.Product.ProductName
    %form{action='#{Url.Action("Update", new { ID=ViewData.Model.Product.ProductID \})}' method="post"}
      %table
        %tr
          %td Name:
          %td= Html.TextBox("ProductName", ViewData.Model.Product.ProductName)
        %tr
          %td Category:
          %td= Html.DropDownList("CategoryID", ViewData.Model.Categories, (string)null)
        %tr
          %td Supplier:
          %td= Html.DropDownList("SupplierID", ViewData.Model.Suppliers, (string)null)
        %tr
          %td Unit Price:
          %td= Html.TextBox("UnitPrice", ViewData.Model.Product.UnitPrice.ToString())
      %p
      - Html.RenderPartial(@"_Button")
