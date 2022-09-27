using System.Collections.Generic;
using UnityEngine;

namespace Isamu.Map.Navigation
{
    // I was hoping to use PriorityQueue from the library, but apparently it wasn't a thing till .NET 6
    internal class NavQueue
    {
        private class NodeWithPriority 
        {
            public readonly NavigationNode Node;
            public readonly int Priority;
            
            public NodeWithPriority(NavigationNode node, int priority) 
            { 
                Node = node;
                Priority = priority;
            }
        }

        public int Count => _list.Count;
        private readonly List<NodeWithPriority> _list;

        public NavQueue()
        {
            _list = new List<NodeWithPriority>();
        }

        public NavQueue(int count)
        {
            _list = new List<NodeWithPriority>(count);
        }

        public void Enqueue(NavigationNode node, int priority)
        {
            NodeWithPriority element = new NodeWithPriority(node, priority);   
            
            for (int i = 0; i < Count; i++) 
            {
                if (_list[i].Priority <= priority ) 
                {
                    _list.Insert(i, element);
                    return;
                }
            }
            
            _list.Add(element);
        }

        public NavigationNode Dequeue()
        {
            var value = _list[Count- 1];
            _list.RemoveAt(Count-1);
            return value.Node;
        }
        
        private NodeWithPriority Peek()
        {
            Debug.Assert(Count != 0);
            return _list[0];
        }

        public void Clear()
        {
            _list.Clear();
        }
    }
}
