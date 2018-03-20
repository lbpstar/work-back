select * from kq_dayreport where CONVERT(varchar(100), rdate, 23) like '2017-06%'
select rdate,r.empno,r.empname,wkname,gz_int,jb_ps_int,jb_xx_int,jb_jr_int,ischeck,e.mjmark,e.dcmark,e.bcol1 from kq_dayreport r left join hr_employee e on r.empno = e.empno 
where e.mjmark = '制造中心' and CONVERT(varchar(100), r.rdate, 20) like '2017-07%' 

select * from kq_dayreport where isnull(Jb_xx_int,0) >0
select * from kq_dayreport where empname = ''

select distinct deptname from kq_dayreport where deptname like '%运营部%'
select distinct bcname from kq_dayreport 
select * from hr_employee
select distinct dcmark from hr_employee where mjmark = '制造中心'
--empaddr中信息似乎不准确，比如'供应链管理部\制造中心\SMT运营部'和'制造中心\SMT运营部'，员工10026305的二级部门是质量部。mjmark\dcmark\bcol1比较准确
select * from hr_employee where empaddr like '%运营部%'
select distinct bcol1 from hr_employee where mjmark = '制造中心' and dcmark = 'SMT运营部'
select distinct remark from hr_employee where mjmark = '制造中心' and dcmark = 'SMT运营部'
select * from hr_employee where mjmark = '制造中心' and dcmark = 'SMT运营部'
select distinct bcol1 from hr_employee where empaddr like '%运营部%'
select * from hr_employee where bcol1 like '%D1及其它产品SMT%'
select * from hr_employee where empname = ''
select * from hr_excel2 where empmc = ''