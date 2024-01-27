using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scratch : MonoBehaviour
{
        [SerializeField] int scratchTimes;
        [SerializeField] Transform Head;
        [SerializeField] Vector3 targetPos;
        bool isMoved;
        public void ReduceTimes() {
                if (scratchTimes <= 0) return;
                scratchTimes--;
        }
        void Move() {
                if (isMoved) return;
                if (scratchTimes == 0) {
                        Head.DOMove(targetPos, 0.5f);
                }
        }
        void Update() {
                Move();
        }
}
