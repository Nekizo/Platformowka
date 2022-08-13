using UnityEngine;

public class EnemyDeactivation : MonoBehaviour
{

    private SaveObjectDate date;
    private void Start()
    {
        date = GetComponent<SaveObjectDate>();
        date.action += Deactivation;
        SaveMenager.instance.Restart+=Activation;
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
