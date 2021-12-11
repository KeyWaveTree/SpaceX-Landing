using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz_Five: MonoBehaviour
{
    public Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.velocity = transform.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.angularVelocity = new Vector3(0, 1, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.angularVelocity = new Vector3(0, -1, 0);
        }
    }
}
