if  exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetFormGLID]') and xtype in (N'FN', N'IF', N'TF'))
Drop FUNCTION [dbo].[GetFormGLID] 
