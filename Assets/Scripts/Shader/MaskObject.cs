using UnityEngine;

public class MaskObject : MonoBehaviour
{
    public GameObject[] maskObjs;

    void Start()
    {
        for (int i = 0; i < maskObjs.Length; ++i)
        {
            maskObjs[i].GetComponent<MeshRenderer>().material.renderQueue = 3002;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
