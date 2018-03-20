EXEC  sp_addlinkedserver
@server='KAOQIN',   --链接服务器别名
@srvproduct='',
@provider='SQLOLEDB',
@datasrc='192.168.0.175'  --要访问的的数据库所在的服务器的ip
GO
EXEC sp_addlinkedsrvlogin
'KAOQIN',                  --链接服务器别名
'false', 
 NULL,
'scmuser',                     --要访问的数据库的用户              
'xyz123'                    --要访问的数据库，用户的密码
GO





