    Public Property IList_Item(ByVal index As Integer) As Object Implements System.Collections.IList.Item
        Get
            Return DirectCast(innerArray(index), MyObject)
        End Get
        Set(ByVal value As Object)
            innerArray(index) = value
        End Set
    End Property
