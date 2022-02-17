using SFML.System;

namespace AStarAlgorithm.Algorithm
{
    public class Node
    {
        public Node last;
        public NodeStates state;
        public int gCost;//start node
        public int hCost;//end node
        public int FCost => gCost + hCost;
        public Vector2i mapPos;
        public Node(Vector2i mapPos)
        {
            this.mapPos = mapPos;
        }
        public void CounthCost(Node end)
            => hCost = Price(end);
        public void CountgCost(Node start)
            => gCost = Price(start);
        public int Price(Node node)
        {
            var distance = node.mapPos - mapPos;
            distance = distance.Abs();
            if (distance.X > distance.Y)
                return distance.X * 10 + distance.Y * 4; //(distance.X - distance.Y) * 10 + distance.Y * 14
            else
                return distance.Y * 10 + distance.X * 4;
        }
        public int Price(Node start, Node end)
        {
            var distance = start.mapPos - end.mapPos;
            distance = distance.Abs();
            if (distance.X > distance.Y)
                return distance.X * 10 + distance.Y * 4;
            else
                return distance.Y * 10 + distance.X * 4;
        }
        public override string ToString()
            =>$"gCost:{gCost}|hCost:{hCost}";
    }
    public enum NodeStates
    {
        isWalkable = 0,
        nonWalkable = 1
    }
}
