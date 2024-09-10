using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    private float Speed = 200;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            transform.Rotate(Vector3.up, Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.J))
        {
            transform.Rotate(Vector3.up, -Speed * Time.deltaTime);
        } 

        transform.position = player.transform.position; // Move focal point with player

    }
}
