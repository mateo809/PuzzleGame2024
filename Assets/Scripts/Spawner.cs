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

    [Header("Vitesse de déplacement")]
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

        GameObject instance = Instantiate(prefab, pointA.position, pointA.rotation);
        instance.transform.SetParent(ParentSpawn);

        MoveToTarget mover = instance.GetComponent<MoveToTarget>();
        if (mover != null)
        {
            mover.targetPosition = pointB.position;
            mover.moveSpeed = moveSpeed;
        }
    }
}
