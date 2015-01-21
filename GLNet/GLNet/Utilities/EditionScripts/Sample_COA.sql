--Start, Deleting Data 
update tblGlConfiguration set config_value='0' where config_name='gl_ac_income_tax_payable'
update tblGlConfiguration set config_value='0' where config_name='gl_ac_income_tax_receivable'
update tblGlConfiguration set config_value='0' where config_name='Profit_Loss_Acc_ID'

delete from tblGLCustomer_Receipts
delete from tblGLCustomer_Receipts_Detail
delete from tblGLSupplier_Payments
delete from tblGLSupplier_Payments_Detail

delete from tblGlvoucherDetail
delete from tblGlvoucher
delete from tblGlCOAMainSubSubDetail 
delete from tblGlCOAMainSubSub 
delete from  tblGlCOAMainSub 
delete from tblGlCOAMain 
--End, Truncate Table 

SET NOCOUNT ON
SET IDENTITY_INSERT [dbo].[tblGlCOAMain] ON
INSERT INTO [tblGlCOAMain] ([coa_main_id],[main_code],[main_title],[main_type]) VALUES (1, '30', 'Owners Capital', 'Capital')
INSERT INTO [tblGlCOAMain] ([coa_main_id],[main_code],[main_title],[main_type]) VALUES (2, '11', 'Current Asset', 'Assets')
INSERT INTO [tblGlCOAMain] ([coa_main_id],[main_code],[main_title],[main_type]) VALUES (3, '60', 'Operating and Admin Expense', 'Expense')
INSERT INTO [tblGlCOAMain] ([coa_main_id],[main_code],[main_title],[main_type]) VALUES (4, '61', 'Cost of Goods Sold', 'Expense')
INSERT INTO [tblGlCOAMain] ([coa_main_id],[main_code],[main_title],[main_type]) VALUES (5, '62', 'Selling Expense', 'Expense')
INSERT INTO [tblGlCOAMain] ([coa_main_id],[main_code],[main_title],[main_type]) VALUES (6, '20', 'Current Liabilities', 'Liability')
INSERT INTO [tblGlCOAMain] ([coa_main_id],[main_code],[main_title],[main_type]) VALUES (7, '21', 'Long Term Liabilities', 'Liability')
INSERT INTO [tblGlCOAMain] ([coa_main_id],[main_code],[main_title],[main_type]) VALUES (8, '50', 'Income', 'Income')
INSERT INTO [tblGlCOAMain] ([coa_main_id],[main_code],[main_title],[main_type]) VALUES (9, '51', 'Other Income', 'Income')
INSERT INTO [tblGlCOAMain] ([coa_main_id],[main_code],[main_title],[main_type]) VALUES (10, '10', 'Fixed Assets', 'Assets')
SET IDENTITY_INSERT [dbo].[tblGlCOAMain] OFF



SET NOCOUNT OFF

 

SET NOCOUNT ON

SET IDENTITY_INSERT [dbo].[tblGlCOAMainSub] ON

INSERT INTO [tblGlCOAMainSub] ([main_sub_id],[coa_main_id],[sub_code],[sub_title]) VALUES (1, 1, '30-001', 'Owners Capital')
INSERT INTO [tblGlCOAMainSub] ([main_sub_id],[coa_main_id],[sub_code],[sub_title]) VALUES (2, 2, '11-001', 'Cash')
INSERT INTO [tblGlCOAMainSub] ([main_sub_id],[coa_main_id],[sub_code],[sub_title]) VALUES (3, 2, '11-002', 'Bank')
INSERT INTO [tblGlCOAMainSub] ([main_sub_id],[coa_main_id],[sub_code],[sub_title]) VALUES (4, 2, '11-003', 'Accounts Receivable')
INSERT INTO [tblGlCOAMainSub] ([main_sub_id],[coa_main_id],[sub_code],[sub_title]) VALUES (5, 10, '10-001', 'Office Equipment')
INSERT INTO [tblGlCOAMainSub] ([main_sub_id],[coa_main_id],[sub_code],[sub_title]) VALUES (6, 10, '10-002', 'Building')
INSERT INTO [tblGlCOAMainSub] ([main_sub_id],[coa_main_id],[sub_code],[sub_title]) VALUES (7, 6, '20-001', 'Accounts Payable')
INSERT INTO [tblGlCOAMainSub] ([main_sub_id],[coa_main_id],[sub_code],[sub_title]) VALUES (8, 6, '20-002', 'Accrued Expenses')
INSERT INTO [tblGlCOAMainSub] ([main_sub_id],[coa_main_id],[sub_code],[sub_title]) VALUES (9, 7, '21-001', 'Loans')
INSERT INTO [tblGlCOAMainSub] ([main_sub_id],[coa_main_id],[sub_code],[sub_title]) VALUES (10, 8, '50-001', 'Sales')
INSERT INTO [tblGlCOAMainSub] ([main_sub_id],[coa_main_id],[sub_code],[sub_title]) VALUES (11, 9, '51-001', 'Other Income')
INSERT INTO [tblGlCOAMainSub] ([main_sub_id],[coa_main_id],[sub_code],[sub_title]) VALUES (12, 2, '11-004', 'Advances Paid')
INSERT INTO [tblGlCOAMainSub] ([main_sub_id],[coa_main_id],[sub_code],[sub_title]) VALUES (14, 3, '60-001', 'Operating Expense')
INSERT INTO [tblGlCOAMainSub] ([main_sub_id],[coa_main_id],[sub_code],[sub_title]) VALUES (15, 3, '60-002', 'Administration Expense')
INSERT INTO [tblGlCOAMainSub] ([main_sub_id],[coa_main_id],[sub_code],[sub_title]) VALUES (16, 4, '61-001', 'Cost of Goods Sold')
SET IDENTITY_INSERT [dbo].[tblGlCOAMainSub] OFF

SET NOCOUNT OFF

 

SET NOCOUNT ON

SET IDENTITY_INSERT [dbo].[tblGlCOAMainSubSub] ON

INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (1, 1, '30-001-001', 'Owners Capital', 'General', 9, 9, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (2, 2, '11-001-001', 'Cash In Hand', 'Cash', 29, 29, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (3, 2, '11-001-002', 'Petty Cash', 'Cash', 29, 29, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (4, 3, '11-002-001', 'Current Account', 'Bank', 29, 29, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (5, 3, '11-002-002', 'Saving Account', 'Bank', 29, 29, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (6, 4, '11-003-001', 'Receiveable From Customers', 'Customer', 26, 26, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (9, 7, '20-001-001', 'Vendors Payble', 'Vendor', 11, 11, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (10, 12, '11-004-002', 'Employee Advances', 'General', 27, 27, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (13, 8, '20-002-001', 'Payroll Liabilities', 'General', 11, 11, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (14, 8, '20-002-002', 'Utility Bills Payable', 'General', 11, 11, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (15, 9, '21-001-001', 'Bank Loans', 'Bank', 17, 17, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (16, 10, '50-001-001', 'Product Sales', 'General', 10, 10, 1)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (17, 11, '51-001-001', 'Other Income', 'General', 10, 10, 3)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (18, 10, '50-001-002', 'Service Charges', 'General', 10, 10, 1)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (19, 5, '10-001-001', 'Computers Equipment', 'General', 22, 22, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (20, 5, '10-001-002', 'Furniture', 'General', 22, 22, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (21, 6, '10-002-001', 'Office Building', 'General', 23, 23, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (22, 6, '10-002-002', 'Warehouse', 'General', 23, 23, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (26, 14, '60-001-001', 'Utility Bills Expense', 'General', 10, 10, 6)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (27, 14, '60-001-002', 'Staf Salary Expense', 'General', 10, 10, 6)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (28, 15, '60-002-001', 'Automobile Expense', 'General', 10, 10, 4)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (29, 15, '60-002-002', 'Office Expense', 'General', 10, 10, 4)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (33, 14, '60-001-003', 'Bank Charges Expense', 'General', 10, 10, 7)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (35, 1, '30-001-002', 'Reservers', 'General', 14, 14, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (36, 1, '30-001-003', 'Drawings', 'General', 9, 9, NULL)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (37, 16, '61-001-001', 'Cost of Goods Sold Expense', 'General', 10, 10, 2)
INSERT INTO [tblGlCOAMainSubSub] ([main_sub_sub_id],[main_sub_id],[sub_sub_code],[sub_sub_title],[account_type],[DrBS_note_id],[CrBS_note_id],[PL_note_id]) VALUES (38, 14, '60-001-004', 'Selling Expense', 'General', 10, 10, 5)
SET IDENTITY_INSERT [dbo].[tblGlCOAMainSubSub] OFF

SET NOCOUNT OFF

 

SET NOCOUNT ON

SET IDENTITY_INSERT [dbo].[tblGlCOAMainSubSubDetail] ON

INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (1, 1, '30-001-001-00001', 'Owners Investment', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (2, 2, '11-001-001-00001', 'Cash In Hand', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (3, 3, '11-001-002-00001', 'Petty Cash', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (4, 4, '11-002-001-00001', 'UBL-Garden Town (2943-6)', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (5, 5, '11-002-002-00001', 'NIB Gulberg (123456)', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (7, 28, '60-002-002-00002', 'Vehical Fuel Expense', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (8, 28, '60-002-002-00003', 'Vehical Maintenance Expense', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (9, 29, '60-002-003-00001', 'Office Expense', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (10, 29, '60-002-003-00002', 'Entertainment Expense', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (17, 33, '60-001-003-00001', 'Bank Service Charges', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (20, 35, '30-001-002-00001', 'Un-appropriated profit & loss', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (21, 35, '30-001-002-00002', 'Company Reservers', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (22, 36, '30-001-003-00001', 'Owners Drawings', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (23, 17, '51-001-001-00001', 'Income from other sources', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (24, 26, '60-001-001-00001', 'Telephone Bills Expense', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (25, 26, '60-001-001-00002', 'Gas Bills Expense', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (26, 26, '60-001-001-00003', 'Electricity Bills Expense', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (27, 15, '21-001-001-00001', 'Bank Alflah Garden Town Cash Line', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (30, 14, '20-002-002-00001', 'Telephone Bills Payable', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (31, 14, '20-002-002-00002', 'Gas Bills Payable', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (32, 14, '20-002-002-00003', 'Electricity Bills Payable', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (33, 13, '20-002-001-00001', 'Employee Salaries Payable', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (34, 9, '20-001-001-00001', 'Haji Sons Ravi Road Lahore', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (35, 9, '20-001-001-00002', 'Ahmed Traders Islamabad', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (36, 9, '20-001-001-00003', 'JS Computers Hafeez Center Lahore', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (37, 27, '60-001-002-00001', 'Rizwan Rasheed Salary Expense', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (38, 27, '60-001-002-00002', 'Zubair Ali Salary Expenses', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (39, 27, '60-001-002-00003', 'Shuja Mirza Salary Expense', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (40, 10, '11-004-002-00001', 'Shuja Mirza Advances', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (41, 6, '11-003-001-00001', 'ZM Corporation 13Km Shiekhupura Road', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (42, 6, '11-003-001-00002', 'Shiekh Brothers Lahore', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (43, 6, '11-003-001-00003', 'Muhammad Ali Traders Rawalpindi', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (46, 19, '10-001-001-00001', 'Personal Computers', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (47, 19, '10-001-001-00002', 'Printers', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (48, 20, '10-001-002-00001', 'Office Furniture', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (49, 21, '10-002-001-00001', 'Main Office Lahore', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (50, 22, '10-002-002-00001', 'Karachi Warehouse', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (51, 22, '10-002-002-00002', 'Lahore warehouse', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (52, 16, '50-001-001-00001', 'Machine Sales Revenue', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (53, 16, '50-001-001-00002', 'Parts Sales Revenue', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (54, 18, '50-001-002-00001', 'Repairing Charges Revenue', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (55, 18, '50-001-002-00002', 'Labour Charges Revenue', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (56, 29, '60-002-003-00003', 'Stationary Expense', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (57, 29, '60-002-003-00004', 'Miscellaneous Expense', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (58, 37, '61-001-001-00001', 'Cost of Goods Sold', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (59, 38, '60-001-004-00001', 'Marketing Expense', NULL)
INSERT INTO [tblGlCOAMainSubSubDetail] ([coa_detail_id],[main_sub_sub_id],[detail_code],[detail_title],[end_date]) VALUES (60, 38, '60-001-004-00002', 'Promotion Material Expense', NULL)
SET IDENTITY_INSERT [dbo].[tblGlCOAMainSubSubDetail] OFF

SET NOCOUNT OFF
update tblGlConfiguration set config_value='20' where config_name='Profit_Loss_Acc_ID'