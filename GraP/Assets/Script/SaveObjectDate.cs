using UnityEngine;

public class SaveObjectDate : MonoBehaviour
{
    public System.Action action;
    [SerializeField] private bool privateSave=true;
    public ObjectData data;

    private void Awake()
    {
        data.name = gameObject.name;
    }
    private void Start()
    {

        if (privateSave)
        {
            SaveMenager.instance.savePunblic += AddObjectData;
        }
        else
        {
            SaveMenager.instance.saveProgress += AddObjectData;
        }
    }
    private void AddObjectData()
    {
        SaveMenager.instance.SaveAddObjectData(data);
    }
    //public System.Action deactivate;

}
