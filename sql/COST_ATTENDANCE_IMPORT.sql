USE [SMTCost]
GO
/****** Object:  StoredProcedure [dbo].[COST_ATTENDANCE_IMPORT]    Script Date: 09/15/2017 15:43:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[COST_ATTENDANCE_IMPORT]
@cmonth varchar(20)
as
set nocount on
declare @begindate varchar(20),@enddate varchar(20),@sql varchar(500)
--set @cmonth = '2017-06'
set @begindate = @cmonth + '-01'
--月末
select @enddate = convert(varchar,dateadd(day,-day(@begindate),dateadd(month,1,@begindate)),23)
 --链接服务器版,人员信息
create table #temp(emp_no nvarchar(20),dept nvarchar(200),rank nvarchar(50),eband nvarchar(50))
set @sql = 'insert into #temp  select * from  openquery(barcode,''select EMPLOYEE_ID_,DEPARTMENT_,rank_ ,e_band from barcodenew.dbo.IHPS_ID_USER_PROFILE where department_ like ''''制造中心%'''''')'
exec(@sql)
----非链接服务器版,无需要使用临时表，直接使用
--IHPS_ID_USER_PROFILE

--考勤数据
create table #temp2(rdate datetime,emp_no nvarchar(20),emp_name nvarchar(50),wkname nvarchar(50),gz_int decimal(18,2),jb_ps_int decimal(18,2),jb_xx_int decimal(18,2),jb_jr_int decimal(18,2),ischeck bit)
set @sql = 'insert into #temp2  select * from  OPENQUERY(KAOQIN,''Select rdate,empno,empname,wkname,cast(isnull(gz_int,0)/60.0 as decimal(18,2)),cast(isnull(jb_ps_int,0)/60.0 as decimal(18,2)),cast(isnull(jb_xx_int,0)/60.0 as decimal(18,2)),cast(isnull(jb_jr_int,0)/60.0 as decimal(18,2)),ischeck from Ecard_hytera.dbo.kq_dayreport where (isnull(gz_int,0)> 0 or isnull(jb_ps_int,0)>0 or isnull(jb_xx_int,0) >0 or isnull(jb_jr_int,0)>0) and CONVERT(varchar(100), rdate, 23)  like ''''' + @cmonth + '%'''''')'
exec(@sql)
insert into COST_DIRECT_LABOUR_ATTENDANCE(cdate,emp_no,emp_name,wkname,gz_int,jb_ps_int,jb_xx_int,jb_jr_int,ischeck,dept4,rank,PERSON_TYPE,person_type_id) 
select k.*,dept4 = case when len(replace(dept,'\','--'))-len(dept) < 4 then dept else left(dept,charindex('\',dept,charindex('\',dept,charindex('\',dept,CHARINDEX('\',dept,1)+1)+1)+1)-1) end,
rank,person_type = case when cast(eband as int) <=4 or (ISNULL(rank,'0') = '0' and ISNULL(eband,'0')='0') or (RANK = '操作族' and ISNULL(eband,'0')='0') then '直接人工' else '间接人工' end,person_type_id = case when cast(eband as int) <=4 or (ISNULL(rank,'0') = '0' and ISNULL(eband,'0')='0') or (RANK = '操作族' and ISNULL(eband,'0')='0') then 3 else 4 end 
from #temp2 k join #temp e on k.emp_no = e.emp_no 

drop table #temp
drop table #temp2