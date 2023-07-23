using ArxOne.MrAdvice.Advice;
using Serilog;
using System.Diagnostics;

namespace Ahlatci.Shop.Application.Behaviors
{
    public class PerformanceBehavior : Attribute, IMethodAdvice
    {
        public void Advise(MethodAdviceContext context)
        {
            Stopwatch watch = new Stopwatch();
            //Kronometreyi başlat
            watch.Start();

            context.Proceed();

            //Kronometreyi durdur
            watch.Stop();

            var totalDuration = watch.Elapsed.TotalSeconds;

            Log.Information($"{context.TargetName} metodu {totalDuration} saniyede tamamlandı.");
        }
    }
}
