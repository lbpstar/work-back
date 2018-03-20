select * from COST_DIRECT_LABOUR
select employee_id_ 工号,name_ 姓名,DEPARTMENT_ 部门,员工等级 = cast(isnull(e_band,'0') as int)  from OPENQUERY (BARCODE, 'SELECT employee_id_ ,name_ ,DEPARTMENT_ ,e_band FROM IHPS_ID_USER_PROFILE where department_ like ''制造中心%''')
select *  from OPENQUERY (BARCODE, 'SELECT employee_id_ ,name_ ,DEPARTMENT_ ,e_band FROM IHPS_ID_USER_PROFILE where e_band is null and department_ like ''制造中心%''')



--按营业类型查询考勤数据中直接人工、间接人工
--select cdate 日期,emp_no 工号,emp_name 姓名,wkname 星期,gz_int 正常上班,jb_ps_int 平时加班,jb_xx_int 休息日加班,jb_jr_int 节假日加班,t.dept4 部门,d.cname 本系统部门,rank 员工组,PERSON_TYPE 员工类型 
select distinct EMP_NO,EMP_NAME,PERSON_TYPE 
from COST_DIRECT_LABOUR_ATTENDANCE t  
left join cost_dept_map m on  T.DEPT4 = M.DEPT4 
left join cost_dept d on m.dept_id = d.cid 
left join cost_saletype s on d.saletype_id = s.cid where t.cdate like '2017-09%'  and s.CNAME = '主营SMT' 
--统计选定营业类型直接人工、间接人工数量
select t.PERSON_TYPE,COUNT(*) from 
(select distinct EMP_NO,EMP_NAME,PERSON_TYPE 
from COST_DIRECT_LABOUR_ATTENDANCE t  
left join cost_dept_map m on  T.DEPT4 = M.DEPT4 
left join cost_dept d on m.dept_id = d.cid 
left join cost_saletype s on d.saletype_id = s.cid where t.cdate like '2017-09%'  and s.CNAME = '主营SMT' ) t group by t.PERSON_TYPE
--按营业类型查询工时
select cdate 日期,emp_no 工号,emp_name 姓名,wkname 星期,(gz_int + jb_ps_int + jb_xx_int + jb_jr_int) as 工时,gz_int 正常上班,jb_ps_int 平时加班,jb_xx_int 休息日加班,jb_jr_int 节假日加班,t.dept4 部门,d.cname 本系统部门,PERSON_TYPE 员工类型 
--select distinct EMP_NO,EMP_NAME,PERSON_TYPE 
from COST_DIRECT_LABOUR_ATTENDANCE t  
left join cost_dept_map m on  T.DEPT4 = M.DEPT4 
left join cost_dept d on m.dept_id = d.cid 
left join cost_saletype s on d.saletype_id = s.cid where t.cdate like '2017-11%'  and s.CNAME = '主营SMT'  and gz_int + jb_ps_int + jb_xx_int + jb_jr_int > 9 and PERSON_TYPE = '间接人工'
--在175上查原始记录
--select * from kq_dayreport where  empno = '150313122' and CONVERT(varchar(100), rdate, 23) = '2017-08-07' order by rdate
--以下从接口直接获取数据，有班次，上下班时间
declare @begindate varchar(20),@enddate varchar(20),@sql varchar(600),@cmonth varchar(20)
set @cmonth = '2017-12'
set @begindate = @cmonth + '-01'
--月末
select @enddate = convert(varchar,dateadd(day,-day(@begindate),dateadd(month,1,@begindate)),23)
 --链接服务器版,人员信息
create table #temp(emp_no nvarchar(20),dept nvarchar(200),rank nvarchar(50),eband nvarchar(50))
set @sql = 'insert into #temp  select * from  openquery(barcode,''select EMPLOYEE_ID_,DEPARTMENT_,rank_ ,e_band from barcodenew.dbo.IHPS_ID_USER_PROFILE where department_ like ''''制造中心%'''''')'
exec(@sql)
----非链接服务器版,无需要使用临时表，直接使用
--IHPS_ID_USER_PROFILE

--考勤数据
--create table #temp2(rdate datetime,emp_no nvarchar(20),emp_name nvarchar(50),wkname nvarchar(50),bcname nvarchar(50),firsttime nvarchar(50),lasttime nvarchar(50),gz_int decimal(18,2),jb_ps_int decimal(18,2),jb_xx_int decimal(18,2),jb_jr_int decimal(18,2),ischeck bit)
--set @sql = 'insert into #temp2  select * from  OPENQUERY(KAOQIN,''Select rdate,empno,empname,wkname,bcname,firsttime,lasttime,cast(isnull(gz_int,0)/60.0 as decimal(18,2)),cast(isnull(jb_ps_int,0)/60.0 as decimal(18,2)),cast(isnull(jb_xx_int,0)/60.0 as decimal(18,2)),cast(isnull(jb_jr_int,0)/60.0 as decimal(18,2)),ischeck from Ecard_hytera.dbo.kq_dayreport where (isnull(gz_int,0)> 0 or isnull(jb_ps_int,0)>0 or isnull(jb_xx_int,0) >0 or isnull(jb_jr_int,0)>0) and CONVERT(varchar(100), rdate, 23)  like ''''' + @cmonth + '%'''''')'
create table #temp2(rdate datetime,emp_no nvarchar(20),emp_name nvarchar(50),wkname nvarchar(50),bcname nvarchar(100),firsttime nvarchar(50),lasttime varchar(50),gz_int decimal(18,2),jb_ps_int decimal(18,2),jb_xx_int decimal(18,2),jb_jr_int decimal(18,2),ischeck bit)
set @sql = 'insert into #temp2 select * from  OPENQUERY(KAOQIN,''Select rdate,empno,empname,wkname,bcname,firsttime,lasttime,cast(isnull(gz_int,0)/60.0 as decimal(18,2)),cast(isnull(jb_ps_int,0)/60.0 as decimal(18,2)),cast(isnull(jb_xx_int,0)/60.0 as decimal(18,2)),cast(isnull(jb_jr_int,0)/60.0 as decimal(18,2)),ischeck from Ecard_hytera.dbo.kq_dayreport where (isnull(gz_int,0)> 0 or isnull(jb_ps_int,0)>0 or isnull(jb_xx_int,0) >0 or isnull(jb_jr_int,0)>0) and CONVERT(varchar(100), rdate, 23)  like ''''' + @cmonth + '%'''''')'
exec(@sql)
--insert into COST_DIRECT_LABOUR_ATTENDANCE(cdate,emp_no,emp_name,wkname,gz_int,jb_ps_int,jb_xx_int,jb_jr_int,ischeck,dept4,rank,PERSON_TYPE,person_type_id) 
select k.*,dept4 = case when len(replace(dept,'\','--'))-len(dept) < 4 then dept else left(dept,charindex('\',dept,charindex('\',dept,charindex('\',dept,CHARINDEX('\',dept,1)+1)+1)+1)-1) end,
rank,person_type = case when cast(eband as int) <=4 or (ISNULL(rank,'0') = '0' and ISNULL(eband,'0')='0') or (RANK = '操作族' and ISNULL(eband,'0')='0') then '直接人工' else '间接人工' end,person_type_id = case when cast(eband as int) <=4 or (ISNULL(rank,'0') = '0' and ISNULL(eband,'0')='0') or (RANK = '操作族' and ISNULL(eband,'0')='0') then 3 else 4 end 
into #temp3 from #temp2 k join #temp e on k.emp_no = e.emp_no 

select CONVERT(varchar(100), t.rdate, 23) 日期,emp_no 工号,emp_name 姓名,wkname 星期,bcname 班次,isnull(firsttime,'') 上班时间,isnull(lasttime,'') 下班时间,(gz_int + jb_ps_int + jb_xx_int + jb_jr_int) as 工时,gz_int 正常上班,jb_ps_int 平时加班,jb_xx_int 休息日加班,jb_jr_int 节假日加班,t.dept4 部门,d.cname 本系统部门,PERSON_TYPE 员工类型 
--select distinct EMP_NO,EMP_NAME,PERSON_TYPE 
from #temp3 t  
left join cost_dept_map m on  T.DEPT4 = M.DEPT4 
left join cost_dept d on m.dept_id = d.cid 
left join cost_saletype s on d.saletype_id = s.cid 
where CONVERT(varchar(100), t.rdate, 23) like @cmonth + '%'  and s.CNAME = '主营SMT'  and gz_int + jb_ps_int + jb_xx_int + jb_jr_int > 9 and PERSON_TYPE = '间接人工' 
--and bcname like '%10:00-19:00%'
drop table #temp
drop table #temp2
drop table #temp3
