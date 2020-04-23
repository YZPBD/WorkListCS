%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe CSWorkListService.exe

Net Start CSWorkListService

sc config CSWorkListService start= auto

pause