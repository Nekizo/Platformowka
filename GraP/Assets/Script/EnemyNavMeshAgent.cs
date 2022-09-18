using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMeshAgent : MonoBehaviour
{
    NavMeshAgent nma;
    //private bool stunning;
    //public event System.Action Attainment;
    public event System.Action Attack;
    private bool duringAttack;
    [SerializeField]private float frequencyAttack;
    [SerializeField] private float distanceAttack;
    [SerializeField] private float distanceExit;
    public event System.Action StunningEvent;


    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        StartCoroutine(Agent());
    }
    public void Stunning( float time)
    {
        //stunning = true;
        nma.ResetPath();
        Invoke("Revival", time);
        StopAllCoroutines();
        StunningEvent?.Invoke();
    }
    private void Revival()
    {
        //stunning = false;
        if (!duringAttack)
        {
            StartCoroutine(Agent());
        }
        
    }
    
    public IEnumerator Agent()
    {
        nma.stoppingDistance = distanceAttack;
        
        while (true)
        {

            float d = Vector3.Distance(PlayerInstance.instance.transform.position, transform.position);
            if (d > distanceExit)
            {
                StopCoroutine(Watch());
                nma.SetDestination(PlayerInstance.instance.transform.position);
                yield return new WaitForSeconds(d / 8);
            }
            else
            {
                
                StartCoroutine(Watch());
                yield return null;

            }
            
            
            
        }
    }
    IEnumerator Watch()
    {
        //Attainment?.Invoke();
        float a = 0;
        float t = 0;
        float tAttack= frequencyAttack;
        while (true)
        {
            
            if (tAttack <= 0)
            {
                tAttack = frequencyAttack;
                Attack?.Invoke();
                if (Attack!= null) { StopAllCoroutines(); }
            }
            tAttack -= Time.deltaTime;
            if (t <= 0)
            {
                a = Mathf.Atan2((PlayerInstance.instance.transform.position.z - transform.position.z), -(PlayerInstance.instance.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
                t += 0.15f;
            }
            t -= Time.deltaTime;
            //Debug.Log("EnemyR-" + a + "-" + Mathf.DeltaAngle(a, transform.eulerAngles.y) +  "-" + Mathf.Clamp(Mathf.DeltaAngle(a, transform.eulerAngles.y), -0.002f, 0.002f) * nma.angularSpeed * Time.deltaTime);

            transform.eulerAngles += Vector3.down * Mathf.Clamp(Mathf.DeltaAngle(a, transform.eulerAngles.y), -0.002f, 0.002f) * nma.angularSpeed * Time.deltaTime;

            yield return null;
            


        }
    }
}
