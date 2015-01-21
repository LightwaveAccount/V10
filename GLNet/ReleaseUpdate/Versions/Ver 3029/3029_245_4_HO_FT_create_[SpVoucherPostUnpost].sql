--exec SpVoucherHistoryUpdate 207
Create PROCEDURE [dbo].[SpVoucherPostUnPost]
	@VoucherID as int,
	@Status as varchar(50),
	@Condition as numeric(10,0)
AS
BEGIN

Declare @ID as int,
@Count as numeric(10,0),
@VoucherStatus as varchar(50),
@POst_unPost as numeric(10,0)

set @Count=(Select Count(*) from tblGLVoucherHIstory where Voucher_id=@VoucherID)

set @POst_unPost=(select post from tblGlVoucher where Voucher_id=@VoucherID)

if @POst_unPost=0
begin
	set @VoucherStatus='Unpost'
End
else
Begin
	set @VoucherStatus='Post'
End

		if @Status='Post' 
		Begin
			if @Condition=1
			Begin
			
			if @Count <> 0 
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
				due_date,shop_id,shop_code,@VoucherStatus,getdate()from tblGlVoucher where voucher_id=@VoucherID

				set @ID=(select max(id) from tblGlVoucherHistory)

				---Insertion in tblGlVoucherDetailHistory
				insert into tblGlVoucherDetailHistory (id,voucher_detail_id ,voucher_id ,location_id,coa_detail_id,
				comments,debit_amount,credit_amount,cost_center_id,sp_refrence,
				direction,shop_id,Action,Action_date)

				select @ID,voucher_detail_id ,voucher_id ,location_id,coa_detail_id,
				comments,debit_amount,credit_amount,cost_center_id,sp_refrence,
				direction,shop_id,@VoucherStatus,getdate() from tblGlVoucherDetail where voucher_id=@VoucherID
			End
			End
		End

		if @Status='UnPost'
		Begin
			if @Condition=0
			Begin
				if @Count =0
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
				due_date,shop_id,shop_code,@VoucherStatus,getdate()from tblGlVoucher where voucher_id=@VoucherID

				set @ID=(select max(id) from tblGlVoucherHistory)

				---Insertion in tblGlVoucherDetailHistory
				insert into tblGlVoucherDetailHistory (id,voucher_detail_id ,voucher_id ,location_id,coa_detail_id,
				comments,debit_amount,credit_amount,cost_center_id,sp_refrence,
				direction,shop_id,Action,Action_date)

				select @ID,voucher_detail_id ,voucher_id ,location_id,coa_detail_id,
				comments,debit_amount,credit_amount,cost_center_id,sp_refrence,
				direction,shop_id,@VoucherStatus,getdate() from tblGlVoucherDetail where voucher_id=@VoucherID
				End
			End
			if @Condition=1
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
				due_date,shop_id,shop_code,@VoucherStatus,getdate()from tblGlVoucher where voucher_id=@VoucherID

				set @ID=(select max(id) from tblGlVoucherHistory)

				---Insertion in tblGlVoucherDetailHistory
				insert into tblGlVoucherDetailHistory (id,voucher_detail_id ,voucher_id ,location_id,coa_detail_id,
				comments,debit_amount,credit_amount,cost_center_id,sp_refrence,
				direction,shop_id,Action,Action_date)

				select @ID,voucher_detail_id ,voucher_id ,location_id,coa_detail_id,
				comments,debit_amount,credit_amount,cost_center_id,sp_refrence,
				direction,shop_id,@VoucherStatus,getdate() from tblGlVoucherDetail where voucher_id=@VoucherID
			End
			
		ENd
END







