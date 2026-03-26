using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    protected Renderer objectRenderer;
    protected Color originalColor;
    public Color highlightColor = Color.yellow;

    protected virtual void Awake()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }

    public virtual void HighlightOn()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = highlightColor;
        }
    }

    public virtual void HighlightOff()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
        }
    }
}