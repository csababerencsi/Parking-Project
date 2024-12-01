using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AgentController : Agent
{
    [SerializeField] float speed = 5.0f;
    private readonly float turnSpeed = 90.0f;
    Vector3 spawnPoint;
    [SerializeField] Transform target;
    [SerializeField] List<GameObject> obstacles;
    [SerializeField] List<GameObject> collectibles;
    [SerializeField] GameObject frontRect;
    [SerializeField] GameObject backRect;
    Vector3 previousPosition;
    float previousDistance;
    //[SerializeField] SpawnManagerRandom spawnManager; //for random env
    [SerializeField] SpawnManagerStatic spawnManager;
    public bool episodeStarted = false;

    public override void OnEpisodeBegin()
    {
        for (int i = 0; i < collectibles.Count; i++) // Static
        {
            collectibles[i].SetActive(true);
        }
        //
        episodeStarted = true;
        //target = FindLocalObjectWithTag(spawnManager.transform, "GoalArea");
        /*
        if (target == null)
        {
            return;
        }
        */
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("GoalArea").transform;
        }
        Rigidbody rb = GetComponent<Rigidbody>();
        spawnPoint = new Vector3(-0.05f, 0.01f, -12.21f);

        transform.localPosition = spawnPoint;
        transform.localRotation = Quaternion.identity;
        previousPosition = transform.localPosition;
        previousDistance = Vector3.Distance(transform.localPosition, target.localPosition);
    }
    private Transform FindLocalObjectWithTag(Transform parent, string tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag(tag))
            {
                return child;
            }
            if (child.childCount > 0)
            {
                Transform found = FindLocalObjectWithTag(child, tag);
                if (found != null)
                {
                    return found;
                }
            }
        }
        return null;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent position
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.z);
        sensor.AddObservation(transform.localRotation.x);
        sensor.AddObservation(transform.localRotation.z);

        // Target position
        sensor.AddObservation(target.localPosition.x);
        sensor.AddObservation(target.localPosition.z);


        // Distance between the agent and the target
        //sensor.AddObservation(Vector3.Distance(transform.localPosition, target.localPosition));
    }

    public override void OnActionReceived(ActionBuffers actions)
    {

        var actionTaken = actions.ContinuousActions;

        float _actionSpeed = actionTaken[0];
        float _actionSteering = actionTaken[1];

        transform.Translate(_actionSpeed * Vector3.forward * speed * Time.deltaTime);
        transform.Rotate(Vector3.up, _actionSteering * turnSpeed * Time.deltaTime);


        //float currentDistance = Vector3.Distance(transform.localPosition, target.localPosition);
        //AddReward(-currentDistance * 0.01f);
        if (Vector3.Distance(transform.localPosition, previousPosition) < 0.05f)
        {
            AddReward(-0.1f);
        }
        //previousPosition = transform.localPosition;
        //AddReward(-0.001f);

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> actions = actionsOut.ContinuousActions;
        actions[0] = 0; // Vertical
        actions[1] = 0; // Horizontal

        if (Input.GetKey(KeyCode.W))
        {
            actions[0] = +1;

            if (Input.GetKey(KeyCode.A))
            {
                actions[1] = -1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                actions[1] = +1;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            actions[0] = -1;

            if (Input.GetKey(KeyCode.A))
            {
                actions[1] = +1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                actions[1] = -1;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("ParkedCars"))
        {
            AddReward(-30);
            episodeStarted = false;
            EndEpisode();
        }
        if (other.CompareTag("GoalArea"))
        {
            AddReward(10);
            episodeStarted = false;
            Debug.Log("Parked!");
            EndEpisode();
        }
        if (other.CompareTag("Checkpoint"))
        {
            AddReward(5);
            other.gameObject.SetActive(false);
        }

    }
}