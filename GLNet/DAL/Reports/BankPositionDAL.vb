'----------------------------------Modification History ---------------------------------------------------
'CR Reference   By              Date            Descritption
'CR#229         Abdul Jabbar    04 Feb,2013     Newly created bank account are not appearing in bank position report after clearance (i.c. after presented and credited). 
'CR#319         Abdul Jabbar    16 Jul,2014     Bank Position, Bank Statement Bank Balance balance is not appropriate
Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility

Public Class BankPositionDAL

    Public Function DisplayReport(ByVal dtSearchDate As Date) As DataTable
        Dim objDA As SqlDataAdapter
        Dim strSQL As String

        Try
            Dim dt As New DataTable

            'CR#144

            'strSQL = " ALTER VIEW vwGLBanksPosition AS SELECT     Banks.detail_code, Banks.detail_title AS BankName, ISNULL(LedgerBalance.LedgerBalance,0) AS LedgerBalance , ISNULL( UnPresented.Total,0 ) As UnPresented , ISNULL( UnCredited.Total, 0 ) As Uncredited ," & _
            '" ISNULL( LedgerBalance.LedgerBalance +    UnPresented.Total -  UnCredited.Total,0) As BankBalance " & _
            '" FROM         dbo.vwGlCOADetail Banks LEFT OUTER JOIN " & _
            '"               (SELECT     dbo.tblGlVoucherDetail.coa_detail_id, SUM(dbo.tblGlVoucherDetail.debit_amount) - SUM(dbo.tblGlVoucherDetail.credit_amount) AS LedgerBalance " & _
            '"                FROM          dbo.tblGlVoucherDetail INNER JOIN " & _
            '"                                       dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id " & _
            '"               WHERE      (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.voucher_no <> '000000') AND (dbo.tblGlVoucher.voucher_date <= '" & Format(dtSearchDate, pbDateFormat) & "' ) " & _
            '"                 GROUP BY dbo.tblGlVoucherDetail.coa_detail_id) LedgerBalance ON Banks.coa_detail_id = LedgerBalance.coa_detail_id " & _
            '" LEFT OUTER JOIN " & _
            '" (SELECT     dbo.tblGlVoucherDetail.coa_detail_id, SUM(dbo.tblGlVoucherDetail.credit_amount)AS Total " & _
            '"  FROM         dbo.tblGlVoucherDetail INNER JOIN " & _
            '"                           dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id " & _
            '"  WHERE     (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.voucher_no <> '000000') AND (dbo.tblGlVoucher.voucher_date <= '" & Format(dtSearchDate, pbDateFormat) & "') AND " & _
            '"                     (dbo.tblGlVoucher.cheque_paid = 0 OR dbo.tblGlVoucher.cheque_date > '" & Format(dtSearchDate, pbDateFormat) & "') " & _
            '" GROUP BY dbo.tblGlVoucherDetail.coa_detail_id) AS UnPresented ON Banks.coa_detail_id = UnPresented.coa_detail_id " & _
            '" LEFT OUTER JOIN " & _
            '" (SELECT     dbo.tblGlVoucherDetail.coa_detail_id, SUM(dbo.tblGlVoucherDetail.Debit_amount)AS Total " & _
            '" FROM         dbo.tblGlVoucherDetail INNER JOIN " & _
            '"                           dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id " & _
            '"  WHERE     (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.voucher_no <> '000000') AND (dbo.tblGlVoucher.voucher_date <= '" & Format(dtSearchDate, pbDateFormat) & "') AND " & _
            '"                   (dbo.tblGlVoucher.cheque_Credited = 0 OR dbo.tblGlVoucher.cheque_date > '" & Format(dtSearchDate, pbDateFormat) & "') " & _
            '" GROUP BY dbo.tblGlVoucherDetail.coa_detail_id) AS UnCredited ON Banks.coa_detail_id = UnCredited.coa_detail_id  " & _
            '" WHERE     (Banks.account_type = 'Bank')  "

            'CR#169 exclude vouchers at Line 8 and 16
            'CR#229 (second line IsNull applied)


            'CR#319
            'In this Query we are Calculating Bank position by using 3 columns Bank Ledger Balance, Unpresented Balance and Uncredited Balance
            '1) Bank Ledger Balance: Fetch All Vouchers of Banks having voucher date upto selected date
            '2) UnPresented : Fetch Sum of Credit Amount of Banks of those transactions of which cheque is not paid yet or check payment date is greater than selected date
            '3) UnCredited : Fetch sum of debit amount of banks of those transactions of which cheque is not credited or check payment date is greater than selected date
            'Bank Balance= Bank Ledger Balance + Unpresented Cheques- Uncredited Cheques
            'Column ChequeDate is replaced with cheque_paid_date 

            'strSQL = " ALTER VIEW vwGLBanksPosition AS SELECT     Banks.detail_code, Banks.detail_title AS BankName, ISNULL(LedgerBalance.LedgerBalance,0) AS LedgerBalance , ISNULL( UnPresented.Total,0 ) As UnPresented , ISNULL( UnCredited.Total, 0 ) As Uncredited ," & _
            '" ISNULL( LedgerBalance.LedgerBalance,0) +    isnull(UnPresented.Total,0) -  isnull(UnCredited.Total,0) As BankBalance " & _
            '" FROM         dbo.vwGlCOADetail Banks LEFT OUTER JOIN " & _
            '"               (SELECT     dbo.tblGlVoucherDetail.coa_detail_id, SUM(dbo.tblGlVoucherDetail.debit_amount) - SUM(dbo.tblGlVoucherDetail.credit_amount) AS LedgerBalance " & _
            '"                FROM          dbo.tblGlVoucherDetail INNER JOIN " & _
            '"                                       dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id " & _
            '"               WHERE      (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.voucher_no <> '000000') AND (dbo.tblGlVoucher.voucher_date <= '" & Format(dtSearchDate, pbDateFormat) & "' ) " & _
            '" and (dbo.tblGlVoucher.Other_Voucher = 0) " & _
            '"                 GROUP BY dbo.tblGlVoucherDetail.coa_detail_id) LedgerBalance ON Banks.coa_detail_id = LedgerBalance.coa_detail_id " & _
            '" LEFT OUTER JOIN " & _
            '" (SELECT     dbo.tblGlVoucherDetail.coa_detail_id, SUM(dbo.tblGlVoucherDetail.credit_amount)AS Total " & _
            '"  FROM         dbo.tblGlVoucherDetail INNER JOIN " & _
            '"                           dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id " & _
            '"  WHERE     (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.voucher_no <> '000000') AND (dbo.tblGlVoucher.voucher_date <= '" & Format(dtSearchDate, pbDateFormat) & "') AND " & _
            '"                     (dbo.tblGlVoucher.cheque_paid = 0 OR dbo.tblGlVoucher.cheque_paid_date > '" & Format(dtSearchDate, pbDateFormat) & "') " & _
            '" and (dbo.tblGlVoucher.Other_Voucher = 0) " & _
            '" GROUP BY dbo.tblGlVoucherDetail.coa_detail_id) AS UnPresented ON Banks.coa_detail_id = UnPresented.coa_detail_id " & _
            '" LEFT OUTER JOIN " & _
            '" (SELECT     dbo.tblGlVoucherDetail.coa_detail_id, SUM(dbo.tblGlVoucherDetail.Debit_amount)AS Total " & _
            '" FROM         dbo.tblGlVoucherDetail INNER JOIN " & _
            '"                           dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id " & _
            '"  WHERE     (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.voucher_no <> '000000') AND (dbo.tblGlVoucher.voucher_date <= '" & Format(dtSearchDate, pbDateFormat) & "') AND " & _
            '"                   (dbo.tblGlVoucher.cheque_Credited = 0 OR tblGlVoucher.cheque_date > '" & Format(dtSearchDate, pbDateFormat) & "') " & _
            '" GROUP BY dbo.tblGlVoucherDetail.coa_detail_id) AS UnCredited ON Banks.coa_detail_id = UnCredited.coa_detail_id  " & _
            '" WHERE     (Banks.account_type = 'Bank')  "

            strSQL = " ALTER VIEW vwGLBanksPosition AS SELECT     Banks.detail_code, Banks.detail_title AS BankName, ISNULL(LedgerBalance.LedgerBalance,0) AS LedgerBalance , ISNULL( UnPresented.Total,0 ) As UnPresented , ISNULL( UnCredited.Total, 0 ) As Uncredited ," & _
             " ISNULL( LedgerBalance.LedgerBalance,0) +    isnull(UnPresented.Total,0) -  isnull(UnCredited.Total,0) As BankBalance " & _
             " FROM         dbo.vwGlCOADetail Banks LEFT OUTER JOIN " & _
             "               (SELECT     dbo.tblGlVoucherDetail.coa_detail_id, SUM(dbo.tblGlVoucherDetail.debit_amount) - SUM(dbo.tblGlVoucherDetail.credit_amount) AS LedgerBalance " & _
             "                FROM          dbo.tblGlVoucherDetail INNER JOIN " & _
             "                                       dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id " & _
             "               WHERE      (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.voucher_no <> '000000') AND (dbo.tblGlVoucher.voucher_date <= '" & Format(dtSearchDate, pbDateFormat) & "' ) " & _
             " and (dbo.tblGlVoucher.Other_Voucher = 0) " & _
             "                 GROUP BY dbo.tblGlVoucherDetail.coa_detail_id) LedgerBalance ON Banks.coa_detail_id = LedgerBalance.coa_detail_id " & _
             " LEFT OUTER JOIN " & _
             " (SELECT     dbo.tblGlVoucherDetail.coa_detail_id, SUM(dbo.tblGlVoucherDetail.credit_amount)AS Total " & _
             "  FROM         dbo.tblGlVoucherDetail INNER JOIN " & _
             "                           dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id " & _
             "  WHERE     (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.voucher_no <> '000000') AND (dbo.tblGlVoucher.voucher_date <= '" & Format(dtSearchDate, pbDateFormat) & "') AND " & _
             "                     (dbo.tblGlVoucher.cheque_paid = 0 OR dbo.tblGlVoucher.cheque_paid_date > '" & Format(dtSearchDate, pbDateFormat) & "') " & _
             " and (dbo.tblGlVoucher.Other_Voucher = 0) " & _
             " GROUP BY dbo.tblGlVoucherDetail.coa_detail_id) AS UnPresented ON Banks.coa_detail_id = UnPresented.coa_detail_id " & _
             " LEFT OUTER JOIN " & _
             " (SELECT     dbo.tblGlVoucherDetail.coa_detail_id, SUM(dbo.tblGlVoucherDetail.Debit_amount)AS Total " & _
             " FROM         dbo.tblGlVoucherDetail INNER JOIN " & _
             "                           dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id " & _
             "  WHERE     (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.voucher_no <> '000000') AND (dbo.tblGlVoucher.voucher_date <= '" & Format(dtSearchDate, pbDateFormat) & "') AND " & _
             "                   (dbo.tblGlVoucher.cheque_Credited = 0 OR tblGlVoucher.cheque_paid_date > '" & Format(dtSearchDate, pbDateFormat) & "') " & _
             " GROUP BY dbo.tblGlVoucherDetail.coa_detail_id) AS UnCredited ON Banks.coa_detail_id = UnCredited.coa_detail_id  " & _
             " WHERE     (Banks.account_type = 'Bank')  "

            SQLHelper.ExecuteScaler(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

            strSQL = "SELECT * FROM vwGLBanksPosition"

            objDA = New SqlDataAdapter(strSQL, SQLHelper.CON_STR)
            objDA.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GoForSelectionCriteria(ByVal dtpDate As Date) As Boolean

        Dim strSQL As String

        Try
            strSQL = " ALTER VIEW vwGLBanksPositionRPT AS SELECT     Banks.detail_code, Banks.detail_title AS BankName, LedgerBalance.LedgerBalance, UnPresented.Total As UnPresented , UnCredited.Total As Uncredited ," & _
                        " LedgerBalance.LedgerBalance +    UnPresented.Total -  UnCredited.Total As BankBalance " & _
                        " FROM         dbo.vwGlCOADetail Banks INNER JOIN " & _
                        "               (SELECT     dbo.tblGlVoucherDetail.coa_detail_id, SUM(dbo.tblGlVoucherDetail.debit_amount) - SUM(dbo.tblGlVoucherDetail.credit_amount) AS LedgerBalance " & _
                        "                FROM          dbo.tblGlVoucherDetail INNER JOIN " & _
                        "                                       dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id " & _
                        "               WHERE      (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.voucher_no <> '000000') AND (dbo.tblGlVoucher.voucher_date <= '" & Format(dtpDate, "yyyy-MM-dd") & "' ) " & _
                        "                 GROUP BY dbo.tblGlVoucherDetail.coa_detail_id) LedgerBalance ON Banks.coa_detail_id = LedgerBalance.coa_detail_id " & _
                        " Inner Join " & _
                        " (SELECT     dbo.tblGlVoucherDetail.coa_detail_id, SUM(dbo.tblGlVoucherDetail.credit_amount)AS Total " & _
                        "  FROM         dbo.tblGlVoucherDetail INNER JOIN " & _
                        "                           dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id " & _
                        "  WHERE     (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.voucher_no <> '000000') AND (dbo.tblGlVoucher.voucher_date <= '" & Format(dtpDate, "yyyy-MM-dd") & "') AND " & _
                        "                     (dbo.tblGlVoucher.cheque_paid = 0 OR dbo.tblGlVoucher.cheque_date > '" & Format(dtpDate, "yyyy-MM-dd") & "') " & _
                        " GROUP BY dbo.tblGlVoucherDetail.coa_detail_id) AS UnPresented ON Banks.coa_detail_id = UnPresented.coa_detail_id " & _
                        "   Inner Join " & _
                        " (SELECT     dbo.tblGlVoucherDetail.coa_detail_id, SUM(dbo.tblGlVoucherDetail.Debit_amount)AS Total " & _
                        " FROM         dbo.tblGlVoucherDetail INNER JOIN " & _
                        "                           dbo.tblGlVoucher ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id " & _
                        "  WHERE     (dbo.tblGlVoucher.post = 1) AND (dbo.tblGlVoucher.voucher_no <> '000000') AND (dbo.tblGlVoucher.voucher_date <= '" & Format(dtpDate, "yyyy-MM-dd") & "') AND " & _
                        "                   (dbo.tblGlVoucher.cheque_Credited = 0 OR dbo.tblGlVoucher.cheque_date > '" & Format(dtpDate, "yyyy-MM-dd") & "') " & _
                        " GROUP BY dbo.tblGlVoucherDetail.coa_detail_id) AS UnCredited ON Banks.coa_detail_id = UnCredited.coa_detail_id  " & _
                        " WHERE     (Banks.account_type = 'Bank')  "

            SQLHelper.ExecuteScaler(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

            'Clearing tblrptBankPosition which is using by Bank Position rpt
            strSQL = "Delete from tblrptBankPosition"
            SQLHelper.ExecuteScaler(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

            strSQL = "insert into tblrptBankPosition([detail_code],[BankName],[LedgerBalance],[UnPresented],[Uncredited],[BankBalance])  " & _
                     " Select [detail_code],[BankName],[LedgerBalance],[UnPresented],[Uncredited],[BankBalance] from vwGLBanksPosition"
            SQLHelper.ExecuteScaler(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
