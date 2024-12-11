using UnityEngine;

public class CharcoalMover : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 sidePosition;
    private bool isMoved = false;
    public string originalTag = "Charcoal";
    public string movedTag = "CharcoalMoved";
    public Transform parentTransform;

    void Start()
    {
        if (parentTransform != null)
        {
            originalPosition = parentTransform.InverseTransformPoint(transform.position);
            sidePosition = originalPosition + new Vector3(0.25f, 0, 0);
        }
        else
        {
            originalPosition = transform.position;
            sidePosition = originalPosition + new Vector3(0.25f, 0, 0);
        }
        gameObject.tag = originalTag;
    }

    void OnMouseDown()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero; 
            rb.angularVelocity = Vector3.zero; 
        }
        if (isMoved)
        {
            if (parentTransform != null)
            {
                transform.position = parentTransform.TransformPoint(originalPosition);
            }
            else
            {
                transform.position = originalPosition;
            }
            gameObject.tag = originalTag;
        }
        else
        {
            if (parentTransform != null)
            {
                transform.position = parentTransform.TransformPoint(sidePosition);
            }
            else
            {
                transform.position = sidePosition;
            }
            gameObject.tag = movedTag;
        }
        isMoved = !isMoved;
    }
}