﻿using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Quartz;

#region SendMessageJob
public class SendMessageJob : IJob
{
    static ILog log = LogManager.GetLogger<SendMessageJob>();

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var endpointInstance = context.EndpointInstance();
            await endpointInstance.Send("Samples.QuartzScheduler.Receiver", new MyMessage())
                .ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            log.Fatal("Execution Failed", exception);
            // TODO: handle exception and dont throw.
            // consider implementing a circuit breaker
            throw;
        }
    }
}
#endregion