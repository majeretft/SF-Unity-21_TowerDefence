using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefence
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private TowerProperties[] _availableTowers;

        public TowerProperties[] AvailableTowers
        {
            get
            {
                return _availableTowers;
            }
            set
            {
                if (value == null || value.Length < 1)
                {
                    Destroy(transform.parent.gameObject);
                    // gameObject.SetActive(false);
                }

                _availableTowers = value;
            }
        }

        public static event Action<BuildSite> OnClick;

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClick(this);
        }

        public static void HideControls()
        {
            OnClick?.Invoke(null);
        }
    }
}
