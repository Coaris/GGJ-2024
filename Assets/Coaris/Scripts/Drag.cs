using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

        [SerializeField] Transform basePoint;
        [SerializeField] Transform dragPoint;
        [SerializeField] DragWay dragWay;
        //[SerializeField] Transform sprite;

        Vector2 direction;
        float distance;

        void Start() {
                distance = Vector3.Distance(dragPoint.position, basePoint.position);
        }
        void Update() {
                Rotation();
                Move();
        }
        void Move() {
                if (dragWay != DragWay.move) return;
                basePoint.position = dragPoint.position;
        }
        void Rotation() {
                if (dragWay != DragWay.rotate) return;
                direction = dragPoint.position - basePoint.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                basePoint.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }
        //public void BackDragPoint() {
        //        dragPoint.position = basePoint.position + (Vector3)direction * distance;
        //}
}
enum DragWay {
        rotate, move
}