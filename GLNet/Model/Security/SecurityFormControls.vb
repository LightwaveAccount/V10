Public Class SecurityFormControls

    Private _ControlID As Integer
    Private _ControlName As String
    Private _ControlCaption As String
    Private _FormInfo As SecurityForm


    Public Property ControlID() As Integer
        Get
            Return _ControlID
        End Get
        Set(ByVal value As Integer)
            _ControlID = value
        End Set
    End Property

    Public Property ControlName() As String
        Get
            Return _ControlName
        End Get
        Set(ByVal value As String)
            _ControlName = value
        End Set
    End Property

    Public Property ControlCaption() As String
        Get
            Return _ControlCaption
        End Get
        Set(ByVal value As String)
            _ControlCaption = value
        End Set
    End Property

    Public Property FromInfo() As SecurityForm
        Get
            Return _FormInfo
        End Get
        Set(ByVal value As SecurityForm)
            _FormInfo = value
        End Set
    End Property



End Class
