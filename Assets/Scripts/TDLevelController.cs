using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class TDLevelController : LevelController
    {
        public int levelScore = 1;

        private new void Start()
        {
            base.Start();

            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                ResultPanelController.Instance.ShowResults(false);
            };

            _levelCompletedEvent.AddListener(() =>
            {
                StopLevelActivity();
                UIMapCompletion.SaveEpisodeResult(levelScore);
            });


            PausePanel.Instance.OnBackButtonClick.AddListener(() => {
                PausePanel.Instance.Hide();
                LevelSequenceController.Instance.LoadMapLevel();
            });

            PausePanel.Instance.OnContinueButtonClick.AddListener(() => {
                PausePanel.Instance.Hide();
                ResumeLevelActivity();
            });
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

        private void ResumeLevelActivity()
        {
            foreach (var o in FindObjectsOfType<Enemy>())
            {
                o.GetComponent<Spaceship>().enabled = true;
            }

            EnableAll<Spawner>();
            EnableAll<Projectile>();
            EnableAll<Tower>();
        }

        private void DisableAll<T>() where T : MonoBehaviour
        {
            foreach (var o in FindObjectsOfType<T>())
            {
                o.enabled = false;
            }
        }

        private void EnableAll<T>() where T : MonoBehaviour
        {
            foreach (var o in FindObjectsOfType<T>())
            {
                o.enabled = true;
            }
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StopLevelActivity();
                PausePanel.Instance.Show();
            }
        }
    }
}
