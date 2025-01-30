using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    //hand data, rb vel is too small

    // A struct to store data
    public struct Data
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 velocity;
        public Vector3 angVelocity;
        public Pose[] leftHand;
        public Pose[] rightHand;

        public Data(Vector3 pos, Quaternion rot, Vector3 vel, Vector3 angVel, Pose[] lHand, Pose[] rhand)
        {
            position = pos;
            rotation = rot;
            velocity = vel;
            angVelocity = angVel;
            leftHand = lHand;
            rightHand = rhand;
        }
    }

    // List to store the recorded data
    private List<Data> recordedData = new List<Data>();

    // Method to retrieve the recorded data
    public List<Data> GetRecordedData()
    {
        return new List<Data>(recordedData);
    }

    // *****************************************************************************************************************************************

    public Rigidbody rb;
    public float speedThreshold = .1f;

    // Time interval for recording positions
    public float recordInterval = .1f; // in seconds
    private float timeSinceLastRecord = 0.0f;

    // Flag to start/stop recording
    public bool isRecording;

    void Update()
    {
        
        // Check if recording is enabled
        if (isRecording)
        {
            // Calculate the time passed
            timeSinceLastRecord += Time.deltaTime;

            // Calculate the current speed of the object
            float speed = rb.velocity.magnitude;

            if (speedThreshold >= speed && timeSinceLastRecord >= recordInterval)
            {
                // Record the current position and rotation
                recordedData.Add(new Data(transform.position, transform.rotation, rb.velocity, rb.angularVelocity,
                    GameManager.Instance.GetLeftHandPoses(), GameManager.Instance.GetRightHandPoses()));

                // Reset the timer
                timeSinceLastRecord = 0.0f;
            }
        }
    }


    // Method to save recorded data to a file on the desktop
    public void SaveDataToDesktop()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath + "/Recorded_Data", this.name + "_Data.txt");

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (Data data in recordedData)
            {
                writer.WriteLine($"{data.position},{data.rotation}, {data.velocity}, {data.angVelocity}, {data.leftHand}, {data.rightHand}");
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
