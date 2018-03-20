declare
@cmonth varchar(20),@monthbegin datetime,@begin datetime,@end datetime,@cno varchar(20),@dept1 varchar(50),@dept2 varchar(200),@dept3 varchar(200),@lastmonth datetime

select @dept1 = '制造中心',@cno = '',@dept2 = '0',@dept3 = '0'
set @cmonth = '2017-11'
set @monthbegin = @cmonth + '-01'
set @end = @cmonth + '-25'
--周期始，上月26号
select @begin= DATEADD(mm,DATEDIFF(mm,0,dateadd(month,-1,@monthbegin)),0) +25
--上上个月26号
select @lastmonth= DATEADD(mm,DATEDIFF(mm,0,dateadd(month,-1,DATEADD(mm,DATEDIFF(mm,0,dateadd(month,-1,@monthbegin)),0))),0) +25

set nocount on
create table #temp(cno nvarchar(20),cname nvarchar(20),SEX nvarchar(20),REGISTER_DATE date,LEAVE_DATE date,CFROM nvarchar(200),dept2 nvarchar(200),dept3 nvarchar(200),STATUS nvarchar(50),
begin_date date,end_date date,shift nvarchar(50),HOURS decimal(18,2),NORMAL_HOURS decimal(18,2),OVERTIME_HOURS decimal(18,2),PRICE decimal(18,2),MEAL_PRICE decimal(18,2),NIGHT_PRICE decimal(18,2),TRAVEL_PRICE decimal(18,2),salary decimal(18,4),work_type int)
--每人每天的工资情况；和出勤价格时间范围匹配时，以报道日期为准(后期可能调整为按出勤日期)
if @cno <> ''
begin
insert into #temp 
select e.CNO ,e.CNAME ,e.SEX ,e.REGISTER_DATE ,e.LEAVE_DATE ,e.CFROM ,e.DEPT2 ,e.DEPT3 ,e.STATUS,a.BEGIN_DATE ,a.END_DATE ,shift = case when isnull(a.shift,'') <> '' then a.shift else e.shift end ,isnull(a.HOURS,0) HOURS,isnull(a.NORMAL_HOURS,0) NORMAL_HOURS ,isnull(a.OVERTIME_HOURS,0) OVERTIME_HOURS,
isnull(p.PRICE,0) PRICE, isnull(p.MEAL_PRICE,0) MEAL_PRICE,isnull(p.NIGHT_PRICE,0) NIGHT_PRICE,isnull(p.TRAVEL_PRICE,0) TRAVEL_PRICE,isnull(a.HOURS,0)* isnull(p.PRICE,0) salary,work_type = case when f.FESTIVAL_TYPE IS not null then f.FESTIVAL_TYPE when  datepart(weekday,a.BEGIN_DATE) IN (1,7) then 1 else 0 end
 from COST_TEMP_EMPLOYEE_ATTENDANCE a 
left join COST_TEMP_EMPLOYEE e on a.CNO = e.CNO 
left join COST_TEMP_EMPLOYEE_PRICE p on e.CFROM = p.SUPPLIER and e.from_type = p.from_type and e.REGISTER_DATE >=p.BEGIN_DATE and e.REGISTER_DATE <= p.END_DATE 
left join COST_FESTIVAL f on a.BEGIN_DATE = f.FESTIVAL_DATE 
where a.BEGIN_DATE >= @begin and a.BEGIN_DATE <= @end and a.hours > 0 and isnull(isclose,0) = 0 and isnull(a.status,0) >= 2 and e.CNO = @cno
end
else if @dept3 <>'0'
begin 
insert into #temp 
select e.CNO ,e.CNAME ,e.SEX ,e.REGISTER_DATE ,e.LEAVE_DATE ,e.CFROM ,e.DEPT2 ,e.DEPT3 ,e.STATUS ,a.BEGIN_DATE ,a.END_DATE ,shift = case when isnull(a.shift,'') <> '' then a.shift else e.shift end ,isnull(a.HOURS,0) HOURS,isnull(a.NORMAL_HOURS,0) NORMAL_HOURS ,isnull(a.OVERTIME_HOURS,0) OVERTIME_HOURS,
isnull(p.PRICE,0) PRICE, isnull(p.MEAL_PRICE,0) MEAL_PRICE,isnull(p.NIGHT_PRICE,0) NIGHT_PRICE,isnull(p.TRAVEL_PRICE,0) TRAVEL_PRICE,isnull(a.HOURS,0)* isnull(p.PRICE,0) salary,work_type = case when f.FESTIVAL_TYPE IS not null then f.FESTIVAL_TYPE when  datepart(weekday,a.BEGIN_DATE) IN (1,7) then 1 else 0 end
 from COST_TEMP_EMPLOYEE_ATTENDANCE a 
left join COST_TEMP_EMPLOYEE e on a.CNO = e.CNO 
left join COST_TEMP_EMPLOYEE_PRICE p on e.CFROM = p.SUPPLIER and e.from_type = p.from_type and e.REGISTER_DATE >=p.BEGIN_DATE and e.REGISTER_DATE <= p.END_DATE 
left join COST_FESTIVAL f on a.BEGIN_DATE = f.FESTIVAL_DATE 
where a.BEGIN_DATE >= @begin and a.BEGIN_DATE <= @end and a.hours > 0 and isnull(isclose,0) = 0 and isnull(a.status,0) >= 2 and dept3 = @dept3 and dept2 = @dept2 and dept1 = @dept1
end
else if @dept2 <>'0'
begin 
insert into #temp 
select e.CNO ,e.CNAME ,e.SEX ,e.REGISTER_DATE ,e.LEAVE_DATE ,e.CFROM ,e.DEPT2 ,e.DEPT3 ,e.STATUS ,a.BEGIN_DATE ,a.END_DATE ,shift = case when isnull(a.shift,'') <> '' then a.shift else e.shift end ,isnull(a.HOURS,0) HOURS,isnull(a.NORMAL_HOURS,0) NORMAL_HOURS ,isnull(a.OVERTIME_HOURS,0) OVERTIME_HOURS,
isnull(p.PRICE,0) PRICE, isnull(p.MEAL_PRICE,0) MEAL_PRICE,isnull(p.NIGHT_PRICE,0) NIGHT_PRICE,isnull(p.TRAVEL_PRICE,0) TRAVEL_PRICE,isnull(a.HOURS,0)* isnull(p.PRICE,0) salary,work_type = case when f.FESTIVAL_TYPE IS not null then f.FESTIVAL_TYPE when  datepart(weekday,a.BEGIN_DATE) IN (1,7) then 1 else 0 end
 from COST_TEMP_EMPLOYEE_ATTENDANCE a 
left join COST_TEMP_EMPLOYEE e on a.CNO = e.CNO 
left join COST_TEMP_EMPLOYEE_PRICE p on e.CFROM = p.SUPPLIER and e.from_type = p.from_type and e.REGISTER_DATE >=p.BEGIN_DATE and e.REGISTER_DATE <= p.END_DATE 
left join COST_FESTIVAL f on a.BEGIN_DATE = f.FESTIVAL_DATE 
where a.BEGIN_DATE >= @begin and a.BEGIN_DATE <= @end and a.hours > 0 and isnull(isclose,0) = 0 and isnull(a.status,0) >= 2 and dept2 = @dept2 and dept1 = @dept1
end
else if @dept1 <>'0'
begin 
insert into #temp 
select e.CNO ,e.CNAME ,e.SEX ,e.REGISTER_DATE ,e.LEAVE_DATE ,e.CFROM ,e.DEPT2 ,e.DEPT3 ,e.STATUS ,a.BEGIN_DATE ,a.END_DATE ,shift = case when isnull(a.shift,'') <> '' then a.shift else e.shift end ,isnull(a.HOURS,0) HOURS,isnull(a.NORMAL_HOURS,0) NORMAL_HOURS ,isnull(a.OVERTIME_HOURS,0) OVERTIME_HOURS,
isnull(p.PRICE,0) PRICE, isnull(p.MEAL_PRICE,0) MEAL_PRICE,isnull(p.NIGHT_PRICE,0) NIGHT_PRICE,isnull(p.TRAVEL_PRICE,0) TRAVEL_PRICE,isnull(a.HOURS,0)* isnull(p.PRICE,0) salary,work_type = case when f.FESTIVAL_TYPE IS not null then f.FESTIVAL_TYPE when  datepart(weekday,a.BEGIN_DATE) IN (1,7) then 1 else 0 end
 from COST_TEMP_EMPLOYEE_ATTENDANCE a 
left join COST_TEMP_EMPLOYEE e on a.CNO = e.CNO 
left join COST_TEMP_EMPLOYEE_PRICE p on e.CFROM = p.SUPPLIER and e.from_type = p.from_type and e.REGISTER_DATE >=p.BEGIN_DATE and e.REGISTER_DATE <= p.END_DATE 
left join COST_FESTIVAL f on a.BEGIN_DATE = f.FESTIVAL_DATE 
where a.BEGIN_DATE >= @begin and a.BEGIN_DATE <= @end and a.hours > 0 and isnull(isclose,0) = 0 and isnull(a.status,0) >= 2 and dept1 = @dept1
end
else 
begin 
insert into #temp 
select e.CNO ,e.CNAME ,e.SEX ,e.REGISTER_DATE ,e.LEAVE_DATE ,e.CFROM ,e.DEPT2 ,e.DEPT3 ,e.STATUS ,a.BEGIN_DATE ,a.END_DATE ,shift = case when isnull(a.shift,'') <> '' then a.shift else e.shift end ,isnull(a.HOURS,0) HOURS,isnull(a.NORMAL_HOURS,0) NORMAL_HOURS ,isnull(a.OVERTIME_HOURS,0) OVERTIME_HOURS,
isnull(p.PRICE,0) PRICE, isnull(p.MEAL_PRICE,0) MEAL_PRICE,isnull(p.NIGHT_PRICE,0) NIGHT_PRICE,isnull(p.TRAVEL_PRICE,0) TRAVEL_PRICE,isnull(a.HOURS,0)* isnull(p.PRICE,0) salary,work_type = case when f.FESTIVAL_TYPE IS not null then f.FESTIVAL_TYPE when  datepart(weekday,a.BEGIN_DATE) IN (1,7) then 1 else 0 end
 from COST_TEMP_EMPLOYEE_ATTENDANCE a 
left join COST_TEMP_EMPLOYEE e on a.CNO = e.CNO 
left join COST_TEMP_EMPLOYEE_PRICE p on e.CFROM = p.SUPPLIER and e.from_type = p.from_type and e.REGISTER_DATE >=p.BEGIN_DATE and e.REGISTER_DATE <= p.END_DATE 
left join COST_FESTIVAL f on a.BEGIN_DATE = f.FESTIVAL_DATE 
where a.BEGIN_DATE >= @begin and a.BEGIN_DATE <= @end and a.hours > 0 and isnull(isclose,0) = 0 and isnull(a.status,0) >= 2 
end
--检查数据：考勤重复提报或者出勤价格时间范围有交叉部分
if exists (select COUNT(*) from #temp group by cno,begin_date having COUNT(*)>1)
begin
	raiserror 20000 '考勤记录可能有重复值!'
	return
end
--每人合计
select cno,max(CNAME) CNAME,max(SEX) SEX,max(REGISTER_DATE) REGISTER_DATE,max(LEAVE_DATE) LEAVE_DATE,max(CFROM) CFROM,max(DEPT2) DEPT2,max(DEPT3) DEPT3,max(STATUS) STATUS,COUNT(*) sum_days,
sum(isnull(HOURS,0)) SUM_HOURS,sum(isnull(NORMAL_HOURS,0)) NORMAL_HOURS,sum(isnull(OVERTIME_HOURS,0)) OVERTIME_HOURS,SUM(salary) salary,SUM(MEAL_PRICE) MEAL,MAX(TRAVEL_PRICE) travel  
 into #temp1 from #temp group by cno

--每人合计:日常班
select cno,sum(isnull(HOURS,0)) HOURS1,sum(isnull(NORMAL_HOURS,0)) NORMAL_HOURS,sum(isnull(OVERTIME_HOURS,0)) OVERTIME_HOURS,SUM(salary) salary1
 into #temp2 from #temp where work_type = 0 group by cno

--每人合计:周末班
select cno,sum(isnull(HOURS,0)) HOURS2,SUM(salary) salary2
 into #temp3 from #temp where work_type = 1 group by cno

--每人合计:节假日班
select cno,sum(isnull(HOURS,0)) HOURS3,SUM(salary) salary3
 into #temp4 from #temp where work_type = 2 group by cno

--每人合计:夜班补助
select cno,isnull(COUNT(*),0) nights,isnull(sum(isnull(NIGHT_PRICE,0)),0)  salary4
 into #temp5 from #temp where shift like '夜班%' group by cno
 
--工时汇总
select t1.CNO 工号,t1.CNAME 姓名,t1.SEX 性别,t1.REGISTER_DATE 报道日期,t1.LEAVE_DATE 离职日期,t1.CFROM 输送渠道,t1.DEPT2 二级部门,t1.DEPT3 三级部门,t1.STATUS 状态,t1.twodays_leave 两天内离职,
t1.SUM_HOURS 总工时数,t1.NORMAL_HOURS 正常班时数,t1.OVERTIME_HOURS 加班时数,t5.nights 夜班天数,t5.salary4 夜班补助,t1.salary+isnull(t5.salary4,0) 工资,t2.HOURS1 工作日时数,t2.NORMAL_HOURS 工作日8H内时数,
t2.OVERTIME_HOURS 工作日8H外时数,t3.HOURS2 周末时数,t4.HOURS3 节假日时数 
from #temp1 t1 left join #temp2 t2 on t1.cno = t2.cno left join #temp3 t3 on t1.cno = t3.cno left join #temp4 t4 on t1.cno = t4.cno left join #temp5 t5 on t1.cno= t5.cno
--drop table #temp
--drop table #temp1
--drop table #temp2
--drop table #temp3
--drop table #temp4
--drop table #temp5
--drop table #temp6
--drop table #temp7
--drop table #temp8
select * from #temp
select * from #temp1

declare
@cmonth varchar(20),@begin datetime,@end datetime,@cno varchar(20),@dept1 varchar(50),@dept2 varchar(200),@dept3 varchar(200),@lastmonth varchar(20)
select @cmonth = '2017-11',@dept1 = '制造中心',@cno = '',@dept2 = '0',@dept3 = '0'
set @begin = @cmonth + '-01'
--月末、上月初
select @end = convert(varchar,dateadd(day,-day(@begin),dateadd(month,1,@begin)),23),@lastmonth= DATEADD(mm,DATEDIFF(mm,0,dateadd(month,-1,@begin)),0)
--上月入职但上月未离职并且上月工时累计小于24h,这部分是要计入当月的,这部分没有计算补助的，而这是需要计算的
select e.cno, max(e.register_date) register_date,max(e.leave_date) leave_date,count(*) last_days,sum(isnull(HOURS,0)) last_hours24,sum(isnull(a.HOURS,0)* isnull(p.PRICE,0)) last_hours24_salary,sum(p.MEAL_PRICE) meal,SUM(case when case when isnull(a.shift,'') <> '' then a.shift else e.shift end like '夜班%' then p.NIGHT_PRICE else 0 end) NIGHT into #temp6 
from  (select * from COST_TEMP_EMPLOYEE where REGISTER_DATE >= @lastmonth and REGISTER_DATE < @begin and (LEAVE_DATE is null or LEAVE_DATE >=@begin)) e 
left join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno 
left join COST_TEMP_EMPLOYEE_PRICE p on e.CFROM = p.SUPPLIER and e.from_type = p.from_type and e.REGISTER_DATE >=p.BEGIN_DATE and e.REGISTER_DATE <= p.END_DATE 
where a.BEGIN_DATE >= @lastmonth and a.BEGIN_DATE < @begin and a.hours > 0 and isnull(isclose,0) = 0 and isnull(a.status,0) >= 2 group by e.cno having sum(isnull(HOURS,0)) <24
--本月入职并本月工时累计小于24h,这部分用来展示的，也和本月工时一致,本月结算工时为0
select e.cno, max(e.register_date) register_date,max(e.leave_date) leave_date,sum(isnull(HOURS,0)) hours24 into #temp7 
from  (select * from COST_TEMP_EMPLOYEE where REGISTER_DATE >= @begin and REGISTER_DATE <= @end) e left join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno
where a.BEGIN_DATE >= @begin and a.BEGIN_DATE <= @end and a.hours > 0 and isnull(isclose,0) = 0 and isnull(a.status,0) >= 2 group by e.cno having sum(isnull(HOURS,0)) <24
--上月和本月入职，合计工时小于24，本月结算工时为0
select e.cno, max(e.register_date) register_date,max(e.leave_date) leave_date,sum(isnull(HOURS,0)) sum_hours24 into #temp8 
from  (select * from COST_TEMP_EMPLOYEE where REGISTER_DATE >= @lastmonth and REGISTER_DATE <= @end and (LEAVE_DATE is null or LEAVE_DATE >=@begin)) e left join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno
where a.BEGIN_DATE >= @lastmonth and a.BEGIN_DATE <= @end and a.hours > 0 and isnull(isclose,0) = 0 and isnull(a.status,0) >= 2 group by e.cno having sum(isnull(HOURS,0)) <24

declare
@cmonth varchar(20),@begin datetime,@end datetime,@cno varchar(20),@dept1 varchar(50),@dept2 varchar(200),@dept3 varchar(200),@lastmonth varchar(20)
select @cmonth = '2017-11',@dept1 = '制造中心',@cno = '',@dept2 = '0',@dept3 = '0'
set @begin = @cmonth + '-01'
--月末、上月初
select @end = convert(varchar,dateadd(day,-day(@begin),dateadd(month,1,@begin)),23),@lastmonth= DATEADD(mm,DATEDIFF(mm,0,dateadd(month,-1,@begin)),0)
--工时汇总
select t1.CNO 工号,t1.CNAME 姓名,t1.SEX 性别,t1.REGISTER_DATE 报道日期,t1.LEAVE_DATE 离职日期,t1.CFROM 输送渠道,t1.DEPT2 二级部门,t1.DEPT3 三级部门,t1.STATUS 状态,t1.sum_days 在岗天数,
t2.HOURS1 工作日时数,t2.NORMAL_HOURS 工作日8H内时数,
t2.OVERTIME_HOURS 工作日8H外时数,t3.HOURS2 周末时数,t4.HOURS3 节假日时数,t1.SUM_HOURS 本月工时数,case when t8.sum_hours24 IS null then isnull(t1.SUM_HOURS,0) + isnull(t6.last_hours24,0) else 0 end 应结算工时数,t1.MEAL 餐补,t5.nights 夜班天数,t5.salary4 夜班补助,
case when (t1.REGISTER_DATE >= @begin and t1.REGISTER_DATE <= @end and t8.sum_hours24 IS null) OR (t6.last_hours24 is not null and t8.sum_hours24 IS null) then t1.travel else 0 end 车补, 
case when t8.sum_hours24 IS null then t1.salary+t1.MEAL + isnull(t5.salary4,0)+ ISNULL(t6.last_hours24_salary,0) + ISNULL(t6.meal,0)+ ISNULL(t6.night,0) + case when (t1.REGISTER_DATE >= @begin and t1.REGISTER_DATE <= @end and t8.sum_hours24 IS null) OR (t6.last_hours24 is not null and t8.sum_hours24 IS null) then t1.travel else 0 end  else 0 end 应付工资,
t6.last_days 上月未结算天数,t6.last_hours24 上月未结算工时,t6.meal 上月未结算餐补,t6.night 上月未结算夜班补助,t7.hours24 本月未结算工时
from #temp1 t1 left join #temp2 t2 on t1.cno = t2.cno 
left join #temp3 t3 on t1.cno = t3.cno 
left join #temp4 t4 on t1.cno = t4.cno 
left join #temp5 t5 on t1.cno= t5.cno 
left join #temp6 t6 on t1.cno = t6.CNO 
left join #temp7 t7 on t1.cno= t7.cno 
left join #temp8 t8 on t1.cno= t8.cno 
