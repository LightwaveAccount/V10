Public Class CustomerInfo

    Private _CustomerInfoID As Integer
    Private _AccountID As Integer
    Private _ContactPerson As String
    Private _PhoneOffice As String
    Private _Mobile As String
    Private _Fax As String
    Private _Email As String
    Private _Address As String
    Private _Remarks As String
    Private _CreationDate As Date
    Private _MaintStartDate As Date
    Private _MaintEndDate As Date
    Private _PaymentRcvdDate As Date
    Private _Product As String
    Private _Status As String
    Private _SlaType As String
    Private _MonthlyAmount As Integer
    Private _dtlProduct As String
    Private _dtlStatus As String
    Private _dtlSlaType As String
    Private _dtlContactPerson As String
    Private _dtlCompOppCreationDate As String
    Private _dtlCompOppCreationDate2 As String
    Private _dtlComOppMaintEndDate As String
    Private _dtlCompOppPaymentDate As String
    Private _dtlCreationDate As Date
    Private _dtlCreationDate2 As Date
    Private _dtlMaintEndDate As Date
    Private _dtlPaymentRcvdDate As Date
    Private _dtlPaymentRcvdToDate As Date
    Private _dtlMaintEndDateTo As Date


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


    Public Property CustomerInfoID() As Integer
        Get
            Return _CustomerInfoID
        End Get
        Set(ByVal value As Integer)
            _CustomerInfoID = value
        End Set
    End Property

    Public Property AccountID() As Integer
        Get
            Return _AccountID
        End Get
        Set(ByVal value As Integer)
            _AccountID = value
        End Set
    End Property

    Public Property ContactPerson() As String
        Get
            Return _ContactPerson
        End Get
        Set(ByVal value As String)
            _ContactPerson = value
        End Set
    End Property

    Public Property PhoneOffice() As String
        Get
            Return _PhoneOffice
        End Get
        Set(ByVal value As String)
            _PhoneOffice = value
        End Set
    End Property

    Public Property Mobile() As String
        Get
            Return _Mobile
        End Get
        Set(ByVal value As String)
            _Mobile = value
        End Set
    End Property

    Public Property Fax() As String
        Get
            Return _Fax
        End Get
        Set(ByVal value As String)
            _Fax = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property

    Public Property Address() As String
        Get
            Return _Address
        End Get
        Set(ByVal value As String)
            _Address = value
        End Set
    End Property

    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

    Public Property CreationDate() As Date
        Get
            Return _CreationDate
        End Get
        Set(ByVal value As Date)
            _CreationDate = value
        End Set
    End Property

    Public Property MaintStartDate() As Date
        Get
            Return _MaintStartDate
        End Get
        Set(ByVal value As Date)
            _MaintStartDate = value
        End Set
    End Property

    Public Property MaintEndDate() As Date
        Get
            Return _MaintEndDate
        End Get
        Set(ByVal value As Date)
            _MaintEndDate = value
        End Set
    End Property

    Public Property PaymentRcvdDate() As Date
        Get
            Return _PaymentRcvdDate
        End Get
        Set(ByVal value As Date)
            _PaymentRcvdDate = value
        End Set
    End Property

    Public Property Product() As String
        Get
            Return _Product
        End Get
        Set(ByVal value As String)
            _Product = value
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

    Public Property SlaType() As String
        Get
            Return _SlaType
        End Get
        Set(ByVal value As String)
            _SlaType = value
        End Set
    End Property

    Public Property MonthlyAmount() As Integer
        Get
            Return _MonthlyAmount
        End Get
        Set(ByVal value As Integer)
            _MonthlyAmount = value
        End Set
    End Property

    Public Property DtlCreationDate() As Date
        Get
            Return _dtlCreationDate
        End Get
        Set(ByVal value As Date)
            _dtlCreationDate = value
        End Set
    End Property

    Public Property DtlCreationDate2() As Date
        Get
            Return _dtlCreationDate2
        End Get
        Set(ByVal value As Date)
            _dtlCreationDate2 = value
        End Set
    End Property

    Public Property DtlMaintEndDate() As Date
        Get
            Return _dtlMaintEndDate
        End Get
        Set(ByVal value As Date)
            _dtlMaintEndDate = value
        End Set
    End Property

    Public Property DtlPaymentRcvdDate() As Date
        Get
            Return _dtlPaymentRcvdDate
        End Get
        Set(ByVal value As Date)
            _dtlPaymentRcvdDate = value
        End Set
    End Property

    Public Property DtlProduct() As String
        Get
            Return _dtlProduct
        End Get
        Set(ByVal value As String)
            _dtlProduct = value
        End Set
    End Property

    Public Property DtlStatus() As String
        Get
            Return _dtlStatus
        End Get
        Set(ByVal value As String)
            _dtlStatus = value
        End Set
    End Property

    Public Property DtlSlaType() As String
        Get
            Return _dtlSlaType
        End Get
        Set(ByVal value As String)
            _dtlSlaType = value
        End Set
    End Property

    Public Property DtlCompOppCreationDate() As String
        Get
            Return _dtlCompOppCreationDate
        End Get
        Set(ByVal value As String)
            _dtlCompOppCreationDate = value
        End Set
    End Property

    Public Property DtlCompOppCreationDate2() As String
        Get
            Return _dtlCompOppCreationDate2
        End Get
        Set(ByVal value As String)
            _dtlCompOppCreationDate2 = value
        End Set
    End Property

    Public Property DtlCompOppMaintEndDate() As String
        Get
            Return _dtlComOppMaintEndDate
        End Get
        Set(ByVal value As String)
            _dtlComOppMaintEndDate = value
        End Set
    End Property

    Public Property DtlComOppPaymentRcvdDate() As String
        Get
            Return _dtlCompOppPaymentDate
        End Get
        Set(ByVal value As String)
            _dtlCompOppPaymentDate = value
        End Set
    End Property

    Public Property DtlContactPerson() As String
        Get
            Return _dtlContactPerson
        End Get
        Set(ByVal value As String)
            _dtlContactPerson = value
        End Set
    End Property

    Public Property DtlPaymentRcvdToDate() As Date
        Get
            Return _dtlPaymentRcvdToDate
        End Get
        Set(ByVal value As Date)
            _dtlPaymentRcvdToDate = value
        End Set
    End Property

    Public Property DtlMaintEndToDate() As Date
        Get
            Return _dtlMaintEndDateTo
        End Get
        Set(ByVal value As Date)
            _dtlMaintEndDateTo = value
        End Set
    End Property

End Class

