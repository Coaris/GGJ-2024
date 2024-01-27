using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

        [SerializeField] float checkRadius;

        Vector2 mousePos;
        Shape nearestShape;
        int nearest = -1;

        void Update() {
                GetMousePosition();
                Click();
                MovePoint();
        }
        void GetMousePosition() {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
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
        void MovePoint() {
                if (nearestShape != null) {
                        
                        nearestShape.UpdateSpriteShape(nearest, mousePos);
                        
                }
        }
}
