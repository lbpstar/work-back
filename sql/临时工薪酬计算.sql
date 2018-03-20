declare
@cmonth varchar(20),@monthbegin datetime,@begin datetime,@end datetime,@cno varchar(20),@dept1 varchar(50),@dept2 varchar(200),@dept3 varchar(200),@lastmonth datetime

select @dept1 = '��������',@cno = '',@dept2 = '0',@dept3 = '0'
set @cmonth = '2017-11'
set @monthbegin = @cmonth + '-01'
set @end = @cmonth + '-25'
--����ʼ������26��
select @begin= DATEADD(mm,DATEDIFF(mm,0,dateadd(month,-1,@monthbegin)),0) +25
--���ϸ���26��
select @lastmonth= DATEADD(mm,DATEDIFF(mm,0,dateadd(month,-1,DATEADD(mm,DATEDIFF(mm,0,dateadd(month,-1,@monthbegin)),0))),0) +25

set nocount on
create table #temp(cno nvarchar(20),cname nvarchar(20),SEX nvarchar(20),REGISTER_DATE date,LEAVE_DATE date,CFROM nvarchar(200),dept2 nvarchar(200),dept3 nvarchar(200),STATUS nvarchar(50),
begin_date date,end_date date,shift nvarchar(50),HOURS decimal(18,2),NORMAL_HOURS decimal(18,2),OVERTIME_HOURS decimal(18,2),PRICE decimal(18,2),MEAL_PRICE decimal(18,2),NIGHT_PRICE decimal(18,2),TRAVEL_PRICE decimal(18,2),salary decimal(18,4),work_type int)
--ÿ��ÿ��Ĺ���������ͳ��ڼ۸�ʱ�䷶Χƥ��ʱ���Ա�������Ϊ׼(���ڿ��ܵ���Ϊ����������)
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
--������ݣ������ظ��ᱨ���߳��ڼ۸�ʱ�䷶Χ�н��沿��
if exists (select COUNT(*) from #temp group by cno,begin_date having COUNT(*)>1)
begin
	raiserror 20000 '���ڼ�¼�������ظ�ֵ!'
	return
end
--ÿ�˺ϼ�
select cno,max(CNAME) CNAME,max(SEX) SEX,max(REGISTER_DATE) REGISTER_DATE,max(LEAVE_DATE) LEAVE_DATE,max(CFROM) CFROM,max(DEPT2) DEPT2,max(DEPT3) DEPT3,max(STATUS) STATUS,COUNT(*) sum_days,
sum(isnull(HOURS,0)) SUM_HOURS,sum(isnull(NORMAL_HOURS,0)) NORMAL_HOURS,sum(isnull(OVERTIME_HOURS,0)) OVERTIME_HOURS,SUM(salary) salary,SUM(MEAL_PRICE) MEAL,MAX(TRAVEL_PRICE) travel  
 into #temp1 from #temp group by cno

--ÿ�˺ϼ�:�ճ���
select cno,sum(isnull(HOURS,0)) HOURS1,sum(isnull(NORMAL_HOURS,0)) NORMAL_HOURS,sum(isnull(OVERTIME_HOURS,0)) OVERTIME_HOURS,SUM(salary) salary1
 into #temp2 from #temp where work_type = 0 group by cno

--ÿ�˺ϼ�:��ĩ��
select cno,sum(isnull(HOURS,0)) HOURS2,SUM(salary) salary2
 into #temp3 from #temp where work_type = 1 group by cno

--ÿ�˺ϼ�:�ڼ��հ�
select cno,sum(isnull(HOURS,0)) HOURS3,SUM(salary) salary3
 into #temp4 from #temp where work_type = 2 group by cno

--ÿ�˺ϼ�:ҹ�ಹ��
select cno,isnull(COUNT(*),0) nights,isnull(sum(isnull(NIGHT_PRICE,0)),0)  salary4
 into #temp5 from #temp where shift like 'ҹ��%' group by cno
 
--��ʱ����
select t1.CNO ����,t1.CNAME ����,t1.SEX �Ա�,t1.REGISTER_DATE ��������,t1.LEAVE_DATE ��ְ����,t1.CFROM ��������,t1.DEPT2 ��������,t1.DEPT3 ��������,t1.STATUS ״̬,t1.twodays_leave ��������ְ,
t1.SUM_HOURS �ܹ�ʱ��,t1.NORMAL_HOURS ������ʱ��,t1.OVERTIME_HOURS �Ӱ�ʱ��,t5.nights ҹ������,t5.salary4 ҹ�ಹ��,t1.salary+isnull(t5.salary4,0) ����,t2.HOURS1 ������ʱ��,t2.NORMAL_HOURS ������8H��ʱ��,
t2.OVERTIME_HOURS ������8H��ʱ��,t3.HOURS2 ��ĩʱ��,t4.HOURS3 �ڼ���ʱ�� 
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
select @cmonth = '2017-11',@dept1 = '��������',@cno = '',@dept2 = '0',@dept3 = '0'
set @begin = @cmonth + '-01'
--��ĩ�����³�
select @end = convert(varchar,dateadd(day,-day(@begin),dateadd(month,1,@begin)),23),@lastmonth= DATEADD(mm,DATEDIFF(mm,0,dateadd(month,-1,@begin)),0)
--������ְ������δ��ְ�������¹�ʱ�ۼ�С��24h,�ⲿ����Ҫ���뵱�µ�,�ⲿ��û�м��㲹���ģ���������Ҫ�����
select e.cno, max(e.register_date) register_date,max(e.leave_date) leave_date,count(*) last_days,sum(isnull(HOURS,0)) last_hours24,sum(isnull(a.HOURS,0)* isnull(p.PRICE,0)) last_hours24_salary,sum(p.MEAL_PRICE) meal,SUM(case when case when isnull(a.shift,'') <> '' then a.shift else e.shift end like 'ҹ��%' then p.NIGHT_PRICE else 0 end) NIGHT into #temp6 
from  (select * from COST_TEMP_EMPLOYEE where REGISTER_DATE >= @lastmonth and REGISTER_DATE < @begin and (LEAVE_DATE is null or LEAVE_DATE >=@begin)) e 
left join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno 
left join COST_TEMP_EMPLOYEE_PRICE p on e.CFROM = p.SUPPLIER and e.from_type = p.from_type and e.REGISTER_DATE >=p.BEGIN_DATE and e.REGISTER_DATE <= p.END_DATE 
where a.BEGIN_DATE >= @lastmonth and a.BEGIN_DATE < @begin and a.hours > 0 and isnull(isclose,0) = 0 and isnull(a.status,0) >= 2 group by e.cno having sum(isnull(HOURS,0)) <24
--������ְ�����¹�ʱ�ۼ�С��24h,�ⲿ������չʾ�ģ�Ҳ�ͱ��¹�ʱһ��,���½��㹤ʱΪ0
select e.cno, max(e.register_date) register_date,max(e.leave_date) leave_date,sum(isnull(HOURS,0)) hours24 into #temp7 
from  (select * from COST_TEMP_EMPLOYEE where REGISTER_DATE >= @begin and REGISTER_DATE <= @end) e left join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno
where a.BEGIN_DATE >= @begin and a.BEGIN_DATE <= @end and a.hours > 0 and isnull(isclose,0) = 0 and isnull(a.status,0) >= 2 group by e.cno having sum(isnull(HOURS,0)) <24
--���ºͱ�����ְ���ϼƹ�ʱС��24�����½��㹤ʱΪ0
select e.cno, max(e.register_date) register_date,max(e.leave_date) leave_date,sum(isnull(HOURS,0)) sum_hours24 into #temp8 
from  (select * from COST_TEMP_EMPLOYEE where REGISTER_DATE >= @lastmonth and REGISTER_DATE <= @end and (LEAVE_DATE is null or LEAVE_DATE >=@begin)) e left join COST_TEMP_EMPLOYEE_ATTENDANCE a on e.cno = a.cno
where a.BEGIN_DATE >= @lastmonth and a.BEGIN_DATE <= @end and a.hours > 0 and isnull(isclose,0) = 0 and isnull(a.status,0) >= 2 group by e.cno having sum(isnull(HOURS,0)) <24

declare
@cmonth varchar(20),@begin datetime,@end datetime,@cno varchar(20),@dept1 varchar(50),@dept2 varchar(200),@dept3 varchar(200),@lastmonth varchar(20)
select @cmonth = '2017-11',@dept1 = '��������',@cno = '',@dept2 = '0',@dept3 = '0'
set @begin = @cmonth + '-01'
--��ĩ�����³�
select @end = convert(varchar,dateadd(day,-day(@begin),dateadd(month,1,@begin)),23),@lastmonth= DATEADD(mm,DATEDIFF(mm,0,dateadd(month,-1,@begin)),0)
--��ʱ����
select t1.CNO ����,t1.CNAME ����,t1.SEX �Ա�,t1.REGISTER_DATE ��������,t1.LEAVE_DATE ��ְ����,t1.CFROM ��������,t1.DEPT2 ��������,t1.DEPT3 ��������,t1.STATUS ״̬,t1.sum_days �ڸ�����,
t2.HOURS1 ������ʱ��,t2.NORMAL_HOURS ������8H��ʱ��,
t2.OVERTIME_HOURS ������8H��ʱ��,t3.HOURS2 ��ĩʱ��,t4.HOURS3 �ڼ���ʱ��,t1.SUM_HOURS ���¹�ʱ��,case when t8.sum_hours24 IS null then isnull(t1.SUM_HOURS,0) + isnull(t6.last_hours24,0) else 0 end Ӧ���㹤ʱ��,t1.MEAL �Ͳ�,t5.nights ҹ������,t5.salary4 ҹ�ಹ��,
case when (t1.REGISTER_DATE >= @begin and t1.REGISTER_DATE <= @end and t8.sum_hours24 IS null) OR (t6.last_hours24 is not null and t8.sum_hours24 IS null) then t1.travel else 0 end ����, 
case when t8.sum_hours24 IS null then t1.salary+t1.MEAL + isnull(t5.salary4,0)+ ISNULL(t6.last_hours24_salary,0) + ISNULL(t6.meal,0)+ ISNULL(t6.night,0) + case when (t1.REGISTER_DATE >= @begin and t1.REGISTER_DATE <= @end and t8.sum_hours24 IS null) OR (t6.last_hours24 is not null and t8.sum_hours24 IS null) then t1.travel else 0 end  else 0 end Ӧ������,
t6.last_days ����δ��������,t6.last_hours24 ����δ���㹤ʱ,t6.meal ����δ����Ͳ�,t6.night ����δ����ҹ�ಹ��,t7.hours24 ����δ���㹤ʱ
from #temp1 t1 left join #temp2 t2 on t1.cno = t2.cno 
left join #temp3 t3 on t1.cno = t3.cno 
left join #temp4 t4 on t1.cno = t4.cno 
left join #temp5 t5 on t1.cno= t5.cno 
left join #temp6 t6 on t1.cno = t6.CNO 
left join #temp7 t7 on t1.cno= t7.cno 
left join #temp8 t8 on t1.cno= t8.cno 
