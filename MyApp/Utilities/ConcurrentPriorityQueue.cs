// Utilities/ConcurrentPriorityQueue.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class ConcurrentPriorityQueue<T> : IConcurrentPriorityQueue<T> where T : ICustomTask
{
    private readonly SortedDictionary<Priority, Queue<T>> _queueDict = new SortedDictionary<Priority, Queue<T>>();
    private readonly object _lock = new object();

    public void Enqueue(Priority priority, T task)
    {
        lock (_lock)
        {
            if (!_queueDict.TryGetValue(priority, out var queue))
            {
                queue = new Queue<T>();
                _queueDict[priority] = queue;
            }

            queue.Enqueue(task);
            Monitor.PulseAll(_lock); // Notify waiting Dequeue threads
        }
    }

    public bool TryDequeue(out T task)
    {
        lock (_lock)
        {
            while (_queueDict.Count == 0)
            {
                Monitor.Wait(_lock); // Wait for items to be enqueued
            }

            var highestPriority = _queueDict.Keys.Max();
            var queue = _queueDict[highestPriority!];

            if (queue.TryDequeue(out task!))
            {
                if (task.ReRun)
                {
                    task.ReRun = false;
                    queue.Enqueue(task);
                }

                if (queue.Count == 0)
                {
                    _queueDict.Remove(highestPriority!);
                }
                return true;
            }

            return false;
        }
    }
}
