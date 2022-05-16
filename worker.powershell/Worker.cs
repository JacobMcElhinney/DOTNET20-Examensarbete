using System.Management.Automation;
using Microsoft.Extensions.Options;
using worker.powershell.src.Models;
using worker.powershell.src.Utilities;

namespace worker.powershell;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly WorkerOptions _options;

    private string EnvironmentVariables { get; set; }
    //MockData
    private int logEntryCount { get; set; }


    //Called once when resolved from Dependency Injection container in Program.cs
    public Worker(ILogger<Worker> logger, IOptions<WorkerOptions> options)
    {
        _logger = logger;
        _options = options.Value; //Appsettings.Development.json section: "WorkerOptions": 
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker starting...");

        while (!stoppingToken.IsCancellationRequested)
        {
            /*  
                const path = require("path");
                const fetch = require("node-fetch");
            */

            var files = Directory.GetFiles(".");
            foreach (string file in files)
            {

                // System.Console.WriteLine("worker directory: " + Path.GetRelativePath(".",file));

            }

            if (EnvironmentVariables == null)
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                System.Console.WriteLine(currentDirectory + _options.Path);
                EnvironmentVariables = currentDirectory + _options.Path;
            }


            PowerShellClient powerShellClient = new();
            // var path = powerShellClient.RunScript(_options.path)
            var output = await powerShellClient.RunScript(
                ScriptParser.GetScriptFromPath(
                    MockData.ScriptPath));

            if (_options.Logging != "disabled")
            {
                foreach (PSObject item in output)
                {
                    System.Console.WriteLine("PowerShellClient: " + item.BaseObject.ToString());
                }
            }


            await Task.Delay(3000, stoppingToken); //! defaut value: 1000ms
        }
    }

    //Called when the worker is started. Allows for asynchronous initialization calls.
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        if (_options.Logging != "disabled") _logger.LogInformation("Worker starting...");
        return base.StartAsync(cancellationToken);
    }

    //Called when the worker is stopped. Allows for cleanup calls.
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker stopping...");
        return base.StopAsync(cancellationToken);
    }
}


/* STEPS FROM worker.ts TO REPLICATE IN THIS FILE

import * as dotenv from "dotenv";
import {
  ProcessStep,
  ProcessStepStatus,
} from "../../lib/clients/mongoose/model/Process";
import getNextSteps from "./client/getNextSteps";
import setStepStatus from "./client/setStepStatus";
import clone from "../../lib/clients/git/clone";
import fs from "promise-fs";
import postLog from "./client/postLog";
import { NextStep } from "../../lib/types/NextStep";
import { LogSeverity, Log } from "../../lib/types/Log";

//Q&A Declares and initialises path variable with absolute path to current file?
const path = require("path");
const fetch = require("node-fetch");

//! this is achieved in the configure services method in Program.cs
const env = process.env.ENV || "local"; 
dotenv.config({ path: `config/${env}.env` });

const log = (log : Log) => {
  try {
  postLog(log);
} catch (e) {
  console.log('Could not connect to log api ' + e.toString());
}
}

const runCycle = async () => {
  let steps = [];
  try {
    steps = await getNextSteps();
  } catch (e) {
    console.log("Could not connect to flow api to retrieve upcoming steps");
  }

  log({ message: `${steps.length} steps found. ${steps.length > 0 ? `Starting to execute!` : "" }`,severity: LogSeverity.Information });

  steps.forEach(async (step: NextStep) => {
    //TODO: Check for correct version / git hash
    const script = await fs.readFile(path.join("./repo", step.path));
    log({ processId: step.processId, processStep: step.stepIndex, message: "Script is about to start", severity: LogSeverity.Information });
    const runFunction = new Function("smash", "fetch", script);
    const smash = {
      params: step.parameters,
      complete: () => {
        log({ processId: step.processId, processStep: step.stepIndex, message: "Step completed", severity: LogSeverity.Information });
        setStepStatus(
          step.processId,
          step.stepIndex,
          ProcessStepStatus.Completed
        );
      },
      error: (error) => {
        log({ processId: step.processId, processStep: step.stepIndex, message: error.toString(), severity: LogSeverity.Error });
        setStepStatus(step.processId, step.stepIndex, ProcessStepStatus.Error);
      },
      log: (message: string) => log({ processId: step.processId, processStep: step.stepIndex, message, severity: LogSeverity.Information })
    };
    setStepStatus(step.processId, step.stepIndex, ProcessStepStatus.Started);
    try {
      runFunction(smash, fetch);
    } catch (e) {
      smash.error(e.toString());
    }
  });
};

clone(process.env.GIT_URL, "./repo").then(() => {
  setInterval(runCycle, Number(process.env.CYCLE_INTERVAL));
});

*/