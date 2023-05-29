using UnityEngine;

namespace SpaceShooter
{
    public class ImpactEffect : MonoBehaviour
    {
        [SerializeField]
        private float _lifeSpan;

        private float _timer;

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > _lifeSpan)
                Destroy(gameObject);
        }
    }
}
