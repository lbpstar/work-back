
set nocount on
declare @sql varchar(500)

 --链接服务器版,人员信息
create table #temp(dept nvarchar(1000))
set @sql = 'insert into #temp  select * from  openquery(barcode,''select DEPARTMENT_ from barcodenew.dbo.IHPS_ID_USER_PROFILE group by DEPARTMENT_'')'
exec(@sql)

insert into cost_dept_list select  
dept1 = case when len(replace(dept,'\','--'))-len(dept) < 1 then dept else left(dept,CHARINDEX('\',dept,1)-1) end ,
dept2 = case when len(replace(dept,'\','--'))-len(dept) < 2 then dept else left(dept,charindex('\',dept,CHARINDEX('\',dept,1)+1)-1) end ,
dept3 = case when len(replace(dept,'\','--'))-len(dept) < 3 then dept else left(dept,charindex('\',dept,charindex('\',dept,CHARINDEX('\',dept,1)+1)+1)-1) end ,
dept 
from  #temp e  where e.dept like '制造中心%'  order by dept

select * from #temp where dept like '制造中心%' order by dept
drop table #temp
drop table #temp2