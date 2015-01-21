'12-dec-2014        CR # 346        Fatima Tajammal   Changes are required in Sale Tax Invoice
Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility


Public Class CustomerInfoDAL


#Region "Local Variables and Functions"

#End Region

#Region "Global Variables and Functions "

    '' For the Insertion of Record in the "Customer Information Table" 

    Public Function Add(ByVal objCustomerInfo As CustomerInfo) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)        '' Assigning Connection String
        conn.Open()                                             '' Connection Opening
        Dim trans As SqlTransaction = conn.BeginTransaction     '' Begining Transaction

        Try

            Dim strSQL As String

            'Code commented against CR # 346
            'strSQL = "Insert into tblGLCustomerInforamtion(Account_id, Creation_date, Maint_startdate, Maint_enddate, Payment_rcvddate, Product, Status, Sla_Type,Monthly_amount,Contact_person,Phone_office,Mobile,Fax,Email,Address,Remarks ) " & _
            '         " Values(" & objCustomerInfo.AccountID & ", '" & objCustomerInfo.CreationDate & "', " & IIf(objCustomerInfo.MaintStartDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(objCustomerInfo.MaintStartDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", " & IIf(objCustomerInfo.MaintEndDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(objCustomerInfo.MaintEndDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", " & IIf(objCustomerInfo.PaymentRcvdDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(objCustomerInfo.PaymentRcvdDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ",'" & objCustomerInfo.Product & "','" & objCustomerInfo.Status & "','" & objCustomerInfo.SlaType & "'," & objCustomerInfo.MonthlyAmount & ",'" & objCustomerInfo.ContactPerson & "','" & objCustomerInfo.PhoneOffice & "','" & objCustomerInfo.Mobile & "','" & objCustomerInfo.Fax & "','" & objCustomerInfo.Email & "','" & objCustomerInfo.Address & "','" & objCustomerInfo.Remarks & "' ) "

            'CR # 346
            strSQL = "Insert into tblGLCustomerInforamtion(Account_id, Creation_date, Maint_startdate, Maint_enddate, Payment_rcvddate, Product, Status, Sla_Type,Monthly_amount,Contact_person,Phone_office,Mobile,Fax,Email,Address,Remarks,Cust_NTN_Num,Cust_STR_Num ) " & _
                     " Values(" & objCustomerInfo.AccountID & ", '" & objCustomerInfo.CreationDate & "', " & IIf(objCustomerInfo.MaintStartDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(objCustomerInfo.MaintStartDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", " & IIf(objCustomerInfo.MaintEndDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(objCustomerInfo.MaintEndDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ", " & IIf(objCustomerInfo.PaymentRcvdDate = Date.MinValue, "NULL", " convert(datetime,'" & Format(objCustomerInfo.PaymentRcvdDate, "dd-MMM-yyyy hh:m tt") & "',108)  ") & ",'" & objCustomerInfo.Product & "','" & objCustomerInfo.Status & "','" & objCustomerInfo.SlaType & "'," & objCustomerInfo.MonthlyAmount & ",'" & objCustomerInfo.ContactPerson & "','" & objCustomerInfo.PhoneOffice & "','" & objCustomerInfo.Mobile & "','" & objCustomerInfo.Fax & "','" & objCustomerInfo.Email & "','" & objCustomerInfo.Address & "','" & objCustomerInfo.Remarks & "','" & objCustomerInfo.cust_NTNNumber & "','" & objCustomerInfo.cust_STRNumber & "' ) "

            ''Execute SQL Command
            objCustomerInfo.CustomerInfoID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

            ''For SQL Statement Log
            objCustomerInfo.ActivityLog.SQLType = "INSERT"
            objCustomerInfo.ActivityLog.TableName = "tblGLCustomerInforamtion"
            objCustomerInfo.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objCustomerInfo.ActivityLog, trans)

            ''For Activity Log
            objCustomerInfo.ActivityLog.FormAction = "Save"
            UtilityDAL.BuildActivityLog(objCustomerInfo.ActivityLog, trans)

            ''Commiting Transaction
            trans.Commit()

            ''Return
            Return True

        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function

#End Region


    '' For the Updation of the Record in "Customer Information Table"

    Public Function Update(ByVal objCustomerInfo As CustomerInfo) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String
            'Code commented against CR # 346
            'strSQL = "Update tblGLCustomerInforamtion Set Account_id =" & objCustomerInfo.AccountID & ",Creation_date ='" & objCustomerInfo.CreationDate & "', Maint_startdate = '" & objCustomerInfo.MaintStartDate & "' , Maint_enddate ='" & objCustomerInfo.MaintEndDate & "', Payment_rcvddate ='" & objCustomerInfo.PaymentRcvdDate & "', Product ='" & objCustomerInfo.Product & "', Status ='" & objCustomerInfo.Status & "', Sla_Type='" & objCustomerInfo.SlaType & "', Monthly_amount=" & objCustomerInfo.MonthlyAmount & ", Contact_person='" & objCustomerInfo.ContactPerson & "', Phone_office='" & objCustomerInfo.PhoneOffice & "', Mobile='" & objCustomerInfo.Mobile & "', Fax='" & objCustomerInfo.Fax & "', Email='" & objCustomerInfo.Email & "', Address='" & objCustomerInfo.Address & "', Remarks='" & objCustomerInfo.Remarks & "' where Account_id =" & objCustomerInfo.AccountID & ""
            'CR # 346
            strSQL = "Update tblGLCustomerInforamtion Set Account_id =" & objCustomerInfo.AccountID & ",Creation_date ='" & objCustomerInfo.CreationDate & "', Maint_startdate = '" & objCustomerInfo.MaintStartDate & "' , Maint_enddate ='" & objCustomerInfo.MaintEndDate & "', Payment_rcvddate ='" & objCustomerInfo.PaymentRcvdDate & "', Product ='" & objCustomerInfo.Product & "', Status ='" & objCustomerInfo.Status & "', Sla_Type='" & objCustomerInfo.SlaType & "', Monthly_amount=" & objCustomerInfo.MonthlyAmount & ", Contact_person='" & objCustomerInfo.ContactPerson & "', Phone_office='" & objCustomerInfo.PhoneOffice & "', Mobile='" & objCustomerInfo.Mobile & "', Fax='" & objCustomerInfo.Fax & "', Email='" & objCustomerInfo.Email & "', Address='" & objCustomerInfo.Address & "', Remarks='" & objCustomerInfo.Remarks & "',Cust_NTN_Num='" & objCustomerInfo.cust_NTNNumber & "',Cust_STR_Num='" & objCustomerInfo.cust_STRNumber & "' where Account_id =" & objCustomerInfo.AccountID & ""

            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

            'SQL Statement Log
            objCustomerInfo.ActivityLog.SQLType = "UPDATE"
            objCustomerInfo.ActivityLog.TableName = "tblGLCustomerInforamtion"
            objCustomerInfo.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objCustomerInfo.ActivityLog, trans)

            ' ''Activity Log
            objCustomerInfo.ActivityLog.FormAction = "UPDATE"
            UtilityDAL.BuildActivityLog(objCustomerInfo.ActivityLog, trans)

            '    ''Committing Transaction
            trans.Commit()

            '    ''Return
            Return True

        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    '' For the Deletion of the Record from the "Customer Information Table"

    Public Function Deleted(ByVal objCustomerInfo As CustomerInfo) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "Delete from tblGLCustomerInforamtion where Account_id =" & objCustomerInfo.AccountID & ""
            ''Execute SQL 

            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)
            'SQL Statement Log
            objCustomerInfo.ActivityLog.SQLType = "DELETE"
            objCustomerInfo.ActivityLog.TableName = "tblGLCustomerInforamtion"
            objCustomerInfo.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objCustomerInfo.ActivityLog, trans)

            ''Activity Log
            objCustomerInfo.ActivityLog.FormAction = "Delete"
            UtilityDAL.BuildActivityLog(objCustomerInfo.ActivityLog, trans)

            ''Commit Traction
            trans.Commit()

            ''Return
            Return True

        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    '' Get All Records

    Public Function GetAll(Optional ByVal strCondition As String = "") As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String

            'COde commented against CR # 346
            'strSQL = "SELECT tblGLCustomerInforamtion.CustomerInfoId, tblGlCOAMainSubSubDetail.Coa_Detail_Id, tblGlCOAMainSubSubDetail.detail_code AS [Account Code], tblGlCOAMainSubSubDetail.detail_title as [Account Name], tblGLCustomerInforamtion.Creation_date [Creation Date],tblGLCustomerInforamtion.Maint_startdate [Maintainance Start Date],tblGLCustomerInforamtion.Maint_enddate [Maintainance End Date],tblGLCustomerInforamtion.Payment_rcvddate [Payment Recieved Date],tblGLCustomerInforamtion.product [Product],tblGLCustomerInforamtion.status [Status],tblGLCustomerInforamtion.sla_type [SLA Type],tblGLCustomerInforamtion.monthly_amount [Monthly Amount],tblGLCustomerInforamtion.contact_person [Contact Person],tblGLCustomerInforamtion.phone_office [Phone Office],tblGLCustomerInforamtion.mobile [Mobile],tblGLCustomerInforamtion.fax [Fax],tblGLCustomerInforamtion.email [Email],tblGLCustomerInforamtion.address [Address],tblGLCustomerInforamtion.Remarks [Remarks] From tblGLCustomerInforamtion INNER JOIN tblGlCOAMainSubSubDetail ON tblGLCustomerInforamtion.Account_id = tblGlCOAMainSubSubDetail.coa_detail_id"
            'CR # 346
            strSQL = "SELECT tblGLCustomerInforamtion.CustomerInfoId, tblGlCOAMainSubSubDetail.Coa_Detail_Id, tblGlCOAMainSubSubDetail.detail_code AS [Account Code], tblGlCOAMainSubSubDetail.detail_title as [Account Name], tblGLCustomerInforamtion.Creation_date [Creation Date],tblGLCustomerInforamtion.Maint_startdate [Maintainance Start Date],tblGLCustomerInforamtion.Maint_enddate [Maintainance End Date],tblGLCustomerInforamtion.Payment_rcvddate [Payment Recieved Date],tblGLCustomerInforamtion.product [Product],tblGLCustomerInforamtion.status [Status],tblGLCustomerInforamtion.sla_type [SLA Type],tblGLCustomerInforamtion.monthly_amount [Monthly Amount],tblGLCustomerInforamtion.contact_person [Contact Person],tblGLCustomerInforamtion.phone_office [Phone Office],tblGLCustomerInforamtion.mobile [Mobile],tblGLCustomerInforamtion.fax [Fax],tblGLCustomerInforamtion.email [Email],tblGLCustomerInforamtion.address [Address],tblGLCustomerInforamtion.Remarks [Remarks],isnull(Cust_NTN_Num,'') as [National Tax#],isnull(Cust_STR_Num,'') as [Sales Tax Reg.#] From tblGLCustomerInforamtion INNER JOIN tblGlCOAMainSubSubDetail ON tblGLCustomerInforamtion.Account_id = tblGlCOAMainSubSubDetail.coa_detail_id"
            'strSQL = "SELECT tblGLCustomerInforamtion.CustomerInfoId, tblGlCOAMainSubSubDetail.Coa_Detail_Id, tblGlCOAMainSubSubDetail.detail_code AS [Account Code], tblGlCOAMainSubSubDetail.detail_title as [Account Name], CONVERT(VARCHAR(10), tblGLCustomerInforamtion.Creation_date, 103)  [Creation Date],CONVERT(VARCHAR(10), tblGLCustomerInforamtion.Maint_startdate,103) [Maintainance Start Date],CONVERT(VARCHAR(10), tblGLCustomerInforamtion.Maint_enddate,103) [Maintainance End Date],CONVERT(VARCHAR(10), tblGLCustomerInforamtion.Payment_rcvddate,103) [Payment Recieved Date],tblGLCustomerInforamtion.product [Product],tblGLCustomerInforamtion.status [Status],tblGLCustomerInforamtion.sla_type [SLA Type],tblGLCustomerInforamtion.monthly_amount [Monthly Amount],tblGLCustomerInforamtion.contact_person [Contact Person],tblGLCustomerInforamtion.phone_office [Phone Office],tblGLCustomerInforamtion.mobile [Mobile],tblGLCustomerInforamtion.fax [Fax],tblGLCustomerInforamtion.email [Email],tblGLCustomerInforamtion.address [Address],tblGLCustomerInforamtion.Remarks [Remarks] From tblGLCustomerInforamtion INNER JOIN tblGlCOAMainSubSubDetail ON tblGLCustomerInforamtion.Account_id = tblGlCOAMainSubSubDetail.coa_detail_id"
           
            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("CustomerInfo")
            objDA.Fill(MyCollectionList)

            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function


    '' Get All Detail Records
    Public Function GetAll_Detail(ByVal objCustomerInfo As CustomerInfo) As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String

            'strSQL = "SELECT tblGLCustomerInforamtion.CustomerInfoId, tblGlCOAMainSubSubDetail.Coa_Detail_Id, tblGlCOAMainSubSubDetail.detail_code AS [Account Code], tblGlCOAMainSubSubDetail.detail_title as [Account Name], CONVERT(VARCHAR(10), tblGLCustomerInforamtion.Creation_date, 103) [Creation Date],CONVERT(VARCHAR(10), tblGLCustomerInforamtion.Maint_startdate, 103) [Maintainance Start Date],CONVERT(VARCHAR(10), tblGLCustomerInforamtion.Maint_enddate, 103) [Maintainance End Date],CONVERT(VARCHAR(10), tblGLCustomerInforamtion.Payment_rcvddate, 103) [Payment Recieved Date],tblGLCustomerInforamtion.product [Product],tblGLCustomerInforamtion.status [Status],tblGLCustomerInforamtion.sla_type [SLA Type],tblGLCustomerInforamtion.monthly_amount [Monthly Amount],tblGLCustomerInforamtion.contact_person [Contact Person],tblGLCustomerInforamtion.phone_office [Phone Office],tblGLCustomerInforamtion.mobile [Mobile],tblGLCustomerInforamtion.fax [Fax],tblGLCustomerInforamtion.email [Email],tblGLCustomerInforamtion.address [Address],tblGLCustomerInforamtion.Remarks [Remarks] From tblGLCustomerInforamtion INNER JOIN tblGlCOAMainSubSubDetail ON tblGLCustomerInforamtion.Account_id = tblGlCOAMainSubSubDetail.coa_detail_id WHERE 1=1"
            'Code commented against CR # 346
            'strSQL = "SELECT tblGLCustomerInforamtion.CustomerInfoId, tblGlCOAMainSubSubDetail.Coa_Detail_Id, tblGlCOAMainSubSubDetail.detail_code AS [Account Code], tblGlCOAMainSubSubDetail.detail_title as [Account Name], tblGLCustomerInforamtion.Creation_date [Creation Date],tblGLCustomerInforamtion.Maint_startdate [Maintainance Start Date],tblGLCustomerInforamtion.Maint_enddate [Maintainance End Date],tblGLCustomerInforamtion.Payment_rcvddate [Payment Recieved Date],tblGLCustomerInforamtion.product [Product],tblGLCustomerInforamtion.status [Status],tblGLCustomerInforamtion.sla_type [SLA Type],tblGLCustomerInforamtion.monthly_amount [Monthly Amount],tblGLCustomerInforamtion.contact_person [Contact Person],tblGLCustomerInforamtion.phone_office [Phone Office],tblGLCustomerInforamtion.mobile [Mobile],tblGLCustomerInforamtion.fax [Fax],tblGLCustomerInforamtion.email [Email],tblGLCustomerInforamtion.address [Address],tblGLCustomerInforamtion.Remarks [Remarks] From tblGLCustomerInforamtion INNER JOIN tblGlCOAMainSubSubDetail ON tblGLCustomerInforamtion.Account_id = tblGlCOAMainSubSubDetail.coa_detail_id where 1=1"
            'CR # 346
            strSQL = "SELECT tblGLCustomerInforamtion.CustomerInfoId, tblGlCOAMainSubSubDetail.Coa_Detail_Id, tblGlCOAMainSubSubDetail.detail_code AS [Account Code], tblGlCOAMainSubSubDetail.detail_title as [Account Name], tblGLCustomerInforamtion.Creation_date [Creation Date],tblGLCustomerInforamtion.Maint_startdate [Maintainance Start Date],tblGLCustomerInforamtion.Maint_enddate [Maintainance End Date],tblGLCustomerInforamtion.Payment_rcvddate [Payment Recieved Date],tblGLCustomerInforamtion.product [Product],tblGLCustomerInforamtion.status [Status],tblGLCustomerInforamtion.sla_type [SLA Type],tblGLCustomerInforamtion.monthly_amount [Monthly Amount],tblGLCustomerInforamtion.contact_person [Contact Person],tblGLCustomerInforamtion.phone_office [Phone Office],tblGLCustomerInforamtion.mobile [Mobile],tblGLCustomerInforamtion.fax [Fax],tblGLCustomerInforamtion.email [Email],tblGLCustomerInforamtion.address [Address],tblGLCustomerInforamtion.Remarks [Remarks],isnull(Cust_NTN_Num,'') as [National Tax#],isnull(Cust_STR_Num,'') as [Sales Tax Reg.#] From tblGLCustomerInforamtion INNER JOIN tblGlCOAMainSubSubDetail ON tblGLCustomerInforamtion.Account_id = tblGlCOAMainSubSubDetail.coa_detail_id where 1=1"



            If Not objCustomerInfo.DtlCreationDate = Date.MinValue And Not objCustomerInfo.DtlCreationDate2 = Date.MinValue Then

                strSQL = strSQL & "AND tblGLCustomerInforamtion.Creation_date >= '" & objCustomerInfo.DtlCreationDate.ToString("yyyy-MM-dd") & "' AND tblGLCustomerInforamtion.Creation_date <= '" & objCustomerInfo.DtlCreationDate2.ToString("yyyy-MM-dd") & "'"

            ElseIf Not objCustomerInfo.DtlCreationDate = Date.MinValue Then

                strSQL = strSQL & " AND tblGLCustomerInforamtion.Creation_date > '" & objCustomerInfo.DtlCreationDate.ToString("yyyy-MM-dd") & "'"

            ElseIf Not objCustomerInfo.DtlCreationDate2 = Date.MinValue Then

                strSQL = strSQL & " AND tblGLCustomerInforamtion.Creation_date < '" & objCustomerInfo.DtlCreationDate2.ToString("yyyy-MM-dd") & "'"

            End If

            ''--------------------

            If Not objCustomerInfo.DtlMaintEndDate = Date.MinValue And Not objCustomerInfo.DtlMaintEndToDate = Date.MinValue Then

                strSQL = strSQL & "AND tblGLCustomerInforamtion.Maint_enddate >= '" & objCustomerInfo.DtlMaintEndDate.ToString("yyyy-MM-dd") & "' AND tblGLCustomerInforamtion.Maint_enddate <= '" & objCustomerInfo.DtlMaintEndToDate.ToString("yyyy-MM-dd") & "'"

            ElseIf Not objCustomerInfo.DtlMaintEndDate = Date.MinValue Then

                strSQL = strSQL & " AND tblGLCustomerInforamtion.Maint_enddate > '" & objCustomerInfo.DtlMaintEndDate.ToString("yyyy-MM-dd") & "'"

            ElseIf Not objCustomerInfo.DtlMaintEndToDate = Date.MinValue Then

                strSQL = strSQL & " AND tblGLCustomerInforamtion.Maint_enddate < '" & objCustomerInfo.DtlMaintEndToDate.ToString("yyyy-MM-dd") & "'"

            End If

            ''-----------

            If Not objCustomerInfo.DtlPaymentRcvdDate = Date.MinValue And Not objCustomerInfo.DtlPaymentRcvdToDate = Date.MinValue Then

                strSQL = strSQL & "AND tblGLCustomerInforamtion.Payment_rcvddate >= '" & objCustomerInfo.DtlPaymentRcvdDate.ToString("yyyy-MM-dd") & "' AND tblGLCustomerInforamtion.Payment_rcvddate <= '" & objCustomerInfo.DtlPaymentRcvdToDate.ToString("yyyy-MM-dd") & "'"

            ElseIf Not objCustomerInfo.DtlPaymentRcvdDate = Date.MinValue Then

                strSQL = strSQL & " AND tblGLCustomerInforamtion.Payment_rcvddate > '" & objCustomerInfo.DtlPaymentRcvdDate.ToString("yyyy-MM-dd") & "'"

            ElseIf Not objCustomerInfo.DtlPaymentRcvdToDate = Date.MinValue Then

                strSQL = strSQL & " AND tblGLCustomerInforamtion.Payment_rcvddate < '" & objCustomerInfo.DtlPaymentRcvdToDate.ToString("yyyy-MM-dd") & "'"

            End If

            ''------------


            If Not objCustomerInfo.DtlProduct = "--Select--" Then

                strSQL = strSQL & " AND tblGLCustomerInforamtion.Product = '" & objCustomerInfo.DtlProduct & "'"

            End If

            ''------

            If Not objCustomerInfo.DtlStatus = "--Select--" Then

                strSQL = strSQL & " AND tblGLCustomerInforamtion.Status = '" & objCustomerInfo.DtlStatus & "'"

            End If

            ''----------

            If Not objCustomerInfo.DtlSlaType = "--Select--" Then

                strSQL = strSQL & " AND tblGLCustomerInforamtion.Sla_Type = '" & objCustomerInfo.DtlSlaType & "'"

            End If

            ''---------

            If Not objCustomerInfo.DtlContactPerson Is Nothing Then

                strSQL = strSQL & " AND tblGLCustomerInforamtion.Contact_Person  LIKE '%" & objCustomerInfo.DtlContactPerson & "%'"

            End If

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("CustomerInfo")
            objDA.Fill(MyCollectionList)

            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function


End Class
