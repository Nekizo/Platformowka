//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenagerScene : MonoBehaviour
{
    public static MenagerScene instance;
    public string[] loadScene=new string[4];
    public int landingScene;

    private List<AsyncOperation> asyncOperations = new List<AsyncOperation>();

    public event System.Action<SceneTypes> AddedScene;

    private void Awake()
    {
        //SceneManager.LoadScene("menu 1", LoadSceneMode.Additive);
        
         
        instance = this;

        
    }
    
    
    //public event System.Action loaded;

    public void AddScene(SceneTypes type, string name)
    {
        Debug.Log("AddScene-" + name);
        loadScene[(int)type]=(name);
        if (landingScene != 0)
        {
            landingScene -= 1;
        }
        
        AddedScene?.Invoke(type);
        
    }

    public void LoadScene(Scene scene)
    {
        
        Debug.Log("loaud scene"+(int)scene.typ+"-"+ scene.name + "-" + loadScene[(int)scene.typ]);
        if (loadScene[(int)scene.typ] != scene.name)
        {
            landingScene += 1;
            //AsyncOperation[] asyncOperations = new AsyncOperation[2];
            if (loadScene[(int)scene.typ] != "")
            {
                asyncOperations.Add(SceneManager.UnloadSceneAsync(loadScene[(int)scene.typ]));
            }
            
            if (scene.name != "")
            {
                asyncOperations.Add( SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive));
            }
            //return asyncOperations;
        }
        else
        {
            
            AddedScene?.Invoke(scene.typ);
            //return null;
        }
        
        
        
        
        
        
    }
    public Scene CurrentScene(SceneTypes typ)
    {
        Debug.Log("CurrentScene-" + MenagerScene.instance.loadScene[(int)typ] + "-" + typ);
        return new Scene( MenagerScene.instance.loadScene[(int)typ],typ);
        
    }
    public Scene[] CurrentScene(SceneTypes[] typ)
    {
        Scene[] currentScenes = new Scene[typ.Length];
        
       
        for (int i = 0; i < typ.Length; i++)
        {
            
            currentScenes[i]=CurrentScene(typ[i]);
        }
        StartCoroutine(GetComponent<LoadingScreen>().Loading(asyncOperations));
        return currentScenes;
    }
    public void LoadScene(Scene[] scenes)
    {
        for (int i = 0; i < scenes.Length; i++)
        {

            LoadScene(scenes[i]);
        }
    }
}
[System.Serializable]public  struct Scene
{
    public string name;
    public SceneTypes typ;

    public Scene(string _name, SceneTypes _typ)
    {
        name = _name;
        typ = _typ;


    }
    
}
public enum SceneTypes
{
    level,
    UI,
    player,
    collectiblesPublic,
    logicLevel,
}