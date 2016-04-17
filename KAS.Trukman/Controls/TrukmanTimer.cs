using System;
using System.Threading.Tasks;
using System.Threading;

namespace Trukman
{
	internal delegate void TimerCallback(object state);

	internal sealed class Timer : CancellationTokenSource, IDisposable
	{
    	internal Timer(TimerCallback callback, object state, int dueTime, int period)
    	{
        	//Contract.Assert(period == -1, "This stub implementation only supports dueTime.");
        	Task.Delay(dueTime, Token).ContinueWith(async (t, s) =>
      	  {
        	    var tuple = (Tuple<TimerCallback, object>)s;

					while (!IsCancellationRequested)
					{
						//if (waitForCallbackBeforeNextPeriod)
						//	tuple.Item1(tuple.Item2);
						//else
							Task.Run(() => tuple.Item1(tuple.Item2));
						await Task.Delay(period);
						//await Task.Delay(period, Token).ConfigureAwait(false);
					}
            	//tuple.Item1(tuple.Item2);
	        }, Tuple.Create(callback, state), CancellationToken.None,
    	        TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion,
        	    TaskScheduler.Default);
	    }

	    public new void Dispose() { base.Cancel(); }
	}
}