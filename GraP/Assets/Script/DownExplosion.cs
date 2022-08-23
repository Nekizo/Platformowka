using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownExplosion : MonoBehaviour
{
    [SerializeField] private float streng=1;
    [SerializeField] private float activateTime = 0.2f;
    [SerializeField] private GunInputMenager gim;
    private void Start()
    {
        GetComponentInParent<PlayerGroundSensor>().GroundEnter += Activate;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Rigidbody>().velocity += (transform.position - other.transform.position).normalized * -10* streng;
            other.GetComponent<EnemyNavMeshAgent>().Stunning(2);
        }
        if (other.tag == "Cube")
        {
            
            StartCoroutine(Rotate(other));
            //Vector3 a= (transform.position - other.transform.position).normalized*200  * streng;
            //other.GetComponent<Rigidbody>().rotation=Quaternion.EulerRotation(0,0,180);
            //= Quaternion.LookRotation(other.transform.position,transform.position); 
        }
    }

    private void Activate()
    {
        if (gim.fall)
        {
            gim.fall = false;
            GetComponent<Collider>().enabled = true;
            Invoke("Deactivate", activateTime);
        }
        
    }
    private void Deactivate()
    {
        GetComponent<Collider>().enabled = false;
    }
    IEnumerator Rotate(Collider c)
    {
        Transform t = c.transform;
        float timeRotate = 0.5f;
        

        Vector3 difrent = (transform.position - t.position);

        if (Mathf.Abs( difrent.x) < Mathf.Abs(difrent.z))
        {
            if (difrent.z > 0)
            {
                difrent = Vector3.left;
            }
            else
            {
                difrent = Vector3.right;
            }
            
        }
        else
        {
            if (difrent.x > 0)
            {
                difrent = Vector3.forward;
            }
            else
            {
                difrent = Vector3.back;
            }
            
            
        }
        Debug.Log("Cube " + (1 - timeRotate) * 90 + " " + difrent + " " + (transform.position - t.position));
        while (timeRotate > 0)
        {
            //c.GetComponent<Rigidbody>().AddTorque(difrent * 90, ForceMode.VelocityChange);
            //Debug.Log("Cube "+ (1 - timeRotate) * 90 + " " + difrent + " " + t.eulerAngles);
            //t.right = new Vector3(Mathf.DeltaAngle(t.eulerAngles.x, -difrent.x * 0.15f), Mathf.DeltaAngle(t.eulerAngles.y, -difrent.y * 0.15f), Mathf.DeltaAngle(t.eulerAngles.z, -difrent.z * 0.15f));
            t.Rotate( difrent, 180* Time.deltaTime, Space.World);
            
            /*if (difrent.x != 0)
            {

                
                //t.eulerAngles =new Vector3(Mathf.DeltaAngle(a, -difrent.x * (1 - timeRotate) * 90), t.eulerAngles.y, t.eulerAngles.z);
            }
            else
            {
                //t.eulerAngles = new Vector3(t.eulerAngles.x, t.eulerAngles.y, Mathf.DeltaAngle(a , -difrent.z * (1 - timeRotate) * 90));
            }*/



            timeRotate -= Time.deltaTime;
            yield return null;
        }
        t.eulerAngles = new Vector3(Mathf.Round( t.eulerAngles.x/90)*90, Mathf.Round(t.eulerAngles.y/90) * 90, Mathf.Round(t.eulerAngles.z/90) * 90);
        yield return null;
    }
}
