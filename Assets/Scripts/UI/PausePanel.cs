using SpaceShooter;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TowerDefence
{
    public class PausePanel : SingletonBase<PausePanel>
    {
        [SerializeField]
        private Button _backButton;

        [SerializeField]
        private Button _continue;

        [SerializeField]
        private RectTransform _innerPanel;

        public UnityEvent OnBackButtonClick;
        public UnityEvent OnContinueButtonClick;

        private void Start()
        {
            Hide();
        }

        public void Back()
        {
            OnBackButtonClick?.Invoke();
        }

        public void Continue()
        {
            OnContinueButtonClick?.Invoke();
        }

        public void Show()
        {
            _innerPanel.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _innerPanel.gameObject.SetActive(false);
        }
    }
}
