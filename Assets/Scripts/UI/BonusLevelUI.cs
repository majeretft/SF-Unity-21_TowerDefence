using TMPro;
using UnityEngine;

namespace TowerDefence
{
    [RequireComponent(typeof(UIMapLevel))]
    public class BonusLevelUI : MonoBehaviour
    {
        [SerializeField]
        private UIMapLevel _parentLevel;

        [SerializeField]
        private int _minPoints = 3;

        [SerializeField]
        private TextMeshProUGUI _valueText;

        public void TryActivate()
        {
            gameObject.SetActive(_parentLevel.Score > 0);

            if (_minPoints > UIMapCompletion.Instance.ScoreTotal)
            {
                _valueText.text = _minPoints.ToString();
            }
            else
            {
                _valueText.transform.parent.parent.gameObject.SetActive(false);
                GetComponent<UIMapLevel>().Initialize(_parentLevel);
            }
        }
    }
}
