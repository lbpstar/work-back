select * from COST_DIRECT_LABOUR
select employee_id_ ����,name_ ����,DEPARTMENT_ ����,Ա���ȼ� = cast(isnull(e_band,'0') as int)  from OPENQUERY (BARCODE, 'SELECT employee_id_ ,name_ ,DEPARTMENT_ ,e_band FROM IHPS_ID_USER_PROFILE where department_ like ''��������%''')
select *  from OPENQUERY (BARCODE, 'SELECT employee_id_ ,name_ ,DEPARTMENT_ ,e_band FROM IHPS_ID_USER_PROFILE where e_band is null and department_ like ''��������%''')



--��Ӫҵ���Ͳ�ѯ����������ֱ���˹�������˹�
--select cdate ����,emp_no ����,emp_name ����,wkname ����,gz_int �����ϰ�,jb_ps_int ƽʱ�Ӱ�,jb_xx_int ��Ϣ�ռӰ�,jb_jr_int �ڼ��ռӰ�,t.dept4 ����,d.cname ��ϵͳ����,rank Ա����,PERSON_TYPE Ա������ 
select distinct EMP_NO,EMP_NAME,PERSON_TYPE 
from COST_DIRECT_LABOUR_ATTENDANCE t  
left join cost_dept_map m on  T.DEPT4 = M.DEPT4 
left join cost_dept d on m.dept_id = d.cid 
left join cost_saletype s on d.saletype_id = s.cid where t.cdate like '2017-09%'  and s.CNAME = '��ӪSMT' 
--ͳ��ѡ��Ӫҵ����ֱ���˹�������˹�����
select t.PERSON_TYPE,COUNT(*) from 
(select distinct EMP_NO,EMP_NAME,PERSON_TYPE 
from COST_DIRECT_LABOUR_ATTENDANCE t  
left join cost_dept_map m on  T.DEPT4 = M.DEPT4 
left join cost_dept d on m.dept_id = d.cid 
left join cost_saletype s on d.saletype_id = s.cid where t.cdate like '2017-09%'  and s.CNAME = '��ӪSMT' ) t group by t.PERSON_TYPE
--��Ӫҵ���Ͳ�ѯ��ʱ
select cdate ����,emp_no ����,emp_name ����,wkname ����,(gz_int + jb_ps_int + jb_xx_int + jb_jr_int) as ��ʱ,gz_int �����ϰ�,jb_ps_int ƽʱ�Ӱ�,jb_xx_int ��Ϣ�ռӰ�,jb_jr_int �ڼ��ռӰ�,t.dept4 ����,d.cname ��ϵͳ����,PERSON_TYPE Ա������ 
--select distinct EMP_NO,EMP_NAME,PERSON_TYPE 
from COST_DIRECT_LABOUR_ATTENDANCE t  
left join cost_dept_map m on  T.DEPT4 = M.DEPT4 
left join cost_dept d on m.dept_id = d.cid 
left join cost_saletype s on d.saletype_id = s.cid where t.cdate like '2017-11%'  and s.CNAME = '��ӪSMT'  and gz_int + jb_ps_int + jb_xx_int + jb_jr_int > 9 and PERSON_TYPE = '����˹�'
--��175�ϲ�ԭʼ��¼
--select * from kq_dayreport where  empno = '150313122' and CONVERT(varchar(100), rdate, 23) = '2017-08-07' order by rdate
--���´ӽӿ�ֱ�ӻ�ȡ���ݣ��а�Σ����°�ʱ��
declare @begindate varchar(20),@enddate varchar(20),@sql varchar(600),@cmonth varchar(20)
set @cmonth = '2017-12'
set @begindate = @cmonth + '-01'
--��ĩ
select @enddate = convert(varchar,dateadd(day,-day(@begindate),dateadd(month,1,@begindate)),23)
 --���ӷ�������,��Ա��Ϣ
create table #temp(emp_no nvarchar(20),dept nvarchar(200),rank nvarchar(50),eband nvarchar(50))
set @sql = 'insert into #temp  select * from  openquery(barcode,''select EMPLOYEE_ID_,DEPARTMENT_,rank_ ,e_band from barcodenew.dbo.IHPS_ID_USER_PROFILE where department_ like ''''��������%'''''')'
exec(@sql)
----�����ӷ�������,����Ҫʹ����ʱ��ֱ��ʹ��
--IHPS_ID_USER_PROFILE

--��������
--create table #temp2(rdate datetime,emp_no nvarchar(20),emp_name nvarchar(50),wkname nvarchar(50),bcname nvarchar(50),firsttime nvarchar(50),lasttime nvarchar(50),gz_int decimal(18,2),jb_ps_int decimal(18,2),jb_xx_int decimal(18,2),jb_jr_int decimal(18,2),ischeck bit)
--set @sql = 'insert into #temp2  select * from  OPENQUERY(KAOQIN,''Select rdate,empno,empname,wkname,bcname,firsttime,lasttime,cast(isnull(gz_int,0)/60.0 as decimal(18,2)),cast(isnull(jb_ps_int,0)/60.0 as decimal(18,2)),cast(isnull(jb_xx_int,0)/60.0 as decimal(18,2)),cast(isnull(jb_jr_int,0)/60.0 as decimal(18,2)),ischeck from Ecard_hytera.dbo.kq_dayreport where (isnull(gz_int,0)> 0 or isnull(jb_ps_int,0)>0 or isnull(jb_xx_int,0) >0 or isnull(jb_jr_int,0)>0) and CONVERT(varchar(100), rdate, 23)  like ''''' + @cmonth + '%'''''')'
create table #temp2(rdate datetime,emp_no nvarchar(20),emp_name nvarchar(50),wkname nvarchar(50),bcname nvarchar(100),firsttime nvarchar(50),lasttime varchar(50),gz_int decimal(18,2),jb_ps_int decimal(18,2),jb_xx_int decimal(18,2),jb_jr_int decimal(18,2),ischeck bit)
set @sql = 'insert into #temp2 select * from  OPENQUERY(KAOQIN,''Select rdate,empno,empname,wkname,bcname,firsttime,lasttime,cast(isnull(gz_int,0)/60.0 as decimal(18,2)),cast(isnull(jb_ps_int,0)/60.0 as decimal(18,2)),cast(isnull(jb_xx_int,0)/60.0 as decimal(18,2)),cast(isnull(jb_jr_int,0)/60.0 as decimal(18,2)),ischeck from Ecard_hytera.dbo.kq_dayreport where (isnull(gz_int,0)> 0 or isnull(jb_ps_int,0)>0 or isnull(jb_xx_int,0) >0 or isnull(jb_jr_int,0)>0) and CONVERT(varchar(100), rdate, 23)  like ''''' + @cmonth + '%'''''')'
exec(@sql)
--insert into COST_DIRECT_LABOUR_ATTENDANCE(cdate,emp_no,emp_name,wkname,gz_int,jb_ps_int,jb_xx_int,jb_jr_int,ischeck,dept4,rank,PERSON_TYPE,person_type_id) 
select k.*,dept4 = case when len(replace(dept,'\','--'))-len(dept) < 4 then dept else left(dept,charindex('\',dept,charindex('\',dept,charindex('\',dept,CHARINDEX('\',dept,1)+1)+1)+1)-1) end,
rank,person_type = case when cast(eband as int) <=4 or (ISNULL(rank,'0') = '0' and ISNULL(eband,'0')='0') or (RANK = '������' and ISNULL(eband,'0')='0') then 'ֱ���˹�' else '����˹�' end,person_type_id = case when cast(eband as int) <=4 or (ISNULL(rank,'0') = '0' and ISNULL(eband,'0')='0') or (RANK = '������' and ISNULL(eband,'0')='0') then 3 else 4 end 
into #temp3 from #temp2 k join #temp e on k.emp_no = e.emp_no 

select CONVERT(varchar(100), t.rdate, 23) ����,emp_no ����,emp_name ����,wkname ����,bcname ���,isnull(firsttime,'') �ϰ�ʱ��,isnull(lasttime,'') �°�ʱ��,(gz_int + jb_ps_int + jb_xx_int + jb_jr_int) as ��ʱ,gz_int �����ϰ�,jb_ps_int ƽʱ�Ӱ�,jb_xx_int ��Ϣ�ռӰ�,jb_jr_int �ڼ��ռӰ�,t.dept4 ����,d.cname ��ϵͳ����,PERSON_TYPE Ա������ 
--select distinct EMP_NO,EMP_NAME,PERSON_TYPE 
from #temp3 t  
left join cost_dept_map m on  T.DEPT4 = M.DEPT4 
left join cost_dept d on m.dept_id = d.cid 
left join cost_saletype s on d.saletype_id = s.cid 
where CONVERT(varchar(100), t.rdate, 23) like @cmonth + '%'  and s.CNAME = '��ӪSMT'  and gz_int + jb_ps_int + jb_xx_int + jb_jr_int > 9 and PERSON_TYPE = '����˹�' 
--and bcname like '%10:00-19:00%'
drop table #temp
drop table #temp2
drop table #temp3
