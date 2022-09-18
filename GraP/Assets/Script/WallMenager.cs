using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMenager : MonoBehaviour
{
    private SaveObjectDate date;
    //[SerializeField] private string component;
    
    private void Start()
    {
        date = GetComponent<SaveObjectDate>();
        date.action += Deactivation;
        SaveMenager.instance.Restart += Activation;
        SaveMenager.instance.Unload += Unload;
        GetComponent<BulletActivateDisappear>().AfterDisappear+=Deactivation;
    }
    private void Unload()
    {
        SaveMenager.instance.Restart -= Activation;
        SaveMenager.instance.Unload -= Unload;
    }
    public void Deactivation()
    {
        //data.active = false;
        date.data.active = true;

        gameObject.SetActive(false);

    }
    private void Activation()
    {
        date.data.active = false;
        gameObject.SetActive(true);

    }
}
