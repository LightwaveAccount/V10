Imports DAL
Imports System.Data
Imports System.Data.SqlClient

Public Class frmSchemaSnapshot
    Implements IGeneral


    Private Sub frmPartialSchema_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim dt As DataTable = New DAL.CompanyDAL().GetAll()
            Me.lstShops.ListItem.ValueMember = "Company ID"
            Me.lstShops.ListItem.DisplayMember = "Company Name"
            Me.lstShops.ListItem.DataSource = dt


            Me.dtpGLLogDateTo.Enabled = False
            Me.dtpGLLogFrom.Enabled = False

            Me.dtpVoucherDateFrom.Enabled = True
            Me.dtpVoucherDateTo.Enabled = True

            Me.SetButtonImages()

            btnFirst.Enabled = False
            btnPrevious.Enabled = False
            btnNext.Enabled = False
            btnLast.Enabled = False


            btnNew.Enabled = False
            btnSave.Enabled = False
            btnUpdate.Enabled = False
            btnDelete.Enabled = False
            btnCancel.Enabled = False

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub chkSales_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGLSQLLog.CheckedChanged
        Try
            Dim chk As CheckBox = CType(sender, CheckBox)
            Select Case chk.Name
                Case Me.chkGLSQLLog.Name
                    Me.dtpGLLogDateTo.Enabled = Me.chkGLSQLLog.Checked
                    Me.dtpGLLogFrom.Enabled = Me.chkGLSQLLog.Checked
            End Select
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmDefArea_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try

            If e.Control And e.KeyCode = Keys.X Then
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click

        If Me.lstShops.SelectedIDs.Length = 0 Then
            ShowValidationMessage("Select company")
            Exit Sub
        End If

        Dim frmPrgBar As Utility.frmDualProgressBar
        Dim con As SqlConnection
        Dim DestinationConnection As SqlConnection
        Dim bulkcopier As SqlBulkCopy
        Dim IsReplicationDone As Boolean = False

        Try
            frmPrgBar = Utility.Utility.ShowDualProgressBar_Dynamic(Me.Name, 2, "Droping database if already exists")
            frmPrgBar.PBMain.Maximum = 7
            frmPrgBar.lblMain.Text = "Creating database""SchemaSnapshot"""
            Application.DoEvents()
            Dim strSQL As New System.Text.StringBuilder()

            ''Drop database if already exists
            strSQL.AppendLine(" IF  EXISTS ( ")
            strSQL.AppendLine("             Select name ")
            strSQL.AppendLine(" FROM sys.databases ")
            strSQL.AppendLine(" WHERE Name = N'SchemaSnapshot' ")
            strSQL.AppendLine(" ) ")
            strSQL.AppendLine(" Begin ")
            strSQL.AppendLine(" DECLARE @DatabaseName nvarchar(50) ")
            strSQL.AppendLine(" SET @DatabaseName = 'SchemaSnapShot' ")
            strSQL.AppendLine(" DECLARE @SQL varchar(max) ")
            strSQL.AppendLine(" SET @SQL = '' ")
            strSQL.AppendLine(" SELECT @SQL = @SQL + 'Kill ' + Convert(varchar, SPId) + ';' ")
            strSQL.AppendLine(" FROM MASTER..SysProcesses ")
            strSQL.AppendLine(" WHERE DBId = DB_ID(@DatabaseName) AND SPId <> @@SPId ")
            strSQL.AppendLine(" EXEC(@SQL) ")
            strSQL.AppendLine(" DROP DATABASE SchemaSnapshot ")
            strSQL.AppendLine(" END ")

            SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, strSQL.ToString(), Nothing)
            Application.DoEvents()
            frmPrgBar.PB.Value += 1

            'create database
            frmPrgBar.lblStatus.Text = "Creating database""SchemaSnapshot"""
            strSQL = New System.Text.StringBuilder()
            strSQL.AppendLine("CREATE DATABASE SchemaSnapshot")
            SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, strSQL.ToString(), Nothing)
            Application.DoEvents()
            frmPrgBar.PB.Value += 1
            frmPrgBar.PBMain.Value += 1

            ''create new connection string of newly created database to create views and sps on it
            con = New SqlConnection(SQLHelper.CON_STR)
            Dim bilder As New SqlConnectionStringBuilder
            bilder.DataSource = con.DataSource
            bilder.InitialCatalog = "SchemaSnapshot"

            Dim strUserName As String = System.Configuration.ConfigurationManager.AppSettings("StrUserName")
            Dim strUserPassword As String = System.Configuration.ConfigurationManager.AppSettings("StrPassword")

            If Not strUserName.Contains("sa") Then
                strUserName = Utility.Utility.SymmetricEncryption.Decrypt(strUserName, "f")
            End If

            If Not strUserPassword.Contains("lumensoft") Then
                strUserPassword = Utility.Utility.SymmetricEncryption.Decrypt(strUserPassword, "f")
            End If
            bilder.UserID = strUserName
            bilder.Password = strUserPassword

            ''create Destination Connection
            DestinationConnection = New SqlConnection(bilder.ToString())

            ''create sql bulk copier
            bulkcopier = New SqlBulkCopy(DestinationConnection)

            ''create table for storing data 
            Dim dtTableData As DataTable

            DestinationConnection.Open()

            Application.DoEvents()
            frmPrgBar.lblMain.Text = "Transfering Configuration Tables"
            frmPrgBar.PB.Maximum = 158

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tmpTblGLAccountsOpening"
            'Copy tmpTblGLAccountsOpening
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tmpTblGLAccountsOpening from tmpTblGLAccountsOpening  ", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Try
                Application.DoEvents()
                frmPrgBar.lblStatus.Text = "Copying tblGLSecurityControlRight"
                'Copy tblGLSecurityControlRight
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGLSecurityControlRight from tblGLSecurityControlRight", Nothing)
                frmPrgBar.PB.Value += 1
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try

            Try
                Application.DoEvents()
                frmPrgBar.lblStatus.Text = "Copying tblGLSecurityFormControl"
                'Copy tblGLSecurityFormControl
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGLSecurityFormControl from tblGLSecurityFormControl", Nothing)
                frmPrgBar.PB.Value += 1
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try


            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGlVoucherTemp"
            'Copy tblGlVoucherTemp
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGlVoucherTemp from tblGlVoucherTemp  ", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGlVoucherTempDetail"
            'Copy tblGlVoucherTempDetail
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGlVoucherTempDetail from tblGlVoucherTempDetail  ", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGLSupplier_Payments"
            'Copy tblGLSupplier_Payments
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGLSupplier_Payments from tblGLSupplier_Payments  ", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGLSupplier_Payments_Detail"
            'Copy tblGLSupplier_Payments_Detail
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGLSupplier_Payments_Detail from tblGLSupplier_Payments_Detail  ", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGLCustomer_Receipts"
            'Copy tblGLCustomer_Receipts
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGLCustomer_Receipts from tblGLCustomer_Receipts  ", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGLCustomer_Receipts_Detail"
            'Copy tblGLCustomer_Receipts_Detail
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGLCustomer_Receipts_Detail from tblGLCustomer_Receipts_Detail ", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGLConfiguration"
            'Copy tblGLConfiguration
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGLConfiguration from tblGLConfiguration ", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGLComputerList"
            'Copy tblGLComputerList
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGLComputerList from tblGLComputerList  ", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGLActivityLog"
            'Copy tblGLActivityLog
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGLActivityLog from tblGLActivityLog  ", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1


            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblCashBankClosing"
            'Copy tblCashBankClosing
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblCashBankClosing from tblCashBankClosing ", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblBSDummyTable"
            'Copy tblBSDummyTable
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblBSDummyTable from tblBSDummyTable  ", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1



            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying cboGlVoucherType"
            'Copy cboGlVoucherType
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.cboGlVoucherType from cboGlVoucherType ", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblAccountShopBAB"
            'Copy tblAccountShopBAB
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblAccountShopBAB from tblAccountShopBAB  ", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblsqllog"
            'Copy tblsqllog
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblsqllog from tblsqllog   ", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGlCOAMain"
            'Copy tblGlCOAMain
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGlCOAMain from tblGlCOAMain", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGlCOAMainSub"
            'Copy tblGlCOAMainSub
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGlCOAMainSub from tblGlCOAMainSub", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGlCOAMainSubSub"
            'Copy tblGlCOAMainSubSub
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGlCOAMainSubSub from tblGlCOAMainSubSub", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGlCOAMainSubSubDetail"
            'Copy tblGlCOAMainSubSubDetail
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGlCOAMainSubSubDetail from tblGlCOAMainSubSubDetail", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGlDefFinancialYear"
            'Copy tblGlDefFinancialYear
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGlDefFinancialYear from tblGlDefFinancialYear", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGlDefFinancialYearStatus"
            'Copy tblGlDefFinancialYearStatus
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGlDefFinancialYearStatus from tblGlDefFinancialYearStatus", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGlDefGLCostCenter"
            'Copy tblGlDefGLCostCenter
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGlDefGLCostCenter from tblGlDefGLCostCenter", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGlDefGLNotes"
            'Copy tblGlDefGLNotes
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGlDefGLNotes from tblGlDefGLNotes", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGlDefLocation"
            'Copy tblGlDefLocation
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGlDefLocation from tblGlDefLocation", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGlDefVoucherType"
            'Copy tblGlDefVoucherType
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGlDefVoucherType from tblGlDefVoucherType", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGLSecurityForm"
            'Copy tblGLSecurityForm
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGLSecurityForm from tblGLSecurityForm", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGLSecurityGroup"
            'Copy tblGLSecurityGroup
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGLSecurityGroup from tblGLSecurityGroup", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGLSecurityUser"
            'Copy tblGLSecurityUser
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGLSecurityUser from tblGLSecurityUser", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGLSecurityUserLocation"
            'Copy tblGLSecurityUserLocation
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, "select * into SchemaSnapShot.dbo.tblGLSecurityUserLocation from tblGLSecurityUserLocation", Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1
            frmPrgBar.PBMain.Value += 1

            frmPrgBar.PB.Maximum = 3
            frmPrgBar.PB.Value = 0
            If Me.chkGLSQLLog.Checked Then

                frmPrgBar.PBMain.Maximum = 8

                Application.DoEvents()
                frmPrgBar.lblStatus.Text = "Copying tblGLDMLLog"
                frmPrgBar.lblMain.Text = "GL SQL Log"
                'Copy tblGLDMLLog
                strSQL = New System.Text.StringBuilder
                strSQL.AppendLine("select * into SchemaSnapShot.dbo.tblGLDMLLog from tblGLDMLLog where 1=1")
                If Me.dtpGLLogFrom.Checked Then
                    strSQL.AppendLine("and log_date >='" & Me.dtpGLLogFrom.Value.Date.ToString("yyyy-MM-dd") & "'")
                End If
                If Me.dtpGLLogDateTo.Checked Then
                    strSQL.AppendLine("and log_date <='" & Me.dtpGLLogDateTo.Value.Date.ToString("yyyy-MM-dd") & "'")
                End If
                Try
                    SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, strSQL.ToString, Nothing)
                Catch ex As Exception
                    ShowErrorMessage(ex.Message)
                End Try
                frmPrgBar.PB.Value += 1
                frmPrgBar.PBMain.Value += 1
            End If

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGLVoucher"
            'Copy tblGLVoucher
            strSQL = New System.Text.StringBuilder
            strSQL.AppendLine("select * into SchemaSnapShot.dbo.tblGLVoucher from tblGLVoucher where 1=1")

            If Me.dtpVoucherDateFrom.Checked Then
                strSQL.AppendLine("and Voucher_date >='" & Me.dtpVoucherDateFrom.Value.Date.ToString("yyyy-MM-dd") & "'")
            End If
            If Me.dtpVoucherDateTo.Checked Then
                strSQL.AppendLine("and Voucher_date <='" & Me.dtpVoucherDateTo.Value.Date.ToString("yyyy-MM-dd") & "'")
            End If
            strSQL.AppendLine("and Location_ID in(" & Me.lstShops.SelectedIDs & ")")

            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, strSQL.ToString, Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1

            Application.DoEvents()
            frmPrgBar.lblStatus.Text = "Copying tblGLVoucherDetail"
            'Copy tblGLVoucherDetail
            strSQL = New System.Text.StringBuilder
            strSQL.AppendLine("select * into SchemaSnapShot.dbo.tblGLVoucherDetail from tblGLVoucherDetail where voucher_id in (select Voucher_ID from schemasnapshot.dbo.tblGLVoucher) and location_ID in (select location_ID from schemasnapshot.dbo.tblGLVoucher)")
            Try
                SQLHelper.ExecuteNonQuery(SQLHelper.CON_STR, CommandType.Text, strSQL.ToString, Nothing)
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            frmPrgBar.PB.Value += 1
            frmPrgBar.PBMain.Value += 1


            frmPrgBar.PB.Value = 0
            frmPrgBar.PBMain.Value += 1

            frmPrgBar.PB.Value = 0

            frmPrgBar.PB.Value = 0

            frmPrgBar.lblMain.Text = "Copying Stored Procedures"
            'Create SPs with category 0(user defined sps)
            Dim dtSps As DataTable = New DALReports().GetDataTable("select  * from sys.procedures inner join sysobjects on sysobjects.id = sys.procedures.Object_ID where category = 0  and is_ms_shipped = 0  and  sysobjects.name like 'S%' and sysobjects.name not like 'sp_sel%' order by sysobjects.name")
            frmPrgBar.PB.Maximum += dtSps.Rows.Count
            For Each drSp As DataRow In dtSps.Rows
                Dim str As New System.Text.StringBuilder()
                Dim dt As DataTable = New DALReports().GetDataTable("exec sp_helptext '" & drSp(0) & "'")
                For Each drScript As DataRow In dt.Rows
                    str.Append(drScript(0))
                Next

                Try
                    Application.DoEvents()
                    frmPrgBar.lblStatus.Text = "Copying " & drSp(0)
                    SQLHelper.ExecuteNonQuery(bilder.ToString(), CommandType.Text, str.ToString(), Nothing)
                Catch ex As Exception
                End Try
                frmPrgBar.PB.Value += 1
            Next

            frmPrgBar.PB.Value = 0
            frmPrgBar.PBMain.Value += 1
            frmPrgBar.lblMain.Text = "Copying Views"
            'Create Views
            Dim dtViews As DataTable = New DAL.DALReports().GetDataTable("select  * from sys.views inner join sysobjects on sysobjects.id = sys.Views.Object_ID where category = 0  and is_ms_shipped = 0  and  sysobjects.name like 'V%' order by sysobjects.name")
            frmPrgBar.PB.Maximum += dtViews.Rows.Count
            For Each drView As DataRow In dtViews.Rows
                '' call sp_helptext and pass it the view name, it will return all the content of the view
                Dim dt As DataTable = New DAL.DALReports().GetDataTable("exec sp_helptext '" & drView(0) & "'")
                Dim str As New System.Text.StringBuilder
                'iterate the table and concat view content
                For Each drScript As DataRow In dt.Rows
                    str.Append(drScript(0))
                Next

                ''execute the statement to generate view in db
                Try
                    Application.DoEvents()
                    frmPrgBar.lblStatus.Text = "Copying " & drView(0)
                    SQLHelper.ExecuteNonQuery(bilder.ToString(), CommandType.Text, str.ToString(), Nothing)
                Catch ex As Exception
                End Try
                frmPrgBar.PB.Value += 1
            Next

            frmPrgBar.PB.Value = 0
            frmPrgBar.PBMain.Value += 1
            frmPrgBar.lblMain.Text = "Copying Functions"
            'Create functions
            Dim dtFunctions As DataTable = New DAL.DALReports().GetDataTable("select * from sys.objects where type in ('FN','TF')")
            frmPrgBar.PB.Maximum += dtFunctions.Rows.Count
            For Each drFunction As DataRow In dtFunctions.Rows
                '' call sp_helptext and pass it the function name, it will return all the content of the function
                Dim dt As DataTable = New DAL.DALReports().GetDataTable("exec sp_helptext '" & drFunction(0) & "'")
                Dim str As New System.Text.StringBuilder
                'iterate the table and concat function content
                For Each drScript As DataRow In dt.Rows
                    str.Append(drScript(0))
                Next

                ''execute the statement to generate functions in db
                Try
                    Application.DoEvents()
                    frmPrgBar.lblStatus.Text = "Copying " & drFunction(0)
                    SQLHelper.ExecuteNonQuery(bilder.ToString(), CommandType.Text, str.ToString(), Nothing)
                Catch ex As Exception
                End Try
                frmPrgBar.PB.Value += 1
            Next

            Application.DoEvents()
            frmPrgBar.PBMain.Value += 1

            frmPrgBar.Dispose()
            ShowInformationMessage("Schema Generated Successfully")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Try
                DestinationConnection.Close()
                con.Close()
                frmPrgBar.Dispose()
            Catch ex As Exception

            End Try
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

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

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As Utility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
End Class