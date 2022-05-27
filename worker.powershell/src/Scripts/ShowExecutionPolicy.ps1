$Policy =  Get-ExecutionPolicy -list
Write-Output ($Policy | Out-String)