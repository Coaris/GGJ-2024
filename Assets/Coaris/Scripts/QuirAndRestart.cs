using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuirAndRestart : MonoBehaviour {
        public void OnQuit() {
                Application.Quit();
        }
        public void OnRestart() {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
}
