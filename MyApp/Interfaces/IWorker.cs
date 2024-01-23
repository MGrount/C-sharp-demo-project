public interface IWorker<T> : ITaskObserver<T> where T : ICustomTask {
    void Consume();
    void Produce(List<T> listOfTasks);
}