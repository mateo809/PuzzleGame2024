using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [HideInInspector] public Vector3 targetPosition;
    [HideInInspector] public float moveSpeed = 3f;

    private bool isMoving = true;

    void OnEnable()
    {
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving) return;

        if (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
            Destroy(gameObject);
        }
    }
}
