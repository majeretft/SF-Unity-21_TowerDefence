using UnityEngine;

namespace TowerDefence
{
    public class MenuInit : MonoBehaviour
    {
        private void Start()
        {
            SoundPlayer.Instance.PlayMenuMusic();
        }
    }
}
