--1��ֱ���˹��ɱ�
--ԭʼ����
select * from COST_DIRECT_LABOUR_ATTENDANCE a left join COST_DIRECT_LABOUR l on a.DIRECT_LABOUR_ID = l.CID left join COST_WORK_TYPE w on a.WORK_TYPE_ID = w.CID
 left join COST_LINETYPE lt on l.LINETYPE_ID = lt.CID left join cost_dept d on l.dept_id = d.cid left join COST_SALETYPE s on d.saletype_id = s.cid where s.CNAME = 'ems smt' 
--ÿ������.�����ڡ�����Ҫ���·���
select a.CDATE ����,max(d.cname) ����,MAX(l.CNAME) ����,w.cid,MAX(w.CNAME) �ϰ�����,SUM(a.HOURS) Сʱ��,MAX(p.PRICE) ����,MAX(p2.PRICE) from COST_DIRECT_LABOUR_ATTENDANCE a left join COST_DIRECT_LABOUR l on a.DIRECT_LABOUR_ID = l.CID left join COST_WORK_TYPE w on a.WORK_TYPE_ID = w.CID
 left join COST_LINETYPE lt on l.LINETYPE_ID = lt.CID left join cost_dept d on l.dept_id = d.cid left join COST_SALETYPE s on d.saletype_id = s.cid 
 left join COST_DIRECT_LABOUR_PRICE p on w.CID = p.WORK_TYPE 
 cross join (select PRICE from COST_DIRECT_LABOUR_PRICE where WORK_TYPE = 3) p2 
 where s.CNAME = 'ems smt' group by a.CDATE,w.cid,a.DIRECT_LABOUR_ID
--�����ڡ����š���Ա����ֱ���˹����á�ֱ����ͣ����Ǹ�Ӫҵ���ŵ�ֱ���˹����ã������ŷ�����;���ÿ�����ŵ�ֱ���˹�����

declare @cmonth varchar(20) 
set @cmonth = '2017-05'
select a.CDATE ����,max(s.cname) Ӫҵ����,max(d.cname) ����,MAX(l.CNAME) ����,MAX(w.CNAME) �ϰ�����,SUM(a.HOURS) Сʱ��,
�ɱ� = case when  w.cid = 1 and SUM(a.HOURS) <= 8 then SUM(a.HOURS)*max(p.PRICE) 
when w.cid = 1 and SUM(a.HOURS) > 8 then (SUM(a.HOURS)-8)*max(p2.PRICE) +8*max(p.PRICE) 
else  SUM(a.HOURS)*max(p.PRICE) end into #direct
from COST_DIRECT_LABOUR_ATTENDANCE a 
left join COST_DIRECT_LABOUR l on a.DIRECT_LABOUR_ID = l.CID 
left join COST_WORK_TYPE w on a.WORK_TYPE_ID = w.CID 
left join COST_LINETYPE lt on l.LINETYPE_ID = lt.CID 
left join cost_dept d on l.dept_id = d.cid 
left join COST_SALETYPE s on d.saletype_id = s.cid 
left join (select * from COST_DIRECT_LABOUR_PRICE where YYYYMM = @cmonth) p on w.CID = p.WORK_TYPE 
cross join (select PRICE from COST_DIRECT_LABOUR_PRICE where YYYYMM = @cmonth and WORK_TYPE = 3) p2 
where s.CNAME = 'ems smt' and a.cdate like '%' + @cmonth + '%' and p.YYYYMM = @cmonth and l.person_type_id = 3 group by a.CDATE,a.DIRECT_LABOUR_ID,w.cid
select * from #direct
select ����,Ӫҵ����,sum(Сʱ��) Сʱ���ϼ�,sum(�ɱ�) �ɱ� from #direct group by ����,Ӫҵ����
drop table #direct
--2������˹��ɱ�
declare @cmonth2 varchar(20) 
set @cmonth2 = '2017-05'
select a.CDATE ���� ,max(s.cname) Ӫҵ����,max(d.cname) ����,MAX(l.CNAME) ����,MAX(w.CNAME) �ϰ�����,SUM(a.HOURS) Сʱ��,
�ɱ� = case when  w.cid = 1 and SUM(a.HOURS) <= 8 then SUM(a.HOURS)*max(p.PRICE) 
when w.cid = 1 and SUM(a.HOURS) > 8 then (SUM(a.HOURS)-8)*max(p2.PRICE) +8*max(p.PRICE) 
else  SUM(a.HOURS)*max(p.PRICE) end into #indirect
from COST_DIRECT_LABOUR_ATTENDANCE a 
left join COST_DIRECT_LABOUR l on a.DIRECT_LABOUR_ID = l.CID 
left join COST_WORK_TYPE w on a.WORK_TYPE_ID = w.CID 
left join cost_dept d on l.dept_id = d.cid 
left join COST_SALETYPE s on d.saletype_id = s.cid 
left join (select * from COST_inDIRECT_LABOUR_PRICE where YYYYMM = @cmonth2) p on w.CID = p.WORK_TYPE_ID and a.DIRECT_LABOUR_ID = p.INDIRECT_LABOUR_ID  
left join (select * from COST_inDIRECT_LABOUR_PRICE where YYYYMM = @cmonth2 and WORK_TYPE_id = 3) p2 on a.DIRECT_LABOUR_ID = p2.INDIRECT_LABOUR_ID 
where s.CNAME = 'ems smt' and a.cdate like '%' + @cmonth2 + '%' and p.YYYYMM = @cmonth2 and l.person_type_id = 4 group by a.CDATE,w.cid,a.DIRECT_LABOUR_ID
 select * from #indirect
 drop table #indirect
 select ����,Ӫҵ����,sum(Сʱ��) Сʱ���ϼ�,sum(�ɱ�) �ɱ� from #indirect group by ����,Ӫҵ����
 drop table #indirect
 --3����������
 SELECT 32-DAY(getdate()+32-DAY(getdate())) 
 --4�����ճɱ�
 --�۾ɷ�
 declare @cmonth3 varchar(20),@quarter int
 set @cmonth3 = '2017-05'
 set @quarter = 2
 select * from cost_depreciation where YYYYMM = @cmonth3 and SALE_TYPE_ID = 10
 --���޷�
  select * from COST_RENT_EXPENSE where YYYYMM = @cmonth3 and SALE_TYPE_ID = 10
  --ˮ���
  select * from COST_WATER_ELECTRICITY where YYYYMM = @cmonth3 and SALE_TYPE_ID = 10
  --�����׼�ɱ�
  select * from COST_STANDARD_POINT where YYYYMM = @cmonth3 and SALE_TYPE_ID = 10
  --�ɱ�����
  select * from cost_rate where SALE_TYPE_ID = 10 and QUARTER_ID = 2
  --����ռ��
  select * from COST_STEEL_NET_RATE where YYYYMM = @cmonth3
  --��������
  select * from cost_pointcount_sum where cdate like '2017-05%'
  
  --���ڡ�Ӫҵ���͡�ֱ������Сʱ����ֱ�������ɱ����������Сʱ������������ɱ����۾ɷѡ����޷ѡ�ˮ��ѡ�����������Ԥ���ɱ���Ԥ������ɱ�����׼�ɱ���ӯ��
  --дһ���洢����
