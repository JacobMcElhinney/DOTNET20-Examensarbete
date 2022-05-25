$Date = Get-Date
$Message = "Log entry successful. Log file size:";
$LogMessage = "LogScript Ran..."
Write-Output "$($Date): $($LogMessage)" >> log.txt
$File = Get-Item log.txt
Write-Output "$($Date): $($Message) $($File.Length/1kb) kilobytes"

