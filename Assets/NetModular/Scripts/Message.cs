using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] Color friendColor;
    [SerializeField] Text message;
    [SerializeField] Image image;

    public Vector2 leftMidAnchor = new Vector2(0, 0.5f);
    public Vector2 rightMidAnchor = new Vector2(1, 0.5f);
    public Vector2 rightMidPosition = new Vector2(-850, 0);
    RectTransform rectTransform;

    public void ShowMessage(object msg, bool isMine)
    {
        rectTransform = image.GetComponent<RectTransform>();
        message.text = msg.ToString();
        image.color = isMine ? color : friendColor;
        rectTransform.anchoredPosition = isMine ? rightMidPosition : Vector2.zero;
        rectTransform.anchorMin = rectTransform.anchorMax = isMine ? rightMidAnchor : leftMidAnchor;
    }
}
