using System.Management.Automation;
using Microsoft.Extensions.Options;
using worker.powershell.src.Interfaces;
using worker.powershell.src.Models;
using worker.powershell.src.Utilities;
using System.ComponentModel; //For enum attributes

namespace worker.powershell;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly WorkerOptions _options;
    private readonly IPowerShellService _powerShellService;
    private readonly IProcessStepService<ProcessStep> _processStepsService;
    private readonly ILogService<Log> _LogService;

    private string? EnvironmentVariables { get; set; }
    //MockData



    //Called once when resolved from Dependency Injection container in Program.cs
    public Worker(ILogger<Worker> logger, IOptions<WorkerOptions> options, IPowerShellService powerShellService, IProcessStepService<ProcessStep> processStepService, ILogService<Log> logService)
    {
        _logger = logger;
        _options = options.Value; //Appsettings.Development.json section: "WorkerOptions": 
        _powerShellService = powerShellService;
        _processStepsService = processStepService;
        _LogService = logService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        while (!stoppingToken.IsCancellationRequested)
        {

            //!TEST: call ProcessStepService
            try
            {
   
              DebuggingAssistant.LogMessage(DebuggingAssistant.MessageType.Info, "Attempting to call Flow API..." );

                var steps = await _processStepsService.GetPendingSteps();
            }
            catch (Exception e)
            {
              DebuggingAssistant.LogMessage(DebuggingAssistant.MessageType.Error,  e.Message);
            }

            //!TEST: call LogService
            try
            {
                DebuggingAssistant.LogMessage(DebuggingAssistant.MessageType.Info, "Attempting to call Log API..." );
                await _LogService.postLog(null);
            }
            catch (Exception e)
            { 
                DebuggingAssistant.LogMessage(DebuggingAssistant.MessageType.Error, e.Message );

            }




            var files = Directory.GetFiles(".");
            foreach (string file in files)
            {

                // System.Console.WriteLine("worker directory: " + Path.GetRelativePath(".",file));

            }

            //! TEST: refactor this code, create method for navigating to script directory/env file etc etc..
            if (EnvironmentVariables == null)
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                DebuggingAssistant.LogMessage(DebuggingAssistant.MessageType.Info, $"Create env file here: {currentDirectory}{_options.Path}"); 
            }


            // var path = powerShellClient.RunScript(_options.path)
            PSDataCollection<PSObject> output = await _powerShellService.RunScript(
                ScriptParser.GetScriptFromPath(
                    MockData.ScriptPath));

            if (_options.Logging != "disabled")
            {
                foreach (PSObject item in output)
                {
                    DebuggingAssistant.LogMessage(DebuggingAssistant.MessageType.Info, $"PowerShellService: {item.BaseObject.ToString()}");
                }
            }


            await Task.Delay(_options.CycleInterval, stoppingToken); //! defaut value: 1000ms
        }
    }

    //Called when the worker is started. Allows for asynchronous initialization calls.
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker starting...");
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