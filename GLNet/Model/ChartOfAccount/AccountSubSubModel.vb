Public Class AccountSubSubModel

    Private _AccountSubID As Integer
    Private _SubSubAccountID As Integer
    Private _SubAccountCode As String
    Private _SubSubAccountCode As String
    Private _SubSubAccountTitle As String

    Private _AccountType As String
    Private _DrBSNote As String
    Private _CrBSNote As String
    Private _PLNote As String

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

    Public Property AccountSubID() As Integer

        Get
            Return _AccountSubID
        End Get

        Set(ByVal value As Integer)
            _AccountSubID = value
        End Set

    End Property

    Public Property SubSubAccountID() As Integer

        Get
            Return _SubSubAccountID
        End Get
        Set(ByVal value As Integer)
            _SubSubAccountID = value
        End Set

    End Property

    Public Property SubAccountCode() As String

        Get
            Return _SubAccountCode
        End Get
        Set(ByVal value As String)
            _SubAccountCode = value
        End Set

    End Property

    Public Property SubSubAccountCode() As String

        Get
            Return _SubSubAccountCode
        End Get
        Set(ByVal value As String)
            _SubSubAccountCode = value
        End Set

    End Property

    Public Property SubSubAccountTitle() As String

        Get
            Return _SubSubAccountTitle
        End Get
        Set(ByVal value As String)
            _SubSubAccountTitle = value
        End Set

    End Property

    Public Property AccountType() As String

        Get
            Return _AccountType
        End Get
        Set(ByVal value As String)
            _AccountType = value
        End Set

    End Property

    Public Property DrBSNote() As String

        Get
            Return _DrBSNote
        End Get
        Set(ByVal value As String)
            _DrBSNote = value
        End Set

    End Property

    Public Property CrBSNote() As String

        Get
            Return _CrBSNote
        End Get
        Set(ByVal value As String)
            _CrBSNote = value
        End Set

    End Property

    Public Property PLNote() As String

        Get
            Return _PLNote
        End Get
        Set(ByVal value As String)
            _PLNote = value
        End Set

    End Property



End Class
