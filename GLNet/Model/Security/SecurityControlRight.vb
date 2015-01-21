Public Class SecurityControlRight

    Private _rightID As Integer
    Private _userInfo As SecurityUser
    Private _formControlInfo As SecurityFormControls
    


    Public Property RightID() As Integer
        Get
            Return _rightID
        End Get
        Set(ByVal value As Integer)
            _rightID = value
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

    Public Property FormControlInfo() As SecurityFormControls
        Get
            Return _formControlInfo
        End Get
        Set(ByVal value As SecurityFormControls)
            _formControlInfo = value
        End Set
    End Property

    

End Class
