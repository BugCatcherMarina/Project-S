namespace Isamu.Map.Navigation
{
    public class AffordableNodesRequest
    {
        public NavigationNode Start { get; }
        public int MaxCost { get; }
        public bool GoOverBlocked { get; }

        public AffordableNodesRequest(NavigationNode start, int maxCost, bool goOverBlocked = false)
        {
            Start = start;
            MaxCost = maxCost;
            GoOverBlocked = goOverBlocked;
        }
    }
}
