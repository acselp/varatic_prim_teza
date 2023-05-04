namespace VaraticPrim.Framework.Managers;

public class BackgroundTaskManager
{
    private readonly BackgroundTaskManager _taskManager;

    public BackgroundTaskManager()
    {
        _taskManager = new BackgroundTaskManager();
    }
    
    public Task GenerateInvoicesMonthly()
    {
        throw new Exception();
    }
}