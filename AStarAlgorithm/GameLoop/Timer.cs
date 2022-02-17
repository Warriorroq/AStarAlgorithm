using SFML.System;
namespace AStarAlgorithm.GLoop
{
    public class Timer
    {
        public float deltaTime;
        public float timeScale;
        public event Action<float> timeUpdate;

        private float TotalTimeElapsed => _clock.ElapsedTime.AsSeconds();
        private float _totalTimeBeforeUpdate;
        private float _updateTime;
        private float _previosTimeElapsed;
        private Clock _clock;
        public Timer()
        {
            deltaTime = 0f;
            _totalTimeBeforeUpdate = 0f;
            _previosTimeElapsed = 0f;
            timeScale = 1f;
            _clock = new Clock();
        }
        public void Init(float fps)
            => _updateTime = 1f / fps;
        public bool IsUpdate()
        {
            _totalTimeBeforeUpdate += TotalTimeElapsed - _previosTimeElapsed;
            _previosTimeElapsed = TotalTimeElapsed;
            if (_totalTimeBeforeUpdate >= _updateTime)
            {
                deltaTime = _totalTimeBeforeUpdate * timeScale;
                timeUpdate?.Invoke(deltaTime);
                _totalTimeBeforeUpdate = 0;
                return true;
            }
            return false;
        }        
    }
}