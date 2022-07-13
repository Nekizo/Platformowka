
using UnityEngine;
using UnityEngine.UI;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private float hpMax;
    private float hp;

    [SerializeField, Tooltip("You need to connect a moving health bar here.")] private Image healthBar;

    public event System.Action Death;
    private void Awake()
    {
        hp = hpMax;
    }
    
    public void Damage(float value)
    {
        hp -= value;
        if (hp <= 0)
        {
            healthBar.transform.localScale = Vector3.zero;
            Death.Invoke();
        }
        else
        {
            healthBar.transform.localScale = new Vector3((hp / hpMax), 1);
        }
        

    }
}
