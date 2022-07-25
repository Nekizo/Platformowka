using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    [SerializeField] private float streng = 1;
    [SerializeField] private int idActivator = 2;
    [SerializeField] private float activateTime = 0.1f;
    [SerializeField] private GameObject notActive;
    private void Start()
    {
        GetComponentInParent<BulletActivates>().Hit += Activate;
    }
    
    private void Activate(int id)
    {
        if (id == idActivator)
        {
            notActive.SetActive(true);
            Invoke("Deactivate", activateTime);
        }
        
        

    }
    private void Deactivate()
    {
        notActive.SetActive(false);
    }
}
