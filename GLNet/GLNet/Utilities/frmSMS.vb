''/////////////////////////////////////////////////////////////////////////////////////////
''//                      GL.NET
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Send SMS .. 
''// Programmer	     : Farooq-H
''// Creation Date	 : 15-MAy-2013
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
' 30-05-2013       farooq-H           CR# 242  SMS: Send sms from customer information screen. 
''19-jul-2013      Fatima Tajammal    CR# 258  Send Message: some changes require on this screen.
''12-sep-2013      Fatima Tajammal    CR# 272  Send SMS: System should accept diffrent mobile number formats to send sms.
' 04-10-2013       farooq-H           CR# 282  Send Message: System should prompt if SMS account is expired.
''/////////////////////////////////////////////////////////////////////////////////////////
Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports Utility.Utility
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web

Public Class frmSMS
    Implements IGeneral
#Region "Variables"
    ''This collection will hold the controls' names, upon which the logged in user has rights
    Private mobjControlList As NameValueCollection
    ''This is the model object which will be set with data values and refered for Save|Update|Delete|Loading Record in Edit Mode
    Private mobjModel As Company
    Private StartDate As DateTime
    Private EndDate As DateTime
    Private intPkId As Integer
    Private Dt As DataTable
    Private StrMobile As String
    'Fatima
    Dim count As Integer = 0
#End Region

#Region "Enumeration"
    Enum EnumGridHelp
        AccountCode = 1
        AccountName = 2
        ContactName = 3
        Type = 4
        EMail = 5
        Phone = 6
        Mobile = 7
        Address = 8
    End Enum
#End Region

#Region "Interface methodes "
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try


            If Mode.ToString = EnumDataMode.Disabled.ToString Then

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System           
                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(EnumDataMode.Edit)
                '      Me.grdAllRecords.Enabled = True

            ElseIf Mode.ToString = EnumDataMode.[New].ToString Then

                btnNew.Enabled = False ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System

                If mobjControlList.Item("btnSave") Is Nothing Then
                    btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System
                Else
                    btnSave.Enabled = True ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                End If

                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = True ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue

                SetNavigationButtons(Mode)

                '     Me.grdAllRecords.Enabled = False

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

                '     Me.grdAllRecords.Enabled = True

                '    Me.grdAllRecords.Focus()

            ElseIf Mode.ToString = EnumDataMode.ReadOnly.ToString Then

                btnNew.Enabled = True ': btnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
                btnSave.Enabled = False ': btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System


                btnUpdate.Enabled = False ': btnUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnDelete.Enabled = False ': btnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System
                btnCancel.Enabled = False ': btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System

                SetNavigationButtons(Mode)

                '     Me.grdAllRecords.Enabled = True

                '    Me.grdAllRecords.Focus()

            End If


            '' Disabl/Enable the Button that converts Grid data in Excel Sheet According to Login User rights
            If mobjControlList.Item("btnSendSMS") Is Nothing Then
                Me.btnSendSMS.Enabled = False
                '  Me.UiCtrlGridBar1.btnExport.Enabled = False
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            'Me.LstShops.DisplayMember = "Shop_Name"
            'Me.LstShops.ValueMember = "Shop_ID"
            'Dim dtShop As DataTable = New DAL.SMSDAL().GetShopList()
            'If dtShop.Rows(0).Item("Shop_Name") = gstrComboZeroIndexString Then
            '    dtShop.Rows.RemoveAt(0)
            'End If
            'Me.LstShops.DataSource = dtShop

        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try



            If Not Condition = "Filter Only" Then

                Dim StrSQL As New System.Text.StringBuilder

                StrSQL.AppendLine(" SELECT  tblGlCOAMainSubSubDetail.detail_code [Account Code], tblGlCOAMainSubSubDetail.detail_title [Account Name]  ,    ")
                StrSQL.AppendLine(" tblGLContactDirectory.Contact_person [Contact Person],  tblGlCOAMainSubSub.account_type [Type], tblGLContactDirectory.Email [Email] ,   ")
                StrSQL.AppendLine(" tblGLContactDirectory.Phone_office [Phone], tblGLContactDirectory.Mobile [Mobile], tblGLContactDirectory.Address [Address]   ")

                StrSQL.AppendLine(" FROM         tblGlCOAMainSubSub INNER JOIN   ")
                StrSQL.AppendLine("   tblGlCOAMainSubSubDetail ON tblGlCOAMainSubSub.main_sub_sub_id = tblGlCOAMainSubSubDetail.main_sub_sub_id    ")
                If ChkAll.Checked Then
                    StrSQL.AppendLine(" left outer JOIN   ")
                Else
                    StrSQL.AppendLine(" Inner JOIN   ")
                End If
                StrSQL.AppendLine(" tblGLContactDirectory ON tblGlCOAMainSubSubDetail.coa_detail_id = tblGLContactDirectory.Account_id  ")
                StrSQL.AppendLine(" WHERE     (tblGlCOAMainSubSub.account_type = 'Customer') OR   ")
                StrSQL.AppendLine("   (tblGlCOAMainSubSub.account_type = 'Vendor')   ")


                Dt = New DALReports().GetDataTable(StrSQL.ToString)
                Dt.TableName = "DTable"

            End If

            Dim strCriteria As New System.Text.StringBuilder
            strCriteria.AppendLine(" ")
            If Me.ComboBox1.SelectedIndex > 0 Then
                strCriteria.AppendLine(" [Type] = '" & Me.ComboBox1.Text.Replace("'", "''") & "' ")
            End If
            If Me.txtCustomerName.Text.Trim.Length > 0 Then
                If strCriteria.ToString.Trim.Length > 0 Then strCriteria.AppendLine(" and ")
                strCriteria.AppendLine(" [Contact Person] like '%" & Me.txtCustomerName.Text.Trim & "%' ")
            End If
 
            Me.GrdCustomerVenderDetails.DataSource = Nothing
            Me.GrdCustomerVenderDetails.DataSource = GetFilterDataFromDataTable(Dt, strCriteria.ToString)
            Call ApplyGridSettings()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Function IsValidate(Optional ByVal Mode As Utility.Utility.EnumDataMode = Utility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

        Try
            If Condition = "Mobile" Then
                If Not Me.StrMobile.Trim.Length > 0 Then
                    'CR # 258
                    If count > 0 Then
                        ShowValidationMessage("Invalid mobile number(s).See log of invalid mobile number(s) in NotSentSMSLog file.")
                        Me.txtCustomerName.Focus()
                        Return False
                    End If
                    'TODO: Message box string is hard coded
                    ShowValidationMessage("Please select the mobile number first.")
                    Me.txtCustomerName.Focus()
                    Return False
                ElseIf Me.txtSMSText.Text.Trim.Length <= 0 Then
                    'TODO: Message box string is hard coded
                    ShowValidationMessage("Please enter the message text.")
                    Me.txtSMSText.Focus()
                    Return False
                    'code commented against CR # 258
                    'ElseIf Me.txtSMSText.Text.Trim.Length > 160 Then
                    '    'TODO: Message box string is hard coded
                    '    ShowValidationMessage("Message length can not be greater then 160 words.")
                    '    Me.txtSMSText.Focus()
                    '    Return False
                End If

                Return True
                Exit Function
            End If

            
            'If Me.txtFile.Text.Trim.Length > 0 Then
            '    If System.IO.File.Exists(Me.txtFile.Text) Then

            '    End If
            'End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try

            'Me.txtFile.Text = String.Empty
            Me.txtSMSText.Text = Nothing
            Me.ComboBox1.SelectedIndex = 0
            Me.txtCustomerName.Text = String.Empty
            Me.GetAllRecords("Filter Only")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

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

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
#End Region

    Private Sub GrdCustomerVenderDetails_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrdCustomerVenderDetails.DoubleClick
       
        
    End Sub

    Private Sub GrdCustomerVenderDetails_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GrdCustomerVenderDetails.KeyUp
       
    End Sub
    Private Sub GrdCustomerVenderDetails_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrdCustomerVenderDetails.SelectionChanged

       
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            If Me.Dt Is Nothing Then Exit Sub
            Me.GetAllRecords("Filter Only")
        Catch ex As Exception
        End Try
    End Sub

     
     
    Private Sub frmSMS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ''Getting all available controls list to the user on this screen; in a collection 
            mobjControlList = GetFormSecurityControls(Me.Name)
            'filling combos
            Me.FillCombos()
            ' Me.grdHelp.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
            ''Set Default Values
            Me.ComboBox1.SelectedIndex = 0
            Me.txtCustomerName.Text = ""
            Me.GetAllRecords()
            SetButtonImages()
            
            Me.ApplySecurity(EnumDataMode.Disabled)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtCustomerName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomerName.TextChanged
        Try
            If Me.Dt Is Nothing Then Exit Sub
            Me.GetAllRecords("Filter Only")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSendSMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendSMS.Click
        Try

            Me.BuildMobile()
            If Not IsValidate(EnumDataMode.[New], "Mobile") Then Exit Sub
            If My.Computer.Network.IsAvailable = False Then
                MessageBox.Show("Please Connect to the Internet First . ", "Candela")
                Exit Sub
            End If
            Dim respose As String = SendSMS(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("SMSAPIUserName"), DecryptWithALP(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("SMSAPIPassword")), Me.txtSMSText.Text.Trim, StrMobile.Trim.Replace("-", "").Replace(" ", "").Replace(".", ""), DAL.SystemConfigurationDAL.GetSystemConfigurationValue("BrandName"))
            If respose.Contains("<type>Success</type>") Then
                'Cr # 258
                Dim strMblNum As String() = StrMobile.Split(",")
                If strMblNum.Length > 0 Then
                    ShowInformationMessage("Mobile text message has been sent.")
                End If
                '282
            ElseIf respose.Contains("<response>Insufficient Credit in account for this SMS.</response>") Then
                ShowInformationMessage("SMS account credit has been expired, Please contact Lumensoft Technologies.")
            Else
                ShowInformationMessage("Please try again! Message not sent Succefully .")
            End If
            'CR # 258
            If count > 0 Then
                ShowInformationMessage("Invalid mobile number(s).see log of invalid mobile number(s) in NotSentSMSLog file ")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    ' change by farooq-H  242 SMS: Send sms from customer information screen. 
    'Public Function SendSMS(ByVal USErName As String, ByVal password As String, ByVal MessageText As String, ByVal number As String, ByVal CompanyName As String) As String
    '    Try

    '        Dim url As String = "http://www.sms4connect.com/api/sendsms.php/sendsms/url"
    '        Dim result As String = ""
    '        'String message = HttpUtility.UrlEncode("Hello this is a test with a 5 note and an ampersand (&) symbol,send using API");
    '        Dim message As [String] = HttpUtility.UrlEncode(MessageText.ToString)
    '        'String strPost = "id=923467999797&pass=lumen123&msg=" + message + "&to=923344004704" + "&mask=Lumensoft&type=xml&lang=English";
    '        Dim strPost As [String] = "id=" & USErName.ToString & "&pass=" & password.ToString & "&msg=" & message & "&to=" & number & "&mask=" & CompanyName.ToString & "&type=xml&lang=English"
    '        Dim myWriter As StreamWriter = Nothing
    '        Dim objRequest As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
    '        objRequest.Method = "POST"
    '        objRequest.ContentLength = Encoding.UTF8.GetByteCount(strPost)
    '        objRequest.ContentType = "application/x-www-form-urlencoded"
    '        Try
    '            myWriter = New StreamWriter(objRequest.GetRequestStream())
    '            myWriter.Write(strPost)
    '        Catch e As Exception
    '            Return e.Message
    '        Finally
    '            myWriter.Close()
    '        End Try

    '        Dim objResponse As HttpWebResponse = DirectCast(objRequest.GetResponse(), HttpWebResponse)
    '        Using sr As New StreamReader(objResponse.GetResponseStream())
    '            ' Close and clean up the StreamReader sr.Close(); } return result; }
    '            result = sr.ReadToEnd()
    '        End Using


    '        Return result

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Public Sub BuildMobile()
        Try
            'Cr # 258
            If System.IO.File.Exists(gstrReportPath & "/.." & "\NotSentSMSLOG.txt") Then
                System.IO.File.Delete(gstrReportPath & "/.." & "\NotSentSMSLOG.txt")

            End If
            Dim fs As New IO.FileStream(gstrReportPath & "/.." & "\SentSMSLog.txt", IO.FileMode.Create, IO.FileAccess.ReadWrite)
            fs.Flush()
            fs.Close()
            fs.Dispose()

            StrMobile = String.Empty
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.GrdCustomerVenderDetails.GetCheckedRows
                If r.Cells(EnumGridHelp.Mobile).Text.Length > 0 Then
                    Dim Str As String = r.Cells(EnumGridHelp.Mobile).Text
                    Dim strtemp As String = Str.Substring(0, 2)
                    'CR # 258

                    'Code commented against CR # 272
                    'If Str.Length <> 12 Or strtemp <> "92" Then
                    '    count = count + 1
                    '    Dim sw As StreamWriter
                    '    sw = File.AppendText(gstrReportPath & "/.." & "\NotSentSMSLOG.txt")
                    '    sw.WriteLine(r.Cells(EnumGridHelp.Mobile).Text)
                    '    sw.Close()
                    'Else
                    '    Str = Replace(Replace(Str, ".", ""), " ", "")
                    '    StrMobile = StrMobile & r.Cells(EnumGridHelp.Mobile).Text & ", "
                    'End If

                    'Str = Replace(Replace(Str, ".", ""), " ", "")
                    'StrMobile = StrMobile & r.Cells(EnumGridHelp.Mobile).Text & ", "

                    'CR # 272
                    Dim phonenumber As String = Str.Trim.Replace("-", "").Replace(" ", "").Replace(".", "")
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
                    If phonenumber.Length = 11 And phonenumber.Substring(0, 2) <> "92" Then
                        phonenumber = "92" & phonenumber
                    End If
                    If phonenumber.Length < 12 Or phonenumber.Substring(0, 3) <> "923" Then
                        count = count + 1
                        Dim sw As System.IO.StreamWriter
                        sw = System.IO.File.AppendText(gstrReportPath & "/.." & "\NotSentSMSLOG.txt")
                        sw.WriteLine(r.Cells(EnumGridHelp.Mobile).Text)
                        sw.Close()
                        Continue For
                    End If

                    StrMobile = StrMobile & phonenumber & ", "

                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    
    Private Sub LstShops_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            If Me.Dt Is Nothing Then Exit Sub
            Me.GetAllRecords("Filter Only")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            If Me.btnExit.Enabled = True Then
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click, btnPrevious.Click, btnNext.Click, btnLast.Click

        Try

            Dim btn As Button = CType(sender, Button)

            ''If Move First is clicked, then navigate to first record of the grid
            If btn.Name = Me.btnFirst.Name Then
                'Me.grdVoucher.Row = 0
                Me.GrdCustomerVenderDetails.MoveFirst()

                ''If Move Previous is clicked, then navigate to Previous record of the grid
            ElseIf btn.Name = Me.btnPrevious.Name Then
                'If Me.grdVoucher.Row > 0 Then Me.grdVoucher.Row = (Me.grdVoucher.Row - 1)
                Me.GrdCustomerVenderDetails.MovePrevious()

                ''If Move Next is clicked, then navigate to next record of the grid
            ElseIf btn.Name = Me.btnNext.Name Then
                'If Me.grdVoucher.Row >= 0 Then Me.grdVoucher.Row = (Me.grdVoucher.Row + 1)
                Me.GrdCustomerVenderDetails.MoveNext()


                ''If Move Last is clicked, then navigate to Last record of the grid
            ElseIf btn.Name = Me.btnLast.Name Then
                'Me.grdVoucher.Row = (Me.grdVoucher.RowCount - 1)
                Me.GrdCustomerVenderDetails.MoveLast()

            End If



        Catch ex As Exception

        End Try

    End Sub

    Private Sub ChkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAll.CheckedChanged
        Try
            Me.GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BtnLoadtxtFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLoadtxtFile.Click
        Try
            Me.OpenFileDialog1.DefaultExt = ".xls"
            Me.OpenFileDialog1.Filter = "Excel Files|*.xls|Text Files (*.CSV,*.txt)|*.CSV;*.txt"
            Me.OpenFileDialog1.FileName = ""

            If Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If Me.OpenFileDialog1.FilterIndex = 1 Then
                    Me.proReadExcelFile()
                Else
                    Me.proReadCSVFile()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub proReadCSVFile(Optional ByVal Condition As String = "Mobile")

        Try
            Dim strLine() As String
            Dim strArray() As String
            Dim dr As DataRow

            strArray = IO.File.ReadAllLines(Me.OpenFileDialog1.FileName)
            Me.Cursor = Cursors.WaitCursor

            Dim myData As New DataTable("Product")
            Dim colData As New DataColumn(Condition, GetType(String))
            myData.Columns.Add(Condition)
            '*********CR # 258
            For i As Integer = 0 To strArray.Length - 1
                'For i As Integer = 1 To strArray.Length - 1
                '******End of CR # 258
                Application.DoEvents()

                strLine = strArray(i).Split(",")
                If strLine(0).ToString <> "," And strLine(0).ToString <> String.Empty Then

                    dr = myData.NewRow
                    dr(Condition) = strLine(0).ToString

                    myData.Rows.Add(dr)

                End If

                Application.DoEvents()
            Next

            Me.GrdCustomerVenderDetails.DataSource = myData
            Me.ApplyGridSettings()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Public Sub proReadExcelFile(Optional ByVal Condition As String = "Mobile")
        Dim cnn As System.Data.OleDb.OleDbConnection

        Try

            Dim sConn As String = String.Empty
            Dim strExcelPath As String = String.Empty
            Dim lngTotalRows As Long = 0L
            lngTotalRows = 0


            Me.Cursor = Cursors.WaitCursor

            If InStr(1, Me.OpenFileDialog1.FileName, ".", vbTextCompare) Then
                Me.OpenFileDialog1.FileName = Mid(Me.OpenFileDialog1.FileName, 1, InStr(1, Me.OpenFileDialog1.FileName, ".", vbTextCompare) - 1)
            End If

            If Trim(Me.OpenFileDialog1.FileName) = "" Or Trim(Me.OpenFileDialog1.FileName) = "*" Then
                Exit Sub
            End If

            strExcelPath = Me.OpenFileDialog1.FileName


            sConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strExcelPath & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1;"""

            cnn = New System.Data.OleDb.OleDbConnection(sConn)
            cnn.Open()

            Dim dt As DataTable = cnn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, _
                                       New Object() {Nothing, Nothing, Nothing, "TABLE"})


            Dim strSheetName As String = dt.Rows(0)!TABLE_NAME.ToString


            'Getting Data from Execl Sheet
            Dim da As New OleDb.OleDbDataAdapter("SELECT DISTINCT *  FROM [" & strSheetName & "]", cnn)

            Dim myData As New DataTable("Product")
            da.Fill(myData)
            myData.Columns(0).ColumnName = "Mobile"

            Me.GrdCustomerVenderDetails.DataSource = myData
            'Me.grdHelp.RetrieveStructure()
            Me.ApplyGridSettings()

        Catch ex As Exception
            Throw ex
            Me.BtnLoadtxtFile_Click(Nothing, Nothing)
        Finally
            Me.Cursor = Cursors.Default
            cnn.Close()
        End Try

    End Sub

    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click
        Try
            Dim strBuilder As New System.Text.StringBuilder
            strBuilder.AppendLine("Mobile number must contain Country code i.e 923216957643 ")
            Help.ShowPopup(Me.btnHelp, strBuilder.ToString, New System.Drawing.Point(Me.btnHelp.Location.X - 140, 140))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class