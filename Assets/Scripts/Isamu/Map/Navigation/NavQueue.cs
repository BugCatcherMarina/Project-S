using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Isamu.Map.Navigation
{
    
    // I was hoping to use PriorityQueue from the library, but apparently it wasn't a thing till .NET 6
    // It is the slowest possible implementation of a priority queue. If pathfinding ever becomes a bottleneck
    // it's a primary place for optimization.
    class NavQueue
    {
        class NodeWithPriority {
            public NavigationNode node;
            public int priority;
            public NodeWithPriority(NavigationNode node, int priority) 
            { 
                this.node = node;
                this.priority = priority;
            }
        }
        private List<NodeWithPriority> list;
        public int Count { get { return list.Count; } }

        public NavQueue()
        {
            list = new List<NodeWithPriority>();
        }

        public NavQueue(int count)
        {
            list = new List<NodeWithPriority>(count);
        }


        public void Enqueue(NavigationNode node, int priority)
        {
            NodeWithPriority element = new NodeWithPriority(node, priority);
            for (int i = 0; i < Count; i++)
            {
                if (list[i].priority <= priority)
                {
                    list.Insert(i, element);
                    return;
                }
            }
            list.Add(element);

        }

        public NavigationNode Dequeue()
        {
            var value = list[Count - 1];
            list.RemoveAt(Count - 1);
            return value.node;

        }
        private NodeWithPriority Peek()
        {
            Debug.Assert(Count != 0);
            return list[0];
        }

        public void Clear()
        {
            list.Clear();
        }
    }
}
