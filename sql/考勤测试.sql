select * from kq_dayreport where CONVERT(varchar(100), rdate, 23) like '2017-06%'
select rdate,r.empno,r.empname,wkname,gz_int,jb_ps_int,jb_xx_int,jb_jr_int,ischeck,e.mjmark,e.dcmark,e.bcol1 from kq_dayreport r left join hr_employee e on r.empno = e.empno 
where e.mjmark = '��������' and CONVERT(varchar(100), r.rdate, 20) like '2017-07%' 

select * from kq_dayreport where isnull(Jb_xx_int,0) >0
select * from kq_dayreport where empname = ''

select distinct deptname from kq_dayreport where deptname like '%��Ӫ��%'
select distinct bcname from kq_dayreport 
select * from hr_employee
select distinct dcmark from hr_employee where mjmark = '��������'
--empaddr����Ϣ�ƺ���׼ȷ������'��Ӧ������\��������\SMT��Ӫ��'��'��������\SMT��Ӫ��'��Ա��10026305�Ķ�����������������mjmark\dcmark\bcol1�Ƚ�׼ȷ
select * from hr_employee where empaddr like '%��Ӫ��%'
select distinct bcol1 from hr_employee where mjmark = '��������' and dcmark = 'SMT��Ӫ��'
select distinct remark from hr_employee where mjmark = '��������' and dcmark = 'SMT��Ӫ��'
select * from hr_employee where mjmark = '��������' and dcmark = 'SMT��Ӫ��'
select distinct bcol1 from hr_employee where empaddr like '%��Ӫ��%'
select * from hr_employee where bcol1 like '%D1��������ƷSMT%'
select * from hr_employee where empname = ''
select * from hr_excel2 where empmc = ''