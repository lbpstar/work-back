
declare @startime datetime,@finishtime datetime
select @startime = '2016-12-01',@finishtime = '2016-12-31'
select A.workdate as 时间,isnull(生产点数,0) as 生产点数,
                              (case when isnull(生产点数,0)*isnull(成本比例,0)=0 then 0 
                               else isnull(((isnull(折旧费,0) +isnull(直接人工,0)+isnull(间接人工,0))/成本比例/生产点数),0) end) as 预估单点成本,
                             (case when isnull(成本比例,0)=0 then 0 
                               else isnull(((isnull(折旧费,0) +isnull(直接人工,0)+isnull(间接人工,0))/成本比例),0) end) as 预估成本,
                              isnull(单点标准成本,0) as 单点标准成本,isnull(折旧费,0) as 折旧费,
                              isnull(直接人工,0) as 直接人工,isnull(间接人工,0) as 间接人工,isnull(直接材料消耗,0) as 直接材料消耗,isnull(其它费用,0) as 其它费用,
                              isnull(直接人力数,0) as 直接人力数
                              from
                           (select isnull(depreciation,0) as 折旧费,workdate,isnull(pointcost,0) as 单点标准成本,
                              isnull(material,0) as 直接材料消耗,isnull(othercost,0) as 其它费用,isnull(costper,0) as 成本比例
                              from SMT_materialcost where typle='主营SMT' and workdate>= @startime  and workdate<= @finishtime ) as A   
                                full join
                           (SELECT  workdate as workdays, SUM((worktime*costrate)/10000) as 直接人工
                              FROM SMT_worktimeinfo 
                              where typle='主营SMT' and correlation='生产' and workdate>=@startime and workdate<=@finishtime 
                                 group by workdate) B
                              on A.workdate=B.workdays
                              full join                          
                           (select  SUM(cast(pointcount as float)/10000) as 生产点数,workdate from
                             (select pointcount,convert(char(10),workdate,120) as workdate,latyple from openquery
                             (LINKERP,'select 日期 as workdate, sum(生产总点数) as pointcount,生产线别 as latyple from 
                             CUX_SMT_PROD_RP where 工单类型=''正常'' and 日期>=to_date(''2016-12-01'',''yyyy-mm-dd'') and 日期<=to_date(''2016-12-31'',''yyyy-mm-dd'') group by 日期,生产线别 order by 日期'))  m 
                                left join SMT_departinfo d 
                             on m.latyple=d.department 
                             where worktyple='主营SMT' and correlation='生产' 
                             group by workdate) C
                             on  A.workdate=C.workdate
                             full join                            
                           (SELECT  workdate, SUM((worktime*costrate)/10000) as 间接人工
                             FROM SMT_worktimeinfo  where typle='主营SMT' and correlation='部门'  and workdate>= @startime and workdate<= @finishtime 
                                group by workdate) D
                             on A.workdate=D.workdate
                             full join
                           (select workdate,COUNT(DISTINCT  workernum) as 直接人力数 from SMT_worktimeinfo where correlation='生产' and typle='主营SMT' and workdate>= @startime and workdate<= @finishtime  group by workdate) E 
                            on A.workdate=E.workdate
                             order by A.workdate