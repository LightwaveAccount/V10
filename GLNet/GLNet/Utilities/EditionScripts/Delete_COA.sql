update tblGlConfiguration set config_value='0' where config_name='gl_ac_income_tax_payable'
update tblGlConfiguration set config_value='0' where config_name='gl_ac_income_tax_receivable'
update tblGlConfiguration set config_value='0' where config_name='Profit_Loss_Acc_ID'


delete from tblGLCustomer_Receipts_Detail
delete from tblGLCustomer_Receipts
delete from tblGLSupplier_Payments_Detail
delete from tblGLSupplier_Payments

delete from tblChequeSetting

delete from tblGlvoucherDetail
delete from tblGlvoucher
delete from tblGlCOAMainSubSubDetail 
delete from tblGlCOAMainSubSub 
delete from  tblGlCOAMainSub 
delete from tblGlCOAMain 
