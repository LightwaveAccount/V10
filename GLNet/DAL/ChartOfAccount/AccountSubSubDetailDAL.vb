''//24-Dec-2012         Fatima Tajammal     CR # 226 GL Chart of Accout should not be delete if GL integration is On and COA account mapped.
''// 323     23 Jul,2014          farooq-H   Cheque Printing: add cheque printing on voucher screen for bank payment voucher
''//22-Oct-2014      M.Shoaib        CR # 332  Data log: Datalog should prepare properly for all configuration of Lightwave
''//22-Oct-2014      M.Shoaib        CR # 333  DataLog Issue: Chart Of Account Detail
''//31-dec-2014      Fatima          CR # 353   Detail Account: Search On Detail Account works perfectly only once
Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility

Public Class AccountSubSubDetailDAL

    Public Function Deleted(ByVal ObjModel As AccountSubSubDetailModel) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "Delete from tblGlCOAMainSubSubDetail where coa_detail_id = " & ObjModel.DetailID


            ' Execute SQL ..
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))

            ' SQL Statement Log ..
            ObjModel.ActivityLog.SQLType = "DELETE"
            ObjModel.ActivityLog.TableName = "tblGlCOAMainSubSubDetail"
            ObjModel.ActivityLog.SQL = strSQL
            ' CR # 332
            'UtilityDAL.BuildSQLLog(ObjModel.ActivityLog, trans)
            UtilityDAL.BuildSQLLog(ObjModel.ActivityLog, trans, True)


            ' Activity Log ..
            ObjModel.ActivityLog.FormAction = "Delete"
            UtilityDAL.BuildActivityLog(ObjModel.ActivityLog, trans)


            ' Commit Transaction .. 
            trans.Commit()


            ' Return ..
            Return True


        Catch ex As SqlException
            trans.Rollback()
            Return False
            Throw ex

        Catch ex As Exception
            trans.Rollback()
            Return False
            Throw ex

        Finally
            conn.Close()

        End Try
    End Function

    Public Function GetAll(Optional ByVal strCondition As String = "") As DataTable

        Dim ObjDA As SqlClient.SqlDataAdapter

        Try

            Dim strSQL As String = ""
            Dim G_SEPERATOR As String = "-"
            '''// 323     23 Jul,2014          farooq-H  
            'strSQL = " SELECT tblGlCOAMainSubSubDetail.coa_detail_id, tblGlCOAMainSubSub.sub_sub_title + ' - ' + tblGlCOAMainSubSub.sub_sub_code AS sub_sub_code, " _
            '       & " tblGlCOAMainSubSubDetail.detail_code , tblGlCOAMainSubSubDetail.detail_title , " _
            '       & " case when tblGlCOAMainSubSubDetail.end_date is null then 'Open'  else 'Close' end as status, tblGlCOAMainSubSub.main_sub_sub_id " _
            '       & " FROM tblGlCOAMainSubSubDetail INNER JOIN " _
            '       & " tblGlCOAMainSubSub ON tblGlCOAMainSubSubDetail.main_sub_sub_id = tblGlCOAMainSubSub.main_sub_sub_id " _
            '       & " Where (tblGlCOAMainSubSubDetail.main_sub_sub_id = " & strCondition & ")"

            strSQL = " SELECT     tblGlCOAMainSubSubDetail.coa_detail_id, tblGlCOAMainSubSub.sub_sub_title + ' - ' + tblGlCOAMainSubSub.sub_sub_code AS sub_sub_code,  " _
                        & " tblGlCOAMainSubSubDetail.detail_code, tblGlCOAMainSubSubDetail.detail_title, CASE WHEN tblGlCOAMainSubSubDetail.end_date IS NULL  " _
                        & " THEN 'Open' ELSE 'Close' END AS status, tblGlCOAMainSubSub.main_sub_sub_id, tblChequeTemplates.Cheque_name as [Cheque Type] " _
                        & " FROM         tblGlCOAMainSubSubDetail INNER JOIN " _
                        & " tblGlCOAMainSubSub ON tblGlCOAMainSubSubDetail.main_sub_sub_id = tblGlCOAMainSubSub.main_sub_sub_id LEFT OUTER JOIN " _
                        & " tblChequeTemplates ON tblGlCOAMainSubSubDetail.cheque_id = tblChequeTemplates.Cheque_ID " _
                        & " Where (tblGlCOAMainSubSubDetail.main_sub_sub_id = " & strCondition & ")"

            ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim myDataTable As New DataTable("DetailAccounts")
            ObjDA.Fill(myDataTable)

            Return myDataTable

        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            ObjDA = Nothing

        End Try

    End Function

    Public Function GetNewAccountMainCode(Optional ByVal strCondition As String = "") As String

        Dim ObjDA As SqlClient.SqlDataAdapter

        Try

            Dim strSQL As String = ""

            strSQL = " SELECT MAX(detail_code) AS NewCode FROM tblGlCOAMainSubSubDetail GROUP BY main_sub_sub_id HAVING (main_sub_sub_id = " & strCondition & " ) "
            ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim myDataTable As New DataTable("AccountSubSubData")
            ObjDA.Fill(myDataTable)

            If myDataTable.Rows.Count <> 0 AndAlso myDataTable.Rows(0).Item("NewCode").ToString <> "" Then
                Return (Val(Right(myDataTable.Rows(0).Item("NewCode").ToString, 5)) + 1).ToString.PadLeft(5, "0")
            Else
                Return Val("1").ToString.PadLeft(5, "0")
            End If

        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            ObjDA = Nothing

        End Try

    End Function

    ' 07-Oct-2011       Asif Kamal      CR# 151 Account Detail search on Account detail form.
    Public Function GetSubSubGLId(Optional ByVal GLDtlID As Integer = 0) As Integer

        Dim ObjDA As SqlClient.SqlDataAdapter
        Dim dr As SqlClient.SqlDataReader
        Dim GLID As Integer = 0

        Try

            Dim strSQL As String = ""

            strSQL = " select main_sub_sub_id from tblGlCOAMainSubSubDetail" & _
                    " where coa_detail_id = " & GLDtlID & "  "
            dr = UtilityDAL.ExecuteReader(strSQL)

            If dr.HasRows Then
                While dr.Read
                    GLID = Convert.ToInt64(dr.Item("main_sub_sub_id").ToString())
                End While

            End If

            'ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            'Dim myDataTable As New DataTable("AccountSubSubData")
            'ObjDA.Fill(myDataTable)

            'If myDataTable.Rows.Count <> 0 AndAlso myDataTable.Rows(0).Item("NewCode").ToString <> "" Then
            '    Return (Val(Right(myDataTable.Rows(0).Item("NewCode").ToString, 5)) + 1).ToString.PadLeft(5, "0")
            'Else
            '    Return Val("1").ToString.PadLeft(5, "0")
            'End If
            Return GLID

        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            ObjDA = Nothing

        End Try

    End Function


    Public Function IsAlreadyExists(ByVal ObjModel As AccountSubSubDetailModel, Optional ByVal strMode As String = "") As Boolean

        Try

            Dim strSQL As String
            ' Building SQL ..

            If strMode = "Update" Then
                strSQL = "select detail_code from tblGlCOAMainSubSubDetail where (detail_code = '" & ObjModel.DetailCode & "' or detail_title = '" & ObjModel.DetailTitle & "') and coa_detail_id <> " & ObjModel.DetailID

            Else
                strSQL = "select detail_code from tblGlCOAMainSubSubDetail Where (detail_code = '" & ObjModel.DetailCode & "' or detail_title = '" & ObjModel.DetailTitle & "') "

            End If


            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If Not objNameDR.HasRows Then
                    Return False

                Else
                    Return True

                End If
                Return True

            End Using

        Catch ex As Exception
            Throw ex

        End Try

    End Function


    Public Function Save(ByVal ObjModel As AccountSubSubDetailModel) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction
        Try


            Dim strSQL As String

            If ObjModel.EndDateFlag = 1 Then
                ''''// 323     23 Jul,2014          farooq-H  
                'strSQL = " Insert into tblGlCOAMainSubSubDetail (main_sub_sub_id, detail_code, detail_title,end_date  ) Values(" _
                '                      & ObjModel.SubSubAccountID & ", '" & ObjModel.DetailCode & "','" & ObjModel.DetailTitle & "',GetDate() ) " _
                '                      & " Select Ident_Current('tblGlCOAMainSubSubDetail')"

                strSQL = " Insert into tblGlCOAMainSubSubDetail (main_sub_sub_id, detail_code, detail_title,end_date , Cheque_id ) Values(" _
                       & ObjModel.SubSubAccountID & ", '" & ObjModel.DetailCode & "','" & ObjModel.DetailTitle & "',GetDate() , " & ObjModel.ChequeID & ") " _
                       & " Select Ident_Current('tblGlCOAMainSubSubDetail')"
            Else
                '''// 323     23 Jul,2014          farooq-H  
                'strSQL = " Insert into tblGlCOAMainSubSubDetail (main_sub_sub_id, detail_code, detail_title,end_date) Values(" _
                '       & ObjModel.SubSubAccountID & ", '" & ObjModel.DetailCode & "','" & ObjModel.DetailTitle & "',NULL) " _
                '       & " Select Ident_Current('tblGlCOAMainSubSubDetail')"

                strSQL = " Insert into tblGlCOAMainSubSubDetail (main_sub_sub_id, detail_code, detail_title,end_date , cheque_id ) Values(" _
                      & ObjModel.SubSubAccountID & ", '" & ObjModel.DetailCode & "','" & ObjModel.DetailTitle & "',NULL , " & ObjModel.ChequeID & ") " _
                      & " Select Ident_Current('tblGlCOAMainSubSubDetail')"

            End If



            ' Execute SQL  ..
            ObjModel.DetailID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))
            '''''// 323     23 Jul,2014          farooq-H  
            'CR#160
            'If ObjModel.EndDateFlag = 1 Then
            '    strSQL = " Insert into tblGlCOAMainSubSubDetail (coa_detail_id,main_sub_sub_id, detail_code, detail_title,end_date)" & _
            '            " Values(" & ObjModel.DetailID & "," & ObjModel.SubSubAccountID & ", '" & ObjModel.DetailCode & "','" & ObjModel.DetailTitle & "',GetDate())"

            'Else

            '    strSQL = " Insert into tblGlCOAMainSubSubDetail (coa_detail_id,main_sub_sub_id, detail_code, detail_title,end_date)" & _
            '            " Values(" & ObjModel.DetailID & "," & ObjModel.SubSubAccountID & ", '" & ObjModel.DetailCode & "','" & ObjModel.DetailTitle & "',NULL) "

            'End If
            If ObjModel.EndDateFlag = 1 Then
                ' CR # 333 Column checque_id is removed.
                'strSQL = " Insert into tblGlCOAMainSubSubDetail (coa_detail_id,main_sub_sub_id, detail_code, detail_title,end_date , cheque_id )" & _
                '        " Values(" & ObjModel.DetailID & "," & ObjModel.SubSubAccountID & ", '" & ObjModel.DetailCode & "','" & ObjModel.DetailTitle & "',GetDate() , " & ObjModel.ChequeID & ")"

                strSQL = " Insert into tblGlCOAMainSubSubDetail (coa_detail_id,main_sub_sub_id, detail_code, detail_title,end_date  )" & _
                        " Values(" & ObjModel.DetailID & "," & ObjModel.SubSubAccountID & ", '" & ObjModel.DetailCode & "','" & ObjModel.DetailTitle & "',GetDate() )"

            Else

                'strSQL = " Insert into tblGlCOAMainSubSubDetail (coa_detail_id,main_sub_sub_id, detail_code, detail_title,end_date , cheque_id )" & _
                '       " Values(" & ObjModel.DetailID & "," & ObjModel.SubSubAccountID & ", '" & ObjModel.DetailCode & "','" & ObjModel.DetailTitle & "',NULL , " & ObjModel.ChequeID & " ) "
                strSQL = " Insert into tblGlCOAMainSubSubDetail (coa_detail_id,main_sub_sub_id, detail_code, detail_title,end_date )" & _
                        " Values(" & ObjModel.DetailID & "," & ObjModel.SubSubAccountID & ", '" & ObjModel.DetailCode & "','" & ObjModel.DetailTitle & "',NULL ) "
                ' End CR # 333
            End If


            ' SQL Statement Log ..
            ObjModel.ActivityLog.SQLType = "INSERT"
            ObjModel.ActivityLog.TableName = "tblGlCOAMainSubSubDetail"
            ObjModel.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(ObjModel.ActivityLog, trans, True)

            ' Activity Log ..
            ObjModel.ActivityLog.FormAction = "Save"
            UtilityDAL.BuildActivityLog(ObjModel.ActivityLog, trans)

            ''Commit Traction
            trans.Commit()

            ' Return
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


    Public Function Update(ByVal ObjModel As AccountSubSubDetailModel) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction
        Try


            Dim strSQL As String
            '''''// 323     23 Jul,2014          farooq-H  
            'If ObjModel.EndDateFlag = 1 Then
            '    strSQL = " Update tblGlCOAMainSubSubDetail Set detail_title = '" & ObjModel.DetailTitle & "', " _
            '           & " detail_code ='" & ObjModel.DetailCode & "', end_date = getdate() where coa_detail_id = " & ObjModel.DetailID


            'Else
            '    strSQL = "Update tblGlCOAMainSubSubDetail Set detail_title ='" & ObjModel.DetailTitle & "', " & _
            '       " detail_code ='" & ObjModel.DetailCode & "' ,end_date = NULL where coa_detail_id = " & ObjModel.DetailID

            'End If
            If ObjModel.EndDateFlag = 1 Then
                strSQL = " Update tblGlCOAMainSubSubDetail Set detail_title = '" & ObjModel.DetailTitle & "', " _
                       & " detail_code ='" & ObjModel.DetailCode & "', end_date = getdate() , cheque_id  = " & ObjModel.ChequeID & " where coa_detail_id = " & ObjModel.DetailID


            Else
                strSQL = "Update tblGlCOAMainSubSubDetail Set detail_title ='" & ObjModel.DetailTitle & "', " & _
                   " detail_code ='" & ObjModel.DetailCode & "' ,end_date = NULL  , cheque_id  = " & ObjModel.ChequeID & "  where coa_detail_id = " & ObjModel.DetailID

            End If


            ' Execute SQL  ..
            ObjModel.SubSubAccountID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

            ' CR # 333 Column checque_id is removed from strSQL (Start CR)
            If ObjModel.EndDateFlag = 1 Then
                strSQL = " Update tblGlCOAMainSubSubDetail Set detail_title = '" & ObjModel.DetailTitle & "', " _
                       & " detail_code ='" & ObjModel.DetailCode & "', end_date = getdate() where coa_detail_id = " & ObjModel.DetailID
            Else
                strSQL = "Update tblGlCOAMainSubSubDetail Set detail_title ='" & ObjModel.DetailTitle & "', " & _
                   " detail_code ='" & ObjModel.DetailCode & "' ,end_date = NULL  where coa_detail_id = " & ObjModel.DetailID
            End If
            ' End CR # 333

            ' SQL Statement Log ..
            ObjModel.ActivityLog.SQLType = "UPDATE"
            ObjModel.ActivityLog.TableName = "tblGlCOAMainSubSubDetail"
            ObjModel.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(ObjModel.ActivityLog, trans, True)


            ' Activity Log ..
            ObjModel.ActivityLog.FormAction = "Update"
            UtilityDAL.BuildActivityLog(ObjModel.ActivityLog, trans)


            ' Commit Transaction .. 
            trans.Commit()


            ' Return
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
    Public Function TransactionsExist(Optional ByVal DetailAccount As String = "") As Boolean

        Dim ObjDA As SqlClient.SqlDataAdapter

        Try

            Dim strSQL As String = ""
            Dim isTransExist As Boolean = False

            strSQL = " Select count(*) from tblglvoucherdetail where coa_detail_id= " & DetailAccount
            ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Using objTransDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objTransDR.HasRows Then

                    objTransDR.Read()
                    If Convert.ToInt32(objTransDR.Item(0)) > 0 Then
                        isTransExist = True
                    Else
                        isTransExist = False
                    End If

                End If

                Return isTransExist

            End Using


        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            ObjDA = Nothing

        End Try

    End Function

    ' 07-Oct-2011       Asif Kamal      CR# 151 Account Detail search on Account detail form.
    Public Function GetAccountName(Optional ByVal strCondition As String = "") As String

        Dim ObjDA As SqlClient.SqlDataAdapter
        Dim AccountName As String


        Try

            Dim strSQL As String = ""
            strSQL = "SELECT sub_sub_title from tblGlCOAMainSubSub where sub_sub_code = '" & strCondition & "'"

            ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim myDataTable As New DataTable("AccountName")
            ObjDA.Fill(myDataTable)
            'Cr # 353 if datatable has no rows then system should return empty string
            If myDataTable.Rows.Count > 0 Then
                AccountName = myDataTable.Rows(0).Item(0).ToString()
            Else
                AccountName = String.Empty
            End If


            Return AccountName

        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            ObjDA = Nothing

        End Try

    End Function
    'CR # 226
    Public Function IsMappingExsist(ByVal Condition As Utility.Utility.EnumMappingChk, Optional ByVal DetailAccount As String = "") As Boolean

        Dim ObjDA As SqlClient.SqlDataAdapter

        Try

            Dim strSQL As String = ""
            Dim isTransExist As Boolean = False

            If Condition = EnumMappingChk.System_Configuration Then
                strSQL = "select Count(*) from tblrcmsconfiguration where config_name in('gl_acc_stock_value_account','CustomerSubSubAccHead','SupplierSubSubAccHead','gl_acc_sales_tax_deducted','gl_acc_sales_tax_payable','gl_acc_income_tax_payable','gl_acc_income_tax_deducted','gl_acc_loading_expense','gl_acc_freight_expense','gl_acc_other_expense' ) And Config_value ='" & DetailAccount & "'"
            ElseIf Condition = EnumMappingChk.Shop_Defination Then
                strSQL = "select count(*) from tblshopconfiguration where config_name in('gl_acc_sale','gl_acc_expense','gl_acc_phy_audit_expense','gl_acc_cash_in_hand','gl_acc_COGS','gl_acc_cr_card_rec') And Config_value='" & DetailAccount & "'"
            ElseIf Condition = EnumMappingChk.Customer Then
                strSQL = "select count(*) from tblmemberinfo where gl_acc_member=" & DetailAccount & ""
            ElseIf Condition = EnumMappingChk.Supplier Then
                strSQL = "select count(*) from tbldefsuppliers where gl_acc_supplier=" & DetailAccount & ""
            ElseIf Condition = EnumMappingChk.Line_Item Then
                strSQL = "select count(*) from tbldeflineitems where gl_acc_Lineitem=" & DetailAccount & ""
            ElseIf Condition = EnumMappingChk.Acount_Heads Then
                strSQL = "select count(*) from tbldefaccountheads where gl_account_num=" & DetailAccount & ""
            End If

            ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Using objTransDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objTransDR.HasRows Then

                    objTransDR.Read()
                    If Convert.ToInt32(objTransDR.Item(0)) > 0 Then
                        isTransExist = True
                    Else
                        isTransExist = False
                    End If

                End If

                Return isTransExist

            End Using


        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            ObjDA = Nothing

        End Try

    End Function
    'CR # 226
    Public Function IsGLIntegrated() As String
        Dim ObjDA As SqlClient.SqlDataAdapter
        Try
            Dim strSQL As String = ""
            Dim GLKey As String = String.Empty
            strSQL = "select config_value from tblrcmsconfiguration where config_name ='integrated_with_gl'"
            ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)
            Using objTransDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objTransDR.HasRows Then

                    objTransDR.Read()
                    Return objTransDR.Item("Config_value")

                End If
            End Using

        Catch ex As Exception

        End Try
    End Function
    Public Function GetChequeType() As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String
            Dim gstrComboZeroIndexString As String = "---Select---"

            strSQL = "SELECT [Cheque_ID]   ,[Cheque_name] as [Cheque Name] ,[Cheque_template] ,[Cheque_Report] FROM [dbo].[tblChequeTemplates]"

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("ChequeTemplate")

            objDA.Fill(MyCollectionList)

            Dim dr As DataRow = MyCollectionList.NewRow
            dr.Item("Cheque_ID") = 0
            dr.Item("Cheque Name") = gstrComboZeroIndexString
            MyCollectionList.Rows.InsertAt(dr, 0)

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
