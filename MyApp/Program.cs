using System;

namespace MyApp
{
    // Program.cs
    class Program
    {
        static void Main()
        {
            ITaskQueue<ICustomTask> taskQueue = new TaskQueue<ICustomTask>();

            Worker<ICustomTask> workerConsumer = new Worker<ICustomTask>(taskQueue);
            Worker<ICustomTask> workerProducer = new Worker<ICustomTask>(taskQueue);

            var consumerTask = Task.Run(() => workerConsumer.Consume());

            List<ICustomTask> listOfTasks =
            [
                new CustomTask
                {
                    Description = "Task 1",
                    Priority = Priority.High,
                    ReRun = true,
                    Action = () => { Console.WriteLine("1"); }
                },
                new CustomTask
                {
                    Description = "Task 2",
                    Priority = Priority.Medium,
                    ReRun = false,
                    Action = () => { Console.WriteLine("2"); }
                },
                new CustomTask
                {
                    Description = "Task 3",
                    Priority = Priority.Low,
                    ReRun = true,
                    Action = () => { Console.WriteLine("3"); }
                },
                new CustomTask
                {
                    Description = "Task 4",
                    Priority = Priority.High,
                    ReRun = false,
                    Action = () => { Console.WriteLine("4"); }
                },
                new CustomTask
                {
                    Description = "Task 5",
                    Priority = Priority.Low,
                    ReRun = false,
                    Action = () => { Console.WriteLine("5"); }
                },
                new CustomTask
                {
                    Description = "Task 6",
                    Priority = Priority.High,
                    ReRun = false,
                    Action = () => { Console.WriteLine("6"); }
                },
            ];

            var producerTask = Task.Run(() => workerProducer.Produce(listOfTasks));

            Task.WaitAll(producerTask, consumerTask);
        }
    }
}
