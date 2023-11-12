using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CustomCursor : MonoBehaviour
{
    public Image cursorImage; // Assign your UI Image in the inspector
    public Image clickPoint; // Assign your UI Image in the inspector

    public int minX = -960, maxX = 960;
    public int minY = 0, maxY = 1080;
    public float mouseMoveSpeed = 10f;

    float mouseSpeedMin = 0.5f, mouseSpeedMax = 9f;

    private void Start() 
    {
        Cursor.visible = false;
    }

    private void Update() 
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        float cursorSpeed = Mathf.Clamp(mouseDelta.magnitude, mouseSpeedMin, mouseSpeedMax);
        
        cursorImage.rectTransform.anchoredPosition += mouseDelta.normalized * mouseMoveSpeed * cursorSpeed * Time.deltaTime;

        cursorImage.rectTransform.anchoredPosition = new Vector2(
            Mathf.Clamp(cursorImage.rectTransform.anchoredPosition.x, minX, maxX),
            Mathf.Clamp(cursorImage.rectTransform.anchoredPosition.y, minY, maxY));
        
        if (Input.GetMouseButtonDown(0)) {
            FakeClick();
        }
    }

    public void FakeClick()
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = GetScreenPositionFromScreenSpaceUI(cursorImage.rectTransform);
        GameRunner.currentInstance.effectController.ChopWoodEffect(clickPoint.rectTransform.position); // click here
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.TryGetComponent(out IPointerClickHandler clickHandler))
            {
                clickHandler.OnPointerClick(eventData);
                break; // Break after the first successful click
            }
        }
    }

    public Vector2 GetScreenPositionFromScreenSpaceUI(RectTransform uiElement)
    {
        Vector2 screenPoint = uiElement.anchoredPosition;

        screenPoint.x += uiElement.rect.width * uiElement.pivot.x;
        screenPoint.y += uiElement.rect.height * uiElement.pivot.y;

        screenPoint.x += Screen.width / 2;
        screenPoint.y += Screen.height / 2;

        Debug.Log(screenPoint);

        return screenPoint;
    }
}