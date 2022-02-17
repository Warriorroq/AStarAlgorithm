using SFML.Graphics;
using SFML.System;
namespace AStarAlgorithm.GLoop
{
    public class Scene
    {
        private List<GameObject> _gameObjects;
        private List<GameObject> _destroyedObjects;
        public Scene()
        {
            _gameObjects = new();
            _destroyedObjects = new();
        }
        public void AddObject(GameObject obj)
        {
            if (_gameObjects.Contains(obj))
                return;
            obj.state = GameObjectState.active;
            _gameObjects.Add(obj);
        }
        public void InstantiatePrefab(GameObject prefab, Vector2f position)
        {
            if (prefab is null)
                return;
            var clone = (prefab.Clone() as GameObject);
            clone.position = position;
            clone.state = GameObjectState.active;
            _gameObjects.Add(clone);
        }
        public T FindByType<T>() where T : GameObject
        {
            foreach (var obj in _gameObjects)
                if (obj.GetType() ==  typeof(T))
                    return obj as T;
            return default(T);
        }
        public void Update()
        {
            foreach(var gameObject in _gameObjects)
            {
                if(gameObject.state is GameObjectState.active)
                    gameObject.Update();
                if (gameObject.state is GameObjectState.destroyed)
                    _destroyedObjects.Add(gameObject);
            }
            if(_destroyedObjects.Count is not 0)
                ClearDestroyedGameObjects();
        }
        public void Draw(RenderWindow render)
        {
            foreach (var gameObject in _gameObjects)
                if(gameObject.state == GameObjectState.active)
                    gameObject.Draw(render);
        }
        private void ClearDestroyedGameObjects()
        {
            foreach(var gameObject in _destroyedObjects)
            {
                _gameObjects.Remove(gameObject);
                gameObject.Dispose();
            }
            _gameObjects.Clear();
        }
    }
}
