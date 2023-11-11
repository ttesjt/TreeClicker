using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public Texture2D cursorTexture; // Drag your cursor image here in the inspector
    public Vector2 hotSpot = Vector2.zero; // Set the point of the cursor that will click things

    void Start()
    {
        // Apply the custom cursor texture
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    void Update() {
        // Mouse.current.WarpCursorPosition(new Vector2(123, 234));
    }
}