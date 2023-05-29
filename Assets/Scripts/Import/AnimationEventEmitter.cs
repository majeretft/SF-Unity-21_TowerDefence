using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class AnimationEventEmitter : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _animationStartedEvent;
        public UnityEvent AnimationStartedEvent => _animationStartedEvent;


        [SerializeField]
        private UnityEvent _animationEndedEvent;
        public UnityEvent AnimationEndedEvent => _animationEndedEvent;

        protected void FireAnimationStartedEvent()
        {
            _animationStartedEvent?.Invoke();
        }


        protected void FireAnimationEndedEvent()
        {
            _animationEndedEvent?.Invoke();
        }
    }
}
