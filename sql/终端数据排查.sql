select * into #temp  from  openquery(barcode,'barcodenew.dbo.WorkERPinstock_query ''2017-07-01'',''2017-07-30'',''''') 
select * from #temp where item like '10%' or item like 'x%' order by CONVERT(varchar(100), transaction_date, 23)
select * from #temp where item like '10%' or item like 'x%' and CONVERT(varchar(100), transaction_date, 23) = '2017-07-22'
select CONVERT(varchar(100), transaction_date, 23) cdate,type = 1,SUM(transaction_quantity) quantity from #temp where item like '10%' or item like 'x%' group by CONVERT(varchar(100), transaction_date, 23) order by CONVERT(varchar(100), transaction_date, 23)
select CONVERT(varchar(100), transaction_date, 23) cdate,type = 1,SUM(transaction_quantity) quantity from #temp where (item like '10%' or item like 'x%') and ISNUMERIC(left(wip_entity_name,1)) =1 group by CONVERT(varchar(100), transaction_date, 23) order by CONVERT(varchar(100), transaction_date, 23)

--select distinct item from #temp where item like '10%' or item like 'x%'
drop table #temp