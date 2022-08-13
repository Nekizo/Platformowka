using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveMenager : MonoBehaviour
{
    [SerializeField] private string directory = "/SaveDate/";

    [SerializeField] private string fileNamePublic = "SavePublicDate";
    [SerializeField] private SceneTypes[] publicSceneTypes;
    public event System.Action savePunblic;

    [SerializeField] private string fileNameProgress = "SaveProgressDate";
    public event System.Action saveProgress;

    private List<ObjectData> objectDatas = new List<ObjectData>();
    public static SaveMenager instance;
    private bool landing;

    [SerializeField] private string fileNameEnd = ".save";

    public event System.Action Restart;
    public void SaveAddObjectData(ObjectData objectData)
    {
        objectDatas.Add(objectData);
    }
    
    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + directory + fileNameProgress + fileNameEnd))
        {
            using (FileStream plik = File.Open(Application.persistentDataPath + directory + fileNameProgress + fileNameEnd, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                SaveProgress date = (SaveProgress)bf.Deserialize(plik);
                plik.Close();
                GameDate.instance.instructions = date.instructions;
                GameDate.instance.ScoreReplace(date.score);
            }
        }
        if (File.Exists(Application.persistentDataPath + directory + fileNamePublic + fileNameEnd))
        {
            using (FileStream plik = File.Open(Application.persistentDataPath + directory + fileNamePublic + fileNameEnd, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                SavePublic date = (SavePublic)bf.Deserialize(plik);
                plik.Close();
                objectDatas = date.objects;
                /*for (int i = 0; i < date.objects.Count; i++)
                {
                    GameObject.Find(date.objects[i].name)?.GetComponent<SaveObjectDate>().action?.Invoke();

                }*/
                PlayerInstance.instance.transform.position = date.playerPosition.downloadVector();
                PlayerInstance.instance.transform.rotation= date.playerQuaternion.downloadVector();
                MenagerScene.instance.LoadScene(date.scenes);
                Debug.Log(MenagerScene.instance.landingScene);
                if (MenagerScene.instance.landingScene == 0)
                {
                    
                    loadObjects();
                }
                else
                {
                    
                    landing = true;
                }
            }
        }
    }

    public void SaveProgress()
    {
        saveProgress?.Invoke();
        FileStream plikScene = File.Create(Application.persistentDataPath + directory + MenagerScene.instance.CurrentScene(SceneTypes.level).name + fileNameEnd);
        SaveProgressScene dataScene = new SaveProgressScene();


        


        
        
        dataScene.objects = objectDatas;

        //data.position = new Vector3Serializable(PlayerMovement.instance.transform.position);
        //data.opponents = opponentDate;
        //data.objects = objecDate;

        BinaryFormatter bfScene = new BinaryFormatter();

        bfScene.Serialize(plikScene, dataScene);
        plikScene.Close();
        FileStream plik= File.Create(Application.persistentDataPath + directory + fileNameProgress + fileNameEnd);
        SaveProgress data = new SaveProgress();
        data.score = GameDate.instance.ScoreReturn();
        data.instructions = GameDate.instance.instructions;
        BinaryFormatter bf = new BinaryFormatter();

        bf.Serialize(plik, data);
        plik.Close();
    }
    public void SavePublic()
    {
        
        FileStream plik = File.Create(Application.persistentDataPath + directory+fileNamePublic + fileNameEnd);

        SavePublic data = new SavePublic();

        //Debug.Log("SavePublic-" + MenagerScene.instance.CurrentScene(publicSceneTypes)[0].name);
        data.scenes = MenagerScene.instance.CurrentScene(publicSceneTypes);
            
        
        data.playerPosition = new Vector3Serializable( PlayerInstance.instance.transform.position);
        data.playerQuaternion = new QuaternionSerializable(PlayerInstance.instance.transform.rotation);
        savePunblic?.Invoke();
        data.objects = objectDatas;
        //data.position = new Vector3Serializable(PlayerMovement.instance.transform.position);
        //data.opponents = opponentDate;
        //data.objects = objecDate;

        BinaryFormatter bf = new BinaryFormatter();

        bf.Serialize(plik, data);
        plik.Close();
    }
    private void Start()
    {
        MenagerScene.instance.AddedScene += loadScene;
    }
    private void loadScene(SceneTypes typ)
    {
        if (MenagerScene.instance.landingScene == 0 && landing)
        {
            loadObjects();
        }
    }
    private void loadObjects()
    {
        Debug.Log("loadObjects");
        Restart.Invoke();
        for (int i = 0; i < objectDatas.Count; i++)
        {
            if (objectDatas[i].active)
            {
                GameObject.Find(objectDatas[i].name)?.GetComponent<SaveObjectDate>().action?.Invoke();
            }
            

        }
        if(File.Exists(Application.persistentDataPath + directory + MenagerScene.instance.CurrentScene(SceneTypes.level).name + fileNameEnd))
        {
            using (FileStream plik = File.Open(Application.persistentDataPath + directory + MenagerScene.instance.CurrentScene(SceneTypes.level).name + fileNameEnd, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                SaveProgressScene date = (SaveProgressScene)bf.Deserialize(plik);
                plik.Close();

                for (int i = 0; i < date.objects.Count; i++)
                {
                    if (date.objects[i].active)
                    {
                        GameObject.Find(date.objects[i].name)?.GetComponent<SaveObjectDate>().action?.Invoke();
                    }
                    
                    

                }


            }
        }
        
    }
    private void Awake()
    {
        instance = this;
        
    }
    

}
[Serializable]
public class ObjectData
{

    public bool active;
    public string name;

    public ObjectData(bool active, string name)
    {
        this.active = active;
        this.name = name;

    }
}
[Serializable] class SavePublic
{
    public Vector3Serializable playerPosition;
    public QuaternionSerializable playerQuaternion;
    public List<ObjectData> objects = new List<ObjectData>();
    public Scene[] scenes;
}
[Serializable]
class SaveProgressScene
{
    public List<ObjectData> objects = new List<ObjectData>();
    
}
[Serializable]
class SaveProgress
{
    public int score;
    public bool[] instructions;

}
[Serializable]
class QuaternionSerializable
{
    public float x;
    public float y;
    public float z;
    public float w;
    public QuaternionSerializable(Quaternion v)
    {
        setVector(v);
    }
    public void setVector(Quaternion v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
        this.w = v.w;
    }
    public Quaternion downloadVector()
    {
        Quaternion vec = new Quaternion();
        vec.x = this.x;
        vec.y = this.y;
        vec.z = this.z;
        vec.w = this.w;
        return vec;
    }

}
[Serializable]
class Vector3Serializable
{
    public float x;
    public float y;
    public float z;
    public Vector3Serializable(Vector3 v)
    {
        setVector(v);
    }
    public void setVector(Vector3 v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
    }
    public Vector3 downloadVector()
    {
        Vector3 vec = new Vector3();
        vec.x = this.x;
        vec.y = this.y;
        vec.z = this.z;
        return vec;
    }

}