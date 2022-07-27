using UnityEngine;

public class BulletActivateGun : MonoBehaviour
{
    [SerializeField] private GunBulletsMagazine gun;
    [SerializeField] private int id;
    private void Start()
    {
        GetComponent<BulletActivates>().Hit += Fire;
    }
    void Fire(int idBullet)
    {
        if (idBullet == id)
        {
            gun.Fire();
        }
    }
}
