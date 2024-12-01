using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerStatic : MonoBehaviour
{
    [SerializeField] List<GameObject> targetPrefabs;
    [SerializeField] Transform goalArea;

    private readonly List<Vector3> spawnPositions = new()
    {
        new (-6.311f, 0f, 9.61f),
        new (-6.311f, 0f, 8.11f),
        new (-6.311f, 0f, 6.716f),
        new (-6.311f, 0f, 5.266f),
        new (-6.311f, 0f, 3.816f),
        new (-6.311f, 0f, 2.316f),
        new (-6.311f, 0f, 0.897f),
        new (-6.311f, 0f, -0.579f),
        new (-6.311f, 0f, -2.074f),
        new (-6.311f, 0f, -3.495f),
        new (-6.311f, 0f, -4.965f),
        new (-6.311f, 0f, -6.429f),
        new (-6.311f, 0f, -7.99f),
        new (-6.311f, 0f, -9.371f),

        new (-3.811f, 0f, 9.61f),
        new (-3.811f, 0f, 8.11f),
        new (-3.811f, 0f, 6.716f),
        new (-3.811f, 0f, 5.266f),
        new (-3.811f, 0f, 3.816f),
        new (-3.811f, 0f, 2.316f),
        new (-3.811f, 0f, 0.897f),
        new (-3.811f, 0f, -0.579f),
        new (-3.811f, 0f, -2.074f),
        new (-3.811f, 0f, -3.495f),
        new (-3.811f, 0f, -4.965f),
        new (-3.811f, 0f, -6.429f),
        new (-3.811f, 0f, -7.99f),
        new (-3.811f, 0f, -9.371f),

        new (3.62f, 0f, 9.61f),
        new (3.62f, 0f, 8.11f),
        new (3.62f, 0f, 6.716f),
        new (3.62f, 0f, 5.266f),
        new (3.62f, 0f, 3.816f),
        new (3.62f, 0f, 2.316f),
        new (3.62f, 0f, 0.897f),
        new (3.62f, 0f, -0.579f),
        new (3.62f, 0f, -2.074f),
        new (3.62f, 0f, -3.495f),
        new (3.62f, 0f, -4.965f),
        new (3.62f, 0f, -6.429f),
        new (3.62f, 0f, -7.99f),
        new (3.62f, 0f, -9.371f),

        new (6.12f, 0f, 9.61f),
        new (6.12f, 0f, 8.11f),
        new (6.12f, 0f, 6.716f),
        new (6.12f, 0f, 5.266f),
        new (6.12f, 0f, 3.816f),
        new (6.12f, 0f, 2.316f),
        new (6.12f, 0f, 0.897f),
        new (6.12f, 0f, -0.579f),
        new (6.12f, 0f, -2.074f),
        new (6.12f, 0f, -3.495f),
        new (6.12f, 0f, -4.965f),
        new (6.12f, 0f, -6.429f),
        new (6.12f, 0f, -7.99f),
        new (6.12f, 0f, -9.371f)
    };

    private readonly List<Vector3> positionsTaken = new();

    void Start()
    {
        /*
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            if (i % 3 == 0)
            {
                int randomPrefabIndex = Random.Range(0, 6);
                Instantiate(targetPrefabs[randomPrefabIndex], spawnPositions[i], targetPrefabs[randomPrefabIndex].transform.localRotation, transform);
                positionsTaken.Add(spawnPositions[i]);
            }
        }

        for (int j = 0; j < spawnPositions.Count; j++)
        {
            if (j % 2 == 0)
            {
                int randomPrefabIndex = Random.Range(0, 6);
                if (!positionsTaken.Contains(spawnPositions[j]))
                {
                    Instantiate(targetPrefabs[randomPrefabIndex], spawnPositions[j], targetPrefabs[randomPrefabIndex].transform.localRotation, transform);
                    positionsTaken.Add(spawnPositions[j]);
                }
            }
        }*/
        /*
        Vector3 collectablePosition = new(3.74f, 0.0f, -0.593f);
        Vector3 restrictionPos = new(6.12f, 0f, -0.579f);
        Instantiate(targetPrefabs[0], restrictionPos, targetPrefabs[0].transform.rotation, transform);
        Instantiate(goalArea, collectablePosition, Quaternion.identity, transform);
        */
    }
}
