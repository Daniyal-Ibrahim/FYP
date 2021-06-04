using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float time = 5;
    private void Awake()
    {
        Destroy(this.gameObject, time);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.CompareTag("Shield"))
        Destroy(this.gameObject);

    }
}
