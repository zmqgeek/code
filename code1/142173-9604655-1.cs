    Private ClientControl As MdiClient
    
        Public Sub New()
            InitializeComponent()
    
            ClientControl = Nothing
            For Each Ctl As Control In Me.Controls
                ClientControl = TryCast(Ctl, MdiClient)
                If ClientControl IsNot Nothing Then Exit For
            Next
        End Sub
    
    'iN FORM LOAD
    
    ClientControl.BackColor = Color.Cyan
