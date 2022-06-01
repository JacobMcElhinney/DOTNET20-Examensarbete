# Generic Workflow Engine

## Notes on testing
Please review these brief instruction on how to run and debugg the applications in this repository.

### DOTNET20-WebAPI
Before starting up the workflow engine please open the project `DOTNET20-WebAPI` using Vistual Studio 2022 and make you you create a local SQL database using the command `Update-Database` (from project root folder), which will create a local database using the project migration files, provided that you have SQL Server installed on you system.

### worker.powershell
Once your SQL database and TestApi are running, navigate to the `worker.powershell` folder and run the terminal command `dotnet run`.

```Pwsh
#Example:
PS ...DOTNET20-Examensarbete\worker.powershell> dotnet run   
```
Note that if the **TestAPI** (DOTNET20-WebAPI) is not running, the worker service (workflow engine) will throw the following error:

```Pwsh

JobService: No connection could be made because the target machine actively refused it. (localhost:7065). Make sure API is running
[!]: Worker JobService failed to connect. Press [ctrl + c] to exit application...

```

Note that for testing and demonstration purposes, the databse is reset once the worker finishes processing all jobs:

```CSharp
            //If TestData in appsettings.Development.json is set to true, try to reset each job's status in the database, allowing them to be performed once more. 
            try
            {
                if (_options.TestData == true)
                {
                    foreach (var job in jobs)
                    {
                        await _jobService.ResetJobsInDb(job);
                    }
                }
            }

```
To disable this behaviour open `worker.powershell\appsettings.Development.json` and set the value of `TestData` to `false`. You also change the `CycleInterval` key, to any *integer value* in order to slow down the rate at which the worker operates.

```JSON

  "WorkerOptions": {
    "WhyTheseOptions": "So that we can switch features on and off",
    "Name": "worker.powershell",
    "Version":"1.0.0",
    "Description":"A Generic reusable workflow engine",
    "Main": "Program.cs",
    "Logging" : true,
    "SqlTestApi": "https://localhost:7065",
    "TestData": true,
    "LogApiBaseUrl":"http://localhost:4004",
    "FlowApiBaseUrl":"http://localhost:4001",
    "AgentName":"cloud",
    "CycleInterval":5000,
    "Path":"\\config\\"
  }

```

I recommed putting a breakpoint at line 55 in the Test api `JobController.cs` to better visualise the control flow or simply step through the `worker service` application during debugging. 

Feel free try any of the sample json requests from `swagger.md` with the OpenAPi/Swagger interface (`https://localhost:7065/swagger/index.html`) if you want to try to add Jobs to the SQL Database.