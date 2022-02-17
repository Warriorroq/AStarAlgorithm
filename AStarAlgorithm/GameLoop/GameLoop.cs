using SFML.Graphics;
namespace AStarAlgorithm.GLoop
{
    public abstract class GameLoop
    {
        public const int FPS = 145;
        public Timer gameTime;
        protected GameLoop(string nameOfTheWindow)
        {

            GlobalRenderVideo.GetRenderWindow().Closed += WindowClosed;
            GlobalRenderVideo.SetRenderWindowName(nameOfTheWindow);
            gameTime = new Timer();
        }
        public void Start()
        {

            LoadContent();
            Init();
            gameTime.Init(FPS);
            GlobalRenderVideo.GetRenderWindow().SetFramerateLimit(FPS);
            while (GlobalRenderVideo.GetRenderWindow().IsOpen)
            {
                GlobalRenderVideo.GetRenderWindow().DispatchEvents();
                if (gameTime.IsUpdate())
                {
                    Update();

                    GlobalRenderVideo.GetRenderWindow().Clear(Color.Black);
                    Draw();
                    GlobalRenderVideo.GetRenderWindow().Display();
                }
            }
        }
        private void WindowClosed(object sender, EventArgs e)
            => GlobalRenderVideo.GetRenderWindow().Close();

        public abstract void LoadContent();
        public abstract void Init();
        public abstract void Update();
        public abstract void Draw();
    }
}