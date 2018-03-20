DECLARE @Sql VARCHAR(1000)
declare @startime datetime,@finishtime datetime
select @startime = '2016-12-01',@finishtime = '2016-12-31'
SET @Sql = 'select 日期 as workdate, sum(生产总点数) as pointcount,生产线别 as latyple from 
                             CUX_SMT_PROD_RP where 工单类型=''正常'' and 日期>=to_date(''' +   CONVERT(varchar(100), @startime, 23) + ''',''yyyy-mm-dd'') and 日期<=to_date(''' + CONVERT(varchar(100), @startime, 23) + ''',''yyyy-mm-dd'') group by 日期,生产线别 order by 日期'
SET @Sql = 'SELECT pointcount,convert(char(10),workdate,120) as workdate,latyple FROM OPENQUERY(LINKERP, ''' + REPLACE(@Sql, '''', '''''') + ''')'
--select @sql
EXEC(@Sql)

select CONVERT(varchar(100), cdate, 23) as cdate,linetype,pointcount from openquery
                             (LINKERP,'select 日期 as cdate, 生产线别 as linetype,sum(生产总点数) as pointcount from 
                             CUX_SMT_PROD_RP where 工单类型=''正常'' and 日期>=to_date(''2016-12-01'',''yyyy-mm-dd'') and 日期<=to_date(''2016-12-31'',''yyyy-mm-dd'') group by 日期,生产线别 order by 日期')

