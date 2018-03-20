--开启链接服务器的rpc选项，直接在属性界面修改，提示“不允许对系统目录进行即席更新”，直接通过下面代码执行成功
exec sp_serveroption @server='barcode' , @optname= 'rpc', @optvalue ='TRUE'  
exec sp_serveroption @server='barcode' , @optname= 'rpc out', @optvalue='TRUE' 
--执行存储过程
exec barcode.barcodenew.dbo.WorkERPinstock_query '2017-07-01','2017-07-30',''

--create table #temp(organization_code nvarchar(20),wip_entity_name nvarchar(50),item nvarchar(50),item_desc nvarchar(500),transaciton_date datetime,transaciton_quantity int,transaciton_uom nvarchar(20))
----调用了dblink后，下面这样的insert语句有问题，反之正常。服务器 'L96264' 上的 MSDTC 不可用。http://www.cnblogs.com/lyl6796910/p/3622473.html
--insert into #temp exec barcode.barcodenew.dbo.WorkERPinstock_query '2017-06-01','2017-06-30',''
--select * from (exec barcode.barcodenew.dbo.WorkERPinstock_query '2017-06-01','2017-06-30','')

--两个版本，有链接服务器和本服务器的，可以快速切换
select * into #temp  from  openquery(barcode,'barcodenew.dbo.WorkERPinstock_query ''2017-07-01'',''2017-07-30'',''''') 
select CONVERT(varchar(100), transaction_date, 23) cdate,SUM(transaction_quantity) quantity from #temp where item like '10%' or item like 'x%' group by CONVERT(varchar(100), transaction_date, 23) order by CONVERT(varchar(100), transaction_date, 23)
drop table #temp

