''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela New
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Account Help Form .. 
''// Programmer	     : Tariq Majeed
''// Creation Date	 : 16-July-2009
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''/////////////////////////////////////////////////////////////////////////////////////////


Imports System.Data.SqlClient
Imports DAL
Imports Utility.Utility

Public Class frmAccountHelp


#Region "Enumeration"

    Enum EnumGridColumns
        AccountID = 0
        AccountCode = 1
        AccountName = 2
        AccountType = 3
    End Enum

#End Region


#Region "Custom Properties"

    Private _AccountHeadType As String
    Public Property AccountHeadType() As String

        Get
            Return _AccountHeadType
        End Get

        Set(ByVal value As String)
            _AccountHeadType = value

        End Set

    End Property


    Private _FilterCondition As String
    Public Property FilterCondition() As String

        Get
            Return _FilterCondition
        End Get
        Set(ByVal value As String)
            _FilterCondition = value
        End Set

    End Property

    Private _AccountType As String
    Public Property AccountType() As String

        Get
            Return _AccountType
        End Get
        Set(ByVal value As String)
            _AccountType = value
        End Set

    End Property


    Private _GLFilterAccount As String
    Public Property GLFilterAccount() As String

        Get
            Return _GLFilterAccount
        End Get

        Set(ByVal value As String)
            _GLFilterAccount = value

        End Set

    End Property



   
#End Region

#Region "Local Methods and Funtions"

    ' This Procedure Will Be Used To Set The Formatting Of Grid .. 
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "")

        Try

            Me.grdHelp.AutomaticSort = False

            ' Setting Column Visibility .. 
            Me.grdHelp.RootTable.Columns(EnumGridColumns.AccountID).Visible = False
            Me.grdHelp.RootTable.Columns(EnumGridColumns.AccountType).Visible = False
            Me.grdHelp.RootTable.Columns(EnumGridColumns.AccountCode).Visible = True
            Me.grdHelp.RootTable.Columns(EnumGridColumns.AccountName).Visible = True

            ' Setting Columns Widths For Visible Columns ..
            Me.grdHelp.RootTable.Columns(EnumGridColumns.AccountCode).Width = 125
            Me.grdHelp.RootTable.Columns(EnumGridColumns.AccountName).Width = 230


            ' Setting Numeric Columns Formats ..
            Me.grdHelp.RootTable.Columns(EnumGridColumns.AccountID).FormatString = ""
            Me.grdHelp.RootTable.Columns(EnumGridColumns.AccountCode).FormatString = ""
            Me.grdHelp.RootTable.Columns(EnumGridColumns.AccountName).FormatString = ""


            Me.grdHelp.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True

            For ColInd As Integer = 0 To Me.grdHelp.RootTable.Columns.Count - 1
                If Me.grdHelp.RootTable.Columns(ColInd).Visible = True Then Me.grdHelp.RootTable.Columns(ColInd).AutoSize()

            Next

        Catch ex As Exception
            Throw ex

        End Try

    End Sub


#End Region

    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    Private Sub frmAccountHelp_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Try
            If e.KeyCode = Keys.Escape Then Me.Close()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub frmProductCodeHelp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.grdHelp.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False

        ' Set Default Values ..
        Me.txtAcCode.Text = ""
        Me.txtAcName.Text = ""
        Me.grdHelp.DataSource = Nothing
        Me.grdHelp.ClearStructure()
        Me.grdHelp.Refresh()



        Dim ObjDataTable As DataTable
        Dim ObjGLAccountDAL As New GLAccountDAL
        ObjDataTable = ObjGLAccountDAL.GetAll(FilterCondition, AccountHeadType, AccountType, GLFilterAccount)

        Me.grdHelp.DataSource = ObjDataTable
        Me.grdHelp.RetrieveStructure()


        Call ApplyGridSettings()

    End Sub

    Private Sub txtProductCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAcCode.KeyDown, txtAcName.KeyDown

        If grdHelp.RowCount <= 1 Then Exit Sub

        If e.KeyCode = Keys.Enter Then
            Me.Tag = CType(grdHelp.GetRow(), Janus.Windows.GridEX.GridEXRow)
            Me.Close()
            Exit Sub

        ElseIf e.KeyCode = Keys.Down Then

            If Me.grdHelp.Row >= 0 And (Me.grdHelp.Row < Me.grdHelp.RowCount - 1) Then Me.grdHelp.Row = (Me.grdHelp.Row + 1)
            Me.txtAcCode.Text = IIf(IsDBNull(Me.grdHelp.GetValue(EnumGridColumns.AccountCode).ToString), "", Me.grdHelp.GetValue(EnumGridColumns.AccountCode).ToString)
            Me.txtAcName.Text = IIf(IsDBNull(Me.grdHelp.GetValue(EnumGridColumns.AccountName).ToString), "", Me.grdHelp.GetValue(EnumGridColumns.AccountName).ToString)
            Exit Sub

        ElseIf e.KeyCode = Keys.Up Then

            If Me.grdHelp.Row > 0 Then Me.grdHelp.Row = (Me.grdHelp.Row - 1)
            Me.txtAcCode.Text = IIf(IsDBNull(Me.grdHelp.GetValue(EnumGridColumns.AccountCode).ToString), "", Me.grdHelp.GetValue(EnumGridColumns.AccountCode).ToString)
            Me.txtAcName.Text = IIf(IsDBNull(Me.grdHelp.GetValue(EnumGridColumns.AccountName).ToString), "", Me.grdHelp.GetValue(EnumGridColumns.AccountName).ToString)
            Exit Sub

        End If
    End Sub

    Private Sub grdHelp_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdHelp.DoubleClick

        Me.Tag = CType(grdHelp.GetRow(), Janus.Windows.GridEX.GridEXRow)
        Me.Close()

    End Sub

    Private Sub grdHelp_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdHelp.KeyUp

        If e.KeyCode = Keys.Enter Then
            Me.Tag = CType(grdHelp.GetRow(), Janus.Windows.GridEX.GridEXRow)
            Me.Close()

        End If

    End Sub

    Private Sub txtAcCode_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAcCode.KeyUp
        Try
            If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then Exit Sub


            Dim ObjDataTable As DataTable
            Dim ObjGLAccountDAL As New GLAccountDAL
            ObjDataTable = ObjGLAccountDAL.GetAll(FilterCondition, AccountHeadType, AccountType.ToString, GLFilterAccount)

            Dim ObjDataView As DataView
            ObjDataView = GetFilterDataFromDataTable(ObjDataTable, "AccountID <> 0 AND AccountCode like '" & txtAcCode.Text.Trim.Replace("'", "''").ToString & "%'")
            Me.grdHelp.DataSource = ObjDataView

            ObjDataView = Nothing

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub txtAcName_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAcName.KeyUp
        Try
            If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then Exit Sub

            Dim ObjDataTable As DataTable
            Dim ObjGLAccountDAL As New GLAccountDAL
            ObjDataTable = ObjGLAccountDAL.GetAll(FilterCondition, AccountHeadType, AccountType.ToString, GLFilterAccount)

            Dim ObjDataView As DataView
            ObjDataView = GetFilterDataFromDataTable(ObjDataTable, "AccountID <> 0 AND AccountName like '" & txtAcName.Text.Trim.Replace("'", "''").ToString & "%'")
            Me.grdHelp.DataSource = ObjDataView

            ObjDataView = Nothing

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try



    End Sub

    ' Grid Loading Row .. 
    Private Sub grdHelp_LoadingRow(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdHelp.LoadingRow
        Try

            If e.Row.RowType = Janus.Windows.GridEX.RowType.TotalRow Then

                ' Too View The Total Records in Grid Footer ..
                Dim dv As DataView = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Control Type]= 'DataDictionary'  AND [Control Name] = 'GridRowCount'")
                Dim strTotalRecords As String = String.Empty

                If Not dv Is Nothing Then
                    If Not dv.Count = 0 Then
                        strTotalRecords = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName)
                    End If
                End If

                e.Row.Cells(EnumGridColumns.AccountCode).Text = strTotalRecords & " (" & Me.grdHelp.GetTotal(Me.grdHelp.RootTable.Columns(EnumGridColumns.AccountCode), Janus.Windows.GridEX.AggregateFunction.Count) & ") "

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub


    ' Grid - Row Selection Changed .. 
    Private Sub grdHelp_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdHelp.SelectionChanged
        Try

            If Me.grdHelp.GetRow.RowType = Janus.Windows.GridEX.RowType.TotalRow Then

                If Me.grdHelp.RowCount > 0 Then
                    If Me.grdHelp.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then Me.grdHelp.MoveLast()
                End If

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub
End Class