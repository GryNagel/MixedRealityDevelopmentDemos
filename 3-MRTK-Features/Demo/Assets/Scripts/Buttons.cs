using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public float moveDistance; 

    private Vector3 cubePosition; 

    // Start is called before the first frame update
    void Start()
    {
        cubePosition = transform.position; 
    }

    public void MoveCube(string direction)
    {
     
        if (direction == "left")
        {
            transform.position = new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z);
        }
        else if(direction == "right")
        {
            transform.position = new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z);
        }
    }

    public void ResetCubePosition()
    {
        transform.position = cubePosition;
    }
}
