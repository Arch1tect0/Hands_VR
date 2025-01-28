using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollilder : MonoBehaviour
{

    //for now reset the position of the object to the initial pos
    public Vector3 initPosition;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            print("method is called.");
            ResetPosition(other.transform.parent);
        }
    }

    private void ResetPosition(Transform tran)
    {
        tran.position = initPosition;
    }
}
