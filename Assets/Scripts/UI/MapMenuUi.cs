using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class MapMenuUi : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _menuPanel;

        [SerializeField]
        private RectTransform _updatePanel;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_updatePanel.gameObject.activeSelf)
                    _updatePanel.gameObject.SetActive(false);
                else
                    _menuPanel.gameObject.SetActive(!_menuPanel.gameObject.activeSelf);
            }
        }

        public void ReturnToMainMenu()
        {
            LevelSequenceController.Instance.LoadMainMenu();
        }
    }
}
