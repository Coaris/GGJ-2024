using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Shape : MonoBehaviour {
        SpriteShapeController _ssc;
        static List<Shape> shapeList=new List<Shape>();

        Spline spline;
        int pointCount;
        [SerializeField] int[] lockedPointIndex;

        void Start() {
                if (shapeList == null) {
                        //shapeList = new List<Shape>();
                        shapeList.Add(this);
                }
                else {
                        shapeList.Add(this);
                }
                _ssc = GetComponent<SpriteShapeController>();
                spline = _ssc.spline;
                pointCount = spline.GetPointCount();
        }
        public static void ClearShapeList() {
                shapeList.Clear();
        }

        static float GetDistance(Vector2 from, Vector2 to) {
                return Vector2.Distance(from, to);
        }
        public static void GetNearestPoint(Vector3 pos, float checkRadius, out int nearest, out Shape nearestShape) {
                Vector3 checkPos;
                nearest = -1;
                nearestShape = null;
                if (shapeList.Count == 0) return;
                foreach (Shape _shape in shapeList) {

                        for (int i = 0; i < _shape.pointCount; i++) {
                                checkPos = pos - _shape.transform.position;
                                //查找最近控制点
                                if (nearestShape == null) {//这里的条件要不要判断nearestShape为空
                                        if (GetDistance(checkPos, _shape.spline.GetPosition(i)) < checkRadius) {
                                                nearestShape = _shape;
                                                nearest = i;
                                        }
                                }
                                else if (GetDistance(checkPos, _shape.spline.GetPosition(i)) < checkRadius) {
                                        if (GetDistance(checkPos, _shape.spline.GetPosition(i)) < GetDistance(checkPos, nearestShape.spline.GetPosition(nearest))) {
                                                nearestShape = _shape;
                                                nearest = i;
                                        }
                                }

                        }
                }
                ////查看该控制点是否在锁定列表里
                //if (nearestShape != null) {
                //        for (int j = 0; j < nearestShape.lockedPointIndex.Length - 1; j++) {
                //                if (nearest == nearestShape.lockedPointIndex[j]) {
                //                        nearestShape = null;
                //                        nearest = -1;
                //                }
                //        }
                //}
        }
        public bool CheckLocked(int _i) {
                bool isLocked = false;
                //Debug.Log(_shape.lockedPointIndex.Length);
                foreach (int j in lockedPointIndex) {
                        if (_i == lockedPointIndex[j]) {
                                isLocked = true;
                                break;
                        }
                }
                return isLocked;
        }

        void ReTangent(int i) {
                //计算切线方向
                Vector3 tangent = Vector3.zero;
                Vector3 direction = Vector3.zero;
                if (i > 0 && i < pointCount - 1 && i > lockedPointIndex.Length - 1) {
                        Vector3 prevPoint = spline.GetPosition(i - 1);
                        Vector3 nextPoint = spline.GetPosition(i + 1);
                        direction = (nextPoint - prevPoint).normalized;

                }
                else if (i == 0 || i == pointCount - 1) {
                        if (i == 0) {
                                direction = (spline.GetPosition(i + 1) - spline.GetPosition(i)).normalized;
                        }
                        else {
                                direction = (spline.GetPosition(i) - spline.GetPosition(i - 1)).normalized;
                        }
                }
                else return;
                tangent = direction;

                // 设置控制点切线方向
                spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                spline.SetLeftTangent(i, -tangent);
                spline.SetRightTangent(i, tangent);
        }
        public void UpdateSpriteShape(int i, Vector3 pos) {
                spline.SetPosition(i, pos - transform.position);
                //ReTangent(i);
                //ReTangent(i - 1);
                //ReTangent(i + 1);
        }
}