using UnityEngine;

namespace TowerDefence
{
    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField]
        private UIMapLevel[] levels;

        private void Start()
        {
            var drawLevel = 0;
            var score = 1;

            while (score != 0
                && drawLevel < levels.Length
                && UIMapCompletion.Instance.TryIndex(drawLevel, out var episode, out score))
            {
                levels[drawLevel].SetLevelData(episode, score);
                drawLevel++;
            }

            for (int i = drawLevel; i < levels.Length; i++)
            {
                levels[i].gameObject.SetActive(false);
            }
        }
    }
}
