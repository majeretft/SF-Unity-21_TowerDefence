using UnityEngine;

namespace TowerDefence
{
    public class LevelInit : MonoBehaviour
    {
        private void Start()
        {
            SoundPlayer.Instance.PlayLevelMusic();
        }
    }
}
