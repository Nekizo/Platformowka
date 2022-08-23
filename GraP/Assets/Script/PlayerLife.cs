using UnityEngine.UI;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private float hpMax=100;
    private float hp ;
    public event System.Action Death;
    public static PlayerLife instance;
    [SerializeField, Tooltip("You need to connect a moving health bar here.")] private Image healthBar;
    public void Damage(float value)
    {
        hp -= value;
        if (hp < 0)
        {
            healthBar.transform.localScale = new Vector3((0), 1);
            Death?.Invoke();
            SaveMenager.instance.Load();
            MenuMenager.instance.MenuOn();
        }
        else
        {
            healthBar.transform.localScale = new Vector3((hp / hpMax), 1);
        }
    }
    private void LifeRest()
    {
        hp = hpMax;
    }
    private void Awake()
    {
        hp = hpMax;
        instance = this;
    }
    private void Start()
    {
        SaveMenager.instance.Restart += LifeRest;
    }
}
