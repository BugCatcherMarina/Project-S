using System.Collections.Generic;

namespace Isamu.Map.Navigation
{
    public class PathRequestResult
    {
        public List<NavigationNode> Nodes { get; }

        public int NodeCount { get; }

        public PathRequestResult(List<NavigationNode> nodes)
        {
            Nodes = nodes;
            NodeCount = nodes.Count;
        }
    }
}
