Public Class AccountSubModel

    Private _AccountMainSubID As Integer
    Private _AccountMainSubCode As String
    Private _AccountMainSubTitle As String
    Private _AccountMainID As Integer
    Private _AccountMain As String
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

    Public Property AccountMainSubCode() As String

        Get
            Return _AccountMainSubCode
        End Get
        Set(ByVal value As String)
            _AccountMainSubCode = value
        End Set

    End Property

    Public Property AccountMainSubTitle() As String

        Get
            Return _AccountMainSubTitle
        End Get
        Set(ByVal value As String)
            _AccountMainSubTitle = value
        End Set

    End Property

    Public Property AccountMainSubID() As Integer

        Get
            Return _AccountMainSubID
        End Get
        Set(ByVal value As Integer)
            _AccountMainSubID = value
        End Set

    End Property

    Public Property AccountMain() As String

        Get
            Return _AccountMain
        End Get
        Set(ByVal value As String)
            _AccountMain = value
        End Set

    End Property

End Class
