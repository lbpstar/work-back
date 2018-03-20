declare @month varchar(20)
set @month = '2017-10'
select max(status) status,CNO,cname,max(REGISTER_DATE) REGISTER_DATE,max(LEAVE_DATE) leave_date,max(CFROM) cfrom,max(dept2) dept2,max(dept3) dept3,max(dept) dept,
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-01' then HOURS else 0 end) as '01',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-02' then HOURS else 0 end) as '02',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-03' then HOURS else 0 end) as '03',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-04' then HOURS else 0 end) as '04',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-05' then HOURS else 0 end) as '05',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-06' then HOURS else 0 end) as '06',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-07' then HOURS else 0 end) as '07', 
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-08' then HOURS else 0 end) as '08',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-09' then HOURS else 0 end) as '09',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-10' then HOURS else 0 end) as '10',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-11' then HOURS else 0 end) as '11',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-12' then HOURS else 0 end) as '12',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-13' then HOURS else 0 end) as '13',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-14' then HOURS else 0 end) as '14',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-15' then HOURS else 0 end) as '15',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-16' then HOURS else 0 end) as '16',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-17' then HOURS else 0 end) as '17',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-18' then HOURS else 0 end) as '18',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-19' then HOURS else 0 end) as '19',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-20' then HOURS else 0 end) as '20', 
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-21' then HOURS else 0 end) as '21',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-22' then HOURS else 0 end) as '22',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-23' then HOURS else 0 end) as '23',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-24' then HOURS else 0 end) as '24',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-25' then HOURS else 0 end) as '25',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-26' then HOURS else 0 end) as '26',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-27' then HOURS else 0 end) as '27',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-28' then HOURS else 0 end) as '28',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-29' then HOURS else 0 end) as '29',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-30' then HOURS else 0 end) as '30',
MAX(case when cast(BEGIN_DATE AS varchar(20)) like '%-31' then HOURS else 0 end) as '31'
 from (select e.status,a.CNO,e.CNAME,e.REGISTER_DATE,e.LEAVE_DATE,e.CFROM,e.dept2,e.dept3,e.dept,a.BEGIN_DATE,a.hours from COST_TEMP_EMPLOYEE_ATTENDANCE a 
left join COST_TEMP_EMPLOYEE e on a.CNO = e.cno where  begin_apply is not null) t group by CNO,cname order by cno

select cast(BEGIN_DATE AS varchar(20)) from (select a.CNO,e.CNAME,a.BEGIN_DATE,a.hours from COST_TEMP_EMPLOYEE_ATTENDANCE a 
left join COST_TEMP_EMPLOYEE e on a.CNO = e.cno where e.STATUS = 'ÔÚÖ°' and begin_apply is not null) t order by cno