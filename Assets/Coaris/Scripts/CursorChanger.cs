using UnityEngine;

public class CursorManager : MonoBehaviour {
        // ������������Ĳ�ͬ�����ʽ������
        public Texture2D normalCursor;
        public Texture2D clickCursor;

        void Start() {
                // ����Ĭ�Ϲ����ʽ
                Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        }

        private void Update() {
                if (Input.GetMouseButtonDown(0)) {
                        Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.Auto);
                }
                if (Input.GetMouseButtonUp(0)) {
                        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
                }
        }
}

