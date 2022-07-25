using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchesPlayerDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    private void Start()
    {
        GetComponent<TouchesSensor>().Collision+=Damage;
    }
    private void Damage(Collider collider)
    {
        
        if (collider.GetComponent<PlayerLife>() != null)
        {
            collider.GetComponent<PlayerLife>().Damage(damage);
        }
    }
}
