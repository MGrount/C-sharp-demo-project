// Interfaces/ITaskObserver.cs
public interface ITaskObserver<T> where T : ICustomTask
{
    void TaskAdded(object? sender, T task);
}