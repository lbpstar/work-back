select * from COST_POINTCOUNT where CDATE like '2017-07-04%'
select i.CDATE,e.CNAME,i.POINTCOUNT from COST_POINTCOUNT_SUM i left join cost_saletype e on i.saletype_id =e.CID where CDATE  like '2017-07-04%'
select cdate, ������,saletype, װ�������, �����ܵ��� from openquery (LINKERPNEW, 'select ���� as cdate, ������,organization_code as saletype,װ�������,�����ܵ���  from CUX_SMT_PROD_RP where  (װ�������  not like ''%F1ר��%'' and   װ������� not like ''%F1 TB%'')   and to_char(����,''yyyy-mm-dd'')=''2017-07-04'' ')
select cdate, ������,saletype, װ�������, �����ܵ��� from openquery (LINKERPNEW, 'select ���� as cdate, ������,organization_code as saletype,װ�������,�����ܵ���  from CUX_SMT_PROD_RP where  (װ�������  not like ''%F1ר��%'' and   װ������� not like ''%F1 TB%'')   and to_char(����,''yyyy-mm-dd'')=''2017-07-04'' ') where ������ not like '%ws' and saletype = 'shl'

--Ӫҵ���͵�����ѯ
select CONVERT(varchar(100), cdate, 23) as cdate, saletypeid, sum(cast(�����ܵ��� as decimal(18, 2)))  pointcount from 
(select cdate, ������,saletype, װ�������, �����ܵ��� =case when saletype = 'shl' and ������ like '%-ws' then cast(�����ܵ��� as decimal(18,2))*3.56 else �����ܵ��� end, saletypeid = case when(saletype = 'HCL' AND CODE2 IS null and װ������� not like '��HBTGר��%') OR CODE2 = 'HCL' then 2 else 10 end from 
(select cdate, ������,saletype, װ�������, �����ܵ��� from openquery (LINKERPNEW, 'select ���� as cdate, ������,organization_code as saletype,װ�������,�����ܵ���  from CUX_SMT_PROD_RP where  (װ�������  not like ''%F1ר��%'' and   װ������� not like ''%F1 TB%'') and to_char(����,''yyyy-mm'')=''2017-07'' ')) i 
left join (select d.*, o.CODE, O2.CODE CODE2 from COST_SPECIAL_DEAL d left join cost_organization o on d.ORGANIZATION_ID = o.CID left join cost_organization o2 on d.TO_ORGANIZATION_ID = o2.CID where d.YYYYMM = '2017-07') j
on i.װ������� like '%' + j.TASK_NAME + '%' and i.saletype = j.CODE) t 
group by cdate, saletypeid order by cdate

set nocount on
declare @begindate varchar(20),@enddate varchar(20),@sql varchar(200),@cmonth varchar(20)
set @cmonth = '2017-07'
set @begindate = @cmonth + '-01'
--��ĩ
select @enddate = convert(varchar,dateadd(day,-day(@begindate),dateadd(month,1,@begindate)),23)
 --���ӷ�������
 --select * into #temp  from  openquery(barcode,'barcodenew.dbo.WorkERPinstock_query ''2017-07-01'',''2017-07-30'',''''') 
create table #temp(organization_code nvarchar(20),wip_entity_name nvarchar(50),item nvarchar(50),item_desc nvarchar(500),transaction_date datetime,transaction_quantity int,transaciton_uom nvarchar(20))
set @sql = 'insert into #temp  select * from  openquery(barcode,''barcodenew.dbo.WorkERPinstock_query ''''' + @begindate + ''''',''''' + @enddate + ''''','''''''''')'
exec(@sql)
select * from #temp where item like '10%' or item like 'x%'

--Ӫҵ���͵�����ѯ
select CONVERT(varchar(100), cdate, 23) as cdate, saletypeid, sum(cast(�����ܵ��� as decimal(18, 2)))  pointcount from 
(select cdate, ������,saletype, װ�������, �����ܵ��� =case when saletype = 'shl' and ������ like '%-ws' then cast(�����ܵ��� as decimal(18,2))*3.56 else �����ܵ��� end, saletypeid = case when(saletype = 'HCL' AND CODE2 IS null and װ������� not like '��HBTGר��%') OR CODE2 = 'HCL' then 2 else 10 end from 
(select cdate, ������,saletype, װ�������, �����ܵ��� from openquery (LINKERPNEW, 'select ���� as cdate, ������,organization_code as saletype,װ�������,�����ܵ���  from CUX_SMT_PROD_RP where  (װ�������  not like ''%F1ר��%'' and   װ������� not like ''%F1 TB%'') and to_char(����,''yyyy-mm'')=''2017-07'' ')) i 
left join (select d.*, o.CODE, O2.CODE CODE2 from COST_SPECIAL_DEAL d left join cost_organization o on d.ORGANIZATION_ID = o.CID left join cost_organization o2 on d.TO_ORGANIZATION_ID = o2.CID where d.YYYYMM = '2017-07') j
on i.װ������� like '%' + j.TASK_NAME + '%' and i.saletype = j.CODE) t 
group by cdate, saletypeid order by cdate

select cdate, ������,saletype, װ�������, �����ܵ���,�����ܵ���2 = case when saletype = 'shl' and ������ like '%-ws' then cast(�����ܵ��� as decimal(18,2))*3.56 else �����ܵ��� end, saletypeid = case when(saletype = 'HCL' AND CODE2 IS null and װ������� not like '��HBTGר��%') OR CODE2 = 'HCL' then 2 else 10 end from 
(select cdate,������, saletype, װ�������, �����ܵ��� from openquery (LINKERPNEW, 'select ���� as cdate, ������,organization_code as saletype,װ�������,�����ܵ���  from CUX_SMT_PROD_RP where  (װ�������  not like ''%F1ר��%'' and   װ������� not like ''%F1 TB%'') and to_char(����,''yyyy-mm'')=''2017-07'' ')) i 
left join (select d.*, o.CODE, O2.CODE CODE2 from COST_SPECIAL_DEAL d left join cost_organization o on d.ORGANIZATION_ID = o.CID left join cost_organization o2 on d.TO_ORGANIZATION_ID = o2.CID where d.YYYYMM = '2017-07') j
on i.װ������� like '%' + j.TASK_NAME + '%' and i.saletype = j.CODE


select cdate, saletype, װ�������, �����ܵ��� from openquery (LINKERPNEW, 'select ���� as cdate, organization_code as saletype,װ�������,�����ܵ���  from CUX_SMT_PROD_RP where  (װ�������  not like ''%F1ר��%'' and   װ������� not like ''%F1 TB%'' and  to_char(����,''yyyy-mm-dd'')=''2017-07-04'' ')

select * from openquery (LINKERPNEW, 'select *  from CUX_SMT_PROD_RP where  (װ�������  not like ''%F1ר��%'' and   װ������� not like ''%F1 TB%'') and to_char(����,''yyyy-mm'')=''2017-07'' ') where ������ like '%s' and organization_code = 'shl'