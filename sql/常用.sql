--获取上个月份
select convert(varchar(7),dateadd(month,-1,getdate()),120)
--获取上个月的今天
select dateadd(month,-1,getdate()) 
select convert(varchar(10),dateadd(month,-1,getdate()),120)
--获取当前月份
select convert(varchar(7),getdate(),120)
--获取月末
select convert(varchar,dateadd(day,-day(getdate()),dateadd(month,1,getdate())),23) 
--获取上月末
SELECT DATEADD(Day,-1,CONVERT(char(8),GetDate(),120)+'1')
--获取上月初
SELECT DATEADD(mm,DATEDIFF(mm,0,dateadd(month,-1,GetDate())),0)
--列转行
--UNPIVOT语法：http://database.51cto.com/art/201107/276189.htm
CREATE TABLE #ProgrectDetail
          
(    
    ProgrectName         NVARCHAR(20), --工程名称
    OverseaSupply        INT,          --海外供应商供给数量    
    NativeSupply         INT,          --国内供应商供给数量   
    SouthSupply          INT,          --南方供应商供给数量     
    NorthSupply          INT           --北方供应商供给数量     
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

