Public Class FiniancialYearStatus

    Private _FYearStatusID As Integer
    Private _FYearID As Integer
    Private _StartDate As DateTime
    Private _EndDate As DateTime
    Private _YearCode As String
    Private _Status As String
    Private _OldStatus As String
    Private _LocationID As Integer
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


    Public Property FYearID() As Integer
        Get
            Return _FYearID
        End Get
        Set(ByVal value As Integer)
            _FYearID = value
        End Set
    End Property
    Public Property FYearStatusID() As Integer
        Get
            Return _FYearStatusID
        End Get
        Set(ByVal value As Integer)
            _FYearStatusID = value
        End Set
    End Property

    Public Property StartDate() As DateTime
        Get
            Return _StartDate
        End Get
        Set(ByVal value As DateTime)
            _StartDate = value
        End Set
    End Property


    Public Property EndDate() As DateTime
        Get
            Return _EndDate
        End Get
        Set(ByVal value As DateTime)
            _EndDate = value
        End Set
    End Property


    Public Property YearCode() As String
        Get
            Return _YearCode
        End Get
        Set(ByVal value As String)
            _YearCode = value
        End Set
    End Property


    Public Property Status() As String
        Get
            Return _Status
        End Get
        Set(ByVal value As String)
            _Status = value
        End Set
    End Property

    Public Property OldStatus() As String
        Get
            Return _OldStatus
        End Get
        Set(ByVal value As String)
            _OldStatus = value
        End Set
    End Property

    Public Property LocationID() As Integer
        Get
            Return _LocationID
        End Get
        Set(ByVal value As Integer)
            _LocationID = value
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
