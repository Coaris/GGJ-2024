using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Shape : MonoBehaviour {
        SpriteShapeController _ssc;

        static bool isTouching;
        static Vector3 mousePos;
        static Vector3 worldMousePos;

        static List<Shape> shapeList;

        Spline spline;
        int pointCount;
        [SerializeField] float checkRadius;
        [SerializeField] int[] lockedPointIndex;
        static int nearest = -1;
        static Shape nearestShape = null;

        void Start() {
                if (shapeList == null) {
                        shapeList = new List<Shape>();
                        shapeList.Add(this);
                }
                else {
                        shapeList.Add(this);
                }
                _ssc = GetComponent<SpriteShapeController>();
                spline = _ssc.spline;
                pointCount = spline.GetPointCount();
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
                        GetNearestPoint(worldMousePos, checkRadius, out nearest, out nearestShape);
                        if (nearest != -1 && nearestShape != null) {
                                Debug.Log(nearestShape);
                                Debug.Log(nearest);
                        }
                        //Debug.Log(isTouching);
                }

                if (Input.GetMouseButtonUp(0)) {
                        isTouching = false;
                        nearestShape = null;
                        nearest = -1;
                        //Debug.Log(isTouching);
                }
        }

        void GetMousePosition() {
                if (!isTouching) return;
                mousePos = Input.mousePosition;
                worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z));
        }

        static float GetDistance(Vector2 from, Vector2 to) {
                return Vector2.Distance(from, to);
        }
        static void GetNearestPoint(Vector3 pos, float checkRadius, out int nearest, out Shape shape) {
                nearest = -1;
                shape = null;
                foreach (Shape _shape in shapeList) {
                        for (int i = 0; i < _shape.pointCount; i++) {
                                //�鿴�ÿ��Ƶ��Ƿ��������б���
                                for (int j = 0; j < _shape.lockedPointIndex.Length; j++) {
                                        if (i == _shape.lockedPointIndex[j]) {
                                                continue;
                                        }
                                }
                                //����������Ƶ�
                                if (nearestShape == null) {//���������Ҫ��Ҫ�ж�nearestShapeΪ��
                                        if (GetDistance(pos, _shape.spline.GetPosition(i)) < checkRadius) {
                                                nearestShape = _shape;
                                                nearest = i;
                                        }
                                }
                                else if (GetDistance(pos, _shape.spline.GetPosition(i)) < checkRadius) {
                                        if (GetDistance(pos, _shape.spline.GetPosition(i)) < GetDistance(pos, nearestShape.spline.GetPosition(nearest))) {
                                                nearestShape = _shape;
                                                nearest = i;
                                        }
                                }
                        }
                }
        }
        //int GetNearestPoint(Vector3 pos, float checkRadius) {
        //        int nearest = -1;
        //        Shape nearestShape = null;
        //        for (int i = 0; i < pointCount; i++) {
        //                //�鿴�ÿ��Ƶ��Ƿ��������б���
        //                for (int j = 0; j < lockedPointIndex.Length; j++) {
        //                        if (i == lockedPointIndex[j]) {
        //                                if (i == pointCount - 1) {
        //                                        return nearest;
        //                                }
        //                                else {
        //                                        i++;
        //                                }
        //                        }
        //                }
        //                //��������Ŀ��Ƶ�
        //                if (nearest == -1) {
        //                        if (GetDistance(pos, _spline.GetPosition(i)) < checkRadius) {
        //                                nearest = i;
        //                        }
        //                }
        //                else {
        //                        if (GetDistance(pos, _spline.GetPosition(i)) < GetDistance(pos, _spline.GetPosition(nearest)))
        //                                nearest = i;
        //                }
        //        }
        //        return nearest;
        //}


        void ReTangent(int i) {
                //�������߷���
                Vector3 tangent = Vector3.zero;
                Vector3 direction = Vector3.zero;
                if (i > 0 && i < pointCount - 1 && i > lockedPointIndex.Length - 1) {
                        Vector3 prevPoint = nearestShape.spline.GetPosition(i - 1);
                        Vector3 nextPoint = nearestShape.spline.GetPosition(i + 1);
                        direction = (nextPoint - prevPoint).normalized;

                }
                else if (i == 0 || i == pointCount - 1) {
                        if (i == 0) {
                                direction = (nearestShape.spline.GetPosition(i + 1) - nearestShape.spline.GetPosition(i)).normalized;
                        }
                        else {
                                direction = (nearestShape.spline.GetPosition(i) - nearestShape.spline.GetPosition(i - 1)).normalized;
                        }
                }
                else return;
                tangent = direction;

                // ���ÿ��Ƶ����߷���
                nearestShape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                nearestShape.spline.SetLeftTangent(i, -tangent);
                nearestShape.spline.SetRightTangent(i, tangent);
        }
        void UpdateSpriteShape(Vector3 pos) {
                if (!isTouching || nearest == -1) return;
                nearestShape.spline.SetPosition(nearest, pos);
                //spline.SetPosition(nearest, pos);
                ReTangent(nearest);
                ReTangent(nearest - 1);
                ReTangent(nearest + 1);
        }
}