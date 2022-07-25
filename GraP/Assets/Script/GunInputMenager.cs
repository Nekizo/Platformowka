using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunInputMenager : MonoBehaviour
{
    private PlayerControler playerInputAction;

    private bool overloaded=true;

    public event System.Action SmallBullet;
    [SerializeField] private GunBulletsMagazine smallBulletMagazine;
    public event System.Action BigBullet;
    [SerializeField] private GunBulletsMagazine bigBulletMagazine;
    public event System.Action BigFireBullet;
    [SerializeField] private GunBulletsMagazine bigFireBulletMagazine;
    public event System.Action Explosion;
    public event System.Action SpawnCube;
    [SerializeField] private GunBulletsMagazine cubeMagazine;

    [SerializeField] private float overloadedTime = .1f;

    private float fireStartTime;

    private float fireEndTime;

    [SerializeField] private float bigBulletOverloadedTime = 1;

    [SerializeField] private float bigFireBulletOverloadedTime = 10;

    [SerializeField] private float cubeOverloadedTime = 2;

    [SerializeField] private float explosionTime = 12;

    [SerializeField] private float recolStreng = 10;

    [SerializeField] private float recolDownStreng = 200;
    [SerializeField] private float recolDownAngle = 30;

    [SerializeField] private PlayerGroundSensor groundSensor;

    private int smallBulletCombo;

    public bool fall;

    private Rigidbody rb;
    private void Start()
    {
        SmallBullet += smallBulletMagazine.Fire;
        BigBullet += bigBulletMagazine.Fire;
        BigFireBullet += bigFireBulletMagazine.Fire;
        SpawnCube += cubeMagazine.Fire;

        rb = GetComponentInParent<Rigidbody>();

        PlayerInput playerInput = GetComponentInParent<PlayerInput>();

        playerInputAction = new PlayerControler();
        playerInputAction.GamePlay.Enable();
        playerInputAction.GamePlay.Fire.canceled+=FireStop;
        playerInputAction.GamePlay.Fire.performed += FireStart;
    }
    private void FireStart(InputAction.CallbackContext ctx)
    {
        //SmallBullet.Invoke();

        fireStartTime = Time.time;
    }

    private void FireStop(InputAction.CallbackContext ctx)
    {
        fireEndTime= Time.time;
        if (Time.time - fireStartTime < bigBulletOverloadedTime)
        {
            if (overloaded)
            {
                SmallBullet.Invoke();
                Invoke("Charging", overloadedTime);
                smallBulletCombo += 1;
                overloaded = false;
            }
        }
        else
        {
            
            if (Time.time - fireStartTime < bigFireBulletOverloadedTime)
            {
                if(smallBulletCombo==2 && Time.time - fireStartTime < cubeOverloadedTime)
                {
                    SpawnCube.Invoke();
                }
                else
                {
                    BigBullet.Invoke();
                    Ricol();
                }
                
            }
            else
            {
                BigFireBullet.Invoke();
                Ricol();
            }
            smallBulletCombo = 0;
        }
        

    }
    private void Ricol()
    {
        
        
        if (Mathf.Abs( transform.eulerAngles.x-270)<recolDownAngle)
        {
            if(!groundSensor.isGrounded)
            {
                rb.velocity += Vector3.down * recolDownStreng;
                fall = true;
            }
            
        }
        else
        {
            Vector3 direction = transform.rotation * Vector3.back;
            rb.velocity += new Vector3(direction.x, 0, direction.z) * recolStreng;
        }
    }
    
    private void Charging()
    {
        overloaded = true;
    }
}
