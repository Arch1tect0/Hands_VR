using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    //hand data, rb vel is too small

    // A struct to store data
    public struct Data
    {
        public float posX;
        public float posY;
        public float posZ;

        public float rotX;
        public float rotY;
        public float rotZ;

        public float velX;
        public float velY;
        public float velZ;

        public float angVelX;
        public float angVelY;
        public float angVelZ;

        public float headsetposX;
        public float headsetposY;
        public float headsetposZ;
        public float headsetrotX;
        public float headsetrotY;
        public float headsetrotZ;

        public Pose[] lHposes;
        public Pose[] rHposes;

        public Data(
       float posX, float posY, float posZ,
       float rotX, float rotY, float rotZ,
       float velX, float velY, float velZ,
       float angVelX, float angVelY, float angVelZ,
       float headsetposX, float headsetposY, float headsetposZ,
       float headsetrotX, float headsetrotY, float headsetrotZ,
       Pose[] lHposes, Pose[] rHposes)
        {
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;

            this.rotX = rotX;
            this.rotY = rotY;
            this.rotZ = rotZ;

            this.velX = velX;
            this.velY = velY;
            this.velZ = velZ;

            this.angVelX = angVelX;
            this.angVelY = angVelY;
            this.angVelZ = angVelZ;

            this.headsetposX = headsetposX;
            this.headsetposY = headsetposY;
            this.headsetposZ = headsetposZ;

            this.headsetrotX = headsetrotX;
            this.headsetrotY = headsetrotY;
            this.headsetrotZ = headsetrotZ;

            this.lHposes = lHposes;
            this.rHposes = rHposes;
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
    public float rbMultiplier = 100f;

    // Time interval for recording positions
    private float recordInterval = .0167f; // in seconds
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


           // print(GameManager.Instance.GetRightHandPoses()[0].position);
           // print(GameManager.Instance.GetLeftHandPoses()[0].position);


            //Get the hand poses here:
            //Pose[] lHandPoses = 
            //Pose[] rHandPoses = GameManager.Instance.GetRightHandPoses();

            if (!rb.IsSleeping()  && timeSinceLastRecord >= recordInterval)
            {
                // Record the current position and rotation
                recordedData.Add(new Data(transform.position.x, transform.position.y, transform.position.z, transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z,
                    rb.velocity.x * rbMultiplier, rb.velocity.y * rbMultiplier, rb.velocity.z * rbMultiplier, rb.angularVelocity.x * rbMultiplier, rb.angularVelocity.y * rbMultiplier, rb.angularVelocity.z * rbMultiplier,
                    GameManager.Instance.headset.position.x, GameManager.Instance.headset.position.y, GameManager.Instance.headset.position.z,
                    GameManager.Instance.headset.rotation.eulerAngles.x, GameManager.Instance.headset.rotation.eulerAngles.y, GameManager.Instance.headset.rotation.eulerAngles.z,
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
            // Assuming 'writer' is a StreamWriter instance
            writer.Write("posX,posY,posZ,rotX,rotY,rotZ,velX,velY,velZ,angVelX,angVelY,angVelZ," +
                         "headsetposX,headsetposY,headsetposZ,headsetrotX,headsetrotY,headsetrotZ");

            int maxLHPoses = 24; // Adjust as needed
            int maxRHPoses = 24; // Adjust as needed

            for (int i = 0; i < maxLHPoses; i++)
            {
                writer.Write($",LHPos{i}_X,LHPos{i}_Y,LHPos{i}_Z,LHRot{i}_X,LHRot{i}_Y,LHRot{i}_Z");
            }
            for (int i = 0; i < maxRHPoses; i++)
            {
                writer.Write($",RHPos{i}_X,RHPos{i}_Y,RHPos{i}_Z,RHRot{i}_X,RHRot{i}_Y,RHRot{i}_Z");
            }
            writer.WriteLine();


            foreach (Data data in recordedData)
            {
                // writer.WriteLine($"{data.position},{data.rotation}, {data.velocity}, {data.angVelocity}, {data.leftHand}, {data.rightHand}");


                writer.Write($"{data.posX},{data.posY},{data.posZ},{data.rotX},{data.rotY},{data.rotZ}," +
                    $"{data.velX},{data.velY},{data.velZ},{data.angVelX},{data.angVelY},{data.angVelZ}" +
                    $"{data.headsetposX},{data.headsetposY},{data.headsetposZ},{data.headsetrotX},{data.headsetrotY},{data.headsetrotZ}");

                foreach (var pose in data.lHposes) 
                {
                    Vector3 eulerRotation = pose.rotation.eulerAngles;
                    writer.Write($",{pose.position.x},{pose.position.y},{pose.position.z},{eulerRotation.x},{eulerRotation.y},{eulerRotation.x}");
                }
                foreach (var pose in data.rHposes)
                {
                    Vector3 eulerRotation = pose.rotation.eulerAngles;
                    writer.Write($",{pose.position.x},{pose.position.y},{pose.position.z},{eulerRotation.x},{eulerRotation.y},{eulerRotation.x}");
                }

                writer.WriteLine(); // Move to the next line after writing all data
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
