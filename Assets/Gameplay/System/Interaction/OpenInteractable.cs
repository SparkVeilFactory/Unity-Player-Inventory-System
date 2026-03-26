using UnityEngine;

public class OpenInteractable : InteractableBase
{
    public Vector3 moveOffset = new Vector3(2f, 0f, 0f);
    public float moveSpeed = 3f;
    public float stopDistance = 0.01f;

    private bool isOpen = false;
    private bool isMoving = false;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Vector3 targetPosition;

    protected override void Awake()
    {
        base.Awake();

        closedPosition = transform.position;
        openPosition = closedPosition + moveOffset;
        targetPosition = closedPosition;
    }

    void Update()
    {
        if (!isMoving) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) <= stopDistance)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }

    public override void Interact()
    {
        if (isMoving) return;

        if (!isOpen)
        {
            targetPosition = openPosition;
            isOpen = true;
        }
        else
        {
            targetPosition = closedPosition;
            isOpen = false;
        }

        isMoving = true;
    }
}