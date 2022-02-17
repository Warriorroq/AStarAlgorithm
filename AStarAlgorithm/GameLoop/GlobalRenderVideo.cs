using SFML.Graphics;
using SFML.Window;
namespace AStarAlgorithm.GLoop
{
    public static class GlobalRenderVideo
    {
        public static void SetRenderWindowName(string newName)
        {
            if (_window is null)
                throw new Exception("");
            _window.SetTitle(newName);
        }
        public static RenderWindow GetRenderWindow()
        {
            if (_window is null)
                _window = new RenderWindow(_windowMode," ");
            return _window;
        }
        private static RenderWindow _window = null;
        private static VideoMode _windowMode = new VideoMode(1080, 720);
    }
}
