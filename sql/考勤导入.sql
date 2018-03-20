--考勤，就实时查询，不保存数据
--Select rdate,empno,empname,deptname,Gz_int as work_hours,Jb_ps_int as overtime,Jb_xx_int as overtime2,Jb_jr_int as overtime3 from OpenQuery(KAOQIN,'Select * from kq_dayreport')

declare @cmonth varchar(20),@date datetime,@sql nvarchar(1000),@empno varchar(20)
create table #temp(rdate datetime,empno nvarchar(20),empname nvarchar(50),deptname nvarchar(50),work_hours decimal(18,2),overtime decimal(18,2),overtime2 decimal(18,2),overtime3 decimal(18,2))

if @cmonth <> '' and @date is null
set @sql = 'insert into #temp SELECT rdate,empno,empname,deptname,Gz_int as work_hours,Jb_ps_int as overtime,Jb_xx_int as overtime2,Jb_jr_int as overtime3 FROM OPENQUERY(KAOQIN,''Select * from kq_dayreport where rdate like ''''' + @cmonth + '%'''''')'
if @date is not null
--where语句中date格式可能需要转换一下
set @sql = 'insert into #temp SELECT rdate,empno,empname,deptname,Gz_int as work_hours,Jb_ps_int as overtime,Jb_xx_int as overtime2,Jb_jr_int as overtime3 FROM OPENQUERY(KAOQIN,''Select * from kq_dayreport where rdate = ''''' + @date + ''''''')'
else
return
exec(@sql)
select t.rdate,t.empno,t.empname,m.dept_id,t.deptname,t.work_hours,work_type_id = case when overtime2 = 0 and overtime3 = 0 then 1 when overtime2 >0 then 4 when overtime3>0 then 5 end from #temp t 
 left join cost_dept_map m on t.deptname = m.attendance_dept_name 
drop table #temp



--获取考勤数据
declare @sql nvarchar(1000),@cmonth varchar(20),@saletypeid int
set @cmonth = '2017-07'
set @saletypeid = 10
create table #attendance(rdate datetime,empno nvarchar(20),empname nvarchar(50),wkname nvarchar(50),gz_int decimal(18,2),jb_ps_int decimal(18,2),jb_xx_int decimal(18,2),jb_jr_int decimal(18,2),ischeck bit,mjmark nvarchar(50),dcmark nvarchar(50),bcol1 nvarchar(50))
set @sql = 'insert into #attendance SELECT * FROM OPENQUERY(KAOQIN,''Select rdate,r.empno,r.empname,wkname,cast(isnull(gz_int,0)/60 as decimal(18,2)),cast(isnull(jb_ps_int,0)/60 as decimal(18,2)),cast(isnull(jb_xx_int,0)/60 as decimal(18,2)),cast(isnull(jb_jr_int,0)/60 as decimal(18,2)),ischeck,e.mjmark,e.dcmark,e.bcol1 from kq_dayreport r left join hr_employee e on r.empno = e.empno where (isnull(gz_int,0)> 0 or isnull(jb_ps_int,0)>0 or isnull(jb_xx_int,0) >0 or isnull(jb_jr_int,0)>0) and  e.mjmark = ''''制造中心'''' and CONVERT(varchar(100), r.rdate, 20) like ''''' + @cmonth + '%'''''')'
exec(@sql)
--select t.rdate,t.empno,t.empname,m.dept_id,t.deptname,t.work_hours,work_type_id = case when overtime2 = 0 and overtime3 = 0 then 1 when overtime2 >0 then 4 when overtime3>0 then 5 end from #attendance t 
-- left join cost_dept_map m on t.deptname = m.attendance_dept_name 
--select * from #attendance
--drop table #attendance
--drop table #direct

select a.rDATE ,max(s.cid) saletypeid,max(s.cname) saletypename,max(d.cid) deptid,max(d.cname) deptname,MAX(a.empname) empname,MAX(w.CNAME) worktypename,SUM(a.gz_int + a.jb_ps_int + a.jb_xx_int + a.jb_jr_int) chours,
cost = case when  w.cid = 1 then SUM(a.gz_int)*max(p.PRICE) 
when w.cid = 3 then SUM(a.jb_ps_int)*max(p.PRICE) +SUM(a.gz_int)*max(p2.PRICE) when w.cid = 4 then SUM(a.jb_xx_int)*max(p.PRICE) +SUM(a.gz_int)*max(p2.PRICE) when w.CID = 5 then SUM(a.jb_jr_int)*max(p.PRICE) +SUM(a.gz_int)*max(p2.PRICE) end into #direct
from 
(select t.rdate,t.empno,t.empname,m.dept_id,t.gz_int,t.jb_ps_int,t.jb_xx_int,t.jb_jr_int,work_type_id = case when t.jb_ps_int = 0 and t.jb_xx_int = 0 and t.jb_jr_int = 0 then 1 when t.jb_ps_int >0 then 3 when t.jb_xx_int>0 then 4 when t.jb_jr_int >0 then 5 end from #attendance t 
 left join cost_dept_map m on t.dcmark = m.ATTENDANCE_DEPT_NAME and t.bcol1 = m.ATTENDANCE_DEPT_3RD) 
 a 
left join COST_WORK_TYPE w on a.WORK_TYPE_ID = w.CID 
left join cost_dept d on a.dept_id = d.cid 
left join COST_SALETYPE s on d.saletype_id = s.cid 
left join (select * from COST_DIRECT_LABOUR_PRICE where YYYYMM = @cmonth) p on w.CID = p.WORK_TYPE 
cross join (select PRICE from COST_DIRECT_LABOUR_PRICE where YYYYMM = @cmonth and WORK_TYPE = 1) p2 
group by a.rDATE,a.empno,w.cid
select * from #direct
select * from #attendance where empname = '王超红'
select cast(chours/60 as decimal(18,2)) from #direct

