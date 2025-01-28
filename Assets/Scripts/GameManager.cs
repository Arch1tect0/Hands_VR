using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 position;
    public GameObject toMove;

    private void Update()
    {
        toMove.transform.position = position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Reset the object's position
            toMove.transform.position = new Vector3(0f, 0f, 0f);
        }
    }
}
