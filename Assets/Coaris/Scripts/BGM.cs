using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {

        static BGM instance;
        void Awake() {
                if (instance == null) {
                        instance = this;
                }
                else if (instance != this) {
                        Destroy(gameObject);
                }
                DontDestroyOnLoad(gameObject);
        }
}
