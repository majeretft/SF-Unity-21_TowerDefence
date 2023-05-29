using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SpaceShooter
{
    public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool _isHold;
        public bool IsHold => _isHold;
        

        public void OnPointerDown(PointerEventData eventData)
        {
            _isHold = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isHold = false;
        }
    }
}
