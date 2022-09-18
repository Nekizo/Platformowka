using UnityEngine;

public class BulletActivateDisappear : MonoBehaviour
{
    [SerializeField] private int idActivator = 2;
    [SerializeField] private float disappearTime = 0;
    [SerializeField] private GameObject activeObject;
    [SerializeField] private BulletActivates bulletActivates;
    public event System.Action AfterDisappear;
    private void Start()
    {
        if (bulletActivates == null)
        {
            GetComponentInParent<BulletActivates>().AfterHit += Activate;
        }
        else
        {
            bulletActivates.AfterHit += Activate;

        }
        
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
                Deactivate();
            }
            
        }



    }
    private void Deactivate()
    {
        activeObject.SetActive(false);
        AfterDisappear?.Invoke();
    }

}
