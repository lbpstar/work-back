SELECT CSD.WIP_ENTITY_ID,
               csd.organization_id,
               CSD.AB_SIDE,SCHEDULE_GROUP_DESC
        FROM   CUX_WIP_SMT_DONEINFO CSD
        where wip_entity_id = 165 and ab_side = 'A'
        GROUP BY CSD.WIP_ENTITY_ID,
                 CSD.AB_SIDE,
                 SCHEDULE_GROUP_DESC  
                 
 select * from cux_wip_smt_doneinfo where created_date >= to_date('2017/3/1' ,'YYYY-MM-DD')  and ab_side = 'A' and schedule_group_desc = 'NSMT-L02B'
                
 select * from cux_wip_smt_doneinfo where wip_entity_id = 165 and ab_side = 'A' and schedule_group_name = 'SMT02B'
 --ÿ�졢ÿһ��������ÿһ���߱�ÿһ��ab�棬�����ж����㱨��¼
 select * from cux_wip_smt_doneinfo where (WIP_ENTITY_ID,AB_SIDE, SCHEDULE_GROUP_DESC,created_date) in 
 (SELECT CSD.WIP_ENTITY_ID,CSD.AB_SIDE,SCHEDULE_GROUP_DESC,created_date FROM   CUX_WIP_SMT_DONEINFO CSD where  created_date >= to_date('2017/3/1' ,'YYYY-MM-DD') 
         GROUP BY CSD.WIP_ENTITY_ID, CSD.AB_SIDE,SCHEDULE_GROUP_DESC,created_date having count(*) > 1) 
         order by WIP_ENTITY_ID,created_date,AB_SIDE, SCHEDULE_GROUP_DESC
         
 --��ͬ��wip_entity_id��organization_id��һ����                
 SELECT CSD.WIP_ENTITY_ID,max(csd.organization_id) maxid,min(csd.organization_id) minid FROM   CUX_WIP_SMT_DONEINFO CSD  GROUP BY CSD.WIP_ENTITY_ID
having max(csd.organization_id) <> min(csd.organization_id)

                 
