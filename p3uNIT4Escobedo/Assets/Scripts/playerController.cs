using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody PlayerRb;
    public float speed = 5.0f;
    private GameObject focalPoint;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        PlayerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }
}
