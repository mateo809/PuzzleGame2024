using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class PlaneData : MonoBehaviour
{
    public List<GameObject> Objects = new List<GameObject>();
    private List<Material> _materials = new List<Material>();
    public Material mat;

    void Start()
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            Color c = Objects[i].GetComponent<MeshRenderer>().material.color;

            Material newMat = new Material(mat);
            Objects[i].GetComponent<MeshRenderer>().material = newMat;
            newMat.SetColor("_BaseColor", c);

            _materials.Add(newMat);
        }
    }

    void Update()
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            _materials[i].SetVector("_PlanePosition", transform.position);
            _materials[i].SetVector("_PlaneNormal", transform.up);
        }
    }
    [ContextMenu("Debug/DeactivateShadows")]
    public void DeactivateShadows()
    {
        for (int i = 0; i < Objects.Count; i++) 
        {
            _materials[i].SetInt("_LightIsOn", 1);
        }
        Destroy(gameObject);
    }
}
