EXEC  sp_addlinkedserver
@server='KAOQIN',   --���ӷ���������
@srvproduct='',
@provider='SQLOLEDB',
@datasrc='192.168.0.175'  --Ҫ���ʵĵ����ݿ����ڵķ�������ip
GO
EXEC sp_addlinkedsrvlogin
'KAOQIN',                  --���ӷ���������
'false', 
 NULL,
'scmuser',                     --Ҫ���ʵ����ݿ���û�              
'xyz123'                    --Ҫ���ʵ����ݿ⣬�û�������
GO





