using UnityEngine;
using System.Collections.Generic;

public class SpawnZones : MonoBehaviour
{
    public List<Transform> SpawnZoneTransforms;

    public Vector3 GetRandomSpawnZone()
    {
        return SpawnZoneTransforms[Random.Range(0, SpawnZoneTransforms.Count)].position;
    }
}
