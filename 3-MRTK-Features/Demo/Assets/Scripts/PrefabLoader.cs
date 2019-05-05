using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLoader : MonoBehaviour
{
    
    public void SpawnPrefab(GameObject prefab)
    {
        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
