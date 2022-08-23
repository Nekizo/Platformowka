using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] Transform bar;
    [SerializeField] Transform loadingScreen;
    [SerializeField] private Vector3 on;
    [SerializeField] private Vector3 off;
    public IEnumerator Loading(List<AsyncOperation> asyncOperations)
    {
        loadingScreen.position = on;
        while (asyncOperations.Count != 0)
        {
            float sum = 0;
            for (int i = 0; i < asyncOperations.Count; i++)
            {
                if (asyncOperations[i].progress == 1)
                {
                    asyncOperations.RemoveAt(i);
                }
                else
                {
                    sum += asyncOperations[i].progress;
                }
                


            }
            if (asyncOperations.Count != 0)
            {
                bar.localScale = new Vector2(sum / asyncOperations.Count, 1);
            }
            
            yield return null;
        }
        yield return null;
    }
}
