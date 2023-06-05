using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefence
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private string _mapSceneName = "Level_Map";

        [SerializeField]
        private Button _continueButton;

        private void Start()
        {
            _continueButton.interactable = FileHandler.HasFile(UIMapCompletion.FILE_NAME);
        }

        public void NewGame()
        {
            FileHandler.Reset(UIMapCompletion.FILE_NAME);
            Continue();
        }

        public void Continue()
        {
            SceneManager.LoadScene(_mapSceneName);
        }

        public void Quit()
        {
#if UNITY_EDITOR
            if (Application.isEditor && Application.isPlaying)
            {
                EditorApplication.ExitPlaymode();
            }
#endif
            Application.Quit();
        }
    }
}
