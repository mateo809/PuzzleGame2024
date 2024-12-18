//using UnityEngine;
//using System.Collections.Generic;

//public class MovementManager : MonoBehaviour
//{
//    [SerializeField]
//    private LayerMask interactableMask;

//    private Dictionary<string, Vector3> movementOffsets;

//    private void Start()
//    {
//        movementOffsets = new Dictionary<string, Vector3>
//        {
//            { "MovementXPlus", new Vector3(0.25f, 0, 0) },
//            { "MovementXMinus", new Vector3(-0.25f, 0, 0) },
//            { "MovementYPlus", new Vector3(0, 0.25f, 0) },
//            { "MovementYMinus", new Vector3(0, -0.25f, 0) }
//        };
//    }

//    private void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            InteractWithObject();
//        }
//    }

//    private void InteractWithObject()
//    {
//        if (Camera.main == null)
//            return;

//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        RaycastHit hit;

//        Debug.DrawRay(ray.origin, ray.direction.normalized * 1000, Color.red, 10);

//        if (Physics.Raycast(ray, out hit, 1000, interactableMask))
//        {
//            GameObject hitObject = hit.collider.gameObject;
//            string currentTag = hitObject.tag;

//            if (movementOffsets.ContainsKey(currentTag))
//            {
//                MoveObject(hitObject, currentTag);
//            }
//        }
//    }

//    private void MoveObject(GameObject obj, string currentTag)
//    {
//        Vector3 currentPosition = obj.transform.position;
//        Vector3 offset = movementOffsets[currentTag];

//        Vector3 newPosition = currentPosition + offset;
//        obj.transform.position = newPosition;
//        string newTag = GetNextTag(currentTag);
//        obj.tag = newTag;

//        Debug.Log($"Moved {obj.name} to {newPosition}, updated tag to {newTag}");
//    }

//    private string GetNextTag(string currentTag)
//    {
//        switch (currentTag)
//        {
//            case "MovementXPlus": return "MovementXMinus";
//            case "MovementXMinus": return "MovementYPlus";
//            case "MovementYPlus": return "MovementYMinus";
//            case "MovementYMinus": return "MovementXPlus";
//            default: return currentTag; 
//        }
//    }
//}
