exec costing '2017-05',10
delete from COST_DAY_CALCULATE
delete from COST_MONTH_CALCULATE
delete from COST_DEPT_CALCULATE
delete from COST_DEPT_MONTH_CALCULATE

select * from COST_DAY_CALCULATE
select * from COST_MONTH_CALCULATE
select * from COST_DEPT_CALCULATE
select * from COST_DEPT_MONTH_CALCULATE

select top 1 * from COST_DIRECT_LABOUR_ATTENDANCE

select SALE_TYPE_NAME Ӫҵ����,dept_name ����,CMONTH ����,DIRECT_HOURS ֱ���˹�Сʱ��,DIRECT_COST ֱ���˹��ɱ�,INDIRECT_HOURS ����˹�Сʱ��,INDIRECT_COST ����˹��ɱ�,POINTCOUNT ����,COST Ԥ���ɱ�,COST_POINT Ԥ������ɱ�,STANDARD_POINT ��׼����ɱ�,STANDARD_COST ��׼�ɱ�,PROFIT ӯ�� from COST_DEPT_CALCULATE