using System.Collections.Generic;
using UnityEngine;

public class OnCollisionsMaterials : MonoBehaviour
{
    public Material blackMaterial;

    private Dictionary<Renderer, Material> originalMaterials = new Dictionary<Renderer, Material>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                if (!originalMaterials.ContainsKey(renderer))
                {
                    originalMaterials[renderer] = renderer.material;
                }
                renderer.material = blackMaterial;
            }
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        Renderer renderer = other.GetComponent<Renderer>();
        if (renderer! && originalMaterials.ContainsKey(renderer)) 
        { 
            renderer.material = originalMaterials[renderer];           
        }
    }
}
