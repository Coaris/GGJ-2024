using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour {
        public GameObject body;
        public GameObject bandage;
        public GameObject mouth;
        public Animator anim;
        void Update() {
                IsOK();
        }
        void IsOK() {
                if (Input.GetKeyDown(KeyCode.E)) {
                        body.SetActive(false);
                        bandage.SetActive(false);
                        mouth.SetActive(false);
                        anim.Play("change");
                }
        }
}
