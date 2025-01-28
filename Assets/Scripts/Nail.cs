using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nail : MonoBehaviour
{
    public float hammerForce = 10f; // Force applied by the hammer
    public float maxAngle = 90f; // Maximum angle the nail can be pushed
    public Rigidbody rb;

    private bool isHammering = false;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Limit the nail rotation angle
        Vector3 euler = transform.rotation.eulerAngles;
        euler.z = Mathf.Clamp(euler.z, 0, maxAngle);
        transform.rotation = Quaternion.Euler(euler);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hammer"))
        {
            isHammering = true;
            ApplyHammerForce(collision);
        }
    }

    void ApplyHammerForce(Collision collision)
    {
        // Apply force based on impact
        Vector3 forceDirection = transform.up * -1; // Push the nail downward
        rb.AddForce(forceDirection * hammerForce, ForceMode.Impulse);
    }
}
