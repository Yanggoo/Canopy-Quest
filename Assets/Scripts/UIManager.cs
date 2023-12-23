using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RectTransform UI_Element;
    public RectTransform CanvasRect;
    public Transform trashBinPosition;
    public float xOffest;
    public float yOffset;
    public Text coinNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 viewPortPosition = Camera.main.WorldToViewportPoint(trashBinPosition.position);
        Vector2 worldObjectScreenPosition = new Vector2(viewPortPosition.x * CanvasRect.sizeDelta.x - 0.5f * CanvasRect.sizeDelta.x + xOffest,
            viewPortPosition.y * CanvasRect.sizeDelta.y - 0.5f * CanvasRect.sizeDelta.y + yOffset);
        UI_Element.anchoredPosition = worldObjectScreenPosition;
    }
}
