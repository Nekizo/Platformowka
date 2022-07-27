using UnityEngine;

public class BulletActivateDisappear : MonoBehaviour
{
    [SerializeField] private int idActivator = 2;
    [SerializeField] private float disappearTime = 0;
    [SerializeField] private GameObject activeObject;
    private void Start()
    {
        GetComponentInParent<BulletActivates>().AfterHit += Activate;
    }

    private void Activate(int id)
    {
        if (id == idActivator)
        {
            activeObject.SetActive(true);
            if (disappearTime != 0)
            {
                Invoke("Deactivate", disappearTime);
            }
            else
            {
                activeObject.SetActive(false);
            }
            
        }



    }
    private void Deactivate()
    {
        activeObject.SetActive(false);
    }

}
