using Hangfire;
using Hangfire.Common;

namespace VaraticPrim.Framework.Managers;

public class JobManager
{
    private readonly RecurringJobManager _jobManager;

    public JobManager()
    {
        _jobManager = new RecurringJobManager();
    }
    
    public void GenerateInvoicesMonthly()
    {
        _jobManager.AddOrUpdate("console-log", Job.FromExpression(() => Console.WriteLine("InvoiceGenerated")), Cron.Monthly());
    }
}