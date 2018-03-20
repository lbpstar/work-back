--1、直接人工成本
--原始数据
select * from COST_DIRECT_LABOUR_ATTENDANCE a left join COST_DIRECT_LABOUR l on a.DIRECT_LABOUR_ID = l.CID left join COST_WORK_TYPE w on a.WORK_TYPE_ID = w.CID
 left join COST_LINETYPE lt on l.LINETYPE_ID = lt.CID left join cost_dept d on l.dept_id = d.cid left join COST_SALETYPE s on d.saletype_id = s.cid where s.CNAME = 'ems smt' 
--每天数据.分日期、费率要按月份来
select a.CDATE 日期,max(d.cname) 部门,MAX(l.CNAME) 姓名,w.cid,MAX(w.CNAME) 上班类型,SUM(a.HOURS) 小时数,MAX(p.PRICE) 费率,MAX(p2.PRICE) from COST_DIRECT_LABOUR_ATTENDANCE a left join COST_DIRECT_LABOUR l on a.DIRECT_LABOUR_ID = l.CID left join COST_WORK_TYPE w on a.WORK_TYPE_ID = w.CID
 left join COST_LINETYPE lt on l.LINETYPE_ID = lt.CID left join cost_dept d on l.dept_id = d.cid left join COST_SALETYPE s on d.saletype_id = s.cid 
 left join COST_DIRECT_LABOUR_PRICE p on w.CID = p.WORK_TYPE 
 cross join (select PRICE from COST_DIRECT_LABOUR_PRICE where WORK_TYPE = 3) p2 
 where s.CNAME = 'ems smt' group by a.CDATE,w.cid,a.DIRECT_LABOUR_ID
--分日期、部门、人员计算直接人工费用。直接求和，就是该营业部门的直接人工费用，按部门分组求和就是每个部门的直接人工费用

declare @cmonth varchar(20) 
set @cmonth = '2017-05'
select a.CDATE 日期,max(s.cname) 营业类型,max(d.cname) 部门,MAX(l.CNAME) 姓名,MAX(w.CNAME) 上班类型,SUM(a.HOURS) 小时数,
成本 = case when  w.cid = 1 and SUM(a.HOURS) <= 8 then SUM(a.HOURS)*max(p.PRICE) 
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
select 日期,营业类型,sum(小时数) 小时数合计,sum(成本) 成本 from #direct group by 日期,营业类型
drop table #direct
--2、间接人工成本
declare @cmonth2 varchar(20) 
set @cmonth2 = '2017-05'
select a.CDATE 日期 ,max(s.cname) 营业类型,max(d.cname) 部门,MAX(l.CNAME) 姓名,MAX(w.CNAME) 上班类型,SUM(a.HOURS) 小时数,
成本 = case when  w.cid = 1 and SUM(a.HOURS) <= 8 then SUM(a.HOURS)*max(p.PRICE) 
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
 select 日期,营业类型,sum(小时数) 小时数合计,sum(成本) 成本 from #indirect group by 日期,营业类型
 drop table #indirect
 --3、当月天数
 SELECT 32-DAY(getdate()+32-DAY(getdate())) 
 --4、单日成本
 --折旧费
 declare @cmonth3 varchar(20),@quarter int
 set @cmonth3 = '2017-05'
 set @quarter = 2
 select * from cost_depreciation where YYYYMM = @cmonth3 and SALE_TYPE_ID = 10
 --租赁费
  select * from COST_RENT_EXPENSE where YYYYMM = @cmonth3 and SALE_TYPE_ID = 10
  --水电费
  select * from COST_WATER_ELECTRICITY where YYYYMM = @cmonth3 and SALE_TYPE_ID = 10
  --单点标准成本
  select * from COST_STANDARD_POINT where YYYYMM = @cmonth3 and SALE_TYPE_ID = 10
  --成本比率
  select * from cost_rate where SALE_TYPE_ID = 10 and QUARTER_ID = 2
  --钢网占比
  select * from COST_STEEL_NET_RATE where YYYYMM = @cmonth3
  --生产点数
  select * from cost_pointcount_sum where cdate like '2017-05%'
  
  --日期、营业类型、直接人力小时数、直接人力成本、间接人力小时数、间接人力成本、折旧费、租赁费、水电费、生产点数、预估成本、预估单点成本、标准成本、盈亏
  --写一个存储过程
