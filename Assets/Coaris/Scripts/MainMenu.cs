using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
        [SerializeField] int sceneNum;
        public void OnQuit() {
                Application.Quit();
        }
        public void OnStartGame() {
                SceneManager.LoadScene(sceneNum);
        }
}
