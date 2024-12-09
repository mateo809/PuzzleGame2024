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

            mainCam.gameObject.SetActive(false);
            previewCam.gameObject.SetActive(true);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                itemToCopy = hit.collider.gameObject;
                PreviewUpdate(itemToCopy);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            mainCam.gameObject.SetActive(true);
            previewCam.gameObject.SetActive(false);
        }
    }

    public void PreviewUpdate(GameObject p_gm)
    {
        previewItem = Instantiate(p_gm,previewItem.gameObject.transform.position, previewItem.gameObject.transform.rotation);
    }
}
