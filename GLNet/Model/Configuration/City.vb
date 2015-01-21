Public Class City


    Private _CityID As Integer
    Private _CityName As String = String.Empty = String.Empty
    Private _CityCode As String = String.Empty
    Private _CityPerference As Integer
    Private _SortOrder As Integer
    Private _Comments As String = String.Empty
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


    Public Property CityID() As Integer
        Get
            Return _CityID
        End Get
        Set(ByVal value As Integer)
            _CityID = value
        End Set
    End Property


    Public Property CityName() As String
        Get
            Return _CityName
        End Get
        Set(ByVal value As String)
            _CityName = value
        End Set
    End Property


    Public Property CityCode() As String
        Get
            Return _CityCode
        End Get
        Set(ByVal value As String)
            _CityCode = value
        End Set
    End Property


    Public Property CityPerference() As Integer
        Get
            Return _CityPerference
        End Get
        Set(ByVal value As Integer)
            _CityPerference = value
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


    Public Property Comments() As String
        Get
            Return _Comments
        End Get
        Set(ByVal value As String)
            _Comments = value
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
