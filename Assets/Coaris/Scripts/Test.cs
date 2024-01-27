using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Test : MonoBehaviour {
        SpriteShapeController _ssc;

        bool isTouching;
        Vector3 mousePos;
        Vector3 worldMousePos;

        Spline _spline;
        int pointCount;
        [SerializeField] float checkRadius;
        int nearest = -1;

        void Start() {
                _ssc = GetComponent<SpriteShapeController>();
                _spline = _ssc.spline;
                pointCount = _spline.GetPointCount();
        }
        void Update() {
                MouseClick();
                GetMousePosition();
                UpdateSpriteShape(worldMousePos);
        }
        void MouseClick() {
                if (Input.GetMouseButtonDown(0)) {
                        isTouching = true;
                        GetMousePosition();
                        if (GetNearestPoint(worldMousePos, checkRadius) != -1) {
                                //Debug.Log(GetNearestPoint(worldMousePos, checkRadius));
                                nearest = GetNearestPoint(worldMousePos, checkRadius);
                        }
                        //Debug.Log(isTouching);
                }

                if (Input.GetMouseButtonUp(0)) {
                        isTouching = false;
                        nearest = -1;
                        //Debug.Log(isTouching);
                }
        }

        void GetMousePosition() {
                if (!isTouching) return;
                mousePos = Input.mousePosition;
                worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z));
        }

        float GetDistance(Vector2 from, Vector2 to) {
                return Vector2.Distance(from, to);
        }

        int GetNearestPoint(Vector3 pos, float checkRadius) {
                int nearest = -1;
                for (int i = 0; i < pointCount; i++) {
                        if (nearest == -1) {
                                if (GetDistance(pos, _spline.GetPosition(i)) < checkRadius) {
                                        nearest = i;
                                }
                        }
                        else {
                                if (GetDistance(pos, _spline.GetPosition(i)) < GetDistance(pos, _spline.GetPosition(nearest)))
                                        nearest = i;
                        }
                }
                return nearest;
        }
        void UpdateSpriteShape(Vector3 pos) {
                if (!isTouching || nearest == -1) return;
                _spline.SetPosition(nearest, pos);
        }
}