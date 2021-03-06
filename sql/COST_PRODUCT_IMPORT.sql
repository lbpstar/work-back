
ALTER procedure [dbo].[COST_PRODUCT_IMPORT]
@cmonth varchar(20),
@type int
as
set nocount on
declare @begindate varchar(20),@enddate varchar(20),@sql varchar(200)
--set @cmonth = '2017-05'
set @begindate = @cmonth + '-01'
--月末
select @enddate = convert(varchar,dateadd(day,-day(@begindate),dateadd(month,1,@begindate)),23)
 --链接服务器版
 --select * into #temp  from  openquery(barcode,'barcodenew.dbo.WorkERPinstock_query ''2017-07-01'',''2017-07-30'',''''') 
create table #temp(organization_code nvarchar(20),wip_entity_name nvarchar(50),item nvarchar(50),item_desc nvarchar(500),transaction_date datetime,transaction_quantity int,transaciton_uom nvarchar(20))
set @sql = 'insert into #temp  select * from  openquery(barcode,''barcodenew.dbo.WorkERPinstock_query ''''' + @begindate + ''''',''''' + @enddate + ''''','''''''''')'
exec(@sql)
--终端台数
if @type = 1 
begin
	insert into cost_product_quantity(CDATE,TYPE,PRODUCT_QUANTITY) select CONVERT(varchar(100), transaction_date, 23) cdate,type = 1,SUM(transaction_quantity) quantity from #temp where (item like '10%' or item like 'x%') and ISNUMERIC(left(wip_entity_name,1)) =1  group by CONVERT(varchar(100), transaction_date, 23) order by CONVERT(varchar(100), transaction_date, 23)
end
--hh工时
if @type = 2
begin
	insert into COST_EMS_HH_HOURS(CDATE,hours) 
	select cdate,SUM(HOURS) HOURS from (select CONVERT(varchar(100), transaction_date, 23) cdate,transaction_quantity*STANDARD_HOURS HOURS from #temp i join (select * from COST_HH_STANDARD_HOURS where cmonth = @cmonth) j on ltrim(rtrim(i.item)) = ltrim(rtrim(j.MAT_CODE)) where organization_code = 'SHL') i group by cdate order by cdate
end
drop table #temp