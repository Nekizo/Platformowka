using UnityEngine;

public class SwapScenes : MonoBehaviour
{
    
    [SerializeField] private Scene[] scenes;
    public void Swap()
    {

        for (int i = 0; i < scenes.Length; i++)
        {
            MenagerScene.instance.LoadScene(scenes[i]);
        }
    }
}
