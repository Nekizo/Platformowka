using UnityEngine;

public class BulletActivates : MonoBehaviour
{
    public System.Action<int> Hit;
    public event System.Action<int> AfterHit;
    private int idBullet;
    private void Awake()
    {
        Hit += AfterHitStart;
    }
    private void AfterHitStart(int id)
    {
        idBullet = id;
        Invoke("AfterHitStart", 0.001f);
    }
    private void AfterHitStart()
    {
        AfterHit.Invoke(idBullet);
    }
}
