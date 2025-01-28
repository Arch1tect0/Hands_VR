using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReset : MonoBehaviour
{
    // List of objects to reset
    public List<GameObject> objectsToReset;

    // Initial positions and rotations of objects
    private List<Vector3> initialPositions = new List<Vector3>();
    private List<Quaternion> initialRotations = new List<Quaternion>();

    void Start()
    {
        // Store the initial positions and rotations
        foreach (GameObject obj in objectsToReset)
        {
            initialPositions.Add(obj.transform.position);
            initialRotations.Add(obj.transform.rotation);
        }
    }

    // Method to reset all objects in the list
    public void ResetObjects()
    {
        for (int i = 0; i < objectsToReset.Count; i++)
        {
            objectsToReset[i].transform.position = initialPositions[i];
            objectsToReset[i].transform.rotation = initialRotations[i];
        }

        Debug.Log("All objects have been reset to their initial positions and rotations.");
    }
}
