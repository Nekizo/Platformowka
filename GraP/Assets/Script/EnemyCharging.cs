using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharging : MonoBehaviour
{
    [SerializeField] private float speed;
    Rigidbody rb;
    [SerializeField] private Collider attackCollider;
    [SerializeField] private float time;
    void Awake()
    {
        // Get the rigidbody on this.
        rb = GetComponent<Rigidbody>();
        
    }
    private void Start()
    {
        attackCollider.enabled = false;
        GetComponent<EnemyNavMeshAgent>().Attack += Attack;
        attackCollider.GetComponent<EnemyAttack>().Hit += StopAttack;
        attackCollider.GetComponent<EnemyAttack>().Ground+= StopAttack;
    }

    
    private void Attack()
    {
        attackCollider.enabled = true;
        StartCoroutine(Watch());
        
    }
    private void StopAttack()
    {
        attackCollider.enabled = false;
        StopAllCoroutines();
        StartCoroutine(GetComponent<EnemyNavMeshAgent>().Agent());
    }
    
    public IEnumerator Watch()
    {

        rb.velocity += (transform.rotation * Vector3.forward) * speed;
        attackCollider.enabled = true;
        float t = time;
        while (t>0)
        {


            t -= Time.deltaTime;
            yield return null;



        }
        StopAttack();
    }
}
