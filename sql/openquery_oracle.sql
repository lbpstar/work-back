DECLARE @Sql VARCHAR(1000)
declare @startime datetime,@finishtime datetime
select @startime = '2016-12-01',@finishtime = '2016-12-31'
SET @Sql = 'select ���� as workdate, sum(�����ܵ���) as pointcount,�����߱� as latyple from 
                             CUX_SMT_PROD_RP where ��������=''����'' and ����>=to_date(''' +   CONVERT(varchar(100), @startime, 23) + ''',''yyyy-mm-dd'') and ����<=to_date(''' + CONVERT(varchar(100), @startime, 23) + ''',''yyyy-mm-dd'') group by ����,�����߱� order by ����'
SET @Sql = 'SELECT pointcount,convert(char(10),workdate,120) as workdate,latyple FROM OPENQUERY(LINKERP, ''' + REPLACE(@Sql, '''', '''''') + ''')'
--select @sql
EXEC(@Sql)

select CONVERT(varchar(100), cdate, 23) as cdate,linetype,pointcount from openquery
                             (LINKERP,'select ���� as cdate, �����߱� as linetype,sum(�����ܵ���) as pointcount from 
                             CUX_SMT_PROD_RP where ��������=''����'' and ����>=to_date(''2016-12-01'',''yyyy-mm-dd'') and ����<=to_date(''2016-12-31'',''yyyy-mm-dd'') group by ����,�����߱� order by ����')

