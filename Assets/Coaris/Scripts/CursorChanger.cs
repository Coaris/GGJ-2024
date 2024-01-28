using UnityEngine;

public class CursorManager : MonoBehaviour {
        // 在这里声明你的不同光标样式的纹理
        public Texture2D normalCursor;
        public Texture2D clickCursor;

        void Start() {
                // 设置默认光标样式
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

