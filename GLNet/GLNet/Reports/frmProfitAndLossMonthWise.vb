''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela New
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Month Wise Profit and Loss Statement
''// Programmer	     : Asif Kamal
''// Creation Date	 : 17-May-2013
''// Description     : 
''// Function List   : 								                                    	                                            
''//-------------------------------------------------------------------------------------
''// CR#    Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''// 239    17-May-2013       Asif Kamal   
''// 266    26-Aug-2013       Rana Saeed/Jabbar   Profit & Loss Month Wise: Some changes are required plus detail report for P & L month wise is required.
''// 262    11-sep-2013       Fatima TAjammal     P&L Month Wise Report: Group Rights are not being implemented 
''// 274    13-sep-2013       Fatima Tajammal     Profit & Loss month wise: some changes are requried 
''// 275    13-sep-2013       Rana Saeed          P&L Month Wise Report: An error occurs on generating the report
''/////////////////////////////////////////////////////////////////////////////////////////

Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports Microsoft.VisualBasic


Public Class frmProfitAndLossMonthWise
    Implements IGeneral

#Region "Variable"
    Private mobjControlList As NameValueCollection
    Private intExp1stLvl As Int16 = 0
    Private intExp2ndLvl As Int16 = 0
    Private intExp3rdLvl As Int16 = 0
#End Region

    Enum EnumGrid
        PLNoteTitle
        SubSubTitle
        DetailTitle
        Total
        Average
    End Enum

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

        Try
            Me.grdAllRecords.RootTable.Columns(EnumGrid.PLNoteTitle).Width = 230
            Me.grdAllRecords.AutomaticSort = False
            Me.grdAllRecords.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
            'Me.grdAllRecords.AutoSizeColumns()
            Me.grdAllRecords.RootTable.RowHeight = 20


            'Formating of Columns
            For intVar As Integer = 1 To Me.grdAllRecords.RootTable.Columns.Count - 1
                Me.grdAllRecords.RootTable.Columns(intVar).FormatString = "###,###,##0"
                Me.grdAllRecords.RootTable.Columns(intVar).TotalFormatString = "###,###,##0"
                Me.grdAllRecords.RootTable.Columns(intVar).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdAllRecords.RootTable.Columns(intVar).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdAllRecords.RootTable.Columns(intVar).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum         '266
            Next

            'Formating of Rows
            If Me.rdoSummary.Checked Then
                Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdAllRecords.RootTable.Columns(EnumGrid.PLNoteTitle), Janus.Windows.GridEX.ConditionOperator.EndsWith, ":")
                fc.FormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
                fc.FormatStyle.ForeColor = Color.Crimson
                Me.grdAllRecords.RootTable.FormatConditions.Add(fc)

                fc = New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdAllRecords.RootTable.Columns(EnumGrid.PLNoteTitle), Janus.Windows.GridEX.ConditionOperator.NotContains, ":")    '266
                fc.FormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
                fc.FormatStyle.ForeColor = Color.Black
                Me.grdAllRecords.RootTable.FormatConditions.Add(fc)
            End If

            If Me.rdoDetail.Checked Then
                Me.grdAllRecords.GroupByBoxVisible = False

                Me.grdAllRecords.RootTable.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
                Dim grpAcc As New Janus.Windows.GridEX.GridEXGroup(Me.grdAllRecords.RootTable.Columns(EnumGrid.PLNoteTitle))
                grpAcc.GroupPrefix = String.Empty
                Me.grdAllRecords.RootTable.Groups.Add(grpAcc)
                Me.grdAllRecords.RootTable.Columns(EnumGrid.PLNoteTitle).HideWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True

                Dim grpSubTitle As New Janus.Windows.GridEX.GridEXGroup(Me.grdAllRecords.RootTable.Columns(EnumGrid.SubSubTitle))
                grpSubTitle.GroupPrefix = String.Empty
                Me.grdAllRecords.RootTable.Groups.Add(grpSubTitle)
                Me.grdAllRecords.RootTable.Columns(EnumGrid.SubSubTitle).HideWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True

                Dim grpDetailTitle As New Janus.Windows.GridEX.GridEXGroup(Me.grdAllRecords.RootTable.Columns(EnumGrid.DetailTitle))
                grpDetailTitle.GroupPrefix = String.Empty
                Me.grdAllRecords.RootTable.Groups.Add(grpDetailTitle)
                Me.grdAllRecords.RootTable.Columns(EnumGrid.DetailTitle).HideWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True

                intExp1stLvl = 1 : intExp2ndLvl = 1 : intExp3rdLvl = 1
            End If
            '266    End

            Me.UiCtrlGridBar1.txtGridTitle.Text = "Month Wise Profit & Loss Statement"


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

        Try
            If Mode.ToString = EnumDataMode.Disabled.ToString Then

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System           
                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(EnumDataMode.[New])
            End If

            'CR # 262

            If mobjControlList.Item("btnPrint") Is Nothing Then
                Me.UiCtrlGridBar1.btnPrint.Enabled = False
            End If

            If mobjControlList.Item("btnExport") Is Nothing Then
                Me.UiCtrlGridBar1.btnExport.Enabled = False
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

        Dim strSQL As String = ""
        Dim ObjDataTable As DataTable
        Dim ObjDataRow As DataRow

        Try

            Dim ObjDalFinancialYear As New FiniancialYearDefDAL
            ObjDataTable = ObjDalFinancialYear.GetAll()

            ObjDataRow = ObjDataTable.NewRow
            ObjDataRow.Item("FYear Code") = gstrComboZeroIndexString
            ObjDataRow.Item("FYear ID") = 0
            ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


            cmbFinancialYear.DataSource = ObjDataTable.Copy


            cmbFinancialYear.DisplayMember = "FYear Code"
            cmbFinancialYear.ValueMember = "FYear ID"

            ObjDalFinancialYear = Nothing

            If Me.cmbFinancialYear.Items.Count > 1 Then
                Me.cmbFinancialYear.SelectedValue = gObjFinancialYearInfo.FYearID

            End If

            Dim ObjGLCostCenterDal As New GLCostCenterDal
            ObjDataTable = ObjGLCostCenterDal.GetAll()

            ObjDataRow = ObjDataTable.NewRow
            ObjDataRow.Item(1) = gstrComboZeroIndexString
            ObjDataRow.Item(0) = 0
            ObjDataTable.Rows.InsertAt(ObjDataRow, 0)

            cmbCostCenter.DisplayMember = ObjDataTable.Columns(1).ColumnName
            cmbCostCenter.ValueMember = ObjDataTable.Columns(0).ColumnName
            cmbCostCenter.DataSource = ObjDataTable.Copy

            Dim ObjDalCompany As New CompanyDAL
            ObjDataTable = ObjDalCompany.GetAll(gObjUserInfo.UserID)


            ObjDataRow = ObjDataTable.NewRow
            ObjDataRow.Item("Company Name") = gstrComboZeroIndexString
            ObjDataRow.Item("Company ID") = 0
            ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


            cmbCompany.DataSource = ObjDataTable.Copy


            cmbCompany.DisplayMember = "Company Name"
            cmbCompany.ValueMember = "Company ID"

            ObjDalCompany = Nothing

            If Me.cmbCompany.Items.Count > 1 Then
                Me.cmbCompany.SelectedValue = gobjLocationInfo.CompanyID

            End If

            Dim ObjDAL As New DAL.PostedVouchersDAL
            Dim DTConfigList As New DataTable
            DTConfigList = ObjDAL.GetTblshopConfigValues()

            If (DTConfigList.Rows.Count > 0) Then


                ObjDataRow = DTConfigList.NewRow
                ObjDataRow.Item("config_value") = gstrComboZeroIndexString
                DTConfigList.Rows.InsertAt(ObjDataRow, 0)

                cboShop.DataSource = DTConfigList.Copy

                cboShop.DisplayMember = "config_value"
                cboShop.ValueMember = "config_value"

                cboShop.SelectedIndex = 0
            Else
                Me.cboShop.Visible = False
                Me.Label4.Visible = False

            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

        Dim strSql As String
        Dim strLocationCriteria As String
        Dim strOtherVoucherCriteria As String
        Dim strCostCenterCriteria As String
        Dim strShop As String
        Dim intlocation_id As Integer
        Dim dt As DataTable

        Try
            Me.Cursor = Cursors.WaitCursor

            If Me.cmbCompany.SelectedIndex > 0 Then

                strLocationCriteria = " AND(dbo.tblGlDefLocation.location_id = " & Me.cmbCompany.SelectedValue & ") "
            Else
                strLocationCriteria = " "
            End If

            If Me.cmbCostCenter.SelectedIndex > 0 Then

                strCostCenterCriteria = " AND ((dbo.tblGlVoucherDetail.cost_center_id = " & cmbCostCenter.SelectedValue & ") "
            Else

                strCostCenterCriteria = ""
            End If


            If (Me.cboShop.SelectedIndex > 0 And Me.cmbCostCenter.SelectedIndex > 0) Then
                strShop = " OR (dbo.tblGlVoucher.source= '" & cboShop.SelectedValue & "') "
            ElseIf Me.cboShop.SelectedIndex > 0 Then

                strShop = " AND (dbo.tblGlVoucher.source= '" & cboShop.SelectedValue & "') "
            Else

                strShop = ""
            End If

            If Me.cmbCostCenter.SelectedIndex > 0 Then
                strShop += " )"
            End If

            '' if user dont check the checkbox of "include unposted vouchers" then user want to see only
            '' posted vouchers so we add the check
            If Me.chkOtherVoucher.Checked = False Then
                'If Me.chkOtherVoucher.Visible = False Then                     '275    Rana Saeed
                strOtherVoucherCriteria = " AND (tblGlVoucher.other_voucher = 0) "
            Else
                strOtherVoucherCriteria = ""
            End If
            '   get the location id
            If Me.cmbCompany.SelectedIndex > 0 Then

                intlocation_id = Me.cmbCompany.SelectedValue
            Else

                intlocation_id = 0
            End If


            'strSql = "SELECT tblGLDefGLNotes.Note_Title [PL_Note_Title], ISNULL(t.ClosingAmount,0) ClosingAmount, ISNULL(t.Voucher_Month,'') Voucher_Month , ISNULL(t.Voucher_Month,'') + '-' + ISNULL(SUBSTRING(t.Year_Code,0,5),'') Year_Code FROM tblGLDefGLNotes LEFT OUTER JOIN ( " & _
            '   " SELECT dbo.vwGlCOADetail.PL_Note_Title,dbo.vwGLCOADetail.PL_Note_ID ,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, tblGLVoucher.Voucher_Month, tblGLDefFinancialYear.Year_Code  " & _
            '   " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
            '   " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
            '   " WHERE (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(Me.dtFromDate.Value.Date, "yyyyMMdd") & "'  AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(Me.dtToDate.Value.Date, "yyyyMMdd") & "' " & strOtherVoucherCriteria & " " & strLocationCriteria & " " & strCostCenterCriteria & " " & strShop & " " & _
            '   " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id, tblGLVoucher.Voucher_Month " & _
            '   " HAVING (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) AND (dbo.tblGlDefFinancialYear.year_code = '" & Me.cmbFinancialYear.Text & "') AND (dbo.vwGlCOADetail.PL_note_id > 0 )" & _
            '   "  ) t ON tblGLDefGLNotes.gl_Note_ID = t.PL_Note_ID WHERE tblGLDefGLNotes.Note_Type = 'PL' " & _
            '   " ORDER BY CASE WHEN (CONVERT(DateTime,t.Voucher_Month + '1,1900')) IS NULL THEN '1900-01-01' ELSE CONVERT(DateTime,t.Voucher_Month + '1,1900') END "

            '266
            'strSql = " SELECT dbo.vwGlCOADetail.PL_Note_Title,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, tblGLVoucher.Voucher_Month,CASE WHEN MONTH(CONVERT(DateTime,tblGLVoucher.Voucher_Month + '1,1900')) < 7 THEN ISNULL(tblGLVoucher.Voucher_Month,'') + '-' + ISNULL(SUBSTRING(tblGLDefFinancialYear.Year_Code,6,4),'') " & _
            '   " ELSE ISNULL(tblGLVoucher.Voucher_Month,'') + '-' + ISNULL(SUBSTRING(tblGLDefFinancialYear.Year_Code,0,5),'') END Year_Code  " & _
            '   " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
            '   " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
            '   " WHERE (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(Me.dtFromDate.Value.Date, "yyyyMMdd") & "'  AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(Me.dtToDate.Value.Date, "yyyyMMdd") & "' " & strOtherVoucherCriteria & " " & strLocationCriteria & " " & strCostCenterCriteria & " " & strShop & " " & _
            '   " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id, tblGLVoucher.Voucher_Month " & _
            '   " HAVING (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) AND (dbo.tblGlDefFinancialYear.year_code = '" & Me.cmbFinancialYear.Text & "') AND (dbo.vwGlCOADetail.PL_note_id > 0 )" & _
            '   " ORDER BY CONVERT(DateTime,tblGLVoucher.Voucher_Month + '1,1900')"

            '266    Rana Saeed (if financial year not selected record should be available of selected date range)
            strSql = " SELECT dbo.vwGlCOADetail.PL_Note_Title,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) AS ClosingAmount, tblGLVoucher.Voucher_Month,CASE WHEN MONTH(CONVERT(DateTime,tblGLVoucher.Voucher_Month + '1,1900')) < 7 THEN ISNULL(tblGLVoucher.Voucher_Month,'') + '-' + ISNULL(SUBSTRING(tblGLDefFinancialYear.Year_Code,6,4),'') " & _
                           " ELSE ISNULL(tblGLVoucher.Voucher_Month,'') + '-' + ISNULL(SUBSTRING(tblGLDefFinancialYear.Year_Code,0,5),'') END Year_Code  " & _
                           " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN" & _
                           " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id  " & _
                           " WHERE (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.Voucher_Date) >= '" & Format(Me.dtFromDate.Value.Date, "yyyyMMdd") & "'  AND (dbo.tblGlVoucher.Voucher_Date) <= '" & Format(Me.dtToDate.Value.Date, "yyyyMMdd") & "' " & strOtherVoucherCriteria & " " & strLocationCriteria & " " & strCostCenterCriteria & " " & strShop & " " & _
                           " GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id, tblGLVoucher.Voucher_Month " & _
                           " HAVING (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) " & IIf(Me.cmbFinancialYear.SelectedIndex <= 0, "", "AND (dbo.tblGlDefFinancialYear.year_code = '" & Me.cmbFinancialYear.Text & "')") & " AND (dbo.vwGlCOADetail.PL_note_id > 0 )" & _
                           " ORDER BY CONVERT(DateTime,tblGLVoucher.Voucher_Month + '1,1900')"

            dt = PLMonthly(strSql)
            Me.grdAllRecords.DataSource = dt
            Me.grdAllRecords.RetrieveStructure()

            ApplyGridSettings()

        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Arrow
        End Try

    End Sub

    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

        Try
            If dtFromDate.Value.Date > dtToDate.Value.Date Then
                ShowValidationMessage("FromDate should be less than ToDate")
                dtFromDate.Focus()
                Return False
            End If

            'check that either financial year is selected or not
            'If Me.cmbFinancialYear.SelectedIndex <= 0 Then                 '266

            '    ShowValidationMessage("Select valid Financial year")
            '    Me.cmbFinancialYear.Focus()
            '    Return False

            'End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

        Try

            Me.btnFirst.ImageList = gobjMyImageListForOperationBar
            Me.btnFirst.ImageKey = "First"

            Me.btnNext.ImageList = gobjMyImageListForOperationBar
            Me.btnNext.ImageKey = "Next"

            Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
            Me.btnPrevious.ImageKey = "Previous"

            Me.btnLast.ImageList = gobjMyImageListForOperationBar
            Me.btnLast.ImageKey = "Last"


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
        Try
            Me.chkOtherVoucher.Visible = gblnShowOtherVoucher

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
        Try
            If Mode = EnumDataMode.[New] Then
                ''if New Mode then Set Disable all Navigation Buttons
                btnFirst.Enabled = False ': btnFirst.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnPrevious.Enabled = False ': btnPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnNext.Enabled = False ': btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnLast.Enabled = False ': btnLast.VisualStyle = C1.Win.C1Input.VisualStyle.System

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub frmProfitAndLossMonthWise_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Control And e.KeyCode = Keys.X Then
                Me.Close()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProfitAndLossMonthWise_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            ''Getting all available controls list to the user on this screen; in a collection 
            mobjControlList = GetFormSecurityControls(Me.Name)

            Me.ApplySecurity(EnumDataMode.Disabled)
            Me.SetButtonImages()

            dtFromDate.Value = DateAdd(DateInterval.Day, -7, Now)
            dtToDate.Value = Now

            ' Fill Combos Of Financial Year, Company nd Voucher Type ..   
            FillCombos()
            SetConfigurationBaseSetting()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnGenerateReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerateReport.Click

        Try
            If IsValidate() = False Then Exit Sub
            If Me.rdoSummary.Checked Then           '266
                GetAllRecords()
                Me.grpExpClp.Visible = False
            Else
                PnLDetailQuery()
                If Me.grdAllRecords.RowCount > 0 Then
                    Me.grpExpClp.Visible = True
                Else
                    Me.grpExpClp.Visible = False
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function PLMonthly(ByVal strSQLQuery As String) As DataTable

        Dim strSQL As String = String.Empty
        Dim dt As DataTable
        Dim dtNew As New DataTable("PLMonthly")
        Dim dtPLNotes As DataTable
        Dim dtCol As DataColumn
        Dim varStartDate As Date
        Dim strPLNotes As String = String.Empty
        Dim strMonths As New List(Of String)
        Dim dblRowTotal As Double = 0.0         '266    (To calculate rows total)
        Dim dblRowAverage As Double = 0.0       '266    (To calculate rows average)

        Try

            dtCol = New DataColumn("PL Note Title", GetType(String))
            dtNew.Columns.Add(dtCol)

            'dtCol = New DataColumn("PL Note ID", GetType(Integer))
            'dtNew.Columns.Add(dtCol)

            varStartDate = Me.dtFromDate.Value

            For intCount As Integer = 0 To (Microsoft.VisualBasic.DateDiff(DateInterval.Month, Me.dtFromDate.Value, Me.dtToDate.Value))

                dtCol = New DataColumn("" & varStartDate.Date.ToString("MMM-yyyy"), GetType(Double))
                dtNew.Columns.Add(dtCol)
                strMonths.Add(varStartDate.Date.ToString("MMM-yyyy"))

                varStartDate = varStartDate.Date.AddMonths(1)

            Next

            '266 Start
            Dim dcTotal As New DataColumn
            dcTotal.DataType = System.Type.GetType("System.Decimal")
            dcTotal.ColumnName = "Total"
            dtNew.Columns.Add(dcTotal)
            Dim dcAverage As New DataColumn
            dcAverage.DataType = System.Type.GetType("System.Decimal")
            dcAverage.ColumnName = "Average"
            dtNew.Columns.Add(dcAverage)
            '266 End

            'strSQL = " SELECT PL_Note_Title,PL_Note_ID,ISNULL(CY_ClosingAmount,0) AS CY_ClosingAmount,ISNULL(PY_ClosingAmount,0) AS PY_ClosingAmount,Voucher_Month FROM tblPLMonthly "
            dt = New DAL.DALReports().GetDataTable(strSQLQuery)
            dt.TableName = "PLMonthly"

            If dt.Rows.Count = 0 Then ShowInformationMessage("No Record Found") : Return dtNew

            Dim dv As DataView
            Dim dr As DataRow
            Dim dblTotal(4, strMonths.Count) As Double
            Dim intCheck As Integer = 0
            Dim strMonthColumn As String = String.Empty
            'Dim intTemp As Integer = 0
            'intTemp = strMonths.IndexOf(dt.Rows(0).Item("Year_Code"))

            strSQL = " SELECT Note_Title FROM tblGLDefGLNotes WHERE Note_Type = 'PL' "
            dtPLNotes = New DAL.DALReports().GetDataTable(strSQL)

            For intCount As Integer = 0 To dtPLNotes.Rows.Count - 1
                dblRowTotal = 0.0               '266
                If intCount = 2 Or intCount = 3 Or intCount = 6 Or intCount = 7 Then
                    intCheck = intCheck + 1
                End If

                dr = dtNew.NewRow
                dtNew.Rows.Add(dr)

                'strPLNotes = dt.Rows(intCount).Item("PL_Note_Title")
                'dtNew.Rows(intCount).Item("PL Note Title") = strPLNotes
                strPLNotes = dtPLNotes.Rows(intCount).Item("Note_Title")
                If strPLNotes = "Non Operating income" Then                             '266
                    dtNew.Rows(intCount).Item("PL Note Title") = "Non Operating Revenue"
                Else
                    dtNew.Rows(intCount).Item("PL Note Title") = strPLNotes
                End If
                'dtNew.Rows(intCount).Item("PL Note Title") = strPLNotes                'Commented under 266 to rename "Non Operating income"    Rana Saeed
                'dtNew.Rows(intCount).Item("PL Note ID") = dt.Rows(intCount).Item("PL_Note_ID")

                dv = GetFilterDataFromDataTable(dt, "[PL_Note_Title] = '" & strPLNotes & "'")
                For intVar As Integer = 0 To dv.Count - 1

                    'strMonthColumn = IIf(dv.Item(intVar).Item("Year_Code").ToString.Length = 1, strMonths(intTemp + intVar), dv.Item(intVar).Item("Year_Code"))
                    strMonthColumn = dv.Item(intVar).Item("Year_Code")

                    dtNew.Rows(intCount).Item(strMonthColumn) = IIf(Val(dv.Item(intVar).Item("ClosingAmount").ToString) < 0, Val(dv.Item(intVar).Item("ClosingAmount").ToString) * -1, Val(dv.Item(intVar).Item("ClosingAmount").ToString))
                    dblRowTotal = dblRowTotal + IIf(Val(dv.Item(intVar).Item("ClosingAmount").ToString) < 0, Val(dv.Item(intVar).Item("ClosingAmount").ToString) * -1, Val(dv.Item(intVar).Item("ClosingAmount").ToString)) '266 (calculate sum of row values)
                    dblTotal(intCheck, strMonths.IndexOf(strMonthColumn)) = dblTotal(intCheck, strMonths.IndexOf(strMonthColumn)) + Val(dv.Item(intVar).Item("ClosingAmount").ToString)

                Next
                If dblRowTotal <> 0.0 Then
                    dtNew.Rows(intCount).Item("Total") = Val(dtNew.Rows(intCount).Item("Total").ToString) + dblRowTotal    '266 (place total of row values in Total Cell)
                    dblRowAverage = Val(dtNew.Rows(intCount).Item("Total").ToString) / strMonths.Count          'calculate average
                    dtNew.Rows(intCount).Item("Average") = dblRowAverage
                End If
            Next

            dtNew.AcceptChanges()
            Dim intNetTotal As Double = 0
            Dim i As Integer = 0
            Dim dblTotalProfit(strMonths.Count) As Double
            'Loop for 6 times to add rows
            For intCount As Integer = 2 To 14 Step 2
                dblRowTotal = 0.0           '266
                dblRowAverage = 0.0
                dr = dtNew.NewRow()
                If intCount = 6 Then intCount = 8
                If intCount = 2 Then
                    strPLNotes = "Gross Profit:"
                ElseIf intCount = 4 Then
                    'strPLNotes = "Operating Expenses"               '266
                    strPLNotes = "Gross Operating Income:"               '266
                ElseIf intCount = 8 Then
                    strPLNotes = "Net Expenses:"
                ElseIf intCount = 9 Then
                    strPLNotes = "Operating Profit:"
                ElseIf intCount = 11 Then
                    strPLNotes = "Profit Before Taxation:"
                ElseIf intCount = 13 Then
                    strPLNotes = "Profit After Taxation:"
                End If

                dtNew.Rows.InsertAt(dr, intCount)
                dtNew.Rows(intCount).Item("PL Note Title") = strPLNotes

                For intVar As Integer = 0 To strMonths.Count - 1

                    If intCount = 8 Then
                        dtNew.Rows(intCount).Item(strMonths(intVar).ToString) = IIf(dblTotal(i, intVar) = 0, DBNull.Value, IIf(dblTotal(i, intVar) < 0, dblTotal(i, intVar) * -1, dblTotal(i, intVar)))
                        dblRowTotal = dblRowTotal + IIf(dblTotal(i, intVar) = 0, 0.0, IIf(dblTotal(i, intVar) < 0, dblTotal(i, intVar) * -1, dblTotal(i, intVar)))         '266
                    Else
                        dblTotalProfit(intVar) = dblTotalProfit(intVar) + dblTotal(i, intVar)
                        dtNew.Rows(intCount).Item(strMonths(intVar).ToString) = IIf(dblTotalProfit(intVar) = 0, DBNull.Value, dblTotalProfit(intVar))
                        dblRowTotal = dblRowTotal + IIf(dblTotalProfit(intVar) = 0, 0.0, dblTotalProfit(intVar))       '266
                    End If

                Next
                If dblRowTotal <> 0.0 Then
                    dtNew.Rows(intCount).Item("Total") = Val(dtNew.Rows(intCount).Item("Total").ToString) + dblRowTotal        '266
                    dblRowAverage = Val(dtNew.Rows(intCount).Item("Total").ToString) / strMonths.Count
                    dtNew.Rows(intCount).Item("Average") = dblRowAverage
                End If
                
                If intCount <> 8 Then
                    i = i + 1
                End If


                If intCount = 8 Then intCount = 7
            Next

            dtNew.AcceptChanges()

            Return dtNew

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbFinancialYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFinancialYear.SelectedIndexChanged

        If cmbFinancialYear.SelectedIndex > 0 Then
            Dim dtRow As DataRowView = CType(cmbFinancialYear.SelectedItem, DataRowView)

            dtFromDate.MinDate = dtRow("Start Date")
            dtToDate.MaxDate = dtRow("End Date")

            dtFromDate.Value = dtRow("Start Date")
            dtToDate.Value = dtRow("End Date")

            'CR # 274

        Else                   '266    don't change if selected index is less than or equal to zero. as per said by Abdul Jabbar.


            dtFromDate.MinDate = CDate("01/07/1980")
            dtToDate.MaxDate = CDate("01/01/2099")

            'Me.dtFromDate.Value = gobjBusinessStartDate
            'Me.dtToDate.Value = Now

        End If
    End Sub

    '266        Start   
    Private Sub PnLDetailQuery()
        Try
            Dim strsql As New System.Text.StringBuilder
            Dim strOtherVoucherCriteria As String = ""
            Dim strLocationCriteria As String = ""
            Dim strYearCriteria As String = ""
            Dim strCostCenterCriteria As String = ""
            Dim strShop As String = ""
            Dim dtPnLDetail As New DataTable

            If Me.cmbFinancialYear.SelectedIndex > 0 Then
                strYearCriteria = " AND (dbo.tblGlDefFinancialYear.year_code = '" & Me.cmbFinancialYear.Text & "')"
            Else
                strYearCriteria = " "
            End If

            If Me.cmbCompany.SelectedIndex > 0 Then
                strLocationCriteria = " (dbo.tblGlDefLocation.location_id = " & Me.cmbCompany.SelectedValue & ") "
            Else
                strLocationCriteria = " "
            End If

            If Me.cmbCostCenter.SelectedIndex > 0 Then
                strCostCenterCriteria = " AND ((dbo.tblGlVoucherDetail.cost_center_id = " & cmbCostCenter.SelectedValue & ") "
            Else
                strCostCenterCriteria = ""
            End If

            If (Me.cboShop.SelectedIndex > 0 And Me.cmbCostCenter.SelectedIndex > 0) Then
                strShop = " OR (dbo.tblGlVoucher.source= '" & cboShop.SelectedValue & "') "
            ElseIf Me.cboShop.SelectedIndex > 0 Then
                strShop = " AND (dbo.tblGlVoucher.source= '" & cboShop.SelectedValue & "') "
            Else
                strShop = ""
            End If

            If Me.cmbCostCenter.SelectedIndex > 0 Then
                strShop += " )"
            End If

            If Me.chkOtherVoucher.Checked = False Then
                strOtherVoucherCriteria = " AND (tblGlVoucher.other_voucher = 0) "
            Else
                strOtherVoucherCriteria = ""
            End If


            strsql.AppendLine("SELECT     TOP (100) PERCENT tblGlDefGLNotes.note_no, tblGlDefGLNotes.note_title AS PL_Note_Title, tblGlCOAMainSubSub.sub_sub_code, tblGlCOAMainSubSub.sub_sub_title, ")
            strsql.AppendLine("tblGlCOAMainSubSubDetail.detail_code, tblGlCOAMainSubSubDetail.detail_title, ISNULL(CurrDtl.ClosingAmount, 0) AS ClosingAmount, CurrDtl.year_code ")
            strsql.AppendLine("FROM tblGlDefGLNotes INNER JOIN tblGlCOAMainSubSub ON tblGlDefGLNotes.gl_note_id = tblGlCOAMainSubSub.PL_note_id INNER JOIN ")
            strsql.AppendLine("tblGlCOAMainSubSubDetail ON tblGlCOAMainSubSub.main_sub_sub_id = tblGlCOAMainSubSubDetail.main_sub_sub_id LEFT OUTER JOIN ")
            strsql.AppendLine("(SELECT     vwGlCOADetail.PL_Note_Title, vwGlCOADetail.sub_sub_code, vwGlCOADetail.sub_sub_title, vwGlCOADetail.detail_code,  ")
            strsql.AppendLine("vwGlCOADetail.detail_title, SUM(tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(tblGlVoucherDetail.credit_amount)  ")
            strsql.AppendLine("AS credit_amount, SUM(tblGlVoucherDetail.credit_amount) - SUM(tblGlVoucherDetail.debit_amount) AS ClosingAmount,  ")
            strsql.AppendLine("tblGlDefFinancialYear.year_code AS Year, tblGlDefLocation.location_code, tblGlDefLocation.location_name, tblGlDefLocation.location_id, ")
            strsql.AppendLine("tblGlVoucher.voucher_month, CASE WHEN MONTH(CONVERT(DateTime, tblGLVoucher.Voucher_Month + '1,1900')) ")
            strsql.AppendLine("< 7 THEN ISNULL(tblGLVoucher.Voucher_Month, '') + '-' + ISNULL(SUBSTRING(tblGLDefFinancialYear.Year_Code, 6, 4), '') ")
            strsql.AppendLine("ELSE ISNULL(tblGLVoucher.Voucher_Month, '') + '-' + ISNULL(SUBSTRING(tblGLDefFinancialYear.Year_Code, 0, 5), '') END AS Year_Code ")
            strsql.AppendLine("FROM tblGlVoucherDetail INNER JOIN vwGlCOADetail ON tblGlVoucherDetail.coa_detail_id = vwGlCOADetail.coa_detail_id INNER JOIN ")
            strsql.AppendLine("tblGlVoucher ON tblGlVoucherDetail.voucher_id = tblGlVoucher.voucher_id AND tblGlVoucherDetail.location_id = tblGlVoucher.location_id INNER JOIN ")
            strsql.AppendLine("tblGlDefFinancialYear ON tblGlVoucher.finiancial_year_id = tblGlDefFinancialYear.financial_year_id INNER JOIN tblGlDefLocation ON tblGlVoucher.location_id = tblGlDefLocation.location_id ")
            ' strsql.AppendLine("WHERE (tblGlVoucher.post = 1) " & strOtherVoucherCriteria & " AND (tblGlVoucher.voucher_date <= '" & Format(Me.dtToDate.Value.Date, "yyyyMMdd") & "') AND (tblGlVoucher.voucher_date >= '" & Format(dtFromDate.Value.Date, "yyyyMMdd") & "') ")    '275
            strsql.AppendLine("WHERE " & strLocationCriteria & " AND (tblGlVoucher.post = 1) " & strOtherVoucherCriteria & " AND (tblGlVoucher.voucher_date <= '" & Format(Me.dtToDate.Value.Date, "yyyyMMdd") & "') ")         '275
            strsql.AppendLine("AND (tblGlVoucher.voucher_date >= '" & Format(dtFromDate.Value.Date, "yyyyMMdd") & "') " & strYearCriteria & " " & strCostCenterCriteria & " " & strShop)            '275
            strsql.AppendLine("GROUP BY vwGlCOADetail.PL_Note_Title, tblGlDefFinancialYear.year_code, vwGlCOADetail.PL_note_id, tblGlDefLocation.location_code, ")
            strsql.AppendLine("tblGlDefLocation.location_name, tblGlDefLocation.location_id, vwGlCOADetail.sub_sub_code, vwGlCOADetail.sub_sub_title, ")
            strsql.AppendLine("vwGlCOADetail.detail_code, vwGlCOADetail.detail_title, tblGlVoucher.voucher_month ")
            '******** Commented under cr # 275   Rana Saeed
            'strsql.AppendLine("HAVING " & strLocationCriteria & " AND (vwGlCOADetail.PL_Note_Title IS NOT NULL) " & strYearCriteria & " " & strCostCenterCriteria & " " & strShop)
            'strsql.AppendLine(" AND (vwGlCOADetail.PL_note_id > 0)) AS CurrDtl ON tblGlCOAMainSubSubDetail.detail_code = CurrDtl.detail_code AND tblGlCOAMainSubSub.sub_sub_code = CurrDtl.sub_sub_code ")
            strsql.AppendLine("HAVING (vwGlCOADetail.PL_Note_Title IS NOT NULL)AND (vwGlCOADetail.PL_note_id > 0)) AS CurrDtl ")        '275
            strsql.AppendLine("ON tblGlCOAMainSubSubDetail.detail_code = CurrDtl.detail_code AND tblGlCOAMainSubSub.sub_sub_code = CurrDtl.sub_sub_code ")  '275
            strsql.AppendLine("WHERE     (ISNULL(CurrDtl.ClosingAmount, 0) <> 0) ORDER BY CurrDtl.year_code")
            ' tblGlDefGLNotes.note_no, tblGlCOAMainSubSub.sub_sub_title, tblGlCOAMainSubSubDetail.detail_title")

            dtPnLDetail = New DAL.DALReports().GetDataTable(strsql.ToString)

            Call Me.PnLCreateFinalView(dtPnLDetail)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PnLCreateFinalView(ByVal dtPnL As DataTable)
        Try
            Dim dtNew As New DataTable("PLMonthly")
            Dim dtCol As DataColumn
            Dim varStartDate As Date
            Dim strMonths As New List(Of String)
            
            dtCol = New DataColumn("PL Note Title", GetType(String))
            dtNew.Columns.Add(dtCol)
            varStartDate = Me.dtFromDate.Value
            dtCol = New DataColumn("Sub Sub Title", GetType(String))
            dtNew.Columns.Add(dtCol)
            varStartDate = Me.dtFromDate.Value
            dtCol = New DataColumn("Detail Title", GetType(String))
            dtNew.Columns.Add(dtCol)


            varStartDate = Me.dtFromDate.Value
            dtPnL.TableName = "PLMonthly"

            For intCount As Integer = 0 To (Microsoft.VisualBasic.DateDiff(DateInterval.Month, Me.dtFromDate.Value, Me.dtToDate.Value))
                dtCol = New DataColumn("" & varStartDate.Date.ToString("MMM-yyyy"), GetType(Double))
                dtNew.Columns.Add(dtCol)
                strMonths.Add(varStartDate.Date.ToString("MMM-yyyy"))
                varStartDate = varStartDate.Date.AddMonths(1)
            Next

            Dim dcTotal As New DataColumn
            dcTotal.DataType = System.Type.GetType("System.Decimal")
            dcTotal.ColumnName = "Total"
            dtNew.Columns.Add(dcTotal)
            Dim dcAverage As New DataColumn
            dcAverage.DataType = System.Type.GetType("System.Decimal")
            dcAverage.ColumnName = "Average"
            dtNew.Columns.Add(dcAverage)

            If dtPnL.Rows.Count = 0 Then
                ShowErrorMessage("No record found.")
                Me.grdAllRecords.DataSource = Nothing
                Exit Sub
            End If
            Dim drow As DataRow
            Dim intRowPos As Integer = 0

            '///////////// Reading all accounts in new table /////////
            Dim DrAccounts As DataRow()
            Dim strExpression As String
            Dim strSQL As String
            Dim dtPLNotes As DataTable
            strSQL = " SELECT Note_Title FROM tblGLDefGLNotes WHERE Note_Type = 'PL' "
            dtPLNotes = New DAL.DALReports().GetDataTable(strSQL)

            'strExpression = dtPnL.Rows(0).Item("year_code").ToString()
            'DrAccounts = dtPnL.Select(" year_code='" & strExpression & "'", "")

            'If DrAccounts.Length > 0 Then
            '    For Each Dr As DataRow In DrAccounts
            '        drow = dtNew.NewRow
            '        drow("PL Note Title") = Dr.Item("PL_Note_Title")
            '        drow("Sub Sub Title") = Dr.Item("sub_sub_title")
            '        drow("Detail Title") = Dr.Item("detail_title")
            '        dtNew.Rows.Add(drow)
            '    Next
            'End If
            If dtPLNotes.Rows.Count > 0 Then
                For Each drPLNotes As DataRow In dtPLNotes.Rows

                    'CR # 274
                    'DrAccounts = dtPnL.Select(" PL_Note_Title='" & drPLNotes("Note_Title") & "'", "")
                    DrAccounts = dtPnL.Select(" PL_Note_Title='" & funFilterReserveText(drPLNotes("Note_Title")) & "'", "")
                    If DrAccounts.Length > 0 Then

                        For Each dr As DataRow In DrAccounts
                            Dim drExists As DataRow()
                            'Cr # 274
                            'drExists = dtNew.Select(" [PL Note Title]='" & dr.Item("PL_Note_Title") & "' AND [Sub Sub Title]='" & dr.Item("sub_sub_title") & "' AND [Detail Title]='" & dr.Item("detail_title") & "'", "")
                            drExists = dtNew.Select(" [PL Note Title]='" & funFilterReserveText(dr.Item("PL_Note_Title")) & "' AND [Sub Sub Title]='" & funFilterReserveText(dr.Item("sub_sub_title")) & "' AND [Detail Title]='" & funFilterReserveText(dr.Item("detail_title")) & "'", "")
                            If drExists.Length = 0 Then
                                drow = dtNew.NewRow
                                drow("PL Note Title") = dr.Item("PL_Note_Title")
                                drow("Sub Sub Title") = dr.Item("sub_sub_title")
                                drow("Detail Title") = dr.Item("detail_title")
                                dtNew.Rows.Add(drow)
                            End If
                        Next
                    End If
                Next
            End If

            '//Reading accounts closing amount for each month'//
            Dim drAccountsClosing As DataRow()
            Dim dblRowTotal As Double = 0.0
            Dim dblRowAvg As Double = 0.0
            For Each dr As DataRow In dtNew.Rows

                'CR # 274
                'strExpression = " detail_title='" & dr.Item("Detail title") & "'"
                strExpression = " detail_title='" & funFilterReserveText(dr.Item("Detail title")) & "'"
                drAccountsClosing = dtPnL.Select(strExpression)

                For Each col As DataColumn In dtNew.Columns
                    If col.ColumnName <> "PL Note Title" AndAlso col.ColumnName <> "Sub Sub Title" AndAlso col.ColumnName <> "Detail Title" AndAlso col.ColumnName <> "Total" AndAlso col.ColumnName <> "Average" Then
                        For Each Rows As DataRow In drAccountsClosing
                            If Rows.Item("Year_code").ToString() = col.ColumnName.ToString() Then
                                dr.Item(col.ColumnName) = Rows.Item("ClosingAmount")
                                dr.Item("Total") = IIf(IsDBNull(dr.Item("Total")), 0.0, dr.Item("Total")) + Rows.Item("ClosingAmount")
                                dblRowTotal = IIf(IsDBNull(dr.Item("Total")), 0.0, dr.Item("Total")) + Rows.Item("ClosingAmount")
                            End If
                        Next
                        If Not dblRowTotal = 0.0 Then
                            dblRowAvg = Val(dr.Item("Total").ToString) / strMonths.Count
                            dr.Item("Average") = dblRowAvg
                        End If
                    End If
                Next
            Next

            Me.grdAllRecords.DataSource = dtNew
            Me.grdAllRecords.RetrieveStructure()
            Me.ApplyGridSettings()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnExp2ndLvl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExp2ndLvl.Click, btnExp1stLvl.Click, btnExp3rdLvl.Click
        Try
            'CR # 274
            If Me.grdAllRecords.RowCount = 0 Then
                Exit Sub
            End If

            Dim btn As Windows.Forms.Button = CType(sender, Windows.Forms.Button)
            If btn.Name = Me.btnExp1stLvl.Name Then
                If Me.intExp1stLvl = 0 Then
                    Me.grdAllRecords.RootTable.Groups(0).Expand()
                    Me.intExp1stLvl = 1
                Else
                    Me.grdAllRecords.RootTable.Groups(0).Collapse()
                    Me.intExp1stLvl = 0
                End If

            ElseIf btn.Name = Me.btnExp2ndLvl.Name Then
                If Me.intExp2ndLvl = 0 Then
                    Me.grdAllRecords.RootTable.Groups(1).Expand()
                    Me.intExp2ndLvl = 1
                Else
                    Me.grdAllRecords.RootTable.Groups(1).Collapse()
                    Me.intExp2ndLvl = 0
                End If

            ElseIf btn.Name = Me.btnExp3rdLvl.Name Then
                If Me.intExp3rdLvl = 0 Then
                    Me.grdAllRecords.RootTable.Groups(2).Expand()
                    Me.intExp3rdLvl = 1
                Else
                    Me.grdAllRecords.RootTable.Groups(2).Collapse()
                    Me.intExp3rdLvl = 0
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '266        End

    'Cr # 274
    Private Function funFilterReserveText(ByVal Txt As String) As String
        funFilterReserveText = Replace(Txt, "'", "''", , , vbTextCompare)
    End Function
End Class