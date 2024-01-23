// Interfaces/IConcurrentPriorityQueue.cs
public interface IConcurrentPriorityQueue<T> where T: ICustomTask
{
    void Enqueue(Priority priority, T task);
    bool TryDequeue(out T value);
}
