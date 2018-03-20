alter procedure COST_TEMP_EMPLOYEE_STATUS
@month varchar(7),@group nvarchar(20)
as
declare @end varchar(10)
set @end =  convert(varchar,dateadd(day,-day(@month+'-01'),dateadd(month,1,@month+'-01')),23) 
--�·ݺ����в���
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
--�·ݺ����й�Ӧ��
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
if @group = '����'
begin
select �·�,����,��ְ����,��ְ����,ת������,��ĩ��ְ���� from 
(
	select '1' orderid,a.cmonth �·�,a.DEPT2 ����,b.��ְ����,c.��ְ���� ,d.ת������ ,e.��ĩ��ְ���� from #dept2 a left join 
	(
		select convert(varchar(7),REGISTER_DATE,120) cmonth,dept2,COUNT(*) ��ְ���� from COST_TEMP_EMPLOYEE where REGISTER_DATE like @month + '%'  group by convert(varchar(7),REGISTER_DATE,120),dept2
	) b on a.cmonth = b.cmonth and a.DEPT2 = b.DEPT2
	left join 
	(
		select convert(varchar(7),LEAVE_DATE,120) cmonth,dept2,COUNT(*) ��ְ���� from COST_TEMP_EMPLOYEE where LEAVE_DATE like @month + '%' and STATUS = '��ְ' group by convert(varchar(7),LEAVE_DATE,120),dept2
	) c on a.cmonth = c.cmonth and a.DEPT2 = c.DEPT2
	left join 
	(
		select convert(varchar(7),LEAVE_DATE,120) cmonth,dept2,COUNT(*) ת������ from COST_TEMP_EMPLOYEE where LEAVE_DATE like @month + '%' and STATUS = 'ת��' group by convert(varchar(7),LEAVE_DATE,120),dept2
	) d on a.cmonth = d.cmonth and a.DEPT2 = d.DEPT2
	left join 
	(
		select @month cmonth,dept2,COUNT(*) ��ĩ��ְ���� from COST_TEMP_EMPLOYEE where REGISTER_DATE <= @end and (LEAVE_DATE is null or LEAVE_DATE > @end) group by dept2
	) e on a.cmonth = e.cmonth and a.DEPT2 = e.DEPT2

union 
	select '2' orderid,a.cmonth �·�,'����' ����,b.��ְ����,c.��ְ���� ,d.ת������ ,e.��ĩ��ְ���� from (select distinct cmonth from #dept2) a left join 
	(
		select convert(varchar(7),REGISTER_DATE,120) cmonth,COUNT(*) ��ְ���� from COST_TEMP_EMPLOYEE where REGISTER_DATE like @month + '%'  group by convert(varchar(7),REGISTER_DATE,120)
	) b on a.cmonth = b.cmonth 
	left join 
	(
		select convert(varchar(7),LEAVE_DATE,120) cmonth,COUNT(*) ��ְ���� from COST_TEMP_EMPLOYEE where LEAVE_DATE like @month + '%' and STATUS = '��ְ' group by convert(varchar(7),LEAVE_DATE,120)
	) c on a.cmonth = c.cmonth 
	left join 
	(
		select convert(varchar(7),LEAVE_DATE,120) cmonth,COUNT(*) ת������ from COST_TEMP_EMPLOYEE where LEAVE_DATE like @month + '%' and STATUS = 'ת��' group by convert(varchar(7),LEAVE_DATE,120)
	) d on a.cmonth = d.cmonth 
	left join 
	(
		select @month cmonth,COUNT(*) ��ĩ��ְ���� from COST_TEMP_EMPLOYEE where REGISTER_DATE <= @end and (LEAVE_DATE is null or LEAVE_DATE > @end) 
	) e on a.cmonth = e.cmonth 
) t where ��ְ���� is not null or ��ְ���� is not null or ת������ is not null or ��ĩ��ְ���� is not null order by t.�·�,t.orderid
end
if @group = '��Ӧ��'
begin
select �·�,��Ӧ��,��ְ����,��ְ����,ת������,��ĩ��ְ���� from 
(
	select '1' orderid,a.cmonth �·�,a.cfrom ��Ӧ��,b.��ְ����,c.��ְ���� ,d.ת������ ,e.��ĩ��ְ���� from #cfrom a left join 
	(
		select convert(varchar(7),REGISTER_DATE,120) cmonth,cfrom,COUNT(*) ��ְ���� from COST_TEMP_EMPLOYEE where REGISTER_DATE like @month + '%'  group by convert(varchar(7),REGISTER_DATE,120),cfrom
	) b on a.cmonth = b.cmonth and a.cfrom = b.cfrom
	left join 
	(
		select convert(varchar(7),LEAVE_DATE,120) cmonth,cfrom,COUNT(*) ��ְ���� from COST_TEMP_EMPLOYEE where LEAVE_DATE like @month + '%' and STATUS = '��ְ' group by convert(varchar(7),LEAVE_DATE,120),cfrom
	) c on a.cmonth = c.cmonth and a.cfrom = c.cfrom
	left join 
	(
		select convert(varchar(7),LEAVE_DATE,120) cmonth,cfrom,COUNT(*) ת������ from COST_TEMP_EMPLOYEE where LEAVE_DATE like @month + '%' and STATUS = 'ת��' group by convert(varchar(7),LEAVE_DATE,120),cfrom
	) d on a.cmonth = d.cmonth and a.cfrom = d.cfrom
	left join 
	(
		select @month cmonth,cfrom,COUNT(*) ��ĩ��ְ���� from COST_TEMP_EMPLOYEE where REGISTER_DATE <= @end and (LEAVE_DATE is null or LEAVE_DATE > @end) group by cfrom
	) e on a.cmonth = e.cmonth and a.cfrom = e.cfrom

union 
	select '2' orderid,a.cmonth �·�,'����' ��Ӧ��,b.��ְ����,c.��ְ���� ,d.ת������ ,e.��ĩ��ְ���� from (select distinct cmonth from #cfrom) a left join 
	(
		select convert(varchar(7),REGISTER_DATE,120) cmonth,COUNT(*) ��ְ���� from COST_TEMP_EMPLOYEE where REGISTER_DATE like @month + '%'  group by convert(varchar(7),REGISTER_DATE,120)
	) b on a.cmonth = b.cmonth 
	left join 
	(
		select convert(varchar(7),LEAVE_DATE,120) cmonth,COUNT(*) ��ְ���� from COST_TEMP_EMPLOYEE where LEAVE_DATE like @month + '%' and STATUS = '��ְ' group by convert(varchar(7),LEAVE_DATE,120)
	) c on a.cmonth = c.cmonth 
	left join 
	(
		select convert(varchar(7),LEAVE_DATE,120) cmonth,COUNT(*) ת������ from COST_TEMP_EMPLOYEE where LEAVE_DATE like @month + '%' and STATUS = 'ת��' group by convert(varchar(7),LEAVE_DATE,120)
	) d on a.cmonth = d.cmonth 
	left join 
	(
		select @month cmonth,COUNT(*) ��ĩ��ְ���� from COST_TEMP_EMPLOYEE where REGISTER_DATE <= @end and (LEAVE_DATE is null or LEAVE_DATE > @end) 
	) e on a.cmonth = e.cmonth 
) t where ��ְ���� is not null or ��ְ���� is not null or ת������ is not null or ��ĩ��ְ���� is not null order by t.�·�,t.orderid
end

