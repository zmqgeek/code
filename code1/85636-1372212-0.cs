    Public Class Form1
        Inherits System.Windows.Forms.Form
    
        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub
    
        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer
    
        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.ListBox1 = New System.Windows.Forms.ListBox
            Me.SuspendLayout()
            '
            'ListBox1
            '
            Me.ListBox1.FormattingEnabled = True
            Me.ListBox1.Location = New System.Drawing.Point(13, 13)
            Me.ListBox1.Name = "ListBox1"
            Me.ListBox1.Size = New System.Drawing.Size(248, 147)
            Me.ListBox1.TabIndex = 0
            '
            'Form1
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(292, 266)
            Me.Controls.Add(Me.ListBox1)
            Me.Name = "Form1"
            Me.Text = "Form1"
            Me.ResumeLayout(False)
    
        End Sub
        Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    
        Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.ListBox1.UseCustomTabOffsets = True
            Me.ListBox1.CustomTabOffsets.AddRange(New Integer() {40, 40, 40})
            Me.ListBox1.Items.Add("a" + vbTab + "b" + vbTab + "c")
        End Sub
    
    End Class
