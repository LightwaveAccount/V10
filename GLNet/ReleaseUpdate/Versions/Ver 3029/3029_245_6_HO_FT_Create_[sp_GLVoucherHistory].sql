--exec Sp_GLVoucherHistory 0,'06-03-2012','06-03-2013',0
Create PROCEDURE [dbo].[Sp_GLVoucherHistory]
	@voucherNo as numeric(10,0),
	@VoucherFromDate as varchar(100),
	@VoucherToDate as varchar(100),
	@VoucherType as numeric(10,0)
AS
BEGIN
Declare @SQL as nvarchar(4000)
set @SQL	='SELECT     tblGlVoucherHistory.id, tblGlVoucherHistory.voucher_no, tblGlVoucherHistory.voucher_date AS Voucher_date, tblGlVoucherDetailHistory.debit_amount, 
                      tblGlVoucherDetailHistory.credit_amount, tblGlCOAMainSubSubDetail.detail_title, tblGlVoucherHistory.Action, tblGlVoucherHistory.Action_date, 
                      tblGlDefVoucherType.voucher_type, tblGlVoucherDetailHistory.comments, tblGlVoucherHistory.voucher_code, 
                      tblGlCOAMainSubSubDetail.detail_code
FROM         tblGlVoucherHistory INNER JOIN
                      tblGlVoucherDetailHistory ON tblGlVoucherHistory.id = tblGlVoucherDetailHistory.id INNER JOIN
                      tblGlDefVoucherType ON tblGlVoucherHistory.voucher_type_id = tblGlDefVoucherType.voucher_type_id INNER JOIN
                      tblGlCOAMainSubSubDetail ON tblGlVoucherDetailHistory.coa_detail_id = tblGlCOAMainSubSubDetail.coa_detail_id
WHERE     (1 = 1)'
if @voucherNo <>0
Begin
set @SQL=@SQL + ' And tblGlVoucherHistory.voucher_no= ' + cast(@voucherNo  as varchar(20)) + ''
End

if @VoucherType <>0
Begin
set @SQL=@SQL + ' And tblGlVoucherHistory.voucher_type_id= ' + cast(@VoucherType  as varchar(20)) + ''
End


set @SQL=@SQL + ' AND dbo.tblGlVoucherHistory.Action_date > =''' + cast(@VoucherFromDate   as varchar(100)) + ''''



set @SQL=@SQL + ' AND dbo.tblGlVoucherHistory.Action_date <=''' + cast(@VoucherToDate    as varchar(100)) + ''''


set @SQL=@SQL + ' ORDER BY tblGlVoucherHistory.id '

Execute sp_Executesql @SQL 
Print @SQL

END


