--drop table #direct
-- drop table #indirect
-- drop table #direct_sum
--  drop table #indirect_sum
-- drop table #temp
-- drop table #temp_days
alter procedure COSTING
@cmonth varchar(20),
@saletypeid int
as
set nocount on
declare @days int,@quarter int,@cdate varchar(20),@type int
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
if @saletypeid = 14 
set @type = 1
else if @saletypeid = 15
set @type =2

--ֱ���˹��ɱ�
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

--�����ն˺�ϵͳ���ųɱ�����
select d.cdate,d.saletypeid,sum(chours) chours,sum(cost) cost,sum(cast(cost/CAST(r.cost_rate as decimal(18,4))  as decimal(18,5))) cost2 into #direct_dept_sum_rate from #direct_dept_sum d
left join (select sale_type_id,dept_id,COST_RATE from cost_rate where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and isnull(dept_id,0) >0) r on d.saletypeid = r.sale_type_id and d.deptid = r.dept_id 
where saletypeid = 14 or saletypeid = 15 group by d.cdate,d.saletypeid
--��ʱ���ɱ�,ÿ�����ʱ���ɱ�,Ҳ�����˲��ųɱ�����
select a.CDATE ,a.SALE_TYPE_ID saletypeid,a.dept_id,a.HOURS chours,cast(a.HOURS*p.PRICE  as decimal(18,5)) cost,
cast(a.HOURS*p.PRICE/CAST(r.cost_rate as decimal(18,4))  as decimal(18,5)) cost2 into #temp_direct 
from COST_TEMP_WORKER a 
left join (select * from COST_TEMP_WORKER_PRICE where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on 1=1 
left join (select sale_type_id,dept_id,COST_RATE from cost_rate where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and isnull(dept_id,0) >0) r on a.SALE_TYPE_ID = r.sale_type_id and a.dept_id= r.dept_id 
where a.SALE_TYPE_ID = @saletypeid and a.cdate like '%' + @cmonth + '%'  
--select * from #direct

--����˹��ɱ�
select a.CDATE ,max(s.cid) saletypeid,max(s.cname) saletypename,max(d.cid) deptid,max(d.cname) deptname,MAX(a.emp_name) personname,MAX(w.CNAME) worktypename,SUM(a.gz_int + a.jb_ps_int + a.jb_xx_int + a.jb_jr_int) chours,
cost = case when  w.cid = 1 then SUM(a.gz_int)*max(p.PRICE) 
when w.cid = 3 then SUM(a.jb_ps_int)*max(p.PRICE) +SUM(a.gz_int)*max(p2.PRICE) when w.cid = 4 then SUM(a.jb_xx_int)*max(p.PRICE) +SUM(a.gz_int)*max(p2.PRICE) when w.CID = 5 then SUM(a.jb_jr_int)*max(p.PRICE) +SUM(a.gz_int)*max(p2.PRICE) end into #indirect 
from 
(select t.cdate,t.emp_no,t.emp_name,t.person_type_id,m.dept_id,t.gz_int,t.jb_ps_int,t.jb_xx_int,t.jb_jr_int,work_type_id = case when t.jb_ps_int = 0 and t.jb_xx_int = 0 and t.jb_jr_int = 0 then 1 when t.jb_ps_int >0 then 3 when t.jb_xx_int>0 then 4 when t.jb_jr_int >0 then 5 end from COST_DIRECT_LABOUR_ATTENDANCE t 
 left join cost_dept_map m on  T.DEPT4 = M.DEPT4 where t.CDATE like + @cmonth + '%') 
 a 
--left join COST_DIRECT_LABOUR l on a.emp_no = l.CNO 
left join (select * from OPENQUERY (BARCODE, 'SELECT employee_id_,e_band FROM IHPS_ID_USER_PROFILE where department_ like ''��������%''')) u on a.emp_no = u.employee_id_ 
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
--�����ն˺�ϵͳ���ųɱ�����
select d.cdate,d.saletypeid,sum(chours) chours,sum(cost) cost,sum(cast(cost/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5))) cost2 into #indirect_dept_sum_rate from #indirect_dept_sum d
left join (select sale_type_id,dept_id,COST_RATE from cost_rate where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and isnull(dept_id,0) >0) r on d.saletypeid = r.sale_type_id and d.deptid = r.dept_id 
where saletypeid = 14 or saletypeid = 15 group by d.cdate,d.saletypeid
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

 --���ճɱ�
  --���ڡ�Ӫҵ���͡�ֱ������Сʱ����ֱ�������ɱ����������Сʱ������������ɱ����۾ɷѡ����޷ѡ�ˮ��ѡ�����������Ԥ���ɱ���Ԥ������ɱ�����׼����ɱ�����׼�ɱ���ӯ��
  if @saletypeid = 2
  begin
	  insert into COST_DAY_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,CDATE,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,DEPRECIATION,RENT_EXPENSE,WATER_ELECTRICITY,POINTCOUNT,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname Ӫҵ����,t.cdate ����,d.chours ֱ������Сʱ��,d.cost ֱ�������ɱ�,i.chours �������Сʱ��,i.cost ��������ɱ�,dp.DEPRECIATION �۾ɷ�,re.RENT_EXPENSE*2 ���޷�,
	  we.WATER_ELECTRICITY ˮ���,s.POINTCOUNT ��������,
	  cast((isnull(d.cost,0) + isnull(i.cost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0)*2 + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) as Ԥ���ɱ�,
	  Ԥ������ɱ�= case when s.POINTCOUNT is null or s.POINTCOUNT = 0 then 0 else cast(((isnull(d.cost,0) + isnull(i.cost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0)*2 + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4)))/s.POINTCOUNT as decimal(18,5)) end,
	  p.STANDARD_POINT as ��׼����ɱ�,  cast(p.STANDARD_POINT*s.POINTCOUNT  as decimal(18,5)) ��׼�ɱ�,
	  cast(p.STANDARD_POINT*s.POINTCOUNT as decimal(18,5)) - cast((isnull(d.cost,0) + isnull(i.cost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0) + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) ӯ�� 
	  from #temp_days t left join cost_pointcount_sum s on t.cdate = s.CDATE and t.saletypeid = s.SALETYPE_ID 
	  left join  #direct_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid 
	  left join #indirect_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select cast(DEPRECIATION/cast(@days as decimal(18,2)) as decimal(18,5)) DEPRECIATION from cost_depreciation where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) dp on 1=1 
	  left join (select cast(RENT_EXPENSE*0.5/cast(@days as decimal(18,2)) as decimal(18,5)) RENT_EXPENSE from COST_RENT_EXPENSE where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) re on 1=1 
	  left join (select cast(WATER_ELECTRICITY/cast(@days as decimal(18,2)) as decimal(18,5)) WATER_ELECTRICITY from COST_WATER_ELECTRICITY where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) we on 1=1 
	  left join (select STANDARD_POINT from COST_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on 1=1
	  left join (select COST_RATE from cost_rate where yyyy = left(@cmonth,4) and SALE_TYPE_ID = @saletypeid and QUARTER_ID = @quarter and isnull(dept_id,0) = 0) r  on 1=1 
  end
  if @saletypeid = 10
  begin
	  insert into COST_DAY_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,CDATE,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,DEPRECIATION,RENT_EXPENSE,WATER_ELECTRICITY,POINTCOUNT,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname Ӫҵ����,t.cdate ����,d.chours ֱ������Сʱ��,d.cost ֱ�������ɱ�,i.chours �������Сʱ��,i.cost ��������ɱ�,dp.DEPRECIATION �۾ɷ�,re.RENT_EXPENSE*2 ���޷�,
	  we.WATER_ELECTRICITY ˮ���,s.POINTCOUNT ��������,
	  cast((isnull(d.cost,0) + isnull(i.cost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0)*2 + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4))*(1-n.STEEL_NET_RATE) as decimal(18,5)) as Ԥ���ɱ�,
	  Ԥ������ɱ�= case when s.POINTCOUNT is null or s.POINTCOUNT = 0 then 0 else cast(((isnull(d.cost,0) + isnull(i.cost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0)*2 + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4))*(1-n.STEEL_NET_RATE) - isnull(re.RENT_EXPENSE,0))/s.POINTCOUNT as decimal(18,5)) end,
	  p.STANDARD_POINT as ��׼����ɱ�,  cast(p.STANDARD_POINT*s.POINTCOUNT + isnull(re.RENT_EXPENSE,0) as decimal(18,5)) ��׼�ɱ�,
	  cast(p.STANDARD_POINT*s.POINTCOUNT + isnull(re.RENT_EXPENSE,0) as decimal(18,5)) - cast((isnull(d.cost,0) + isnull(i.cost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0) + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4))*(1-n.STEEL_NET_RATE) as decimal(18,5)) ӯ�� 
	  from #temp_days t left join cost_pointcount_sum s on t.cdate = s.CDATE and t.saletypeid = s.SALETYPE_ID 
	  left join  #direct_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid 
	  left join #indirect_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select cast(DEPRECIATION/cast(@days as decimal(18,2)) as decimal(18,5)) DEPRECIATION from cost_depreciation where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) dp on 1=1 
	  left join (select cast(RENT_EXPENSE*0.5/cast(@days as decimal(18,2)) as decimal(18,5)) RENT_EXPENSE from COST_RENT_EXPENSE where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) re on 1=1 
	  left join (select cast(WATER_ELECTRICITY/cast(@days as decimal(18,2)) as decimal(18,5)) WATER_ELECTRICITY from COST_WATER_ELECTRICITY where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) we on 1=1 
	  left join (select STANDARD_POINT from COST_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on 1=1 
	  left join (select COST_RATE from cost_rate where yyyy = left(@cmonth,4) and SALE_TYPE_ID = @saletypeid and QUARTER_ID = @quarter and isnull(dept_id,0) = 0) r on 1=1 
	  left join (select STEEL_NET_RATE from COST_STEEL_NET_RATE where YYYYMM = @cmonth) n on 1=1  
  end
  if @saletypeid = 13
  begin
	  insert into COST_DAY_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,CDATE,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,EMS_HH_HOURS,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname Ӫҵ����,t.cdate ����,d.chours ֱ������Сʱ��,d.cost ֱ�������ɱ�,i.chours �������Сʱ��,i.cost ��������ɱ�,h.HOURS ������ʱ,
	  cast((isnull(d.cost,0) + isnull(i.cost,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) as Ԥ���ɱ�,
	  Ԥ����Сʱ�ɱ�= case when h.HOURS is null or h.HOURS = 0 then 0 else cast(((isnull(d.cost,0) + isnull(i.cost,0))/CAST(r.cost_rate as decimal(18,4)))/h.HOURS as decimal(18,5)) end,
	  p.STANDARD_POINT as ��׼��Сʱ�ɱ�,  cast(p.STANDARD_POINT*h.HOURS as decimal(18,5)) ��׼�ɱ�,
	  cast(p.STANDARD_POINT*h.HOURS  as decimal(18,5)) - cast((isnull(d.cost,0) + isnull(i.cost,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) ӯ�� 
	  from #temp_days t left join COST_EMS_HH_HOURS h on t.cdate = h.CDATE 
	  left join  #direct_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid 
	  left join #indirect_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select STANDARD_POINT from COST_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on 1=1 
	  left join (select COST_RATE from cost_rate where yyyy = left(@cmonth,4) and SALE_TYPE_ID = @saletypeid and QUARTER_ID = @quarter and isnull(dept_id,0) = 0) r on 1=1 
  end
  if @saletypeid = 14 or @saletypeid = 15
  begin
	  insert into COST_DAY_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,CDATE,DIRECT_HOURS,DIRECT_COST,TEMP_HOURS,TEMP_COST,INDIRECT_HOURS,INDIRECT_COST,DEPRECIATION,OPERATION_TRANSFER,TRIAL_TRANSFER,COMPOSITE_EXPENSE,POINTCOUNT,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname Ӫҵ����,t.cdate ����,d.chours ֱ������Сʱ��,isnull(d.cost,0) ֱ�������ɱ�,isnull(td.chours,0) ��ʱ����ʱ,isnull(td.cost,0) ��ʱ���ɱ�,i.chours �������Сʱ��,i.cost ��������ɱ�,dp.DEPRECIATION �۾ɷ�,t1.expense ��Ӫ��ת�޷���,t2.expense �Բ�ת�޷���,e.COMPOSITE_EXPENSE ��Ӫ�ۺϷ���,�ն�̨�� = case when @type = 1 then s.product_quantity*qr.RATE else s.product_quantity end,
	  cast((isnull(d.cost2,0) +isnull(td.cost2,0) + isnull(i.cost2,0) + isnull(dp.DEPRECIATION,0) - isnull(t1.expense,0) - isnull(t2.expense,0)+ isnull(e.COMPOSITE_EXPENSE,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) as Ԥ���ɱ�,
	  Ԥ����̨�ɱ�= case when s.PRODUCT_QUANTITY is null or s.PRODUCT_QUANTITY = 0 then 0 else cast(((isnull(d.cost2,0) +isnull(td.cost2,0) + isnull(i.cost2,0) + isnull(dp.DEPRECIATION,0) - isnull(t1.expense,0) - isnull(t2.expense,0)+ isnull(e.COMPOSITE_EXPENSE,0))/CAST(r.cost_rate as decimal(18,4)))/case when @type = 1 then s.product_quantity*qr.RATE else s.product_quantity end as decimal(18,5)) end,
	  p.STANDARD_POINT as ��׼��̨�ɱ�,  cast(p.STANDARD_POINT*case when @type = 1 then s.product_quantity*qr.RATE else s.product_quantity end  as decimal(18,5)) ��׼�ɱ�,
	  cast(p.STANDARD_POINT*case when @type = 1 then s.product_quantity*qr.RATE else s.product_quantity end as decimal(18,5)) - cast((isnull(d.cost2,0) +isnull(td.cost2,0) + isnull(i.cost2,0) + isnull(dp.DEPRECIATION,0) - isnull(t1.expense,0) - isnull(t2.expense,0)+ isnull(e.COMPOSITE_EXPENSE,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) ӯ�� 
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
  end
--if @@error <> 0 
--goto ErrMsg

--�¶ȳɱ�
 --Ӫҵ���͡��·ݡ�ֱ������Сʱ����ֱ�������ɱ����������Сʱ������������ɱ����۾ɷѡ����޷ѡ�ˮ��ѡ�����������Ԥ���ɱ���Ԥ������ɱ�����׼����ɱ�����׼�ɱ���ӯ��
  if @saletypeid = 2
  begin
	  insert into COST_MONTH_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,CMONTH,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,DEPRECIATION,RENT_EXPENSE,WATER_ELECTRICITY,POINTCOUNT,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname Ӫҵ����,@cmonth �·�,t.chours ֱ������Сʱ��,t.cost ֱ�������ɱ�,t.inhours �������Сʱ��,t.incost ��������ɱ�,dp.DEPRECIATION �۾ɷ�,re.RENT_EXPENSE*2 ���޷�,
	  we.WATER_ELECTRICITY ˮ���,t.POINTCOUNT ��������,
	  cast((isnull(t.cost,0) + isnull(t.incost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0)*2 + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) as Ԥ���ɱ�,
	  Ԥ������ɱ�= case when t.POINTCOUNT is null or t.POINTCOUNT = 0 then 0 else cast(((isnull(t.cost,0) + isnull(t.incost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0)*2 + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4)))/t.POINTCOUNT as decimal(18,5)) end,
	  p.STANDARD_POINT as ��׼����ɱ�,
	  cast(p.STANDARD_POINT*t.POINTCOUNT as decimal(18,5)) ��׼�ɱ�,
	  cast(p.STANDARD_POINT*t.POINTCOUNT  as decimal(18,5)) - cast((isnull(t.cost,0) + isnull(t.incost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0) + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) ӯ�� 
	  from 
	  (select t.saletypeid,sum(d.chours) chours,SUM(d.cost) cost,SUM(i.chours) inhours,SUM(i.cost) incost,SUM(s.POINTCOUNT) pointcount from #temp_days t left join cost_pointcount_sum s on t.cdate = s.CDATE and t.saletypeid = s.SALETYPE_ID 
	  left join  #direct_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid 
	  left join #indirect_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid group by t.saletypeid
	  ) t 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select cast(DEPRECIATION as decimal(18,5)) DEPRECIATION from cost_depreciation where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) dp on 1=1 
	  left join (select cast(RENT_EXPENSE*0.5 as decimal(18,5)) RENT_EXPENSE from COST_RENT_EXPENSE where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) re on 1=1 
	  left join (select cast(WATER_ELECTRICITY as decimal(18,5)) WATER_ELECTRICITY from COST_WATER_ELECTRICITY where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) we on 1=1 
	  left join (select STANDARD_POINT from COST_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on 1=1 
	  left join (select COST_RATE from cost_rate where yyyy = left(@cmonth,4) and SALE_TYPE_ID = @saletypeid and QUARTER_ID = @quarter and isnull(dept_id,0) = 0) r on 1=1
  end
  if @saletypeid = 10
  begin
	  insert into COST_MONTH_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,CMONTH,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,DEPRECIATION,RENT_EXPENSE,WATER_ELECTRICITY,POINTCOUNT,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname Ӫҵ����,@cmonth �·�,t.chours ֱ������Сʱ��,t.cost ֱ�������ɱ�,t.inhours �������Сʱ��,t.incost ��������ɱ�,dp.DEPRECIATION �۾ɷ�,re.RENT_EXPENSE*2 ���޷�,
	  we.WATER_ELECTRICITY ˮ���,t.POINTCOUNT ��������,
	  cast((isnull(t.cost,0) + isnull(t.incost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0)*2 + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4))*(1-n.STEEL_NET_RATE) as decimal(18,5)) as Ԥ���ɱ�,
	  Ԥ������ɱ�= case when t.POINTCOUNT is null or t.POINTCOUNT = 0 then 0 else cast(((isnull(t.cost,0) + isnull(t.incost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0)*2 + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4))*(1-n.STEEL_NET_RATE) - isnull(re.RENT_EXPENSE,0))/t.POINTCOUNT as decimal(18,5)) end,
	  p.STANDARD_POINT as ��׼����ɱ�,
	  cast(p.STANDARD_POINT*t.POINTCOUNT + isnull(re.RENT_EXPENSE,0) as decimal(18,5)) ��׼�ɱ�,
	  cast(p.STANDARD_POINT*t.POINTCOUNT + isnull(re.RENT_EXPENSE,0) as decimal(18,5)) - cast((isnull(t.cost,0) + isnull(t.incost,0) + isnull(dp.DEPRECIATION,0) + isnull(re.RENT_EXPENSE,0) + isnull(we.WATER_ELECTRICITY,0))/CAST(r.cost_rate as decimal(18,4))*(1-n.STEEL_NET_RATE) as decimal(18,5)) ӯ�� 
	  from 
	  (select t.saletypeid,sum(d.chours) chours,SUM(d.cost) cost,SUM(i.chours) inhours,SUM(i.cost) incost,SUM(s.POINTCOUNT) pointcount from #temp_days t left join cost_pointcount_sum s on t.cdate = s.CDATE and t.saletypeid = s.SALETYPE_ID 
	  left join  #direct_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid 
	  left join #indirect_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid group by t.saletypeid
	  ) t 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select cast(DEPRECIATION as decimal(18,5)) DEPRECIATION from cost_depreciation where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) dp on 1=1 
	  left join (select cast(RENT_EXPENSE*0.5 as decimal(18,5)) RENT_EXPENSE from COST_RENT_EXPENSE where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) re on 1=1 
	  left join (select cast(WATER_ELECTRICITY as decimal(18,5)) WATER_ELECTRICITY from COST_WATER_ELECTRICITY where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) we on 1=1 
	  left join (select STANDARD_POINT from COST_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on 1=1 
	  left join (select COST_RATE from cost_rate where yyyy = left(@cmonth,4) and SALE_TYPE_ID = @saletypeid and QUARTER_ID = @quarter and isnull(dept_id,0) = 0) r on 1=1 
	  left join (select STEEL_NET_RATE from COST_STEEL_NET_RATE where YYYYMM = @cmonth) n on 1=1
  end
  if @saletypeid = 13
  begin
	  insert into COST_MONTH_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,CMONTH,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,EMS_HH_HOURS,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname Ӫҵ����,@cmonth �·�,t.chours ֱ������Сʱ��,t.cost ֱ�������ɱ�,t.inhours �������Сʱ��,t.incost ��������ɱ�,t.EMS_HH_HOURS ������ʱ,
	  cast((isnull(t.cost,0) + isnull(t.incost,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) as Ԥ���ɱ�,
	  Ԥ������ɱ�= case when t.EMS_HH_HOURS is null or t.EMS_HH_HOURS = 0 then 0 else cast(((isnull(t.cost,0) + isnull(t.incost,0))/CAST(r.cost_rate as decimal(18,4)))/t.EMS_HH_HOURS as decimal(18,5)) end,
	  p.STANDARD_POINT as ��׼����ɱ�,
	  cast(p.STANDARD_POINT*t.EMS_HH_HOURS as decimal(18,5)) ��׼�ɱ�,
	  cast(p.STANDARD_POINT*t.EMS_HH_HOURS as decimal(18,5)) - cast((isnull(t.cost,0) + isnull(t.incost,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) ӯ�� 
	  from 
	  (select t.saletypeid,sum(d.chours) chours,SUM(d.cost) cost,SUM(i.chours) inhours,SUM(i.cost) incost,SUM(h.HOURS) EMS_HH_HOURS from #temp_days t left join COST_EMS_HH_HOURS h on t.cdate = h.CDATE  
	  left join  #direct_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid 
	  left join #indirect_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid group by t.saletypeid
	  ) t 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select STANDARD_POINT from COST_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on 1=1 
	  left join (select COST_RATE from cost_rate where yyyy = left(@cmonth,4) and SALE_TYPE_ID = @saletypeid and QUARTER_ID = @quarter and isnull(dept_id,0) = 0) r on 1=1
  end
  if @saletypeid = 14 or @saletypeid = 15
  begin
	  insert into COST_MONTH_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,CMONTH,DIRECT_HOURS,DIRECT_COST,TEMP_HOURS,TEMP_COST,INDIRECT_HOURS,INDIRECT_COST,DEPRECIATION,OPERATION_TRANSFER,TRIAL_TRANSFER,COMPOSITE_EXPENSE,POINTCOUNT,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname Ӫҵ����,@cmonth �·�,t.chours ֱ������Сʱ��,t.cost ֱ�������ɱ�,t.thours ��ʱ����ʱ,t.tcost ��ʱ���ɱ�,t.inhours �������Сʱ��,t.incost ��������ɱ�,dp.DEPRECIATION �۾ɷ�,t.operation_expense Ӫҵ��ת�޷���,t.trial_expense �Բ�ת�޷���,e.COMPOSITE_EXPENSE ��Ӫ�ۺϷ���, t.product_quantity �ն�̨��,
	  cast((isnull(t.cost2,0) + isnull(t.tcost2,0)+ isnull(t.incost2,0) + isnull(dp.DEPRECIATION,0) - isnull(t.operation_expense,0)  - isnull(t.trial_expense,0)  + isnull(e.COMPOSITE_EXPENSE,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) as Ԥ���ɱ�,
	  Ԥ����̨�ɱ�= case when product_quantity is null or product_quantity = 0 then 0 else cast(((isnull(t.cost2,0) + isnull(t.tcost2,0)+ isnull(t.incost2,0) + isnull(dp.DEPRECIATION,0) - isnull(t.operation_expense,0)  - isnull(t.trial_expense,0)  + isnull(e.COMPOSITE_EXPENSE,0))/CAST(r.cost_rate as decimal(18,4)))/t.product_quantity  as decimal(18,5)) end,
	  p.STANDARD_POINT as ��׼��̨�ɱ�,
	  cast(p.STANDARD_POINT*t.product_quantity as decimal(18,5)) ��׼�ɱ�,
	  cast(p.STANDARD_POINT*t.product_quantity  as decimal(18,5)) - cast((isnull(t.cost2,0) + isnull(t.tcost2,0)+ isnull(t.incost2,0) + isnull(dp.DEPRECIATION,0) - isnull(t.operation_expense,0)  - isnull(t.trial_expense,0)  + isnull(e.COMPOSITE_EXPENSE,0))/CAST(r.cost_rate as decimal(18,4)) as decimal(18,5)) ӯ�� 
	  from 
	  (select t.saletypeid,sum(d.chours) chours,SUM(isnull(d.cost,0)) COST,SUM(isnull(d.cost2,0)) COST2,SUM(isnull(td.chours,0)) thours, sum(isnull(td.cost,0)) tcost,sum(isnull(td.cost2,0)) tcost2,SUM(i.chours) inhours,SUM(i.cost) incost,SUM(i.cost2) incost2,sum(t1.expense) operation_expense,sum(t2.expense) trial_expense,SUM(case when @type = 1 then s.product_quantity*qr.RATE else s.product_quantity end) product_quantity from #temp_days t left join COST_PRODUCT_QUANTITY s on t.cdate = s.CDATE and s.type = @type 
	  left join (select rate from COST_QUANTITY_RATE where YYYY = left(@cmonth,4) and QUARTER_ID = @quarter) qr on 1=1 
	  left join  #direct_dept_sum_rate d on t.cdate = d.cdate and t.saletypeid = d.saletypeid 
	  left join #indirect_dept_sum_rate i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid 
	  left join (select CDATE,saletypeid,sum(chours) chours,SUM(cost) cost,SUM(cost2) cost2 from  #temp_direct group by CDATE,saletypeid) td on t.CDATE = td.CDATE and t.saletypeid = td.saletypeid 
	  left join ( select i.cdate,i.sale_type_id,i.hours*t.price expense from cost_transfer i left join (select * from cost_transfer_price  where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and SALE_TYPE_ID = @saletypeid) t on 1=1 where i.cdate like '%' + @cmonth + '%' and i.sale_type_id = @saletypeid and i.transfer_type = 1) t1 on t.cdate = t1.cdate and t.saletypeid = t1.sale_type_id 
	  left join ( select i.cdate,i.sale_type_id,i.hours*t.price expense from cost_transfer i left join (select * from cost_transfer_price  where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and SALE_TYPE_ID = @saletypeid) t on 1=1 where i.cdate like '%' + @cmonth + '%' and i.sale_type_id = @saletypeid and i.transfer_type = 2) t2 on t.cdate = t2.cdate and t.saletypeid = t2.sale_type_id 
	  group by t.saletypeid
	  ) t 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select cast(DEPRECIATION as decimal(18,5)) DEPRECIATION from cost_depreciation where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) dp on 1=1 
	  left join (select cast(expense as decimal(18,5)) COMPOSITE_EXPENSE from COST_COMPOSITE_EXPENSE where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) e on 1=1 
	  left join (select STANDARD_POINT from COST_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on 1=1 
	  left join (select COST_RATE from cost_rate where yyyy = left(@cmonth,4) and SALE_TYPE_ID = @saletypeid and QUARTER_ID = @quarter and isnull(dept_id,0) = 0) r on 1=1 
  end
  --if @@error <> 0 
  --goto ErrMsg
  
  --�����ųɱ�������
  --���š�Ӫҵ���͡����ڡ�ֱ������Сʱ����ֱ�������ɱ����������Сʱ������������ɱ�������������Ԥ���ɱ���Ԥ������ɱ�����׼����ɱ�����׼�ɱ���ӯ��
  if @saletypeid = 2 or @saletypeid = 10
  begin
	  insert into COST_DEPT_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,DEPT_ID,DEPT_NAME,CDATE,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,POINTCOUNT,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname Ӫҵ����,t.cid,t.cname,t.cdate ����,d.chours ֱ������Сʱ��,d.cost ֱ�������ɱ�,i.chours �������Сʱ��,i.cost ��������ɱ�,
	  s.POINTCOUNT ��������,
	  cast((isnull(d.cost,0) + isnull(i.cost,0)) as decimal(18,5)) as Ԥ���ɱ�,
	  Ԥ������ɱ�= case when s.POINTCOUNT is null or s.POINTCOUNT = 0 then 0 else cast((isnull(d.cost,0) + isnull(i.cost,0))/cast(s.POINTCOUNT as decimal(18,5)) as decimal(18,5)) end, 
	  p.DEPT_STANDARD_POINT as ��׼����ɱ�, cast(p.DEPT_STANDARD_POINT*s.POINTCOUNT as decimal(18,5)) ��׼�ɱ�,
	  cast(p.DEPT_STANDARD_POINT*s.POINTCOUNT as decimal(18,5)) - cast((isnull(d.cost,0) + isnull(i.cost,0)) as decimal(18,5)) ӯ�� 
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
	  select st.cid saletypeid,st.cname Ӫҵ����,t.cid,t.cname,t.cdate ����,d.chours ֱ������Сʱ��,d.cost ֱ�������ɱ�,i.chours �������Сʱ��,i.cost ��������ɱ�,
	  h.HOURS ������ʱ,
	  cast((isnull(d.cost,0) + isnull(i.cost,0)) as decimal(18,5)) as Ԥ���ɱ�,
	  Ԥ����Сʱ�ɱ�= case when h.HOURS is null or h.HOURS = 0 then 0 else cast((isnull(d.cost,0) + isnull(i.cost,0))/cast(h.HOURS as decimal(18,5)) as decimal(18,5)) end, 
	  p.DEPT_STANDARD_POINT as ��׼����ɱ�, cast(p.DEPT_STANDARD_POINT*h.HOURS as decimal(18,5)) ��׼�ɱ�,
	  cast(p.DEPT_STANDARD_POINT*h.HOURS as decimal(18,5)) - cast((isnull(d.cost,0) + isnull(i.cost,0)) as decimal(18,5)) ӯ�� 
	  from (select * from #temp_days t left join cost_dept dp on t.saletypeid = dp.SALETYPE_ID) t 
	  left join COST_EMS_HH_HOURS h on t.cdate = h.CDATE 
	  left join  #direct_dept_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid and t.cid = d.deptid 
	  left join #indirect_dept_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid  and t.cid = i.deptid 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select dept_id,DEPT_STANDARD_POINT from COST_DEPT_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on t.cid = p.DEPT_ID
  end
  if @saletypeid = 14 or @saletypeid = 15
  begin
	  insert into COST_DEPT_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,DEPT_ID,DEPT_NAME,CDATE,DIRECT_HOURS,DIRECT_COST,TEMP_HOURS,TEMP_COST,INDIRECT_HOURS,INDIRECT_COST,POINTCOUNT,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select st.cid saletypeid,st.cname Ӫҵ����,t.cid,t.cname,t.cdate ����,d.chours ֱ������Сʱ��,cast(isnull(d.cost,0) as decimal(18,5))  ֱ�������ɱ�,isnull(td.chours,0) ��ʱ����ʱ,isnull(td.cost,0) ��ʱ���ɱ�,i.chours �������Сʱ��,cast(isnull(i.cost,0) as decimal(18,5)) ��������ɱ�,
	   ����̨�� = case when @type = 1 then s.PRODUCT_QUANTITY*qr.RATE else s.PRODUCT_QUANTITY end,
	  cast((isnull(d.cost,0)/cast(r.cost_rate as decimal(18,4)) + isnull(td.cost2,0) + isnull(i.cost,0)/cast(r2.cost_rate as decimal(18,4))) as decimal(18,5)) as Ԥ���ɱ�,
	  Ԥ����̨�ɱ�= case when s.PRODUCT_QUANTITY is null or s.PRODUCT_QUANTITY = 0 then 0 else cast((isnull(d.cost,0)/cast(r.cost_rate as decimal(18,4)) + isnull(td.cost2,0) + isnull(i.cost,0)/cast(r2.cost_rate as decimal(18,4)))/cast(case when @type = 1 then s.PRODUCT_QUANTITY*qr.RATE else s.PRODUCT_QUANTITY end as decimal(18,5)) as decimal(18,5)) end, 
	  p.DEPT_STANDARD_POINT as ��׼��̨�ɱ�, cast(p.DEPT_STANDARD_POINT*case when @type = 1 then s.PRODUCT_QUANTITY*qr.RATE else s.PRODUCT_QUANTITY end as decimal(18,5)) ��׼�ɱ�,
	  cast(p.DEPT_STANDARD_POINT*case when @type = 1 then s.PRODUCT_QUANTITY*qr.RATE else s.PRODUCT_QUANTITY end as decimal(18,5)) - cast((isnull(d.cost,0)/cast(r.cost_rate as decimal(18,4)) + isnull(td.cost2,0) + isnull(i.cost,0)/cast(r2.cost_rate as decimal(18,4))) as decimal(18,5)) ӯ�� 
	  from (select * from #temp_days t left join cost_dept dp on t.saletypeid = dp.SALETYPE_ID) t 
	  left join COST_PRODUCT_QUANTITY s on t.cdate = s.CDATE and s.type = @type 
	  left join  #direct_dept_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid and t.cid = d.deptid 
	  left join (select COST_RATE,sale_type_id,dept_id from cost_rate where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and isnull(dept_id,0) >0) r on d.saletypeid = r.sale_type_id and d.deptid = r.dept_id 
	  left join #indirect_dept_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid  and t.cid = i.deptid 
	  left join (select COST_RATE,sale_type_id,dept_id from cost_rate where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and isnull(dept_id,0) >0) r2 on i.saletypeid = r2.sale_type_id and i.deptid = r2.dept_id 
	  left join #temp_direct td on t.CDATE = td.CDATE and t.saletypeid = td.saletypeid and t.cid = td.dept_id 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select dept_id,DEPT_STANDARD_POINT from COST_DEPT_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on t.cid = p.DEPT_ID 
	  left join (select rate from COST_QUANTITY_RATE where YYYY = left(@cmonth,4) and QUARTER_ID = @quarter) qr on 1=1 
  end
  --if @@error <> 0 
  --goto ErrMsg
  
  --�����ųɱ����¶�
  --���š�Ӫҵ���͡��¶ȡ�ֱ������Сʱ����ֱ�������ɱ����������Сʱ������������ɱ�������������Ԥ���ɱ���Ԥ������ɱ�����׼����ɱ�����׼�ɱ���ӯ��
  if @saletypeid = 2 or @saletypeid = 10
  begin
	  insert into COST_DEPT_MONTH_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,DEPT_ID,DEPT_NAME,CMONTH,DIRECT_HOURS,DIRECT_COST,INDIRECT_HOURS,INDIRECT_COST,POINTCOUNT,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select saletypeid,saletypename Ӫҵ����,deptid,deptname,cmonth �·�,dhours ֱ������Сʱ��,dcost ֱ�������ɱ�,inhours �������Сʱ��,incost ��������ɱ�,
	  pointcount ��������,
	  cast((isnull(dcost,0) + isnull(incost,0)) as decimal(18,5)) as Ԥ���ɱ�,
	  Ԥ������ɱ�= case when POINTCOUNT is null or POINTCOUNT = 0 then 0 else cast((isnull(dcost,0) + isnull(incost,0))/cast(POINTCOUNT as decimal(18,5)) as decimal(18,5)) end, 
	  DEPT_STANDARD_POINT as ��׼����ɱ�, cast(DEPT_STANDARD_POINT*POINTCOUNT as decimal(18,5)) ��׼�ɱ�,
	  cast(DEPT_STANDARD_POINT*POINTCOUNT as decimal(18,5)) - cast((isnull(dcost,0) + isnull(incost,0)) as decimal(18,5)) ӯ�� 
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
	  select saletypeid,saletypename Ӫҵ����,deptid,deptname,cmonth �·�,dhours ֱ������Сʱ��,dcost ֱ�������ɱ�,inhours �������Сʱ��,incost ��������ɱ�,
	  EMS_HH_HOURS ������ʱ,
	  cast((isnull(dcost,0) + isnull(incost,0)) as decimal(18,5)) as Ԥ���ɱ�,
	  Ԥ����Сʱ�ɱ�= case when EMS_HH_HOURS is null or EMS_HH_HOURS = 0 then 0 else cast((isnull(dcost,0) + isnull(incost,0))/cast(EMS_HH_HOURS as decimal(18,5)) as decimal(18,5)) end, 
	  DEPT_STANDARD_POINT as ��׼����ɱ�, cast(DEPT_STANDARD_POINT*EMS_HH_HOURS as decimal(18,5)) ��׼�ɱ�,
	  cast(DEPT_STANDARD_POINT*EMS_HH_HOURS as decimal(18,5)) - cast((isnull(dcost,0) + isnull(incost,0)) as decimal(18,5)) ӯ�� 
	  from 
	  (select max(st.cid) saletypeid,max(st.cname) saletypename,t.cid deptid,max(t.cname) deptname,@cmonth cmonth,sum(d.chours) dhours,sum(d.cost) dcost,sum(i.chours) inhours,sum(i.cost) incost,
	  sum(h.HOURS) EMS_HH_HOURS,max(p.DEPT_STANDARD_POINT) as dept_standard_point from (select * from #temp_days t left join cost_dept dp on t.saletypeid = dp.SALETYPE_ID) t 
	  left join COST_EMS_HH_HOURS h on t.cdate = h.CDATE 
	  left join  #direct_dept_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid and t.cid = d.deptid 
	  left join #indirect_dept_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid  and t.cid = i.deptid 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select dept_id,DEPT_STANDARD_POINT from COST_DEPT_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on t.cid = p.DEPT_ID group by t.cid) t
  end
  if @saletypeid = 14 or @saletypeid = 15
  begin
	  insert into COST_DEPT_MONTH_CALCULATE(SALE_TYPE_ID,SALE_TYPE_NAME,DEPT_ID,DEPT_NAME,CMONTH,DIRECT_HOURS,DIRECT_COST,TEMP_HOURS,TEMP_COST,INDIRECT_HOURS,INDIRECT_COST,POINTCOUNT,COST,COST_POINT,STANDARD_POINT,STANDARD_COST,PROFIT) 
	  select saletypeid,saletypename Ӫҵ����,deptid,deptname,cmonth �·�,dhours ֱ������Сʱ��,dcost ֱ�������ɱ�,tdhours ��ʱ����ʱ,tdcost ��ʱ���ɱ�,inhours �������Сʱ��,incost ��������ɱ�,
	  PRODUCT_QUANTITY ����̨��,
	  cast((isnull(dcost2,0) + isnull(tdcost2,0) + isnull(incost2,0)) as decimal(18,5)) as Ԥ���ɱ�,
	  Ԥ����̨�ɱ�= case when PRODUCT_QUANTITY is null or PRODUCT_QUANTITY = 0 then 0 else cast((isnull(dcost2,0) + isnull(tdcost2,0) + isnull(incost2,0))/cast(PRODUCT_QUANTITY as decimal(18,5)) as decimal(18,5)) end, 
	  DEPT_STANDARD_POINT as ��׼��̨�ɱ�, cast(DEPT_STANDARD_POINT*PRODUCT_QUANTITY as decimal(18,5)) ��׼�ɱ�,
	  cast(DEPT_STANDARD_POINT*PRODUCT_QUANTITY as decimal(18,5)) - cast((isnull(dcost2,0) + isnull(tdcost2,0) + isnull(incost2,0)) as decimal(18,5)) ӯ�� 
	  from 
	  (select max(st.cid) saletypeid,max(st.cname) saletypename,t.cid deptid,max(t.cname) deptname,@cmonth cmonth,sum(d.chours) dhours,sum(cast(isnull(d.cost,0) as decimal(18,5))) dcost,sum(cast(isnull(d.cost,0)/cast(r.cost_rate as decimal(18,4)) as decimal(18,5))) dcost2,sum(isnull(td.chours,0)) tdhours,sum(ISNULL(td.cost,0)) tdcost,sum(ISNULL(td.cost2,0)) tdcost2,sum(i.chours) inhours,sum(cast(isnull(i.cost,0) as decimal(18,5))) incost,sum(cast(isnull(i.cost,0)/cast(r2.cost_rate as decimal(18,4)) as decimal(18,5))) incost2,
	  sum(case when @type = 1 then s.PRODUCT_QUANTITY*qr.RATE else s.PRODUCT_QUANTITY end) PRODUCT_QUANTITY,max(p.DEPT_STANDARD_POINT) as dept_standard_point from (select * from #temp_days t left join cost_dept dp on t.saletypeid = dp.SALETYPE_ID) t 
	  left join COST_PRODUCT_QUANTITY s on t.cdate = s.CDATE and s.type = @type 
	  left join (select rate from COST_QUANTITY_RATE where YYYY = left(@cmonth,4) and QUARTER_ID = @quarter) qr on 1=1 
	  left join  #direct_dept_sum d on t.cdate = d.cdate and t.saletypeid = d.saletypeid and t.cid = d.deptid 
	  left join (select COST_RATE,sale_type_id,dept_id from cost_rate where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and isnull(dept_id,0) >0) r on d.saletypeid = r.sale_type_id and d.deptid = r.dept_id 
	  left join #indirect_dept_sum i on t.CDATE = i.CDATE and t.saletypeid = i.saletypeid  and t.cid = i.deptid 
	  left join (select COST_RATE,sale_type_id,dept_id from cost_rate where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and isnull(dept_id,0) >0) r2 on i.saletypeid = r2.sale_type_id and i.deptid = r2.dept_id 
	  left join #temp_direct td on t.CDATE = td.CDATE and t.saletypeid = td.saletypeid and t.cid = td.dept_id 
	  left join COST_SALETYPE st on t.saletypeid = st.CID 
	  left join (select dept_id,DEPT_STANDARD_POINT from COST_DEPT_STANDARD_POINT where YYYYMM = @cmonth and SALE_TYPE_ID = @saletypeid) p on t.cid = p.DEPT_ID group by t.cid) t 
  end
--if @@error <> 0 
--goto ErrMsg

--ErrMsg:	
--rollback 
--raiserror 20000 '�ɱ�����ʧ�ܣ�'
--return

