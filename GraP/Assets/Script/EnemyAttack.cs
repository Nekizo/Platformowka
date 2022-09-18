using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float recoil;
    public event System.Action Hit;
    public event System.Action Ground;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerLife.instance.Damage(damage);
            PlayerInstance.instance.GetComponent<Rigidbody>().velocity=((PlayerInstance.instance.transform.position-transform.position).normalized* recoil);
            Hit?.Invoke();
        }
        if (other.tag == "Ground")
        {
            Ground?.Invoke();
        }
    }
}
