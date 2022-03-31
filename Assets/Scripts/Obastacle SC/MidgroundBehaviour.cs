using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidgroundBehaviour : MonoBehaviour
{
    public float Speed;
    public float BoundaryX;

    private void Update()
    {
        if (transform.position.x > -BoundaryX)
        {
            transform.position -= new Vector3(Speed * Time.deltaTime, 0, 0);
        }
        else Destroy(gameObject);
    }
}
