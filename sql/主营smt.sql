
declare @startime datetime,@finishtime datetime
select @startime = '2016-12-01',@finishtime = '2016-12-31'
select A.workdate as ʱ��,isnull(��������,0) as ��������,
                              (case when isnull(��������,0)*isnull(�ɱ�����,0)=0 then 0 
                               else isnull(((isnull(�۾ɷ�,0) +isnull(ֱ���˹�,0)+isnull(����˹�,0))/�ɱ�����/��������),0) end) as Ԥ������ɱ�,
                             (case when isnull(�ɱ�����,0)=0 then 0 
                               else isnull(((isnull(�۾ɷ�,0) +isnull(ֱ���˹�,0)+isnull(����˹�,0))/�ɱ�����),0) end) as Ԥ���ɱ�,
                              isnull(�����׼�ɱ�,0) as �����׼�ɱ�,isnull(�۾ɷ�,0) as �۾ɷ�,
                              isnull(ֱ���˹�,0) as ֱ���˹�,isnull(����˹�,0) as ����˹�,isnull(ֱ�Ӳ�������,0) as ֱ�Ӳ�������,isnull(��������,0) as ��������,
                              isnull(ֱ��������,0) as ֱ��������
                              from
                           (select isnull(depreciation,0) as �۾ɷ�,workdate,isnull(pointcost,0) as �����׼�ɱ�,
                              isnull(material,0) as ֱ�Ӳ�������,isnull(othercost,0) as ��������,isnull(costper,0) as �ɱ�����
                              from SMT_materialcost where typle='��ӪSMT' and workdate>= @startime  and workdate<= @finishtime ) as A   
                                full join
                           (SELECT  workdate as workdays, SUM((worktime*costrate)/10000) as ֱ���˹�
                              FROM SMT_worktimeinfo 
                              where typle='��ӪSMT' and correlation='����' and workdate>=@startime and workdate<=@finishtime 
                                 group by workdate) B
                              on A.workdate=B.workdays
                              full join                          
                           (select  SUM(cast(pointcount as float)/10000) as ��������,workdate from
                             (select pointcount,convert(char(10),workdate,120) as workdate,latyple from openquery
                             (LINKERP,'select ���� as workdate, sum(�����ܵ���) as pointcount,�����߱� as latyple from 
                             CUX_SMT_PROD_RP where ��������=''����'' and ����>=to_date(''2016-12-01'',''yyyy-mm-dd'') and ����<=to_date(''2016-12-31'',''yyyy-mm-dd'') group by ����,�����߱� order by ����'))  m 
                                left join SMT_departinfo d 
                             on m.latyple=d.department 
                             where worktyple='��ӪSMT' and correlation='����' 
                             group by workdate) C
                             on  A.workdate=C.workdate
                             full join                            
                           (SELECT  workdate, SUM((worktime*costrate)/10000) as ����˹�
                             FROM SMT_worktimeinfo  where typle='��ӪSMT' and correlation='����'  and workdate>= @startime and workdate<= @finishtime 
                                group by workdate) D
                             on A.workdate=D.workdate
                             full join
                           (select workdate,COUNT(DISTINCT  workernum) as ֱ�������� from SMT_worktimeinfo where correlation='����' and typle='��ӪSMT' and workdate>= @startime and workdate<= @finishtime  group by workdate) E 
                            on A.workdate=E.workdate
                             order by A.workdate