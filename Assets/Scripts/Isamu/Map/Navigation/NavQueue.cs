using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Isamu.Map.Navigation
{
    
    // I was hoping to use PriorityQueue from the library, but apparently it wasn't a thing till .NET 6
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
            list.Add(new NodeWithPriority(node, priority));
            int i = Count - 1;

            while (i > 0)
            {
                int p = (i - 1) / 2;
                if (list[p].priority <= priority) break;

                list[i] = list[p];
                i = p;
            }

            if (Count > 0) list[i].priority = priority;
        }

        public NavigationNode Dequeue()
        {
            NodeWithPriority min = Peek();
            NodeWithPriority root = list[Count - 1];
            list.RemoveAt(Count - 1);

            int i = 0;
            while (i * 2 + 1 < Count)
            {
                int a = i * 2 + 1;
                int b = i * 2 + 2;
                int c = b < Count && list[b].priority < list[a].priority ? b : a;

                if (list[c].priority >= root.priority) break;
                list[i] = list[c];
                i = c;
            }

            if (Count > 0) list[i] = root;
            return min.node;
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
