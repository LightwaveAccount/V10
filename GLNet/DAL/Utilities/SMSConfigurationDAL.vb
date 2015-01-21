''/////////////////////////////////////////////////////////////////////////////////////////
''//                        SMS Utility 
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : SMSConfigurationDAl.vb           				                            
''// Programmer	     : Farooq-H 
''// Creation Date	 : 06-June-2013
''// Description     :   CR# 241
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by           CR#        Brief Description		
''//10-09-2013          Farooq-H            265         SMS: During Reading Mobile numbers for Customer/Suppliers system should pick required format.
''//------------------------------------------------------------------------------------
''/////////////////////////////////////////////////////////////////////////////////////////
Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility
Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.VisualBasic

Public Class SMSConfigurationDAL



    Public Function GetAll(Optional ByVal strCondition As String = "") As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try
            Dim strSQL As String
            strSQL = "SELECT     SMSConfig_id [SMSConfig_id], SMS_Code [SMS Code], Screen_Name [Screen Name], Action [Action], Mode [Mode], SMS_Recipient [SMS Recipient], " _
            & " Send_SMS [Send SMS], SMS [SMS], PhoneNumber [Phone Number]  " _
            & " FROM         tblGLSMSConfiguration "

            '& " ORDER BY SMS_Code   "
            If strCondition.ToString <> "" Then
                Dim STr() As String = strCondition.ToString.Split(";")
                Dim CodeString As String = ""
                For Each item As String In STr
                    If CodeString <> "" Then
                        CodeString = CodeString & ", '" & item.ToString & "'  "
                    Else
                        CodeString = "'" & item.ToString & "' "
                    End If
                Next
                strSQL = strSQL & " Where  Send_SMS = 'true'  and  SMS_Code in  ( " & CodeString.ToString & " )     "
            End If
            strSQL = strSQL & " ORDER BY SMS_Code   "

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable(EnumHashTableKeyConstants.GetSMSConfigurationList.ToString)
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

    Public Function Update(ByVal objSMSConfiguration As SMSConfiguration) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String
            strSQL = "UPDATE TblGLSMSConfiguration SET " _
            & " Send_SMS = N'" & objSMSConfiguration.SendSMS & "', " _
            & " SMS = '" & objSMSConfiguration.SMS.Trim.Replace("'", "''") & "' , " _
            & " PhoneNumber = '" & objSMSConfiguration.PhoneNumber.Trim.Replace("'", "''") & "'  " _
            & " WHERE SMSConfig_id = " & objSMSConfiguration.SMSConfig_id
            ''Execute SQL 
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


            ''SQL Statement Log
            objSMSConfiguration.ActivityLog.SQLType = "UPDATE"
            objSMSConfiguration.ActivityLog.TableName = "tblGLSMSConfiguration"
            objSMSConfiguration.ActivityLog.SQL = strSQL
            objSMSConfiguration.ActivityLog.ShopID = -1
            UtilityDAL.BuildSQLLog(objSMSConfiguration.ActivityLog, trans)

            ''Activity Log
            objSMSConfiguration.ActivityLog.FormAction = "Update"
            objSMSConfiguration.ActivityLog.LogRef = objSMSConfiguration.SMSConfig_id
            objSMSConfiguration.ActivityLog.RefType = "SMSConfig ID"
            UtilityDAL.BuildActivityLog(objSMSConfiguration.ActivityLog, trans)


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


    Public Function SMSSendToRecipients(Optional ByVal strCondition As String = "") As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try
            Dim strSQL As String
            strSQL = " SELECT     SMS_id , SMS_Number , SMS_Text , Send_Status   " _
                    & " FROM         tblGLSMSLog where Send_status =  'false' "
            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable(EnumHashTableKeyConstants.GetSMSConfigurationList.ToString)
            objDA.Fill(MyCollectionList)

            ''''''''''''''''''''''''''''''''''''''''
            'Send SMS branded or non branded '''
            '''''''''''''''''''''''''''''''''''''''

            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function
    'cr#265 by Farooq-H
    'Public Sub UpdateStatus(ByVal SMSId As String)
    Public Sub UpdateStatus(ByVal SMSId As String, ByVal SmsNotSendBit As Boolean)
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String
            'cr#265 by Farooq-H
            'strSQL = "UPDATE TblGLSMSLog SET " _
            '& " Send_Status = 'True' " _
            '& " WHERE SMS_id = '" & SMSId & "'"

            strSQL = "UPDATE TblGLSMSLog SET "
            If SmsNotSendBit = True Then
                strSQL = strSQL & " Send_Status = 'True' "
            Else
                strSQL = strSQL & " Send_Status = null "
            End If
            strSQL = strSQL & " WHERE SMS_id = '" & SMSId & "'"

            ''Execute SQL 
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))
            ' ''Commit Traction
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

    Public Function GetCustomerSupplierDetails(ByVal strInfoID As String) As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String
            strSQL = "SELECT tblGLContactDirectory.InfoId [InfoID] , tblGLContactDirectory.Account_ID ,  tblGLContactDirectory.contact_person [Person] ,tblGLContactDirectory.mobile [Mobile]  From tblGLContactDirectory  "
            strSQL = strSQL & " Where Account_Id = " & strInfoID.ToString & "  "
            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)
            Dim MyCollectionList As New DataTable("CustomerSupplierInfo")
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

    Public Sub SendSMS(ByVal StrCode As String, ByVal StrParameters As String, Optional ByVal SupplierCustomerID As Integer = 0, Optional ByVal ShopID As String = "")
        Try
            Dim strSQL As String
            If DAL.SystemConfigurationDAL.GetSystemConfigurationValue("SendSMS").ToString.ToUpper <> "TRUE" Then
                Exit Sub
            End If
            Dim code() As String = StrCode.Split(";")
            Dim objSmsDT As DataTable = GetAll(StrCode.ToString.Replace("_", "-"))
            Dim objCusctomerSupplierDT As DataTable = GetCustomerSupplierDetails(SupplierCustomerID.ToString)

            objSmsDT.PrimaryKey = New DataColumn() {objSmsDT.Columns("SMS Code")}
            'objCusctomerSupplierDT.PrimaryKey = New DataColumn() {objCusctomerSupplierDT.Columns("InfoID")}

            For Each cod As String In code
                ' phone numbers will be saved in an array because user can define more then one 
                ' phone number seperated by ;
                Dim PhoneNumber() As String
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '' Phone Number Manipulation of HeadOffice , Supplier and Customer ''
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ' here we are getting the information of Phone numbers 
                ' where Head office Phone number will be taken from tblGLsmsConfiguration table 
                ' and Customer and Suppliers phone number information is comming from Contact Directory  table 

                Dim dr As DataRow = objSmsDT.Rows.Find(cod.ToString.Replace("_", "-"))
                ' this the data Row in which we take the information of one SMS 
                If dr Is Nothing Then
                    'Exit For
                    Continue For
                End If

                If dr.Item("SMS Recipient").ToString = "Head Office" Then
                    'Getting the Mobile number details in case of Head office 
                    PhoneNumber = dr.Item("Phone Number").ToString().Split(";")

                ElseIf dr.Item("SMS Recipient").ToString = "Supplier" Then
                    'Getting the Mobile number details in case of Supplier , Customers or it can be 
                    'any record which is defined if Contact directory screen   
                    If objCusctomerSupplierDT.Rows.Count > 0 Then
                        If objCusctomerSupplierDT.Rows(0).Item("Mobile").ToString.Trim <> "" Then
                            PhoneNumber = objCusctomerSupplierDT.Rows(0).Item("Mobile").ToString.Split(";")
                        End If
                    Else
                        Continue For
                    End If
                Else
                    ' it is the case when there is no SMS recepient is defined this case is not possible 
                    ' but for handling the exceptions i defined this case 
                    'Exit For
                    Continue For
                End If
                ' if phone number is not available then skip this SMS record 
                If PhoneNumber.Length <= 0 Then
                    ' If PhoneNumber(0).Contains("") Then
                    Continue For
                    'End If
                End If

                ''''''''''''''''''''''''''''
                ''String ManiPulation'''''''
                ''''''''''''''''''''''''''''
                Dim StrSMS As String = ""
                If dr.Item("SMS").ToString = "" Then
                    ' if SMS text is not defined against the specific SMS Code then Skip this Record 
                    Continue For
                End If
                StrSMS = dr.Item("SMS").ToString
                Dim paramList() As String = StrParameters.Split(";")
                For Each item As String In paramList
                    Dim StrParam() As String = Split(item, "=", 2)
                    If StrSMS.Contains(StrParam(0)) = True Then
                        StrSMS = StrSMS.Replace(StrParam(0).Trim, StrParam(1))
                    End If
                Next
                '''''''''''''''''''''''''''''''''''''''''''''
                'Save the messages in table tblSMSLog '''''''
                ' '''''''''''''''''''''''''''''''''''''''''''

                'If PhoneNumber.Length > 0 Then

                For Each item As String In PhoneNumber
                    If item.ToString.Trim = "" Then
                        Continue For
                    End If
                    Dim conn As New SqlConnection(SQLHelper.CON_STR)
                    conn.Open()
                    Dim trans As SqlTransaction = conn.BeginTransaction
                    Try
                        strSQL = " insert into tblGLsmsLog  (sms_number , sms_text , send_status  ) " _
                               & " values ( N'" & item & "' , N'" & StrSMS & "' , N'false'  ) "
                        Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))
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
                Next
                strSQL = Nothing
                PhoneNumber = Nothing

            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
