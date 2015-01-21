
''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Lumensoft GL
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmBankReconcilation.vb           				                            
''// Programmer	     : R@! Shahid
''// Creation Date	 : 16-Jul-2009
''// Description     : This form will be used to reconcile banks
''//                 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// CR#    Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''//48      18 May,2010      Abdul Jabbar         In Bank Reconcilation report, Paid (Presented/uncredited) cheques should be coloured as in old (VB 6) GL.  
''//44      17 May,2010      Abdul Jabbar         Bank Reconcilation Report Formate of Dr,Cr column's footer is not appropriate
''//160     08 Dec,2011      Abdul Jabbar      (changes in DAL)    Service Broker relevant changes in GL , when SSB will be On identity of both voucher master and detail will be off, now we will use shop id in where clause with shop_id<=0 so pick next voucher id in head office 
''//169     20 Dec,2011      Abdul Jabbar       CR#169:Bank Ledger in Bank Position and Bank Reconcilation doesn't match with Ledger report (change in DAL)
''//304     17-apr-2014      Fatima               Bank Statement report
''//319     16 Jul,2014      Abdul Jabbar       Bank Position, Bank Statement Bank Balance balance is not appropriate
''//326     06-aug-2014      Fatima Tajammal      Bank Statement: No need to show Total of Bank balance column at the end (in footer)
''/////////////////////////////////////////////////////////////////////////////////////////

Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility

Public Class frmBankReconcilation

    Implements IGeneral

#Region "Variables"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection
    ''This is the model object which will be set with data values and refered for Save|Update|Delete|Loading Record in Edit Mode
    Private mobjModel As BankReconcilation
    Private intPkId As Integer

#End Region

#Region "Enumerations"
    ''This is the representation of grid columns, and will be used while referring grid values, 
    ''instead of giving hard coded column's indexes
    Private Enum EnumGridActivity
        voucherId = 0
        Selector = 1
        VoucherNo = 2
        VoucherDate = 3
        VoucherType = 4
        ChequeNo = 5
        ChequeDate = 6
        DrAmount = 7
        CrAmount = 8
        PaidDate = 9
        colChqPaidChq = 10
    End Enum

    Private Enum EnumGridView
        voucherId = 0
        Selector = 1
        VoucherNo = 2
        VoucherDate = 3
        VoucherType = 4
        ChequeNo = 5
        ChequeDate = 6
        DrAmount = 7
        CrAmount = 8
        PaidDate = 9
        colChqPaidChq = 10
    End Enum

    'Cr # 304
    Private Enum EnumBankStatement
        PaidDate
        ChequeNo
        BankDebit
        BankCredit
        BankBlnc
        ChequeDate
        VoucherNo
        VoucherDate

    End Enum

#End Region

#Region "Interface Methods"

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            If Condition = "Activity" Then

                Me.grdActivity.RootTable.Columns(EnumGridActivity.DrAmount).FormatString = "n"
                Me.grdActivity.RootTable.Columns(EnumGridActivity.CrAmount).FormatString = "n"
                Me.grdActivity.RootTable.Columns(EnumGridActivity.VoucherDate).FormatString = "dd/MMM/yyyy"
                Me.grdActivity.RootTable.Columns(EnumGridActivity.ChequeDate).FormatString = "dd/MMM/yyyy"
                Me.grdActivity.RootTable.Columns(EnumGridActivity.PaidDate).FormatString = "dd/MMM/yyyy"

                Me.grdActivity.RootTable.Columns(EnumGridActivity.CrAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdActivity.RootTable.Columns(EnumGridActivity.DrAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                Me.grdActivity.RootTable.Columns(EnumGridActivity.colChqPaidChq).Visible = False

                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdActivity.RootTable.Columns
                    col.AutoSize()
                    If Not col.Index = EnumGridActivity.Selector Then col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                Next

                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdActivity.GetRows

                    If row.Cells(EnumGridActivity.colChqPaidChq).Value.ToString = "True" Then
                        row.BeginEdit()
                        row.IsChecked = True
                        row.EndEdit()
                    End If

                Next

                'Cr # 304
            ElseIf Condition = "Bank Statement" Then

                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.BankDebit).FormatString = "n"
                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.BankCredit).FormatString = "n"
                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.BankBlnc).FormatString = "n"
                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.VoucherDate).FormatString = "dd/MMM/yyyy"
                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.ChequeDate).FormatString = "dd/MMM/yyyy"
                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.PaidDate).FormatString = "dd/MMM/yyyy"

                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.BankDebit).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.BankCredit).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.BankBlnc).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.VoucherDate).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.ChequeDate).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.PaidDate).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.BankBlnc).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.VoucherNo).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.ChequeNo).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.BankDebit).TotalFormatString = "n"
                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.BankCredit).TotalFormatString = "n"
                'Code commented against CR # 326
                'Me.grdBankStatement.RootTable.Columns(EnumBankStatement.BankBlnc).TotalFormatString = "n"

                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.BankCredit).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdBankStatement.RootTable.Columns(EnumBankStatement.BankDebit).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                'Code commented against CR # 326
                'Me.grdBankStatement.RootTable.Columns(EnumBankStatement.BankBlnc).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdBankStatement.RootTable.Columns
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                Next

            Else

                Dim s As New Janus.Windows.GridEX.GridEXFormatStyle
                s.BackColor = Color.LightSalmon

                Me.GrdView.RootTable.Columns(EnumGridView.DrAmount).FormatString = "n"
                Me.GrdView.RootTable.Columns(EnumGridView.CrAmount).FormatString = "n"
                Me.GrdView.RootTable.Columns(EnumGridView.VoucherDate).FormatString = "dd/MMM/yyyy"
                Me.GrdView.RootTable.Columns(EnumGridView.ChequeDate).FormatString = "dd/MMM/yyyy"
                Me.GrdView.RootTable.Columns(EnumGridView.PaidDate).FormatString = "dd/MMM/yyyy"

                Me.GrdView.RootTable.Columns(EnumGridView.CrAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.GrdView.RootTable.Columns(EnumGridView.DrAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                Me.GrdView.RootTable.Columns(EnumGridView.colChqPaidChq).Visible = False

                For Each row As Janus.Windows.GridEX.GridEXRow In Me.GrdView.GetRows

                    If row.Cells(EnumGridView.colChqPaidChq).Value.ToString = "True" Then
                        row.BeginEdit()
                        row.IsChecked = True

                        'CR# 48
                        row.RowStyle = s

                        row.EndEdit()
                    End If

                    For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GrdView.RootTable.Columns
                        col.AutoSize()
                        col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                    Next

                Next
                Me.GrdView.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            End If


            ' ''Columns with In-visible setting
            'Me.grdActivity.RootTable.Columns(EnumGridActivity.CityAreaID).FormatString = ""
            'Me.grdActivity.RootTable.Columns(EnumGridActivity.CityAreaID).Visible = False
            'Me.grdActivity.RootTable.Columns(EnumGridActivity.CityID).FormatString = ""
            'Me.grdActivity.RootTable.Columns(EnumGridActivity.CityID).Visible = False
            'Me.grdActivity.RootTable.Columns(EnumGridActivity.ISReadOnly).Visible = False

            ' ''Set columns widths for visible columns
            'Me.grdActivity.RootTable.Columns(EnumGridActivity.AreaName).Width = 200
            'Me.grdActivity.RootTable.Columns(EnumGridActivity.Comments).Width = 500
            'Me.grdActivity.RootTable.Columns(EnumGridActivity.SortOrder).Width = 70
            'Me.grdActivity.RootTable.Columns(EnumGridActivity.SortOrder).FormatString = ""


            'Me.grdActivity.TotalRowFormatStyle.BackColor = gobjRequiredFieldtBKColor



            ''Getting Language based captions for the selected Grid, after applying filter criteria on Language Based Controls List
            'Dim dv As DataView = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form Name] = '" & EnumProjectForms.frmDefCityAreas.ToString & "' AND [Control Name] like 'grdAllRecords%'")

            'If Not dv Is Nothing Then
            '    If Not dv.Count = 0 Then
            '        ''for each Caption Returned by Filter criteria
            '        For rowIndex As Integer = 0 To dv.Count - 1
            '            ''For each Column of the grid, against the selected caption
            '            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdAllRecords.RootTable.Columns
            '                ''As the coulumn's Name is concatinated with Grid name, therefor, firs we will split it and then will pick column name as caption from the second index of the split
            '                Dim strGridColumName() As String = dv.Item(rowIndex).Item(dv.Table.Columns("Control Name").ColumnName).Split(".")
            '                ''IF filtered column name is matched with the grid column Name then change the caption of the column as per language setting.
            '                If col.Caption = strGridColumName(1) Then
            '                    col.Caption = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName)
            '                    col.HeaderStyle.Font = gobjDefaultFontSettingForLables
            '                    Exit For
            '                End If
            '            Next

            '        Next
            '        grdAllRecords.Font = gobjDefaultFontSettingForInput
            '    End If
            'End If
            'ApplyGridCaptions(Me.Name, "grdAllRecords", grdAllRecords)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try

           
            'If Mode.ToString = EnumDataMode.Disabled.ToString Then



            btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
            btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System           
            btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
            btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
            btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

            SetNavigationButtons(EnumDataMode.Disabled)

            'ElseIf Mode.ToString = EnumDataMode.[New].ToString Then

            '    btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System

            '    If mobjControlList.Item("btnSave") Is Nothing Then
            '        btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System
            '    Else
            '        btnSave.Enabled = True ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
            '    End If

            '    btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
            '    btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
            '    btnCancel.Enabled = True ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

            '    SetNavigationButtons(Mode)


            'ElseIf Mode.ToString = EnumDataMode.Edit.ToString Then

            '    btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
            '    btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System

            '    If mobjControlList.Item("btnUpdate") Is Nothing Then
            '        btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
            '    Else
            '        btnUpdate.Enabled = True ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
            '    End If

            '    If mobjControlList.Item("btnDelete") Is Nothing Then
            '        btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
            '    Else
            '        btnDelete.Enabled = True ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
            '    End If
            '    btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

            '    SetNavigationButtons(Mode)

            '    ' Me.grdAllRecords.Enabled = True

            '    ''Me.grdAllRecords.Focus()

            'ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

            '    btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
            '    btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System


            '    btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
            '    btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
            '    btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

            '    SetNavigationButtons(Mode)

            '    'Me.grdAllRecords.Enabled = True

            '    ''Me.grdAllRecords.Focus()

            'End If

            btnSave.Enabled = True ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

            '' Disabl/Enable the Button that converts Grid data in Excel Sheet According to Login User rights
            If mobjControlList.Item("btnExport") Is Nothing Then
                Me.UiCtrlGridBar1.btnExport.Enabled = False
                Me.UiCtrlGridBar2.btnExport.Enabled = False
                'Cr # 304
                Me.UiCtrlGridBar3.btnExport.Enabled = False
            End If


            '' Disabl/Enable the Button that Prints Grid data According to Login User rights
            If mobjControlList.Item("btnPrint") Is Nothing Then
                Me.UiCtrlGridBar1.btnPrint.Enabled = False
                Me.UiCtrlGridBar2.btnPrint.Enabled = False
                'Cr # 304
                Me.UiCtrlGridBar3.btnPrint.Enabled = False
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete


    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try

            ''Getting Datasource for List from DAL
            Dim dt As DataTable = New BankReconcilationDal().GetBanks()

            ' Dim dr As DataRow = dt.NewRow

            Me.ListBox1.DisplayMember = "DETAIL_TITLE"
            Me.ListBox1.ValueMember = "COA_DETAIL_ID"

            Me.ListBox1.DataSource = dt

            ''cboCities.Items.Insert(0, gstrComboZeroIndexString)


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            ''Create Model object
            mobjModel = New BankReconcilation
            With mobjModel
                ''Setting the Model Object Values
                '.CityAreaID = intPkId
                '.CityID = CInt(cboCities.SelectedValue)
                '.AreaName = Me.txtName.Text.Trim
                '.AreaCode = Me.txtCode.Text.Trim
                '.SortOrder = Me.txtSortOrder.Text.Trim
                '.Comments = Me.txtComments.Text.Trim

                .ActivityLog.LogGroup = "Transactions"
                .ActivityLog.ScreenTitle = "Bank Reconcilation"
                .ActivityLog.LogRef = ""
                .ActivityLog.UserID = gObjUserInfo.UserID
                .ActivityLog.ScreenTitle = Me.Text


            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.Cursor = Cursors.WaitCursor

            If Condition = "Activity" Then

                ''Getting Datasource for Grid from DAL
                Dim dt As DataTable = New BankReconcilationDal().GetVoucherDetail(Me.ListBox1.SelectedValue, Me.dtpActivityFromDate.Value.Date, Me.dtpActivityToDate.Value.Date, "", Me.chkUnPresented.Checked, Me.chkPresented.Checked, chkUnCredited.Checked, chkCredited.Checked)

                Me.grdActivity.DataSource = dt

                ''Applying Grid Formatting Setting
                Me.ApplyGridSettings(Condition)
            Else

                Me.txtBankBalance.Text = 0
                Me.txtLedgerBalance.Text = 0
                Me.txtUnCredited.Text = 0
                Me.txtUnPresented.Text = 0

                If Not Me.TabControl1.SelectedIndex = 1 Then Exit Sub
                Dim m_voucher_type As String

                If optViewAll.Checked = True Then
                    m_voucher_type = ""
                ElseIf optViewUnPresented.Checked = True Then

                    m_voucher_type = "Credit_Amount"
                ElseIf optViewUnCredited.Checked = True Then

                    m_voucher_type = "Debit_Amount"
                End If

                ''Getting Datasource for Grid from DAL
                Dim dt As DataTable = New BankReconcilationDal().GetVoucherDetail_View(Me.ListBox1.SelectedValue, Me.dtpViewStartDate.Value.Date, Me.dtpViewToDate.Value.Date, m_voucher_type)

                Me.GrdView.DataSource = dt

                ''Applying Grid Formatting Setting
                Me.ApplyGridSettings(Condition)

                Me.txtLedgerBalance.Text = New BankReconcilationDal().GetLedgerBalance(Me.ListBox1.SelectedValue, Me.dtpViewStartDate.Value.Date, Me.dtpViewToDate.Value.Date)
                Me.txtUnPresented.Text = New BankReconcilationDal().GetUnPresented(Me.ListBox1.SelectedValue, Me.dtpViewStartDate.Value.Date, Me.dtpViewToDate.Value.Date)
                Me.txtUnCredited.Text = New BankReconcilationDal().GetUnCredited(Me.ListBox1.SelectedValue, Me.dtpViewStartDate.Value.Date, Me.dtpViewToDate.Value.Date)
                Me.txtBankBalance.Text = Val(txtLedgerBalance.Text) + Val(txtUnPresented.Text) - Val(txtUnCredited.Text)

                Me.txtLedgerBalance.Text = Microsoft.VisualBasic.FormatNumber(Me.txtLedgerBalance.Text, 2, , , TriState.True)
                Me.txtUnPresented.Text = Microsoft.VisualBasic.FormatNumber(Me.txtUnPresented.Text, 2, , , TriState.True)
                Me.txtUnCredited.Text = Microsoft.VisualBasic.FormatNumber(txtUnCredited.Text, 2, , , TriState.True)

                If Val(Me.txtBankBalance.Text) < 0 Then Me.txtBankBalance.Text = "(" & Microsoft.VisualBasic.FormatNumber(Math.Abs(Val(Me.txtBankBalance.Text)), 2, , , TriState.True) & ")" Else Me.txtBankBalance.Text = Microsoft.VisualBasic.FormatNumber(txtBankBalance.Text, 2, , , TriState.True)

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        'Try

        '    ''1 First Check Front End Validations
        '    If Mode = EnumDataMode.[New] Or Mode = EnumDataMode.Edit Then
        '        ''Check Name is Required
        '        If Me.txtName.Text.Trim = String.Empty Then
        '            ShowValidationMessage(gstrMsgNameRequired)
        '            Me.txtName.Focus()
        '            Return False

        '            ''Check Code is Required
        '        ElseIf Me.txtCode.Text.Trim = String.Empty Then
        '            ShowValidationMessage(gstrMsgCodeRequired)
        '            Me.txtCode.Focus()
        '            Return False

        '            ''if SortOrder is not Given  
        '        ElseIf Me.txtSortOrder.Text.Trim = String.Empty Then
        '            Me.txtSortOrder.Text = 0
        '            ''if SortOrder is Given  but Not Numeric
        '        ElseIf Me.txtSortOrder.Text.Trim <> String.Empty And Not IsNumeric(Me.txtSortOrder.Text.Trim) Then
        '            ShowValidationMessage(gstrMsgWrongInput)
        '            Me.txtSortOrder.Focus()
        '            Return False
        '        End If
        '    End If
        '    ''===========================================   
        '    ''2 Database End Validations

        '    ''Fill Model with the front end values
        '    Me.FillModel()

        '    If Condition = "BackEndDeleteValidation" Then
        '        ''Check Dependancy existance
        '        Return New BankReconcilationDal().IsValidateForDelete(mobjModel)

        '    End If

        '    ''Check Name or Code Duplication
        '    Call New BankReconcilationDal().IsValidateForSave(mobjModel)



        '    Return True

        'Catch ex As Exception
        '    Throw ex
        'End Try


        Return True

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try

            Me.dtpActivityFromDate.Value = gobjBusinessStartDate
            Me.dtpViewStartDate.Value = gobjBusinessStartDate
            Me.optViewUnPresented.Checked = True
            ''Set New Mode and Applying Security Setting
            'Call ApplySecurity(EnumDataMode.[New])

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            Dim objDtable As New DataTable
            ''Applying Front End Validation Checks
            If Me.IsValidate(EnumDataMode.[New]) Then

                Dim result As DialogResult = Windows.Forms.DialogResult.Yes

                If gblnShowSaveConfirmationMessages Then
                    ''Getting Save Confirmation from User
                    result = ShowConfirmationMessage(gstrMsgSave, MessageBoxDefaultButton.Button1)
                End If

                If result = Windows.Forms.DialogResult.Yes Then

                    For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdActivity.GetRows
                        row.BeginEdit()
                        If row.IsChecked = True Then

                            row.Cells(EnumGridActivity.colChqPaidChq).Value = "True"
                        Else
                            row.Cells(EnumGridActivity.colChqPaidChq).Value = "False"
                        End If
                        row.EndEdit()
                    Next

                    ''Create a DAL Object and calls its Add Method by passing Model Object

                    objDtable = Me.grdActivity.DataSource

                    Me.FillModel("")

                    If New BankReconcilationDal().Add(objDtable, Me.dtpChequePaidDate.Value.Date, mobjModel) = True Then

                        ''Query to Database and get fressh modifications in the Grid
                        'Me.GetAllRecords()

                        ''Reset controls and set New Mode
                        ' Me.ReSetControls()
                        ''Getting Save Confirmation from User
                        ShowInformationMessage("Saved Successfully")
                    End If

                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages
        Try

            If gEnumIsRightToLeft = Windows.Forms.RightToLeft.No Then
                Me.btnFirst.ImageList = gobjMyImageListForOperationBar
                Me.btnFirst.ImageKey = "First"

                Me.btnNext.ImageList = gobjMyImageListForOperationBar
                Me.btnNext.ImageKey = "Next"

                Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
                Me.btnPrevious.ImageKey = "Previous"

                Me.btnLast.ImageList = gobjMyImageListForOperationBar
                Me.btnLast.ImageKey = "Last"


            Else
                Me.btnFirst.ImageList = gobjMyImageListForOperationBar
                Me.btnFirst.ImageKey = "Last"

                Me.btnNext.ImageList = gobjMyImageListForOperationBar
                Me.btnNext.ImageKey = "Previous"

                Me.btnPrevious.ImageList = gobjMyImageListForOperationBar
                Me.btnPrevious.ImageKey = "Next"

                Me.btnLast.ImageList = gobjMyImageListForOperationBar
                Me.btnLast.ImageKey = "First"
            End If

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

            ElseIf Mode = EnumDataMode.Disabled Then
                ''if New Mode then Set Enable all Navigation Buttons
                btnFirst.Enabled = False  ': btnFirst.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnPrevious.Enabled = False ': btnPrevious.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnNext.Enabled = False ': btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnLast.Enabled = False '
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Update(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try

            ' ''Applying Front End Validation Checks
            'If Me.IsValidate(EnumDataMode.Edit) Then

            '    Dim result As DialogResult = Windows.Forms.DialogResult.Yes
            '    ''Getting Save Confirmation from User
            '    result = ShowConfirmationMessage(gstrMsgUpdate, MessageBoxDefaultButton.Button1)

            '    If result = Windows.Forms.DialogResult.Yes Then

            '        ''Create a DAL Object and calls its Update Method by passing Model Object
            '        If New BankReconcilationDal().Update(Me.mobjModel) Then

            '            ''Query to Database and get fressh modifications in the Grid
            '            Me.GetAllRecords()

            '            'to select the last updated record
            '            For Rind As Int16 = 0 To (grdAllRecords.RowCount - 1)
            '                If Me.grdAllRecords.GetRow(Rind).Cells(EnumGridArea.CityAreaID).Value = mobjModel.CityBankReconcilationID Then
            '                    Me.grdAllRecords.Row = Rind
            '                    Exit For
            '                End If
            '            Next

            '            Me.grdAllRecords_SelectionChanged(Nothing, Nothing)

            '            If gblnShowAfterUpdateMessages Then
            '                ''Getting Save Confirmation from User
            '                ShowInformationMessage(gstrMsgAfterUpdate)
            '            End If


            '        End If
            '    End If
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Local Functions and Procedures"


#End Region

#Region "Form Controls Events"

    Private Sub frmDefArea_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        AddHandler grdActivity.SelectionChanged, AddressOf Me.grdAllRecords_SelectionChanged
    End Sub

    ''This event will prevent the user to change the system language.
    Private Sub frmDefArea_InputLanguageChanging(ByVal sender As Object, ByVal e As System.Windows.Forms.InputLanguageChangingEventArgs) Handles Me.InputLanguageChanging
        e.Cancel = True
    End Sub

    Private Sub frmDefArea_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        ' 'Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frmDefArea_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            ''Getting all available controls list to the user on this screen; in a collection 
            mobjControlList = GetFormSecurityControls(Me.Name)

            ''Assing Images to Buttons
            Me.SetButtonImages()

            Call FillCombos()

            Me.ApplySecurity(EnumDataMode.[New])

            Me.dtpViewStartDate.Value = gobjBusinessStartDate

            'Cr # 304
            Me.dtpFrmForBnkst.Value = gobjBusinessStartDate
            ''To avoid implecit call of rowcol chang event , We are assinging event handler at runtime.
            'AddHandler grdAllRecords.SelectionChanged, AddressOf Me.grdAllRecords_SelectionChanged

            ''Reset the controls for new mode
            Me.TabControl1.SelectedIndex = 1
            Call ReSetControls()
            If Me.ListBox1.Items.Count > 0 Then Me.ListBox1.SelectedIndex = 0 : Me.ListBox1_SelectedIndexChanged(Me, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmDefArea_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Control And e.KeyCode = Keys.S Then
                If Me.btnSave.Enabled = True Then Me.Save()
            ElseIf e.Control And e.KeyCode = Keys.U Then
                If Me.btnUpdate.Enabled = True Then Me.Update()
            ElseIf e.Control And e.KeyCode = Keys.D Then
                If Me.btnDelete.Enabled = True Then Me.Delete()
            ElseIf e.Control And e.KeyCode = Keys.N Then
                If Me.btnNew.Enabled = True Then Me.ReSetControls()
            ElseIf e.Control And e.KeyCode = Keys.E Then
                If Me.btnCancel.Enabled = True Then Me.grdAllRecords_SelectionChanged(Nothing, Nothing)
            ElseIf e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click, btnNew.Click, btnUpdate.Click, btnDelete.Click, btnCancel.Click, btnExit.Click

        Try
            'Dim btn As C1.Win.C1Input.C1Button = CType(sender, C1.Win.C1Input.C1Button)
            Dim btn As Windows.Forms.Button = CType(sender, Windows.Forms.Button)

            ''If New Button is Clicked
            If btn.Name = btnNew.Name Then

                ''Refresh the controls for new mode
                Me.ReSetControls()

            ElseIf btn.Name = btnSave.Name Then
                '' Call Save method to save the record
                Me.Save()
            ElseIf btn.Name = btnUpdate.Name Then
                '' Call Update method to update the record
                Me.Update()
            ElseIf btn.Name = btnDelete.Name Then
                '' Call Delete method to delete the record
                Me.Delete()
            ElseIf btn.Name = btnCancel.Name Then
                ''Load Selected record in Edit Mode
                Me.grdAllRecords_SelectionChanged(Nothing, Nothing)


            ElseIf btn.Name = btnExit.Name Then
                Me.Close()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            mobjModel = Nothing
        End Try

    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click, btnPrevious.Click, btnNext.Click, btnLast.Click

        Try

            Dim btn As Button = CType(sender, Button)

            ''If Move First is clicked, then navigate to first record of the grid
            If btn.Name = Me.btnFirst.Name Then
                Me.grdActivity.Row = 0

                ''If Move Previous is clicked, then navigate to Previous record of the grid
            ElseIf btn.Name = Me.btnPrevious.Name Then
                If Me.grdActivity.Row > 0 Then Me.grdActivity.Row = (Me.grdActivity.Row - 1)

                ''If Move Next is clicked, then navigate to next record of the grid
            ElseIf btn.Name = Me.btnNext.Name Then
                If Me.grdActivity.Row >= 0 Then Me.grdActivity.Row = (Me.grdActivity.Row + 1)


                ''If Move Last is clicked, then navigate to Last record of the grid
            ElseIf btn.Name = Me.btnLast.Name Then
                Me.grdActivity.Row = (Me.grdActivity.RowCount - 1)

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GrdView_LoadingRow(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles GrdView.LoadingRow
        Try

            'CR# 48
            If e.Row.RowType = Janus.Windows.GridEX.RowType.Record And e.Row.Cells(EnumGridView.Selector).Value = True Then
                Dim s As New Janus.Windows.GridEX.GridEXFormatStyle
                s.BackColor = Color.LightSalmon
                e.Row.RowStyle = s
            End If
            '    If e.Row.RowType = Janus.Windows.GridEX.RowType.TotalRow Then

            '        ' ''to view the Total Records in Grid Footer
            '        'Dim dv As DataView = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Control Type]= 'DataDictionary'  AND [Control Name] = 'GridRowCount'")
            '        'Dim strTotalRecords As String = String.Empty
            '        'If Not dv Is Nothing Then
            '        '    If Not dv.Count = 0 Then
            '        '        strTotalRecords = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName)
            '        '    End If
            '        'End If

            '        '  e.Row.Cells(EnumGridActivity.VoucherNo).Text = strTotalRecords & " (" & Me.grdActivity.GetTotal(Me.grdActivity.RootTable.Columns(EnumGridActivity.VoucherNo), Janus.Windows.GridEX.AggregateFunction.Count) & ") "
            '    End If

            If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Or e.Row.RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                If e.Row.Cells(EnumGridView.CrAmount).Text = "" Then e.Row.Cells(EnumGridView.CrAmount).Text = "0.00" Else e.Row.Cells(EnumGridView.CrAmount).Text = Microsoft.VisualBasic.FormatNumber(e.Row.Cells(EnumGridView.CrAmount).Text, 2, , , TriState.True)
                If e.Row.Cells(EnumGridView.DrAmount).Text = "" Then e.Row.Cells(EnumGridView.DrAmount).Text = "0.00" Else e.Row.Cells(EnumGridView.DrAmount).Text = Microsoft.VisualBasic.FormatNumber(e.Row.Cells(EnumGridView.DrAmount).Text, 2, , , TriState.True)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdAllRecords_LoadingRow(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdActivity.LoadingRow
        ' Try

        'If e.Row.RowType = Janus.Windows.GridEX.RowType.TotalRow Then

        '    ' ''to view the Total Records in Grid Footer
        '    'Dim dv As DataView = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Control Type]= 'DataDictionary'  AND [Control Name] = 'GridRowCount'")
        '    'Dim strTotalRecords As String = String.Empty
        '    'If Not dv Is Nothing Then
        '    '    If Not dv.Count = 0 Then
        '    '        strTotalRecords = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName)
        '    '    End If
        '    'End If

        '    '  e.Row.Cells(EnumGridActivity.VoucherNo).Text = strTotalRecords & " (" & Me.grdActivity.GetTotal(Me.grdActivity.RootTable.Columns(EnumGridActivity.VoucherNo), Janus.Windows.GridEX.AggregateFunction.Count) & ") "
        'End If

        'If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Or e.Row.RowType = Janus.Windows.GridEX.RowType.TotalRow Then
        '    e.Row.Cells(EnumGridActivity.CrAmount).Text = Microsoft.VisualBasic.FormatNumber(e.Row.Cells(EnumGridActivity.CrAmount).Text, 2, , , TriState.True)
        '    e.Row.Cells(EnumGridActivity.DrAmount).Text = Microsoft.VisualBasic.FormatNumber(e.Row.Cells(EnumGridActivity.DrAmount).Text, 2, , , TriState.True)
        'End If

        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub grdAllRecords_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdActivity.SelectionChanged

    End Sub

    Private Sub btnDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisplay.Click
        If chkUnCredited.Checked = False And chkUnPresented.Checked = False And chkCredited.Checked = False And chkPresented.Checked = False And Me.optActivitySelected.Checked = True Then ShowErrorMessage("Plz select a valid option from selected criteria") : Exit Sub
        If chkPresented.Checked = True Or chkCredited.Checked = True Or optActivityAll.Checked = True Then
            Me.grdActivity.RootTable.Columns(EnumGridActivity.Selector).UseHeaderSelector = False

        Else

            Me.grdActivity.RootTable.Columns(EnumGridActivity.Selector).UseHeaderSelector = True
        End If

        Me.GetAllRecords("Activity")

    End Sub

#End Region

    Private Sub optActivityAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optActivityAll.CheckedChanged
        If optActivityAll.Checked = True Then

            Me.pnlChecks.Enabled = False
            Me.chkCredited.Checked = False
            Me.chkPresented.Checked = False
            Me.chkUnCredited.Checked = False
            Me.chkUnPresented.Checked = False

        Else

            Me.pnlChecks.Enabled = True

        End If
    End Sub

    'Private Sub optViewAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optViewAll.CheckedChanged, optViewUnPresented.CheckedChanged, optViewUnCredited.CheckedChanged
    '    'Try
    '    '    Me.GrdView.RootTable.Columns(EnumGridActivity.Selector).UseHeaderSelector = False

    '    '    Me.GetAllRecords()
    '    'Catch ex As Exception

    '    'End Try
    'End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        If Me.TabControl1.SelectedIndex = 0 Then
            Me.GetAllRecords("Activity")
        Else
            Me.GetAllRecords()
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub optViewAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optViewAll.Click, optViewUnPresented.Click, optViewUnCredited.Click
        Try
            Me.GrdView.RootTable.Columns(EnumGridActivity.Selector).UseHeaderSelector = False

            Me.GetAllRecords()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtpViewToDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpViewToDate.ValueChanged
        Try
            Me.GrdView.RootTable.Columns(EnumGridActivity.Selector).UseHeaderSelector = False

            Me.GetAllRecords()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GrdView_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles GrdView.FormattingRow

    End Sub

    'CR # 304
    Private Sub btnDsplyBnkStatmnt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDsplyBnkStatmnt.Click
        Try

            Me.CalculateBankBalance()
            Dim dt As DataTable = New BankReconcilationDal().GetBankStatementReport(Me.ListBox1.SelectedValue, Me.dtpFrmForBnkst.Value.Date, Me.dtpToFrBnkSt.Value.Date)
            If dt.Rows.Count > 0 Then
                dt.Columns.Add("Balance", GetType(System.Decimal))
                dt.Rows(0).Item("Balance") = Me.txtBnkStatmntBlnc.Text + (dt.Rows(0).Item("Dr_amount") - dt.Rows(0).Item("Cr_amount"))
                For i As Integer = 1 To dt.Rows.Count - 1
                    dt.Rows(i).Item("Balance") = dt.Rows(i - 1).Item("Balance") + (dt.Rows(i).Item("Dr_amount") - dt.Rows(i).Item("Cr_amount"))
                Next
                Me.grdBankStatement.DataSource = dt
                Me.ApplyGridSettings("Bank Statement")
            Else
                Me.grdBankStatement.DataSource = Nothing
            End If
            Me.txtBnkStatmntBlnc.Text = Microsoft.VisualBasic.FormatNumber(Me.txtBnkStatmntBlnc.Text, 2, , , TriState.True)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'CR # 304
    Private Sub CalculateBankBalance()
        Try
            'CR#319
            'We don't need calculation here for Ledger Balance, Unpresented,uncredited, we have to simply fetch all voucher of banks having cheque paid up to selected date
            'Dim ledgerbalance As Long = New BankReconcilationDal().GetLedgerBalance(Me.ListBox1.SelectedValue, Me.dtpFrmForBnkst.Value.AddDays(-1).Date, Me.dtpFrmForBnkst.Value.AddDays(-1).Date)
            'Dim unpresentedbalance As Long = New BankReconcilationDal().GetUnPresented(Me.ListBox1.SelectedValue, Me.dtpFrmForBnkst.Value.AddDays(-1).Date, Me.dtpFrmForBnkst.Value.AddDays(-1).Date)
            'Dim uncreditedBalance As Long = New BankReconcilationDal().GetUnCredited(Me.ListBox1.SelectedValue, Me.dtpFrmForBnkst.Value.AddDays(-1).Date, Me.dtpFrmForBnkst.Value.AddDays(-1).Date)
            'Me.txtBnkStatmntBlnc.Text = Val(ledgerbalance) + Val(unpresentedbalance) - Val(uncreditedBalance)
            Dim ledgerbalance As Long = New BankReconcilationDal().GetBankOpeningBalance(Me.ListBox1.SelectedValue, Me.dtpFrmForBnkst.Value.AddDays(-1).Date, Me.dtpToFrBnkSt.Value.AddDays(-1).Date)
            Me.txtBnkStatmntBlnc.Text = Val(ledgerbalance)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'CR # 304
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Try
            If Me.TabControl1.SelectedIndex = 2 Then
                Me.btnSave.Enabled = False
            Else
                Me.btnSave.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class