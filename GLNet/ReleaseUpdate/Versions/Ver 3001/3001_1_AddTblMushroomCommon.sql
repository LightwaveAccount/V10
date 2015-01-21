IF OBJECT_ID (N'dbo.tblMushroomCommon') IS NULL
CREATE TABLE [dbo].[tblMushroomCommon](
	[mushroom_common_id] [numeric](10, 0) IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[expiry_service] [numeric](10, 0) NULL,
	[membership_fee] [numeric](10, 0) NULL,
	[renewal_fee] [numeric](10, 0) NULL,
	[membership_card_expiry] [datetime] NULL,
	[psr_expiry] [numeric](10, 0) NULL,
	[shop_priority_date] [datetime] NULL,
	[shop_priority_validity] [datetime] NULL,
	[membership_duration] [numeric](10, 0) NULL,
	[Membership_Signature] [image] NULL,
	[LM] [numeric](10, 2) NULL,
	[TM] [numeric](10, 2) NULL,
	[label_type] [int] NULL,
	[production_product_detail] [bit] NULL,
	[STR_product_name_report] [bit] NULL,
	[replication_status] [bit] NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL CONSTRAINT [MSmerge_df_rowguid_5C7685B9C20E4325AF061F1D94F543DD]  DEFAULT (newsequentialid()),
 CONSTRAINT [PK_tblMushroomCommon] PRIMARY KEY CLUSTERED 
(
	[mushroom_common_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[tblMushroomCommon]  WITH NOCHECK ADD  CONSTRAINT [repl_identity_range_517EEAFD_C8DF_4996_8B93_025A97CEF1FA] CHECK NOT FOR REPLICATION (([mushroom_common_id]>(1) AND [mushroom_common_id]<=(10001) OR [mushroom_common_id]>(10001) AND [mushroom_common_id]<=(20001)))

ALTER TABLE [dbo].[tblMushroomCommon] CHECK CONSTRAINT [repl_identity_range_517EEAFD_C8DF_4996_8B93_025A97CEF1FA]