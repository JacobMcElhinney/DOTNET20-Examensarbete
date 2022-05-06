namespace worker.powershell.src.Interfaces
{
    using System.Collections.Generic;
    public interface IProcess<T>
    {
        string FlowName { get; set; }
        IList<Object> Parameters { get; set; }

        IList<Object> Steps { get; set; }

        T Status {get;set;}

    }

    /*
        export interface IProcess extends Document {
            flowName: string,
            parameters: any,
            steps: ProcessStep[],
            status: ProcessStepStatus
        }
    */
    

}