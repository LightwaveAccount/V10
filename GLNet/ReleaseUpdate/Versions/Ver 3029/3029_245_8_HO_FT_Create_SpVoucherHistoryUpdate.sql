--exec SpVoucherHistoryUpdate 207
Create PROCEDURE [dbo].[SpVoucherHistoryUpdate]
	@VoucherID as int,
	@Condition as int
AS
BEGIN

Declare @ID as int,
@Count as int

set @Count=(Select count(*) from tblGlVoucherHistory)

if @Condition=1
begin
 if @Count=0
 Begin
	--Insertion in tblGlVoucherHistory
insert into tblGlVoucherHistory (voucher_id ,  location_id ,voucher_code,finiancial_year_id,
voucher_type_id,voucher_month,voucher_no,voucher_date,paid_to,
coa_detail_id,cheque_no,cheque_date,cheque_paid,cheque_paid_date,
post,other_voucher,source,cheque_credited,temp_voucher_id,
due_date,shop_id,shop_code,Action,Action_date)

Select voucher_id ,  location_id ,voucher_code,finiancial_year_id,
voucher_type_id,voucher_month,voucher_no,voucher_date,paid_to,
coa_detail_id,cheque_no,cheque_date,cheque_paid,cheque_paid_date,
post,other_voucher,source,cheque_credited,temp_voucher_id,
due_date,shop_id,shop_code,'Save',getdate()from tblGlVoucher where voucher_id=@VoucherID

set @ID=(select max(id) from tblGlVoucherHistory)

---Insertion in tblGlVoucherDetailHistory
insert into tblGlVoucherDetailHistory (id,voucher_detail_id ,voucher_id ,location_id,coa_detail_id,
comments,debit_amount,credit_amount,cost_center_id,sp_refrence,
direction,shop_id,Action,Action_date)

select @ID,voucher_detail_id ,voucher_id ,location_id,coa_detail_id,
comments,debit_amount,credit_amount,cost_center_id,sp_refrence,
direction,shop_id,'Save',getdate() from tblGlVoucherDetail where voucher_id=@VoucherID
 End
End

if @Condition=0
Begin

--Insertion in tblGlVoucherHistory
insert into tblGlVoucherHistory (voucher_id ,  location_id ,voucher_code,finiancial_year_id,
voucher_type_id,voucher_month,voucher_no,voucher_date,paid_to,
coa_detail_id,cheque_no,cheque_date,cheque_paid,cheque_paid_date,
post,other_voucher,source,cheque_credited,temp_voucher_id,
due_date,shop_id,shop_code,Action,Action_date)

Select voucher_id ,  location_id ,voucher_code,finiancial_year_id,
voucher_type_id,voucher_month,voucher_no,voucher_date,paid_to,
coa_detail_id,cheque_no,cheque_date,cheque_paid,cheque_paid_date,
post,other_voucher,source,cheque_credited,temp_voucher_id,
due_date,shop_id,shop_code,'Update',getdate()from tblGlVoucher where voucher_id=@VoucherID

set @ID=(select max(id) from tblGlVoucherHistory)

---Insertion in tblGlVoucherDetailHistory
insert into tblGlVoucherDetailHistory (id,voucher_detail_id ,voucher_id ,location_id,coa_detail_id,
comments,debit_amount,credit_amount,cost_center_id,sp_refrence,
direction,shop_id,Action,Action_date)

select @ID,voucher_detail_id ,voucher_id ,location_id,coa_detail_id,
comments,debit_amount,credit_amount,cost_center_id,sp_refrence,
direction,shop_id,'Update',getdate() from tblGlVoucherDetail where voucher_id=@VoucherID
End

End

--truncate table tblglvoucherHistory
--truncate table tblglvoucherdetailHistory