
declare @days int,@quarter int,@cdate varchar(20),@type int,@cmonth varchar(20),@saletypeid int
set @cmonth = '2017-08'
set @saletypeid = 2
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
select * from #direct
select distinct empname from #direct
select SUM(chours) from #direct
select * from #direct where CDATE = '2017-08-7'

select t.cdate,s.CNAME, t.emp_no,t.emp_name,t.person_type_id,m.dept_id,t.gz_int,t.jb_ps_int,t.jb_xx_int,t.jb_jr_int,work_type_id = case when t.jb_ps_int = 0 and t.jb_xx_int = 0 and t.jb_jr_int = 0 then 1 when t.jb_ps_int >0 then 3 when t.jb_xx_int>0 then 4 when t.jb_jr_int >0 then 5 end from COST_DIRECT_LABOUR_ATTENDANCE t 
 left join cost_dept_map m on  T.DEPT4 = M.DEPT4 left join cost_dept d on m.dept_id = d.cid  left join COST_SALETYPE s on d.saletype_id = s.cid  where t.CDATE = '2017-08-2' and person_type_id = 3 and s.CID = 2
select t.cdate,s.CNAME, t.emp_no,t.emp_name,t.person_type_id,m.dept_id,t.gz_int,t.jb_ps_int,t.jb_xx_int,t.jb_jr_int,work_type_id = case when t.jb_ps_int = 0 and t.jb_xx_int = 0 and t.jb_jr_int = 0 then 1 when t.jb_ps_int >0 then 3 when t.jb_xx_int>0 then 4 when t.jb_jr_int >0 then 5 end from COST_DIRECT_LABOUR_ATTENDANCE t 
 left join cost_dept_map m on  T.DEPT4 = M.DEPT4 left join cost_dept d on m.dept_id = d.cid  left join COST_SALETYPE s on d.saletype_id = s.cid  where t.CDATE = '2017-08-2' and person_type_id = 3 
 --2017-08-02部门映射后，部门为空的考勤记录，也就是还没有进行部门映射的
 select t.cdate,s.CNAME, t.emp_no,t.emp_name,T.DEPT4,t.person_type_id,m.dept_id,t.gz_int,t.jb_ps_int,t.jb_xx_int,t.jb_jr_int,work_type_id = case when t.jb_ps_int = 0 and t.jb_xx_int = 0 and t.jb_jr_int = 0 then 1 when t.jb_ps_int >0 then 3 when t.jb_xx_int>0 then 4 when t.jb_jr_int >0 then 5 end from COST_DIRECT_LABOUR_ATTENDANCE t 
 left join cost_dept_map m on  T.DEPT4 = M.DEPT4 left join cost_dept d on m.dept_id = d.cid  left join COST_SALETYPE s on d.saletype_id = s.cid  where t.CDATE = '2017-08-2' and d.cid is null
  --8月没有进行部门映射的部门
  select distinct T.DEPT4 from COST_DIRECT_LABOUR_ATTENDANCE t 
 left join cost_dept_map m on  T.DEPT4 = M.DEPT4 left join cost_dept d on m.dept_id = d.cid  left join COST_SALETYPE s on d.saletype_id = s.cid  where t.CDATE like  '2017-08%' and d.cid is null
    --8月没有进行部门映射的人员
  select distinct t.emp_no,t.emp_name,T.DEPT4 from COST_DIRECT_LABOUR_ATTENDANCE t 
 left join cost_dept_map m on  T.DEPT4 = M.DEPT4 left join cost_dept d on m.dept_id = d.cid  left join COST_SALETYPE s on d.saletype_id = s.cid  where t.CDATE like  '2017-08%' and d.cid is null
 select t.cdate,s.CNAME, t.emp_no,t.emp_name,t.person_type_id,m.dept_id,t.gz_int,t.jb_ps_int,t.jb_xx_int,t.jb_jr_int,work_type_id = case when t.jb_ps_int = 0 and t.jb_xx_int = 0 and t.jb_jr_int = 0 then 1 when t.jb_ps_int >0 then 3 when t.jb_xx_int>0 then 4 when t.jb_jr_int >0 then 5 end from COST_DIRECT_LABOUR_ATTENDANCE t 
 left join cost_dept_map m on  T.DEPT4 = M.DEPT4 left join cost_dept d on m.dept_id = d.cid  left join COST_SALETYPE s on d.saletype_id = s.cid  where t.CDATE = '2017-08-2'  and s.CID = 2
 select distinct t.EMP_NO from COST_DIRECT_LABOUR_ATTENDANCE t 
 left join cost_dept_map m on  T.DEPT4 = M.DEPT4 left join cost_dept d on m.dept_id = d.cid  left join COST_SALETYPE s on d.saletype_id = s.cid  where t.CDATE like '2017-08%'  and s.CID = 2


select cdate,saletypeid,max(saletypename) saletypename,sum(chours) chours,sum(cost) cost into #direct_sum from #direct group by cdate,saletypeid
select cdate,saletypeid,deptid,max(deptname) deptname,max(saletypename) saletypename,sum(chours) chours,sum(cost) cost into #direct_dept_sum from #direct group by cdate,saletypeid,deptid

drop table #direct

--部门映射查询
select * from cost_dept_map