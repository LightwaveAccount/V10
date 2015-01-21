Public Class frmExportToCSV
    Private CSVGrid As Janus.Windows.GridEX.GridEX
    Public SelectedColumns As String = ""
    Public IsExport As Boolean = False
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal _MyGrid As Janus.Windows.GridEX.GridEX)
        Me.New()
        If Not _MyGrid.RowCount > 0 Then
            MsgBox("No data to export", vbCritical)
            Exit Sub

        End If

        CSVGrid = _MyGrid
    End Sub
    Private Sub frmExportToCSV_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''''''''''''''''''CR # 1991 add icon''''''''''''''''''''''''''''''''''''''
        Try
            Me.Icon = Drawing.Icon.FromHandle((CType(gobjMyImageListForOperationBar.Images("Candela2"), Bitmap)).GetHicon())
        Catch ex As Exception

        End Try
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Me.Height = 152
        Me.Width = 180

        optAll.Checked = True
        ' Me.Height = 152
        GenerateColumns()
        chk.Checked = True

        Checked_UnChecked_All(chk.Checked)
        IsExport = False
    End Sub
    Private Sub GenerateColumns()
        Dim chkBox As CheckBox
        For i As Integer = 0 To CSVGrid.RootTable.Columns.Count - 1

            With lstColumns
                If CSVGrid.RootTable.Columns(i).Visible = True Then
                    .Items.Add(CSVGrid.RootTable.Columns(i).Caption)

                End If
            End With
        Next
    End Sub
    Private Sub Checked_UnChecked_All(ByVal chk As Boolean)
        For i As Integer = 0 To lstColumns.Items.Count - 1
            lstColumns.SetItemChecked(i, chk)
        Next
    End Sub
    Private Sub chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk.CheckedChanged
        Checked_UnChecked_All(chk.Checked)
    End Sub

    Private Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        For Each i As Object In lstColumns.CheckedItems
            SelectedColumns = SelectedColumns & "," & i.ToString
        Next
        SelectedColumns = Mid(SelectedColumns, 2, Len(SelectedColumns))
        IsExport = True
        Me.Close()
    End Sub

    Private Sub optSelected_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optSelected.CheckedChanged
        Me.Height = 361
        Me.Width = 396
        chk.Checked = False
    End Sub

    Private Sub optAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optAll.CheckedChanged
        Me.Height = 152
        Me.Width = 180
        chk.Checked = True

    End Sub
End Class