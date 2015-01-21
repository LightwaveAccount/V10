''/////////////////////////////////////////////////////////////////////////////////////////
''//                      GL.NET
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : frmReIndex .. 
''// Programmer	     : Fatima Tajammal
''// Creation Date	 : 29-jul-2013
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''//29-jul-2013         Fatima Tajammal     CR # 264    Build a utility for ReIndexing database
Public Class frmReindex

    Private Sub btnReindex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReindex.Click
        Try
            Dim objIndex As New DAL.DALReports
            If ShowConfirmationMessage("Do you want to reindex the database?", MessageBoxDefaultButton.Button2) = vbYes Then
                Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

                ShowProgressBar("Reindexing Tables")
                Application.DoEvents()

                objIndex.ExecuteQuery("DBCC DBREINDEX ('tblGLSecurityUser','', 70);")
                Application.DoEvents()
                objIndex.ExecuteQuery("DBCC DBREINDEX ('tblGlDefFinancialYear','', 70);")
                Application.DoEvents()

                objIndex.ExecuteQuery("DBCC DBREINDEX ('tblGlCOAMainSubSubDetail','', 70);")
                Application.DoEvents()
                objIndex.ExecuteQuery("DBCC DBREINDEX (tblGlCOAMainSubSubDetail, '', 70);")
                Application.DoEvents()

                objIndex.ExecuteQuery("DBCC DBREINDEX (tblGlDefVoucherType, '', 70);")
                Application.DoEvents()
                objIndex.ExecuteQuery("DBCC DBREINDEX (tblGLDMLLog, '', 70);")
                Application.DoEvents()
                Application.DoEvents()
                objIndex.ExecuteQuery("DBCC DBREINDEX (tblGlCOAMain, '', 70);")
                Application.DoEvents()
                objIndex.ExecuteQuery("DBCC DBREINDEX (tblGlCOAMainSub, '', 70);")
                Application.DoEvents()

                objIndex.ExecuteQuery("DBCC DBREINDEX (tblGlCOAMainSubSub, '', 70);")
                Application.DoEvents()

                objIndex.ExecuteQuery("DBCC DBREINDEX (tblGlCOAMainSubSubDetail, '', 70);")
                Application.DoEvents()
                objIndex.ExecuteQuery("DBCC DBREINDEX (tblGlVoucher, '', 70);")
                Application.DoEvents()
                Application.DoEvents()
                objIndex.ExecuteQuery("DBCC DBREINDEX (tblGlVoucherDetail, '', 70);")
                Application.DoEvents()
                objIndex.ExecuteQuery("DBCC DBREINDEX (TblrptGLBsNotes, '', 70);")
                Application.DoEvents()
                Application.DoEvents()
                objIndex.ExecuteQuery("DBCC DBREINDEX (TblrptGLCashFlow,'', 70);")
                Application.DoEvents()
                objIndex.ExecuteQuery("DBCC DBREINDEX (TblrptGLCashFlowStander,'', 70);")
                Application.DoEvents()
                objIndex.ExecuteQuery("DBCC DBREINDEX (TblrptGLPLNotes,'', 70);")
                Application.DoEvents()
                objIndex.ExecuteQuery("DBCC DBREINDEX (TblrptGLBsNotes,'', 70);")
                Application.DoEvents()
                objIndex.ExecuteQuery("DBCC DBREINDEX (TblrptGlTrailBalance,'', 70);")
                Application.DoEvents()
                objIndex.ExecuteQuery("DBCC DBREINDEX (TblrptGlVoucher,'', 70);")
                Application.DoEvents()
                objIndex.ExecuteQuery("DBCC DBREINDEX (TblrptLedgerDetail,'', 70);")
                Application.DoEvents()
                objIndex.ExecuteQuery("DBCC DBREINDEX (TblrptProftAndLossStatement_Formated,'', 70);")
                Application.DoEvents()

                Application.DoEvents()
                Application.DoEvents()
                Application.DoEvents()


                objIndex.ExecuteQuery("DECLARE @TableName VARCHAR(255) " & _
                                           "DECLARE @sql NVARCHAR(500) " & _
                                            "DECLARE @fillfactor INT " & _
                                            "SET @fillfactor = 80 " & _
                                            "DECLARE TableCursor CURSOR FOR " & _
                                            "SELECT name AS TableName " & _
                                            "FROM sys.tables " & _
                                            "OPEN TableCursor " & _
                                            "FETCH NEXT FROM TableCursor INTO @TableName " & _
                                            "WHILE @@FETCH_STATUS = 0 " & _
                                            "BEGIN " & _
                                            "SET @sql = 'ALTER INDEX ALL ON ' + @TableName + ' REBUILD WITH (FILLFACTOR = ' + CONVERT(VARCHAR(3),@fillfactor) + ')' " & _
                                            "EXEC (@sql) " & _
                                            "FETCH NEXT FROM TableCursor INTO @TableName " & _
                                            "END " & _
                                            "CLOSE TableCursor " & _
                                            "DEALLOCATE TableCursor ")
                Application.DoEvents()
                Application.DoEvents()

                EndProgressBar()
                Application.DoEvents()

                ShowInformationMessage("Reindex completed successfully")

            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmReindex_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Control And e.KeyCode = Keys.X Then
                If Me.btnExit.Enabled = True Then Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmReindex_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.SetButtonImages()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub SetButtonImages()

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
End Class