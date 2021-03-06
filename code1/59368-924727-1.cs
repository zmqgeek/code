    <%@ Page Language="VB" AutoEventWireup="false" %>
    <%@ Import Namespace="System.Collections.Generic" %>
    
    <script runat="server">
      
      Private Shared addedItems As List(Of HtmlGenericControl)
    
      Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
          'On first load, instantiate the List.
          addedItems = New List(Of HtmlGenericControl)
        End If
      End Sub
      
      Protected Sub btn1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Add the previously created items to the UL list.
        'This step is necessary because
        '...the previously added items are lost on postback.
        For i As Integer = 0 To addedItems.Count - 1
          myList.Controls.Add(addedItems.Item(i))
        Next
        
        'Get the existing no. of items in the list
        Dim count As Integer = myList.Controls.Count
        
        'Create a new list item based on input in textbox.
        Dim li As HtmlGenericControl = CreateBulletItem()
        'Add the new list item at the end of the BulletedList.
        myList.Controls.AddAt(count, li)
        'Also add this newly created list item to the generic list.
        addedItems.Add(li)
      End Sub
      
      Private Function CreateBulletItem() As HtmlGenericControl
        Dim li As New HtmlGenericControl("li")
        li.InnerText = txtNewItem.Value
        li.Attributes("class") = "myItemClass"
    
        Return li
      End Function
    </script>
    
    <html>
    <head runat="server">
      <title>Test Page</title>
    </head>
    <body>
      <form id="form1" runat="server">
        <div>
          <ul id="myList" class='myClass' runat="server">
            <li runat="server" class="myItemClass">Item 1</li>
            <li runat="server" class="myItemClass">Item 2</li>
          </ul>
          <input type="text" id="txtNewItem" runat="server" />
          <asp:Button ID="btn1" runat="server" Text="Add" OnClick="btn1_Click" />
        </div>
      </form>
    </body>
    </html>
