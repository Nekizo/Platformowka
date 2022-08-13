using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (PlayerGroundSensor.isGrounded)
            {
                SaveMenager.instance.SaveProgress();
                SaveMenager.instance.SavePublic();
            }
        }
    }
}
