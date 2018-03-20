EXEC  sp_addlinkedserver
@server='BARCODE',  
@srvproduct='',
@provider='SQLOLEDB',
@datasrc='192.168.0.176'  
GO
EXEC sp_addlinkedsrvlogin
'BARCODE',                 
'false', 
 NULL,
'costapp',                        
'hytera2017huopj'                  
GO
