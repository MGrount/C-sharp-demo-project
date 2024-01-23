// Interfaces/ITaskQueue.cs
public interface ITaskQueue<T> where T : ICustomTask
{
    event EventHandler<T> TaskAdded;
    void Enqueue(List<T> ListOfTasks);
    T Dequeue();
    void WaitForTasksEnqueued();
}
