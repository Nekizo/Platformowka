using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMeshAgent : MonoBehaviour
{
    NavMeshAgent nma;
    private bool stunning;
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        StartCoroutine(Agent());
    }
    public void Stunning( float time)
    {
        stunning = true;
        nma.ResetPath();
        Invoke("Revival", time);
    }
    private void Revival()
    {
        stunning = false;
    }
    IEnumerator Agent()
    {
        nma.stoppingDistance = 4;
        while (true)
        {
            if (!stunning)
            {
                float d = Vector3.Distance(PlayerInstance.instance.transform.position, transform.position);
                if (d > 5)
                {
                    nma.SetDestination(PlayerInstance.instance.transform.position);
                    yield return new WaitForSeconds(d / 10);
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                yield return null;
            }
            
            
        }
    }
}
