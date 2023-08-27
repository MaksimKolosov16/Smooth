using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{
    public static class MovingMaxTask
    {
        public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
        {
            var listOfPotentialMaxes = new LinkedList<double>();
            var queueInWindow = new Queue<double>();
            foreach (var point in data)
            {
                queueInWindow.Enqueue(point.OriginalY);
                if (queueInWindow.Count > windowWidth)
                {
                    if (queueInWindow.Dequeue() == listOfPotentialMaxes.First.Value)
                    {
                        listOfPotentialMaxes.RemoveFirst();
                    }
                }
                while (listOfPotentialMaxes.Last != null && point.OriginalY > listOfPotentialMaxes.Last.Value)
                {
                    listOfPotentialMaxes.RemoveLast();
                }
                listOfPotentialMaxes.AddLast(point.OriginalY);
                yield return point.WithMaxY(listOfPotentialMaxes.First.Value);;
            }
        }
    }
}