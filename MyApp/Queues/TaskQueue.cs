// Queues/TaskQueue.cs
public class TaskQueue<T> : ITaskQueue<T> where T : ICustomTask
{
    private readonly ConcurrentPriorityQueue<T> queue;
    private ManualResetEvent tasksEnqueuedEvent;

    public event EventHandler<T>? TaskAdded;

    public TaskQueue()
    {
        this.queue = new ConcurrentPriorityQueue<T>();
        this.tasksEnqueuedEvent = new ManualResetEvent(false);
    }

    public void Enqueue(List<T> listOfTasks)
    {
        listOfTasks.ForEach(task =>
        {
            queue.Enqueue(task.Priority, task);
            OnTaskAdded(task);
        });

        // Signal the worker after all tasks are enqueued
        tasksEnqueuedEvent.Set();
    }

    // TaskQueue.cs
    public void WaitForTasksEnqueued()
    {
        // Wait for the signal that all tasks are enqueued
        tasksEnqueuedEvent.WaitOne();
    }

    public T Dequeue()
    {
        return queue.TryDequeue(out var task) ? task : default!;
    }

    void OnTaskAdded(T task)
    {
        TaskAdded?.Invoke(this, task);
    }
}
