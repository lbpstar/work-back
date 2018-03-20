
set nocount on
declare @days int,@quarter int,@cdate varchar(20),@type int,@cmonth varchar(20),@saletypeid int
set @cmonth = '2017-08'
set @saletypeid = 14
if cast(RIGHT(@cmonth,2) as int) < 4 
set @quarter = 1
else if cast(RIGHT(@cmonth,2) as int) >= 4 and cast(RIGHT(@cmonth,2) as int) <7 
set @quarter = 2
else if cast(RIGHT(@cmonth,2) as int) >= 7 and cast(RIGHT(@cmonth,2) as int) <10 
set @quarter = 3
else
set @quarter = 4
if @saletypeid = 14 
set @type = 1
else if @saletypeid = 15
set @type =2
--临时工成本...运营部..该营业类型每天的临时工成本
select a.CDATE ,a.SALE_TYPE_ID saletypeid,a.dept_id,a.HOURS chours,
cast(a.HOURS*p.PRICE/CAST(r.cost_rate as decimal(18,4))  as decimal(18,5)) cost,*
from COST_TEMP_WORKER a 
left join (select * from COST_TEMP_WORKER_PRICE where YYYYMM = @cmonth) p on 1=1 
left join (select sale_type_id,dept_id,COST_RATE from cost_rate where yyyy = left(@cmonth,4) and QUARTER_ID = @quarter and isnull(dept_id,0) >0) r on a.SALE_TYPE_ID = r.sale_type_id and a.dept_id= r.dept_id 
where a.SALE_TYPE_ID = @saletypeid and a.cdate like '%' + @cmonth + '%'  



select a.CDATE ,a.SALE_TYPE_ID saletypeid,a.dept_id,a.HOURS chours,
cast(a.HOURS*p.PRICE/CAST(r.cost_rate as decimal(18,4))  as decimal(18,5)) cost,*
from COST_TEMP_WORKER a 
left join (select * from COST_TEMP_WORKER_PRICE where YYYYMM = '2017-08' and SALE_TYPE_ID = 14) p on 1=1 
left join (select sale_type_id,dept_id,COST_RATE from cost_rate where yyyy = left('2017-08',4) and QUARTER_ID = 3 and isnull(dept_id,0) >0) r on a.SALE_TYPE_ID = r.sale_type_id and a.dept_id= r.dept_id 
where a.SALE_TYPE_ID = 14 and a.cdate like '2017-08%'  