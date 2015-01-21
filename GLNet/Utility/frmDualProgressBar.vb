Public Class frmDualProgressBar
    Public TotalRows As Integer
    Dim Delay As Integer
    Public strMessage As String
    Public intLoopCount As Integer
    'Public oControl As Control
    Public propName As String
    Public propValue As Object

    Private intValue As Int32
    Private intReturn(99) As Int32
    'Delegate Sub SetControlValueCallback(ByVal oControl As Control, ByVal propName As String, ByVal propValue As Object)
    Delegate Sub SetControlValueCallback()

    Public KillProcess As Boolean = False

    Public WriteOnly Property Value() As Int32
        Set(ByVal Value As Int32)
            intValue = Value
        End Set
    End Property



    Public ReadOnly Property Return1() As Int32()
        Get
            Return1 = intReturn
        End Get
    End Property

    Public ReadOnly Property ReturnIndex(ByVal Index As Int32)
        Get
            ReturnIndex = intReturn(Index)
        End Get
    End Property

    Private Sub frmDualProgressBar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'PB.Minimum = 0
        PB.Maximum = TotalRows
        ''Me.ShowDialog()
        'PB.Value = 0
        lblStatus.Text = strMessage
        'For i As Integer = 0 To TotalRows - 1
        '    If PB.Value >= 100 Then PB.Value = 70
        '    PB.Value += intLoopCount
        'Next
        'PB.Value = 100
        'Me.Worker.RunWorkerAsync()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal intRows As Integer, ByVal strMsg As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        TotalRows = intRows
        ' Delay = intDelay
        strMessage = strMsg

    End Sub
    Public Sub FillPrograssBar()
        Dim current As Int32
        current = intValue
        'PB = oControl
        'oControl = CType(oControl, ProgressBar)
        'Dim p As Reflection.PropertyInfo

        'Dim t As Type = oControl.GetType()
        'Dim props() As Reflection.PropertyInfo = t.GetProperties()

        For i As Integer = 0 To TotalRows - 1

            'If PB.Value >= 100 Then PB.Value = 70
            'oControl.Value += 1
            Me.PB.Value += 1
            'PB.Value += 1
            'propValue += 1
            'For Each p In props
            '    If p.Name.ToUpper() = propName.ToUpper() Then
            '        p.SetValue(oControl, propValue, Nothing)
            '    End If
            'Next


        Next
    End Sub
    'Public Sub SetControlPropertyValue(ByVal oControl As Control, ByVal propName As String, ByVal propValue As Object)
    Public Sub SetControlPropertyValue()
        'If (oControl.InvokeRequired) Then
        '    ' SetControlValueCallback d = New SetControlValueCallback(SetControlPropertyValue))
        '    Dim d As SetControlValueCallback = New SetControlValueCallback(AddressOf SetControlPropertyValue)
        '    oControl.Invoke(d, New Object())
        'Else
        '    Dim t As Type = oControl.GetType()
        '    Dim props() As Reflection.PropertyInfo = t.GetProperties()
        '    Dim p As Reflection.PropertyInfo
        '    For Each p In props
        '        If p.Name.ToUpper() = propName.ToUpper() Then
        '            p.SetValue(oControl, propValue, Nothing)
        '        End If
        '    Next

        'End If

    End Sub

End Class