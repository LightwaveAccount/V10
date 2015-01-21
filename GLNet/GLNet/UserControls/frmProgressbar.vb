Public Class frmProgressbar

    Public Property ProgressText() As String
        Get
            Return Me.Label3.Text
        End Get
        Set(ByVal value As String)
            Me.Label3.Text = value
        End Set
    End Property

End Class