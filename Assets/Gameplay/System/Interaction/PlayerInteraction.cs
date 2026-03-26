using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform interactOrigin;
    public float interactDistance = 3.5f;
    public float interactRadius = 0.8f;
    public GameObject interactionPrompt;

    private InteractableBase currentInteractable;

    void Update()
    {
        CheckForInteractable();

        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact();
        }
    }

    void CheckForInteractable()
    {
        InteractableBase newInteractable = null;

        RaycastHit hit;

        if (Physics.SphereCast(interactOrigin.position, interactRadius, interactOrigin.forward, out hit, interactDistance))
        {
            newInteractable = hit.collider.GetComponent<InteractableBase>();
        }

        if (newInteractable == null)
        {
            Collider[] nearbyColliders = Physics.OverlapSphere(interactOrigin.position, interactRadius);

            foreach (Collider col in nearbyColliders)
            {
                InteractableBase interactableBase = col.GetComponent<InteractableBase>();

                if (interactableBase != null)
                {
                    newInteractable = interactableBase;
                    break;
                }
            }
        }

        if (currentInteractable != newInteractable)
        {
            if (currentInteractable != null)
            {
                currentInteractable.HighlightOff();
            }

            currentInteractable = newInteractable;

            if (currentInteractable != null)
            {
                currentInteractable.HighlightOn();
            }
        }

        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(currentInteractable != null);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (interactOrigin == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactOrigin.position, interactRadius);
        Gizmos.DrawLine(interactOrigin.position, interactOrigin.position + interactOrigin.forward * interactDistance);
    }
}