using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
        public int sceneIndex;
        public void OnStartGame() {
                SceneManager.LoadScene(sceneIndex);
        }
        public void OnQuit() {
                Application.Quit();
        }
}
