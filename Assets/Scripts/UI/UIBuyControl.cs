using UnityEngine;

namespace TowerDefence
{
    public class UIBuyControl : MonoBehaviour
    {
        private RectTransform _uiTransform;

        private void Awake()
        {
            _uiTransform = GetComponent<RectTransform>();
            BuildSite.OnClick += MoveToBuildSite;
            gameObject.SetActive(false);
        }

        private void MoveToBuildSite(Transform buildSite)
        {
            if (!buildSite)
            {
                gameObject.SetActive(false);
                return;
            }

            var pos = Camera.main.WorldToScreenPoint(buildSite.position);
            _uiTransform.anchoredPosition = pos;

            gameObject.SetActive(true);

            foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
            {
                tbc.SetBuildSite(buildSite);
            }
        }
    }
}
