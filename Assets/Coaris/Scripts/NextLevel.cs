using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
        private void Update() {
                if (Input.GetKeyDown(KeyCode.D)) {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                if (Input.GetKeyDown(KeyCode.A)) {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                }
        }
}
