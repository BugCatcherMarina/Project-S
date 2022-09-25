namespace Isamu.Map.Navigation
{
    public class PathRequestInput
    {
        public NavigationNode From { get; }
        public NavigationNode To { get; }

        public PathRequestInput(NavigationNode from, NavigationNode to)
        {
            From = from;
            To = to;
        }
    }
}
