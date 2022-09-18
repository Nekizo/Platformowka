
using UnityEngine;
using UnityEngine.UI;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private float hpMax;
    private float hp;

    private Vector3 startPos;

    [SerializeField, Tooltip("You need to connect a moving health bar here.")] private Image healthBar;

    public event System.Action Death;
    private void Awake()
    {
        hp = hpMax;
        Death += GetComponent<EnemyDeactivation>().Deactivation;
        startPos = transform.position;
    }
    private void Start()
    {
        SaveMenager.instance.Restart += ResetGame;
        SaveMenager.instance.Unload += Unload;
    }
    private void Unload()
    {
        SaveMenager.instance.Restart -= ResetGame;
        SaveMenager.instance.Unload -= Unload;
    }
    private void ResetGame()
    {
        hp = hpMax;
        healthBar.transform.localScale = new Vector3(1, 1);
        transform.position = startPos;
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
