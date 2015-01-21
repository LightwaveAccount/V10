Public Class AccountMainModel

    Private _AccountMainID As Integer
    Private _AccountMainCode As String
    Private _AccountMainTitle As String
    Private _AccountMainType As String
    Private _ActivityLog As ActivityLog

    Public Sub New()
        Me._ActivityLog = New ActivityLog

    End Sub

    Public Property ActivityLog() As ActivityLog

        Get
            Return Me._ActivityLog
        End Get

        Set(ByVal value As ActivityLog)
            Me._ActivityLog = value
        End Set

    End Property


    Public Property AccountMainID() As Integer

        Get
            Return _AccountMainID
        End Get

        Set(ByVal value As Integer)
            _AccountMainID = value
        End Set

    End Property

    Public Property AccountMainCode() As String

        Get
            Return _AccountMainCode
        End Get
        Set(ByVal value As String)
            _AccountMainCode = value
        End Set

    End Property

    Public Property AccountMainTitle() As String

        Get
            Return _AccountMainTitle
        End Get
        Set(ByVal value As String)
            _AccountMainTitle = value
        End Set

    End Property

    Public Property AccountMainType() As String

        Get
            Return _AccountMainType
        End Get
        Set(ByVal value As String)
            _AccountMainType = value
        End Set

    End Property

   

End Class
