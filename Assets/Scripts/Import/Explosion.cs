using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _explosionFinishedEvent;
        public UnityEvent ExplosionFinishedEvent => _explosionFinishedEvent;

        private void Awake()
        {
            var animatorEvents = GetComponentInChildren<AnimationEventEmitter>();

            animatorEvents.AnimationEndedEvent.AddListener(HandleAnimationEndedEvent);
        }

        private void HandleAnimationEndedEvent()
        {
            ExplosionFinishedEvent?.Invoke();
            Destroy(gameObject);
        }

        public void SetTransform(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }
    }
}
