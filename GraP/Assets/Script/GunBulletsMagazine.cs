using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBulletsMagazine : MonoBehaviour
{
    private GameObject[] bullets;

    [SerializeField] private GameObject bullet;
    [SerializeField] private int bulletQuantity;
    
    private void Awake()
    {
        //GetComponent<GunInputMenager>().SmallBullet += Fire;

        bullets = new GameObject[bulletQuantity];
        for (int i = 0; i < bulletQuantity; i++)
        {
            GameObject newBullet= Instantiate(bullet);

            bullets[i] = newBullet;
        }
    }
    
    public void Fire()
    {
        GameObject notActive = null;
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i].activeSelf)
            {
                continue;
            }
            notActive = bullets[i];
        }
        if (notActive == null)
        {
            notActive = bullets[bullets.Length - 1];
        }
        

        notActive.SetActive(true);
        notActive.transform.position = transform.position;
        notActive.transform.rotation = transform.rotation;
        notActive.GetComponent<BulletMenager>()?.BulletStart();


    }


    void EndTime()
    {
        gameObject.SetActive(false);
    }
}
