using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{

    public Transform C_Target;
    public float C_Height = 10f;
    public float C_Distance = 20f;
    public float C_Angle = 45f;

    // Update is called once per frame
    void Update()
    {
        if (!C_Target) { return;  }

        Vector3 worldPosition = (Vector3.forward * -C_Distance) + (Vector3.up * C_Height);
        Vector3 rotatedVector = Quaternion.AngleAxis(C_Angle,Vector3.up) * worldPosition;
        Vector3 flatTergetPosition = C_Target.position;
        flatTergetPosition.y = 0f;

        Vector3 finalPosition = flatTergetPosition + rotatedVector;
        transform.position = finalPosition;
        transform.LookAt(C_Target.position);
    }
}
