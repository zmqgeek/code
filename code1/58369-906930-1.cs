    Public Class Form1
        Public Enum Test
            pete
            jack
            fran
            bill
        End Enum
        Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ComboBox1.DataSource = [Enum].GetValues(GetType(Test))
            ComboBox1.SelectedIndex = ComboBox1.FindStringExact("jack")
            ComboBox1.SelectedIndex = ComboBox1.FindStringExact(Test.jack.ToString())
            ComboBox1.SelectedIndex = ComboBox1.FindStringExact([Enum].GetName(GetType(Test), Test.jack))
            ComboBox1.SelectedItem = Test.bill
        End Sub
    End Class
