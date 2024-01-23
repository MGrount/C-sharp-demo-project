public delegate void TaskAction();
// Interfaces/ITask.cs
public interface ICustomTask
{
    string Description { get; }
    Priority Priority { get; }
    TaskAction Action { get; }
    bool ReRun { get; set; }
}
