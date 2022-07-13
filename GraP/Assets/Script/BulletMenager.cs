using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMenager : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter(Collider other)
    {
        EnemyLife enemy = other.GetComponent<EnemyLife>();
        if (enemy != null)
        {
            enemy.Damage(damage);
        }
        Destroy(gameObject);
    }
}
