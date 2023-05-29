using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image _joystickBg;
    [SerializeField] private Image _stick;

    public Vector3 Value { get; protected set; }

    public void OnDrag(PointerEventData eventData)
    {
        var position = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _joystickBg.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out position
        );

        position.x = position.x / _joystickBg.rectTransform.sizeDelta.x * 2;
        position.y = position.y / _joystickBg.rectTransform.sizeDelta.y * 2;

        if (position.sqrMagnitude > 1)
            position.Normalize();

        
        var offsetX = _joystickBg.rectTransform.sizeDelta.x / 2 - _stick.rectTransform.sizeDelta.x / 2;
        var offsetY = _joystickBg.rectTransform.sizeDelta.y / 2 - _stick.rectTransform.sizeDelta.y / 2;

        Value = new Vector3(position.x, position.y, 0);

        _stick.rectTransform.anchoredPosition = new Vector3(position.x * offsetX, position.y * offsetY, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Value = Vector3.zero;
        _stick.rectTransform.anchoredPosition = Value;
    }
}
