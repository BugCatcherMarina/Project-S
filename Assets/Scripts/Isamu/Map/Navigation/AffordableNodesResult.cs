using System.Collections.Generic;

namespace Isamu.Map.Navigation
{
    public class AffordableNodesResult
    {
        public List<NavigationNode> Nodes { get; }
        public List<int> Costs { get; }

        public AffordableNodesResult(List<NavigationNode> nodes, List<int> costs)
        {
            Nodes = nodes;
            Costs = costs;
        }
    }
}
