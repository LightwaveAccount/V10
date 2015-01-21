
''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela New
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Cash Flow Report .. 
''// Programmer	     : R@! Shahid
''// Creation Date	 : 20-July-2009
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''// 175     19 Jan,2012       Abdul Jabbar        Profit & Loss Notes detail report should show net Profit or loss.
''/////////////////////////////////////////////////////////////////////////////////////////

Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility

Public Class RptProfitAndLossDal

    Function InsertDataForNotes() As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction

        Dim strSQL As String = ""

        Try


            strSQL = "Delete from TblrptGLPLNotes"
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))

            'CR#175
            'strSQL = "Insert Into TblrptGLPLNotes (note_no ,note_title ,sub_sub_code ,sub_sub_title ,debit_amount ,credit_amount ,ClosingAmount ,debit_amount_Prev ,credit_amount_Prev ,ClosingAmount_Prev) " & _
            '              "Select note_no ,note_title ,sub_sub_code ,sub_sub_title ,debit_amount ,credit_amount ,ClosingAmount ,debit_amount_Prev ,credit_amount_Prev ,ClosingAmount_Prev from vwGLPLNotes"

            strSQL = "Insert Into TblrptGLPLNotes (note_no ,note_title ,sub_sub_code ,sub_sub_title ,debit_amount ,credit_amount ,ClosingAmount ,debit_amount_Prev ,credit_amount_Prev ,ClosingAmount_Prev,Note_Group) " & _
                          "Select note_no ,note_title ,sub_sub_code ,sub_sub_title ,debit_amount ,credit_amount ,ClosingAmount ,debit_amount_Prev ,credit_amount_Prev ,ClosingAmount_Prev,Note_Group from vwGLPLNotes"

            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


            ' Commit Transaction .. 
            trans.Commit()

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

    Function InsertDataForNotesDetail() As Boolean 'CR#40

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction

        Dim strSQL As String = ""

        Try


            strSQL = "Delete from TblrptGLPLNotesDetail"
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


            strSQL = "Insert Into TblrptGLPLNotesDetail (note_no ,note_title ,sub_sub_code ,sub_sub_title ,Detail_code,Detail_title,debit_amount ,credit_amount ,ClosingAmount ,debit_amount_Prev ,credit_amount_Prev ,ClosingAmount_Prev) " & _
                          "Select note_no ,note_title ,sub_sub_code ,sub_sub_title ,Detail_code,Detail_title,debit_amount ,credit_amount ,ClosingAmount ,debit_amount_Prev ,credit_amount_Prev ,ClosingAmount_Prev from vwGLPLNotesDetail"

            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


            ' Commit Transaction .. 
            trans.Commit()

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
    Function InsertDataForReport() As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction

        Dim strSQL As String = ""

        Try


            strSQL = " Delete from TblrptProftAndLossStatement_Formated"
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


            strSQL = "Insert Into TblrptProftAndLossStatement_Formated (col1 ,col2 ,col3 ,col4 ,col5 ,col6 ,col7 ,col8 ,col9 ,col10 ,col11 ,col12 ,col13 ,col14 ,col15 ,col16 ,pcol1 ,pcol2 ,pclo3 ,pclo4 ,pcol5 ,pcol6 ,pcol7 ,pcol8 ,pclo9 ,pclo10 ,pcol11 ,pcol12 ,pcol13 ,pcol14 ,pcol15 ,pcol16 ,CYear ,PREYear) " & _
                          "Select col1 ,col2 ,col3 ,col4 ,col5 ,col6 ,col7 ,col8 ,col9 ,col10 ,col11 ,col12 ,col13 ,col14 ,col15 ,col16 ,pcol1 ,pcol2 ,pclo3 ,pclo4 ,pcol5 ,pcol6 ,pcol7 ,pcol8 ,pclo9 ,pclo10 ,pcol11 ,pcol12 ,pcol13 ,pcol14 ,pcol15 ,pcol16 ,CYear ,PREYear from tblPALDummyTable"

            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


            ' Commit Transaction .. 
            trans.Commit()

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

    Public Function proSaveDummyTable(ByVal FinancialYear As String) As Boolean

        Dim strSql As String
        Dim recDummy As DataTable
        Dim recPAL As DataTable
        Dim intTotalField As Integer

        'Delete from Dummy table
        strSql = "Delete from tblPALDummyTable"
        UtilityDAL.ExecuteQuery(strSql)


        strSql = " SELECT   dbo.tblGlDefGLNotes.note_title AS PL_Note_title, ISNULL(dbo.vwGLProfitLoss.debit_amount, 0) AS [.debit_amount], " & _
                 "          ISNULL(dbo.vwGLProfitLoss.credit_amount, 0) AS credit_amoun, ISNULL(dbo.vwGLProfitLoss.ClosingAmount, 0) AS ClosingAmount, " & _
                 "          dbo.vwGLProfitLoss.year_code ,dbo.tblGlDefGLNotes.note_no " & _
                 " FROM         dbo.tblGlDefGLNotes LEFT OUTER JOIN " & _
                 "          dbo.vwGLProfitLoss ON dbo.tblGlDefGLNotes.note_title = dbo.vwGLProfitLoss.PL_Note_Title " & _
                 " WHERE     (dbo.tblGlDefGLNotes.note_type = 'PL') ORDER BY dbo.tblGlDefGLNotes.note_no "

        recPAL = UtilityDAL.GetDataTable(strSql).Copy

        'Get recordset of Dummy table for updating recordset
        strSql = "Select * from tblPALDummyTable"
        recDummy = UtilityDAL.GetDataTable(strSql).Copy


        intTotalField = recDummy.Columns.Count

        Dim intCounter As Integer
        ''intCounter = intTotalField

        intCounter = 0
        Dim dr As DataRow
        dr = recDummy.NewRow
        recDummy.Rows.Add(dr)
        If recPAL.Rows.Count > 0 Then

            Add(recDummy.Columns(intTotalField - 2).ColumnName.ToString, recPAL.Rows(0).Item("year_code").ToString)

            'While Not recPAL.EOF
            For Each drow As DataRow In recPAL.Rows
                ''recDummy.Fields(intCounter) = recPAL.Fields("main_title")
                ''intCounter = intCounter + 1
                Update(recDummy.Columns(intCounter).ColumnName.ToString, drow.Item("Note_no"))
                intCounter = intCounter + 1
                Update(recDummy.Columns(intCounter).ColumnName.ToString, drow.Item("ClosingAmount"))
                intCounter = intCounter + 1


            Next
        Else
            Throw New Exception("No data to print")
            Exit Function
            proSaveDummyTable = True

        End If


        Dim recPreviousPAL As DataTable

        strSql = " SELECT   dbo.tblGlDefGLNotes.note_title AS PL_Note_title, ISNULL(dbo.vwGLProfitLossPrevious.debit_amount, 0) AS [.debit_amount], " & _
                 "          ISNULL(dbo.vwGLProfitLossPrevious.credit_amount, 0) AS credit_amoun, ISNULL(dbo.vwGLProfitLossPrevious.ClosingAmount, 0) AS ClosingAmount, " & _
                 "          dbo.vwGLProfitLossPrevious.year_code ,dbo.tblGlDefGLNotes.note_no " & _
                 " FROM         dbo.tblGlDefGLNotes LEFT OUTER JOIN " & _
                 "          dbo.vwGLProfitLossPrevious ON dbo.tblGlDefGLNotes.note_title = dbo.vwGLProfitLossPrevious.PL_Note_Title " & _
                 " WHERE     (dbo.tblGlDefGLNotes.note_type = 'PL') ORDER BY dbo.tblGlDefGLNotes.note_no "

        recPreviousPAL = UtilityDAL.GetDataTable(strSql).Copy

        ''recDummy.AddNew
        If  recPreviousPAL.Rows.Count > 0 Then

            recDummy.Rows(0).Item(intTotalField - 1) = Trim(FinancialYear.ToString)

            For Each drow As DataRow In recPreviousPAL.Rows


                'recDummy.Fields(intCounter) = recPreviousPAL.Fields("main_title")
                'intCounter = intCounter + 1
                Update(recDummy.Columns(intCounter).ColumnName.ToString, drow.Item("PL_Note_Title"))
                intCounter = intCounter + 1
                Update(recDummy.Columns(intCounter).ColumnName.ToString, drow.Item("ClosingAmount"))
                intCounter = intCounter + 1

            Next

        End If

    End Function

    Public Function Add(ByVal ColName As String, ByVal Value As String) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "insert into tblPALDummyTable ( " & ColName & " ) values('" & Value & "')"

            ''Execute SQL 
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

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
    Public Function Update(ByVal ColName As String, ByVal Value As String) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "UPDATE tblPALDummyTable SET " & ColName & " ='" & Value & "'"

            ''Execute SQL 
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

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


End Class

