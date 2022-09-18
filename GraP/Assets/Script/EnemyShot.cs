using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{

    private void Start()
    {
        GetComponent<EnemyNavMeshAgent>().Attack += GetComponentInChildren<GunBulletsMagazine>().Fire;
        GetComponent<EnemyNavMeshAgent>().Attack += Attack;
    }
    private void Attack()
    {
        Debug.Log("Fire Enemy");
        StartCoroutine(GetComponent<EnemyNavMeshAgent>().Agent());
    }
}
