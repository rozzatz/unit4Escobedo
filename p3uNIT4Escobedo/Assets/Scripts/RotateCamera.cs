using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float RSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
          {
            transform.Rotate(Vector3.up, RSpeed * Time.deltaTime);
          }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -RSpeed * Time.deltaTime);
        }
    }
}
