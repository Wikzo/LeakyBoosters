using UnityEngine;
using System.Collections.Generic;

public class SpawnZones : MonoBehaviour
{
    public List<Transform> SpawnZoneTransforms;
    public List<Transform> BallSpawnZones;

    public Vector3 GetRandomSpawnZone()
    {
        return SpawnZoneTransforms[Random.Range(0, SpawnZoneTransforms.Count)].position;
    }

    public Vector3 GetRandomBallSpawnZone()
    {
        return BallSpawnZones[Random.Range(0, BallSpawnZones.Count)].position;
    }
}
