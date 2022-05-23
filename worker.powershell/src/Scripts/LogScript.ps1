$Date = Get-Date
$Message = "Log entry successful. Log file size:";
#This could be a parameter coming from one of the apis
$LogMessage = "LogScript Ran..."

Write-Output "$($Date): $($LogMessage)" >> log.txt
$File = Get-Item log.txt

Write-Output "$($Date): $($Message) $($File.Length/1kb) kilobytes"

