using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMenager : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float destroyTime;
    [SerializeField] private float speed;
    [SerializeField] private int bulletId;
    //public event System.Action<Collider> EnterCollision;
    //[SerializeField] private string[] tagList;
    private float startTime;
    public void BulletStart()
    {
        startTime = Time.time;
        Invoke("EndTime", destroyTime);
        
        gameObject.GetComponent<Rigidbody>().velocity=((transform.rotation * Vector3.forward) * 100 * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        //EnterCollision?.Invoke(other);
        /*if (tagList != null)
        {
            for (int i = 0; i < tagList.Length; i++)
            {
                if (other.tag == tagList[i])
                {
                    
                    break;
                }
            }
        }*/
        other.GetComponent<BulletActivates>()?.Hit.Invoke(bulletId);
        EnemyLife enemy = other.GetComponent<EnemyLife>();
        enemy?.Damage(damage);
        gameObject.SetActive(false);

    }
    void EndTime()
    {
        if (Time.time- startTime+0.05f>= destroyTime)
        {
            gameObject.SetActive(false);
        }
        
    }
}
