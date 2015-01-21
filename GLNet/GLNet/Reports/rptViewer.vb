'' 01-06-2012  farooq ul hassan   CR#207   Crystal Reports: All buttons get disabled and whole data disappears on refreshing the crystal report
Imports Utility.Utility
Imports System.IO
Imports Model
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class rptViewer

    Dim objArrlistParamter As ArrayList
    Dim objHashTableParamter As Hashtable
    Dim objReportViewOption As Hashtable
    Dim intLoopIndex As Integer
    Dim strCriteriaList As String
    Dim strFilename As String
    Dim StrEMails As String

    Private Sub funShowReport(ByRef rptPath As String, Optional ByVal strRecordSelection As String = "")
        'Try

        Dim objCr As New ReportDocument


        ''Getting Login Information
        Dim strUserName As String = System.Configuration.ConfigurationManager.AppSettings("StrUserName")
        Dim strUserPassword As String = System.Configuration.ConfigurationManager.AppSettings("StrPassword")
        Dim strDBServerName As String = System.Configuration.ConfigurationManager.AppSettings("StrDBServerName")
        Dim strDBName As String = System.Configuration.ConfigurationManager.AppSettings("StrDBName")
        Dim strReportPath As String = System.Configuration.ConfigurationManager.AppSettings("ReportPath")

        If Not strUserName.Contains("sa") Then
            strUserName = Utility.Utility.SymmetricEncryption.Decrypt(strUserName, "f")
        End If

        If Not strUserPassword.Contains("lumensoft") Then
            strUserPassword = Utility.Utility.SymmetricEncryption.Decrypt(strUserPassword, "f")
        End If

        strReportPath = gstrReportPath

        Dim crDatabase As Database
        Dim crTables As Tables
        Dim crTable As Table
        Dim crTableLogOnInfo As TableLogOnInfo
        Dim crConnectionInfo As ConnectionInfo

        'Create an instance of the strongly-typed report object

        'Setup the connection information structure to be used
        'to log onto the datasource for the report.
        crConnectionInfo = New ConnectionInfo

        objCr.Load(strReportPath & rptPath, strDBServerName)

        objCr.DataSourceConnections.Item(0).SetConnection(strDBServerName, strDBName, False, strUserPassword)
        objCr.DataSourceConnections.Item(0).SetLogon(strUserName, strUserPassword)

        If Not (objHashTableParamter.Item("DataTable")) Is Nothing Then
            objCr.SetDataSource(Utility.Utility.gReportDataTable)
        End If


        With crConnectionInfo
            .UserID = strUserName
            .Password = strUserPassword
            .ServerName = strDBServerName
            .DatabaseName = strDBName

        End With

        'Get the table information from the report
        crDatabase = objCr.Database
        crTables = crDatabase.Tables

        'Loop through all tables in the report and apply the connection
        'information for each table.

        For Each crTable In crTables
            crTableLogOnInfo = crTable.LogOnInfo
            crTableLogOnInfo.ConnectionInfo = crConnectionInfo
            crTable.ApplyLogOnInfo(crTableLogOnInfo)

        Next

        Dim objParamterContainer As ParameterFieldDefinitions
        'This object variable is used to hold the name of the parameter
        Dim objParmName As ParameterFieldDefinition
        'Hold all values of the paramete
        Dim objParmValues As New ParameterValues
        'hold current value of the parametere
        Dim objParmCurrentValue As New ParameterDiscreteValue

        Dim strParams As String

        For Each strParams In objHashTableParamter.Keys

            ''Export and print rights also set in hashtable so we check it in if condition.
            If (strParams <> "ReportPath") And (strParams <> "ExportRights") And (strParams <> "PrintRights") And (strParams <> "DataTable") Then

                objParmCurrentValue.Value = objHashTableParamter.Item(strParams)
                ''Getting the reference of the parameter added on the reports
                objParamterContainer = objCr.DataDefinition.ParameterFields
                'Setting the index of the paramter
                objParmName = objParamterContainer.Item(strParams)
                'Adding the value to the parameter
                objParmValues.Add(objParmCurrentValue)
                objParmName.ApplyCurrentValues(objParmValues)

            End If
        Next

        If Len(strRecordSelection.Trim) > 0 Then

            objCr.RecordSelectionFormula = strRecordSelection

        End If

      

        If Not objReportViewOption Is Nothing Then

            If Not objReportViewOption.Item("Left Margin") Is Nothing Then
                CR.Left = (objReportViewOption("Left Margin") * 2.5 * 567)
            End If
            If Not objReportViewOption.Item("Right Margin") Is Nothing Then
                'CR.Right = (objReportViewOption("Right Margin") * 2.5 * 567)
            End If
            If Not objReportViewOption.Item("Top Margin") Is Nothing Then
                CR.Top = (objReportViewOption("Top Margin") * 2.5 * 567)
            End If

        End If

        If Not objHashTableParamter.Item("ShowHeader") Is Nothing Then


            Dim strShowHeader As String = CStr(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Show_Report_Header"))
            Dim strShowAddress As String = CStr(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Show_Report_Address"))

            objCr.SetParameterValue("ShowAddress", IIf(strShowAddress = "", True, strShowAddress), "ReportHeader.rpt")
            objCr.SetParameterValue("strCompanyName", CStr(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Invoice_Company_Name")), "ReportHeader.rpt")
            objCr.SetParameterValue("strCompanyAddress", CStr(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Company_Address")), "ReportHeader.rpt")

        End If
        'Dim crDatabaseFieldDefinition As CrystalDecisions.CrystalReports.Engine.DatabaseFieldDefinition
        'crDatabaseFieldDefinition = objCr.Database.Tables.Item(0).Fields.Item("LastName")
        'objCr.DataDefinition.SortFields.Item(0).Field = crDatabaseFieldDefinition
        'objCr.DataDefinition.SortFields(0).SortDirection = CrystalDecisions.Shared.SortDirection.DescendingOrder


        'objCr.DataDefinition.SortFields().SortDirection
        If Not objReportViewOption Is Nothing Then
            If Not objReportViewOption.Item("Print Option") Is Nothing Then
                'CR.ReportSource = objCr
                'CR.PrintReport()
                objCr.PrintToPrinter(1, False, 1, 1)
                Me.Close()
            Else
                CR.ReportSource = objCr
            End If
        Else
            CR.ReportSource = objCr

        End If


        CR.ShowExportButton = CType(objHashTableParamter.Item("ExportRights"), Boolean)
        CR.ShowPrintButton = CType(objHashTableParamter.Item("PrintRights"), Boolean)

        Me.lblEmail.Enabled = True

        ''''Exporting report to PDF
        'Dim ObjExport As System.IO.MemoryStream = CType(objCr.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat), MemoryStream)

        'With HttpContext.Current.Response
        '    .ClearContent()
        '    .ClearHeaders()
        '    .ContentType = "application/pdf"
        '    .AddHeader("Content-Disposition", "inline; filename=Report.pdf")
        '    .BinaryWrite(ObjExport.ToArray)
        '    .End()

        'End With

        'Catch ex As Exception
        '    'If ex.Message = "Logon failed." Then
        '    Session.Add("ErrorMsg", ex.Message)
        '    'End If

        'End Try

    End Sub

    Private Sub rptViewer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        strCriteriaList = CStr(Utility.Utility.gObjMyAppHashTable(Utility.Utility.EnumHashTableKeyConstants.SetReportCriteria))
        objHashTableParamter = CType(Utility.Utility.gObjMyAppHashTable(Utility.Utility.EnumHashTableKeyConstants.SetReportParametersList), Hashtable)
        objReportViewOption = CType(Utility.Utility.gObjMyAppHashTable(Utility.Utility.EnumHashTableKeyConstants.ReportViewOption), Hashtable)


        Utility.Utility.gObjMyAppHashTable.Remove(Utility.Utility.EnumHashTableKeyConstants.SetReportParametersList)
        Utility.Utility.gObjMyAppHashTable.Remove(Utility.Utility.EnumHashTableKeyConstants.ReportViewOption)


        If Not strCriteriaList Is Nothing Then
            funShowReport(CType(objHashTableParamter.Item("ReportPath"), String), strCriteriaList)

        Else
            funShowReport(CType(objHashTableParamter.Item("ReportPath"), String))

        End If
        Call ReSetControls()


    End Sub

    Public Function AddCompanyInfoParam() As ParameterFields

        Dim strShowHeader As String = CStr(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Show_Report_Header"))
        Dim strShowAddress As String = CStr(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Show_Report_Address"))

        Dim ReportParameters As ParameterFields = New ParameterFields
        Dim ReportParameter As ParameterField = New ParameterField
        Dim ParamValue As ParameterDiscreteValue = New ParameterDiscreteValue

        ReportParameter.ParameterFieldName = "ShowHeader"
        ParamValue.Value = IIf(strShowHeader = "", True, strShowHeader)
        ReportParameter.CurrentValues.Add(ParamValue)
        ReportParameters.Add(ReportParameter)

        ReportParameter.ParameterFieldName = "ShowAddress"
        ParamValue.Value = IIf(strShowAddress = "", True, strShowAddress)
        ReportParameter.CurrentValues.Add(ParamValue)
        ReportParameters.Add(ReportParameter)

        ReportParameter.ParameterFieldName = "strCompanyName"
        ParamValue.Value = CStr(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Invoice_Company_Name"))
        ReportParameter.CurrentValues.Add(ParamValue)
        ReportParameters.Add(ReportParameter)

        ReportParameter.ParameterFieldName = "strCompanyAddress"
        ParamValue.Value = CStr(DAL.SystemConfigurationDAL.GetSystemConfigurationValue("Company_Address"))
        ReportParameter.CurrentValues.Add(ParamValue)
        ReportParameters.Add(ReportParameter)

        Return ReportParameters

    End Function

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblEmail.LinkClicked

        Try

            ShowProgressBar()
            Application.DoEvents()
            Me.ExportToPDF()
            Application.DoEvents()
            Me.GroupBox1.Visible = True
            Application.DoEvents()
            Me.txtEmailTo.Focus()
            Application.DoEvents()
            Me.txtSubject.Text = Me.Text
            Application.DoEvents()
            Me.txtMessageBody.Text = Me.Text & " as on " & Date.Now
            Application.DoEvents()
            EndProgressBar()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            EndProgressBar()
        End Try
    End Sub

    Sub ExportToPDF()

        Dim CrReport As New ReportDocument

        CrReport = CR.ReportSource
        Dim CrExportOptions As ExportOptions

        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()

        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()


        strFilename = "c:\" & Me.Text & ".pdf"
        CrDiskFileDestinationOptions.DiskFileName = strFilename



        CrExportOptions = CrReport.ExportOptions



        With CrExportOptions

            .ExportDestinationType = ExportDestinationType.DiskFile


            .ExportFormatType = ExportFormatType.PortableDocFormat

            .DestinationOptions = CrDiskFileDestinationOptions

            .FormatOptions = CrFormatTypeOptions

        End With




        Try

            CrReport.Export()

        Catch err As Exception

            MessageBox.Show(err.ToString())

        End Try

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.ReSetControls()
    End Sub

    Public Sub ReSetControls(Optional ByVal Condition As String = "")
        Try

            Me.txtEmailTo.Text = String.Empty
            Me.txtSubject.Text = String.Empty
            Me.txtMessageBody.Text = Nothing
            Me.StrEMails = String.Empty
            Me.GroupBox1.Visible = False

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub btnSendMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendMail.Click
        Me.SendEmail()
    End Sub


    Sub SendEmail()

        Try
            Application.DoEvents()
            Me.BuildEmail()
            Application.DoEvents()
            If Not IsValidate() = True Then Application.DoEvents() : Exit Sub : EndProgressBar()
            Application.DoEvents()
            Dim result As DialogResult = Windows.Forms.DialogResult.Yes

            result = MessageBox.Show("Do you want to send email?", "GL", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, False)
            If result = Windows.Forms.DialogResult.Yes Then
                ShowProgressBar()
                Application.DoEvents()
                SendMail(MailSender, "", Me.StrEMails, Me.txtSubject.Text, Me.txtMessageBody.Text, IIf(Me.strFilename.Trim.Length > 0, Me.strFilename.Trim, ""), False)
                Application.DoEvents()
                EndProgressBar()
                Application.DoEvents()
                ShowInformationMessage("E-mail has been sent successfully")
                Me.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            'ShowInformationMessage("Email Configurations missing or there is problem with your STMP server")
        Finally
            EndProgressBar()
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As EnumDataMode = EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean

        Try

            If Not Me.StrEMails.Trim.Length > 0 Then
                'TODO: Message box string is hard coded
                ShowValidationMessage("Please enter email")
                Me.txtEmailTo.Focus()
                Return False

            ElseIf Me.txtSubject.Text.Trim.Length <= 0 Then

                'TODO: Message box string is hard coded
                ShowValidationMessage("Please enter subject")
                Me.txtSubject.Focus()
                Return False
            ElseIf Me.txtMessageBody.Text.Trim.Length <= 0 Then
                'TODO: Message box string is hard coded
                ShowValidationMessage("Please enter message body")
                Me.txtMessageBody.Focus()
                Return False
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

    Public Sub BuildEmail()

        'StrEMails = String.Empty
        'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdHelp.GetCheckedRows
        '    If Regex.IsMatch(r.Cells(EnumGridHelp.EMail).Text, "^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$") Then
        '        'If r.Cells(EnumGridHelp.EMail).Text.Trim.Length > 0 Then
        '        StrEMails = StrEMails & r.Cells(EnumGridHelp.EMail).Text & "; "
        '    End If
        'Next
        StrEMails = Me.txtEmailTo.Text

    End Sub
    ' change by farooq ul hassan   Cr# 207
    Private Sub CR_ReportRefresh(ByVal source As Object, ByVal e As CrystalDecisions.Windows.Forms.ViewerEventArgs) Handles CR.ReportRefresh
        Try
            e.Handled = True
            CR.ReportSource.Refresh()

        Catch ex As Exception

        End Try
    End Sub
End Class