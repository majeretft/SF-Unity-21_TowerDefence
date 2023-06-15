using System;
using SpaceShooter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerDefence
{
    public class ClickProtectionUI : SingletonBase<ClickProtectionUI>, IPointerClickHandler
    {
        private Image _blocker;
        private Action<Vector2> _onClickAction;

        private void Start()
        {
            _blocker = GetComponent<Image>();
            _blocker.enabled = false;
        }

        public void Activate(Action<Vector2> mouseAction)
        {
            _blocker.enabled = true;
            _onClickAction = mouseAction;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _blocker.enabled = false;
            _onClickAction?.Invoke(eventData.pressPosition);
            _onClickAction = null;
        }
    }
}
