using System.Collections.Generic;
using UnityEngine;

public static class EnvironmentManager
{
    // Store the spawn positions globally
    public static List<Vector3> CarSpawnPositions { get; private set; }
    public static Vector3 GoalPosition { get; private set; }
    public static bool IsInitialized { get; private set; } = false;

    // Method to initialize positions if they haven't been set
    public static void Initialize(List<Vector3> carPositions, Vector3 goalPosition)
    {
        if (!IsInitialized)
        {
            CarSpawnPositions = new List<Vector3>(carPositions); // Copy the list to avoid reference issues
            GoalPosition = goalPosition;
            IsInitialized = true;
        }
    }

    // Method to clear positions (if needed)
    public static void Clear()
    {
        CarSpawnPositions = null;
        GoalPosition = Vector3.zero;
        IsInitialized = false;
    }
}
