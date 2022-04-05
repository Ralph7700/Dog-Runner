using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject Arrow;
    public GameObject Player;
    public float Maxtime, MinTime;
    private float time;
    private float timer=0;

    private void Start()
    {
        Player = GameObject.Find("Player");
        time = Random.Range(MinTime, Maxtime);
    }
    private void Update()
    {
        if (timer < time) timer += Time.deltaTime; 
        else
        {
            timer = 0;
            time = Random.Range(MinTime, Maxtime);
            Instantiate(Arrow, new Vector3(transform.position.x,Player.transform.position.y,0), Arrow.transform.rotation);
            AudioManager.Instance.PlaySound("ArrowShot");
        }
    }
}
