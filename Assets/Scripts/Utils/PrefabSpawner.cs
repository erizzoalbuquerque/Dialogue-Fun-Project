using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] Transform _spawnPositionAndRotation;

    public void SpawnPrefab()
    {
        Vector3 spawnPosition;
        Quaternion spawnRotation;
        if (_spawnPositionAndRotation == null)
        {
            spawnPosition = Vector3.zero;
            spawnRotation = Quaternion.identity;
        }
        else
        {
            spawnPosition = _spawnPositionAndRotation.position;
            spawnRotation = _spawnPositionAndRotation.rotation;
        }

        Instantiate(_prefab, spawnPosition, spawnRotation);
    }
}
