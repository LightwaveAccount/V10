''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela New
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Daily Activity Report .. 
''// Programmer	     : Tariq Majeed
''// Creation Date	 : 16-July-2009
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description
''// 01-May-2013       Rana Saeed          CR # 235 New Data Sync changes must be updated for Lightwave
''// 06-Nov-2014       M. Shoaib           CR # 335 Lightwave Release Update, Log should maintain properly
''//------------------------------------------------------------------------------------
''/////////////////////////////////////////////////////////////////////////////////////////


Imports System.Data
Imports System.Data.SqlClient
Imports Utility.Utility
Imports Model

Public Class UtilityDAL

    Public Shared Function GetLanguageBaseControls() As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try
            Dim strSQL As String
            strSQL = "SELECT  tblLanguageBasedControls.FORMID  [Form ID],  tblSecurityForm.FORM_NAME_New [Form Name], tblLanguageBasedControls.ControlType [Control Type], tblLanguageBasedControls.ControlName [Control Name], " & EnumLanguagConstants.ENGL_US.ToString & "," & EnumLanguagConstants.URDU_PK.ToString & ", " & EnumLanguagConstants.ARABIC_UAE.ToString & " " _
            & " FROM         tblLanguageBasedControls Left Outer JOIN tblSecurityForm ON tblLanguageBasedControls.FormID = tblSecurityForm.FORM_ID " _
            & " ORDER BY [Form Name], [Control Name]"

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("MyLanguageBasedControlList")
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



    Friend Shared Sub BuildActivityLog(ByVal Log As ActivityLog, ByVal trans As SqlTransaction)
        Try
            Dim strSQL As String

            strSQL = "INSERT INTO tblGLActivityLog([form_caption] ,[form_action],[user_id],[action_date],[log_ref],[ref_type] ,[log_group]) " _
            & " VALUES('" & Log.ScreenTitle.Trim.Replace("'", "''") & "', '" & Log.FormAction.Trim.Replace("'", "''") & "' , " & Log.UserID & " , getdate() , '" & Log.LogRef.Trim.Replace("'", "''") & "' , '" & Log.RefType.Trim.Replace("'", "''") & "' , '" & Log.LogGroup.Trim.Replace("'", "''") & "')"

            ''Execute SQL 
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Friend Shared Sub BuildSQLLog(ByVal Log As ActivityLog, ByVal trans As SqlTransaction, Optional ByVal MarkDataLog As Boolean = False)
        Try

            'Dim strSQL As String
            'strSQL = "INSERT INTO tblGLDMLLog  (dml_sql, log_date, user_id) " _
            '& " VALUES( '" & Log.SQL.Trim.Replace("'", "''") & "' , getdate()   , " & Log.UserID & ")"

            ' ''Execute SQL 
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)


            '------------------------------CR#160----------------------------
            Dim strSQL As String

            strSQL = "INSERT INTO tblGLDMLLog  (dml_sql, log_date, user_id) " _
           & " VALUES( '" & Log.SQL.Trim.Replace("'", "''") & "' , getdate()   , " & Log.UserID & ")"

            ''Execute SQL 
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

            If MarkDataLog = True Then '//Flag will be true only when we need to transfer server data to shop (Fyear&Satus,Company,COA)
                If G_IsSSBOn = True Then
                    If G_Candela_Version <> "1" Then  ' if not personal version
                        'strSQL = "INSERT INTO tblDataLog  ([time_stamp],[sql_type], [table_name] ,[sql_statement] ,[shop_id] ,[user_id], Shop_ids) " _                 'CR # 235   Rana Saeed
                        strSQL = "INSERT INTO tblDataLogTemp  ([time_stamp],[sql_type], [table_name] ,[sql_statement] ,[shop_id] ,[user_id], Shop_ids) " _
                                & " VALUES( getdate()  , '" & Log.SQLType.Trim.Replace("'", "''") & "', '" & Log.TableName.Trim.Replace("'", "''") & "' , '" & Log.SQL.Trim.Replace("'", "''") & "' , " & Log.ShopID & " , " & Log.UserID & ",'0')"
                        ''Execute SQL 
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                    End If
                End If
            End If



        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try

    End Sub



    Public Shared Function funExecuteScriptFile(ByVal ApplicationStartuoPath As String, ByVal ScriptCollection As Collection, ByVal dblSchemaVer As Double) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        ''Commit Transaction
        Dim trans As SqlTransaction = conn.BeginTransaction

        Dim strSQL As String

        Try


            For intIndex As Integer = 1 To ScriptCollection.Count

                Dim FileName As String = ApplicationStartuoPath & "\" & ScriptCollection.Item(intIndex)



                strSQL = System.IO.File.ReadAllText(FileName)
                'execute sql
                Call SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)


                'log to file
                Call funSaveToFile(ApplicationStartuoPath & "\" & "UpdateLog", strSQL, dblSchemaVer)


            Next intIndex


            strSQL = "Update tblGLConfiguration Set config_value ='" & dblSchemaVer & "' Where config_name='Schema_Version'"
            Call SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

            trans.Commit()


            'log to file
            Call funSaveToFile(ApplicationStartuoPath & "\" & "UpdateLog", strSQL, dblSchemaVer)

        Catch ex As Exception
            trans.Rollback()
            If conn.State = ConnectionState.Open Then conn.Close()
            Throw ex

        End Try
    End Function


    ' This function is used save grid contents into disk
    Public Shared Function funSaveToFile(ByVal FileName As String, ByVal strSql As String, ByVal dblVersion As Double) As Boolean
        Try

            funSaveToFile = True
            Dim strFilePath As String

            'System.IO.File.AppendAllText(FileName & ".Rtf", IO.FileMode.Append)
            System.IO.File.AppendAllText(FileName & ".Rtf", Environment.NewLine + strSql)       ' CR # 335

            funSaveToFile = True




        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ' Tariq Majeed Sheikh .. 
    ' This Function When Called Returns The Company Information .. 
    Public Shared Function setCompanyInfo(ByVal strLocationID As String) As DataTable

        Dim ObjDataAdapter As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String
            strSQL = " Select IsNull(location_name, '-') as CompanyName, IsNull(location_address, '-') as CompanyAddress, IsNull(location_phone, '-') as CompanyPhone, IsNull(location_fax, '-') as CompanyFax, IsNull(location_url, '-')as CompanyURL from tblGlDefLocation Where location_id = " & strLocationID


            ObjDataAdapter = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim myDataTable As New DataTable("ComapanyInfo")
            ObjDataAdapter.Fill(myDataTable)


            Return myDataTable

        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            ObjDataAdapter = Nothing

        End Try


    End Function

    Public Shared Function GetDataTable(ByVal strSql As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim ObjCon As SqlClient.SqlConnection
        Dim objDA As SqlClient.SqlDataAdapter
        Dim Objcmd As SqlClient.SqlCommand

        'ObjCon = New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If trans Is Nothing Then
            ObjCon = New SqlClient.SqlConnection(SQLHelper.CON_STR)
        Else
            ObjCon = trans.Connection
        End If

        Objcmd = New SqlClient.SqlCommand(strSql)

        Try

            Objcmd.CommandTimeout = 300
            Objcmd.Connection = ObjCon
            Objcmd.Transaction = trans

            objDA = New SqlClient.SqlDataAdapter
            objDA.SelectCommand = Objcmd

            Dim MyCollectionList As New DataTable
            objDA.Fill(MyCollectionList)

            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
            If trans Is Nothing Then
                If ObjCon.State = ConnectionState.Open Then ObjCon.Close()
                ObjCon.Dispose()
            End If

        End Try
    End Function


    Public Shared Function ReturnDataRow(ByVal strSql As String, Optional ByVal trans As SqlTransaction = Nothing) As DataRow
        Dim Objcmd As SqlClient.SqlCommand
        Dim objDA As New SqlDataAdapter
        Dim objDS As New DataSet
        Dim ObjCon As SqlClient.SqlConnection

        Try

            If trans Is Nothing Then
                ObjCon = New SqlClient.SqlConnection(SQLHelper.CON_STR)
            Else
                ObjCon = trans.Connection
            End If
            Objcmd = New SqlClient.SqlCommand(strSql)


            'Dim trans As SqlTransaction = conn.BeginTransaction
            Objcmd.Connection = ObjCon
            Objcmd.CommandType = CommandType.Text
            Objcmd.CommandText = strSql
            Objcmd.Transaction = trans
            objDA.SelectCommand = Objcmd
            objDA.Fill(objDS)

            If objDS.Tables(0).Rows.Count > 0 Then
                Return objDS.Tables(0).Rows(0)
            Else
                Return Nothing
            End If


        Catch ex As Exception
            Throw ex

        Finally

            objDA = Nothing
            If trans Is Nothing Then
                If ObjCon.State = ConnectionState.Open Then ObjCon.Close()
                ObjCon.Dispose()
            End If

        End Try

    End Function



    Public Shared Function ExecuteQuery(ByVal strSql As String, Optional ByVal strCondition As String = "") As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            ' Execute SQL ..
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, Nothing))


            ' Commit Transaction .. 
            trans.Commit()


            ' Return ..
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

    Public Shared Sub CreateMushRoomCommon()

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction

        Try
            Dim strSQL As String

            strSQL = "IF OBJECT_ID (N'dbo.tblMushroomCommon') IS NULL Begin " & _
                    " CREATE TABLE [dbo].[tblMushroomCommon]( " & _
                    "	[mushroom_common_id] [numeric](10, 0) IDENTITY(1,1) NOT FOR REPLICATION NOT NULL, " & _
                    "	[expiry_service] [numeric](10, 0) NULL, " & _
                    "	[membership_fee] [numeric](10, 0) NULL, " & _
                    "	[renewal_fee] [numeric](10, 0) NULL, " & _
                    "	[membership_card_expiry] [datetime] NULL, " & _
                    "	[psr_expiry] [numeric](10, 0) NULL, " & _
                    "	[shop_priority_date] [datetime] NULL, " & _
                    "	[shop_priority_validity] [datetime] NULL, " & _
                    "	[membership_duration] [numeric](10, 0) NULL, " & _
                    "	[Membership_Signature] [image] NULL, " & _
                    "	[LM] [numeric](10, 2) NULL, " & _
                    "	[TM] [numeric](10, 2) NULL, " & _
                    "	[label_type] [int] NULL, " & _
                    "	[production_product_detail] [bit] NULL, " & _
                    "	[STR_product_name_report] [bit] NULL, " & _
                    "	[replication_status] [bit] NULL, " & _
                    "	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL CONSTRAINT [MSmerge_df_rowguid_5C7685B9C20E4325AF061F1D94F543DD]  DEFAULT (newsequentialid()), " & _
                    " CONSTRAINT [PK_tblMushroomCommon] PRIMARY KEY CLUSTERED  " & _
                    " ( " & _
                    "	[mushroom_common_id] ASC " & _
                    " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " & _
                    " ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] " & _
                    " ALTER TABLE [dbo].[tblMushroomCommon]  WITH NOCHECK ADD  CONSTRAINT [repl_identity_range_517EEAFD_C8DF_4996_8B93_025A97CEF1FA] CHECK NOT FOR REPLICATION (([mushroom_common_id]>(1) AND [mushroom_common_id]<=(10001) OR [mushroom_common_id]>(10001) AND [mushroom_common_id]<=(20001))) " & _
                    " ALTER TABLE [dbo].[tblMushroomCommon] CHECK CONSTRAINT [repl_identity_range_517EEAFD_C8DF_4996_8B93_025A97CEF1FA]  End"

            ''Execute SQL 
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

            trans.Commit()

        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub

    Public Shared Function ExecuteReader(ByVal strSql As String, Optional ByVal strCondition As String = "") As SqlDataReader

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim DR As SqlDataReader
        Dim cmd As SqlCommand = New SqlCommand

        Try

            If (conn.State <> ConnectionState.Open) Then conn.Open()
            cmd.Connection = conn
            cmd.CommandText = strSql
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 500

            DR = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return DR

        Catch ex As Exception
            conn.Close()
            Throw ex
        End Try

    End Function

    Public Shared Function SETDataLogFlag()
      
        Try

            Dim strSQL As String

            strSQL = "SELECT config_value    From tblRCMSConfiguration where config_name='DataLogEntery'"

            Using objDR As SqlClient.SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then
                    While objDR.Read
                        G_IsSSBOn = Convert.ToBoolean(objDR.Item("config_value"))
                    End While

                End If

            End Using


            strSQL = "SELECT config_value   From tblRCMSConfiguration where config_name='version'"

            Using objDR As SqlClient.SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objDR.HasRows Then
                    While objDR.Read
                        G_Candela_Version = Convert.ToInt16(Utility.Utility.SymmetricEncryption.Decrypt(objDR.Item("config_value"), "f"))
                    End While

                End If

            End Using



        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Utility.Utility.SymmetricEncryption.Decrypt(GetSystemConfigurationValue("Version"), "f")

End Class
