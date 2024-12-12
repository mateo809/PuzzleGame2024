using System.Collections.Generic;
using UnityEngine;

public class PlaneData : MonoBehaviour
{
    public List<GameObject> Objects = new List<GameObject>();
    private List<Material> _materials = new List<Material>();
    public Material mat;

    private int defaultLayer;
    private int interactableLayer;

    void Start()
    {
        defaultLayer = LayerMask.NameToLayer("Default");
        interactableLayer = LayerMask.NameToLayer("InteractableObject");

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
            int lightIsOn = _materials[i].GetInt("_LightIsOn");
            if (lightIsOn == 0 && Objects[i].layer != defaultLayer)
            {
                Objects[i].layer = defaultLayer;
            }
        }
    }

    public void DeactivateShadows()
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            _materials[i].SetInt("_LightIsOn", 1);
            Objects[i].layer = interactableLayer;
        }
        Destroy(gameObject);
    }
}
