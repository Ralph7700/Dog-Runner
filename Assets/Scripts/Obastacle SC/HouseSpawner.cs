using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
    [SerializeField]
    private float SpawningPeriod;
    private float Timer = 0;
    [SerializeField]
    private List<GameObject> BuildingPrefabs;
    private GameObject SelectedObj;
    public GameObject LayerObj;
    private void Update()
    {
        if (Timer < SpawningPeriod)
        {
            Timer += Time.deltaTime;
        }
        else
        {
            SelectedObj = BuildingPrefabs[Random.Range(0, BuildingPrefabs.Count)];
            Timer = 0;
            var newObj = Instantiate(SelectedObj, new Vector3(transform.position.x, SelectedObj.transform.position.y, 0), Quaternion.identity);
            newObj.transform.parent = LayerObj.transform;
        }
    }
}
