--drop table #direct
-- drop table #indirect
-- drop table #direct_sum
--  drop table #indirect_sum
-- drop table #temp
-- drop table #temp_days
alter procedure costing
@cmonth varchar(20),
@saletypeid int
as
set nocount on
declare @days int,@quarter int,@cdate varchar(20)
--set @cmonth = '2017-05'
--set @saletypeid = 10
set @cdate = @cmonth + '-01'
set @days = 32-DAY(cast(@cdate as DATEtime)+32-DAY(cast(@cdate as DATEtime)))
if cast(RIGHT(@cmonth,2) as int) < 4 
set @quarter = 1
else if cast(RIGHT(@cmonth,2) as int) >= 4 and cast(RIGHT(@cmonth,2) as int) <7 
set @quarter = 2
else if cast(RIGHT(@cmonth,2) as int) >= 7 and cast(RIGHT(@cmonth,2) as int) <10 
set @quarter = 3
else
set @quarter = 4
--直接人工成本
select a.CDATE ,max(s.cid) saletypeid,max(s.cname) saletypename,max(d.cid) deptid,max(d.cname) deptname,MAX(a.person_name) personname,MAX(w.CNAME) worktypename,SUM(a.HOURS) chours,
cost = case when  w.cid = 1 and SUM(a.HOURS) <= 8 then SUM(a.HOURS)*max(p.PRICE) 
when w.cid = 1 and SUM(a.HOURS) > 8 then (SUM(a.HOURS)-8)*max(p2.PRICE) +8*max(p.PRICE) 
else  SUM(a.HOURS)*max(p.PRICE) end into #direct
from COST_DIRECT_LABOUR_ATTENDANCE a 
--left join COST_DIRECT_LABOUR l on a.DIRECT_LABOUR_ID = l.CID 
left join COST_WORK_TYPE w on a.WORK_TYPE_ID = w.CID 
--left join COST_LINETYPE lt on l.LINETYPE_ID = lt.CID 
left join cost_dept d on a.dept_id = d.cid 
left join COST_SALETYPE s on a.sale_type_id = s.cid 
left join (select * from COST_DIRECT_LABOUR_PRICE where YYYYMM = @cmonth) p on w.CID = p.WORK_TYPE 
cross join (select PRICE from COST_DIRECT_LABOUR_PRICE where YYYYMM = @cmonth and WORK_TYPE = 3) p2 
where s.CID = @saletypeid and a.cdate like '%' + @cmonth + '%' and p.YYYYMM = @cmonth and a.person_type_id = 3 group by a.CDATE,a.person_no,w.cid
--select * from #direct
select cdate,saletypeid,max(saletypename) saletypename,sum(chours) chours,sum(cost) cost into #direct_sum from #direct group by cdate,saletypeid
select cdate,saletypeid,deptid,max(deptname) deptname,max(saletypename) saletypename,sum(chours) chours,sum(cost) cost into #direct_dept_sum from #direct group by cdate,saletypeid,deptid

--间接人工成本
select a.CDATE ,max(s.cid) saletypeid,max(s.cname) saletypename,max(d.cid) deptid,max(d.cname) deptname,MAX(l.CNAME) personname,MAX(w.CNAME) worktypename,SUM(a.HOURS) chours,
cost = case when  w.cid = 1 and SUM(a.HOURS) <= 8 then SUM(a.HOURS)*max(p.PRICE) 
when w.cid = 1 and SUM(a.HOURS) > 8 then (SUM(a.HOURS)-8)*max(p2.PRICE) +8*max(p.PRICE) 
else  SUM(a.HOURS)*max(p.PRICE) end into #indirect
from COST_DIRECT_LABOUR_ATTENDANCE a 
left join COST_DIRECT_LABOUR l on a.DIRECT_LABOUR_ID = l.CID 
left join COST_WORK_TYPE w on a.WORK_TYPE_ID = w.CID 
left join cost_dept d on l.dept_id = d.cid 
left join COST_SALETYPE s on d.saletype_id = s.cid 
--left join (select * from COST_inDIRECT_LABOUR_PRICE where YYYYMM = @cmonth) p on w.CID = p.WORK_TYPE_ID and a.DIRECT_LABOUR_ID = p.INDIRECT_LABOUR_ID  
--left join (select * from COST_inDIRECT_LABOUR_PRICE where YYYYMM = @cmonth and WORK_TYPE_id = 3) p2 on a.DIRECT_LABOUR_ID = p2.INDIRECT_LABOUR_ID 
left join (select * from COST_inDIRECT_LABOUR_level_PRICE where YYYYMM = @cmonth) p on w.CID = p.WORK_TYPE_ID and l.PERSON_LEVEL>=p.LEVEL_BEGIN and l.PERSON_LEVEL <=p.level_end  
left join (select * from COST_inDIRECT_LABOUR_level_PRICE where YYYYMM = @cmonth and WORK_TYPE_id = 3) p2 on l.PERSON_LEVEL>=p.LEVEL_BEGIN and l.PERSON_LEVEL <=p.level_end 
where s.CID = @saletypeid and a.cdate like '%' + @cmonth + '%' and p.YYYYMM = @cmonth and l.person_type_id = 4 group by a.CDATE,w.cid,a.DIRECT_LABOUR_ID
 --select * from #indirect
 select cdate,saletypeid,max(saletypename) saletypename,sum(chours) chours,sum(cost) cost into #indirect_sum from #indirect group by cdate,saletypeid
 select cdate,saletypeid,deptid,max(saletypename) saletypename,sum(chours) chours,sum(cost) cost into #indirect_dept_sum from #indirect group by cdate,saletypeid,deptid
select * into #temp from 
(
    select '1' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '2' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '3' as 'cdate',@saletypeid as 'saletypeid' 
    union all
    select '4' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '5' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '6' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '7' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '8' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '9' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '10' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '11' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '12' as 'cdate',@saletypeid as 'saletypeid' 
    union all
    select '13' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '14' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '15' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '16' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '17' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '18' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '19' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '20' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '21' as 'cdate',@saletypeid as 'saletypeid' 
    union all
    select '22' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '23' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '24' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '25' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '26' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '27' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '28' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '29' as 'cdate',@saletypeid as 'saletypeid'
    union all
    select '30' as 'cdate',@saletypeid as 'saletypeid' 
    union all
    select '31' as 'cdate',@saletypeid as 'saletypeid'
) as A

select top (@days) @cmonth + '-' + cdate as cdate,saletypeid into #temp_days from #temp

 --单日成本
  --日期、营业类型、直接人力小时数、直接人力成本、间接人力小时数、间接人力成本、折旧费、租赁费、水电费、生产点数、预估成本、预估单点成本、标准单点成本、标准成本、盈亏
  if @saletypeid = 2
  begin
	  insert into COST_DAY_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,CDATE,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,DEPRECIATION,RENT_EXPENSE,WATER_ELECTRICITY,POINTCOUNT,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname 营业类型,t.cdate 日期,d.chours 直接人力小时数,d.cost 直接人力成本,i.chours 间接人力小时数,i.cost 间接人力成本,dp.DEPRECIATION 折旧费,re.RENT_EXPENSE 租赁费,
	  we.WATER_ELECTRICITY 水电费,s.POINTCOUNT 生产点数,
	  cast((isnull(d.cost,0) + isnull(i.cost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0) + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) as 预估成本,
	  预估单点成本= case when s.POINTCOUNT is null or s.POINTCOUNT = 0 then 0 else cast(((isnull(d.cost,0) + isnull(i.cost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0) + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4)))/s.POINTCOUNT as decimal(18,5)) end,
	  p.STANDARD_POINT as 标准单点成本,  cast(p.STANDARD_POINT*s.POINTCOUNT  as decimal(18,5)) 标准成本,
	  cast(p.STANDARD_POINT*s.POINTCOUNT as decimal(18,5)) - cast((isnull(d.cost,0) + isnull(i.cost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0) + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) 盈亏 
	  from #temp_days t left join cost_pointcount_sum s on t.cdate = s.CDATE and t.saletypeid = s.SALETYPE_ID 
	  left join  #direct_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid 
	  left join #indirect_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  cross join (select cast(DEPRECIATION/cast(@days as decimal(18,2)) as decimal(18,5)) DEPRECIATION from cost_depreciation where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) dp
	  cross join (select cast(RENT_EXPENSE/cast(@days as decimal(18,2)) as decimal(18,5)) RENT_EXPENSE from COST_RENT_EXPENSE where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) re
	  cross join (select cast(WATER_ELECTRICITY/cast(@days as decimal(18,2)) as decimal(18,5)) WATER_ELECTRICITY from COST_WATER_ELECTRICITY where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) we
	  cross join (select STANDARD_POINT from COST_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p
	  cross join (select COST_RATE from cost_rate where yyyy = left(@cmonth,4) and SALE_TYPE_ID = @saletypeid and QUARTER_ID = @quarter) r 
	  cross join (select STEEL_NET_RATE from COST_STEEL_NET_RATE where YYYYMM = @cmonth) n 
  end
  if @saletypeid = 10
  begin
	  insert into COST_DAY_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,CDATE,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,DEPRECIATION,RENT_EXPENSE,WATER_ELECTRICITY,POINTCOUNT,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname 营业类型,t.cdate 日期,d.chours 直接人力小时数,d.cost 直接人力成本,i.chours 间接人力小时数,i.cost 间接人力成本,dp.DEPRECIATION 折旧费,re.RENT_EXPENSE 租赁费,
	  we.WATER_ELECTRICITY 水电费,s.POINTCOUNT 生产点数,
	  cast((isnull(d.cost,0) + isnull(i.cost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0) + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4))*(1-n.STEEL_NET_RATE) as decimal(18,5)) as 预估成本,
	  预估单点成本= case when s.POINTCOUNT is null or s.POINTCOUNT = 0 then 0 else cast(((isnull(d.cost,0) + isnull(i.cost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0) + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4))*(1-n.STEEL_NET_RATE) - isnull(re.RENT_EXPENSE,0))/s.POINTCOUNT as decimal(18,5)) end,
	  p.STANDARD_POINT as 标准单点成本,  cast(p.STANDARD_POINT*s.POINTCOUNT + isnull(re.RENT_EXPENSE,0) as decimal(18,5)) 标准成本,
	  cast(p.STANDARD_POINT*s.POINTCOUNT + isnull(re.RENT_EXPENSE,0) as decimal(18,5)) - cast((isnull(d.cost,0) + isnull(i.cost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0) + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4))*(1-n.STEEL_NET_RATE) as decimal(18,5)) 盈亏 
	  from #temp_days t left join cost_pointcount_sum s on t.cdate = s.CDATE and t.saletypeid = s.SALETYPE_ID 
	  left join  #direct_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid 
	  left join #indirect_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  cross join (select cast(DEPRECIATION/cast(@days as decimal(18,2)) as decimal(18,5)) DEPRECIATION from cost_depreciation where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) dp
	  cross join (select cast(RENT_EXPENSE/cast(@days as decimal(18,2)) as decimal(18,5)) RENT_EXPENSE from COST_RENT_EXPENSE where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) re
	  cross join (select cast(WATER_ELECTRICITY/cast(@days as decimal(18,2)) as decimal(18,5)) WATER_ELECTRICITY from COST_WATER_ELECTRICITY where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) we
	  cross join (select STANDARD_POINT from COST_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p
	  cross join (select COST_RATE from cost_rate where yyyy = left(@cmonth,4) and SALE_TYPE_ID = @saletypeid and QUARTER_ID = @quarter) r 
	  cross join (select STEEL_NET_RATE from COST_STEEL_NET_RATE where YYYYMM = @cmonth) n 
  end
  if @saletypeid = 13
  begin
	  insert into COST_DAY_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,CDATE,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,EMS_HH_HOURS,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname 营业类型,t.cdate 日期,d.chours 直接人力小时数,d.cost 直接人力成本,i.chours 间接人力小时数,i.cost 间接人力成本,h.HOURS 产出工时,
	  cast((isnull(d.cost,0) + isnull(i.cost,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) as 预估成本,
	  预估单小时成本= case when h.HOURS is null or h.HOURS = 0 then 0 else cast(((isnull(d.cost,0) + isnull(i.cost,0))/CAST(r.cost_rate as decimal(18,4)))/h.HOURS as decimal(18,5)) end,
	  p.STANDARD_POINT as 标准单小时成本,  cast(p.STANDARD_POINT*h.HOURS as decimal(18,5)) 标准成本,
	  cast(p.STANDARD_POINT*h.HOURS  as decimal(18,5)) - cast((isnull(d.cost,0) + isnull(i.cost,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) 盈亏 
	  from #temp_days t left join COST_EMS_HH_HOURS h on t.cdate = h.CDATE 
	  left join  #direct_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid 
	  left join #indirect_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  cross join (select STANDARD_POINT from COST_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p
	  cross join (select COST_RATE from cost_rate where yyyy = left(@cmonth,4) and SALE_TYPE_ID = @saletypeid and QUARTER_ID = @quarter) r 
  end
--if @@error <> 0 
--goto ErrMsg

--月度成本
 --营业类型、月份、直接人力小时数、直接人力成本、间接人力小时数、间接人力成本、折旧费、租赁费、水电费、生产点数、预估成本、预估单点成本、标准单点成本、标准成本、盈亏
  if @saletypeid = 2
  begin
	  insert into COST_MONTH_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,CMONTH,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,DEPRECIATION,RENT_EXPENSE,WATER_ELECTRICITY,POINTCOUNT,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname 营业类型,@cmonth 月份,t.chours 直接人力小时数,t.cost 直接人力成本,t.inhours 间接人力小时数,t.incost 间接人力成本,dp.DEPRECIATION 折旧费,re.RENT_EXPENSE 租赁费,
	  we.WATER_ELECTRICITY 水电费,t.POINTCOUNT 生产点数,
	  cast((isnull(t.cost,0) + isnull(t.incost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0) + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) as 预估成本,
	  预估单点成本= case when t.POINTCOUNT is null or t.POINTCOUNT = 0 then 0 else cast(((isnull(t.cost,0) + isnull(t.incost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0) + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4)))/t.POINTCOUNT as decimal(18,5)) end,
	  p.STANDARD_POINT as 标准单点成本,
	  cast(p.STANDARD_POINT*t.POINTCOUNT as decimal(18,5)) 标准成本,
	  cast(p.STANDARD_POINT*t.POINTCOUNT + isnull(re.RENT_EXPENSE,0) as decimal(18,5)) - cast((isnull(t.cost,0) + isnull(t.incost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0) + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) 盈亏 
	  from 
	  (select t.saletypeid,sum(d.chours) chours,SUM(d.cost) cost,SUM(i.chours) inhours,SUM(i.cost) incost,SUM(s.POINTCOUNT) pointcount from #temp_days t left join cost_pointcount_sum s on t.cdate = s.CDATE and t.saletypeid = s.SALETYPE_ID 
	  left join  #direct_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid 
	  left join #indirect_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid group by t.saletypeid
	  ) t 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  cross join (select cast(DEPRECIATION as decimal(18,5)) DEPRECIATION from cost_depreciation where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) dp
	  cross join (select cast(RENT_EXPENSE as decimal(18,5)) RENT_EXPENSE from COST_RENT_EXPENSE where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) re
	  cross join (select cast(WATER_ELECTRICITY as decimal(18,5)) WATER_ELECTRICITY from COST_WATER_ELECTRICITY where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) we
	  cross join (select STANDARD_POINT from COST_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p
	  cross join (select COST_RATE from cost_rate where yyyy = left(@cmonth,4) and SALE_TYPE_ID = @saletypeid and QUARTER_ID = @quarter) r 
	  cross join (select STEEL_NET_RATE from COST_STEEL_NET_RATE where YYYYMM = @cmonth) n 
  end
  if @saletypeid = 10
  begin
	  insert into COST_MONTH_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,CMONTH,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,DEPRECIATION,RENT_EXPENSE,WATER_ELECTRICITY,POINTCOUNT,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname 营业类型,@cmonth 月份,t.chours 直接人力小时数,t.cost 直接人力成本,t.inhours 间接人力小时数,t.incost 间接人力成本,dp.DEPRECIATION 折旧费,re.RENT_EXPENSE 租赁费,
	  we.WATER_ELECTRICITY 水电费,t.POINTCOUNT 生产点数,
	  cast((isnull(t.cost,0) + isnull(t.incost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0) + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4))*(1-n.STEEL_NET_RATE) as decimal(18,5)) as 预估成本,
	  预估单点成本= case when t.POINTCOUNT is null or t.POINTCOUNT = 0 then 0 else cast(((isnull(t.cost,0) + isnull(t.incost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0) + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4))*(1-n.STEEL_NET_RATE) - isnull(re.RENT_EXPENSE,0))/t.POINTCOUNT as decimal(18,5)) end,
	  p.STANDARD_POINT as 标准单点成本,
	  cast(p.STANDARD_POINT*t.POINTCOUNT + isnull(re.RENT_EXPENSE,0) as decimal(18,5)) 标准成本,
	  cast(p.STANDARD_POINT*t.POINTCOUNT + isnull(re.RENT_EXPENSE,0) as decimal(18,5)) - cast((isnull(t.cost,0) + isnull(t.incost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0) + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4))*(1-n.STEEL_NET_RATE) as decimal(18,5)) 盈亏 
	  from 
	  (select t.saletypeid,sum(d.chours) chours,SUM(d.cost) cost,SUM(i.chours) inhours,SUM(i.cost) incost,SUM(s.POINTCOUNT) pointcount from #temp_days t left join cost_pointcount_sum s on t.cdate = s.CDATE and t.saletypeid = s.SALETYPE_ID 
	  left join  #direct_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid 
	  left join #indirect_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid group by t.saletypeid
	  ) t 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  cross join (select cast(DEPRECIATION as decimal(18,5)) DEPRECIATION from cost_depreciation where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) dp
	  cross join (select cast(RENT_EXPENSE as decimal(18,5)) RENT_EXPENSE from COST_RENT_EXPENSE where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) re
	  cross join (select cast(WATER_ELECTRICITY as decimal(18,5)) WATER_ELECTRICITY from COST_WATER_ELECTRICITY where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) we
	  cross join (select STANDARD_POINT from COST_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p
	  cross join (select COST_RATE from cost_rate where yyyy = left(@cmonth,4) and SALE_TYPE_ID = @saletypeid and QUARTER_ID = @quarter) r 
	  cross join (select STEEL_NET_RATE from COST_STEEL_NET_RATE where YYYYMM = @cmonth) n 
  end
  if @saletypeid = 13
  begin
	  insert into COST_MONTH_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,CMONTH,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,EMS_HH_HOURS,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname 营业类型,@cmonth 月份,t.chours 直接人力小时数,t.cost 直接人力成本,t.inhours 间接人力小时数,t.incost 间接人力成本,t.EMS_HH_HOURS 产出工时,
	  cast((isnull(t.cost,0) + isnull(t.incost,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) as 预估成本,
	  预估单点成本= case when t.EMS_HH_HOURS is null or t.EMS_HH_HOURS = 0 then 0 else cast(((isnull(t.cost,0) + isnull(t.incost,0))/CAST(r.cost_rate as decimal(18,4)))/t.EMS_HH_HOURS as decimal(18,5)) end,
	  p.STANDARD_POINT as 标准单点成本,
	  cast(p.STANDARD_POINT*t.EMS_HH_HOURS as decimal(18,5)) 标准成本,
	  cast(p.STANDARD_POINT*t.EMS_HH_HOURS as decimal(18,5)) - cast((isnull(t.cost,0) + isnull(t.incost,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) 盈亏 
	  from 
	  (select t.saletypeid,sum(d.chours) chours,SUM(d.cost) cost,SUM(i.chours) inhours,SUM(i.cost) incost,SUM(h.HOURS) EMS_HH_HOURS from #temp_days t left join COST_EMS_HH_HOURS h on t.cdate = h.CDATE  
	  left join  #direct_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid 
	  left join #indirect_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid group by t.saletypeid
	  ) t 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  cross join (select STANDARD_POINT from COST_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p
	  cross join (select COST_RATE from cost_rate where yyyy = left(@cmonth,4) and SALE_TYPE_ID = @saletypeid and QUARTER_ID = @quarter) r 
  end
  --if @@error <> 0 
  --goto ErrMsg
  
  --各部门成本，单日
  --部门、营业类型、日期、直接人力小时数、直接人力成本、间接人力小时数、间接人力成本、生产点数、预估成本、预估单点成本、标准单点成本、标准成本、盈亏
  if @saletypeid = 2 or @saletypeid = 10
  begin
	  insert into COST_DEPT_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,DEPT_ID,DEPT_NAME,CDATE,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,POINTCOUNT,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname 营业类型,t.cid,t.cname,t.cdate 日期,d.chours 直接人力小时数,d.cost 直接人力成本,i.chours 间接人力小时数,i.cost 间接人力成本,
	  s.POINTCOUNT 生产点数,
	  cast((isnull(d.cost,0) + isnull(i.cost,0)) as decimal(18,5)) as 预估成本,
	  预估单点成本= case when s.POINTCOUNT is null or s.POINTCOUNT = 0 then 0 else cast((isnull(d.cost,0) + isnull(i.cost,0))/cast(s.POINTCOUNT as decimal(18,5)) as decimal(18,5)) end, 
	  p.DEPT_STANDARD_POINT as 标准单点成本, cast(p.DEPT_STANDARD_POINT*s.POINTCOUNT as decimal(18,5)) 标准成本,
	  cast(p.DEPT_STANDARD_POINT*s.POINTCOUNT as decimal(18,5)) - cast((isnull(d.cost,0) + isnull(i.cost,0)) as decimal(18,5)) 盈亏 
	  from (select * from #temp_days t left join cost_dept dp on t.saletypeid = dp.SALETYPE_ID) t 
	  left join cost_pointcount_sum s on t.cdate = s.CDATE and t.saletypeid = s.SALETYPE_ID 
	  left join  #direct_dept_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid and t.cid = d.deptid 
	  left join #indirect_dept_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid  and t.cid = i.deptid 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select dept_id,DEPT_STANDARD_POINT from COST_DEPT_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on t.cid = p.DEPT_ID
  end
  if @saletypeid = 13
  begin
	  insert into COST_DEPT_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,DEPT_ID,DEPT_NAME,CDATE,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,EMS_HH_HOURS,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname 营业类型,t.cid,t.cname,t.cdate 日期,d.chours 直接人力小时数,d.cost 直接人力成本,i.chours 间接人力小时数,i.cost 间接人力成本,
	  h.HOURS 产出工时,
	  cast((isnull(d.cost,0) + isnull(i.cost,0)) as decimal(18,5)) as 预估成本,
	  预估单小时成本= case when h.HOURS is null or h.HOURS = 0 then 0 else cast((isnull(d.cost,0) + isnull(i.cost,0))/cast(h.HOURS as decimal(18,5)) as decimal(18,5)) end, 
	  p.DEPT_STANDARD_POINT as 标准单点成本, cast(p.DEPT_STANDARD_POINT*h.HOURS as decimal(18,5)) 标准成本,
	  cast(p.DEPT_STANDARD_POINT*h.HOURS as decimal(18,5)) - cast((isnull(d.cost,0) + isnull(i.cost,0)) as decimal(18,5)) 盈亏 
	  from (select * from #temp_days t left join cost_dept dp on t.saletypeid = dp.SALETYPE_ID) t 
	  left join COST_EMS_HH_HOURS h on t.cdate = h.CDATE 
	  left join  #direct_dept_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid and t.cid = d.deptid 
	  left join #indirect_dept_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid  and t.cid = i.deptid 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select dept_id,DEPT_STANDARD_POINT from COST_DEPT_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on t.cid = p.DEPT_ID
  end
  --if @@error <> 0 
  --goto ErrMsg
  
  --各部门成本，月度
  --部门、营业类型、月度、直接人力小时数、直接人力成本、间接人力小时数、间接人力成本、生产点数、预估成本、预估单点成本、标准单点成本、标准成本、盈亏
  if @saletypeid = 2 or @saletypeid = 10
  begin
	  insert into COST_DEPT_MONTH_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,DEPT_ID,DEPT_NAME,CMONTH,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,POINTCOUNT,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select saletypeid,saletypename 营业类型,deptid,deptname,cmonth 月份,dhours 直接人力小时数,dcost 直接人力成本,inhours 间接人力小时数,incost 间接人力成本,
	  pointcount 生产点数,
	  cast((isnull(dcost,0) + isnull(incost,0)) as decimal(18,5)) as 预估成本,
	  预估单点成本= case when POINTCOUNT is null or POINTCOUNT = 0 then 0 else cast((isnull(dcost,0) + isnull(incost,0))/cast(POINTCOUNT as decimal(18,5)) as decimal(18,5)) end, 
	  DEPT_STANDARD_POINT as 标准单点成本, cast(DEPT_STANDARD_POINT*POINTCOUNT as decimal(18,5)) 标准成本,
	  cast(DEPT_STANDARD_POINT*POINTCOUNT as decimal(18,5)) - cast((isnull(dcost,0) + isnull(incost,0)) as decimal(18,5)) 盈亏 
	  from 
	  (select max(st.cid) saletypeid,max(st.cname) saletypename,t.cid deptid,max(t.cname) deptname,@cmonth cmonth,sum(d.chours) dhours,sum(d.cost) dcost,sum(i.chours) inhours,sum(i.cost) incost,
	  sum(s.POINTCOUNT) pointcount,max(p.DEPT_STANDARD_POINT) as dept_standard_point from (select * from #temp_days t left join cost_dept dp on t.saletypeid = dp.SALETYPE_ID) t 
	  left join cost_pointcount_sum s on t.cdate = s.CDATE and t.saletypeid = s.SALETYPE_ID 
	  left join  #direct_dept_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid and t.cid = d.deptid 
	  left join #indirect_dept_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid  and t.cid = i.deptid 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select dept_id,DEPT_STANDARD_POINT from COST_DEPT_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on t.cid = p.DEPT_ID group by t.cid) t
  end
  if @saletypeid = 13
  begin
	  insert into COST_DEPT_MONTH_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,DEPT_ID,DEPT_NAME,CMONTH,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,EMS_HH_HOURS,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select saletypeid,saletypename 营业类型,deptid,deptname,cmonth 月份,dhours 直接人力小时数,dcost 直接人力成本,inhours 间接人力小时数,incost 间接人力成本,
	  EMS_HH_HOURS 产出工时,
	  cast((isnull(dcost,0) + isnull(incost,0)) as decimal(18,5)) as 预估成本,
	  预估单小时成本= case when EMS_HH_HOURS is null or EMS_HH_HOURS = 0 then 0 else cast((isnull(dcost,0) + isnull(incost,0))/cast(EMS_HH_HOURS as decimal(18,5)) as decimal(18,5)) end, 
	  DEPT_STANDARD_POINT as 标准单点成本, cast(DEPT_STANDARD_POINT*EMS_HH_HOURS as decimal(18,5)) 标准成本,
	  cast(DEPT_STANDARD_POINT*EMS_HH_HOURS as decimal(18,5)) - cast((isnull(dcost,0) + isnull(incost,0)) as decimal(18,5)) 盈亏 
	  from 
	  (select max(st.cid) saletypeid,max(st.cname) saletypename,t.cid deptid,max(t.cname) deptname,@cmonth cmonth,sum(d.chours) dhours,sum(d.cost) dcost,sum(i.chours) inhours,sum(i.cost) incost,
	  sum(h.HOURS) EMS_HH_HOURS,max(p.DEPT_STANDARD_POINT) as dept_standard_point from (select * from #temp_days t left join cost_dept dp on t.saletypeid = dp.SALETYPE_ID) t 
	  left join COST_EMS_HH_HOURS h on t.cdate = h.CDATE 
	  left join  #direct_dept_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid and t.cid = d.deptid 
	  left join #indirect_dept_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid  and t.cid = i.deptid 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select dept_id,DEPT_STANDARD_POINT from COST_DEPT_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on t.cid = p.DEPT_ID group by t.cid) t
  end
--if @@error <> 0 
--goto ErrMsg

--ErrMsg:
--rollback 
--raiserror 20000 '成本计算失败！'
--return

