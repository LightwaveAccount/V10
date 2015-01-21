
Public Class GLVoucher
    Private _OrderAltrationID As Integer
    Private _voucherID As Integer
    Private _LocationID As Integer
    Private _LocationCode As String
    Private _VoucherCode As String
    Private _FiniancialYearID As Integer
    Private _VoucherTypeID As Integer
    Private _VoucherMonth As String
    Private _VoucherNo As String
    Private _VoucherDate As DateTime
    Private _PaidTo As String
    Private _COADetailID As Integer
    Private _CashBankAccID As Long
    Private _ChequeNo As String
    Private _ChequeDate As Date
    Private _DueDate As Date
    Private _ChequePaid As Boolean
    Private _ChequePaidDate As DateTime
    Private _PostVoucher As Boolean
    Private _OtherVoucher As Boolean
    Private _Source As String
    Private _ChequeCredited As Boolean
    Private _IsBalancedVoucher As Boolean
    Private _TempVoucherID As Integer
    Private _ListofVouchers As List(Of VoucherDetailItem)
    Private _ActivityLog As ActivityLog
    Private _UserInfo As SecurityUser
    Private _ListOfDeletedIDs As String
    Private _VoucherNarration As String
    Private _VNoMaxLength As Integer
    Private _BlnSaveVInActualTables As Boolean

    Public Sub New()

        _ListofVouchers = New List(Of VoucherDetailItem)
        _UserInfo = New SecurityUser
        _ActivityLog = New ActivityLog

    End Sub
    Public Property ActivityLog() As ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            _ActivityLog = value
        End Set
    End Property
    Public Property UserInfo() As SecurityUser
        Get
            Return _UserInfo
        End Get
        Set(ByVal value As SecurityUser)
            _UserInfo = value
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
    Public Property voucherID() As Integer
        Get
            Return _voucherID
        End Get
        Set(ByVal value As Integer)
            _voucherID = value
        End Set
    End Property
    Public Property VoucherCode() As String
        Get
            Return _VoucherCode
        End Get
        Set(ByVal value As String)
            _VoucherCode = value
        End Set
    End Property
    Public Property LocationCode() As String
        Get
            Return _LocationCode
        End Get
        Set(ByVal value As String)
            _LocationCode = value
        End Set
    End Property
    Public Property ListOfDeletedIDs() As String
        Get
            Return _ListOfDeletedIDs
        End Get
        Set(ByVal value As String)
            _ListOfDeletedIDs = value
        End Set
    End Property
    Public Property FiniancialYearID() As Integer
        Get
            Return _FiniancialYearID
        End Get
        Set(ByVal value As Integer)
            _FiniancialYearID = value
        End Set
    End Property
    Public Property VoucherTypeID() As Integer
        Get
            Return _VoucherTypeID
        End Get
        Set(ByVal value As Integer)
            _VoucherTypeID = value
        End Set
    End Property
    Public Property TempVoucherID() As Integer
        Get
            Return _TempVoucherID
        End Get
        Set(ByVal value As Integer)
            _TempVoucherID = value
        End Set
    End Property
    Public Property VoucherMonth() As String
        Get
            Return _VoucherMonth
        End Get
        Set(ByVal value As String)
            _VoucherMonth = value
        End Set
    End Property
    Public Property VoucherNo() As String
        Get
            Return _VoucherNo
        End Get
        Set(ByVal value As String)
            _VoucherNo = value
        End Set
    End Property
    Public Property VoucherDate() As Date
        Get
            Return _VoucherDate
        End Get
        Set(ByVal value As Date)
            _VoucherDate = value
        End Set
    End Property
    Public Property CashBankAccID() As Long
        Get
            Return _CashBankAccID
        End Get
        Set(ByVal value As Long)
            _CashBankAccID = value
        End Set
    End Property
    Public Property VoucherNarration() As String
        Get
            Return _VoucherNarration
        End Get
        Set(ByVal value As String)
            _VoucherNarration = value
        End Set
    End Property
    Public Property Source() As String
        Get
            Return _Source
        End Get
        Set(ByVal value As String)
            _Source = value
        End Set
    End Property
    Public Property ChequeNo() As String
        Get
            Return _ChequeNo
        End Get
        Set(ByVal value As String)
            _ChequeNo = value
        End Set
    End Property
    Public Property ChequeDate() As Date
        Get
            Return _ChequeDate
        End Get
        Set(ByVal value As Date)
            _ChequeDate = value
        End Set
    End Property
    Public Property DueDate() As Date
        Get
            Return _DueDate
        End Get
        Set(ByVal value As Date)
            _DueDate = value
        End Set
    End Property
    Public Property OtherVoucher() As Boolean
        Get
            Return _OtherVoucher
        End Get
        Set(ByVal value As Boolean)
            _OtherVoucher = value
        End Set
    End Property
    Public Property IsBalancedVoucher() As Boolean
        Get
            Return _IsBalancedVoucher
        End Get
        Set(ByVal value As Boolean)
            _IsBalancedVoucher = value
        End Set
    End Property
    Public Property VNoMaxLength() As Integer
        Get
            Return _VNoMaxLength
        End Get
        Set(ByVal value As Integer)
            _VNoMaxLength = value
        End Set
    End Property
    Public Property BlnSaveVInActualTables() As Boolean
        Get
            Return _BlnSaveVInActualTables
        End Get
        Set(ByVal value As Boolean)
            _BlnSaveVInActualTables = value
        End Set
    End Property
    Public Property ListofVouchers() As List(Of VoucherDetailItem)
        Get
            Return _ListofVouchers
        End Get
        Set(ByVal value As List(Of VoucherDetailItem))
            _ListofVouchers = value
        End Set
    End Property
End Class
Public Class VoucherDetailItem
    Private _VoucherDetailID As Integer
    Private _VoucherID As Integer
    'Private _LocationID As Integer
    Private _COADetailID As Integer
    Private _Comments As String
    Private _DebitAmount As Double
    Private _CreditAmount As Double
    Private _CostCenterID As Integer
    Private _SPRefrence As String
    Private _Direction As String
    Private _ShopIS As Integer
    Private _ActivityLog As ActivityLog
    Private _UserInfo As SecurityUser
    Private _MPost As Boolean
    Private _MLocation As Integer
    Private _MVoucherID As Long
    Public Sub New()

        _UserInfo = New SecurityUser
        _ActivityLog = New ActivityLog

    End Sub
    Public Property ActivityLog() As ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            _ActivityLog = value
        End Set
    End Property
    Public Property UserInfo() As SecurityUser
        Get
            Return _UserInfo
        End Get
        Set(ByVal value As SecurityUser)
            _UserInfo = value
        End Set
    End Property
    Public Property VoucherDetailID() As Integer
        Get
            Return _VoucherDetailID
        End Get
        Set(ByVal value As Integer)
            _VoucherDetailID = value
        End Set
    End Property
    Public Property COADetailID() As Integer
        Get
            Return _COADetailID
        End Get
        Set(ByVal value As Integer)
            _COADetailID = value
        End Set
    End Property
    Public Property CostCenterID() As Integer
        Get
            Return _CostCenterID
        End Get
        Set(ByVal value As Integer)
            _CostCenterID = value
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
    Public Property DebitAmount() As Double
        Get
            Return _DebitAmount
        End Get
        Set(ByVal value As Double)
            _DebitAmount = value
        End Set
    End Property
    Public Property CreditAmount() As Double
        Get
            Return _CreditAmount
        End Get
        Set(ByVal value As Double)
            _CreditAmount = value
        End Set
    End Property
    Public Property MPost() As Boolean
        Get
            Return _MPost
        End Get
        Set(ByVal value As Boolean)
            _MPost = value
        End Set
    End Property
    Public Property MLocation() As Integer
        Get
            Return _MLocation
        End Get
        Set(ByVal value As Integer)
            _MLocation = value
        End Set
    End Property
    Public Property MVoucherID() As Integer
        Get
            Return _MVoucherID
        End Get
        Set(ByVal value As Integer)
            _MVoucherID = value
        End Set
    End Property
End Class



