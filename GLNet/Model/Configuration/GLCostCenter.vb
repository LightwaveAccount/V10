Public Class GLCostCenter

    Private _CostCenterId As Integer
    Private _CostCenterTitle As String
    Private _CostCenterType As String
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


    Public Property CostCenterId() As Integer
        Get
            Return _CostCenterId
        End Get
        Set(ByVal value As Integer)
            _CostCenterId = value
        End Set
    End Property

    Public Property CostCenterTitle() As String
        Get
            Return _CostCenterTitle
        End Get
        Set(ByVal value As String)
            _CostCenterTitle = value
        End Set
    End Property


    Public Property CostCenterType() As String
        Get
            Return _CostCenterType
        End Get
        Set(ByVal value As String)
            _CostCenterType = value
        End Set
    End Property

End Class


