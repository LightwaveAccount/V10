Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports DAL.SystemConfigurationDAL

Public Class frmSubSubAccountDetail
    Implements IGeneral
    Dim ObjModel As Model.AccountSubSubDetailModel

    Private Enum GridCol

        colDetailID = 0
        colSubSubCode = 1
        colDetailCode = 2
        colDetailTitle = 3
        colEndDateStatus = 4
        colSubSubDetailID = 5

    End Enum

#Region "IGeneral Methods .. "

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

        ' Giving Captions .. 
        grdMainAccounts.RootTable.Columns(GridCol.colDetailID).Caption = "Detail ID"
        grdMainAccounts.RootTable.Columns(GridCol.colSubSubCode).Caption = "A/C Sub Sub Code"
        grdMainAccounts.RootTable.Columns(GridCol.colDetailCode).Caption = "A/C Detail Code"
        grdMainAccounts.RootTable.Columns(GridCol.colDetailTitle).Caption = "A/C Detail Title"
        grdMainAccounts.RootTable.Columns(GridCol.colEndDateStatus).Caption = "Status"
        grdMainAccounts.RootTable.Columns(GridCol.colSubSubDetailID).Caption = "Sub Sub Detail ID"
      

        ' Hiding Columns .. 
        grdMainAccounts.RootTable.Columns(GridCol.colDetailID).Visible = False
        grdMainAccounts.RootTable.Columns(GridCol.colSubSubDetailID).Visible = False


        ' Totals Of Columns .. 
        grdMainAccounts.RootTable.Columns(GridCol.colSubSubCode).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count

    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

        Try
            ' Filling Model .. 
            FillModel()

            If New DAL.AccountSubSubDetailDAL().Deleted(ObjModel) Then
                ShowInformationMessage("Data Is Sucessfully Deleted .. ")
                Me.GetAllRecords(txtAccountSub.GLAccountID)
                Return True
                Exit Function

            End If

        Catch ex As Exception
            ShowErrorMessage("Transaction Cannnot Be Performed .. ")

        End Try

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

        ObjModel = New Model.AccountSubSubDetailModel

        ObjModel.SubSubAccountID = txtAccountSub.GLAccountID
        ObjModel.DetailCode = txtAccountSub.txtACCode.Text & "-" & txtSubSubAccountCode.Text
        ObjModel.DetailTitle = txtSubSubAccountTitle.Text

        ObjModel.DetailID = grdMainAccounts.GetRow().Cells(GridCol.colDetailID).Text


    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

        Dim ObjDAL As New DAL.AccountSubSubDAL
        grdMainAccounts.DataSource = ObjDAL.GetAll(Condition)
        grdMainAccounts.RetrieveStructure()

        ApplyGridSettings()

    End Sub

    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

        If txtSubSubAccountCode.Text.Trim = "" Then
            txtSubSubAccountCode.Text = "00000"

        End If

        If txtSubSubAccountTitle.Text.Trim = "" Then
            ShowValidationMessage("Please enter Sub Sub Account Title")
            txtSubSubAccountTitle.Focus()
            Return False
            Exit Function


        End If

        Return True

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

        ' Setting Controls To Intial Positions .. 
        txtSubSubAccountCode.Text = ""
        txtSubSubAccountTitle.Text = ""
        cmbAccountType.SelectedIndex = 0
        cmbCrBSNote.SelectedIndex = 0
        cmbDrBSNote.SelectedIndex = 0
        cmbPLNote.SelectedIndex = 0

        txtSubSubAccountCode.Focus()

        ' Setting New Code To Its Attached Fields .. 
        txtSubSubAccountCode.Text = New DAL.AccountSubSubDetailDAL().GetNewAccountMainCode(txtAccountSub.GLAccountID).ToString


    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

        ' Filling Model .. 
        FillModel()
        If Not New DAL.AccountSubSubDetailDAL().IsAlreadyExists(ObjModel) Then

            If New DAL.AccountSubSubDetailDAL().Save(ObjModel) Then
                ShowInformationMessage("Data Is Sucessfully Saved .. ")
                Me.GetAllRecords(txtAccountSub.GLAccountID)
                Return True
                Exit Function

            End If


        Else
            ShowErrorMessage("Duplicate Code/Name is not allowed .. ")
            txtAccountSub.Focus()
            Return False
            Exit Function

        End If

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

        Try

            Me.btnFirst.ImageList = gobjMyImageListForOperationBar
            Me.btnFirst.ImageKey = "Last"

            Me.btnNext.ImageList = gobjMyImageListForOperationBar
            Me.btnNext.ImageKey = "Previous"

            Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
            Me.btnPrevious.ImageKey = "Next"

            Me.btnLast.ImageList = gobjMyImageListForOperationBar
            Me.btnLast.ImageKey = "First"


            Me.btnNew.ImageList = gobjMyImageListForOperationBar
            Me.btnNew.ImageKey = "New"

            Me.btnSave.ImageList = gobjMyImageListForOperationBar
            Me.btnSave.ImageKey = "Save"

            Me.btnUpdate.ImageList = gobjMyImageListForOperationBar
            Me.btnUpdate.ImageKey = "Update"

            Me.btnCancel.ImageList = gobjMyImageListForOperationBar
            Me.btnCancel.ImageKey = "Cancel"

            Me.btnDelete.ImageList = gobjMyImageListForOperationBar
            Me.btnDelete.ImageKey = "Delete"

            Me.btnExit.ImageList = gobjMyImageListForOperationBar
            Me.btnExit.ImageKey = "Exit"

        Catch ex As Exception
            Throw ex

        End Try

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting
    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

        ' Filling Model .. 
        FillModel()
        If Not New DAL.AccountSubSubDetailDAL().IsAlreadyExists(ObjModel, "Update") Then

            If New DAL.AccountSubSubDetailDAL().Update(ObjModel) Then
                ShowInformationMessage("Data Is Sucessfully Updated .. ")
                Me.GetAllRecords(txtAccountSub.GLAccountID)
                Return True
                Exit Function

            End If


        Else
            ShowErrorMessage("Duplicate Code/Name is not allowed .. ")
            txtAccountSub.Focus()
            Return False
            Exit Function

        End If

    End Function

#End Region

    ' Form Close Button .. 
    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Close()

    End Sub

    Private Sub txtAccountSub_GetGLAccount(ByVal sender As uiCtrlGLAccount) Handles txtAccountSub.GetGLAccount
        If txtAccountSub.txtACCode.Text = "" Then Exit Sub

        ' Binding Grid Control .. 
        Me.GetAllRecords(txtAccountSub.GLAccountID)

    End Sub

    Private Sub frmSubSubAccountDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            ' Assing Images to Buttons ..
            Me.SetButtonImages()

        Catch ex As Exception
            Throw ex

        End Try


    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click

        If txtAccountSub.txtACCode.Text = "" Then
            ShowValidationMessage("Please Select Main Sub Account InWhich You Want To Create Sub Sub Account .. ")
            txtAccountSub.Focus()
            Exit Sub
        End If
        Me.ReSetControls()

    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If IsValidate() Then
            Me.Delete()

        End If

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        If IsValidate() Then
            Me.Update1()

        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If IsValidate() Then
            Save()

        End If

    End Sub

    Private Sub grdMainAccounts_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMainAccounts.SelectionChanged

        Dim strAccountMain() As String = Split(grdMainAccounts.GetRow().Cells(GridCol.colDetailCode).Text, "-")
        txtAccountSub.Text = strAccountMain(1).Trim & "-" & strAccountMain(2).Trim

        Dim strTemp As String = grdMainAccounts.GetRow().Cells(GridCol.colDetailCode).Text

        txtSubSubAccountCode.Text = Microsoft.VisualBasic.Right(strTemp, Len(strTemp) - 7).ToString.Trim
        txtSubSubAccountTitle.Text = grdMainAccounts.GetRow().Cells(GridCol.colDetailTitle).Text


        txtAccountSub.Focus()

    End Sub

    Private Sub txtSubSubAccountCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubSubAccountCode.LostFocus
        txtSubSubAccountCode.Text = txtSubSubAccountCode.Text.PadLeft(5, "0")

    End Sub
End Class