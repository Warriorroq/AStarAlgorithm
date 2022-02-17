using SFML.System;
using SFML.Graphics;
using AStarAlgorithm.GLoop;
using AStarAlgorithm.Algorithm;
namespace AStarAlgorithm
{
    public class Game : GameLoop
    {
        public Scene scene;
        public Game(string nameOfTheWindow) : base(nameOfTheWindow) { }
        public override void Draw()
        {
            var render = GlobalRenderVideo.GetRenderWindow();
            scene.Draw(render);
        }

        public override void Init()
        {
            scene = new();
            var map = new Map(new Vector2i(54, 36), new Vector2f(20, 20));
            var pathfinder = new PathFinder();
            scene.AddObject(map);
            scene.AddObject(pathfinder);
            pathfinder.StartFindingPath(map);
        }

        public override void LoadContent()
        {

        }

        public override void Update()
        {
            scene.Update();
        }
    }
}
