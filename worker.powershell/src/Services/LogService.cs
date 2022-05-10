namespace worker.powershell.src.Services
{
    public class LogService
    {
        
    }
}




//

/* CONVERT TO C#
const fetch = require("node-fetch");
import { Log } from "../../../lib/types/Log";

const postLog = async (
  { processId,processStep, message, severity } : Log
) => {
  await fetch(`${process.env.LOG_API_URI}/log`, {
    method: "post",
    headers: {
      "content-type": "application/json",
    },
    body: JSON.stringify({
      processId,
      processStep,
      message,
      severity,
    }),
  });
};

export default postLog;
*/