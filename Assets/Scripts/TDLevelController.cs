using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class TDLevelController : LevelController
    {
        private int _levelScore = 3;

        private new void Start()
        {
            base.Start();

            _referenceTime += Time.time;

            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                ResultPanelController.Instance.ShowResults(false);
            };

            _levelCompletedEvent.AddListener(() =>
            {
                StopLevelActivity();

                if (_referenceTime <= Time.time)
                    _levelScore--;

                UIMapCompletion.SaveEpisodeResult(_levelScore);
            });

            PausePanel.Instance.OnBackButtonClick.AddListener(() =>
            {
                PausePanel.Instance.Hide();
                LevelSequenceController.Instance.LoadMapLevel();
            });

            PausePanel.Instance.OnContinueButtonClick.AddListener(() =>
            {
                PausePanel.Instance.Hide();
                ResumeLevelActivity();
            });

            void LifeScoreChange(int _)
            {
                _levelScore--;
                TDPlayer.Instance.OnHpUpdate -= LifeScoreChange;
            }

            TDPlayer.Instance.OnHpUpdate += LifeScoreChange;
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
            DisableAll<NextWaveUI>();
            DisableAll<EnemyWave>();
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
            EnableAll<NextWaveUI>();
            EnableAll<EnemyWave>();
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
