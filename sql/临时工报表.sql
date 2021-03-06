USE [SMTCost]
GO
/****** Object:  StoredProcedure [dbo].[COST_TEMP_EMPLOYEE_DAYS]    Script Date: 01/02/2018 16:34:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[COST_TEMP_EMPLOYEE_DAYS]
--@begin datetime,@end datetime,@cno varchar(20),@dept1 varchar(50),@dept2 varchar(200),@dept3 varchar(200),@dept varchar(1000),@status varchar(50)
@month varchar(20),@cno varchar(20),@dept1 varchar(50),@dept2 varchar(200),@dept3 varchar(200),@dept varchar(1000)
as
--select @cno = '201700014',@begin = '2017-09-11',@end = '2017-09-21',@status = 4
set nocount on
declare @monthbegin datetime,@begin datetime,@end datetime,@lastmonth  datetime
set @monthbegin = @month + '-01'
set @end = @month + '-25'
--周期始，上月26号
select @begin= DATEADD(mm,DATEDIFF(mm,0,dateadd(month,-1,@monthbegin)),0) +25

create table #temp(emp_status nvarchar(50),cno nvarchar(20),cname nvarchar(20),REGISTER_DATE date,leave_DATE date,dept1 nvarchar(200),dept2 nvarchar(200),dept3 nvarchar(200),dept nvarchar(1000),cfrom nvarchar(100),fromtype nvarchar(100),shift nvarchar(50),begin_date date,end_date date,begin_time nvarchar(50),end_time nvarchar(50),begin_apply nvarchar(50),end_apply nvarchar(50),rest_hours decimal(18,2),hours decimal(18,2),normal_hours decimal(18,2),overtime_hours decimal(18,2),REASON_OVERTIME nvarchar(1000),status nvarchar(50),ng nvarchar(50),ng_type nvarchar(50))
insert into #temp 
select e.status,e.cno ,e.cname ,e.REGISTER_DATE,e.leave_date,e.dept1,e.dept2,e.dept3,e.dept ,e.cfrom ,b.cname fromtype,
shift = case when isnull(a.shift,'') <> '' then a.shift else e.shift end,a.begin_date ,a.end_date ,a.begin_time ,a.end_time ,
a.begin_apply ,a.end_apply ,a.rest_hours ,a.hours ,a.normal_hours ,a.overtime_hours ,a.REASON_OVERTIME ,
status= case when isnull(a.status,0) = 0 then '未提报' when isnull(a.status,0) = 1 then '已提报' when isnull(a.status,0) = 2 then '考勤员已审核' else '主管已审批' end,
NG= case when (a.begin_apply IS null and a.END_APPLY IS null) or (DATEDIFF(second, CAST(a.begin_time as datetime) , CAST(a.begin_apply as datetime)) >=0 and DATEDIFF(second, CAST(a.end_apply as datetime) , CAST(a.end_time as datetime)) >=0) then '' else 'NG' end,a.NG_TYPE  
 from COST_TEMP_EMPLOYEE  e full join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno left join cost_base_data b on e.FROM_TYPE = b.sub_id and b.module_id = 1  
where isnull(a.begin_apply,'') <> '' and isnull(a.end_apply,'') <> '' and a.hours > 0 and isnull(isclose,0) = 0 and a.BEGIN_DATE >= @begin and a.BEGIN_DATE <= @end  and isnull(a.status,0)>=2

--白班
select cno as cno, days = count(*) into #temp1 from #temp  where shift like '白班%' group by cno
--夜班
select cno as cno, days = count(*) into #temp2 from #temp  where shift like '夜班%' group by cno
--异常次数
select cno as cno, days = count(*) into #temp5 from #temp  where ng_type = '忘打卡' or ng_type = '漏报' or ng_type = '多报' group by cno
--总天数
select max(emp_status) emp_status,cno as cno, max(cname) cname,max(register_date) register_date,max(leave_date) leave_date,max(dept1) dept1,max(dept2) dept2,max(dept3) dept3,max(dept) dept,max(cfrom) cfrom,max(fromtype) fromtype,hours = sum(hours),days = count(*) into #temp3 from #temp  group by cno
--每天的工时
select CNO,
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),dateadd(month,-1,@monthbegin),120) + '-26' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '26',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),dateadd(month,-1,@monthbegin),120) + '-27' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '27',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),dateadd(month,-1,@monthbegin),120) + '-28' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '28',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),dateadd(month,-1,@monthbegin),120) + '-29' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '29',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),dateadd(month,-1,@monthbegin),120) + '-30' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '30',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),dateadd(month,-1,@monthbegin),120) + '-31' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '31',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-01' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '1',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-02' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '2',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-03' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '3',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-04' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '4',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-05' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '5',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-06' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '6',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-07' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '7', 
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-08' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '8',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-09' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '9',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-10' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '10',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-11' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '11',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-12' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '12',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-13' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '13',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-14' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '14',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-15' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '15',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-16' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '16',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-17' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '17',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-18' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '18',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-19' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '19',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-20' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '20', 
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-21' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '21',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-22' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '22',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-23' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '23',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-24' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '24',
MAX(case when cast(BEGIN_DATE AS varchar(20)) = convert(varchar(7),@monthbegin,120) + '-25' then cast(cast(HOURS as decimal(18,1)) AS varchar(20)) else '' end) as '25'
 into #temp4 from (select e.status,a.CNO,e.CNAME,e.REGISTER_DATE,e.LEAVE_DATE,e.CFROM,e.dept2,e.dept3,e.dept,a.BEGIN_DATE,a.hours from COST_TEMP_EMPLOYEE_ATTENDANCE a 
left join COST_TEMP_EMPLOYEE e on a.CNO = e.cno where  a.hours > 0 and not (e.status = '离职' and a.begin_date is null and a.end_date is null) and isnull(isclose,0) = 0 and a.BEGIN_DATE >= @begin and a.BEGIN_DATE <= @end and isnull(a.status,0)>=2) t group by CNO,cname order by cno
--结果
select c.emp_status 人员状态,c.cno 工号,c.cname 姓名,c.register_date 入职日期,c.leave_date 离职日期,c.cfrom 输送渠道,c.fromtype 输送类型,c.dept1 一级部门,c.dept2 二级部门,c.dept3 三级部门,c.dept 部门,
d.[26],d.[27],d.[28],d.[29],d.[30],d.[31],d.[1],d.[2],d.[3],d.[4],d.[5],d.[6],d.[7],d.[8],d.[9],d.[10],d.[11],d.[12],d.[13],d.[14],d.[15],d.[16],d.[17],d.[18],d.[19],d.[20],d.[21],d.[22],d.[23],d.[24],d.[25],
cast(c.hours as decimal(18,1)) 出勤工时,isnull(c.days,0) 出勤总天数,isnull(a.days,0) 白班天数,isnull(b.days,0) 夜班天数,isnull(e.days,0) 异常次数 into #temp6 from #temp3 c 
left join #temp1 a on c.cno = a.cno left join #temp2 b on c.cno = b.cno left join #temp4 d on c.cno = d.CNO left join #temp5 e on c.cno = e.cno
if @cno <> ''
begin
select * from #temp6 where 工号 like '%' + @cno + '%'
end
else if @dept3 <>'0'
begin 
select * from #temp6 where  三级部门 = @dept3 and 二级部门 = @dept2 and 一级部门 = @dept1 
end
else if @dept2 <>'0'
begin 
select * from #temp6 where   二级部门 = @dept2 and 一级部门 = @dept1 
end
else if @dept1 <>'0'
begin 
select * from #temp6 where   一级部门 = @dept1 
end
else
begin 
select * from #temp6 
end

--select * from #temp
--drop table #temp
--drop table #temp1
--drop table #temp2
--drop table #temp3