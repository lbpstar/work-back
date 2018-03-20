
create procedure COST_DEPT_IMPORT 
AS
set nocount on
declare @sql varchar(500)

 --链接服务器版,人员信息
create table #temp(emp_no nvarchar(20),dept nvarchar(200),rank nvarchar(50),eband nvarchar(50))
set @sql = 'insert into #temp  select * from  openquery(barcode,''select EMPLOYEE_ID_,DEPARTMENT_,rank_ ,e_band from barcodenew.dbo.IHPS_ID_USER_PROFILE where department_ like ''''制造中心%'''''')'
exec(@sql)
----非链接服务器版,无需要使用临时表，直接使用
--IHPS_ID_USER_PROFILE

delete from cost_dept_list

select distinct dept,dept3 = case when len(replace(dept,'\','--'))-len(dept) < 3 then dept else left(dept,charindex('\',dept,charindex('\',dept,CHARINDEX('\',dept,1)+1)+1)-1) end, 
dept2 = case when len(replace(dept,'\','--'))-len(dept) < 2 then dept else left(dept,charindex('\',dept,CHARINDEX('\',dept,1)+1)-1) end,
dept1 = case when len(replace(dept,'\','--'))-len(dept) < 1 then dept else left(dept,CHARINDEX('\',dept,1)-1) end
into #temp2 from #temp

insert into cost_dept_list 
select dept1,dept2 = case when len(replace(dept2,'\','--'))-len(dept2) < 1 then dept else right(dept2,len(dept2) - CHARINDEX('\',dept2,1)) end,
dept3 = case when len(replace(dept2,'\','--'))-len(dept2) < 1 then dept3 else reverse(substring(reverse(dept3),1,charindex('\',reverse(dept3)) - 1))  end,dept
from #temp2

drop table #temp
drop table #temp2
