%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe E:\�ɱ�����\WindowsFormsApplication1\WindowsServiceCost\bin\Release\WindowsServiceCost.exe
Net Start CostService
sc config CostService start= auto
pause