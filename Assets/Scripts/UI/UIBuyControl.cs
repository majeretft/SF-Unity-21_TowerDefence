using UnityEngine;
using System.Collections.Generic;
using System;

namespace TowerDefence
{
    public class UIBuyControl : MonoBehaviour
    {
        [SerializeField]
        private TowerBuyControl _prefab;

        private List<TowerBuyControl> _activeControl;

        private RectTransform _uiTransform;

        private void Awake()
        {
            _uiTransform = GetComponent<RectTransform>();
            BuildSite.OnClick += MoveToBuildSite;
            gameObject.SetActive(false);
        }

        private void MoveToBuildSite(BuildSite buildSite)
        {
            if (buildSite)
            {
                _activeControl = new List<TowerBuyControl>();

                foreach (var prop in buildSite.AvailableTowers)
                {
                    if (prop.IsAvailable())
                    {
                        print(transform.name);
                        var ctrl = Instantiate(_prefab, transform);
                        ctrl.SetProps(prop);
                        _activeControl.Add(ctrl);
                    }
                }

                if (_activeControl.Count > 0)
                {
                    var pos = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);
                    _uiTransform.anchoredPosition = pos;

                    var angle = 360 / _activeControl.Count;
                    for (int i = 0; i < _activeControl.Count; i++)
                    {
                        var rotation = Quaternion.AngleAxis(angle * i, Vector3.forward);
                        var offset = rotation * Vector3.left * 120;
                        _activeControl[i].transform.position += offset;
                    }

                    foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
                    {
                        tbc.SetBuildSite(buildSite.transform.root);
                    }

                    gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (var ctrl in _activeControl)
                    Destroy(ctrl.gameObject);
                _activeControl.Clear();
                gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            BuildSite.OnClick -= MoveToBuildSite;
        }
    }
}
