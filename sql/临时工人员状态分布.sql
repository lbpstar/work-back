alter procedure COST_TEMP_EMPLOYEE_STATUS
@month varchar(7),@group nvarchar(20)
as
declare @end varchar(10)
set @end =  convert(varchar,dateadd(day,-day(@month+'-01'),dateadd(month,1,@month+'-01')),23) 
--月份和所有部门
select cmonth,dept2 into #dept2 from 
(
	select convert(varchar(7),REGISTER_DATE,120) cmonth from COST_TEMP_EMPLOYEE where REGISTER_DATE is not null and REGISTER_DATE like @month + '%'
	union 
	select convert(varchar(7),LEAVE_DATE,120) cmonth from COST_TEMP_EMPLOYEE where LEAVE_DATE is not null and LEAVE_DATE like @month + '%'
) a
cross join 
(
	select distinct dept2 from COST_TEMP_EMPLOYEE where dept2 is not null
) b
--月份和所有供应商
select cmonth,cfrom into #cfrom from 
(
	select convert(varchar(7),REGISTER_DATE,120) cmonth from COST_TEMP_EMPLOYEE where REGISTER_DATE is not null and REGISTER_DATE like @month + '%'
	union 
	select convert(varchar(7),LEAVE_DATE,120) cmonth from COST_TEMP_EMPLOYEE where LEAVE_DATE is not null and LEAVE_DATE like @month + '%'
) a
cross join 
(
	select distinct cfrom from COST_TEMP_EMPLOYEE where cfrom is not null
) b
if @group = '部门'
begin
select 月份,部门,入职人数,离职人数,转正人数,月末在职人数 from 
(
	select '1' orderid,a.cmonth 月份,a.DEPT2 部门,b.入职人数,c.离职人数 ,d.转正人数 ,e.月末在职人数 from #dept2 a left join 
	(
		select convert(varchar(7),REGISTER_DATE,120) cmonth,dept2,COUNT(*) 入职人数 from COST_TEMP_EMPLOYEE where REGISTER_DATE like @month + '%'  group by convert(varchar(7),REGISTER_DATE,120),dept2
	) b on a.cmonth = b.cmonth and a.DEPT2 = b.DEPT2
	left join 
	(
		select convert(varchar(7),LEAVE_DATE,120) cmonth,dept2,COUNT(*) 离职人数 from COST_TEMP_EMPLOYEE where LEAVE_DATE like @month + '%' and STATUS = '离职' group by convert(varchar(7),LEAVE_DATE,120),dept2
	) c on a.cmonth = c.cmonth and a.DEPT2 = c.DEPT2
	left join 
	(
		select convert(varchar(7),LEAVE_DATE,120) cmonth,dept2,COUNT(*) 转正人数 from COST_TEMP_EMPLOYEE where LEAVE_DATE like @month + '%' and STATUS = '转正' group by convert(varchar(7),LEAVE_DATE,120),dept2
	) d on a.cmonth = d.cmonth and a.DEPT2 = d.DEPT2
	left join 
	(
		select @month cmonth,dept2,COUNT(*) 月末在职人数 from COST_TEMP_EMPLOYEE where REGISTER_DATE <= @end and (LEAVE_DATE is null or LEAVE_DATE > @end) group by dept2
	) e on a.cmonth = e.cmonth and a.DEPT2 = e.DEPT2

union 
	select '2' orderid,a.cmonth 月份,'汇总' 部门,b.入职人数,c.离职人数 ,d.转正人数 ,e.月末在职人数 from (select distinct cmonth from #dept2) a left join 
	(
		select convert(varchar(7),REGISTER_DATE,120) cmonth,COUNT(*) 入职人数 from COST_TEMP_EMPLOYEE where REGISTER_DATE like @month + '%'  group by convert(varchar(7),REGISTER_DATE,120)
	) b on a.cmonth = b.cmonth 
	left join 
	(
		select convert(varchar(7),LEAVE_DATE,120) cmonth,COUNT(*) 离职人数 from COST_TEMP_EMPLOYEE where LEAVE_DATE like @month + '%' and STATUS = '离职' group by convert(varchar(7),LEAVE_DATE,120)
	) c on a.cmonth = c.cmonth 
	left join 
	(
		select convert(varchar(7),LEAVE_DATE,120) cmonth,COUNT(*) 转正人数 from COST_TEMP_EMPLOYEE where LEAVE_DATE like @month + '%' and STATUS = '转正' group by convert(varchar(7),LEAVE_DATE,120)
	) d on a.cmonth = d.cmonth 
	left join 
	(
		select @month cmonth,COUNT(*) 月末在职人数 from COST_TEMP_EMPLOYEE where REGISTER_DATE <= @end and (LEAVE_DATE is null or LEAVE_DATE > @end) 
	) e on a.cmonth = e.cmonth 
) t where 入职人数 is not null or 离职人数 is not null or 转正人数 is not null or 月末在职人数 is not null order by t.月份,t.orderid
end
if @group = '供应商'
begin
select 月份,供应商,入职人数,离职人数,转正人数,月末在职人数 from 
(
	select '1' orderid,a.cmonth 月份,a.cfrom 供应商,b.入职人数,c.离职人数 ,d.转正人数 ,e.月末在职人数 from #cfrom a left join 
	(
		select convert(varchar(7),REGISTER_DATE,120) cmonth,cfrom,COUNT(*) 入职人数 from COST_TEMP_EMPLOYEE where REGISTER_DATE like @month + '%'  group by convert(varchar(7),REGISTER_DATE,120),cfrom
	) b on a.cmonth = b.cmonth and a.cfrom = b.cfrom
	left join 
	(
		select convert(varchar(7),LEAVE_DATE,120) cmonth,cfrom,COUNT(*) 离职人数 from COST_TEMP_EMPLOYEE where LEAVE_DATE like @month + '%' and STATUS = '离职' group by convert(varchar(7),LEAVE_DATE,120),cfrom
	) c on a.cmonth = c.cmonth and a.cfrom = c.cfrom
	left join 
	(
		select convert(varchar(7),LEAVE_DATE,120) cmonth,cfrom,COUNT(*) 转正人数 from COST_TEMP_EMPLOYEE where LEAVE_DATE like @month + '%' and STATUS = '转正' group by convert(varchar(7),LEAVE_DATE,120),cfrom
	) d on a.cmonth = d.cmonth and a.cfrom = d.cfrom
	left join 
	(
		select @month cmonth,cfrom,COUNT(*) 月末在职人数 from COST_TEMP_EMPLOYEE where REGISTER_DATE <= @end and (LEAVE_DATE is null or LEAVE_DATE > @end) group by cfrom
	) e on a.cmonth = e.cmonth and a.cfrom = e.cfrom

union 
	select '2' orderid,a.cmonth 月份,'汇总' 供应商,b.入职人数,c.离职人数 ,d.转正人数 ,e.月末在职人数 from (select distinct cmonth from #cfrom) a left join 
	(
		select convert(varchar(7),REGISTER_DATE,120) cmonth,COUNT(*) 入职人数 from COST_TEMP_EMPLOYEE where REGISTER_DATE like @month + '%'  group by convert(varchar(7),REGISTER_DATE,120)
	) b on a.cmonth = b.cmonth 
	left join 
	(
		select convert(varchar(7),LEAVE_DATE,120) cmonth,COUNT(*) 离职人数 from COST_TEMP_EMPLOYEE where LEAVE_DATE like @month + '%' and STATUS = '离职' group by convert(varchar(7),LEAVE_DATE,120)
	) c on a.cmonth = c.cmonth 
	left join 
	(
		select convert(varchar(7),LEAVE_DATE,120) cmonth,COUNT(*) 转正人数 from COST_TEMP_EMPLOYEE where LEAVE_DATE like @month + '%' and STATUS = '转正' group by convert(varchar(7),LEAVE_DATE,120)
	) d on a.cmonth = d.cmonth 
	left join 
	(
		select @month cmonth,COUNT(*) 月末在职人数 from COST_TEMP_EMPLOYEE where REGISTER_DATE <= @end and (LEAVE_DATE is null or LEAVE_DATE > @end) 
	) e on a.cmonth = e.cmonth 
) t where 入职人数 is not null or 离职人数 is not null or 转正人数 is not null or 月末在职人数 is not null order by t.月份,t.orderid
end

