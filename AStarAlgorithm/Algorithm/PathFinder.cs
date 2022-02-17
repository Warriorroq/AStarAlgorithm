using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AStarAlgorithm.GLoop;
using SFML.Graphics;
using SFML.Window;

namespace AStarAlgorithm.Algorithm
{
    public class PathFinder : GameObject
    {
        private Map _map;
        private PriorityQueue<Node, int> _openNodes;
        private HashSet<Node> _closedNodes;
        private Node _start;
        private Node _end;
        private Node _current;
        private Node _path;
        public PathFinder()
        {
            GlobalRenderVideo.GetRenderWindow().MouseMoved += WindowMouseMoved;
        }

        private void WindowMouseMoved(object? sender, MouseMoveEventArgs e)
        {
            var mousePosition = Mouse.GetPosition(GlobalRenderVideo.GetRenderWindow());
            if (Keyboard.IsKeyPressed(Keyboard.Key.E))
            {
                _end = _map.GetNode(mousePosition);
                _current = _start;
                _openNodes.Clear();
                _closedNodes.Clear();
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                _start = _map.GetNode(mousePosition);
                _current = _start;
                _openNodes.Enqueue(_start, _start.FCost);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.C))
            {
                _current = _start;
                _openNodes.Clear();
                _closedNodes.Clear();
            }
        }

        public override void Draw(RenderWindow render)
        {
            if(_end is not null)
                render.Draw(GetShapeByNode(_end, Color.Cyan));
            if(_start is not null)
                render.Draw(GetShapeByNode(_start, Color.Magenta));
            foreach(var node in _closedNodes)
                render.Draw(GetShapeByNode(node, Color.Green));
        }
        private Shape GetShapeByNode(Node node, Color color)
        {
            _map.shape.OutlineColor = color;
            _map.shape.Position = _map.PosByNode(node);
            return _map.shape;
        }
        public void Step()
        {
           if (_current == _end)
            {
                Console.WriteLine("Completed");
                return;
            }
            var neigbours = _map.GetLocalNodes(_current);
            foreach(var neigbour in neigbours)
            {
                neigbour.CounthCost(_end);

                if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift))
                    neigbour.CountgCost(_start);//better without this
                if (!_closedNodes.Contains(_current))
                    _openNodes.Enqueue(neigbour, neigbour.FCost);
            }
            if(!_closedNodes.Contains(_current))
                _closedNodes.Add(_current);
            if(_openNodes.Count == 0)
            {
                Console.WriteLine("NO ways");
                return;
            }
            _current = _openNodes.Dequeue();
        }
        public void StartFindingPath(Map map)
        {
            _map = map;
            _openNodes = new();
            _closedNodes = new();
        }
        protected override void OnUpdate()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                 Step();
        }
    }
}
