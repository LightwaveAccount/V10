''''// 323     23 Jul,2014          farooq-H   Cheque Printing: add cheque printing on voucher screen for bank payment voucher
Public Class AccountSubSubDetailModel



    Private _ActivityLog As ActivityLog


    Private _DetailID As Integer
    Private _SubSubCode As String
    Private _DetailCode As String
    Private _DetailTitle As String
    Private _EndDateFlag As Integer
    Private _EndDateStatus As String
    Private _SubSubAccountID As Integer



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

    Public Property EndDateFlag() As Integer

        Get
            Return _EndDateFlag
        End Get

        Set(ByVal value As Integer)
            _EndDateFlag = value
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


    Public Property DetailID() As Integer

        Get
            Return _DetailID
        End Get

        Set(ByVal value As Integer)
            _DetailID = value
        End Set

    End Property

    Public Property DetailCode() As String

        Get
            Return _DetailCode
        End Get
        Set(ByVal value As String)
            _DetailCode = value
        End Set

    End Property


    Public Property DetailTitle() As String

        Get
            Return _DetailTitle
        End Get
        Set(ByVal value As String)
            _DetailTitle = value
        End Set

    End Property

    Public Property SubSubCode() As String

        Get
            Return _SubSubCode
        End Get
        Set(ByVal value As String)
            _SubSubCode = value
        End Set

    End Property

    Public Property EndDateStatus() As String

        Get
            Return _EndDateStatus
        End Get
        Set(ByVal value As String)
            _EndDateStatus = value
        End Set

    End Property
    '' ''''// 323     23 Jul,2014          farooq-H  
    Private _ChequeID As Integer
    Public Property ChequeID() As Integer

        Get
            Return _ChequeID
        End Get

        Set(ByVal value As Integer)
            _ChequeID = value
        End Set

    End Property
End Class
