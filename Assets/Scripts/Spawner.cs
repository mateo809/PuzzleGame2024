using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("ParentSpawn")]
    public Transform ParentSpawn;
    [Header("Liste de prefabs à instancier")]
    public List<GameObject> prefabs;

    [Header("Points de déplacement")]
    public Transform pointA;
    public Transform pointB;

    [Header("Paramètres de spawn")]
    public float minDelay = 1f;
    public float maxDelay = 5f;

    public float moveSpeed = 3f;


    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnObject();
            float randomDelay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(randomDelay);
        }
    }

    void SpawnObject()
    {
        if (prefabs.Count == 0) return;

        int randomIndex = Random.Range(0, prefabs.Count);
        GameObject prefab = prefabs[randomIndex];

        GameObject instance = Instantiate(prefab, pointA.transform.position, pointA.transform.rotation);
        instance.transform.SetParent(ParentSpawn);
        StartCoroutine(MoveToPoint(instance, pointB.position));
    }

    IEnumerator MoveToPoint(GameObject obj, Vector3 target)
    {
        while (obj != null && Vector3.Distance(obj.transform.position, target) > 0.01f)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        if (obj != null)
            Destroy(obj);
    }
}
