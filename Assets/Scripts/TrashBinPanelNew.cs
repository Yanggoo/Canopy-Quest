using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrashBinPanelNew : MonoBehaviour
{
    [SerializeField]
    private Image trashBinBar;
    [SerializeField]
    private Text coinText;
    public Transform trashBinPosition;
    private RectTransform CanvasRect;
    [SerializeField]
    private float xOffest;
    [SerializeField]
    private float yOffset;
    private RectTransform UI_Element;
    private void Awake()
    {
        UI_Element = GetComponent<RectTransform>();
        CanvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        Vector2 viewPortPosition;
        if (CameraFollow.isInMapMod)
        {
            viewPortPosition = new Vector2(2, 2);
        }
        else
        {
            viewPortPosition = Camera.main.WorldToViewportPoint(trashBinPosition.position);
        }
        Vector2 worldObjectScreenPosition = new Vector2(viewPortPosition.x * CanvasRect.sizeDelta.x - 0.5f * CanvasRect.sizeDelta.x + xOffest,
            viewPortPosition.y * CanvasRect.sizeDelta.y - 0.5f * CanvasRect.sizeDelta.y + yOffset);
        UI_Element.anchoredPosition = worldObjectScreenPosition;
    }
    public void UpdatePanelInfo(float fillamount, string text)
    {
        trashBinBar.fillAmount=fillamount;
        coinText.text = text;
        if (fillamount == 1.0f)
        {
            DisablePanel();
        }
    }
    public void DisablePanel()
    {
        gameObject.SetActive(false);
    }
}
