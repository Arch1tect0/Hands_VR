using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nail : MonoBehaviour
{
    public float pushForce = 0.05f; // How much the nail moves per hit
    public float minDepth = -2f; // Minimum depth the nail can go

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    print("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
    //    if (other.gameObject.CompareTag("Hammer"))
    //    {

    //        MoveNailDown();
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        print("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
        if (collision.gameObject.CompareTag("Hammer"))
        {
    
            MoveNailDown();
        }
    }

    void MoveNailDown()
    {
        Vector3 newPosition = transform.position - new Vector3(0, pushForce, 0);
        if (newPosition.y >= initialPosition.y + minDepth)
        {
            transform.position = newPosition;
        }
    }
}
