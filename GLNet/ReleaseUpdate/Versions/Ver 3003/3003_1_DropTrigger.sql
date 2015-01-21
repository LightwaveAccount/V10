if  exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[trg_Location_Fyears]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
Drop trigger [dbo].[trg_Location_Fyears]