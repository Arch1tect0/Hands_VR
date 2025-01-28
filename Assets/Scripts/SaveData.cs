using System.Collections.Generic;
using UnityEngine;
using System.IO; // Required for file operations

public class PositionRotationRecorder : MonoBehaviour
{

    // To DO: 
    // Add velocity and acceleration
    //check if data is null don't save.

    // *****************************************************************************************************************************************
    // A struct to store data
    public struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;

        public TransformData(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }
    }

    // List to store the recorded data
    private List<TransformData> recordedData = new List<TransformData>();

    // Method to retrieve the recorded data
    public List<TransformData> GetRecordedData()
    {
        return new List<TransformData>(recordedData);
    }

    // *****************************************************************************************************************************************

    public Rigidbody rb;
    public float speedThreshold = .1f;

    // Time interval for recording positions
    public float recordInterval = .1f; // in seconds
    private float timeSinceLastRecord = 0.0f;

    // Flag to start/stop recording
    public bool isRecording = false;

    void Update()
    {
        // Check if recording is enabled
        if (isRecording)
        {
            // Calculate the time passed
            timeSinceLastRecord += Time.deltaTime;

            // Calculate the current speed of the object
            float speed = rb.velocity.magnitude;

            if (timeSinceLastRecord >= recordInterval)
            {
                // Record the current position and rotation
                recordedData.Add(new TransformData(transform.position, transform.rotation));

                // Reset the timer
                timeSinceLastRecord = 0.0f;
            }
        }
    }


    // Method to save recorded data to a file on the desktop
    public void SaveDataToDesktop()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, this.name + "_RecordedData.txt");

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (TransformData data in recordedData)
            {
                writer.WriteLine($"Position: {data.position}, Rotation: {data.rotation}");
            }
        }

        Debug.Log($"Data saved to: {filePath}");
    }


    //now save to desktop when done
    private void OnApplicationQuit()
    {
        SaveDataToDesktop();
    }
}
