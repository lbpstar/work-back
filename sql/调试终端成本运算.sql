drop table #direct
 drop table #indirect
 drop table #direct_sum
  drop table #indirect_sum
  drop table #direct_dept_sum
 drop table #temp
 drop table #temp_days
drop table #direct_dept_sum_rate
drop table #temp_direct

set nocount on
declare @days int,@quarter int,@cdate varchar(20),@type int,@cmonth varchar(20),@saletypeid int
set @cmonth = '2017-11'
set @saletypeid = 14
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
if @saletypeid = 14 
set @type = 1
else if @saletypeid = 15
set @type =2
--直接人工成本
select a.cDATE ,max(s.cid) saletypeid,max(s.cname) saletypename,max(d.cid) deptid,max(d.cname) deptname,MAX(a.emp_name) empname,MAX(w.CNAME) worktypename,SUM(a.gz_int + a.jb_ps_int + a.jb_xx_int + a.jb_jr_int) chours,
cost = case when  w.cid = 1 then SUM(a.gz_int)*max(p.PRICE) 
when w.cid = 3 then SUM(a.jb_ps_int)*max(p.PRICE) +SUM(a.gz_int)*max(p2.PRICE) when w.cid = 4 then SUM(a.jb_xx_int)*max(p.PRICE) +SUM(a.gz_int)*max(p2.PRICE) when w.CID = 5 then SUM(a.jb_jr_int)*max(p.PRICE) +SUM(a.gz_int)*max(p2.PRICE) end into #direct
from 
(select t.cdate,t.emp_no,t.emp_name,t.person_type_id,m.dept_id,t.gz_int,t.jb_ps_int,t.jb_xx_int,t.jb_jr_int,work_type_id = case when t.jb_ps_int = 0 and t.jb_xx_int = 0 and t.jb_jr_int = 0 then 1 when t.jb_ps_int >0 then 3 when t.jb_xx_int>0 then 4 when t.jb_jr_int >0 then 5 end from COST_DIRECT_LABOUR_ATTENDANCE t 
 left join cost_dept_map m on  T.DEPT4 = M.DEPT4 where t.CDATE like + @cmonth + '%') 
 a 
left join COST_WORK_TYPE w on a.WORK_TYPE_ID = w.CID 
left join cost_dept d on a.dept_id = d.cid 
left join COST_SALETYPE s on d.saletype_id = s.cid 
left join (select * from COST_DIRECT_LABOUR_PRICE where YYYYMM = @cmonth) p on w.CID = p.WORK_TYPE 
left join (select PRICE from COST_DIRECT_LABOUR_PRICE where YYYYMM = @cmonth and WORK_TYPE = 1) p2 on 1=1 
where s.CID = @saletypeid  and a.person_type_id = 3 group by a.cDATE,a.emp_no,w.cid
--select * from #direct
select cdate,saletypeid,max(saletypename) saletypename,sum(chours) chours,sum(cost) cost into #direct_sum from #direct group by cdate,saletypeid
select cdate,saletypeid,deptid,max(deptname) deptname,max(saletypename) saletypename,sum(chours) chours,sum(cost) cost into #direct_dept_sum from #direct group by cdate,saletypeid,deptid

--考虑终端和系统部门成本比率
select d.cdate,d.saletypeid,sum(chours) chours,sum(cost) cost,sum(cast(cost/CAST(r.cost_rate as decimal(18,4))  as decimal(18,5))) cost2 into #direct_dept_sum_rate from #direct_dept_sum d
left join (select sale_type_id,dept_id,COST_RATE from cost_rate where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and isnull(dept_id,0) >0) r on d.saletypeid = r.sale_type_id and d.deptid = r.dept_id 
  group by d.cdate,d.saletypeid
--临时工成本,每天的临时工成本,也考虑了部门成本比率
select a.CDATE ,a.SALE_TYPE_ID saletypeid,a.dept_id,a.HOURS chours,cast(a.HOURS*p.PRICE  as decimal(18,5)) cost,
cast(a.HOURS*p.PRICE/CAST(r.cost_rate as decimal(18,4))  as decimal(18,5)) cost2 into #temp_direct 
from COST_TEMP_WORKER a 
left join (select * from COST_TEMP_WORKER_PRICE where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on 1=1 
left join (select sale_type_id,dept_id,COST_RATE from cost_rate where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and isnull(dept_id,0) >0) r on a.SALE_TYPE_ID = r.sale_type_id and a.dept_id= r.dept_id 
where a.SALE_TYPE_ID = @saletypeid and a.cdate like '%' + @cmonth + '%'  
--select * from #direct

--间接人工成本
select a.CDATE ,max(s.cid) saletypeid,max(s.cname) saletypename,max(d.cid) deptid,max(d.cname) deptname,MAX(a.emp_name) personname,MAX(w.CNAME) worktypename,SUM(a.gz_int + a.jb_ps_int + a.jb_xx_int + a.jb_jr_int) chours,
cost = case when  w.cid = 1 then SUM(a.gz_int)*max(p.PRICE) 
when w.cid = 3 then SUM(a.jb_ps_int)*max(p.PRICE) +SUM(a.gz_int)*max(p2.PRICE) when w.cid = 4 then SUM(a.jb_xx_int)*max(p.PRICE) +SUM(a.gz_int)*max(p2.PRICE) when w.CID = 5 then SUM(a.jb_jr_int)*max(p.PRICE) +SUM(a.gz_int)*max(p2.PRICE) end into #indirect 
from 
(select t.cdate,t.emp_no,t.emp_name,t.person_type_id,m.dept_id,t.gz_int,t.jb_ps_int,t.jb_xx_int,t.jb_jr_int,work_type_id = case when t.jb_ps_int = 0 and t.jb_xx_int = 0 and t.jb_jr_int = 0 then 1 when t.jb_ps_int >0 then 3 when t.jb_xx_int>0 then 4 when t.jb_jr_int >0 then 5 end from COST_DIRECT_LABOUR_ATTENDANCE t 
 left join cost_dept_map m on  T.DEPT4 = M.DEPT4 where t.CDATE like + @cmonth + '%') 
 a 
--left join COST_DIRECT_LABOUR l on a.emp_no = l.CNO 
left join (select * from OPENQUERY (BARCODE, 'SELECT employee_id_,e_band FROM IHPS_ID_USER_PROFILE where department_ like ''制造中心%''')) u on a.emp_no = u.employee_id_ 
left join COST_WORK_TYPE w on a.WORK_TYPE_ID = w.CID 
left join cost_dept d on a.dept_id = d.cid 
left join COST_SALETYPE s on d.saletype_id = s.cid 
--left join (select * from COST_inDIRECT_LABOUR_PRICE where YYYYMM = @cmonth) p on w.CID = p.WORK_TYPE_ID and a.DIRECT_LABOUR_ID = p.INDIRECT_LABOUR_ID  
--left join (select * from COST_inDIRECT_LABOUR_PRICE where YYYYMM = @cmonth and WORK_TYPE_id = 3) p2 on a.DIRECT_LABOUR_ID = p2.INDIRECT_LABOUR_ID 
left join (select * from COST_inDIRECT_LABOUR_level_PRICE where YYYYMM = @cmonth) p on w.CID = p.WORK_TYPE and u.e_band>=p.LEVEL_BEGIN and u.e_band <=p.level_end  
left join (select * from COST_inDIRECT_LABOUR_level_PRICE where YYYYMM = @cmonth and WORK_TYPE = 1) p2 on u.e_band>=p.LEVEL_BEGIN and u.e_band <=p.level_end 
where s.CID = @saletypeid and a.cdate like '%' + @cmonth + '%' and a.person_type_id = 4 group by a.CDATE,w.cid,a.emp_no
 --select * from #indirect
 select cdate,saletypeid,max(saletypename) saletypename,sum(chours) chours,sum(cost) cost into #indirect_sum from #indirect group by cdate,saletypeid
 select cdate,saletypeid,deptid,max(saletypename) saletypename,sum(chours) chours,sum(cost) cost into #indirect_dept_sum from #indirect group by cdate,saletypeid,deptid 
--考虑终端和系统部门成本比率
select d.cdate,d.saletypeid,sum(chours) chours,sum(cost) cost,sum(cast(cost/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5))) cost2 into #indirect_dept_sum_rate from #indirect_dept_sum d
left join (select sale_type_id,dept_id,COST_RATE from cost_rate where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and isnull(dept_id,0) >0) r on d.saletypeid = r.sale_type_id and d.deptid = r.dept_id 
 group by d.cdate,d.saletypeid
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
  
	  select st.cid saletypeid,st.cname 营业类型,t.cdate 日期,d.chours 直接人力小时数,isnull(d.cost,0) 直接人力成本,isnull(td.chours,0) 临时工工时,isnull(td.cost,0) 临时工成本,i.chours 间接人力小时数,i.cost 间接人力成本,dp.DEPRECIATION 折旧费,t1.expense 运营部转嫁费用,t2.expense 试产转嫁费用,e.COMPOSITE_EXPENSE 主营综合费用,终端台数 = case when @type = 1 then s.product_quantity*qr.RATE else s.product_quantity end,
	  cast((isnull(d.cost2,0) +isnull(td.cost2,0) + isnull(i.cost2,0) + isnull(dp.DEPRECIATION,0) - isnull(t1.expense,0) - isnull(t2.expense,0)+ isnull(e.COMPOSITE_EXPENSE,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) as 预估成本,
	  预估单台成本= case when s.PRODUCT_QUANTITY is null or s.PRODUCT_QUANTITY = 0 then 0 else cast(((isnull(d.cost2,0) +isnull(td.cost2,0) + isnull(i.cost2,0) + isnull(dp.DEPRECIATION,0) - isnull(t1.expense,0) - isnull(t2.expense,0)+ isnull(e.COMPOSITE_EXPENSE,0))/CAST(r.cost_rate as decimal(18,4)))/case when @type = 1 then s.product_quantity*qr.RATE else s.product_quantity end as decimal(18,5)) end,
	  p.STANDARD_POINT as 标准单台成本,  cast(p.STANDARD_POINT*case when @type = 1 then s.product_quantity*qr.RATE else s.product_quantity end  as decimal(18,5)) 标准成本,
	  cast(p.STANDARD_POINT*case when @type = 1 then s.product_quantity*qr.RATE else s.product_quantity end as decimal(18,5)) - cast((isnull(d.cost2,0) +isnull(td.cost2,0) + isnull(i.cost2,0) + isnull(dp.DEPRECIATION,0) - isnull(t1.expense,0) - isnull(t2.expense,0)+ isnull(e.COMPOSITE_EXPENSE,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) 盈亏 
	  from #temp_days t left join COST_PRODUCT_QUANTITY s on t.cdate = s.CDATE and s.type = @type 
	  left join  #direct_dept_sum_rate d on t.cdate = d.cdate and t.saletypeid = d.saletypeid 
	  left join #indirect_dept_sum_rate i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid 
	  left join (select CDATE,saletypeid,sum(chours) chours,SUM(cost) cost,SUM(cost2) cost2 from  #temp_direct group by CDATE,saletypeid) td on t.CDATE = td.CDATE and t.saletypeid = td.saletypeid 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select cast(DEPRECIATION/cast(@days as decimal(18,2)) as decimal(18,5)) DEPRECIATION from cost_depreciation where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) dp on 1=1 
	  left join ( select i.cdate,i.sale_type_id,i.hours*t.price expense from cost_transfer i left join (select * from cost_transfer_price  where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and SALE_TYPE_ID = @saletypeid) t on 1=1 where i.cdate like '%' + @cmonth + '%' and i.sale_type_id = @saletypeid and i.transfer_type = 1) t1 on t.cdate = t1.cdate and t.saletypeid = t1.sale_type_id 
	  left join ( select i.cdate,i.sale_type_id,i.hours*t.price expense from cost_transfer i left join (select * from cost_transfer_price  where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and SALE_TYPE_ID = @saletypeid) t on 1=1 where i.cdate like '%' + @cmonth + '%' and i.sale_type_id = @saletypeid and i.transfer_type = 2) t2 on t.cdate = t2.cdate and t.saletypeid = t2.sale_type_id 
	  left join (select cast(expense/cast(@days as decimal(18,2)) as decimal(18,5)) COMPOSITE_EXPENSE from COST_COMPOSITE_EXPENSE where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) e on 1=1 
	  left join (select STANDARD_POINT from COST_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on 1=1 
	  left join (select COST_RATE from cost_rate where yyyy = left(@cmonth,4) and SALE_TYPE_ID = @saletypeid and QUARTER_ID = @quarter and isnull(dept_id,0) = 0) r on 1=1 
	  left join (select rate from COST_QUANTITY_RATE where YYYY = left(@cmonth,4) and QUARTER_ID = @quarter) qr on 1=1 

declare @days int,@quarter int,@cdate varchar(20),@type int,@cmonth varchar(20),@saletypeid int
set @cmonth = '2017-11'
set @saletypeid = 14
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
if @saletypeid = 14 
set @type = 1
else if @saletypeid = 15
set @type =2
	  select isnull(d.cost2,0),isnull(td.cost2,0),isnull(i.cost2,0),isnull(dp.DEPRECIATION,0),isnull(t1.expense,0),isnull(t2.expense,0) ,isnull(e.COMPOSITE_EXPENSE,0),
	  (isnull(d.cost2,0) +isnull(td.cost2,0) + isnull(i.cost2,0) + isnull(dp.DEPRECIATION,0) - isnull(t1.expense,0) - isnull(t2.expense,0)+ isnull(e.COMPOSITE_EXPENSE,0)) as 预估成本,CAST(r.cost_rate as decimal(18,4)) 
	  from #temp_days t left join COST_PRODUCT_QUANTITY s on t.cdate = s.CDATE and s.type = @type 
	  left join  #direct_dept_sum_rate d on t.cdate = d.cdate and t.saletypeid = d.saletypeid 
	  left join #indirect_dept_sum_rate i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid 
	  left join (select CDATE,saletypeid,sum(chours) chours,SUM(cost) cost,SUM(cost2) cost2 from  #temp_direct group by CDATE,saletypeid) td on t.CDATE = td.CDATE and t.saletypeid = td.saletypeid 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select cast(DEPRECIATION/cast(@days as decimal(18,2)) as decimal(18,5)) DEPRECIATION from cost_depreciation where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) dp on 1=1 
	  left join ( select i.cdate,i.sale_type_id,i.hours*t.price expense from cost_transfer i left join (select * from cost_transfer_price  where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and SALE_TYPE_ID = @saletypeid) t on 1=1 where i.cdate like '%' + @cmonth + '%' and i.sale_type_id = @saletypeid and i.transfer_type = 1) t1 on t.cdate = t1.cdate and t.saletypeid = t1.sale_type_id 
	  left join ( select i.cdate,i.sale_type_id,i.hours*t.price expense from cost_transfer i left join (select * from cost_transfer_price  where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and SALE_TYPE_ID = @saletypeid) t on 1=1 where i.cdate like '%' + @cmonth + '%' and i.sale_type_id = @saletypeid and i.transfer_type = 2) t2 on t.cdate = t2.cdate and t.saletypeid = t2.sale_type_id 
	  left join (select cast(expense/cast(@days as decimal(18,2)) as decimal(18,5)) COMPOSITE_EXPENSE from COST_COMPOSITE_EXPENSE where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) e on 1=1 
	  left join (select STANDARD_POINT from COST_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on 1=1 
	  left join (select COST_RATE from cost_rate where yyyy = left(@cmonth,4) and SALE_TYPE_ID = @saletypeid and QUARTER_ID = @quarter and isnull(dept_id,0) = 0) r on 1=1 
	  left join (select rate from COST_QUANTITY_RATE where YYYY = left(@cmonth,4) and QUARTER_ID = @quarter) qr on 1=1 


