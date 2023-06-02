using UnityEngine.EventSystems;

namespace TowerDefence
{
    public class BuildSiteNull : BuildSite
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            HideControls();
        }
    }
}
