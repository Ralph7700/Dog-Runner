using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private float SpawningPeriod;
    [SerializeField] private List<GameObject> High, Medium, Low;
    [SerializeField] private GameObject CoinPre;
    int PreviousHigh = 0;
    private float Timer = 0;
    private GameObject SelectedObj;
    [SerializeField] private float Offset;
    private float constante;
    public GameObject LayerObj;


    private void Update()
    {
        constante = (1+GameManager.Instance.speedMultiplier)/2;

        if (Timer < SpawningPeriod / constante)
        {
            Timer += Time.deltaTime;
        }

        else
        {
            int Hight;
            if (PreviousHigh == 0)
            {
                Hight = Random.Range(0, 2);
                HightTOList(Hight);

            }
            else if (PreviousHigh == 1)
            {
                Hight = Random.Range(0, 3);
                HightTOList(Hight);

            }
            else if (PreviousHigh == 2)
            {
                Hight = Random.Range(0, 3);
                HightTOList(Hight);
            }
        }
    }
    void HightTOList(int hight)
    {
        switch (hight)
        {
            case 0: BuildingSelection(Low, hight); break;
            case 1: BuildingSelection(Medium, hight); break;
            case 2: BuildingSelection(High, hight); break;
        }
    }
    void BuildingSelection(List<GameObject> PrefabList, int hight)
    {
        Timer = 0;
        SelectedObj = PrefabList[Random.Range(0, PrefabList.Count)];
        if (hight != PreviousHigh)
        {
            SelectedObj = Instantiate(SelectedObj, new Vector3(transform.position.x, SelectedObj.transform.position.y, 0), Quaternion.identity);
            SelectedObj.transform.parent = LayerObj.transform;
        }
        else
        {
            SelectedObj = Instantiate(SelectedObj, new Vector3(transform.position.x + Random.Range(0, Offset), SelectedObj.transform.position.y, 0), Quaternion.identity);
            SelectedObj.transform.parent = LayerObj.transform;
        }
        PreviousHigh = hight;
        GenerateCoin(SelectedObj);
    }

    private void GenerateCoin(GameObject selectedObj)
    {
        int coin = Random.Range(0, 2);
        if (coin == 1)
        {
            SpriteRenderer spriterend = selectedObj.GetComponent<SpriteRenderer>();
            float Xrange = 2f;
            var newObj = Instantiate(CoinPre, new Vector3(transform.position.x + Random.Range(-Xrange, Xrange), Random.Range(0, 2f), 0), Quaternion.identity);
            newObj.transform.parent = selectedObj.transform;
        }

    }
}
