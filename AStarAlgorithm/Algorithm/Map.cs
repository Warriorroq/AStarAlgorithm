using SFML.Graphics;
using SFML.System;
using AStarAlgorithm.GLoop;
using SFML.Window;

namespace AStarAlgorithm.Algorithm
{
    
    public class Map : GameObject
    {
        private Node[,] _nodes;
        private Vector2i _mapSize;
        private Vector2f _shapeSize;
        public Map(Vector2i mapSize, Vector2f shapeSize)
        {
            CreateShape(shapeSize);
            CreateNodes(mapSize);
            GlobalRenderVideo.GetRenderWindow().MouseMoved += OnMouseMove;
        }
        public override void Draw(RenderWindow render)
        {
            var index = new Vector2f();
            for (int x = 0; x < _mapSize.X; x++)
            {
                index.X = x;
                for (int y = 0; y < _mapSize.Y; y++)
                {
                    index.Y = y;
                    shape.Position = _shapeSize.Mul(index);

                    if (_nodes[x, y].state is NodeStates.isWalkable)
                        shape.OutlineColor = Color.White;
                    else
                        shape.OutlineColor = Color.Red;

                    render.Draw(shape);
                }
            }
        }
        public Node GetNode(Vector2f position)
        {
            var nodePosition = (Vector2i)position.Div(_shapeSize);
            if (!(nodePosition.X >= 0 && nodePosition.X < _mapSize.X))
                return null;

            if (!(nodePosition.Y >= 0 && nodePosition.Y < _mapSize.Y))
                return null;

            return _nodes[nodePosition.X, nodePosition.Y];
        }
        public Node GetNode(Vector2i position)
            => GetNode((Vector2f)position);
        public Vector2f PosByNode(Node node)
            => _shapeSize.Mul((Vector2f)node.mapPos);
        public List<Node> GetLocalNodes(Node node)
        {
            int x = node.mapPos.X > 0 ? -1 : 0;
            int lastX = node.mapPos.X < _mapSize.X - 1 ? 1 : 0;

            int startY = node.mapPos.Y > 0 ? -1 : 0;
            int lastY = node.mapPos.Y < _mapSize.Y - 1 ? 1 : 0;
            List<Node> neighbourNodes = new();
            for (; x <= lastX; x++)
            {
                for (int y = startY; y <= lastY; y++)
                {
                    if (x == 0 && y == 0)
                        continue;
                    var neighbourNode = _nodes[node.mapPos.X + x, node.mapPos.Y + y];
                    if(neighbourNode.state is NodeStates.isWalkable)
                        neighbourNodes.Add(_nodes[node.mapPos.X + x, node.mapPos.Y + y]);                  
                }
            }
            return neighbourNodes;
        }
        private void CreateNodes(Vector2i mapSize)
        {
            _mapSize = mapSize;
            _nodes = new Node[_mapSize.X, _mapSize.Y];
            for (int x = 0; x < _mapSize.X; x++)
                for (int y = 0; y < _mapSize.Y; y++)
                    _nodes[x, y] = new Node(new Vector2i(x, y));
        }
        private void CreateShape(Vector2f shapeSize)
        {
            _shapeSize = shapeSize;
            shape = new RectangleShape(shapeSize);
            shape.FillColor = Color.Black;
            shape.OutlineThickness = -5f;
        }
        private void OnMouseMove(object? sender, MouseMoveEventArgs e)
        {
            var mousePosition = Mouse.GetPosition(GlobalRenderVideo.GetRenderWindow());
            var node = GetNode(mousePosition);
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (node is null)
                    return;
                node.state = NodeStates.nonWalkable;
                if(Keyboard.IsKeyPressed(Keyboard.Key.LShift))
                {
                    foreach (var n in GetLocalNodes(node))
                       n.state = NodeStates.nonWalkable;
                }
            }
            else if(Mouse.IsButtonPressed(Mouse.Button.Right))
            {
                if (node is null)
                    return;
                node.state = NodeStates.isWalkable;
            }
            Console.WriteLine(node);
        }
    }
}
