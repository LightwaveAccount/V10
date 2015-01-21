
Public Class SystemConfiguration

    Private _ConfigName As String
    Private _ConfigValue As String
    Private _SavedIn As String
    Private _SelectedRecord As ArrayList
    Private _arrImage As Byte
    'CR#164
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

    Public Property Config_Name() As String

        Get
            Return Me._ConfigName
        End Get
        Set(ByVal value As String)
            Me._ConfigName = value
        End Set

    End Property

    Public Property Config_Value() As String

        Get
            Return Me._ConfigValue
        End Get
        Set(ByVal value As String)
            Me._ConfigValue = value
        End Set

    End Property


    Public Property Saved_In() As String

        Get
            Return Me._SavedIn
        End Get

        Set(ByVal value As String)
            Me._SavedIn = value
        End Set

    End Property


    Public Property arr_Image() As Byte

        Get
            Return _arrImage
        End Get
        Set(ByVal value As Byte)
            Me._arrImage = value
        End Set

    End Property

    Public Property SELECTEDRECORD_ARRAYLIST() As ArrayList

        Get
            Return _SelectedRecord
        End Get
        Set(ByVal Value As ArrayList)
            _SelectedRecord = Value
        End Set

    End Property

End Class
