using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.Input;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Vector3 position;
    public GameObject toMove;


    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the GameManager between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        //Debug.Log(handJointIds.Length); //its 24
    }
    // Headset *************************************************************************************************************************
    public Transform headset;

    // HAND *************************************************************************************************************************
    public Hand leftHand;
    public Hand rightHand;
    
    private Pose[] leftHandPoses = new Pose[handJointIds.Length];
    private Pose[] rightHandPoses = new Pose[handJointIds.Length];

    private static HandJointId[] handJointIds = new HandJointId[]{HandJointId.HandThumb1, HandJointId.HandThumb2, HandJointId.HandThumb3, HandJointId.HandThumbTip,
        HandJointId.HandIndex0, HandJointId.HandIndex1, HandJointId.HandIndex2, HandJointId.HandIndex3, HandJointId.HandIndexTip,
        HandJointId.HandMiddle0, HandJointId.HandMiddle1, HandJointId.HandMiddle2, HandJointId.HandMiddle3, HandJointId.HandMiddleTip,
        HandJointId.HandRing0, HandJointId.HandRing1, HandJointId.HandRing2, HandJointId.HandRing3, HandJointId.HandRingTip,
        HandJointId.HandPinky0, HandJointId.HandPinky1, HandJointId.HandPinky2, HandJointId.HandPinky3, HandJointId.HandPinkyTip };

    public Pose[] GetLeftHandPoses()
    {
        for (int i = 0; i < leftHandPoses.Length; i++)
        {
            leftHand.GetJointPose(handJointIds[i], out leftHandPoses[i]);
        }
        //Debug.Log(leftHandPoses[5]);
        return leftHandPoses;

    }
    public Pose[] GetRightHandPoses()
    {
        for (int i = 0; i < rightHandPoses.Length; i++)
        {
            rightHand.GetJointPose(handJointIds[i], out rightHandPoses[i]);
        }

        //.Log(rightHandPoses[1]);
        return rightHandPoses;
    }
    // HAND *************************************************************************************************************************



    private void Update()
    {
        toMove.transform.position = position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Reset the object's position
            toMove.transform.position = new Vector3(0f, 0f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
