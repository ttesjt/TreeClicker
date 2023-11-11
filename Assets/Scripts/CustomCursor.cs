using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CustomCursor : MonoBehaviour
{
    public Image cursorImage; // Assign your UI Image in the inspector
    public int minX = -960, maxX = 960;
    public int minY = 0, maxY = 1080;
    public float mouseMoveSpeed = 10f;

    private void Start() 
    {
        // Cursor.visible = false;
    }

    private void Update() 
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        

        // Update the cursor's position based on the mouse delta
        cursorImage.rectTransform.anchoredPosition += mouseDelta.normalized * mouseMoveSpeed * Time.deltaTime;

        // Optionally, clamp the position to ensure it stays within screen bounds
        // You will need to calculate minX, maxX, minY, maxY based on your canvas and cursor image size
        cursorImage.rectTransform.anchoredPosition = new Vector2(
            Mathf.Clamp(cursorImage.rectTransform.anchoredPosition.x, minX, maxX),
            Mathf.Clamp(cursorImage.rectTransform.anchoredPosition.y, minY, maxY));
    }

    public void FakeClick()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = cursorImage.rectTransform.position
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.TryGetComponent(out IPointerClickHandler clickHandler))
            {
                clickHandler.OnPointerClick(pointerData);
                break; // Break after the first successful click
            }
        }
    }
}