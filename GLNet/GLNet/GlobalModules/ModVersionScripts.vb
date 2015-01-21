''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela New
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : ModVersionScripts.vb           				                            
''// Programmer	     : 
''// Creation Date	 : 
''// Description     : 
''// Function List   : 								                                    
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''// 19-Nov-2009       Fahad               Change encryption only if values for Candela IDs 
''//                                       are not blank (CR# 314) 
''// 27-Jan-2011       Abdul Jabbar        CR#124 Lightwave New Release Version 3022
''// 22-Jan-2012       Abdul Jabbar        CR#195 Lightwave New Release Version 3025
''// 01-Jun-2012       Abdul Jabbar        Lightwave New Release Version 3026
''// 27-Jul-2012       Abdul Jabbar        CR#216 Lightwave New Release Version 3027
''// 21-May-2013       Abdul Jabbar        CR#236 Lightwave New Release Version 3028
''// 07-Jul-2013       Abdul Jabbar        CR#263 Lightwave New Release Version 3029
''// 12-Sep-2013       Abdul Jabbar        CR#273 Lightwave New Release Version 3010
''// 26-Sep-2013       Abdul Jabbar        CR#281 GL Release: Some issues on installing new release
''// 05-Sep-2014       Abdul Jabbar        CR#327 Lightwave Release : New Release Version 3.0.3.1
''/////////////////////////////////////////////////////////////////////////////////////////

Imports DAL
Imports Model
Imports System.Collections.Specialized
Imports System.Data
Imports System.Data.SqlClient
Imports Utility.Utility
Module ModVersionScripts


    Public Function funVersionUpdate(ByVal dblSchemaVer As Double) As Boolean

        'Check the version no that needs to be updated
        'each version will have 2 BLOCKS one to EXECUTE SQL and other COPY REPORTS
        funVersionUpdate = True

        Select Case dblSchemaVer

            Case 3001
                '========================================================================================================
                If Not funUpdateVersion3001() Then funVersionUpdate = False : Exit Function

            Case 3002
                '========================================================================================================
                If Not funUpdateVersion3002() Then funVersionUpdate = False : Exit Function

            Case 3003
                '========================================================================================================
                If Not funUpdateVersion3003() Then funVersionUpdate = False : Exit Function

            Case 3011
                '========================================================================================================
                If Not funUpdateVersion3011() Then funVersionUpdate = False : Exit Function

            Case 3020
                '========================================================================================================
                If Not funUpdateVersion3020() Then funVersionUpdate = False : Exit Function

            Case 3021
                '========================================================================================================
                If Not funUpdateVersion3021() Then funVersionUpdate = False : Exit Function

            Case 3022 'CR#124
                '========================================================================================================
                If Not funUpdateVersion3022() Then funVersionUpdate = False : Exit Function

            Case 3023
                '========================================================================================================
                If Not funUpdateVersion3023() Then funVersionUpdate = False : Exit Function

            Case 3024
                '========================================================================================================
                If Not funUpdateVersion3024() Then funVersionUpdate = False : Exit Function

            Case 3025 'CR#195
                '========================================================================================================
                If Not funUpdateVersion3025() Then funVersionUpdate = False : Exit Function

            Case 3026
                '========================================================================================================
                If Not funUpdateVersion3026() Then funVersionUpdate = False : Exit Function


            Case 3027 'CR#216
                '========================================================================================================
                If Not funUpdateVersion3027() Then funVersionUpdate = False : Exit Function

            Case 3028 'CR#236
                '========================================================================================================
                If Not funUpdateVersion3028() Then funVersionUpdate = False : Exit Function

            Case 3029 'CR#263
                '========================================================================================================
                If Not funUpdateVersion3029() Then funVersionUpdate = False : Exit Function

            Case 3030 'CR#273
                '========================================================================================================
                If Not funUpdateVersion3030() Then funVersionUpdate = False : Exit Function

            Case 3031 'CR#327
                '========================================================================================================
                If Not funUpdateVersion3031() Then funVersionUpdate = False : Exit Function

            Case 3032
                '========================================================================================================
                If Not funUpdateVersion3032() Then funVersionUpdate = False : Exit Function

        End Select


    End Function

    Private Function funCopyReport(ByVal strFolderName As String, ByVal strFileName As String, Optional ByVal blnisNew As Boolean = False) As Boolean
        Try


            'Dim fso As New   FileSystemObject
            Dim strAppPath As String

            If Right(Application.StartupPath, 1) = "\" Then
                strAppPath = Application.StartupPath
            Else
                strAppPath = Application.StartupPath & "\"
            End If

            Dim strSourceFile As String
            Dim strDestinationFile As String
            Dim strDestinationOldFile As String

            ''If Folder not Exist then Create Folders
            'If Not IO.Directory.Exists(strAppPath & "Reports\" & strFolderName) Then
            '    IO.Directory.CreateDirectory(strAppPath & "Reports\" & strFolderName)
            'End If

            If Not IO.Directory.Exists(strAppPath & strFolderName) Then
                IO.Directory.CreateDirectory(strAppPath & strFolderName)
            End If
            'strSourceFile = strAppPath & "Release\" & strFolderName & strFileName
            'strDestinationFile = strAppPath & "Reports\" & strFolderName & strFileName
            'strDestinationOldFile = strAppPath & "OldReports\" & strFolderName & strFileName
            strSourceFile = strAppPath & "Release\" & strFolderName & strFileName
            strDestinationFile = strAppPath & strFolderName & strFileName
            strDestinationOldFile = strAppPath & "OldReports\" & strFolderName & strFileName

            ' Check PhysicalSession Folder Exit Or Not
            If IO.File.Exists(strSourceFile) Then

                If IO.File.Exists(strDestinationFile) Then
                    If Not IO.Directory.Exists(strAppPath & "OldReports\" & strFolderName) Then
                        IO.Directory.CreateDirectory(strAppPath & "OldReports\" & strFolderName)
                    End If

                    Try 'rpt r not getting Refresh when Old reports are ReadOnly.

                        Dim dfold As IO.FileInfo = My.Computer.FileSystem.GetFileInfo(strDestinationOldFile)
                        dfold.IsReadOnly = False

                    Catch ex As Exception
                    End Try
                    Try
                        Call IO.File.Copy(strDestinationFile, strDestinationOldFile, True)
                        IO.File.Delete(strDestinationFile)
                    Catch ex As Exception
                    End Try

                End If

                If Not IO.Directory.Exists(strAppPath & "Reports\Cheques") Then
                    IO.Directory.CreateDirectory(strAppPath & "Reports\Cheques\")
                End If

                Call IO.File.Copy(strSourceFile, strDestinationFile, True)
                funCopyReport = True

                'CR#281
            Else
                Throw New Exception("Report File doesn't exist")

            End If
            Exit Function
        Catch ex As Exception
            'CR#281
            'funCopyReport = False
            Throw ex

        End Try

    End Function
    '//-------------------------------------------------------------------------------//
    '// GetVersionsDelta()
    '//-------------------------------------------------------------------------------//
    '// Input/Output    : dblSchemaVersion As Double, dblApplicaitonVersion As Double
    '// Return Type     : Double() array
    '// Author          : Abdul Jabbar
    '// Date            : 03 Feb,2010
    '//
    '// Description     : This function will return the Array of differance of schema versions that needs to be updated on server
    '//                   it will return blank array if no schema version needs to be updated
    '//                   it will take two arguments SchemaVersion and Application/ReleaseVersion
    '//-------------------------------------------------------------------------------//
    Public Function GetVersionsDelta(ByVal dblSchemaVersion As Double, ByVal dblApplicaitonVersion As Double) As Double()

        Const CurrVersion As Integer = 3000 'First Version of Application from where Release Mgt started

        Try

            'declare the null array
            Dim arrVersions() As Double

            'Set the same message for the old versions
            If dblSchemaVersion < 3000 Then

                Throw New Exception("Current Schema version is not compatible with the Application version!", New Exception("Version Not Compatible"))

                'return null array
                ReDim arrVersions(0)
                Return arrVersions


                'check if schema needs to be updated then calculate the dalta and return the array
            ElseIf dblSchemaVersion < dblApplicaitonVersion Then

                Dim dblDiff As Double
                Dim intCounter As Integer

                'calculate the differance
                dblDiff = dblApplicaitonVersion - dblSchemaVersion
                'redefine the array
                ReDim arrVersions(dblDiff)


                'build the versions array
                For intCounter = 0 To dblDiff - 1
                    If dblSchemaVersion + intCounter + 1 > CurrVersion Then
                        arrVersions(intCounter) = dblSchemaVersion + intCounter + 1

                    End If

                Next

                'return the versions array
                Return arrVersions
            Else

                'return the null array
                ReDim arrVersions(0)
                GetVersionsDelta = arrVersions

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    '//-------------------------------------------------------------------------------//
    '// funUpdateVersion6011()
    '//-------------------------------------------------------------------------------//
    '///////////////////A Jabbar Jan,2010 Release 3.0.0.1/////////////////////////////////////////////////////////
    Private Function funUpdateVersion3001() As Boolean

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3001

            'if replication is done and current machine is replicated shop then don't execute the SQL Script to update the version

            'Have ToDo#
            'If Not frmLogin.IsPublisher() And frmLogin.IsReplicationDone() Then
            '    blnExecuteSQL = False
            'Else
            '    blnExecuteSQL = True
            'End If

            blnExecuteSQL = True

            ScriptCollection.Add("Release\" & "3001_1_TblrptAccLedgerAlter.sql")
            'ScriptCollection.Add("Release\" & "3001_1_AddTblMushroomCommon.sql")
            'ScriptCollection.Add("Release\" & "3001_1_UpdateGLConfiguration_VerKey.sql")

            '======================
            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3001 = False
                    Exit Function
                    'CR#281 End
                End Try

                ScriptCollection = Nothing

            End If

            '======================
            ' ''   COPY REPORTS
            Try

                If System.IO.Directory.Exists(Application.StartupPath & "\Release") Then

                    '//Call funCopyReport("Purchase", "\rptGLCashFlow.rpt", True)
                    '//Call funCopyReport("", "\rptGLCashFlow.rpt", True)

                    Call funCopyReport("Reports", "\rptGLCashFlow.rpt", True)
                    Call funCopyReport("Reports", "\rptGLCashFlow1.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_Short.rpt", True)

                End If

            Catch ex As Exception
                'CR#281---Start
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                ShowErrorMessage("Error while version update process. Some report is missing. Please review release log file for detail")
                funUpdateVersion3001 = False
                Exit Function
                'CR#281---End
            End Try

            funUpdateVersion3001 = True


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function
    '///////////////////A Jabbar April 7,2010 Release 3.0.0.2/////////////////////////////////////////////////////////
    Private Function funUpdateVersion3002() As Boolean

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3002

            'if replication is done and current machine is replicated shop then don't execute the SQL Script to update the version

            'Have ToDo#
            'If Not frmLogin.IsPublisher() And frmLogin.IsReplicationDone() Then
            '    blnExecuteSQL = False
            'Else
            '    blnExecuteSQL = True
            'End If
             blnExecuteSQL = True

            ScriptCollection.Add("Release\" & "3002_1_Drop_sp_Rpt_AgedReceivableLedger.sql")
            ScriptCollection.Add("Release\" & "3002_2_Create_sp_Rpt_AgedReceivableLedger.sql")
            ScriptCollection.Add("Release\" & "3002_3_Drop_sp_Rpt_AgedReceivable.sql")
            ScriptCollection.Add("Release\" & "3002_4_Create_sp_Rpt_AgedReceivable.sql")
            ScriptCollection.Add("Release\" & "3002_5_Drop_sp_Rpt_AgedPayableLedger.sql")
            ScriptCollection.Add("Release\" & "3002_6_Drop_sp_Rpt_AgedPayable.sql")
            ScriptCollection.Add("Release\" & "3002_7_Create_sp_Rpt_AgedPayableLedger.sql")
            ScriptCollection.Add("Release\" & "3002_8_Create_sp_Rpt_AgedPayable.sql")
            ScriptCollection.Add("Release\" & "3002_11_a_DropFunc_GetFormGLID.sql")
            ScriptCollection.Add("Release\" & "3002_12_a_CreateFunc_GetFormGLID.sql")
            ScriptCollection.Add("Release\" & "3002_9_Add_SecurityPrintCOA.sql")
            ScriptCollection.Add("Release\" & "3002_10_a_DropConstraints_voucher.sql")
            ScriptCollection.Add("Release\" & "3002_10_b_DropConstraints_voucher.sql")

            '======================
            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3002 = False
                    Exit Function
                    'CR#281 End
                End Try

                ScriptCollection = Nothing

            End If

            '======================
            ' ''   COPY REPORTS
            If System.IO.Directory.Exists(Application.StartupPath & "\Release") Then

                'Removing Readonly attribute of report files

                Try

                    Dim Path As String = Application.StartupPath & "\Reports"

                    For Each fileName As String In System.IO.Directory.GetFiles(Path)
                        Dim fileInfo As New System.IO.FileInfo(fileName)
                        ' alternatively you can use this statement 
                        fileInfo.IsReadOnly = False
                    Next

                    Call funCopyReport("Reports", "\rptAgeingPayable.rpt", True)
                    Call funCopyReport("Reports", "\rptAgeingReceivable.rpt", True)
                    Call funCopyReport("Reports", "\rptAgeingPayableLedgerBase.rpt", True)
                    Call funCopyReport("Reports", "\rptAgeingReceivableLedgerBase.rpt", True)
                    Call funCopyReport("Reports", "\rptPostDatedCheques.rpt", True)
                    Call funCopyReport("Reports", "\rptChartofAccounts.rpt", True)

                Catch ex As Exception
                    'CR#281---Start
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    ShowErrorMessage("Error while version update process. Some report is missing. Please review release log file for detail")
                    funUpdateVersion3002 = False
                    Exit Function
                    'CR#281---End
                End Try



            End If

            funUpdateVersion3002 = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function
    Public Function ChangeEncryption(ByRef strMessageLog As String) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction
        Dim intRecordsAffected As Integer

        Try

            Dim strSQL As String
            Dim strTemp As String

            strSQL = "SELECT config_value FROM tblGlConfiguration WHERE config_name = 'IsEncryptionUpdated'"
            Dim strIsEncryptionUpdated As String = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing)

            If strIsEncryptionUpdated = "True" Then
                strMessageLog = "" ''Must return Empty String
                '"Encryption has already been changed."
                Exit Function
            End If


            '********************************************
            ' Changing Encryption for GL IDs
            '********************************************
            strMessageLog &= "Changing encryption for GL IDs ..." & vbCrLf

            strSQL = "SELECT * From tblGlConfiguration " _
                    & "WHERE (config_name IN ('GL_ID', 'GL_ID1', 'GL_ID2'))"

            Dim dtCandelaIDs As New DataTable

            dtCandelaIDs = New DAL.DALReports().GetDataTable(strSQL, trans)

            Dim strGLID As String = Utility.Utility.DecryptWithALP(dtCandelaIDs.Rows(0)("config_value").ToString)
            Dim strGLID1 As String = Utility.Utility.DecryptWithALP(dtCandelaIDs.Rows(1)("config_value").ToString)
            Dim strGLID2 As String = Utility.Utility.DecryptWithALP(dtCandelaIDs.Rows(2)("config_value").ToString)

            If strGLID <> "" Then
                strGLID = SymmetricEncryption.Encrypt(strGLID, "f")
                strSQL = "UPDATE tblGlConfiguration SET " _
                      & " config_value = '" & strGLID & "'" _
                      & " WHERE config_name = 'GL_ID'"

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing, intRecordsAffected)

                If intRecordsAffected > 0 Then
                    strMessageLog &= "GL_ID updated successfully" & vbCrLf
                Else
                    strMessageLog &= "Configuration value not found" & vbCrLf
                End If
            End If


            If strGLID1 <> "" Then
                strGLID1 = SymmetricEncryption.Encrypt(strGLID1, "f")
                strSQL = "UPDATE tblGlConfiguration SET " _
                               & " config_value = '" & strGLID1 & "'" _
                               & " WHERE config_name = 'GL_ID1'"

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing, intRecordsAffected)

                If intRecordsAffected > 0 Then
                    strMessageLog &= "GL_ID1 updated successfully" & vbCrLf
                Else
                    strMessageLog &= "Configuration value not found" & vbCrLf
                End If
            End If

            If strGLID2 <> "" Then
                strGLID2 = SymmetricEncryption.Encrypt(strGLID2, "f")
                strSQL = "UPDATE tblGlConfiguration SET " _
                                & " config_value = '" & strGLID2 & "'" _
                                & " WHERE config_name = 'GL_ID2'"

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing, intRecordsAffected)

                If intRecordsAffected > 0 Then
                    strMessageLog &= "GL_ID2 updated successfully" & vbCrLf
                Else
                    strMessageLog &= "Configuration value not found" & vbCrLf
                End If
            End If
            strMessageLog &= "Password encrytpion changed successfully" & vbCrLf

            strSQL = "INSERT INTO tblGlConfiguration (config_no, config_name, config_value) " _
               & " VALUES (24,'IsEncryptionUpdated','True') "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing, intRecordsAffected)
            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            strMessageLog &= "Conversion aborted."
            Throw ex
        End Try
    End Function
    '///////////////////A Jabbar July 08,2010 Release 3.0.0.3/////////////////////////////////////////////////////////
    Private Function funUpdateVersion3003() As Boolean

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3003

            'if replication is done and current machine is replicated shop then don't execute the SQL Script to update the version

            'Have ToDo# 
            'If Not frmLogin.IsPublisher() And frmLogin.IsReplicationDone() Then
            '    blnExecuteSQL = False
            'Else
            '    blnExecuteSQL = True
            'End If
            blnExecuteSQL = True

            ScriptCollection.Add("Release\" & "3003_1_DropTrigger.sql")
            ScriptCollection.Add("Release\" & "3003_2_Trig_GLLoc_FYear.sql")
            ScriptCollection.Add("Release\" & "3003_3_Alter_sp_Rpt_AgedPayable.sql")
            ScriptCollection.Add("Release\" & "3003_4_Alter_sp_Rpt_AgedPayableLedger.sql")
            ScriptCollection.Add("Release\" & "3003_5_Alter_sp_Rpt_AgedReceivable.sql")
            ScriptCollection.Add("Release\" & "3003_6_Alter_sp_Rpt_AgedReceivableLedger.sql")
            '======================
            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3003 = False
                    Exit Function
                    'CR#281 End
                End Try

                ScriptCollection = Nothing

            End If

            '======================
            ' ''   COPY REPORTS
            If System.IO.Directory.Exists(Application.StartupPath & "\Release") Then

                'Removing Readonly attribute of report files

                Try

                    Dim Path As String = Application.StartupPath & "\Reports"

                    For Each fileName As String In System.IO.Directory.GetFiles(Path)
                        Dim fileInfo As New System.IO.FileInfo(fileName)
                        ' alternatively you can use this statement 
                        fileInfo.IsReadOnly = False
                    Next

                    Call funCopyReport("Reports", "\rptGLPLNotes.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVoucherMulti.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVoucherMulti_Short.rpt", True)
                    Call funCopyReport("Reports", "\rptProftAndLossStatement_Formated.rpt", True)
                    Call funCopyReport("Reports", "\rptSalesTax.rpt", True)

                Catch ex As Exception
                    'CR#281---Start
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    ShowErrorMessage("Error while version update process. Some report is missing. Please review release log file for detail")
                    funUpdateVersion3003 = False
                    Exit Function
                    'CR#281---End
                End Try


            End If

            funUpdateVersion3003 = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function
    '///////////////////A Jabbar July 2010 Release 3.0.1.1 Just Fix/////////////////////////////////////////////////////////
    Private Function funUpdateVersion3011() As Boolean

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3011

            'if replication is done and current machine is replicated shop then don't execute the SQL Script to update the version

            'Have ToDo# 
            'If Not frmLogin.IsPublisher() And frmLogin.IsReplicationDone() Then
            '    blnExecuteSQL = False
            'Else
            blnExecuteSQL = True
            'End If

            '======================
            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3011 = False
                    Exit Function
                    'CR#281 End
                End Try

                ScriptCollection = Nothing

            End If

            '======================
            ' ''   COPY REPORTS
            'No Report

            funUpdateVersion3011 = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function

    '///////////////////A Jabbar Oct 20,2010 Release 3.0.2.0/////////////////////////////////////////////////////////
    Private Function funUpdateVersion3020() As Boolean

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3020

            'if replication is done and current machine is replicated shop then don't execute the SQL Script to update the version

            'Have ToDo# 
            'If Not frmLogin.IsPublisher() And frmLogin.IsReplicationDone() Then
            '    blnExecuteSQL = False
            'Else
            blnExecuteSQL = True
            'End If

            ScriptCollection.Add("Release\" & "3020_1_ALter_sp_Rpt_AgedReceivableLedger.sql")
            ScriptCollection.Add("Release\" & "3020_2_Alter_sp_Rpt_AgedPayableLedger.sql")
            ScriptCollection.Add("Release\" & "3020_3_CR#40_1_TblrptGLPLNotesDetail.sql")
            ScriptCollection.Add("Release\" & "3020_4_CR#40_2_DropVwGLPLNotesCurrDtl_a.sql")
            ScriptCollection.Add("Release\" & "3020_5_CR#40_2_CreateVwGLPLNotesCurrDtl_b.sql")
            ScriptCollection.Add("Release\" & "3020_6_CR#40_3_DropVwGLPLNotesPrevDtl_a.sql")
            ScriptCollection.Add("Release\" & "3020_7_CR#40_3_CreateVwGLPLNotesPrevDtl_b.sql")
            ScriptCollection.Add("Release\" & "3020_8_CR#40_4_VwPLNotesDetail_a.sql")
            ScriptCollection.Add("Release\" & "3020_9_CR#40_5_VwPLNotesDetail_b.sql")
            ScriptCollection.Add("Release\" & "3020_9_CR#87_VTypeSecurityTuples.sql")
            ScriptCollection.Add("Release\" & "3020_10_CR#88_VDateSecurityTuples.sql")
            '======================
            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3020 = False
                    Exit Function
                    'CR#281 End
                End Try

                ScriptCollection = Nothing

            End If

            '======================
            ' ''   COPY REPORTS
            If System.IO.Directory.Exists(Application.StartupPath & "\Release") Then

                'Removing Readonly attribute of report files

                Try

                    Dim Path As String = Application.StartupPath & "\Reports"

                    For Each fileName As String In System.IO.Directory.GetFiles(Path)
                        Dim fileInfo As New System.IO.FileInfo(fileName)
                        ' alternatively you can use this statement 
                        fileInfo.IsReadOnly = False
                    Next


                    Call funCopyReport("Reports", "\rptAgeingPayableLedgerBase.rpt", True)
                    Call funCopyReport("Reports", "\rptAgeingReceivableLedgerBase.rpt", True)
                    Call funCopyReport("Reports", "\rptGLPLNotes.rpt", True)
                    Call funCopyReport("Reports", "\rptGLPLNotesDetail.rpt", True)

                Catch ex As Exception

                    'CR#281---Start
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    ShowErrorMessage("Error while version update process. Some report is missing. Please review release log file for detail")
                    funUpdateVersion3020 = False
                    Exit Function
                    'CR#281---End

                End Try


            End If

            funUpdateVersion3020 = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function
    '///////////////////A Jabbar Oct 20,2010 Release 3.0.2.1/////////////////////////////////////////////////////////
    Private Function funUpdateVersion3021() As Boolean

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3021

            'if replication is done and current machine is replicated shop then don't execute the SQL Script to update the version

            'Have ToDo# 
            'If Not frmLogin.IsPublisher() And frmLogin.IsReplicationDone() Then
            '    blnExecuteSQL = False
            'Else
            blnExecuteSQL = True
            'End If

            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3021 = False
                    Exit Function
                    'CR#281 End
                End Try

                ScriptCollection = Nothing

            End If

            '======================
            'No Report

            funUpdateVersion3021 = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function
    '///////////////////A Jabbar Jan 25,2011 Release 3.0.2.2/////////////////////////////////////////////////////////
    Private Function funUpdateVersion3022() As Boolean

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3022

            'if replication is done and current machine is replicated shop then don't execute the SQL Script to update the version

            'If Not frmLogin.IsPublisher() And frmLogin.IsReplicationDone() Then
            '    blnExecuteSQL = False
            'Else
            blnExecuteSQL = True
            '    End If

            ScriptCollection.Add("Release\" & "3022_1_CR#116_TblrptAccLedger_CommentsLength.sql")

            ScriptCollection.Add("Release\" & "3022_2_CR113_1H_Drop_vwGLPLNotesCurrentDtlFinal.sql")
            ScriptCollection.Add("Release\" & "3022_3_CR113_2H_Create_vwGLPLNotesCurrentDtlFinal.sql")

            ScriptCollection.Add("Release\" & "3022_4_CR113_3H_Drop_vwGLPLNotesPrevDtlFinal.sql")
            ScriptCollection.Add("Release\" & "3022_13_CR113_4H_Create_vwGLPLNotesPrevDtlFinal.sql")

            ScriptCollection.Add("Release\" & "3022_5_CR113_5H_Drop_vwGLPLNotesDetail.sql")
            ScriptCollection.Add("Release\" & "3022_6_CR113_6H_Create_vwGLPLNotesDetail.sql")

            ScriptCollection.Add("Release\" & "3022_7_CR108_1H_Drop_sp_Rpt_AgedPayableLedger.sql")
            ScriptCollection.Add("Release\" & "3022_9_CR108_2H_Create_sp_Rpt_AgedPayableLedger.sql")

            ScriptCollection.Add("Release\" & "3022_8_CR108_1H_Drop_sp_Rpt_AgedReceivableLedger.sql")
            ScriptCollection.Add("Release\" & "3022_10_CR108_2H_Create_sp_Rpt_AgedReceivableLedger.sql")

            ScriptCollection.Add("Release\" & "3022_11_CR#106_GLConfigKey_VrptSize.sql")
            ScriptCollection.Add("Release\" & "3022_12_CR#114_RA_SchemaSnapshot_SecurityScript.sql")


            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3022 = False
                    Exit Function
                    'CR#281 End
                End Try

                ScriptCollection = Nothing

            End If

            '======================
            ' ''   COPY REPORTS
            If System.IO.Directory.Exists(Application.StartupPath & "\Release") Then

                'Removing Readonly attribute of report files

                Try

                    Dim Path As String = Application.StartupPath & "\Reports"

                    For Each fileName As String In System.IO.Directory.GetFiles(Path)
                        Dim fileInfo As New System.IO.FileInfo(fileName)
                        ' alternatively you can use this statement 
                        fileInfo.IsReadOnly = False
                    Next

                    Call funCopyReport("Reports", "\rptAgeingPayableLedgerBase.rpt", True)
                    Call funCopyReport("Reports", "\rptAgeingReceivableLedgerBase.rpt", True)
                    Call funCopyReport("Reports", "\rptGlTrailBalance.rpt", True)

                Catch ex As Exception

                    'CR#281---Start
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    ShowErrorMessage("Error while version update process. Some report is missing. Please review release log file for detail")
                    funUpdateVersion3022 = False
                    Exit Function
                    'CR#281---End

                End Try


            End If

            funUpdateVersion3022 = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function
    Private Function funUpdateVersion3023() As Boolean

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3023

            'if replication is done and current machine is replicated shop then don't execute the SQL Script to update the version

            'If Not frmLogin.IsPublisher() And frmLogin.IsReplicationDone() Then
            '    blnExecuteSQL = False
            'Else
            blnExecuteSQL = True
            'End If

            ScriptCollection.Add("Release\" & "3023_AJ_CR#119_AutoVoucherSorting.sql")
            ScriptCollection.Add("Release\" & "3023_AJ_CR125_1_Profit&Loss_Drop_vwGLPLNotesDetail.sql.sql")
            ScriptCollection.Add("Release\" & "3023_AJ_CR125_2_Profit&Loss_Create_vwGLPLNotesDetail.sql")
            ScriptCollection.Add("Release\" & "3023_AK_Create_tbGLCustomerInformation.sql")
            ScriptCollection.Add("Release\" & "3023_AK_GL_CustomerInfo.sql")
            ScriptCollection.Add("Release\" & "3023_AK_GL_CustomerInfo_Key.sql")
            ScriptCollection.Add("Release\" & "3023_AK_GL_CustomerInfo_SecurityForm.sql")


            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3023 = False
                    Exit Function
                    'CR#281 End
                End Try

                ScriptCollection = Nothing

            End If

            '======================
            ' ''   COPY REPORTS
            If System.IO.Directory.Exists(Application.StartupPath & "\Release") Then

                'Removing Readonly attribute of report files

                Try

                    Dim Path As String = Application.StartupPath & "\Reports"

                    For Each fileName As String In System.IO.Directory.GetFiles(Path)
                        Dim fileInfo As New System.IO.FileInfo(fileName)
                        ' alternatively you can use this statement 
                        fileInfo.IsReadOnly = False
                    Next

                    Call funCopyReport("Reports", "\rptGlVouchersingle_WithoutSorting.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_Short_WithoutSorting.rpt", True)

                Catch ex As Exception

                    'CR#281---Start
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    ShowErrorMessage("Error while version update process. Some report is missing. Please review release log file for detail")
                    funUpdateVersion3023 = False
                    Exit Function
                    'CR#281---End

                End Try


            End If

            funUpdateVersion3023 = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try



    End Function

    Private Function funUpdateVersion3024() As Boolean

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3024

            'if replication is done and current machine is replicated shop then don't execute the SQL Script to update the version

            'If Not frmLogin.IsPublisher() And frmLogin.IsReplicationDone() Then
            '    blnExecuteSQL = False
            'Else
            blnExecuteSQL = True
            'End If

            ScriptCollection.Add("Release\" & "3024_AK_GLCR#165_ShowVoucherDesc_ConfigurationKey.sql")
            ScriptCollection.Add("Release\" & "3024_RA_141_GLCongirationScript.sql")


            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3024 = False
                    Exit Function
                    'CR#281 End
                End Try

                ScriptCollection = Nothing

            End If

            '======================
            ' ''   COPY REPORTS
            If System.IO.Directory.Exists(Application.StartupPath & "\Release") Then

                'Removing Readonly attribute of report files

                Try

                    Dim Path As String = Application.StartupPath & "\Reports"

                    For Each fileName As String In System.IO.Directory.GetFiles(Path)
                        Dim fileInfo As New System.IO.FileInfo(fileName)
                        ' alternatively you can use this statement 
                        fileInfo.IsReadOnly = False
                    Next

                    Call funCopyReport("Reports", "\rptGlVouchersingle_Short_WithoutSorting_withDesc.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_Short_withDesc.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_WithoutSorting_withDesc.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_withDesc.rpt", True)
                    Call funCopyReport("Reports", "\rptAccountLedger.rpt", True)

                Catch ex As Exception
                    'CR#281---Start
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    ShowErrorMessage("Error while version update process. Some report is missing. Please review release log file for detail")
                    funUpdateVersion3024 = False
                    Exit Function
                    'CR#281---End
                End Try


            End If

            funUpdateVersion3024 = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function

    Private Function funUpdateVersion3025() As Boolean

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3025

            'if replication is done and current machine is replicated shop then don't execute the SQL Script to update the version

            'Commented by Abdul Jabbar no need to check replicated shop as GL release runs only in head office
            'If Not frmLogin.IsPublisher() And frmLogin.IsReplicationDone() Then
            '    blnExecuteSQL = False
            'Else
            '    blnExecuteSQL = True
            'End If

            blnExecuteSQL = True


            ScriptCollection.Add("Release\" & "3025_173_AJ_LicenseExpiryDate_key.sql")
            ScriptCollection.Add("Release\" & "3025_AJ_175_Alter_vwGLPLNotes.sql")
            ScriptCollection.Add("Release\" & "3025_AJ_2_175_TblrptGLPLNotes_Notes_Groups.sql")
            ScriptCollection.Add("Release\" & "3025_AK_186_Altersp_PostDatedCheques.sql")


            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3025 = False
                    Exit Function
                    'CR#281 End

                End Try

                ScriptCollection = Nothing

            End If

            '======================
            ' ''   COPY REPORTS
            If System.IO.Directory.Exists(Application.StartupPath & "\Release") Then

                'Removing Readonly attribute of report files

                Try

                    Dim Path As String = Application.StartupPath & "\Reports"

                    For Each fileName As String In System.IO.Directory.GetFiles(Path)
                        Dim fileInfo As New System.IO.FileInfo(fileName)
                        ' alternatively you can use this statement 
                        fileInfo.IsReadOnly = False
                    Next

                    Call funCopyReport("Reports", "\rptGlVouchersingle.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_Short.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_Short_withDesc.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_Short_WithoutSorting.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_Short_WithoutSorting_withDesc.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_withDesc.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_WithoutSorting_withDesc.rpt", True)
                    Call funCopyReport("Reports", "\rptPostDatedCheques.rpt", True)
                    Call funCopyReport("Reports", "\rptGLCashFlow1.rpt", True)
                    Call funCopyReport("Reports", "\rptGLPLNotes.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_WithoutSorting.rpt", True)

                Catch ex As Exception
                    'CR#281---Start
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    ShowErrorMessage("Error while version update process. Some report is missing. Please review release log file for detail")
                    funUpdateVersion3025 = False
                    Exit Function
                    'CR#281---End
                End Try


            End If

            funUpdateVersion3025 = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function

    Private Function funUpdateVersion3026() As Boolean

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3026

            'if replication is done and current machine is replicated shop then don't execute the SQL Script to update the version

            'Commented by Abdul Jabbar no need to check replicated shop as GL release runs only in head office
            'If Not frmLogin.IsPublisher() And frmLogin.IsReplicationDone() Then
            '    blnExecuteSQL = False
            'Else
            '    blnExecuteSQL = True
            'End If

            blnExecuteSQL = True


            'ScriptCollection.Add("Release\" & "3026_200_Fa_Alter_tblGlCOAMainSubSubDetail.sql")
            ScriptCollection.Add("Release\" & "3026_Fa_GL_Date_Format.sql")
            ScriptCollection.Add("Release\" & "3026_CR#198_AJ_Drop_sp_Rpt_AgedReceivableLedger.sql")
            ScriptCollection.Add("Release\" & "3026_CR#198_AJ_Create_sp_Rpt_AgedReceivableLedger.sql")
            ScriptCollection.Add("Release\" & "3026_CR#203_AJ_Alter_TblrptLedgerDetail.sql")
            ScriptCollection.Add("Release\" & "3026_AJ_Drop_SP_rpt_AgedPayableLedger.sql")
            ScriptCollection.Add("Release\" & "3026_AJ_Create_SP_rpt_AgedPayableLedger.sql")


            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3026 = False
                    Exit Function
                    'CR#281 End
                End Try

                ScriptCollection = Nothing

            End If

            '======================
            ' ''   COPY REPORTS
            If System.IO.Directory.Exists(Application.StartupPath & "\Release") Then

                'Removing Readonly attribute of report files

                Try

                    Dim Path As String = Application.StartupPath & "\Reports"

                    For Each fileName As String In System.IO.Directory.GetFiles(Path)
                        Dim fileInfo As New System.IO.FileInfo(fileName)
                        ' alternatively you can use this statement 
                        fileInfo.IsReadOnly = False
                    Next

                    Call funCopyReport("Reports", "\rptLedgerDetail.rpt", True)
                    Call funCopyReport("Reports", "\rptGLPLNotes.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVoucherMulti_Short.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_Short_withDesc.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_Short_WithoutSorting.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_Short_WithoutSorting_withDesc.rpt", True)

                Catch ex As Exception
                    'CR#281---Start
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    ShowErrorMessage("Error while version update process. Some report is missing. Please review release log file for detail")
                    funUpdateVersion3026 = False
                    Exit Function
                    'CR#281---End
                End Try


            End If

            funUpdateVersion3026 = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function

    Private Function funUpdateVersion3027() As Boolean

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3027

            'if replication is done and current machine is replicated shop then don't execute the SQL Script to update the version

            'Commented by Abdul Jabbar no need to check replicated shop as GL release runs only in head office
            'If Not frmLogin.IsPublisher() And frmLogin.IsReplicationDone() Then
            '    blnExecuteSQL = False
            'Else
            '    blnExecuteSQL = True
            'End If

            blnExecuteSQL = True

            'No Scripts to execute for 
            

            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3027 = False
                    Exit Function
                    'CR#281 End
                End Try

                ScriptCollection = Nothing

            End If

            'No Report

            funUpdateVersion3027 = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function
    Private Function funUpdateVersion3028() As Boolean

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3028

            blnExecuteSQL = True

            ScriptCollection.Add("Release\" & "3028_233_1_FT_insert_tblGLSecurityFormControl.sql")
            ScriptCollection.Add("Release\" & "3028_237_1_AJ_alter_Comments_TblrptGLCashFlow.sql")
            ScriptCollection.Add("Release\" & "3028_233_AJ_2_GLVoucherUnPostDefaultRights.sql")
            ScriptCollection.Add("Release\" & "3028_233_AJ_2_GLVoucherPostDefaultRights.sql")


            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3028 = False
                    Exit Function
                    'CR#281 End
                End Try

                ScriptCollection = Nothing

            End If

            '======================
            ' ''   COPY REPORTS
            If System.IO.Directory.Exists(Application.StartupPath & "\Release") Then

                'Removing Readonly attribute of report files

                Try

                    Dim Path As String = Application.StartupPath & "\Reports"

                    For Each fileName As String In System.IO.Directory.GetFiles(Path)
                        Dim fileInfo As New System.IO.FileInfo(fileName)
                        ' alternatively you can use this statement 
                        fileInfo.IsReadOnly = False
                    Next

                    Call funCopyReport("Reports", "\rptAccountLedger.rpt", True)
                    Call funCopyReport("Reports", "\rptLedgerDetail.rpt", True)

                Catch ex As Exception
                    'CR#281---Start
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    ShowErrorMessage("Error while version update process. Some report is missing. Please review release log file for detail")
                    funUpdateVersion3028 = False
                    Exit Function
                    'CR#281---End
                End Try


            End If

            funUpdateVersion3028 = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function
    Private Function funUpdateVersion3029() As Boolean

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3029

            blnExecuteSQL = True

            ScriptCollection.Add("Release\" & "3029_245_1_HO_FT_Create_tblGLVoucherHistory.sql")
            ScriptCollection.Add("Release\" & "3029_245_2_HO_FT_Create_tblGLVoucherDetailHistory.sql")
            ScriptCollection.Add("Release\" & "3029_245_3_HO_FT_Drop_[spVoucherPostUnpost].sql")
            ScriptCollection.Add("Release\" & "3029_245_4_HO_FT_create_[SpVoucherPostUnpost].sql")
            ScriptCollection.Add("Release\" & "3029_245_5_HO_FT_Drop_[sp_GLVoucherHistory].sql")
            ScriptCollection.Add("Release\" & "3029_245_6_HO_FT_Create_[sp_GLVoucherHistory].sql")
            ScriptCollection.Add("Release\" & "3029_245_7_HO_FT_Drop_[SpVoucherHistoryUpdate].sql")
            ScriptCollection.Add("Release\" & "3029_245_8_HO_FT_Create_SpVoucherHistoryUpdate.sql")
            ScriptCollection.Add("Release\" & "3029_245_9_HO_FT_insert_tblGLSecurity.sql")
            ScriptCollection.Add("Release\" & "3029_232_1_AJ_Drop_vwGLPLNotesCurrentDtl.sql")
            ScriptCollection.Add("Release\" & "3029_232_2_AJ_Create_vwGLPLNotesCurrentDtl.sql")
            ScriptCollection.Add("Release\" & "3029_232-3-AJ-HO-Drop-vwGLPLNotesPrevDtl.sql")
            ScriptCollection.Add("Release\" & "3029_232-4-AJ-HO-Create-vwGLPLNotesPrevDtl.sql")
            ScriptCollection.Add("Release\" & "3029_232-5-AJ-HO-Drop-vwGLPLNotesDtl.sql")
            ScriptCollection.Add("Release\" & "3029_232-6-AJ-HO-Create-vwGLPLNotesDtl.sql")
            ScriptCollection.Add("Release\" & "3029_254_1_SCH_FH_insert_SENDSMS-GLConfiguration.sql")
            ScriptCollection.Add("Release\" & "3029_252_1_SCH_FH_insert_GLConfiguration.sql")
            ScriptCollection.Add("Release\" & "3029_251_1_SCH_FH_DropTAble_tblGLContactDirectory.sql")
            ScriptCollection.Add("Release\" & "3029_251_2_SCH_FH_CreateTAble_tblGLContactDirectory.sql")
            ScriptCollection.Add("Release\" & "3029_251_3_FH_insert_tblGLSecurityFormControl.sql")
            ScriptCollection.Add("Release\" & "3029_241_1_SCH_FH_DropTAble_tblGLsmsConfiguration.sql")
            ScriptCollection.Add("Release\" & "3029_241_2_SCH_FH_dropTAble_tblGLsmsLog.sql")
            ScriptCollection.Add("Release\" & "3029_241_3_SCH_FH_CreateTAble_tblGLsmsConfiguration.sql")
            ScriptCollection.Add("Release\" & "3029_241_4_SCH_FH_CreateTAble_tblGLsmsLog.sql")
            ScriptCollection.Add("Release\" & "3029_241_5_FH_insert_tblGLSecurityFormControl.sql")
            ScriptCollection.Add("Release\" & "3029_241_6_FH_insert_SMSLOG.sql")
            ScriptCollection.Add("Release\" & "3029_240_1_FH_insert_tblGLSecurityFormControl.sql")
            ScriptCollection.Add("Release\" & "3029_239_1_AK_INSERT_tblGLSecurityForm.sql")



            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3029 = False
                    Exit Function
                    'CR#281 End
                End Try

                ScriptCollection = Nothing

            End If

            '======================
            ' ''   COPY REPORTS
            If System.IO.Directory.Exists(Application.StartupPath & "\Release") Then

                'Removing Readonly attribute of report files

                Try

                    Dim Path As String = Application.StartupPath & "\Reports"

                    For Each fileName As String In System.IO.Directory.GetFiles(Path)
                        Dim fileInfo As New System.IO.FileInfo(fileName)
                        ' alternatively you can use this statement 
                        fileInfo.IsReadOnly = False
                    Next

                    Call funCopyReport("Reports", "\rptAccountLedger.rpt", True)
                    Call funCopyReport("Reports", "\rptLedgerDetail.rpt", True)
                    Call funCopyReport("Reports", "\rptGLPLNotesDetail.rpt", True)
                    Call funCopyReport("Reports", "\rptGLVoucherHistory.rpt", True)


                Catch ex As Exception

                    'CR#281---Start
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    ShowErrorMessage("Error while version update process. Some report is missing. Please review release log file for detail")
                    funUpdateVersion3029 = False
                    Exit Function
                    'CR#281---End

                End Try


            End If

            funUpdateVersion3029 = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function

    Private Function funUpdateVersion3030() As Boolean

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3030

            blnExecuteSQL = True

            ScriptCollection.Add("Release\" & "3030_264_1_HO_FT_insert_tblGLSecurity.sql")
            ScriptCollection.Add("Release\" & "3030_267_1_HO_WA_Insert_tblGLSecurityForm.sql")
            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)

                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3030 = False
                    Exit Function
                    'CR#281 End

                End Try

                ScriptCollection = Nothing

            End If

            '======================
            ' ''   COPY REPORTS
            'No Report

            funUpdateVersion3030 = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function
    Private Function funUpdateVersion3031() As Boolean 'CR#327

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3031

            blnExecuteSQL = True

            ScriptCollection.Add("Release\" & "3031_268_1_RS_Drop_vwGLInv_PO_Sales.sql")
            ScriptCollection.Add("Release\" & "3031_268_2_RS_Create_vwGLInv_PO_Sales.sql")
            ScriptCollection.Add("Release\" & "3031_268_3_RS_Button_SecurityRights.sql")
            ScriptCollection.Add("Release\" & "3031_277_1_HO_FT_update_tblGLSecurityFormControl.sql")
            ScriptCollection.Add("Release\" & "3031_299_AJ_1_GLConfiguration.sql")
            ScriptCollection.Add("Release\" & "3031_300_1_AJ_Alter_tblGLVoucher.sql")
            ScriptCollection.Add("Release\" & "3031_300_2_AJ_SecurityForCompanySource.sql")
            ScriptCollection.Add("Release\" & "3031_300_3_AJ_Alter_tblGLVoucherTemp.sql")
            ScriptCollection.Add("Release\" & "3031_317_1_HO_AJ_AlterVDesc_TblrptDAilyActivity.sql")
            ScriptCollection.Add("Release\" & "3031_320_1_HO_AJ_DropSP_usp_AccTrialOpeningBalance.sql")
            ScriptCollection.Add("Release\" & "3031_320_2_HO_AJ_CreateSP_usp_AccTrialOpeningBalance.sql")
            ScriptCollection.Add("Release\" & "3031_323_1_FH_Create_tblchequetemplate.sql")
            ScriptCollection.Add("Release\" & "3031_323_2_FH_alter_tblGLCOAmainsubsubDetail.sql")
            ScriptCollection.Add("Release\" & "3031_323_3_FH_insert_checkTemplates.sql")
            ScriptCollection.Add("Release\" & "3031_323_4_FH_insert_SecurityControls.sql")

            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)

                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3031 = False
                    Exit Function
                    'CR#281 End

                End Try

                ScriptCollection = Nothing

            End If

            '======================
            ' ''   COPY REPORTS
            If System.IO.Directory.Exists(Application.StartupPath & "\Release") Then

                'Removing Readonly attribute of report files

                Try

                    Dim Path As String = Application.StartupPath & "\Reports"

                    For Each fileName As String In System.IO.Directory.GetFiles(Path)
                        Dim fileInfo As New System.IO.FileInfo(fileName)
                        ' alternatively you can use this statement 
                        fileInfo.IsReadOnly = False
                    Next

                    Call funCopyReport("Reports", "\rptDailyActivity.rpt", True)
                    Call funCopyReport("Reports", "\rptGlTrailBalance.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_PV.rpt", True)
                    Call funCopyReport("Reports", "\rptGlVouchersingle_SV.rpt", True)
                    Call funCopyReport("Reports", "\Cheques\crptChequeBankAIB.rpt", True)
                    Call funCopyReport("Reports", "\Cheques\crptChequeBankAlfalah.rpt", True)
                    Call funCopyReport("Reports", "\Cheques\crptChequeBankAllied.rpt", True)
                    Call funCopyReport("Reports", "\Cheques\crptChequeBankAskari.rpt", True)
                    Call funCopyReport("Reports", "\Cheques\crptChequeBankfaysal.rpt", True)
                    Call funCopyReport("Reports", "\Cheques\crptChequeBankHBL.rpt", True)
                    Call funCopyReport("Reports", "\Cheques\crptChequeBankMCB.rpt", True)
                    Call funCopyReport("Reports", "\Cheques\crptChequeBankSCB.rpt", True)
                    Call funCopydLL("Janus.Data.v3.dll", "Janus.Data.v3.dll")
                    Call funCopydLL("Janus.Windows.Common.v3.dll", "Janus.Windows.Common.v3.dll")
                    Call funCopydLL("Janus.Windows.GridEX.v3.dll", "Janus.Windows.GridEX.v3.dll")

                Catch ex As Exception

                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    ShowErrorMessage("Error while version update process. Some report is missing. Please review release log file for detail")
                    funUpdateVersion3031 = False
                    Exit Function

                End Try


            End If


            funUpdateVersion3031 = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function
    Private Function funUpdateVersion3032() As Boolean

        Try

            Dim blnExecuteSQL As Boolean
            Dim FILE_NAME As String = "UpdateLog"
            Dim ScriptCollection As New Collection
            Dim dblSchemaVer As Double

            dblSchemaVer = 3032

            blnExecuteSQL = True

            ScriptCollection.Add("Release\" & "3032_331_1_HO_Shb_Insert_Del_FormRights.sql")
            ScriptCollection.Add("Release\" & "3032_336_1_AJ_Button_SecurityRights.sql")
            ScriptCollection.Add("Release\" & "3032_337_1_Ho_Shb_Insert_tblChequeTempltes.sql")
            ScriptCollection.Add("Release\" & "3032_334_1_FT_Drop_Sp_SalesTaxInvoice.sql")
            ScriptCollection.Add("Release\" & "3032_334_2_FT_Create_Sp_SalesTaxInvoice.sql")

            '   EXECUTE SQL
            '======================
            If blnExecuteSQL Then

                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===================  " & dblSchemaVer & " =============================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "===============================================================", dblSchemaVer)
                Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Update Date: " & CBOCollectionDAL.GetServerDateTime().Date & "", dblSchemaVer)

                Try
                    'start the transaction
                    Call DAL.UtilityDAL.funExecuteScriptFile(Application.StartupPath.ToString, ScriptCollection, dblSchemaVer)

                Catch ex As Exception
                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)

                    'CR#281 Start
                    'ShowErrorMessage("Error while version update process.")
                    ShowErrorMessage("Error while version update process. Some script is missing or having error. Please review release log file for detail")
                    funUpdateVersion3032 = False
                    Exit Function
                    'CR#281 End

                End Try

                ScriptCollection = Nothing

            End If

            '======================
            ' ''   COPY REPORTS
            If System.IO.Directory.Exists(Application.StartupPath & "\Release") Then

                'Removing Readonly attribute of report files

                Try

                    Dim Path As String = Application.StartupPath & "\Reports"

                    For Each fileName As String In System.IO.Directory.GetFiles(Path)
                        Dim fileInfo As New System.IO.FileInfo(fileName)
                        ' alternatively you can use this statement 
                        fileInfo.IsReadOnly = False
                    Next

                    Call funCopyReport("Reports", "\rptSalesTaxInvoice.rpt", True)
                    Call funCopyReport("Reports", "\Cheques\crptChequeBankFaysalNew.rpt", True)
                    Call funCopyReport("Reports", "\Cheques\crptChequeBankMCBNew.rpt", True)
                    Call funCopyReport("Reports", "\Cheques\crptChequeBankSCB.rpt", True)
                    Call funCopyReport("Reports", "\Cheques\crptChequeStandardNew.rpt", True)

                Catch ex As Exception

                    Call DAL.UtilityDAL.funSaveToFile(Application.StartupPath.ToString & "\" & FILE_NAME, "Error while version update process." & Err.Description, dblSchemaVer)
                    ShowErrorMessage("Error while version update process. Some report is missing. Please review release log file for detail")
                    funUpdateVersion3032 = False
                    Exit Function

                End Try

            End If

            funUpdateVersion3032 = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Function
    Private Function funCopydLL(ByVal strFileName As String, ByVal strFile As String) As Boolean
        Try


            'Dim fso As New   FileSystemObject
            Dim strAppPath As String

            If Right(Application.StartupPath, 1) = "\" Then
                strAppPath = Application.StartupPath
            Else
                strAppPath = Application.StartupPath & "\"
            End If

            Dim strSourceFile As String
            Dim strDestinationFile As String
            Dim strDestinationOldFile As String


            strSourceFile = strAppPath & "Release\" & strFileName
            strDestinationFile = strAppPath & strFile

            If IO.File.Exists(strSourceFile) Then
                If IO.File.Exists(strDestinationFile) Then
                    IO.File.Delete(strDestinationFile)
                End If

                Call IO.File.Copy(strSourceFile, strDestinationFile, True)
                funCopydLL = True

            End If
            Exit Function

        Catch ex As Exception
            funCopydLL = False
        End Try

    End Function
End Module
