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
''//------------------------------------------------------------------------------------
''/////////////////////////////////////////////////////////////////////////////////////////


Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility

Public Class GLAccountDAL

#Region "Public Functions and Procedures"

    Public Function GetAll(Optional ByVal strCondition As String = "", Optional ByVal strLevel As String = "", Optional ByVal strAccountType As String = "", Optional ByVal strConditionLevel As String = "") As DataTable
        Dim ObjDA As SqlClient.SqlDataAdapter

        Try

            Dim strSQL As String = ""


            ' First Level Accounts .. 
            If strLevel = "1" Then
                strSQL = " SELECT  coa_main_id AS AccountID, main_code AS AccountCode, main_title AS AccountName,'' AType FROM tblGlCOAMain "
                If strCondition <> "" Then
                    strSQL = strSQL & " WHERE main_type = '" & strCondition & "'"

                End If


            End If



            ' Second Level Accounts .. 
            If strLevel = "2" Then
                strSQL = " SELECT  main_sub_id AS AccountID, sub_code AS AccountCode, sub_title AS AccountName,'' AType FROM tblGlCOAMainSub "
                If strCondition <> "" Then
                    strSQL = strSQL & " WHERE coa_main_id = " & strCondition

                End If

            End If



            ' Thirld Level Accounts .. 
            If strLevel = "3" Then
                strSQL = " SELECT main_sub_sub_id AS AccountID, sub_sub_code AS AccountCode, sub_sub_title AS AccountName,'' AType FROM tblGlCOAMainSubSub "


                If strConditionLevel <> "" Then
                    If strConditionLevel = "1" Then
                        strCondition = " SELECT tblGlCOAMainSub.main_sub_id FROM tblGlCOAMain INNER JOIN tblGlCOAMainSub ON tblGlCOAMain.coa_main_id = tblGlCOAMainSub.coa_main_id WHERE (tblGlCOAMain.coa_main_id = " & strCondition & ")"

                    End If


                End If


                If strCondition <> "" Then
                    strSQL = strSQL & " WHERE main_sub_id In (" & strCondition & ")"

                End If

            End If




            ' Fourth Level Accounts .. 
            If strLevel = "4" Then
                strSQL = " SELECT coa_detail_id AS AccountID, detail_code AS AccountCode, detail_title AS AccountName,Account_Type AType FROM tblGlCOAMainSubSub INNER JOIN " _
                       & " tblGlCOAMainSubSubDetail ON tblGlCOAMainSubSub.main_sub_sub_id = tblGlCOAMainSubSubDetail.main_sub_sub_id " _
                       & " Where end_date Is NULL "


                If strConditionLevel <> "" Then
                    If strConditionLevel = "1" Then
                        'CR# 49
                        If strCondition <> "" Then
                            strCondition = " SELECT tblGlCOAMainSubSub.main_sub_sub_id FROM tblGlCOAMain INNER JOIN tblGlCOAMainSub ON tblGlCOAMain.coa_main_id = tblGlCOAMainSub.coa_main_id INNER JOIN tblGlCOAMainSubSub ON tblGlCOAMainSub.main_sub_id = tblGlCOAMainSubSub.main_sub_id WHERE (tblGlCOAMain.coa_main_id = " & strCondition & ")"
                        Else
                            strCondition = " SELECT tblGlCOAMainSubSub.main_sub_sub_id FROM tblGlCOAMain INNER JOIN tblGlCOAMainSub ON tblGlCOAMain.coa_main_id = tblGlCOAMainSub.coa_main_id INNER JOIN tblGlCOAMainSubSub ON tblGlCOAMainSub.main_sub_id = tblGlCOAMainSubSub.main_sub_id"
                        End If


                    ElseIf strConditionLevel = "2" Then
                        If strCondition <> "" Then
                            strCondition = " SELECT tblGlCOAMainSubSub.main_sub_sub_id FROM tblGlCOAMainSub INNER JOIN tblGlCOAMainSubSub ON tblGlCOAMainSub.main_sub_id = tblGlCOAMainSubSub.main_sub_id WHERE tblGlCOAMainSub.main_sub_id = " & strCondition
                        Else
                            strCondition = " SELECT tblGlCOAMainSubSub.main_sub_sub_id FROM tblGlCOAMainSub INNER JOIN tblGlCOAMainSubSub ON tblGlCOAMainSub.main_sub_id = tblGlCOAMainSubSub.main_sub_id "
                        End If

                    End If


                End If


                If strCondition <> "" Then
                    strSQL = strSQL & " AND tblGlCOAMainSubSubDetail.main_sub_sub_id In (" & strCondition & ")"

                End If

                If strAccountType <> "None" Then
                    strSQL = strSQL & " AND (tblGlCOAMainSubSub.account_type = '" & strAccountType & "') "
                End If



            End If




            strSQL = strSQL & " ORDER BY AccountCode "
            ObjDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)


            Dim MyCollectionList As New DataTable("AccountsTable")
            ObjDA.Fill(MyCollectionList)


            Return MyCollectionList

        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            ObjDA = Nothing

        End Try

    End Function

#End Region

End Class
