--�۾ɡ���ɱ�
select * from SMT_materialcost
--����
select * from SMT_worktimeinfo
--������Ϣ
select * from SMT_departinfo
--��Ա
select * from SMT_workerinfo
--�ɱ�Ԥ������
select * from SMT_ProductForecast

declare @startime datetime,@finishtime datetime
select @startime = '2016-12-01',@finishtime = '2016-12-31'
select isnull(depreciation,0) as �۾ɷ�,workdate,isnull(pointcost,0) as �����׼�ɱ�,
                              isnull(material,0) as ֱ�Ӳ�������,isnull(othercost,0) as ��������,isnull(costper,0) as �ɱ�����
                              from SMT_materialcost where typle='��ӪSMT' and workdate>= @startime  and workdate<= @finishtime 
                            
 select * from SMT_departinfo where correlation='����'
 
 ALTER TABLE COST_COMPOSITE_EXPENSE
ADD CONSTRAINT PK_COST_COMPOSITE_EXPENSE_sale_type_ID FOREIGN KEY (sale_type_ID)
REFERENCES cost_saletype(cid)

 ALTER TABLE cost_TRANSFER
ADD CONSTRAINT PK_cost_TRANSFER_sale_type_ID FOREIGN KEY (sale_type_id)
REFERENCES cost_saletype(cid)

update cost_direct_labour set cno = ltrim(rtrim('002')),cname = ltrim(rtrim('Ա��2')),position_id = null,linetype_id =null,dept_id =6 where cid = 3
select * from cost_direct_labour

select * from COST_DIRECT_LABOUR_ATTENDANCE
select * from COST_INDIRECT_LABOUR_ATTENDANCE
delete from COST_INDIRECT_LABOUR_ATTENDANCE where INDIRECT_LABOUR_ID = 2
update i set i.person_type_id = 3 from cost_direct_labour i 

insert into COST_DIRECT_LABOUR_ATTENDANCE(CDATE,DIRECT_LABOUR_ID,WORK_TYPE_ID,HOURS) select CDATE,inDIRECT_LABOUR_ID,WORK_TYPE_ID,HOURS from COST_INDIRECT_LABOUR_ATTENDANCE

declare @cmonth2 varchar(20) 
set @cmonth2 = '2017-05'
select a.CDATE ����,max(d.cname) ����,MAX(l.CNAME) ����,MAX(w.CNAME) �ϰ�����,SUM(a.HOURS) Сʱ��,
�ɱ� = case when  w.cid = 1 and SUM(a.HOURS) <= 8 then SUM(a.HOURS)*max(p.PRICE) 
when w.cid = 1 and SUM(a.HOURS) > 8 then (SUM(a.HOURS)-8)*max(p2.PRICE) +8*max(p.PRICE) 
else  SUM(a.HOURS)*max(p.PRICE) end 
from COST_DIRECT_LABOUR_ATTENDANCE a 
left join COST_DIRECT_LABOUR l on a.DIRECT_LABOUR_ID = l.CID 
left join COST_WORK_TYPE w on a.WORK_TYPE_ID = w.CID 
left join cost_dept d on l.dept_id = d.cid 
left join COST_SALETYPE s on d.saletype_id = s.cid 
left join (select * from COST_inDIRECT_LABOUR_PRICE where YYYYMM = @cmonth2) p on w.CID = p.WORK_TYPE_ID and a.DIRECT_LABOUR_ID = p.INDIRECT_LABOUR_ID  
left join (select * from COST_inDIRECT_LABOUR_PRICE where YYYYMM = @cmonth2 and WORK_TYPE_id = 3) p2 on a.DIRECT_LABOUR_ID = p2.INDIRECT_LABOUR_ID 
where s.CNAME = 'ems smt' and a.cdate like '%' + @cmonth2 + '%' and p.YYYYMM = @cmonth2  group by a.CDATE,w.cid,a.DIRECT_LABOUR_ID

select * from COST_DIRECT_LABOUR
select * from COST_DIRECT_LABOUR_ATTENDANCE where DIRECT_LABOUR_ID = 10
delete from COST_INDIRECT_LABOUR_PRICE where INDIRECT_LABOUR_ID < 11

select CONVERT(varchar(100), cdate, 23) as cdate,linetype,pointcount from openquery (LINKERP, 'select ���� as cdate, �����߱� as linetype,sum(�����ܵ���) as pointcount from CUX_SMT_PROD_RP where �������� = ''����'' and ���� >= to_date(''2016 - 12 - 01'', ''yyyy - mm - dd'') and ���� <= to_date(''2016 - 12 - 31'', ''yyyy - mm - dd'') group by ����, �����߱� order by ����')

