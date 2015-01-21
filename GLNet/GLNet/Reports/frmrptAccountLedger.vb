''/////////////////////////////////////////////////////////////////////////////////////////
''//                      GL Net
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmrptAccountLedger
''// Programmer	     : Rizwan Asif
''// Creation Date	 : 21-July-2009
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''// 29-Jul-2009        Fahad               Added  functionality for showing/hiding 
''//                                        Other  Voucher checkbox based on configuration value  
''// 18-May-2010        A Jabbar           CR#49.  Account Ledger Report ,Main account and Detail account selection is not appropriate
''// 27-Aug-2012        A Jabbar           CR#220. Account Ledger report; add source filter on report.
''// 04-Oct-2012        Asif Kamal         CR#221   Add Cost Center filter on Account Ledger Report
''// 17 Jul,2014        Abdul Jabbar       CR#320. Include source filter in Trial Balance, Balance Sheet and P&L Report
''// 01 Jan,2015        M.Shoaib           CR#355. Account Ledger: New Account Ledger reprot to display Cost Center (Landscape)
''/////////////////////////////////////////////////////////////////////////////////////////


Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports Microsoft.VisualBasic
Public Class frmrptAccountLedger
    Implements IGeneral, IReportsInterface

    Dim mobjControlList As NameValueCollection
    Dim objHashTableParamter As Hashtable
    Dim strSQL As String

    Private Sub frmrptAccountLedger_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control And e.KeyCode = Keys.X Then
            If Me.btnExit.Enabled = True Then Me.Close()
        ElseIf e.Control And e.KeyCode = Keys.P Then
            If Me.btnPrint.Enabled = True Then btnGenerateButton_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmGLVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If gObjFinancialYearInfo.YearCode = String.Empty Then
            ShowValidationMessage("No Financial Year selected. Please define a Financial Year and then select Financial Year while loging In to program")
            Exit Sub
            Me.Close()
            End
        End If

        ''Getting all available controls list to the user on this screen; in a collection 
        mobjControlList = GetFormSecurityControls(Me.Name)

        ''Assing Images to Buttons
        Me.SetButtonImages()

        ApplySecurity(EnumDataMode.Disabled)
        ' Fill Combos Of Financial Year, Company nd Voucher Type ..   
        FillCombos()

        'TODO: Implement Encryption on Account Ledger (Rizwan)
        'chkOtherVoucher.Visible = IIf(DecryptWithCSP(funGetConfigValue("Other_Voucher"), "true") = "1", True, False)
        'chkOtherVoucher.Value = vbUnchecked

        SetConfigurationBaseSetting()

        Me.ApplySecurity(EnumDataMode.[New])


    End Sub

#Region "Report Interface Metholds .. "

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
    End Sub

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

        Dim strSQL As String = ""
        Dim ObjDataTable As DataTable
        Dim ObjDataRow As DataRow


        ' Binding Financial Year Combo .. 
        ' =========================================================================================
        ' =========================================================================================
        Dim ObjDalFinancialYear As New FiniancialYearDefDAL
        ObjDataTable = ObjDalFinancialYear.GetAll(Me.cmbCompany.SelectedValue)


        ObjDataRow = ObjDataTable.NewRow
        ObjDataRow.Item("FYear Code") = gstrComboZeroIndexString
        ObjDataRow.Item("FYear ID") = 0
        ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


        cmbFinancialYear.DataSource = ObjDataTable.Copy


        cmbFinancialYear.DisplayMember = "FYear Code"
        cmbFinancialYear.ValueMember = "FYear ID"

        If Me.cmbFinancialYear.Items.Count > 1 Then
            Me.cmbFinancialYear.SelectedValue = gObjFinancialYearInfo.FYearID

        End If



        ObjDalFinancialYear = Nothing
        ' =========================================================================================
        ' =========================================================================================


        ' Binding Company .. 
        ' =========================================================================================
        ' =========================================================================================
        Dim ObjDalCompany As New CompanyDAL
        ObjDataTable = ObjDalCompany.GetAll(gObjUserInfo.UserID)


        ObjDataRow = ObjDataTable.NewRow
        ObjDataRow.Item("Company Name") = gstrComboZeroIndexString
        ObjDataRow.Item("Company ID") = 0
        ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


        cmbCompany.DataSource = ObjDataTable.Copy


        cmbCompany.DisplayMember = "Company Name"
        cmbCompany.ValueMember = "Company ID"

        If Me.cmbCompany.Items.Count > 1 Then
            Me.cmbCompany.SelectedValue = gobjLocationInfo.CompanyID
        End If

        ObjDalCompany = Nothing
        ' =========================================================================================
        ' =========================================================================================
        cmbSource.Items.Clear()

        Dim ObjDAL As New DAL.PostedVouchersDAL
        Dim DTConfigList As New DataTable

        DTConfigList = ObjDAL.GetTblshopConfigValues()

        If DTConfigList.Rows.Count > 0 Then

            ObjDataRow = DTConfigList.NewRow
            ObjDataRow.Item("config_value") = gstrComboZeroIndexString
            DTConfigList.Rows.InsertAt(ObjDataRow, 0)

            ObjDataRow = DTConfigList.NewRow
            ObjDataRow.Item("config_value") = "Accounts"
            DTConfigList.Rows.InsertAt(ObjDataRow, 1)

            cmbSource.DataSource = DTConfigList
            cmbSource.SelectedIndex = 0

            cmbSource.DisplayMember = "config_value"
            cmbSource.ValueMember = "config_value"

        Else

            cmbSource.Items.Add(gstrComboZeroIndexString)
            cmbSource.Items.Add("Accounts")
            cmbSource.SelectedIndex = 0

        End If

        'CR 221 [Asif Kamal]        Add Cost Center filter on Account Ledger Report
        Dim ObjGLCostCenterDal As New GLCostCenterDal
        ObjDataTable = ObjGLCostCenterDal.GetAll()

        ObjDataRow = ObjDataTable.NewRow
        ObjDataRow.Item(1) = gstrComboZeroIndexString
        ObjDataRow.Item(0) = 0
        ObjDataTable.Rows.InsertAt(ObjDataRow, 0)


        cmbCostCenter.DisplayMember = ObjDataTable.Columns(1).ColumnName
        cmbCostCenter.ValueMember = ObjDataTable.Columns(0).ColumnName
        cmbCostCenter.DataSource = ObjDataTable.Copy



    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try


       
            btnPrint.Enabled = True

            If Mode.ToString = EnumDataMode.Disabled.ToString Then

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System           
                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                'If mobjControlList.Item("btnPrint") Is Nothing Then
                '    btnPrint.Enabled = False
                'Else
                '    btnPrint.Enabled = True
                'End If

                SetNavigationButtons(EnumDataMode.[New])
                ' Me.grdAllRecords.Enabled = True
            ElseIf Mode.ToString = EnumDataMode.[New].ToString Then

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System

                If mobjControlList.Item("btnSave") Is Nothing Then
                    btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System
                Else
                    btnSave.Enabled = True ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                End If

                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(Mode)

                ' Me.grdAllRecords.Enabled = False

            ElseIf Mode.ToString = EnumDataMode.Edit.ToString Then

                btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System

                If mobjControlList.Item("btnUpdate") Is Nothing Then
                    btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                Else
                    btnUpdate.Enabled = True ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                End If

                If mobjControlList.Item("btnDelete") Is Nothing Then
                    btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                Else
                    btnDelete.Enabled = True ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                End If
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

                SetNavigationButtons(Mode)

                '  Me.grdAllRecords.Enabled = True

                '  Me.grdAllRecords.Focus()

            ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

                btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System


                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

                SetNavigationButtons(Mode)

                ' Me.grdAllRecords.Enabled = True

                '  Me.grdAllRecords.Focus()

            End If


            '' Disabl/Enable the Button that converts Grid data in Excel Sheet According to Login User rights
            If mobjControlList.Item("btnExport") Is Nothing Then
                ' Me.UiCtrlGridBar1.btnExport.Enabled = False
            End If


            '' Disabl/Enable the Button that Prints Grid data According to Login User rights
            If mobjControlList.Item("btnPrint") Is Nothing Then
                '   Me.UiCtrlGridBar1.btnPrint.Enabled = False
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages
        Try

            Me.btnFirst.ImageList = gobjMyImageListForOperationBar
            Me.btnFirst.ImageKey = "First"

            Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
            Me.btnPrevious.ImageKey = "Previous"

            Me.btnNext.ImageList = gobjMyImageListForOperationBar
            Me.btnNext.ImageKey = "Next"

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

            Me.btnPrint.ImageList = gobjMyImageListForOperationBar
            Me.btnPrint.ImageKey = "Print"
            ' CR # 355
            Me.btnPrintLandScape.ImageList = gobjMyImageListForOperationBar
            Me.btnPrintLandScape.ImageKey = "Print"

            Me.btnExit.ImageList = gobjMyImageListForOperationBar
            Me.btnExit.ImageKey = "Exit"

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

            ElseIf Mode = EnumDataMode.Edit Then
                ''if New Mode then Set Enable all Navigation Buttons
                btnFirst.Enabled = True ': btnFirst.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnPrevious.Enabled = True ': btnPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnNext.Enabled = True ': btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnLast.Enabled = True ': btnLast.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    ''//-------------------------------------------------------------------------------------
    ''// Date Modified     Modified by         Brief Description			                
    ''//------------------------------------------------------------------------------------
    ''// 29-Jul-2009        Fahad               Added functionality for showing/hiding 
    ''//                                        Other Voucher checkbox based on configuration value  
    ''//-------------------------------------------------------------------------------------
    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting
        Try            
            chkIncludeOtherVouchers.Visible = gblnShowOtherVoucher
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
    End Function

#End Region

#Region "Report Interface Metholds .. "

    Public Function FunAddReportCriteria() As String Implements IReportsInterface.FunAddReportCriteria

    End Function

    Function GoForSelectionCriteria(ByVal trans As SqlClient.SqlTransaction) As Boolean
        Try

            Dim strYearCriteria As String
            Dim strLocationCriteria As String

            '=========================================================================================
            '-- Selection Criteia Building in case of Financial Year selection
            '=========================================================================================
            If Me.cmbFinancialYear.SelectedIndex > 0 Then

                strYearCriteria = " (dbo.tblGlVoucher.finiancial_year_id = " & Me.cmbFinancialYear.SelectedValue & " ) AND "
            Else

                strYearCriteria = String.Empty
            End If

            '=========================================================================================
            '-- Selection Criteia Building in case of Location selection
            '=========================================================================================
            If Me.cmbCompany.SelectedIndex > 0 Then
                strLocationCriteria = " (dbo.tblGlVoucher.location_id = " & Me.cmbCompany.SelectedValue & ") AND "
            Else
                strLocationCriteria = String.Empty
            End If

            strSQL = "alter view vwGlAcLedgerForPeriod as " & _
                     " SELECT top 100 percent dbo.tblGlVoucherDetail.coa_detail_id, dbo.tblGlVoucher.voucher_code, dbo.tblGlVoucher.finiancial_year_id, dbo.tblGlVoucher.voucher_type_id, " & _
                     " dbo.tblGlVoucher.location_id, dbo.tblGlVoucher.voucher_month, dbo.tblGlVoucher.voucher_no, dbo.tblGlVoucher.voucher_date, " & _
                     " dbo.tblGlVoucher.paid_to, dbo.tblGlVoucher.cheque_no, dbo.tblGlVoucher.cheque_date, dbo.tblGlVoucher.cheque_paid, " & _
                     " dbo.tblGlVoucher.cheque_paid_date, dbo.tblGlVoucher.post, dbo.tblGlVoucherDetail.comments, IsNull(dbo.tblGlVoucherDetail.debit_amount,0) AS debit_amount, " & _
                     " IsNull(dbo.tblGlVoucherDetail.credit_amount,0) AS credit_amount , dbo.tblGlVoucherDetail.cost_center_id " & _
                     " FROM dbo.tblGlVoucherDetail INNER JOIN dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id INNER JOIN " & _
                     " dbo.tblGlCOAMainSubSubDetail ON dbo.tblGlVoucherDetail.coa_detail_id = dbo.tblGlCOAMainSubSubDetail.coa_detail_id INNER JOIN  " & _
                     " dbo.tblGlDefFinancialYear ON dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id " & _
                     " WHERE " & strYearCriteria & strLocationCriteria & "  (dbo.tblGlCOAMainSubSubDetail.detail_code LIKE '" & Me.txtAccount.txtACCode.Text.Trim & "%') "

            '' if user dont check the checkbox of "include unposted vouchers" then user want to see only
            '' posted vouchers so we add the check
            If Me.chkIncludeUnPostedVouchers.Checked = False Then strSQL = strSQL + "  AND (dbo.tblGlVoucher.post = 1) "


            '' if user dont check the checkbox of "include other vouchers" then user not want to see
            '' other vouchers so we add the check
            If Me.chkIncludeOtherVouchers.Checked = False Then strSQL = strSQL + "  AND (dbo.tblGlVoucher.Other_Voucher = 0) "

            'CR#220
            If Me.cmbSource.SelectedIndex > 0 Then
                strSQL = strSQL + "  and tblGlVoucher.source = '" & Me.cmbSource.Text & "'"
            End If

            strSQL = strSQL + " AND Voucher_No <> '000000' AND (dbo.tblGlVoucher.voucher_date BETWEEN '" & Format(Me.dtFromDate.Value, pbDateFormat) & "' AND '" & Format(Me.dtToDate.Value, pbDateFormat) & "') ORDER BY tblGlVoucher.voucher_date"

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

            Dim ilocation_id As Integer
            '   get the location id
            If Me.cmbCompany.SelectedIndex > 0 Then
                ilocation_id = Me.cmbCompany.SelectedValue
            Else
                ilocation_id = 0
            End If

            '   Executing stored procedure to to Insert Opening data into the temp table

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, "usp_AccOpeningBalance '%" & Me.txtAccount.txtACCode.Text.Trim & "','" & Format(Me.dtFromDate.Value, "yyyy/MM/dd") & "'," & ilocation_id & "," & IIf(Me.chkIncludeUnPostedVouchers.Checked = True, 1, 0) & "," & IIf(Me.chkIncludeOtherVouchers.Checked = True, 1, 0), Nothing)

            strSQL = " ALTER VIEW vwGlAcLedgerOpening as " & _
                     " SELECT     dbo.tblGlCOAMainSubSubDetail.coa_detail_id, Sum(dbo.tmpTblGLAccountsOpening.Opening_debit_Amount) AS Opening_debit_Amount," & _
                     " SUM(dbo.tmpTblGLAccountsOpening.Opening_credit_Amount) AS Opening_credit_Amount  , SUM(dbo.tmpTblGLAccountsOpening.OpeningBalance) AS OpeningBalance " & _
                     " FROM         dbo.tblGlCOAMainSubSubDetail INNER JOIN " & _
                     " dbo.tmpTblGLAccountsOpening ON dbo.tblGlCOAMainSubSubDetail.coa_detail_id = dbo.tmpTblGLAccountsOpening.coa_detail_id" & _
                     " GROUP BY dbo.tblGlCOAMainSubSubDetail.coa_detail_id"

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)


            strSQL = "Alter view vwGlAcLedger as " & _
                  "SELECT dbo.vwGlCOADetail.detail_code, dbo.vwGlCOADetail.detail_title, dbo.vwGlAcLedgerForPeriod.voucher_code, " & _
                  "dbo.vwGlAcLedgerForPeriod.voucher_type_id, dbo.vwGlAcLedgerForPeriod.location_id, dbo.vwGlAcLedgerForPeriod.voucher_month, " & _
                  "dbo.vwGlAcLedgerForPeriod.finiancial_year_id, dbo.vwGlAcLedgerForPeriod.voucher_no, dbo.vwGlAcLedgerForPeriod.voucher_date, " & _
                  "dbo.vwGlAcLedgerForPeriod.paid_to, dbo.vwGlAcLedgerForPeriod.cheque_no, dbo.vwGlAcLedgerForPeriod.cheque_date, " & _
                  "dbo.vwGlAcLedgerForPeriod.cheque_paid, dbo.vwGlAcLedgerForPeriod.cheque_paid_date, dbo.vwGlAcLedgerForPeriod.post, " & _
                  "dbo.vwGlAcLedgerForPeriod.comments, IsNull(dbo.vwGlAcLedgerForPeriod.debit_amount,0) AS debit_amount, IsNull(dbo.vwGlAcLedgerForPeriod.credit_amount,0) AS credit_amount, " & _
                  "dbo.vwGlAcLedgerForPeriod.cost_center_id, dbo.tblGlDefGLCostCenter.cost_center_title, IsNull(dbo.vwGlAcLedgerOpening.Opening_Debit_Amount,0) AS Opening_Debit_Amount, " & _
                  "IsNull(dbo.vwGlAcLedgerOpening.Opening_Credit_Amount,0) AS Opening_Credit_Amount, IsNull(dbo.vwGlAcLedgerOpening.OpeningBalance,0) AS OpeningBalance, dbo.tblGlDefLocation.location_code, dbo.tblGlDefLocation.location_name,  " & _
                  "dbo.vwGlCOADetail.main_code, dbo.vwGlCOADetail.main_title, dbo.vwGlCOADetail.sub_code, dbo.vwGlCOADetail.sub_title,  " & _
                  "dbo.vwGlCOADetail.sub_sub_code , dbo.vwGlCOADetail.sub_sub_title  " & _
                  "FROM dbo.vwGlAcLedgerForPeriod INNER JOIN " & _
                  "dbo.tblGlDefLocation ON dbo.vwGlAcLedgerForPeriod.location_id = dbo.tblGlDefLocation.location_id LEFT OUTER JOIN " & _
                  "dbo.tblGlDefGLCostCenter ON dbo.vwGlAcLedgerForPeriod.cost_center_id = dbo.tblGlDefGLCostCenter.cost_center_id RIGHT OUTER JOIN " & _
                  "dbo.vwGlCOADetail ON dbo.vwGlAcLedgerForPeriod.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id LEFT OUTER JOIN " & _
                  "dbo.vwGlAcLedgerOpening ON dbo.vwGlCOADetail.coa_detail_id = dbo.vwGlAcLedgerOpening.coa_detail_id  " & _
                  " where 1 = 1 and "

            'CR 221     [Asif Kamal]        Add Cost Center filter on Account Ledger Report
            If Me.cmbCostCenter.SelectedIndex > 0 Then
                strSQL = strSQL & " dbo.vwGlAcLedgerForPeriod.cost_center_id = " & Me.cmbCostCenter.SelectedValue & " and "
            End If
            
            ' Applying Filters .. 
            If Me.txtAccount.txtACCode.Text.Trim <> String.Empty Then

                If Me.dtFromDate.Value > Me.dtToDate.Value Then
                    ShowValidationMessage("Invalid Date" & vbCrLf & "Start date cant be smaller then from date")
                    Me.dtFromDate.Focus()
                    Return False

                End If

                strSQL = strSQL + "  detail_code = '" & txtAccount.txtACCode.Text & "' "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                GoForSelectionCriteria = True



            Else
                If Me.dtFromDate.Value > Me.dtToDate.Value Then

                    ShowValidationMessage("Invalid Date" & vbCrLf & "Start date cant be smaller then from date")
                    Me.dtFromDate.Focus()
                    Return False

                End If

                If rdbCustomerTax.Checked = True Then
                    strSQL = strSQL + "  main_code = '" & txtMainAccount.txtACCode.Text & "' "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                    GoForSelectionCriteria = True

                ElseIf rdbVendorTax.Checked = True Then
                    strSQL = strSQL + "  sub_code = '" & txtMainAccount.txtACCode.Text & "' "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                    GoForSelectionCriteria = True

                ElseIf RadioButton1.Checked = True Then
                    strSQL = strSQL + "  sub_sub_code = '" & txtMainAccount.txtACCode.Text & "' "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                    GoForSelectionCriteria = True

                End If


            End If
            Return True


        Catch ex As Exception
            Throw ex
        End Try

    End Function
    ' CR # 355
    ' For Landscape report
    Public Sub FunAddReportPramatersLandScape()
        Try
            objHashTableParamter = New Hashtable

            ' Giving Report Name .. 
            objHashTableParamter.Add("ReportPath", "\rptLedgerDetail_LandScape.rpt")

            Dim ObjCompanyData As DataTable
            ObjCompanyData = UtilityDAL.setCompanyInfo(cmbCompany.SelectedValue)

            'CR#84
            ' '' Passing Parameters .. (Report Parameters .. )
            ''objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
            ''objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))

            If cmbCompany.SelectedIndex > 0 Then
                ObjCompanyData = UtilityDAL.setCompanyInfo(cmbCompany.SelectedValue)

                ' Passing Parameters .. (Report Parameters .. )
                objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
                objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))
            Else
                ' Passing Parameters .. (Report Parameters .. )
                objHashTableParamter.Add("companyname", DAL.SystemConfigurationDAL.GetSystemConfigurationValue("CompanyName").ToString)
                objHashTableParamter.Add("address", DAL.SystemConfigurationDAL.GetSystemConfigurationValue("CompanyAddress").ToString)

            End If

            objHashTableParamter.Add("Location", IIf(Me.cmbCompany.SelectedIndex = 0, "All", Me.cmbCompany.Text))
            objHashTableParamter.Add("CostCenter", IIf(cmbCostCenter.SelectedIndex = 0, "", cmbCostCenter.Text)) 'CR 221 [Asif Kamal]

            objHashTableParamter.Add("PrintRights", "True")
            objHashTableParamter.Add("ExportRights", "True")

            Utility.Utility.gObjMyAppHashTable.Remove(Utility.Utility.EnumHashTableKeyConstants.SetReportParametersList)
            gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)

        Catch ex As Exception
            Throw ex

        End Try

    End Sub
    Public Sub FunAddReportPramaters() Implements IReportsInterface.FunAddReportPramaters
        Try
            objHashTableParamter = New Hashtable

            ' Giving Report Name .. 
            objHashTableParamter.Add("ReportPath", "\rptLedgerDetail.rpt")

            Dim ObjCompanyData As DataTable
            ObjCompanyData = UtilityDAL.setCompanyInfo(cmbCompany.SelectedValue)

            'CR#84
            ' '' Passing Parameters .. (Report Parameters .. )
            ''objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
            ''objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))

            If cmbCompany.SelectedIndex > 0 Then
                ObjCompanyData = UtilityDAL.setCompanyInfo(cmbCompany.SelectedValue)

                ' Passing Parameters .. (Report Parameters .. )
                objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
                objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))
            Else
                ' Passing Parameters .. (Report Parameters .. )
                objHashTableParamter.Add("companyname", DAL.SystemConfigurationDAL.GetSystemConfigurationValue("CompanyName").ToString)
                objHashTableParamter.Add("address", DAL.SystemConfigurationDAL.GetSystemConfigurationValue("CompanyAddress").ToString)

            End If

            objHashTableParamter.Add("Location", IIf(Me.cmbCompany.SelectedIndex = 0, "All", Me.cmbCompany.Text))
            objHashTableParamter.Add("CostCenter", IIf(cmbCostCenter.SelectedIndex = 0, "ALL", cmbCostCenter.Text)) 'CR 221 [Asif Kamal]

            objHashTableParamter.Add("PrintRights", "True")
            objHashTableParamter.Add("ExportRights", "True")

            Utility.Utility.gObjMyAppHashTable.Remove(Utility.Utility.EnumHashTableKeyConstants.SetReportParametersList)
            gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)

        Catch ex As Exception
            Throw ex

        End Try

    End Sub

#End Region

    Private Sub btnGenerateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click, btnPrintLandScape.Click

        Dim con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlClient.SqlTransaction = con.BeginTransaction
        Try
            Dim btn As Windows.Forms.Button = CType(sender, Windows.Forms.Button)   ' CR # 355

            If Me.txtAccount.txtACCode.Text.Trim = "" Then
                If Me.txtMainAccount.txtACCode.Text.Trim = "" Then
                    ShowValidationMessage("Please select an account")
                    Me.txtAccount.txtACCode.Focus()
                    Exit Sub
                End If
            End If

            If dtFromDate.Value.Date > dtToDate.Value.Date Then
                ShowValidationMessage("FromDate should be less than ToDate")
                dtFromDate.Focus()
                Exit Sub
            End If


            If Me.chkVoucherDetail.Checked Then
                ' CR # 355
                'Me.FunAddReportPramaters()

                If btn.Name = "btnPrintLandScape" Then
                    Me.FunAddReportPramatersLandScape()
                Else
                    Me.FunAddReportPramaters()
                End If

                If GoForSelectionCriteria1(trans) Then

                    'Delete from Table
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, "Delete from TblrptLedgerDetail", Nothing)

                    'Insert into table
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, "Insert Into TblrptLedgerDetail" _
                     & " (voucher_code, voucher_type, debit_amount, credit_amount, voucher_month, year_code, voucher_date," _
                     & " VDescription, detail_title, detail_code, coa_detail_id, Status, location_code, location_name) Select " _
                     & " voucher_code, voucher_type, debit_amount, credit_amount, voucher_month, year_code," _
                     & " voucher_date, VDescription, detail_title, detail_code, coa_detail_id, Status, location_code, location_name from vwDailyActivityLedger", Nothing)

                    trans.Commit()

                    ' Create A Object Of Report Viewer .. And Calls His Show Method, To Show The Report .. 
                    ' ------------------------------------------------------------------------------------
                    Dim rptViewer As New rptViewer
                    rptViewer.Text = "Ledger Detail Report"
                    rptViewer.Show()
                    ' ------------------------------------------------------------------------------------
                End If

            Else

                objHashTableParamter = New Hashtable

                ' Giving Report Name .. 
                ' CR # 355
                'objHashTableParamter.Add("ReportPath", "\rptAccountLedger.rpt")
                If btn.Name = "btnPrintLandScape" Then
                    objHashTableParamter.Add("ReportPath", "\rptAccountLedger_LandScape.rpt")
                Else
                    objHashTableParamter.Add("ReportPath", "\rptAccountLedger.rpt")
                End If
                ' CR # 355 End

                If cmbCompany.SelectedIndex > 0 Then
                    Dim ObjCompanyData As DataTable
                    ObjCompanyData = UtilityDAL.setCompanyInfo(cmbCompany.SelectedValue)

                    ' Passing Parameters .. (Report Parameters .. )
                    objHashTableParamter.Add("companyname", ObjCompanyData.Rows(0).Item("CompanyName"))
                    objHashTableParamter.Add("address", ObjCompanyData.Rows(0).Item("CompanyAddress"))
                Else
                    ' Passing Parameters .. (Report Parameters .. )
                    objHashTableParamter.Add("companyname", DAL.SystemConfigurationDAL.GetSystemConfigurationValue("CompanyName").ToString)
                    objHashTableParamter.Add("address", DAL.SystemConfigurationDAL.GetSystemConfigurationValue("CompanyAddress").ToString)

                End If

                ' Passing Parameters .. (Report Parameters .. )

                'objHashTableParamter.Add("StartDate", Format(Me.dtFromDate.Value, "dd-MMM-yyyy"))
                'objHashTableParamter.Add("ToDate", Format(Me.dtToDate.Value, "dd-MMM-yyyy"))

                objHashTableParamter.Add("Location", IIf(Me.cmbCompany.SelectedIndex = 0, "All", Me.cmbCompany.Text))
                ' CR # 355
                'objHashTableParamter.Add("CostCenter", IIf(cmbCostCenter.SelectedIndex = 0, "ALL", cmbCostCenter.Text)) 'CR 221 [Asif Kamal]
                If btn.Name = "btnPrintLandScape" Then
                    objHashTableParamter.Add("CostCenter", IIf(cmbCostCenter.SelectedIndex = 0, "", cmbCostCenter.Text))
                Else
                    objHashTableParamter.Add("CostCenter", IIf(cmbCostCenter.SelectedIndex = 0, "ALL", cmbCostCenter.Text))
                End If
                ' CR @ 355 End
                ' objHashTableParamter.Add("strReportTitle", IIf(Me.rdbCustomerTax.Checked, Me.rdbCustomerTax.Text, Me.rdbVendorTax.Text))

                ' Adding Parameter Of Print And Export Button .. 
                ' =======================================================
                ''If mobjControlList.Item("btnPrint") Is Nothing Then
                ''    objHashTableParamter.Add("PrintRights", "False")
                ''Else
                objHashTableParamter.Add("PrintRights", "True")
                ' ''End If


                ' ''If mobjControlList.Item("btnExport") Is Nothing Then
                ' ''    objHashTableParamter.Add("ExportRights", "False")
                ' ''Else
                objHashTableParamter.Add("ExportRights", "True")
                ' ''End If
                ' =======================================================
                ' =======================================================

                objHashTableParamter.Add("description", "From " & Format(Me.dtFromDate.Value, "dd-MMM-yyyy") & " To  " & Format(Me.dtToDate.Value, "dd-MMM-yyyy"))


                Utility.Utility.gObjMyAppHashTable.Remove(Utility.Utility.EnumHashTableKeyConstants.SetReportParametersList)
                gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)

                ' Me.FunAddReportPramaters()

                If GoForSelectionCriteria(trans) Then

                    objHashTableParamter.Add("StartDate", Format(Me.dtFromDate.Value, "dd-MMM-yyyy"))
                    objHashTableParamter.Add("ToDate", Format(Me.dtToDate.Value, "dd-MMM-yyyy"))

                    If Me.txtAccount.txtACCode.Text.Trim <> String.Empty Then
                        objHashTableParamter.Add("DisplayBarChart", Me.chkShowBarChart.Checked)
                        objHashTableParamter.Add("DisplayLineChart", Me.chkShowlineChart.Checked)
                        objHashTableParamter.Add("InvertBalance", Me.chkInvertDatainLineChart.Checked)

                    Else
                        objHashTableParamter.Add("DisplayBarChart", False)
                        objHashTableParamter.Add("DisplayLineChart", False)
                        objHashTableParamter.Add("InvertBalance", False)

                    End If



                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, "Delete from TblrptAccountLedger", Nothing)
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, "Insert Into TblrptAccountLedger (detail_code, detail_title, voucher_code, voucher_type_id, location_id, voucher_month, finiancial_year_id, voucher_no, voucher_date, paid_to ,cheque_no,  cheque_date, cheque_paid, cheque_paid_date, post, comments, debit_amount, credit_amount, cost_center_id, cost_center_title, Opening_Debit_Amount, Opening_Credit_Amount, OpeningBalance, location_code, location_name, main_code, main_title, sub_code, sub_title, sub_sub_code, sub_sub_title) " & _
                                                "Select detail_code, detail_title, voucher_code, voucher_type_id, location_id, voucher_month, finiancial_year_id, voucher_no, voucher_date, paid_to ,cheque_no,  cheque_date, cheque_paid, cheque_paid_date, post, comments, debit_amount, credit_amount, cost_center_id, cost_center_title, Opening_Debit_Amount, Opening_Credit_Amount, OpeningBalance, location_code, location_name, main_code, main_title, sub_code, sub_title, sub_sub_code, sub_sub_title from vwGLAcLedger ", Nothing)

                    If SQLHelper.ExecuteScaler(trans, CommandType.Text, "select isnull(count(distinct voucher_Date),0)DateCount from tblRptAccountLedger", Nothing) > 40 Then
                        objHashTableParamter.Add("ShowEachReocrd", "True")

                    Else
                        objHashTableParamter.Add("ShowEachReocrd", "False")

                    End If

                    trans.Commit()

                    Dim rptViewer As New rptViewer
                    rptViewer.Text = "Account Ledger"
                    rptViewer.Show()

                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            trans.Rollback()
        Finally
            con.Close()

        End Try

    End Sub

    Private Sub cmbFinancialYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFinancialYear.SelectedIndexChanged


        If cmbFinancialYear.SelectedIndex > 0 Then
            Dim dtRow As DataRowView = CType(cmbFinancialYear.SelectedItem, DataRowView)

            dtFromDate.MinDate = dtRow("Start Date")
            dtToDate.MaxDate = dtRow("End Date")

            dtFromDate.Value = dtRow("Start Date")
            dtToDate.Value = dtRow("End Date")

        Else

            dtFromDate.MinDate = CDate("01/07/1980")
            dtToDate.MaxDate = CDate("01/01/3000")

            Me.dtFromDate.Value = gobjBusinessStartDate
            Me.dtToDate.Value = Now

        End If

    End Sub

    Private Sub rdbCustomerTax_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCustomerTax.CheckedChanged

        If rdbCustomerTax.Checked = True Then
            Me.txtMainAccount.GLAccountHeadType = 1
            Me.txtAccount.GLFilterCondition = ""

            If txtMainAccount.txtACCode.Text <> "" Then
                Me.txtAccount.GLFilterAccount = 1

            End If

        End If

    End Sub

    Private Sub rdbVendorTax_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbVendorTax.CheckedChanged

        If rdbVendorTax.Checked = True Then
            Me.txtMainAccount.GLAccountHeadType = 2
            Me.txtAccount.GLFilterCondition = ""

            If txtMainAccount.txtACCode.Text <> "" Then
                Me.txtAccount.GLFilterAccount = 2
            End If

        End If

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged

        If RadioButton1.Checked = True Then
            Me.txtMainAccount.GLAccountHeadType = 3
            Me.txtAccount.GLFilterCondition = ""

            If txtMainAccount.txtACCode.Text <> "" Then
                Me.txtAccount.GLFilterAccount = 3

            End If

        End If

    End Sub

    Private Sub txtMainAccount_GetGLAccount(ByVal sender As GLNet.uiCtrlGLAccount) Handles txtMainAccount.GetGLAccount


        If txtMainAccount.txtACCode.Text <> "" Then
            Me.txtAccount.GLFilterCondition = sender.GLAccountID
            Me.txtAccount.GLAccountHeadType = 4

            If RadioButton1.Checked = True Then Me.txtAccount.GLFilterAccount = 3
            If rdbVendorTax.Checked = True Then Me.txtAccount.GLFilterAccount = 2
            If rdbCustomerTax.Checked = True Then Me.txtAccount.GLFilterAccount = 1

        Else
            Me.txtAccount.Text = ""
            Me.txtAccount.txtAccountName.Text = ""

            Me.txtAccount.GLFilterCondition = ""
            Me.txtAccount.GLAccountHeadType = 4

            If RadioButton1.Checked = True Then Me.txtAccount.GLFilterAccount = 3
            If rdbVendorTax.Checked = True Then Me.txtAccount.GLFilterAccount = 2
            If rdbCustomerTax.Checked = True Then Me.txtAccount.GLFilterAccount = 1


        End If

        'CR# 49
        Me.txtAccount.Text = ""
        Me.txtAccount.txtAccountName.Text = ""

    End Sub

    'Private Sub txtMainAccount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMainAccount.LostFocus

    '    If txtMainAccount.txtACCode.Text <> "" Then
    '        Me.txtAccount.GLFilterCondition = sender.GLAccountID
    '        Me.txtAccount.GLAccountHeadType = 4

    '        If RadioButton1.Checked = True Then Me.txtAccount.GLFilterAccount = 3
    '        If rdbVendorTax.Checked = True Then Me.txtAccount.GLFilterAccount = 2
    '        If rdbCustomerTax.Checked = True Then Me.txtAccount.GLFilterAccount = 1

    '    Else
    '        Me.txtAccount.GLFilterCondition = ""
    '        Me.txtAccount.GLAccountHeadType = 4

    '        If RadioButton1.Checked = True Then Me.txtAccount.GLFilterAccount = 3
    '        If rdbVendorTax.Checked = True Then Me.txtAccount.GLFilterAccount = 2
    '        If rdbCustomerTax.Checked = True Then Me.txtAccount.GLFilterAccount = 1


    '    End If
    'End Sub

    Function GoForSelectionCriteria1(ByVal trans As SqlClient.SqlTransaction) As Boolean
        Try

            If Me.txtAccount.txtACCode.Text.Trim = String.Empty Then
                ShowValidationMessage("Please Select then Account Number from Help")
                Me.txtAccount.txtACCode.Focus()
                Return True

            ElseIf Me.dtFromDate.Value > Me.dtToDate.Value Then
                ShowValidationMessage("Invalid Date" & vbCrLf & "End date cannot be smaller than Start date")
                Me.dtToDate.Focus()

            End If


            Dim strYearCriteria As String
            Dim strLocationCriteria As String
            Dim strCostCenterCriteria As String

            '=========================================================================================
            '-- Selection Criteia Building in case of Financial Year selection
            '=========================================================================================
            If Me.cmbFinancialYear.SelectedIndex > 0 Then
                strYearCriteria = " (dbo.tblGlVoucher.finiancial_year_id = " & Me.cmbFinancialYear.SelectedValue & " ) AND "
            Else
                strYearCriteria = String.Empty

            End If

            '=========================================================================================
            '-- Selection Criteia Building in case of Location selection
            '=========================================================================================
            If Me.cmbCompany.SelectedIndex > 0 Then
                strLocationCriteria = " (dbo.tblGlVoucher.location_id = " & Me.cmbCompany.SelectedValue & ") AND "
            Else
                strLocationCriteria = String.Empty

            End If
            ''**************************************************************************************************
            'CR 221     [Asif Kamal]
            If Me.cmbCostCenter.SelectedIndex > 0 Then
                strCostCenterCriteria = "(dbo.tblGlVoucherDetail.cost_center_id = " & Me.cmbCostCenter.SelectedValue & ") AND "
            Else
                strCostCenterCriteria = String.Empty
            End If


            objHashTableParamter.Add("fromdate", Format(Me.dtFromDate.Value, "dd/MMM/yyyy"))
            objHashTableParamter.Add("todate", Format(Me.dtToDate.Value, "dd/MMM/yyyy"))
            objHashTableParamter.Add("AccountTitle", Me.txtAccount.txtAccountName.Text)


            strSQL = " alter view vwDailyActivityLedger as " _
            & " SELECT     TOP 100 PERCENT tblGlVoucher.voucher_code, tblGlDefVoucherType.voucher_type, tblGlVoucherDetail.debit_amount," _
            & " tblGlVoucherDetail.credit_amount, tblGlVoucher.voucher_month, tblGlDefFinancialYear.year_code, tblGlVoucher.voucher_date," _
            & " tblGlVoucherDetail.comments AS VDescription, tblGlCOAMainSubSubDetail.detail_title, tblGlCOAMainSubSubDetail.detail_code," _
            & " tblGlVoucherDetail.coa_detail_id, tblGlVoucher.post AS Status, tblGlVoucher.voucher_id , dbo.tblGlDefLocation.location_code,   dbo.tblGlDefLocation.location_name" _
            & " FROM         tblGlCOAMainSubSubDetail INNER JOIN" _
            & " tblGlVoucher INNER JOIN" _
            & " tblGlVoucherDetail ON tblGlVoucher.voucher_id = tblGlVoucherDetail.voucher_id AND" _
            & " tblGlVoucher.location_id = tblGlVoucherDetail.location_id INNER JOIN" _
            & " tblGlDefFinancialYear ON tblGlVoucher.finiancial_year_id = tblGlDefFinancialYear.financial_year_id INNER JOIN" _
            & " tblGlDefVoucherType ON tblGlVoucher.voucher_type_id = tblGlDefVoucherType.voucher_type_id ON" _
            & " tblGlCOAMainSubSubDetail.coa_detail_id = tblGlVoucherDetail.coa_detail_id INNER JOIN                      dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id "

            strSQL = strSQL + " WHERE   " & strCostCenterCriteria & strYearCriteria & strLocationCriteria & "  (tblGlVoucher.voucher_date BETWEEN '" & Format(Me.dtFromDate.Value, pbDateFormat) & "' AND '" & Format(Me.dtToDate.Value, pbDateFormat) & "') "
            If Me.chkIncludeUnPostedVouchers.Checked = False Then strSQL = strSQL + " AND (tblGlVoucher.post = 1) "
            If Me.chkIncludeOtherVouchers.Checked = False Then strSQL = strSQL + "  AND (dbo.tblGlVoucher.Other_Voucher = 0) "



            strSQL = strSQL + " AND (tblGlVoucher.voucher_id IN " _
            & " (SELECT    tblGlVoucher.voucher_id" _
            & " FROM  tblGlVoucher INNER JOIN" _
            & " tblGlVoucherDetail ON tblGlVoucher.voucher_id = tblGlVoucherDetail.voucher_id AND" _
            & " tblGlVoucher.location_id = tblGlVoucherDetail.location_id"

            strSQL = strSQL + " WHERE " & strCostCenterCriteria & strYearCriteria & strLocationCriteria & "  Voucher_No <> '000000' AND (tblGlVoucherDetail.coa_detail_id =" & Me.txtAccount.GLAccountID & "  ) AND (tblGlVoucher.voucher_date BETWEEN '" & Format(Me.dtFromDate.Value, pbDateFormat) & "' AND '" & Format(Me.dtToDate.Value, pbDateFormat) & "') "

            If Me.chkIncludeUnPostedVouchers.Checked = False Then strSQL = strSQL + " AND (tblGlVoucher.post = 1)"

            '   to add check of other voucher in Account Ledger Detail report also
            If Me.chkIncludeOtherVouchers.Checked = False Then strSQL = strSQL + "  AND (dbo.tblGlVoucher.Other_Voucher = 0) "

            'CR#220
            If Me.cmbSource.SelectedIndex > 0 Then
                strSQL = strSQL + " ) and tblGlVoucher.source = '" & Me.cmbSource.Text & "'"
            Else
                strSQL = strSQL + " ) "
            End If


            strSQL = strSQL + " )" _
            & " ORDER BY tblGlVoucher.voucher_code"

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
            Return True

        Catch ex As Exception
            Throw ex

        End Try

    End Function

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtAccount_GetGLAccount(ByVal sender As uiCtrlGLAccount) Handles txtAccount.GetGLAccount
        Try

            'CR# 49
            Me.txtMainAccount.Text = ""
            Me.txtMainAccount.txtAccountName.Text = ""
            Me.txtAccount.GLFilterCondition = ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    
End Class