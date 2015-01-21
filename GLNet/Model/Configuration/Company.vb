Public Class Company

    Private _CompanyID As Integer
    Private _CompanyCode As String
    Private _CompanyName As String
    Private _Comments As String
    Private _SortOrder As Integer
    Private _CompanyAddress As String = String.Empty
    Private _CompanyURL As String
    Private _CompanyPhone As String
    Private _CompanyFax As String
    Private _IsReadOnly As Boolean
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
    Public Property CompanyID() As Integer
        Get
            Return _CompanyID
        End Get
        Set(ByVal value As Integer)
            _CompanyID = value
        End Set
    End Property
    Public Property CompanyCode() As String
        Get
            Return _CompanyCode
        End Get
        Set(ByVal value As String)
            _CompanyCode = value
        End Set
    End Property
    Public Property CompanyName() As String
        Get
            Return _CompanyName
        End Get
        Set(ByVal value As String)
            _CompanyName = value
        End Set
    End Property
    Public Property Comments() As String
        Get
            Return _Comments
        End Get
        Set(ByVal value As String)
            _Comments = value
        End Set
    End Property
    Public Property SortOrder() As Integer
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As Integer)
            _SortOrder = value
        End Set
    End Property
    Public Property CompanyAddress() As String
        Get
            Return _CompanyAddress
        End Get
        Set(ByVal value As String)
            _CompanyAddress = value
        End Set
    End Property
    Public Property CompanyURL() As String
        Get
            Return _CompanyURL
        End Get
        Set(ByVal value As String)
            _CompanyURL = value
        End Set
    End Property
    Public Property CompanyPhone() As String
        Get
            Return _CompanyPhone
        End Get
        Set(ByVal value As String)
            _CompanyPhone = value
        End Set
    End Property
    Public Property CompanyFax() As String
        Get
            Return _CompanyFax
        End Get
        Set(ByVal value As String)
            _CompanyFax = value
        End Set
    End Property
    Public Property IsReadOnly() As Boolean
        Get
            Return _IsReadOnly
        End Get
        Set(ByVal value As Boolean)
            _IsReadOnly = value
        End Set
    End Property
End Class



