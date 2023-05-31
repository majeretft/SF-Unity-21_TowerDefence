using UnityEngine;

namespace TowerDefence
{
    public class StandUp : MonoBehaviour
    {
        private Rigidbody2D _rigitBody;
        private SpriteRenderer _renderer;

        private void Start()
        {
            _rigitBody = transform.root.GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            transform.up = Vector2.up;

            if (_rigitBody.velocity.x > 0.01f)
                _renderer.flipX = false;
            if (_rigitBody.velocity.x < -0.01f)
                _renderer.flipX = true;
        }
    }
}
