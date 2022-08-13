using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class SingletonScene : MonoBehaviour
{
    public SceneTypes type;
    private void Start()
    {
        //SceneMenager.instance.loudScene[(int)type].Add(SceneManager.GetActiveScene().name);
        MenagerScene.instance.AddScene(type, gameObject.scene.name);
        //PlayerInstances.instance.Door(Vector3.zero);
        
    }
    
}
