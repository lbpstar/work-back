alter procedure COST_MATL_QUERY
@cmonth varchar(20)
as
set nocount on

declare @sql varchar(1000)
create table #temp(outno nvarchar(50),requiredate date,dept nvarchar(50),line nvarchar(50),states nvarchar(20),materialcode nvarchar(50),materialname nvarchar(500),qty decimal(18,2),unit nvarchar(20))
set @sql = 'insert into #temp  select * from  openquery(barcode,''select i.outno,i.requiredate,i.dept,i.line,i.states,e.materialcode,e.materialname,e.qty,e.unit from Warehouse_materialouthead i left join Warehouse_materialout e on i.outno = e.outno where i.states = ''''同意''''  and i.requiredate like ''''' + @cmonth + '%'''''')'  
exec(@sql)
select t.outno 单号,t.requiredate 日期,t.line MES线体,t.materialcode 料号,t.materialname 物料名称 ,t.qty 数量,t.unit 单位,l.CNAME ERP线体,s.CID,s.CNAME 营业类型,d.MODULE_ID,d.SUB_ID,d.CNAME 车间 from #temp t left join COST_LINETYPE l on t.line = l.CNAME_MES
left join COST_SALETYPE s on l.SALETYPE_ID = s.CID
left join COST_BASE_DATA d on l.WORK_SHOP = d.SUB_ID and d.MODULE_ID = 3

