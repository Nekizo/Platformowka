using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunInputMenager : MonoBehaviour
{
    private PlayerControler playerInputAction;
    private bool overloaded=true;

    public event System.Action SmallBullet;
    [SerializeField] private float overloadedTime = .1f;
    private float fireStartTime;
    [SerializeField] private float bigBulletOverloadedTime = 1;

    private void Start()
    {
        
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
        if(Time.time- fireStartTime < bigBulletOverloadedTime)
        {
            if (overloaded)
            {
                SmallBullet.Invoke();
                Invoke("Charging", overloadedTime);
                overloaded = false;
            }
        }
        
        

    }
    private void Charging()
    {
        overloaded = true;
    }
}
