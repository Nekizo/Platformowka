using UnityEngine;

public class BulletActivateRotate : MonoBehaviour
{
    //[SerializeField] private float speed = 1;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private bool local;
    [SerializeField] private int idActivator = 2;
    
    [SerializeField] private Transform Rotating;
    private void Start()
    {
        GetComponentInParent<BulletActivates>().Hit += Activate;
    }

    private void Activate(int id)
    {
        if (id == idActivator)
        {
            if (local)
            {
                transform.localEulerAngles = endPosition;
            }
            else
            {
                transform.eulerAngles = endPosition;
            }
        }



    }
    
}
