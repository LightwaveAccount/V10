Imports System.Windows.Forms
Imports System.Collections.Specialized
Imports Utility.Utility
Imports Model
Imports System.IO

Public Class MDIParent1

    Private dtToolBar As DataTable
    Dim objSMS As New DAL.SMSConfigurationDAL()
    Private SMSException As String = ""
    ''// CR#    Date Modified     Modified by           Brief Description			                
    '----------------------------------------------------------------------------------------------------------------------
    '//  62    02-July-2010       Abdul Jabbar          System should prompt for Log Off while closing GL Application
    '//  148    27-July-2011        Asif Kamal          New Form "Customer Information" has been added.
    '//  163    7-Dec-2011          Fatima Tajammal     Lightwave application main form should show information about loged in Financial Year and Company
    '//  150    12-Dec-2011        Fatima Tajammal      Put Lighwave's icon on each screen
    '//  177    26-March-2012       Fatima Tajammal     GL: Release version is not showing on the title bar.
    '//  239    17-May-2013        Asif Kamal           Proft & Loss Month Wise Report
    '//  240    22-May-2013        Farooq-H             SMS Integration in lightwave: make new utility to send sms
    '//  245    27-may-2013        Fatima Tajammal      Make a new report for Voucher History
    '//  247    30-may-2013        Fatima Tajammal      Show Release Notes in GL
    '//  241    12-jun-2013        Farooq-h             SMS Configuration Screen: A screen is required for sms configuration 
    '//  251    13-jun-2013        Farooq-h             New Screen as contact directory to record Contacts detail 
    '//  254    18-jun-2013        Farooq-h             SMS implementation 
    '//  264    29-jul-2013        Fatima Tajammal      Build a utility for ReIndexing database
    '//  267    15-Aug-2013        Waqas Anwar          Build a new utility to import lightwave data from souce to destination database. (Design + Code Changes)
    '//  265    10-sep-2013         FArooq-H            SMS: During Reading Mobile numbers for Customer/Suppliers system should pick required format.
    '//  279    23-sep-2013        Fatima Tajammal      SMS Configuration: System is not sending sms messages on making Transactions,
    '//  282    04-OCT-2013         FArooq-H            Send Message: System should prompt if SMS account is expired.
    '----------------------------------------------------------------------------------------------------------------------

#Region "Private Functions"

    'Private Sub CloseForms()
    '    Try

    '        ''frmPurchaseOrder.Close()
    '        ''frmDiscounts.Close()
    '        ''frmCustomerPayment.Close()
    '        ''frmCustomerReceipt.Close()
    '        ''frmPhysicalAuditSearch.Close()
    '        ''frmPhysicalAudit.Close()
    '        ''frmDaySummaryReport.Close()
    '        ''frmPurchaseReturn.Close()
    '        ''frmGRNMatrix.Close()
    '        ''frmGRN.Close()


    '        ''Sales Activities Forms
    '        ''//Check if already opened then Do not allow to open another instance
    '        ''For Each exfrm As Form In Application.OpenForms
    '        For exfrmInd As Integer = (Application.OpenForms.Count - 1) To 0 Step -1

    '            ''Shop Activities Forms
    '            If Application.OpenForms(exfrmInd).Name = frmSaleAndReturn.Name Then
    '                Application.OpenForms(exfrmInd).Close()
    '                Continue For
    '            End If
    '            ''Customer Defination Form
    '            If Application.OpenForms(exfrmInd).Name = frmCustomerInfo.Name Then
    '                Application.OpenForms(exfrmInd).Close()
    '                Continue For
    '            End If

    '            'Customer Receipt Form
    '            If Application.OpenForms(exfrmInd).Name = frmCustomerReceipt.Name Then
    '                Application.OpenForms(exfrmInd).Close()
    '                Continue For
    '            End If

    '            'Customer Payment Form
    '            If Application.OpenForms(exfrmInd).Name = frmCustomerPayment.Name Then
    '                Application.OpenForms(exfrmInd).Close()
    '                Continue For
    '            End If

    '            'Customer Address Blockage
    '            If Application.OpenForms(exfrmInd).Name = frmMemberAddressBlockage.Name Then
    '                Application.OpenForms(exfrmInd).Close()
    '                Continue For
    '            End If

    '            'Customer Order & Alteration
    '            If Application.OpenForms(exfrmInd).Name = frmAlteration.Name Then
    '                Application.OpenForms(exfrmInd).Close()
    '                Continue For
    '            End If

    '            ''Purchase Forms
    '            If Application.OpenForms(exfrmInd).Name = frmGRN.Name OrElse Application.OpenForms(exfrmInd).Name = frmGRNMatrix.Name OrElse Application.OpenForms(exfrmInd).Name = frmPurchaseOrder.Name Then
    '                Application.OpenForms(exfrmInd).Close()
    '                Continue For
    '            End If

    '            ''Accounting Transaction & Accoount Closing
    '            If Application.OpenForms(exfrmInd).Name = frmShopAccountingTransaction.Name OrElse Application.OpenForms(exfrmInd).Name = frmShopAccountClosing.Name Then
    '                Application.OpenForms(exfrmInd).Close()
    '                Continue For
    '            End If

    '            ''Physical Audit, Physical Audit Search, Physical Audit Reversal, Physical Audit Session
    '            If Application.OpenForms(exfrmInd).Name = frmPhysicalAudit.Name OrElse Application.OpenForms(exfrmInd).Name = frmPhysicalAuditSearch.Name _
    '                OrElse Application.OpenForms(exfrmInd).Name = frmPhysicalAuditReversal.Name OrElse Application.OpenForms(exfrmInd).Name = frmPhysicalAuditSession.Name Then

    '                Application.OpenForms(exfrmInd).Close()
    '                Continue For
    '            End If

    '            ''Customer Account Closing
    '            If Application.OpenForms(exfrmInd).Name = frmCustomerAccountClosing.Name Then
    '                Application.OpenForms(exfrmInd).Close()
    '                Continue For
    '            End If

    '        Next


    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub HideOpenedForms(ByVal Exceptfrm As Form)
    '    ''Check if already opened then Do not allow to open another instance
    '    If Application.OpenForms.Count <= 1 Then Exit Sub
    '    For Each frm As Form In Application.OpenForms
    '        If frm.Name <> Me.Name And Exceptfrm.Name <> frm.Name Then
    '            frm.SendToBack()

    '        End If
    '    Next

    'End Sub

    'Private Sub FillCombo()
    '    Try
    '        Dim dt As DataTable
    '        If gObjUserInfo.GroupInfo.GroupType = 1 Or gObjUserInfo.GroupInfo.GroupType = 2 Then
    '            dt = CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetUserShopList.ToString), DataTable).Copy
    '            Me.ddlShop.DisplayMember = "Shop"
    '            Me.ddlShop.ValueMember = "Shop ID"
    '            Me.ddlShop.DataSource = dt

    '        ElseIf gObjUserInfo.GroupInfo.GroupType = 3 Then

    '            dt = CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetUserShopList.ToString), DataTable).Copy
    '            Dim objDV As DataView = GetFilterDataFromDataTable(dt, "[Shop ID] = " & gObjUserInfo.ShopInfo.ShopID)
    '            Me.ddlShop.DisplayMember = "Shop"
    '            Me.ddlShop.ValueMember = "Shop ID"
    '            Me.ddlShop.DataSource = objDV
    '        End If


    '        Dim MyVersion As String = DecryptWithALP(GetSystemConfigurationValue("Version"))
    '        If MyVersion = "1" Then ''PERSONAL

    '            If Me.ddlShop.Items.Count = 2 Then
    '                Me.ddlShop.SelectedIndex = 1
    '            Else
    '                Me.ddlShop.SelectedIndex = 0
    '            End If

    '        Else
    '            Me.ddlShop.SelectedIndex = 0

    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Public Sub SetMenusAsPerVersion()
    '    Try

    '        Dim mnu As Object 'ToolStripMenuItem

    '        For Each mnu In Me.MenuStrip.Items
    '            If TypeOf mnu Is ToolStripMenuItem Then mnu.Visible = True
    '        Next


    '        Dim MyVersion As String = DecryptWithALP(GetSystemConfigurationValue("Version"))


    '        If MyVersion = "1" Then ''PERSONAL

    '            ''If gObjUserInfo.GroupInfo.GroupType = 1 Or gObjUserInfo.GroupInfo.GroupType = 2 Or gObjUserInfo.GroupInfo.GroupType = 3 Then
    '            'mnuConfiguration.Visible = True

    '            ''Configuration\ Misc\
    '            Dim strIsProductCodeOnly As String = GetSystemConfigurationValue("product_code_only")
    '            If strIsProductCodeOnly = "" Or strIsProductCodeOnly = "True" Then mnuSizes.Visible = False
    '            If strIsProductCodeOnly = "" Or strIsProductCodeOnly = "True" Then mnuCombinations.Visible = False


    '            ''Shop Activities
    '            mnuShopActivities.Visible = True
    '            mnuCustomerClaims.Visible = False
    '            ' mnuProcessSTROnShop.Visible = False
    '            '  mnuShopMessages.Visible = False
    '            Dim strPOSClosing As String = GetSystemConfigurationValue("IsEnblePosClosing")
    '            If strPOSClosing = "" Or strPOSClosing = "False" Then mnuPOSCashFlowManagement.Visible = False
    '            mnuReplication.Visible = False
    '            mnuProcessSTROnShop.Visible = False


    '            ''Customer Club
    '            Dim strCustomerModule As String = GetSystemConfigurationValue("Member_Module")
    '            If strCustomerModule = "" Or strCustomerModule = "False" Then
    '                mnuCustomerClubToolStripMenuItem.Visible = False
    '                mnuCustomerDefination.Visible = False
    '                mnuCustomerPayment.Visible = False
    '                mnuCustomerReceipt.Visible = False
    '                mnuCustomerAccountClosing.Visible = False
    '            End If

    '            mnuCustomerClaimsAtHO.Visible = False

    '            ''Utilities
    '            mnuShopMessagesAtHO.Visible = False
    '            mnuSearchProduct.Visible = False


    '            ''Inventoy Mngt.
    '            mnuInventoryMgmt.Visible = False


    '            ''Purchase 
    '            mnuPurchase.Visible = True
    '            If strIsProductCodeOnly = "" Or strIsProductCodeOnly = "True" Then mnuPurchaseOrderMatrix.Visible = False
    '            If strIsProductCodeOnly = "" Or strIsProductCodeOnly = "True" Then mnuGRNMatrix.Visible = False




    '            ''Reports
    '            B02ProductAuditReportToolStripMenuItem.Visible = False
    '            IFranchiseReportsToolStripMenuItem.Visible = False
    '            If strIsProductCodeOnly = "" Or strIsProductCodeOnly = "True" Then mnuRptShopInvMatrix.Visible = False


    '            mnuLogOff.Visible = True



    '        ElseIf MyVersion = "2" Then ''PROFESHIONAL

    '            ''Shop Activities 
    '            ' mnuShopMessages.Visible = False

    '            ''Utilities
    '            mnuShopMessagesAtHO.Visible = False
    '            mnuSearchProduct.Visible = False
    '            ''Inventory Manamgment
    '            mnuSTRByInvLevel.Visible = False



    '            Dim strIsProductCodeOnly As String = GetSystemConfigurationValue("product_code_only")
    '            Dim blnIsPublisher As Boolean = frmLogin.IsPublisher(System.Configuration.ConfigurationManager.AppSettings("StrDBServerName").ToString)

    '            If blnIsPublisher Then
    '                If gObjUserInfo.GroupInfo.GroupType = "3" Then

    '                    ''Customer Club
    '                    Dim strCustomerModule As String = GetSystemConfigurationValue("Member_Module")
    '                    If strCustomerModule = "" Or strCustomerModule = "False" Then
    '                        mnuCustomerClubToolStripMenuItem.Visible = False
    '                        mnuCustomerDefination.Visible = False
    '                        mnuCustomerPayment.Visible = False
    '                        mnuCustomerReceipt.Visible = False
    '                        mnuCustomerAccountClosing.Visible = False
    '                    End If

    '                    mnuCustomerClaimsAtHO.Visible = False

    '                    mnuInventoryMgmt.Visible = False
    '                    mnuReplication.Visible = False


    '                    ''Purchase 
    '                    mnuPurchase.Visible = True
    '                    If strIsProductCodeOnly = "" Or strIsProductCodeOnly = "True" Then mnuPurchaseOrderMatrix.Visible = False
    '                    If strIsProductCodeOnly = "" Or strIsProductCodeOnly = "True" Then mnuGRNMatrix.Visible = False


    '                End If

    '            ElseIf Not blnIsPublisher Then
    '                If gObjUserInfo.GroupInfo.GroupType = "3" Then


    '                    ''Coniguration
    '                    mnuConfiguration.Visible = False


    '                    ''Shop Activities
    '                    mnuShopActivities.Visible = True
    '                    mnuCustomerClaims.Visible = False
    '                    'mnuShopMessages.Visible = False
    '                    Dim strPOSClosing1 As String = GetSystemConfigurationValue("IsEnblePosClosing")
    '                    If strPOSClosing1 = "" Or strPOSClosing1 = "False" Then mnuPOSCashFlowManagement.Visible = False


    '                    ''Customer Club
    '                    Dim strCustomerModule As String = GetSystemConfigurationValue("Member_Module")
    '                    If strCustomerModule = "" Or strCustomerModule = "False" Then
    '                        mnuCustomerClubToolStripMenuItem.Visible = False
    '                        mnuCustomerDefination.Visible = False
    '                        mnuCustomerPayment.Visible = False
    '                        mnuCustomerReceipt.Visible = False
    '                        mnuCustomerAccountClosing.Visible = False
    '                    End If

    '                    mnuCustomerClaimsAtHO.Visible = False

    '                    ''Inventoy Mngt.
    '                    mnuInventoryMgmt.Visible = False

    '                    ''Purchase 
    '                    mnuPurchase.Visible = True
    '                    If strIsProductCodeOnly = "" Or strIsProductCodeOnly = "True" Then mnuPurchaseOrderMatrix.Visible = False
    '                    If strIsProductCodeOnly = "" Or strIsProductCodeOnly = "True" Then mnuGRNMatrix.Visible = False


    '                    ''Reports
    '                    B02ProductAuditReportToolStripMenuItem.Visible = False
    '                    IFranchiseReportsToolStripMenuItem.Visible = False
    '                    If strIsProductCodeOnly = "" Or strIsProductCodeOnly = "True" Then mnuRptShopInvMatrix.Visible = False


    '                    mnuLogOff.Visible = True


    '                    ''Utilities
    '                    mnuInventoryLevelManualInUtilities.Visible = False
    '                    mnuChangeProductNo.Visible = False
    '                    mnuChangeProductCostPrice.Visible = False
    '                    mnuSetItemAverageCost.Visible = False
    '                    mnuChangeProductStatus.Visible = False
    '                    mnuSelectedBarCodeGenerator.Visible = False
    '                    mnuToolbarCustomizationTool.Visible = False
    '                    mnuSetProductRetailPrice.Visible = False
    '                    mnuShopMessagesAtHO.Visible = False

    '                    ''Security
    '                    mnuSecurityToolStrip.Visible = False

    '                Else
    '                    Throw New Exception("***")
    '                End If
    '            End If


    '        ElseIf MyVersion = "3" Then     ''ENTERPRISE


    '            Dim strIsProductCodeOnly As String = GetSystemConfigurationValue("product_code_only")
    '            Dim blnIsPublisher As Boolean = frmLogin.IsPublisher(System.Configuration.ConfigurationManager.AppSettings("StrDBServerName").ToString)

    '            If blnIsPublisher Then
    '                If gObjUserInfo.GroupInfo.GroupType = "3" Then

    '                    ''Customer Club
    '                    Dim strCustomerModule As String = GetSystemConfigurationValue("Member_Module")
    '                    If strCustomerModule = "" Or strCustomerModule = "False" Then
    '                        mnuCustomerClubToolStripMenuItem.Visible = False
    '                        mnuCustomerDefination.Visible = False
    '                        mnuCustomerPayment.Visible = False
    '                        mnuCustomerReceipt.Visible = False
    '                        mnuCustomerAccountClosing.Visible = False
    '                    End If

    '                    'mnuCustomerClaimsAtHO.Visible = False


    '                    mnuInventoryMgmt.Visible = False
    '                    mnuReplication.Visible = False


    '                    ''Purchase 
    '                    mnuPurchase.Visible = True
    '                    If strIsProductCodeOnly = "" Or strIsProductCodeOnly = "True" Then mnuPurchaseOrderMatrix.Visible = False
    '                    If strIsProductCodeOnly = "" Or strIsProductCodeOnly = "True" Then mnuGRNMatrix.Visible = False


    '                End If

    '            ElseIf Not blnIsPublisher Then
    '                If gObjUserInfo.GroupInfo.GroupType = "3" Then


    '                    ''Coniguration
    '                    mnuConfiguration.Visible = False


    '                    ''Shop Activities
    '                    mnuShopActivities.Visible = True

    '                    'mnuCustomerClaims.Visible = False
    '                    ' mnuShopMessages.Visible = False
    '                    Dim strPOSClosing1 As String = GetSystemConfigurationValue("IsEnblePosClosing")
    '                    If strPOSClosing1 = "" Or strPOSClosing1 = "False" Then mnuPOSCashFlowManagement.Visible = False


    '                    ''Customer Club
    '                    Dim strCustomerModule As String = GetSystemConfigurationValue("Member_Module")
    '                    If strCustomerModule = "" Or strCustomerModule = "False" Then
    '                        mnuCustomerClubToolStripMenuItem.Visible = False
    '                        mnuCustomerDefination.Visible = False
    '                        mnuCustomerPayment.Visible = False
    '                        mnuCustomerReceipt.Visible = False
    '                        mnuCustomerAccountClosing.Visible = False
    '                    End If

    '                    mnuCustomerClaimsAtHO.Visible = False

    '                    ''Utilities
    '                    ''mnuShopMessagesAtHO.Visible = False
    '                    mnuSearchProduct.Visible = False


    '                    ''Inventoy Mngt.
    '                    mnuInventoryMgmt.Visible = False


    '                    ''Purchase 
    '                    mnuPurchase.Visible = True
    '                    If strIsProductCodeOnly = "" Or strIsProductCodeOnly = "True" Then mnuPurchaseOrderMatrix.Visible = False
    '                    If strIsProductCodeOnly = "" Or strIsProductCodeOnly = "True" Then mnuGRNMatrix.Visible = False



    '                    ''Reports
    '                    B02ProductAuditReportToolStripMenuItem.Visible = False
    '                    IFranchiseReportsToolStripMenuItem.Visible = False
    '                    If strIsProductCodeOnly = "" Or strIsProductCodeOnly = "True" Then mnuRptShopInvMatrix.Visible = False

    '                    ''Utilities
    '                    mnuInventoryLevelManualInUtilities.Visible = False
    '                    mnuChangeProductNo.Visible = False
    '                    mnuChangeProductCostPrice.Visible = False
    '                    mnuSetItemAverageCost.Visible = False
    '                    mnuChangeProductStatus.Visible = False
    '                    mnuSelectedBarCodeGenerator.Visible = False
    '                    mnuToolbarCustomizationTool.Visible = False
    '                    mnuSetProductRetailPrice.Visible = False

    '                    mnuLogOff.Visible = True


    '                    ''Security
    '                    mnuSecurityToolStrip.Visible = False

    '                Else
    '                    Throw New Exception("***")
    '                End If
    '            End If


    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Function IsShopSelected() As Boolean
    '    If Me.ddlShop.Items.Count = 1 Then
    '        Return True
    '    ElseIf Me.ddlShop.Items.Count > 1 Then
    '        If ddlShop.SelectedIndex = 0 Then
    '            'TODO: (Fahad) use variable for message
    '            ShowErrorMessage("Please Select a Shop")
    '        Else
    '            Return True
    '        End If
    '    End If
    'End Function

    'Private Sub ReNameCustomizeDefinitionMenus()
    '    Try
    '        mnuProductVariable1.Text = GetSystemConfigurationValue("Age Group")

    '        mnuProductVariable2.Text = GetSystemConfigurationValue("Packaging Type")

    '        mnuProductVar3.Text = GetSystemConfigurationValue("Life Type")

    '        mnuProductVariable4.Text = GetSystemConfigurationValue("Product Gender")

    '        mnuProductVariable5.Text = GetSystemConfigurationValue("Value Addition")


    '        If mnuProductVariable1.Text.ToUpper = "Not in Use".ToUpper Then mnuProductVariable1.Enabled = False
    '        If mnuProductVariable2.Text.ToUpper = "Not in Use".ToUpper Then mnuProductVariable2.Enabled = False
    '        If mnuProductVar3.Text.ToUpper = "Not in Use".ToUpper Then mnuProductVar3.Enabled = False
    '        If mnuProductVariable4.Text.ToUpper = "Not in Use".ToUpper Then mnuProductVariable4.Enabled = False
    '        If mnuProductVariable5.Text.ToUpper = "Not in Use".ToUpper Then mnuProductVariable5.Enabled = False


    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
#End Region
    'ByVal frm As EnumProjectForms, 
    Public Sub ShowForm(ByVal MyForm As Form)
        Try

            Application.DoEvents()
            ''Set Cursor as Busy
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            ''//Check if already opened then Do not allow to open another instance
            For Each exfrm As Form In Application.OpenForms
                If exfrm.Name = MyForm.Name Then
                    exfrm.BringToFront()
                    Exit Sub
                End If
            Next

            Dim IsForm As Boolean = True

            If CanUserViewThisForm(MyForm.Name, IsForm) Then
                MyForm.MdiParent = Me
                Application.DoEvents()
                If IsForm Then
                    ApplyStyleSheet(MyForm, MyForm.Name)
                Else
                    ApplyStyleSheet(MyForm, MyForm.Name, "Report")
                End If
                MyForm.ControlBox = True
                MyForm.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                'MyForm.Icon = New System.Drawing.Icon(Icon.FromHandle((CType(ImageList1.Images(0), Bitmap)).GetHicon()), Me.Icon.Size)
                MyForm.MaximizeBox = True
                MyForm.WindowState = FormWindowState.Maximized

                ''sets the text of the form that is the text of Tab
                'TODO: Shipft below line on each form
                'frmCity.Text = FormName.TabPgCity.Text
                MyForm.Show()
            Else
                ShowValidationMessage("You don't have rights to view this option")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''this f(x) will be use only from button bar on the mdi
    Private Sub ShowForms(ByVal frm As String, ByVal MyForm As Form)
        Try

            Application.DoEvents()
            ''Set Cursor as Busy
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            ''//Check if already opened then Do not allow to open another instance
            For Each exfrm As Form In Application.OpenForms
                If exfrm.Name = MyForm.Name Then
                    exfrm.BringToFront()
                    Exit Sub
                End If
            Next


            Dim IsForm As Boolean = True

            If CanUserViewThisForm(frm, IsForm) Then
                MyForm.MdiParent = Me
                Application.DoEvents()
                If IsForm Then
                    ApplyStyleSheet(MyForm, frm.ToString)
                Else
                    ApplyStyleSheet(MyForm, frm.ToString, "Report")
                End If
                MyForm.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                MyForm.MaximizeBox = True
                MyForm.WindowState = FormWindowState.Maximized
                MyForm.ControlBox = True
                ''sets the text of the form that is the text of Tab
                'TODO: Shipft below line on each form
                'frmCity.Text = FormName.TabPgCity.Text
                MyForm.Show()
            Else
                ShowValidationMessage("You don't have rights to view this option")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try
            Call Me.ShowForm(frmDefCity)

        Catch ex As Exception
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub



#Region "Form Events"

    Private Sub MDIParent1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        End
    End Sub

    Private Sub MDIParent1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'CR#62
        If ShowConfirmationMessage("Are you sure you want to quit", MessageBoxDefaultButton.Button1) = MsgBoxResult.Yes Then
            End
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub MDIParent1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Me.Visible = True
            Me.proConfigurationSetting()

            Me.SetProductInformation()

            'CR#150

            Me.Icon = Drawing.Icon.FromHandle((CType(ImageList1.Images(1), Bitmap)).GetHicon())

            'Temporarily Comments...have to uncomment while Versin Mentaining 3.0.0.1

            'Dim dblSchemaVersion As Double
            'Dim dblReleaseVersion As Double
            'Dim dblVerArray() As Double

            'dblReleaseVersion = My.Application.Info.Version.Major & My.Application.Info.Version.Minor & My.Application.Info.Version.Revision
            'dblSchemaVersion = Val(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Schema_Version"))

            'dblVerArray = GetVersionsDelta(dblSchemaVersion, dblReleaseVersion)

            ' ''check if array is not blank then
            ''If UBound(dblVerArray) > 0 Then
            ''    Dim intCounter As Integer
            ''    For intCounter = LBound(dblVerArray) To UBound(dblVerArray) - 1
            ''        If Not funVersionUpdate(dblVerArray(intCounter)) Then Exit For
            ''    Next
            ''End If

            ''dblSchemaVersion = Val(funGetConfigValue("Schema_Version"))

            'If dblReleaseVersion > dblSchemaVersion Then
            '    If ShowConfirmationMessage("Current Database Schema is not compatible with this Candela Release ! You want to continue?", MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
            '        Me.Close()
            '        End
            '    End If
            'End If

            ' ''Check the GL Valid License if its true then continue using GL
            ''If Not ISValidGLLicense Then

            ''    frmRegisterProduct.Show(vbModal)
            ''End If

            ' Set seperator value
            G_SEPERATOR = DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Seperator")

            '               Add new config value for Profit & Loss Account
            G_PROFIT_LOSS_ACC_ID = Val(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Profit_Loss_Acc_ID").ToString)


            '//  240    22-May-2013        Farooq-H 
            If gblnTrialVersion = True Then
                Me.mnuChangePassword.Visible = True
                Me.SchemaSnapshotToolStripMenuItem.Visible = False
                Me.SendSMSToolStripMenuItem.Visible = False
                ' mnuUtilities.Visible = True
            Else
                Me.mnuChangePassword.Visible = False
                Me.SchemaSnapshotToolStripMenuItem.Visible = True
                Me.SendSMSToolStripMenuItem.Visible = True
                ' mnuUtilities.Visible = False
            End If

            ''Asif Kamal
            If DAL.SystemConfigurationDAL.GetSystemConfigurationValue("CustomerInfo") = "True" Then
                CustomerInfo.Visible = True
            Else
                CustomerInfo.Visible = False

            End If
            ''
            If DAL.SystemConfigurationDAL.GetSystemConfigurationValue("SendSMS").ToString.ToUpper = "TRUE" Then
                TmSMS.Enabled = True
            Else
                TmSMS.Enabled = False
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Visible = True
            Me.Cursor = Cursors.Default
        End Try

    End Sub


    Public Function GetVersionsDelta(ByVal dblSchemaVersion As Double, ByVal dblApplicaitonVersion As Double) As Double()
        Try



            Dim arrVersions() As Double

            'Set the same message for the old versions
            If dblSchemaVersion < 201 Then

                If ShowConfirmationMessage("Current Product version is not compatible with the Schema version! Do you want to continue?", MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    Me.Close()
                    End
                End If
                'return null array
                ReDim arrVersions(0)
                GetVersionsDelta = arrVersions

                'check if schema needs to be updated then calculate the dalta and return the array
            ElseIf dblSchemaVersion < dblApplicaitonVersion Then

                Dim dblDiff As Double
                Dim intCounter As Integer

                'calculate the differance
                dblDiff = dblApplicaitonVersion - dblSchemaVersion
                'redefine the array
                ReDim arrVersions(dblDiff)

                '22-Feb-08 03:41 PM    Fawad Nawaz Khan      Modified code for fixing Bug i.e. Auto Version Update: can not update more than one version at a time (Ref cr 946)
                '================
                'build the versions array
                For intCounter = 0 To dblDiff - 1
                    arrVersions(intCounter) = dblSchemaVersion + intCounter + 1
                Next

                'return the versions array
                GetVersionsDelta = arrVersions
            Else

                'return the null array
                ReDim arrVersions(0)
                GetVersionsDelta = arrVersions
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function



#End Region



    Private Sub GroupsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGroups.Click
        Try
            Call Me.ShowForm(frmDefSecurityGroup)

        Catch ex As Exception
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

    End Sub



    Private Sub mnuUsers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUsers.Click
        Try
            Call Me.ShowForm(frmDefSecurityUser)

        Catch ex As Exception
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Private Sub mnuGroupRights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGroupRights.Click
        Try
            Call Me.ShowForm(FrmGroupRights)

        Catch ex As Exception
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub mnuGroupShopsRights_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ' Call Me.ShowForm(fromGroupShopsRights)

        Catch ex As Exception
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub





    Private Sub mnuLogViewer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuChangePass.Click
        Try

            Call Me.ShowForm(frmChangePassword)

        Catch ex As Exception
            ''Set Cursor as Default
            ShowErrorMessage(ex.Message)
        Finally
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub



    Private Sub mnuChangePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuChangePassword.Click
        Try
            'Call Me.ShowForm(frmEdition)
            frmEdition.ShowDialog()
        Catch ex As Exception
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub



    Private Sub mnuCascade_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCascade.Click
        Try
            Me.LayoutMdi(MdiLayout.Cascade)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub mnuTileHorizontal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTileHorizontal.Click
        Try
            Me.LayoutMdi(MdiLayout.TileHorizontal)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub mnuTileVertical_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTileVertical.Click
        Try
            Me.LayoutMdi(MdiLayout.TileVertical)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub mnuArrangeIcons_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuArrangeIcons.Click
        Try
            Me.LayoutMdi(MdiLayout.ArrangeIcons)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub mnuMinimizeAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMinimizeAll.Click
        Try
            For Each frm As Form In Me.MdiChildren
                frm.WindowState = FormWindowState.Minimized
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub mnuRestoreAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRestoreAll.Click
        Try
            For Each frm As Form In Me.MdiChildren
                frm.WindowState = FormWindowState.Normal
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub mnuCloseAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCloseAll.Click
        Try
            For Each frm As Form In Me.MdiChildren
                frm.Close()
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub mnuAboutCandela_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAboutCandela.Click
        Try
            'Call Me.ShowForm(EnumProjectForms.frmRptProductPriceNotDefined, frmUndefinedPrice, False)
            Dim frm As New AboutBox1
            frm.ShowDialog()

        Catch ex As Exception
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub SetProductInformation()
        Try
            Me.lblUserName.Text = "[" + "User:" + gObjUserInfo.UserName + "] "
            Me.lblUserType.Text = "[" + "Group:" + gObjUserInfo.GroupInfo.GroupName.ToString + "] "
            'Me.lblDB.Text = String.Format("[{0}]", System.Configuration.ConfigurationManager.AppSettings.Item("StrDBName").ToString)

            'CR # 163 Lightwave application main form should show information about loged in Financial Year and Company 
            Me.lblFinancialYear.Text = "[" + "Financial Year:" + gObjFinancialYearInfo.YearCode + "] "
            Me.lblCompany.Text = " [Company Name:" + gobjLocationInfo.CompanyName + "]"
            '....................................................................
            'CR # 177 GL: Release version is not showing on the title bar.
            '..........................
            Me.lblSchemaVersion.Text = String.Format("[SV : {0}]", DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Schema_Version"))
            ' Me.lblSchemaVersion.BackColor = Color.Transparent
            '...........................
            Me.lblFinancialYear.BackColor = Color.Transparent
            Me.lblUserType.BackColor = Color.Transparent
            Me.lblUserName.BackColor = Color.Transparent
            Me.mdilblStatus.BackColor = Color.Transparent
            Me.lblCompany.BackColor = Color.Transparent


            'Me.Text = gstrMsgTitle '& " " & IIf(DecryptWithALP(GetSystemConfigurationValue("Version")) = 1, "Personal Edition", IIf(DecryptWithALP(GetSystemConfigurationValue("Version")) = 2, "Professional Edition", "Enterprise Edition")) & " " & My.Application.Info.Version.ToString(4)
            'CR # 177 GL: Release version is not showing on the title bar.
            '...........................
            Me.Text = gstrMsgTitle & " " & My.Application.Info.Version.ToString
            '...........................

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Public Sub mnuProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFYear.Click
        Try
            Call Me.ShowForm(frmDefFiniancialYear)

        Catch ex As Exception
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)

        Finally
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub


    Public Sub proConfigurationSetting()
        Try

            'mnuPaymentVoucher.Enabled = IIf(DecryptWithALP(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Integration_Of_Sale_Purchase"), "true") = "1", True, False)
            'mnuReceiptVoucher.Enabled = IIf(DecryptWithALP(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Integration_Of_Sale_Purchase"), "true") = "1", True, False)

            'mnuAgeingReceivable.Text = IIf(DecryptWithALP(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Integration_Of_Sale_Purchase"), "true") = "1", "Ageing Receivable", "Ageing Receivable (Ledger Based)")
            'mnuAgeingPayable.Text = IIf(DecryptWithALP(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Integration_Of_Sale_Purchase"), "true") = "1", "Ageing Payable", "Ageing Payable (Ledger Based)")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mnuSalesAndReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSalesAndReturn.Click
        Try

            Call Me.ShowForm(frmAccountMain)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Private Sub FinancialYearStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinancialYearStatusToolStripMenuItem.Click

        Try

            Call Me.ShowForm(frmDefGLFinancialYearStatus)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Private Sub mnuLogOff_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLogOff.Click
        Try
            'CR#62
            If ShowConfirmationMessage("Are you sure you want to Log Off", MessageBoxDefaultButton.Button1) = MsgBoxResult.Yes Then
                frmLogin.Show()
                Me.Finalize()
                Me.Dispose()

            Else
                Exit Sub
            End If

        
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        
    End Sub

    Private Sub LogViewerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogViewerToolStripMenuItem.Click
        Try

            Call Me.ShowForm(frmLogViewer)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub BankPositionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankPositionToolStripMenuItem.Click
        Try

            Call Me.ShowForm(frmBankPosition)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub PostDatedChequesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostDatedChequesToolStripMenuItem.Click
        Try

            Call Me.ShowForm(frmRptPostDatedCheques)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub TaxDeductionReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TaxDeductionReportToolStripMenuItem.Click
        Try
            Call Me.ShowForm(frmrptTax)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Public Sub MnuCustomerCardPrinting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCustomerCardPrinting.Click
        Dim _TempCallFromSearch As Boolean = False
        Dim _TempVoucherIdsList As String = String.Empty

        Try


            Dim MyForm As New frmGLVoucher
            Dim BlnISVOpened As Boolean = False

            If CanUserViewThisForm(MyForm.Name, True) Then
                Dim frmV As New System.Windows.Forms.Form

                'If voucher form is opened then close it
                For Each frm As System.Windows.Forms.Form In Application.OpenForms
                    If frm.Name = "frmGLVoucher" Then
                        BlnISVOpened = True
                        frmV = frm
                    End If
                Next

                If _blnCallFromSearchPost Then
                    _TempCallFromSearch = True
                    _TempVoucherIdsList = _gstrVoucherIDs
                Else
                    _TempCallFromSearch = False
                    _TempVoucherIdsList = String.Empty
                End If

                If BlnISVOpened = True Then frmV.Close()

                _blnCallFromSearchPost = _TempCallFromSearch
                _gstrVoucherIDs = _TempVoucherIdsList


                _TempVouchers = False
                MyForm.MdiParent = Me
                MyForm.ControlBox = True
                MyForm.MaximizeBox = True
                ApplyStyleSheet(MyForm, MyForm.Name)
                MyForm.Show()

            Else
                ShowValidationMessage("You don't have rights to view this option")
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub ProfitLossToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProfitLossToolStripMenuItem.Click
        Try

            Call Me.ShowForm(frmRptProfitandLoss)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub AccountLedgerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AccountLedgerToolStripMenuItem.Click
        Try

            Call Me.ShowForm(frmrptAccountLedger)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

   

    Private Sub BalanceSheetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BalanceSheetToolStripMenuItem.Click
        Try

            Call Me.ShowForm(frmRptBalanceSheet)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Private Sub mnuAgeingReceivable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAgeingReceivable.Click, mnuAgeingPayable.Click
        Try

            Dim mnuItem As Windows.Forms.ToolStripMenuItem = CType(sender, Windows.Forms.ToolStripMenuItem)

            'Application.DoEvents()
            If mnuItem.Name = "mnuAgeingReceivable" Then

                If frmRptAgeingLedger.pbFormType = "Ageing Payable (Ledger Based)" Then Exit Sub
            Else
                If frmRptAgeingLedger.pbFormType = "Ageing Receivable (Ledger Based)" Then Exit Sub
            End If

            ''//Check if already opened then Do not allow to open another instance
            For Each exfrm As Form In Application.OpenForms
                If exfrm.Name = frmRptAgeingLedger.Name Then
                    exfrm.Close()
                    Exit For
                End If
            Next

            If mnuItem.Name = "mnuAgeingReceivable" Then
                frmRptAgeingLedger.pbFormType = "Ageing Payable (Ledger Based)"
            Else
                  frmRptAgeingLedger.pbFormType = "Ageing Receivable (Ledger Based)"
            End If

            Call Me.ShowForm(frmRptAgeingLedger)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Private Sub GLCostCenterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLCostCenterToolStripMenuItem.Click
        Try

            Call Me.ShowForm(frmDefGLCostCenter)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Private Sub CashFlowStatementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CashFlowStatementToolStripMenuItem.Click
        Try

            Call Me.ShowForm(frmRptGLCashFlow)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Private Sub BankReconciliationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankReconciliationToolStripMenuItem.Click

        Try

            Call Me.ShowForm(frmBankReconcilation)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Public Sub MnuCustomerAddressBlockage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try
            Call Me.ShowForm(frmGLPostingVoucher)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try


    End Sub


    Private Sub MnuCustomerAddressBlockage_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCustomerAddressBlockage.Click
        Try
            _TempVouchers = False
            Call Me.ShowForm(frmGLPostingVoucher)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Public Sub TemporarayVoucherToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TemporarayVoucherToolStripMenuItem.Click
        'Dim ObjVoucher As New frmGLVoucher(True)

        Try


            Dim MyForm As New frmGLVoucher
            ''//Check if already opened then Close it 
            'For Each exfrm As Form In Application.OpenForms
            '    If exfrm.Name = MyForm.Name Then
            '        exfrm.Close()
            '    End If
            'Next


            ' Dim MyForm As New frmGLVoucher

            If CanUserViewThisForm(MyForm.Name, True) Then

                _TempVouchers = True
                MyForm.MdiParent = Me
                MyForm.ControlBox = True
                MyForm.MaximizeBox = True
                ApplyStyleSheet(MyForm, MyForm.Name)
                MyForm.Show()

            End If

            ' Call Me.ShowForm(frmGLVoucher)

            'Dim myform As New frmGLVoucher


        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub mnuAccountTransacion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub mnuAccountTransacion_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAccountTransacion.Click
        Try

            Call Me.ShowForm(frmAccountSub)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub mnuAccountLedger_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAccountLedger.Click
        Try

            Call Me.ShowForm(frmAccountSubSub)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Private Sub mnuPhysicalAudit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPhysicalAudit.Click
        Try

            Call Me.ShowForm(frmDetailAccount)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub TempVoucherPostingSearchingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TempVoucherPostingSearchingToolStripMenuItem.Click
        Try
            _TempVouchers = True
            Call Me.ShowForm(frmGLPostingVoucher)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub
    Private Sub GLNotesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLNotesToolStripMenuItem.Click
        Try
            Call Me.ShowForm(frmGLNotesReport)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub ToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem8.Click
        Try

            Dim dv As DataView = GetFilterDataFromDataTable(New DAL.SecurityUserDAL().GetUserFormRights(gObjUserInfo.UserID), "[Form Name] ='frmrptCOA' and [Control Name] ='View' ")

            If dv.Count > 0 Then

                Dim mobjControlList As NameValueCollection
                ' Getting all available controls list to the user on this screen; in a collection ..
                'mobjControlList = GetFormSecurityControls(Me.Name)
                mobjControlList = GetFormSecurityControls("frmrptCOA")


                Dim objHashTableParamter As New Hashtable

                ' Giving Report Name .. 
                objHashTableParamter.Add("ReportPath", "\rptChartofAccounts.rpt")

                ' Passing Parameters .. (Report Parameters .. )
                objHashTableParamter.Add("companyname", gobjLocationInfo.CompanyName)
                objHashTableParamter.Add("address", gobjLocationInfo.CompanyAddress)


                ' Adding Parameter Of Print And Export Button .. 
                ' =======================================================
                If mobjControlList.Item("btnPrint") Is Nothing Then
                    objHashTableParamter.Add("PrintRights", "False")
                Else
                    objHashTableParamter.Add("PrintRights", "True")
                End If


                If mobjControlList.Item("btnExport") Is Nothing Then
                    objHashTableParamter.Add("ExportRights", "False")
                Else
                    objHashTableParamter.Add("ExportRights", "True")
                End If
                ' =======================================================
                ' =======================================================

                Utility.Utility.gObjMyAppHashTable.Add(EnumHashTableKeyConstants.SetReportParametersList, objHashTableParamter)

                Dim rptViewer As New rptViewer
                rptViewer.Text = Me.Text
                rptViewer.Show()

            Else
                ShowValidationMessage("You don't have rights to view this option")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub MainSubSubAccountMappingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainSubSubAccountMappingToolStripMenuItem.Click
        Try
            Call Me.ShowForm(frmMainSubSubAccountMappingReport)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub DailyActivityReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DailyActivityReportToolStripMenuItem.Click
        Try
            Call Me.ShowForm(frmDailyActivityReport)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    'Private Sub GLVoucherToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLVoucherToolStripMenuItem.Click
    '    Try
    '        Call Me.ShowForm(frmGLVoucherReport)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    Finally
    '        Me.Cursor = System.Windows.Forms.Cursors.Default
    '    End Try
    'End Sub

    Private Sub GLVoucherToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLVoucherToolStripMenuItem.Click
        Try
            Call Me.ShowForm(frmGLVoucherReport)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub TrilBalanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrilBalanceToolStripMenuItem.Click
        Try

            Call Me.ShowForm(frmRptGLTrialBalance)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub ConfigurationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfigurationToolStripMenuItem.Click
        Try
            Call Me.ShowForm(frmSystemConfiguration)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Public Sub MnuItemCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuItemCompany.Click
        Try

            Call Me.ShowForm(frmDefCompany)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub BackupDatabaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackupDatabaseToolStripMenuItem.Click
        Try

            Me.ShowForm(frmDBBackup)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub ToolStripMenuItem11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem11.Click
        Try

            Call Me.ShowForm(frmRptGLCashFlowStandered)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub RegisterLightwaveOnlineToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegisterLightwaveOnlineToolStripMenuItem.Click
        Try
            Dim frmN As New frmProductRegisteration
            frmN.ShowDialog()

        Catch ex As Exception
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub SchemaSnapshotToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SchemaSnapshotToolStripMenuItem.Click
        Try

            Call Me.ShowForm(frmSchemaSnapshot)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub mnuCustomerClubToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCustomerClubToolStripMenuItem.Click

    End Sub

    Private Sub CustomerInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomerInfo.Click
        ''CR# 148   Transaction > New form need of Customer information
        Try

            Call Me.ShowForm(frmCustomerInfo)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

    End Sub
    'CR 240 [Farooq-H]
    Private Sub SendSMSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendSMSToolStripMenuItem.Click
        Try

            Call Me.ShowForm(frmSMS)

        Catch ex As Exception

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally

            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    'CR 239 [Asif Kamal]
    Private Sub ToolStripMenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem13.Click
        Try

            Call Me.ShowForm(frmProfitAndLossMonthWise)

        Catch ex As Exception
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    
    'Cr # 245
    Private Sub GLVoucherHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLVoucherHistoryToolStripMenuItem.Click
        Try

            Call Me.ShowForm(frmGLVoucherHistory)

        Catch ex As Exception
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub
    'Cr # 247
    Private Sub SeeAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuseeall.Click, mnuR1.Click, mnuR2.Click, mnuR3.Click, mnuR4.Click, mnuR5.Click
        Dim proc As New Process()
        Try
            Dim mnu As Windows.Forms.ToolStripMenuItem = CType(sender, Windows.Forms.ToolStripMenuItem)

            If Not mnu.Name = mnuseeall.Name Then

                With proc.StartInfo
                    .Arguments = mnu.Tag
                    .UseShellExecute = True
                    .WindowStyle = ProcessWindowStyle.Maximized
                    '.WorkingDirectory = "C:\Program Files\Adobe\Reader 9.0\Reader\" '<----- Set Acrobat Install Path
                    .FileName = "AcroRd32.exe" '<----- Set Acrobat Exe Name
                End With

            Else
                With proc.StartInfo
                    .Arguments = IO.Path.GetFullPath(IO.Path.Combine(gstrReportPath, "..\Release Notes"))
                    .UseShellExecute = True
                    .WindowStyle = ProcessWindowStyle.Maximized
                    .FileName = "explorer.exe"
                End With

            End If
            proc.Start()

        Catch ex As Exception
            If ex.Message = "The system cannot find the file specified" Then
                ShowErrorMessage("Adobe Reader either doesn't exist on your system or not properly installed. Try again after installing.")
            Else
                ShowErrorMessage(ex.Message)
            End If

        Finally
            proc.Close()
            proc.Dispose()
        End Try
    End Sub

    'Cr # 247
    Private Sub mnuReviewRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReviewRelease.DropDownOpening
        Try
            If Directory.Exists(IO.Path.GetFullPath(IO.Path.Combine(gstrReportPath, "..\Release Notes"))) Then
                Dim FolderInfo As New DirectoryInfo(IO.Path.GetFullPath(IO.Path.Combine(gstrReportPath, "..\Release Notes")))
                Dim FileInfoArrays As FileInfo()
                FileInfoArrays = FolderInfo.GetFiles("*.pdf")
                Array.Reverse(FileInfoArrays)
                mnuR1.Text = FileInfoArrays(0).Name.Substring(0, FileInfoArrays(0).Name.Length - 4)
                mnuR1.Tag = FileInfoArrays(0).FullName
                mnuR2.Text = FileInfoArrays(1).Name.Substring(0, FileInfoArrays(1).Name.Length - 4)
                mnuR2.Tag = FileInfoArrays(1).FullName
                mnuR3.Text = FileInfoArrays(2).Name.Substring(0, FileInfoArrays(2).Name.Length - 4)
                mnuR3.Tag = FileInfoArrays(2).FullName
                mnuR4.Text = FileInfoArrays(3).Name.Substring(0, FileInfoArrays(3).Name.Length - 4)
                mnuR4.Tag = FileInfoArrays(3).FullName
                mnuR5.Text = FileInfoArrays(4).Name.Substring(0, FileInfoArrays(4).Name.Length - 4)
                mnuR5.Tag = FileInfoArrays(4).FullName

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    '''Cr# 241 SMS Configuration Screen: A screen is required for sms configuration  
    Private Sub SMSConfigurationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMSConfigurationToolStripMenuItem.Click
        Try

            Call Me.ShowForm(frmSMSConfiguration)

        Catch ex As Exception
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub
    '''Cr# 251    
    Private Sub ContactDirectoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContactDirectoryToolStripMenuItem.Click
        Try

            Call Me.ShowForm(frmContectDirec)

        Catch ex As Exception
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub
#Region "SMS CR#254"
    Private Sub TmSMS_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TmSMS.Tick
        Try
            TmSMS.Enabled = False
            Me.BgwSMSSendingProcess.RunWorkerAsync()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BgwSMSSendingProcess_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BgwSMSSendingProcess.DoWork
        Try
            BackgroundProcessSendSMSMethod(objSMS.SMSSendToRecipients)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BgwSMSSendingProcess_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BgwSMSSendingProcess.RunWorkerCompleted
        Try
            ' 282
            '  TmSMS.Enabled = True
            If SMSException.ToString.Trim = "" Then
                TmSMS.Enabled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub BackgroundProcessSendSMSMethod(ByVal MyCollectionList As DataTable)
        'CR # 279
        Dim smsid As String = ""
        'Dim SMSException As String = "" ' 282
        Try
            For Each rowss As DataRow In MyCollectionList.Rows
                Dim responce As String = ""
                'CR # 279
                smsid = rowss.Item("SMS_ID").ToString
                'cr#265 by Farooq-H
                Dim phonenumber As String = rowss.Item("SMS_Number").ToString.Trim.Replace("-", "").Replace(" ", "").Replace(".", "")
                'CR # 279
                If phonenumber = "" Then
                    Continue For
                End If
                If phonenumber.Substring(0, 1) = "+" Then
                    phonenumber = phonenumber.Substring(1, phonenumber.Length - 1)
                    If phonenumber.Substring(0, 2) <> "92" Then
                        phonenumber = "92" & phonenumber
                    End If
                End If
                If phonenumber.Substring(0, 1) = "0" Then
                    phonenumber = phonenumber.Substring(1, phonenumber.Length - 1)
                    If phonenumber.Substring(0, 2) <> "92" Then
                        phonenumber = "92" & phonenumber
                    End If
                End If
                If phonenumber.Substring(0, 3) = "920" Then
                    phonenumber = "92" & phonenumber.Remove(0, 3)
                End If
                If phonenumber.Length = 11 And phonenumber.Substring(0, 3) <> "92" Then
                    phonenumber = "92" & phonenumber
                End If
                '                responce = SendSMS(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("SMSAPIUserName"), DecryptWithALP(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("SMSAPIPassword")), rowss.Item("SMS_Text").ToString, rowss.Item("SMS_Number").ToString.Trim.Replace("-", "").Replace(" ", "").Replace(".", ""), DAL.SystemConfigurationDAL.GetSystemConfigurationValue("BrandName"))
                responce = SendSMS(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("SMSAPIUserName"), DecryptWithALP(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("SMSAPIPassword")), rowss.Item("SMS_Text").ToString, phonenumber.ToString, DAL.SystemConfigurationDAL.GetSystemConfigurationValue("BrandName"))
                If responce.Contains("<type>Success</type>") Then
                    'objSMS.UpdateStatus(rowss.Item("SMS_ID").ToString.Trim)
                    objSMS.UpdateStatus(rowss.Item("SMS_ID").ToString.Trim, True)
                    '282
                ElseIf responce.Contains("<response>Insufficient Credit in account for this SMS.</response>") Then
                    ShowInformationMessage("SMS account credit has been expired, Please contact Lumensoft Technologies.")
                    SMSException = "InsufficientCredit"
                Else
                    objSMS.UpdateStatus(rowss.Item("SMS_ID").ToString.Trim, False)
                End If
            Next
        Catch ex As Exception
            'Throw ex
            'CR # 279
            If ex.Message.Trim.ToString = "Insufficient Credit in account for this SMS." Then
                SMSException = "InsufficientCredit"
                ShowErrorMessage("SMS account credit has been expired, Please contact Lumensoft Technologies.")
            Else
                objSMS.UpdateStatus(smsid.ToString.Trim, False)
            End If
        End Try

    End Sub
#End Region

    'CR # 264
    Private Sub ReIndexTablesDataBaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReIndexTablesDataBaseToolStripMenuItem.Click
        Try
            Call Me.ShowForm(frmReindex)
        Catch ex As Exception
            Me.Cursor = System.Windows.Forms.Cursors.Default
            ShowErrorMessage(ex.Message)
        Finally
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub
    ''CR # 267  Waqas Anwar  15-Aug-2013    Build a new utility to import lightwave data from souce to destination database.
    Private Sub DataTransferToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataTransferToolStripMenuItem.Click
        Try
            Call Me.ShowForm(frmDataTransfer)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            ''Set Cursor as Default
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub
    ''End CR # 267
End Class
