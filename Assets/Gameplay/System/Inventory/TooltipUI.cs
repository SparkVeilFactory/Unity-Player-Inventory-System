using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;

    public GameObject tooltipObject;
    public TextMeshProUGUI tooltipText;
    public Vector2 offset = new Vector2(20f, -20f); // 👈 OVDJE MIJENJAŠ

    private RectTransform tooltipRect;

    private void Awake()
    {
        Instance = this;
        tooltipRect = tooltipObject.GetComponent<RectTransform>();
        tooltipObject.SetActive(false);
    }

    public void Show(string text, Vector2 screenPosition)
    {
        tooltipText.text = text;
        tooltipObject.SetActive(true);
        UpdatePosition(screenPosition);
    }

    public void UpdatePosition(Vector2 screenPosition)
    {
        if (tooltipRect == null) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            tooltipRect.parent as RectTransform,
            screenPosition,
            null,
            out Vector2 localPoint
        );

        tooltipRect.localPosition = localPoint + offset;
    }

    public void Hide()
    {
        tooltipObject.SetActive(false);
    }
}