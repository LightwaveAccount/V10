IF OBJECT_ID (N'dbo.TblrptGLPLNotesDetail') IS NULL
CREATE TABLE [dbo].[TblrptGLPLNotesDetail](
	[note_no] [int] NULL,
	[note_title] [varchar](50) NULL,
	[sub_sub_code] [varchar](10) NULL,
	[sub_sub_title] [varchar](50) NULL,
	[Detail_code] [varchar](18) NULL,
	[Detail_title] [varchar](500) NULL,
	[debit_amount] [money] NULL,
	[credit_amount] [money] NULL,
	[ClosingAmount] [money] NULL,
	[debit_amount_Prev] [money] NULL,
	[credit_amount_Prev] [money] NULL,
	[ClosingAmount_Prev] [money] NULL
) ON [PRIMARY]
