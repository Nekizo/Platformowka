using UnityEngine;

public class TouchesRecoil : MonoBehaviour
{
    [SerializeField] private float force=1000;
    [SerializeField] private string tag = "Player";
    [SerializeField] private Vector3 direction = Vector3.up;
    private void Start()
    {
        GetComponent<TouchesSensor>().Collision += Recoil;
    }
    private void Recoil(Collider collider)
    {

        if (collider.tag == tag)
        {
            collider.GetComponent<Rigidbody>().AddForce(force*direction);
        }
    }
}
