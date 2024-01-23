// Workers/Worker.cs

public class Worker<T> : IWorker<T> where T : ICustomTask
{
    private readonly ITaskQueue<T> taskQueue;

    public Worker(ITaskQueue<T> taskQueue)
    {
        this.taskQueue = taskQueue;
    }

    public void Consume()
    {
        taskQueue.WaitForTasksEnqueued();
        int threadId = Thread.CurrentThread.ManagedThreadId;
        Console.WriteLine($"Consume Thread ID: {threadId}");

        while (true)
        {
            T task = taskQueue.Dequeue();
            if (task != null)
            {
                ExecuteTask(task);
                Thread.Sleep(1000);
            }
            else
            {
                // No tasks in the queue, sleep for a while or perform other operations
                Thread.Sleep(1000);
            }

        }
    }

    public void Produce(List<T> listOfTasks)
    {
        if (listOfTasks.Count != 0)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Produce Thread ID: {threadId}");
            taskQueue.TaskAdded += TaskAdded;
            taskQueue.Enqueue(listOfTasks);
        }
        else
        {
            Thread.Sleep(1000);
        }
    }

    public void TaskAdded(object? sender, T task)
    {
        // Worker is notified when a new task is added to the queue
        // You can implement additional logic here if needed
        Console.WriteLine($"New task added: {task.Description}, Priority: {task.Priority}");
        Thread.Sleep(1000);
    }

    private void ExecuteTask(T task)
    {   
        int threadId = Thread.CurrentThread.ManagedThreadId;
        Console.WriteLine($"Executor Thread ID: {threadId}");
        Console.WriteLine($"Executing task: {task.Description}, Priority: {task.Priority}");
        task.Action();
    }
}
