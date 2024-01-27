using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggChin : MonoBehaviour {
        [SerializeField] GameObject Chin;
        public void DisableChin() {
                Chin.SetActive(false);
        }
}
