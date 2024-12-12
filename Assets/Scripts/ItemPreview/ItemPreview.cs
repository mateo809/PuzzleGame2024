using System.Data;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPreview : MonoBehaviour
{
    [SerializeField] private GameObject previewItem;
    public GameObject itemToCopy;
    [SerializeField] private Camera _mainCam;
    [SerializeField] private Camera _previewCam;
    [SerializeField] private GameObject _parentUI;
    [SerializeField] private bool _canRotateX = false;
    [SerializeField] private bool _canRotateY = false;

    private float _sensitivity = 30f;
    private bool _isRotating = false;
    private float _startMousePosX;
    private float _startMousePosY;
    private Vector3 _itemCopyLastPos;
    private Quaternion _itemCopyLastRot;


    public GameObject ItemCopy
    {
        get { 
            return itemToCopy; 
        }
        set
        {
            Debug.Log(value);
            itemToCopy = value;
            if (itemToCopy == null)
            {
                ChangeView();
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_previewCam.gameObject.activeSelf)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.GetComponent<ItemWaypoint>() == null || !hit.collider.gameObject.GetComponent<ItemWaypoint>().canBePreviewed)
                    return;

                _mainCam.gameObject.SetActive(false);
                _previewCam.gameObject.SetActive(true);
                _parentUI.gameObject.SetActive(false);
                _canRotateX = hit.collider.gameObject.GetComponent<ItemWaypoint>().canRotateX;
                _canRotateY = hit.collider.gameObject.GetComponent<ItemWaypoint>().canRotateY;

                if (hit.collider == null)
                {
                    return;
                }

                itemToCopy = hit.collider.gameObject;

                if (itemToCopy.GetComponent<ItemWaypoint>()._colliderToDisable != null)
                {
                    itemToCopy.GetComponent<ItemWaypoint>()._colliderToDisable.enabled = false;
                }
                PreviewUpdate(itemToCopy);
            }
        }

        else if (Input.GetMouseButtonDown(0))
        {
            _isRotating = true;
            _startMousePosX = Input.mousePosition.x;
            _startMousePosY= Input.mousePosition.y;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            _isRotating = false;
        }

        if ((Input.GetMouseButtonDown(1) && _previewCam.enabled) || itemToCopy==null)
        {
            ChangeView();
        }

        if (_isRotating)
        {
            Rotate();
        }
    }

    public void ChangeView()
    {


        _mainCam.gameObject.SetActive(true);
        _previewCam.gameObject.SetActive(false);
        _parentUI.gameObject.SetActive(true);
        _isRotating = false;

        if (itemToCopy == null)
            return;

        itemToCopy.transform.rotation = _itemCopyLastRot;
        itemToCopy.transform.position = _itemCopyLastPos;

        if (!itemToCopy.transform.GetChild(0).gameObject.activeSelf) return;

        else if (itemToCopy.GetComponent<ItemWaypoint>()._colliderToDisable != null)
        {
            itemToCopy.GetComponent<ItemWaypoint>()._colliderToDisable.enabled = true;
        }


    }

    public void PreviewUpdate(GameObject p_gm)
    {
        _itemCopyLastPos = itemToCopy.transform.position;
        _itemCopyLastRot = itemToCopy.transform.rotation;

        _parentUI.gameObject.SetActive(false);
        if(itemToCopy.GetComponent<Collider>().bounds.size.z * itemToCopy.transform.localScale.z >= itemToCopy.GetComponent<Collider>().bounds.size.y * itemToCopy.transform.localScale.y)
        {
            itemToCopy.transform.position = previewItem.gameObject.transform.position + new Vector3(0, 0, itemToCopy.GetComponent<Collider>().bounds.size.z * itemToCopy.transform.localScale.z);
            //itemToCopy = Instantiate(p_gm,previewItem.gameObject.transform.position + new Vector3(0,0,itemToCopy.GetComponent<MeshFilter>().mesh.bounds.size.z * itemToCopy.transform.localScale.z), previewItem.gameObject.transform.rotation);
        }
        else
        {
            itemToCopy.transform.position = previewItem.gameObject.transform.position + new Vector3(0, 0, itemToCopy.GetComponent<Collider>().bounds.size.y * itemToCopy.transform.localScale.y);
            //itemToCopy = Instantiate(p_gm, previewItem.gameObject.transform.position + new Vector3(0, 0, itemToCopy.GetComponent<MeshFilter>().mesh.bounds.size.y * itemToCopy.transform.localScale.y), previewItem.gameObject.transform.rotation);
        }

        itemToCopy.transform.LookAt(new Vector3(_previewCam.gameObject.transform.position.x, _previewCam.gameObject.transform.position.y,2*_previewCam.gameObject.transform.position.x));


        Debug.Log(itemToCopy.transform.position);
    }

    private void Rotate()
    {
        Debug.Log(itemToCopy.gameObject.transform.rotation);
        if (_canRotateX)
        {
            float currentMousePosX = Input.mousePosition.x;
            float mouseMovementX = currentMousePosX - _startMousePosX;
            itemToCopy.transform.Rotate(Vector3.up, -mouseMovementX * _sensitivity * Time.deltaTime);
            _startMousePosX = currentMousePosX;
        }

        if (_canRotateY)
        {
            float currentMousePosY = Input.mousePosition.y;
            float mouseMovementY = currentMousePosY - _startMousePosY;
            itemToCopy.transform.Rotate(Vector3.left, -mouseMovementY * _sensitivity * Time.deltaTime);
            _startMousePosY = currentMousePosY;
        }
    }
}
