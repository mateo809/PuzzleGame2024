using UnityEngine;

public class ItemPreview : MonoBehaviour
{
    [SerializeField] private GameObject previewItem;
    public GameObject itemToCopy;

    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera previewCam;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !previewCam.gameObject.activeSelf)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit, 1000))
            {

                mainCam.gameObject.SetActive(false);
                previewCam.gameObject.SetActive(true);

                if (hit.collider == null)
                {
                    Debug.Log("nothign was hit");
                    return;
                }

                Debug.Log("this was hit : " + hit.collider.gameObject.name);
                itemToCopy = hit.collider.gameObject;
                PreviewUpdate(itemToCopy);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            mainCam.gameObject.SetActive(true);
            previewCam.gameObject.SetActive(false);
            Destroy(itemToCopy);
        }
    }

    public void PreviewUpdate(GameObject p_gm)
    {
        if(itemToCopy.GetComponent<MeshFilter>().mesh.bounds.size.z * itemToCopy.transform.localScale.z >= itemToCopy.GetComponent<MeshFilter>().mesh.bounds.size.y * itemToCopy.transform.localScale.y)
        {
            itemToCopy = Instantiate(p_gm,previewItem.gameObject.transform.position + new Vector3(0,0,itemToCopy.GetComponent<MeshFilter>().mesh.bounds.size.z * itemToCopy.transform.localScale.z), previewItem.gameObject.transform.rotation);
        }
        else
        {
            itemToCopy = Instantiate(p_gm, previewItem.gameObject.transform.position + new Vector3(0, 0, itemToCopy.GetComponent<MeshFilter>().mesh.bounds.size.y * itemToCopy.transform.localScale.y), previewItem.gameObject.transform.rotation);
        }
    }
}
