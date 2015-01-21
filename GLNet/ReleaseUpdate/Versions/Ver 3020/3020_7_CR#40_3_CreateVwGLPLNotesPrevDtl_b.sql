Create View vwGLPLNotesPrevDtl as 
SELECT dbo.vwGlCOADetail.PL_Note_Title, dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title, 
dbo.vwGlCOADetail.Detail_Code,dbo.vwGlCOADetail.detail_Title,
SUM(dbo.tblGlVoucherDetail.debit_amount) AS debit_amount, SUM(dbo.tblGlVoucherDetail.credit_amount) 
AS credit_amount,(SUM(dbo.tblGlVoucherDetail.credit_amount)- SUM(dbo.tblGlVoucherDetail.debit_amount)) 
AS ClosingAmount, dbo.tblGlDefFinancialYear.year_code , dbo.tblGlDefLocation.location_code,  
dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id  
FROM dbo.tblGlVoucherDetail INNER JOIN dbo.vwGlCOADetail ON 
dbo.tblGlVoucherDetail.coa_detail_id = dbo.vwGlCOADetail.coa_detail_id INNER JOIN dbo.tblGlVoucher 
ON dbo.tblGlVoucherDetail.voucher_id = dbo.tblGlVoucher.voucher_id AND 
dbo.tblGlVoucherDetail.location_id = dbo.tblGlVoucher.location_id INNER JOIN dbo.tblGlDefFinancialYear ON 
dbo.tblGlVoucher.finiancial_year_id = dbo.tblGlDefFinancialYear.financial_year_id INNER JOIN 
dbo.tblGlDefLocation ON dbo.tblGlVoucher.location_id = dbo.tblGlDefLocation.location_id   
WHERE (dbo.tblGlVoucher.post = 1)  AND  (tblGlVoucher.other_voucher = 0)   
AND  (tblGlVoucher.other_voucher = 0)     AND (dbo.tblGlVoucher.Voucher_Date) <= '20090630' 
AND (dbo.tblGlVoucher.Voucher_Date) >= '20080701' 
GROUP BY dbo.vwGlCOADetail.PL_Note_Title, dbo.tblGlDefFinancialYear.year_code, dbo.vwGlCOADetail.PL_note_id,
dbo.tblGlDefLocation.location_code,  dbo.tblGlDefLocation.location_name, dbo.tblGlDefLocation.location_id, 
dbo.vwGlCOADetail.sub_sub_code, dbo.vwGlCOADetail.sub_sub_title,
dbo.vwGlCOADetail.Detail_Code,dbo.vwGlCOADetail.detail_Title  
HAVING  (dbo.tblGlDefLocation.location_id = 6)   and (dbo.vwGlCOADetail.PL_Note_Title IS NOT NULL) 
AND (dbo.tblGlDefFinancialYear.year_code = '2008-2009') AND (dbo.vwGlCOADetail.PL_note_id > 0 )