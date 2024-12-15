using UnityEngine;
using UnityEngine.EventSystems;

public class TrackMouseArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static TrackMouseArea instance;
    public bool isMouseOver;

    private void Awake() {
        instance=this;
    }
    // This method is called when the mouse pointer enters the UI element
    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    // This method is called when the mouse pointer exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }

    // Public method to check if the mouse is currently over the element
    public bool IsMouseOver()
    {
        return isMouseOver;
    }
}
