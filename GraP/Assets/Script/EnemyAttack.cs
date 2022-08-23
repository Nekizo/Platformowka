using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float recoil;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerLife.instance.Damage(damage);
            PlayerInstance.instance.GetComponent<Rigidbody>().AddForce((PlayerInstance.instance.transform.position-transform.position).normalized* recoil, ForceMode.VelocityChange);
        }
    }
}
