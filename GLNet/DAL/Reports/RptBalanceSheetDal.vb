

''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela New
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Balance Sheet Report .. 
''// Programmer	     : R@! Shahid
''// Creation Date	 : 20-July-2009
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

Public Class RptBalanceSheetDal

    Function InsertDataForNotes() As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction

        Dim strSQL As String = ""

        Try


            strSQL = "Delete from TblrptGLBsNotes"

            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


            strSQL = "Insert Into TblrptGLBsNotes (note_no ,note_Title ,sub_sub_code ,sub_sub_title ,debit_amount ,credit_amount ,ClosingAmount ,debit_amount_Prev ,credit_amount_Prev ,ClosingAmount_Prev) " & _
                            "Select note_no ,note_Title ,sub_sub_code ,sub_sub_title ,debit_amount ,credit_amount ,ClosingAmount ,debit_amount_Prev ,credit_amount_Prev ,ClosingAmount_Prev from vwGlBSNotes"

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


            strSQL = "Delete from TblrptBalanceSheet_Formatted"
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


            strSQL = "Insert Into TblrptBalanceSheet_Formatted (col1 ,col2 ,col3 ,col4 ,col5 ,col6 ,col7 ,col8 ,col9 ,col10 ,col11 ,col12 ,col13 ,col14 ,col15 ,col16 ,col17 ,col18 ,col19 ,col20 ,col21 ,col22 ,col23 ,col24 ,col25 ,col26 ,col27 ,col28 ,col29 ,col30 ,col31 ,col32 ,col33 ,col34 ,col35 ,col36 ,col37 ,col38 ,col39 ,col40 ,col41 ,col42 ,pcol1 ,pcol2 ,pcol3 ,pcol4 ,pcol5 ,pcol6 ,pcol7 ,pcol8 ,pcol9 ,pcol10 ,pcol11 ,pcol12 ,pcol13 ,pcol14 ,pcol15 ,pcol16 ,pcol17 ,pcol18 ,pcol19 ,pcol20 ,pcol21 ,pcol22 ,pcol23 ,pcol24 ,pcol25 ,pcol26 ,pcol27 ,pcol28 ,pcol29 ,pcol30 ,pcol31 ,pcol32 ,pcol33 ,pcol34 ,pcol35 ,pcol36 ,pcol37 ,pcol38 ,pcol39 ,pcol40 ,pcol41 ,pcol42 ,CYear ,net_profit) " & _
                            "Select col1 ,col2 ,col3 ,col4 ,col5 ,col6 ,col7 ,col8 ,col9 ,col10 ,col11 ,col12 ,col13 ,col14 ,col15 ,col16 ,col17 ,col18 ,col19 ,col20 ,col21 ,col22 ,col23 ,col24 ,col25 ,col26 ,col27 ,col28 ,col29 ,col30 ,col31 ,col32 ,col33 ,col34 ,col35 ,col36 ,col37 ,col38 ,col39 ,col40 ,col41 ,col42 ,pcol1 ,pcol2 ,pcol3 ,pcol4 ,pcol5 ,pcol6 ,pcol7 ,pcol8 ,pcol9 ,pcol10 ,pcol11 ,pcol12 ,pcol13 ,pcol14 ,pcol15 ,pcol16 ,pcol17 ,pcol18 ,pcol19 ,pcol20 ,pcol21 ,pcol22 ,pcol23 ,pcol24 ,pcol25 ,pcol26 ,pcol27 ,pcol28 ,pcol29 ,pcol30 ,pcol31 ,pcol32 ,pcol33 ,pcol34 ,pcol35 ,pcol36 ,pcol37 ,pcol38 ,pcol39 ,pcol40 ,pcol41 ,pcol42 ,CYear ,net_profit from tblBSDummyTable"

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
        Dim recBS As DataTable
        Dim intTotalField As Integer

        'Delete from Dummy table
        strSql = "Delete from tblBSDummyTable"
        UtilityDAL.ExecuteQuery(strSql)


        strSql = " SELECT     TOP 100 PERCENT dbo.tblGlDefGLNotes.note_title AS BS_Note_title, dbo.tblGlDefGLNotes.note_no, ISNULL(dbo.vwGLBalanceSheet.debit_amount, 0)  " & _
                "         AS debit_amount, ISNULL(dbo.vwGLBalanceSheet.credit_amount, 0) AS Credit_amount, ISNULL(dbo.vwGLBalanceSheet.ClosingAmount, 0) " & _
                "         AS Closing_Amount, dbo.vwGLBalanceSheet.year_code " & _
                "  FROM         dbo.tblGlDefGLNotes LEFT OUTER JOIN  " & _
                "         dbo.vwGLBalanceSheet ON tblGlDefGLNotes.gl_note_id = dbo.vwGLBalanceSheet.note_id " & _
                "  WHERE  (dbo.tblGlDefGLNotes.note_type = 'BS') order by tblGlDefGLNotes.Note_no "

        recBS = UtilityDAL.GetDataTable(strSql).Copy

        'Get recordset of Dummy table for updating recordset
        strSql = "Select * from tblBSDummyTable"
        recDummy = UtilityDAL.GetDataTable(strSql).Copy


        intTotalField = recDummy.Columns.Count

        Dim intCounter As Integer
        ''intCounter = intTotalField

        intCounter = 0
        Dim dr As DataRow
        dr = recDummy.NewRow
        recDummy.Rows.Add(dr)
        If recBS.Rows.Count > 0 Then

            Add(recDummy.Columns(intTotalField - 2).ColumnName.ToString, recBS.Rows(0).Item("year_code").ToString)

            'While Not recBS.EOF
            For Each drow As DataRow In recBS.Rows
                ''recDummy.Fields(intCounter) = recBS.Fields("main_title")
                ''intCounter = intCounter + 1
                Update(recDummy.Columns(intCounter).ColumnName.ToString, drow.Item("Note_no"))
                intCounter = intCounter + 1
                Update(recDummy.Columns(intCounter).ColumnName.ToString, drow.Item("Closing_Amount"))
                intCounter = intCounter + 1


            Next
        Else
            Throw New Exception("No data to print")
            Exit Function
            proSaveDummyTable = True

        End If


        Dim recPreviousBS As DataTable

        strSql = " SELECT     TOP 100 PERCENT dbo.tblGlDefGLNotes.note_title AS BS_Note_title, dbo.tblGlDefGLNotes.note_no, ISNULL(dbo.vwGLBalanceSheetPrevious.debit_amount, 0)  " & _
            "         AS debit_amount, ISNULL(dbo.vwGLBalanceSheetPrevious.credit_amount, 0) AS Credit_amount, ISNULL(dbo.vwGLBalanceSheetPrevious.ClosingAmount, 0) " & _
            "         AS Closing_Amount, dbo.vwGLBalanceSheetPrevious.year_code " & _
            "  FROM         dbo.tblGlDefGLNotes LEFT OUTER JOIN  " & _
            "         dbo.vwGLBalanceSheetPrevious ON dbo.tblGlDefGLNotes.gl_note_id = dbo.vwGLBalanceSheetPrevious.note_id " & _
            "  WHERE     (dbo.tblGlDefGLNotes.note_type = 'BS') order by tblGlDefGLNotes.Note_no "

        recPreviousBS = UtilityDAL.GetDataTable(strSql).Copy

        ''recDummy.AddNew
        If recPreviousBS.Rows.Count > 0 Then

            ' recDummy.Rows(0).Item(intTotalField - 1) = Trim(FinancialYear.ToString)

            For Each drow As DataRow In recPreviousBS.Rows

                'recDummy.Fields(intCounter) = recPreviousBS.Fields("main_title")
                'intCounter = intCounter + 1
                Update(recDummy.Columns(intCounter).ColumnName.ToString, drow.Item("Note_no"))
                intCounter = intCounter + 1
                Update(recDummy.Columns(intCounter).ColumnName.ToString, drow.Item("Closing_Amount"))
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

            strSQL = "insert into tblBSDummyTable ( " & ColName & " ) values('" & Value & "')"

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

            'If (ColName = "pcol2") Then
            '    MsgBox("pause")
            'End If
            strSQL = "UPDATE tblBSDummyTable SET " & ColName & " ='" & Value & "'"

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

