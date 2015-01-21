''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''// 18 Jul,2014       Abdul Jabbar       CR#322: Voucher search required against voucher amount, invoice no. & cheque number

Public Class PostingVouchersModel

    Private _TempVouchers As Boolean
    Public Property TempVouchers() As Boolean

        Get
            Return _TempVouchers
        End Get

        Set(ByVal value As Boolean)
            _TempVouchers = value
        End Set

    End Property


    Private _FinancialYearCode As String
    Public Property FinancialYearCode() As String

        Get
            Return _FinancialYearCode
        End Get

        Set(ByVal value As String)
            _FinancialYearCode = value
        End Set

    End Property

    Private _VoucherType As String
    Public Property VoucherType() As String

        Get
            Return _VoucherType
        End Get

        Set(ByVal value As String)
            _VoucherType = value
        End Set

    End Property


    Private _VoucherMonth As String
    Public Property VoucherMonth() As String

        Get
            Return _VoucherMonth
        End Get

        Set(ByVal value As String)
            _VoucherMonth = value
        End Set

    End Property


    Private _VoucherSource As String
    Public Property VoucherSource() As String

        Get
            Return _VoucherSource
        End Get

        Set(ByVal value As String)
            _VoucherSource = value
        End Set

    End Property


    Private _SelectedRecord As ArrayList
    Public Property SELECTEDRECORD_ARRAYLIST() As ArrayList

        Get
            Return _SelectedRecord
        End Get
        Set(ByVal Value As ArrayList)
            _SelectedRecord = Value
        End Set

    End Property


    Private _LocationID As Integer
    Public Property LocationID() As Integer

        Get
            Return _LocationID
        End Get

        Set(ByVal value As Integer)
            _LocationID = value
        End Set

    End Property


    Private _VoucherID As Integer
    Public Property VoucherID() As Integer

        Get
            Return _VoucherID
        End Get

        Set(ByVal value As Integer)
            _VoucherID = value
        End Set

    End Property

    Private _Post As Integer
    Public Property Post() As Integer

        Get
            Return _Post
        End Get

        Set(ByVal value As Integer)
            _Post = value
        End Set

    End Property






    Private _VoucherNoWiseFlag As Integer
    Public Property VoucherNoWiseFlag() As Integer

        Get
            Return _VoucherNoWiseFlag
        End Get

        Set(ByVal value As Integer)
            _VoucherNoWiseFlag = value
        End Set

    End Property



    Private _VoucherDateWiseFlag As Integer
    Public Property VoucherDateWiseFlag() As Integer

        Get
            Return _VoucherDateWiseFlag
        End Get

        Set(ByVal value As Integer)
            _VoucherDateWiseFlag = value
        End Set

    End Property


    Private _VoucherNoFrom As String
    Public Property VoucherNoFrom() As String

        Get
            Return _VoucherNoFrom
        End Get

        Set(ByVal value As String)
            _VoucherNoFrom = value
        End Set

    End Property


    Private _VoucherNoTO As String
    Public Property VoucherNoTO() As String

        Get
            Return _VoucherNoTO
        End Get

        Set(ByVal value As String)
            _VoucherNoTO = value
        End Set

    End Property


    Private _VoucherStartDate As String
    Public Property VoucherStartDate() As String

        Get
            Return _VoucherStartDate
        End Get

        Set(ByVal value As String)
            _VoucherStartDate = value
        End Set

    End Property


    Private _VoucherEndDate As String
    Public Property VoucherEndDate() As String

        Get
            Return _VoucherEndDate
        End Get

        Set(ByVal value As String)
            _VoucherEndDate = value
        End Set
    End Property
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

    'CR#322 Adding new properties for search Invoice Amount,Cheque No and Remarks
    Private _InvAmount As Long
    Public Property InvAmount() As Long

        Get
            Return _InvAmount
        End Get

        Set(ByVal value As Long)
            _InvAmount = value
        End Set

    End Property
    Private _ChequeNo As String
    Public Property ChequeNo() As String

        Get
            Return _ChequeNo
        End Get

        Set(ByVal value As String)
            _ChequeNo = value
        End Set

    End Property
    Private _Remarks As String
    Public Property Remarks() As String

        Get
            Return _Remarks
        End Get

        Set(ByVal value As String)
            _Remarks = value
        End Set

    End Property

End Class
