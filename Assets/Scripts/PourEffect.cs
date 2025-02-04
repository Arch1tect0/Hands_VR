using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourEffect : MonoBehaviour
{

    public ParticleSystem effect;
    public float angleThreshold = 45f;

    private bool isPouring = false;

    private void Update()
    {
        float tiltAngle = Vector3.Angle(Vector3.up, transform.up);


        if (!isPouring && tiltAngle > angleThreshold)
        {
            effect.Play();
            isPouring = true;
        }
        else if (isPouring && tiltAngle < angleThreshold)
        {
            effect.Stop();
            isPouring = false;

        }
    }
}
