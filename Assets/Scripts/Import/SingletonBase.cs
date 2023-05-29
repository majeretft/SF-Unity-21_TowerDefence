using UnityEngine;

namespace SpaceShooter
{
    public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        [Header("Singleton")]
        [SerializeField]
        private bool _doDonDestroyOnLoad;

        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance)
            {
                Debug.LogWarning($"Singleton: Object of type {typeof(T).Name} already exists, instance will be destroyed");
                Destroy(this);
                return;
            }

            Instance = this as T;

            if (_doDonDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
    }
}
