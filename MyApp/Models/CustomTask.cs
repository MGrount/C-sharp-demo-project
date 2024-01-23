// Models/Task.cs
public class CustomTask : ICustomTask
{
    public required string Description { get; set; }
    public Priority Priority { get; set; }
    public required TaskAction Action { get; set; }
    public required bool ReRun { get; set; }
}