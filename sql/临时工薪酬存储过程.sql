alter procedure [dbo].[COST_TEMP_EMPLOYEE_SALARY]
@cmonth varchar(20),@cno varchar(20),@dept1 varchar(50),@dept2 varchar(200),@dept3 varchar(200)
as
--select @cno = '201700014',@begin = '2017-09-11',@end = '2017-09-21'
set nocount on
declare @monthbegin datetime,@begin datetime,@end datetime,@lastmonth  datetime
set @monthbegin = @cmonth + '-01'
set @end = @cmonth + '-25'
--周期始，上月26号
select @begin= DATEADD(mm,DATEDIFF(mm,0,dateadd(month,-1,@monthbegin)),0) +25
--上上个月26号
select @lastmonth= DATEADD(mm,DATEDIFF(mm,0,dateadd(month,-1,DATEADD(mm,DATEDIFF(mm,0,dateadd(month,-1,@monthbegin)),0))),0) +25
create table #temp(cno nvarchar(20),cname nvarchar(20),SEX nvarchar(20),REGISTER_DATE date,LEAVE_DATE date,CFROM nvarchar(200),FROM_TYPE INT,dept1 nvarchar(200),dept2 nvarchar(200),dept3 nvarchar(200),STATUS nvarchar(50),
begin_date date,end_date date,shift nvarchar(50),HOURS decimal(18,2),NORMAL_HOURS decimal(18,2),OVERTIME_HOURS decimal(18,2),PRICE decimal(18,2),MEAL_PRICE decimal(18,2),NIGHT_PRICE decimal(18,2),TRAVEL_PRICE decimal(18,2),REGULAR_PRICE decimal(18,2),standard_price decimal(18,2),insurance_price decimal(18,2),travel_expense decimal(18,2),salary decimal(18,4),salary2 decimal(18,4),work_type int)
--每人每天的工资情况；和出勤价格时间范围匹配时，以报到日期为准(后期可能调整为按出勤日期)
insert into #temp 
select e.CNO ,e.CNAME ,e.SEX ,e.REGISTER_DATE ,e.LEAVE_DATE ,e.CFROM ,e.from_type,e.DEPT1,e.DEPT2,e.DEPT3 ,e.STATUS,a.BEGIN_DATE ,a.END_DATE ,shift = case when isnull(a.shift,'') <> '' then a.shift else e.shift end ,isnull(a.HOURS,0) HOURS,isnull(a.NORMAL_HOURS,0) NORMAL_HOURS ,isnull(a.OVERTIME_HOURS,0) OVERTIME_HOURS,
isnull(p.PRICE,0) PRICE, isnull(p.MEAL_PRICE,0) MEAL_PRICE,isnull(p.NIGHT_PRICE,0) NIGHT_PRICE,isnull(p.TRAVEL_PRICE,0) TRAVEL_PRICE,isnull(p.regular_price,0) regular_price,isnull(p2.price,0) standard_price,isnull(p2.insurance_price,0) insurance_price,isnull(t.expense,0) travel_expense,isnull(a.HOURS,0)* isnull(p.PRICE,0) salary,
case when f.FESTIVAL_TYPE IS not null then 3*isnull(a.HOURS,0)* isnull(p2.PRICE,0) when  datepart(weekday,a.BEGIN_DATE) IN (1,7) then 2*isnull(a.HOURS,0)* isnull(p2.PRICE,0) else (1.5*isnull(a.OVERTIME_HOURS,0) + isnull(a.NORMAL_HOURS,0))* isnull(p2.PRICE,0) end salary2,
work_type = case when f.FESTIVAL_TYPE IS not null then f.FESTIVAL_TYPE when  datepart(weekday,a.BEGIN_DATE) IN (1,7) then 1 else 0 end
 from COST_TEMP_EMPLOYEE_ATTENDANCE a 
left join COST_TEMP_EMPLOYEE e on a.CNO = e.CNO 
left join COST_TEMP_EMPLOYEE_PRICE p on e.CFROM = p.SUPPLIER and e.from_type = p.from_type and e.REGISTER_DATE >=p.BEGIN_DATE and e.REGISTER_DATE <= p.END_DATE 
left join COST_TEMP_EMPLOYEE_PRICE p2 on p2.type = 2 and a.BEGIN_DATE >=p2.BEGIN_DATE and a.BEGIN_DATE <= p2.END_DATE 
left join COST_FESTIVAL f on a.BEGIN_DATE = f.FESTIVAL_DATE 
left join COST_EXPENSE t on e.CNO = t.EMP_NO and a.BEGIN_DATE = t.BEGIN_DATE and t.CTYPE = 2 
where a.BEGIN_DATE >= @begin and a.BEGIN_DATE <= @end and a.hours > 0 and isnull(isclose,0) = 0 and isnull(a.status,0) >= 2 

--检查数据：考勤重复提报或者出勤价格时间范围有交叉部分
if exists (select COUNT(*) from #temp group by cno,begin_date having COUNT(*)>1)
begin
	raiserror 20000 '考勤记录可能有重复值!'
	return
end
--每人合计
select cno,max(p.CNAME) CNAME,max(SEX) SEX,max(REGISTER_DATE) REGISTER_DATE,max(LEAVE_DATE) LEAVE_DATE,max(CFROM) CFROM,max(d.cname) from_type,max(DEPT1) DEPT1,max(DEPT2) DEPT2,max(DEPT3) DEPT3,max(STATUS) STATUS,COUNT(*) sum_days,
sum(isnull(HOURS,0)) SUM_HOURS,sum(isnull(NORMAL_HOURS,0)) NORMAL_HOURS,sum(isnull(OVERTIME_HOURS,0)) OVERTIME_HOURS,SUM(salary) salary,SUM(salary2) salary2,SUM(MEAL_PRICE) MEAL,MAX(TRAVEL_PRICE) travel,case when max(STATUS) = '转正' and max(LEAVE_DATE) >= @begin and max(LEAVE_DATE) <= @end then MAX(REGULAR_PRICE) else 0 end regular,min(insurance_price) insurance,SUM(travel_expense) travel_expense   
 into #temp1 from #temp p left join cost_base_data d on p.FROM_TYPE = d.SUB_ID and d.MODULE_ID = 1 group by cno

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

--#temp6上月入职但上月未离职并且上月工时累计小于24h,这部分是要计入当月的
select e.cno, max(e.register_date) register_date,max(e.leave_date) leave_date,count(*) last_days,sum(isnull(HOURS,0)) last_hours24,sum(isnull(a.HOURS,0)* isnull(p.PRICE,0)) last_hours24_salary,
sum(case when f.FESTIVAL_TYPE IS not null then 3*isnull(a.HOURS,0)* isnull(p2.PRICE,0) when  datepart(weekday,a.BEGIN_DATE) IN (1,7) then 2*isnull(a.HOURS,0)* isnull(p2.PRICE,0) else (1.5*isnull(a.OVERTIME_HOURS,0) + isnull(a.NORMAL_HOURS,0))* isnull(p2.PRICE,0) end) last_hours24_salary2,
sum(p.MEAL_PRICE) meal,SUM(case when case when isnull(a.shift,'') <> '' then a.shift else e.shift end like '夜班%' then p.NIGHT_PRICE else 0 end) NIGHT,SUM(expense) travel_expense into #temp6 
from  (select * from COST_TEMP_EMPLOYEE where REGISTER_DATE >= @lastmonth and REGISTER_DATE < @begin and (LEAVE_DATE is null or LEAVE_DATE >=@begin)) e 
left join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno 
left join COST_TEMP_EMPLOYEE_PRICE p on e.CFROM = p.SUPPLIER and e.from_type = p.from_type and e.REGISTER_DATE >=p.BEGIN_DATE and e.REGISTER_DATE <= p.END_DATE 
left join COST_TEMP_EMPLOYEE_PRICE p2 on p2.type = 2 and a.BEGIN_DATE >=p2.BEGIN_DATE and a.BEGIN_DATE <= p2.END_DATE 
left join COST_FESTIVAL f on a.BEGIN_DATE = f.FESTIVAL_DATE 
left join COST_EXPENSE t on e.CNO = t.EMP_NO and a.BEGIN_DATE = t.BEGIN_DATE and t.CTYPE = 2 
where a.BEGIN_DATE >= @lastmonth and a.BEGIN_DATE < @begin and a.hours > 0 and isnull(isclose,0) = 0 and isnull(a.status,0) >= 2 group by e.cno having sum(isnull(HOURS,0)) <24
--#temp7本月入职并本月工时累计小于24h,本月结算工时为0
select e.cno, max(e.register_date) register_date,max(e.leave_date) leave_date,sum(isnull(HOURS,0)) hours24 into #temp7 
from  (select * from COST_TEMP_EMPLOYEE where REGISTER_DATE >= @begin and REGISTER_DATE <= @end) e left join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno
where a.BEGIN_DATE >= @begin and a.BEGIN_DATE <= @end and a.hours > 0 and isnull(isclose,0) = 0 and isnull(a.status,0) >= 2 group by e.cno having sum(isnull(HOURS,0)) <24
--#temp8上月和本月入职，合计工时小于24，本月结算工时为0
select e.cno, max(e.register_date) register_date,max(e.leave_date) leave_date,sum(isnull(HOURS,0)) sum_hours24 into #temp8 
from  (select * from COST_TEMP_EMPLOYEE where REGISTER_DATE >= @lastmonth and REGISTER_DATE <= @end and (LEAVE_DATE is null or LEAVE_DATE >=@begin)) e left join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno
where a.BEGIN_DATE >= @lastmonth and a.BEGIN_DATE <= @end and a.hours > 0 and isnull(isclose,0) = 0 and isnull(a.status,0) >= 2 group by e.cno having sum(isnull(HOURS,0)) <24
 
--工时汇总
select t1.CNO 工号,t1.CNAME 姓名,t1.SEX 性别,isnull(convert(varchar(20),t1.REGISTER_DATE,23),'') 报道日期,isnull(convert(varchar(20),t1.LEAVE_DATE,23),'') 离职日期,t1.CFROM 输送渠道,t1.from_type 输送类型,t1.DEPT1 一级部门,t1.DEPT2 二级部门,t1.DEPT3 三级部门,t1.STATUS 状态,t1.sum_days 在岗天数,
t1.SUM_HOURS 本月工时数,case when t8.sum_hours24 IS null then isnull(t1.SUM_HOURS,0) + isnull(t6.last_hours24,0) else 0 end 应结算工时数,isnull(t1.MEAL,0) 餐补,isnull(t5.nights,0) 夜班天数,isnull(t5.salary4,0) 夜班补助,
case when (t1.REGISTER_DATE >= @begin and t1.REGISTER_DATE <= @end and t8.sum_hours24 IS null) OR (t6.last_hours24 is not null and t8.sum_hours24 IS null) then isnull(t1.travel,0) else 0 end 车补, isnull(t1.travel_expense,0) 交通补贴,isnull(t1.regular,0) 转正费,
case when t8.sum_hours24 IS null then t1.salary+t1.MEAL + t1.travel_expense + isnull(t5.salary4,0)+ ISNULL(t6.last_hours24_salary,0) + ISNULL(t6.meal,0)+ ISNULL(t6.travel_expense,0) + ISNULL(t6.night,0) + case when (t1.REGISTER_DATE >= @begin and t1.REGISTER_DATE <= @end and t8.sum_hours24 IS null) OR (t6.last_hours24 is not null and t8.sum_hours24 IS null) then t1.travel else 0 end  else 0 end 应付工资,isnull(t2.HOURS1,0) 工作日时数,isnull(t2.NORMAL_HOURS,0) 工作日8H内时数,isnull(t2.OVERTIME_HOURS,0) 工作日8H外时数,isnull(t3.HOURS2,0) 周末时数,isnull(t4.HOURS3,0) 节假日时数,
case when t8.sum_hours24 IS null then t1.salary2 + t1.insurance + ISNULL(t6.last_hours24_salary2,0) - (t1.salary+t1.MEAL + t1.travel_expense + isnull(t5.salary4,0)+ ISNULL(t6.last_hours24_salary,0) + ISNULL(t6.meal,0)+ ISNULL(t6.travel_expense,0) + ISNULL(t6.night,0) + case when (t1.REGISTER_DATE >= @begin and t1.REGISTER_DATE <= @end and t8.sum_hours24 IS null) OR (t6.last_hours24 is not null and t8.sum_hours24 IS null) then t1.travel else 0 end + isnull(t1.regular,0))  else 0 end 节省费用,
isnull(t6.last_days,0) 上月未结算天数,isnull(t6.last_hours24,0) 上月未结算工时,isnull(t6.meal,0) 上月未结算餐补,isnull(t6.night,0) 上月未结算夜班补助,ISNULL(t6.travel_expense,0) 上月未结算交通补贴,isnull(t7.hours24,0) 本月未结算工时 into #temp9
from #temp1 t1 left join #temp2 t2 on t1.cno = t2.cno 
left join #temp3 t3 on t1.cno = t3.cno 
left join #temp4 t4 on t1.cno = t4.cno 
left join #temp5 t5 on t1.cno= t5.cno 
left join #temp6 t6 on t1.cno = t6.CNO 
left join #temp7 t7 on t1.cno= t7.cno 
left join #temp8 t8 on t1.cno= t8.cno 

if @cno <> ''
begin
select * from #temp9 where 工号 like '%' + @cno + '%'
end
else if @dept3 <>'0'
begin 
select SUM(在岗天数) 在岗天数,SUM(本月工时数) 本月工时数,SUM(应结算工时数) 应结算工时数, sum(餐补) 餐补,SUM(夜班天数) 夜班天数,SUM(夜班补助) 夜班补助,SUM(车补) 车补,sum(交通补贴) 交通补贴,SUM(转正费) 转正费,SUM(应付工资) 应付工资,SUM(工作日时数) 工作日时数,SUM(工作日8H内时数) 工作日8H内时数,SUM(工作日8H外时数) 工作日8H外时数,SUM(周末时数) 周末时数,SUM(节假日时数) 节假日时数,SUM(节省费用) 节省费用,SUM(上月未结算天数) 上月未结算天数,sum(上月未结算工时) 上月未结算工时,SUM(上月未结算餐补) 上月未结算餐补,SUM(上月未结算夜班补助) 上月未结算夜班补助,sum(上月未结算交通补贴) 上月未结算交通补贴,SUM(本月未结算工时) 本月未结算工时 into #temp10 from #temp9 where  三级部门 = @dept3 and 二级部门 = @dept2 and 一级部门 = @dept1
select * from #temp9 where  三级部门 = @dept3 and 二级部门 = @dept2 and 一级部门 = @dept1 
union select '汇总：'工号,'' 姓名,'' 性别,'' 报道日期,'' 离职日期,'' 输送渠道,'' 输送类型,'' 一级部门,'' 二级部门,'' 三级部门,'' 状态,* from #temp10
end
else if @dept2 <>'0'
begin 
select SUM(在岗天数) 在岗天数,SUM(本月工时数) 本月工时数,SUM(应结算工时数) 应结算工时数, sum(餐补) 餐补,SUM(夜班天数) 夜班天数,SUM(夜班补助) 夜班补助,SUM(车补) 车补,sum(交通补贴) 交通补贴,SUM(转正费) 转正费,SUM(应付工资) 应付工资,SUM(工作日时数) 工作日时数,SUM(工作日8H内时数) 工作日8H内时数,SUM(工作日8H外时数) 工作日8H外时数,SUM(周末时数) 周末时数,SUM(节假日时数) 节假日时数,SUM(节省费用) 节省费用,SUM(上月未结算天数) 上月未结算天数,sum(上月未结算工时) 上月未结算工时,SUM(上月未结算餐补) 上月未结算餐补,SUM(上月未结算夜班补助) 上月未结算夜班补助,sum(上月未结算交通补贴) 上月未结算交通补贴,SUM(本月未结算工时) 本月未结算工时 into #temp11 from #temp9 where  二级部门 = @dept2 and 一级部门 = @dept1
select * from #temp9 where   二级部门 = @dept2 and 一级部门 = @dept1 
union select '汇总：'工号,'' 姓名,'' 性别,'' 报道日期,'' 离职日期,'' 输送渠道,'' 输送类型,'' 一级部门,'' 二级部门,'' 三级部门,'' 状态,* from #temp11
end
else if @dept1 <>'0'
begin 
select SUM(在岗天数) 在岗天数,SUM(本月工时数) 本月工时数,SUM(应结算工时数) 应结算工时数, sum(餐补) 餐补,SUM(夜班天数) 夜班天数,SUM(夜班补助) 夜班补助,SUM(车补) 车补,sum(交通补贴) 交通补贴,SUM(转正费) 转正费,SUM(应付工资) 应付工资,SUM(工作日时数) 工作日时数,SUM(工作日8H内时数) 工作日8H内时数,SUM(工作日8H外时数) 工作日8H外时数,SUM(周末时数) 周末时数,SUM(节假日时数) 节假日时数,SUM(节省费用) 节省费用,SUM(上月未结算天数) 上月未结算天数,sum(上月未结算工时) 上月未结算工时,SUM(上月未结算餐补) 上月未结算餐补,SUM(上月未结算夜班补助) 上月未结算夜班补助,sum(上月未结算交通补贴) 上月未结算交通补贴,SUM(本月未结算工时) 本月未结算工时 into #temp12 from #temp9 where  一级部门 = @dept1
select * from #temp9 where   一级部门 = @dept1 
union select '汇总：'工号,'' 姓名,'' 性别,'' 报道日期,'' 离职日期,'' 输送渠道,'' 输送类型,'' 一级部门,'' 二级部门,'' 三级部门,'' 状态,* from #temp12
end
else
begin 
select SUM(在岗天数) 在岗天数,SUM(本月工时数) 本月工时数,SUM(应结算工时数) 应结算工时数, sum(餐补) 餐补,SUM(夜班天数) 夜班天数,SUM(夜班补助) 夜班补助,SUM(车补) 车补,sum(交通补贴) 交通补贴,SUM(转正费) 转正费,SUM(应付工资) 应付工资,SUM(工作日时数) 工作日时数,SUM(工作日8H内时数) 工作日8H内时数,SUM(工作日8H外时数) 工作日8H外时数,SUM(周末时数) 周末时数,SUM(节假日时数) 节假日时数,SUM(节省费用) 节省费用,SUM(上月未结算天数) 上月未结算天数,sum(上月未结算工时) 上月未结算工时,SUM(上月未结算餐补) 上月未结算餐补,SUM(上月未结算夜班补助) 上月未结算夜班补助,sum(上月未结算交通补贴) 上月未结算交通补贴,SUM(本月未结算工时) 本月未结算工时 into #temp13 from #temp9 
select * from #temp9 
union select '汇总：'工号,'' 姓名,'' 性别,'' 报道日期,'' 离职日期,'' 输送渠道,'' 输送类型,'' 一级部门,'' 二级部门,'' 三级部门,'' 状态,* from #temp13
end
--drop table #temp1
--drop table #temp2
--drop table #temp3
--drop table #temp4
--drop table #temp5
