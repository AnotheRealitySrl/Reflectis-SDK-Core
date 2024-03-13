using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;

public abstract class AwaitableEventNode<T> : EventUnit<T>
{
    protected override bool register => true;

    protected List<Flow> runningFlows = new List<Flow>();

    public async Task AwaitableTrigger(GraphReference reference, T args)
    {
        var flow = Flow.New(reference);

        if (!ShouldTrigger(flow, args))
        {
            flow.Dispose();
            return;
        }

        AssignArguments(flow, args);

        Run(flow);

        while (runningFlows.Contains(flow))
        {
            await Task.Yield();
        }
    }


    private void Run(Flow flow)
    {
        if (flow.enableDebug)
        {
            var editorData = flow.stack.GetElementDebugData<IUnitDebugData>(this);

            editorData.lastInvokeFrame = EditorTimeBinding.frame;
            editorData.lastInvokeTime = EditorTimeBinding.time;
        }

        if (coroutine)
        {
            runningFlows.Add(flow);
            Reflectis.SDK.Utilities.CoroutineRunner.Instance.StartCoroutine(TriggerState(flow));
            flow.StartCoroutine(trigger, flow.stack.GetElementData<Data>(this).activeCoroutines);
        }
        else
        {
            flow.Run(trigger);
        }
    }

    private IEnumerator TriggerState(Flow flow)
    {
        while (flow.stack != null) //check if it is still running
        {
            yield return null;
        }
        runningFlows.Remove(flow);
    }
}
