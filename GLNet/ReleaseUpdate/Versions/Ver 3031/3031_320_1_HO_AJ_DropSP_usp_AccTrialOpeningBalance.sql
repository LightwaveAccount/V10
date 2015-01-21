if  exists (select * from sys.procedures where name='usp_AccTrialOpeningBalance')
begin
DROP PROCEDURE [usp_AccTrialOpeningBalance]
end