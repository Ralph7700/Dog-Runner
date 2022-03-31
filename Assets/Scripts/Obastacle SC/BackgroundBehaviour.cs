using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{
    public float Speed;
    public float BoundaryX;

    private void Update()
    {
        if (transform.position.x > -BoundaryX)
        {
            transform.position -= new Vector3(Speed * Time.deltaTime, 0, 0);
        }
        else transform.position = new Vector3(BoundaryX, transform.position.y, transform.position.z);
    }
}
