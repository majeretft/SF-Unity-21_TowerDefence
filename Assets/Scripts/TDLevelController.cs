using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class TDLevelController : LevelController
    {
        private new void Start()
        {
            base.Start();

            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                ResultPanelController.Instance.ShowResults(false);
            };

            _levelCompletedEvent.AddListener(StopLevelActivity);
        }

        private void StopLevelActivity()
        {
            foreach (var o in FindObjectsOfType<Enemy>())
            {
                o.GetComponent<Spaceship>().enabled = false;
                o.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            DisableAll<Spawner>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
        }

        private void DisableAll<T>() where T : MonoBehaviour
        {
            foreach (var o in FindObjectsOfType<T>())
            {
                o.enabled = false;
            }
        }
    }
}
