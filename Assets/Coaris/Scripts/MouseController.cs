using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

        [SerializeField] float checkRadius;

        Vector2 mousePos;
        RaycastHit2D hit;
        bool isDraging;
        Shape nearestShape;
        int nearest = -1;

        void Update() {
                GetMousePosition();
                Click();
                MoveShapePoint();
                GetDragObject();
                Drag();
        }
        void OnDisable() {
                Shape.ClearShapeList();
        }
        void GetMousePosition() {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        #region Íø¸ñÅ¤Çú
        void Click() {
                if (Input.GetMouseButtonDown(0)) {
                        Shape.GetNearestPoint(mousePos, checkRadius, out nearest, out nearestShape);
                        if (nearestShape != null) {
                                if (nearestShape.CheckLocked(nearest)) {
                                        nearestShape = null;
                                        nearest = -1;
                                }
                        }

                }
                if (Input.GetMouseButtonUp(0)) {
                        nearestShape = null;
                        nearest = -1;
                }
        }
        void MoveShapePoint() {
                if (nearestShape != null) {

                        nearestShape.UpdateSpriteShape(nearest, mousePos);

                }
        }
        #endregion

        void GetDragObject() {
                if (Input.GetMouseButtonDown(0)) {
                        hit = Physics2D.Raycast(mousePos, Vector2.zero);
                        if (hit.collider != null) {
                                if (hit.transform.tag == "Dragon") {
                                        isDraging = true;
                                }
                                else if (hit.transform.tag == "Bird") {
                                        hit.transform.GetComponent<Scratch>().ReduceTimes();
                                }
                                else if (hit.transform.tag == "EggChin") {
                                        hit.transform.GetComponent<EggChin>().DisableChin();
                                }
                                else if (hit.transform.tag == "J") {
                                        isDraging = true;
                                }
                        }
                }
                if (Input.GetMouseButtonUp(0)) {
                        //if (hit.transform != null) {
                        //        hit.transform.parent.GetComponent<Drag>().BackDragPoint();
                        //}
                        isDraging = false;
                }
        }
        void Drag() {
                if (isDraging && hit.collider != null) {
                        hit.transform.position = new Vector3(mousePos.x, mousePos.y, hit.transform.position.z);
                        //Debug.Log(hit.collider);
                }
        }

}
