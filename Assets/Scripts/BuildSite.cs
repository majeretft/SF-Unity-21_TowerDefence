using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefence
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        public static event Action<Transform> OnClick;

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClick(transform.root);
        }

        public static void HideControls()
        {
            OnClick?.Invoke(null);
        }
    }
}
