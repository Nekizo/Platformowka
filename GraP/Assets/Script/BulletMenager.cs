using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMenager : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float destroyTime;
    [SerializeField] private float speed;
    public void BulletStart()
    {
        Invoke("EndTime", destroyTime);
        
        gameObject.GetComponent<Rigidbody>().velocity=((transform.rotation * Vector3.forward) * 100 * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyLife enemy = other.GetComponent<EnemyLife>();
        if (enemy != null)
        {
            enemy.Damage(damage);
        }
        gameObject.SetActive(false);

    }
    void EndTime()
    {
        gameObject.SetActive(false);
    }
}
