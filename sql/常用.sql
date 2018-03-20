--��ȡ�ϸ��·�
select convert(varchar(7),dateadd(month,-1,getdate()),120)
--��ȡ�ϸ��µĽ���
select dateadd(month,-1,getdate()) 
select convert(varchar(10),dateadd(month,-1,getdate()),120)
--��ȡ��ǰ�·�
select convert(varchar(7),getdate(),120)
--��ȡ��ĩ
select convert(varchar,dateadd(day,-day(getdate()),dateadd(month,1,getdate())),23) 
--��ȡ����ĩ
SELECT DATEADD(Day,-1,CONVERT(char(8),GetDate(),120)+'1')
--��ȡ���³�
SELECT DATEADD(mm,DATEDIFF(mm,0,dateadd(month,-1,GetDate())),0)
--��ת��
--UNPIVOT�﷨��http://database.51cto.com/art/201107/276189.htm
CREATE TABLE #ProgrectDetail
          
(    
    ProgrectName         NVARCHAR(20), --��������
    OverseaSupply        INT,          --���⹩Ӧ�̹�������    
    NativeSupply         INT,          --���ڹ�Ӧ�̹�������   
    SouthSupply          INT,          --�Ϸ���Ӧ�̹�������     
    NorthSupply          INT           --������Ӧ�̹�������     
)
                
INSERT INTO #ProgrectDetail      
SELECT 'A', 100, 200, 50, 50      
UNION ALL      
SELECT 'B', 200, 300, 150, 150        
UNION ALL      
SELECT 'C', 159, 400, 20, 320         
UNION ALL        
SELECT 'D', 250, 30, 15, 15

SELECT ProgrectName,Supplier,SupplyNum 
FROM  
( 
    SELECT ProgrectName, OverseaSupply, NativeSupply, SouthSupply, NorthSupply FROM #ProgrectDetail 
)T 
UNPIVOT  
( 
    SupplyNum FOR Supplier IN 
    (OverseaSupply, NativeSupply, SouthSupply, NorthSupply ) 
) P

