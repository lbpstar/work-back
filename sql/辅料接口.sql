

--ÿ������ÿ��Ĳ��ϳɱ�
	declare @month varchar(10),@sql varchar(1000)
	set @month = '2018-03'
	create table #temp(outno nvarchar(50),requiredate date,dept nvarchar(50),line nvarchar(50),states nvarchar(20),materialcode nvarchar(50),materialname nvarchar(500),qty decimal(18,2),unit nvarchar(20))
	set @sql = 'insert into #temp  select * from  openquery(barcode,''select i.outno,i.requiredate,i.dept,i.line,i.states,e.materialcode,e.materialname,e.qty,e.unit from Warehouse_materialouthead i left join Warehouse_materialout e on i.outno = e.outno where i.states = ''''ͬ��''''  and i.requiredate like ''''' + @month + '%'''''')'  
	exec(@sql)
	select t.outno,t.requiredate,t.line,t.materialcode,t.materialname ,t.qty,t.unit,l.CNAME,s.CID,s.CNAME,d.MODULE_ID,d.SUB_ID,d.CNAME from #temp t left join COST_LINETYPE l on t.line = l.CNAME_MES
	left join COST_SALETYPE s on l.SALETYPE_ID = s.CID
	left join COST_BASE_DATA d on l.WORK_SHOP = d.SUB_ID and d.MODULE_ID = 3 where s.CNAME = 'EMS SMT'
	--drop table #temp
--���£�ÿ��Ӫҵ���͵��������
select CONVERT(varchar(100), cdate, 23) as cdate, saletypeid, sum(cast(�����ܵ��� as decimal(18, 2)))  pointcount from 
(select cdate,������,saletype, װ�������, �����ܵ��� =case when saletype = 'shl' and ������ like '%-ws' then cast(�����ܵ��� as decimal(18,2))*3.56 else �����ܵ��� end, saletypeid = case when(saletype = 'HCL' and װ������� not like '��HBTGר��%')  then 2 else 10 end from 
(select cdate,������,saletype, װ�������, �����ܵ��� from openquery (LINKERPNEW, 'select ���� as cdate,������, organization_code as saletype,װ�������,�����ܵ���  from CUX_SMT_PROD_RP where (װ�������  not like ''%F1ר��%'' and   װ������� not like ''%F1 TB%'') and to_char(����,''yyyy-mm'')=''2018-03''')) i 
) t 
group by cdate, saletypeid order by cdate

select * from OPENQUERY (BARCODE, 'SELECT * FROM latype')
select distinct linetype,saletypeid from 
(select CONVERT(varchar(100), cdate, 23) as cdate,saletype,װ�������,linetype,�����ܵ���,
saletypeid = case when װ�������  like '%F1ר��%' or   װ������� like '%F1 TB%' then 16 when (saletype = 'HCL' and װ������� not like '��HBTGר��%')  then 2 else 10 end 
from openquery (LINKERPNEW, 'select ���� as cdate,organization_code as saletype,װ�������, �����߱� as linetype,�����ܵ��� from CUX_SMT_PROD_RP where  to_char(����,''yyyy-mm'')=''2018-03''')) t 
where saletypeid in (10,16) order by linetype

exec COST_MATL_QUERY '2018-03'