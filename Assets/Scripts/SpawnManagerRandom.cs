using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManagerRandom : MonoBehaviour
{
    [SerializeField] List<GameObject> targetPrefabs;
    [SerializeField] List<GameObject> checkPoints;
    [SerializeField] Transform goalArea;
    [SerializeField] Transform checkPoint;
    Vector3 collectablePosition;
    public AgentController agent;

    private readonly List<Vector3> spawnPositions = new()
    {
        //normal left
        new Vector3(-6.311f, 0f, 9.61f),
        new Vector3(-6.311f, 0f, 8.11f),
        new Vector3(-6.311f, 0f, 6.716f),
        new Vector3(-6.311f, 0f, 5.266f),
        new Vector3(-6.311f, 0f, 3.816f),
        new Vector3(-6.311f, 0f, 2.316f),
        new Vector3(-6.311f, 0f, 0.897f),
        new Vector3(-6.311f, 0f, -0.579f),
        new Vector3(-6.311f, 0f, -2.074f),
        new Vector3(-6.311f, 0f, -3.495f),
        new Vector3(-6.311f, 0f, -4.965f),
        new Vector3(-6.311f, 0f, -6.429f),
        new Vector3(-6.311f, 0f, -7.99f),
        new Vector3(-6.311f, 0f, -9.371f),
        //inverse (rotate y 180 degree) left
        new Vector3(-3.811f, 0f, 9.61f),
        new Vector3(-3.811f, 0f, 8.11f),
        new Vector3(-3.811f, 0f, 6.716f),
        new Vector3(-3.811f, 0f, 5.266f),
        new Vector3(-3.811f, 0f, 3.816f),
        new Vector3(-3.811f, 0f, 2.316f),
        new Vector3(-3.811f, 0f, 0.897f),
        new Vector3(-3.811f, 0f, -0.579f),
        new Vector3(-3.811f, 0f, -2.074f),
        new Vector3(-3.811f, 0f, -3.495f),
        new Vector3(-3.811f, 0f, -4.965f),
        new Vector3(-3.811f, 0f, -6.429f),
        new Vector3(-3.811f, 0f, -7.99f),
        new Vector3(-3.811f, 0f, -9.371f),
        //normal right
        new Vector3(3.62f, 0f, 9.61f),
        new Vector3(3.62f, 0f, 8.11f),
        new Vector3(3.62f, 0f, 6.716f),
        new Vector3(3.62f, 0f, 5.266f),
        new Vector3(3.62f, 0f, 3.816f),
        new Vector3(3.62f, 0f, 2.316f),
        new Vector3(3.62f, 0f, 0.897f),
        new Vector3(3.62f, 0f, -0.579f),
        new Vector3(3.62f, 0f, -2.074f),
        new Vector3(3.62f, 0f, -3.495f),
        new Vector3(3.62f, 0f, -4.965f),
        new Vector3(3.62f, 0f, -6.429f),
        new Vector3(3.62f, 0f, -7.99f),
        new Vector3(3.62f, 0f, -9.371f),
        //inverse (rotate y 180 degree) right
        new Vector3(6.12f, 0f, 9.61f),
        new Vector3(6.12f, 0f, 8.11f),
        new Vector3(6.12f, 0f, 6.716f),
        new Vector3(6.12f, 0f, 5.266f),
        new Vector3(6.12f, 0f, 3.816f),
        new Vector3(6.12f, 0f, 2.316f),
        new Vector3(6.12f, 0f, 0.897f),
        new Vector3(6.12f, 0f, -0.579f),
        new Vector3(6.12f, 0f, -2.074f),
        new Vector3(6.12f, 0f, -3.495f),
        new Vector3(6.12f, 0f, -4.965f),
        new Vector3(6.12f, 0f, -6.429f),
        new Vector3(6.12f, 0f, -7.99f),
        new Vector3(6.12f, 0f, -9.371f)
    };
    private readonly List<Vector3> collectablePositions = new();
    public Vector3 RandomSpawnPosition()
    {
        if (spawnPositions.Count == 0)
        {
            throw new System.InvalidOperationException("No spawn positions available.");
        }

        int randomIndex = Random.Range(0, spawnPositions.Count-1);
        Vector3 selectedPosition = spawnPositions[randomIndex];
        spawnPositions.RemoveAt(randomIndex);
        return transform.TransformPoint(selectedPosition);
    }

    void Start()
    {
        for (int i = 0; i < spawnPositions.Count -1; i++)
        {
            Vector3 localSpawnPosition = RandomSpawnPosition();
            Instantiate(targetPrefabs[Random.Range(0, 6)], localSpawnPosition, targetPrefabs[Random.Range(0, 6)].transform.rotation, transform);
           }

        collectablePositions.AddRange(spawnPositions);

        int randomVector = Random.Range(0, collectablePositions.Count);
        collectablePosition = collectablePositions[randomVector];
        Vector3 goalPosition = transform.TransformPoint(collectablePosition);
        if (collectablePosition.x == -6.311f || collectablePosition.x == 3.62f)
        {
            Instantiate(goalArea, goalPosition, Quaternion.identity, transform);
            Instantiate(checkPoint, goalPosition + new Vector3(0f, 0.3f, 0f), Quaternion.identity, transform);
        }
        if (collectablePosition.x == -3.811f || collectablePosition.x == 6.12f)
        {
            Instantiate(goalArea, goalPosition, Quaternion.Euler(0, 180, 0), transform);
            Instantiate(checkPoint, goalPosition + new Vector3(0f, 0.3f, 0f), Quaternion.identity, transform);
        }
    }
    void Update()
    {
        if (agent.episodeStarted)
        {
            ResetObjects();
            agent.episodeStarted = false;
        }
    }
    private void ResetObjects()
    {
        // 4 Lane
        for (int i = 0; i < checkPoints.Count; i++) // All checkpoint prefab
        {
            //Left (x = -6.311f)
            if (collectablePosition.x == -6.311f)
            {
                if (i >= 5 && i <= 12) // Only left checkpoint prefabs
                {
                    if (collectablePosition.z == 9.61f || collectablePosition.z == 8.11f || collectablePosition.z == 6.716f)
                    {
                        checkPoints[i].SetActive(true);
                    }
                    if (collectablePosition.z == 5.266f || collectablePosition.z == 3.816f || collectablePosition.z == 2.316f)
                    {
                        checkPoints[i].SetActive(true);
                        if (i == 12) checkPoints[i].SetActive(false);
                    }
                    if (collectablePosition.z == 0.897f || collectablePosition.z == -0.579f || collectablePosition.z == -2.074f)
                    {
                        checkPoints[i].SetActive(true);
                        if (i == 12 || i == 11) checkPoints[i].SetActive(false);
                    }
                    if (collectablePosition.z == -3.495f || collectablePosition.z == -4.965f || collectablePosition.z == -6.429f)
                    {
                        checkPoints[i].SetActive(true);
                        if (i == 12 || i == 11 || i == 10) checkPoints[i].SetActive(false);
                    }
                    if (collectablePosition.z == -7.99f || collectablePosition.z == -9.371f)
                    {
                        checkPoints[i].SetActive(true);
                        if (i == 12 || i == 11 || i == 10 || i == 9) checkPoints[i].SetActive(false);
                    }
                }
            }
            //Middle left (x = -3.811f) and Middle right (x = 3.62f)
            if (collectablePosition.x == -3.811f || collectablePosition.x == 3.62f)
            {
                if (i >= 0 && i <= 4) // Only left checkpoint prefabs
                {
                    if (collectablePosition.z == 9.61f || collectablePosition.z == 8.11f || collectablePosition.z == 6.716f)
                    {
                        checkPoints[i].SetActive(true);
                    }
                    if (collectablePosition.z == 5.266f || collectablePosition.z == 3.816f || collectablePosition.z == 2.316f)
                    {
                        checkPoints[i].SetActive(true);
                        if (i == 4) checkPoints[i].SetActive(false);
                    }
                    if (collectablePosition.z == 0.897f || collectablePosition.z == -0.579f || collectablePosition.z == -2.074f)
                    {
                        checkPoints[i].SetActive(true);
                        if (i == 4 || i == 3) checkPoints[i].SetActive(false);
                    }
                    if (collectablePosition.z == -3.495f || collectablePosition.z == -4.965f || collectablePosition.z == -6.429f)
                    {
                        checkPoints[i].SetActive(true);
                        if (i == 4 || i == 3 || i == 2) checkPoints[i].SetActive(false);
                    }
                    if (collectablePosition.z == -7.99f || collectablePosition.z == -9.371f)
                    {
                        checkPoints[i].SetActive(true);
                        if (i == 4 || i == 3 || i == 2 || i == 1) checkPoints[i].SetActive(false);
                    }
                }
            }
            //Right (x = 6.12f)
            if (collectablePosition.x == 6.12f)
            {
                if (i >= 13 && i <= 20) // Only right checkpoint prefabs
                {
                    if (collectablePosition.z == 9.61f || collectablePosition.z == 8.11f || collectablePosition.z == 6.716f)
                    {
                        checkPoints[i].SetActive(true);
                    }
                    if (collectablePosition.z == 5.266f || collectablePosition.z == 3.816f || collectablePosition.z == 2.316f)
                    {
                        checkPoints[i].SetActive(true);
                        if (i == 20) checkPoints[i].SetActive(false);
                    }
                    if (collectablePosition.z == 0.897f || collectablePosition.z == -0.579f || collectablePosition.z == -2.074f)
                    {
                        checkPoints[i].SetActive(true);
                        if (i == 20 || i == 19) checkPoints[i].SetActive(false);
                    }
                    if (collectablePosition.z == -3.495f || collectablePosition.z == -4.965f || collectablePosition.z == -6.429f)
                    {
                        checkPoints[i].SetActive(true);
                        if (i == 20 || i == 19 || i == 18) checkPoints[i].SetActive(false);
                    }
                    if (collectablePosition.z == -7.99f || collectablePosition.z == -9.371f)
                    {
                        checkPoints[i].SetActive(true);
                        if (i == 20 || i == 19 || i == 18 || i == 17) checkPoints[i].SetActive(false);
                    }
                }
            }
        }
    }
}
