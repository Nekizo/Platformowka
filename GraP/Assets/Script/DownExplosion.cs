using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownExplosion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Rigidbody>().velocity += (transform.position - other.transform.position).normalized * -100;
        }
    }
}
