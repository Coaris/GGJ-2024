using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartAndQuit : MonoBehaviour
{
        public void OnRestart() {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void OnQuit() {
                Application.Quit();
        }
}
