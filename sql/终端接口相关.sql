--�������ӷ�������rpcѡ�ֱ�������Խ����޸ģ���ʾ���������ϵͳĿ¼���м�ϯ���¡���ֱ��ͨ���������ִ�гɹ�
exec sp_serveroption @server='barcode' , @optname= 'rpc', @optvalue ='TRUE'  
exec sp_serveroption @server='barcode' , @optname= 'rpc out', @optvalue='TRUE' 
--ִ�д洢����
exec barcode.barcodenew.dbo.WorkERPinstock_query '2017-07-01','2017-07-30',''

--create table #temp(organization_code nvarchar(20),wip_entity_name nvarchar(50),item nvarchar(50),item_desc nvarchar(500),transaciton_date datetime,transaciton_quantity int,transaciton_uom nvarchar(20))
----������dblink������������insert��������⣬��֮������������ 'L96264' �ϵ� MSDTC �����á�http://www.cnblogs.com/lyl6796910/p/3622473.html
--insert into #temp exec barcode.barcodenew.dbo.WorkERPinstock_query '2017-06-01','2017-06-30',''
--select * from (exec barcode.barcodenew.dbo.WorkERPinstock_query '2017-06-01','2017-06-30','')

--�����汾�������ӷ������ͱ��������ģ����Կ����л�
select * into #temp  from  openquery(barcode,'barcodenew.dbo.WorkERPinstock_query ''2017-07-01'',''2017-07-30'',''''') 
select CONVERT(varchar(100), transaction_date, 23) cdate,SUM(transaction_quantity) quantity from #temp where item like '10%' or item like 'x%' group by CONVERT(varchar(100), transaction_date, 23) order by CONVERT(varchar(100), transaction_date, 23)
drop table #temp

