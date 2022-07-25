using UnityEngine;

public class TouchesEnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    private void Start()
    {
        GetComponent<TouchesSensor>().Collision += Damage;
    }
    private void Damage(Collider collider)
    {

        if (collider.GetComponent<EnemyLife>() != null)
        {
            collider.GetComponent<EnemyLife>().Damage(damage);
        }
    }
}
