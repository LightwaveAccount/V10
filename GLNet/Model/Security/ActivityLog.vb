Public Class ActivityLog

    Private _LogID As Integer = 0
    Public Property LogID() As Integer
        Get
            Return Me._LogID
        End Get
        Set(ByVal value As Integer)
            Me._LogID = value
        End Set
    End Property

    Private _ShopID As Integer = 0
    Public Property ShopID() As Integer
        Get
            Return Me._ShopID
        End Get
        Set(ByVal value As Integer)
            Me._ShopID = value
        End Set
    End Property

    Private _ScreenTitle As String = String.Empty
    Public Property ScreenTitle() As String
        Get
            Return Me._ScreenTitle
        End Get
        Set(ByVal value As String)
            Me._ScreenTitle = value
        End Set
    End Property

    Private _FormAction As String = String.Empty
    Public Property FormAction() As String
        Get
            Return Me._FormAction
        End Get
        Set(ByVal value As String)
            Me._FormAction = value
        End Set
    End Property

    Private _UserID As Integer = 0
    Public Property UserID() As Integer
        Get
            Return Me._UserID
        End Get
        Set(ByVal value As Integer)
            Me._UserID = value
        End Set
    End Property

    Private _LogDate As DateTime
    Public Property LogDate() As DateTime
        Get
            Return Me._LogDate
        End Get
        Set(ByVal value As DateTime)
            Me._LogDate = value
        End Set
    End Property

    Private _LogRef As String = String.Empty
    Public Property LogRef() As String
        Get
            Return Me._LogRef
        End Get
        Set(ByVal value As String)
            Me._LogRef = value
        End Set
    End Property

    Private _RefType As String = String.Empty
    Public Property RefType() As String
        Get
            Return Me._RefType
        End Get
        Set(ByVal value As String)
            Me._RefType = value
        End Set
    End Property

    Private _LogGroup As String = String.Empty
    Public Property LogGroup() As String
        Get
            Return Me._LogGroup
        End Get
        Set(ByVal value As String)
            Me._LogGroup = value
        End Set
    End Property

    Private _SQL As String = String.Empty
    Public Property SQL() As String
        Get
            Return Me._SQL
        End Get
        Set(ByVal value As String)
            Me._SQL = value
        End Set
    End Property

    Private _SQLType As String = String.Empty
    Public Property SQLType() As String
        Get
            Return Me._SQLType
        End Get
        Set(ByVal value As String)
            Me._SQLType = value
        End Set
    End Property

    Private _TableName As String = String.Empty
    Public Property TableName() As String
        Get
            Return Me._TableName
        End Get
        Set(ByVal value As String)
            Me._TableName = value
        End Set
    End Property

End Class

