using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance instance;
    
    private void Awake()
    {
        instance = this;
        
    }
}
