using UnityEngine;

public class GameDate : MonoBehaviour
{
    public static GameDate instance;
    public bool[] instructions;
    private int score;
    
    private void Awake()
    {
        instance = this;
    }
    public int ScoreReturn()
    {
        return score;
    }
    public void ScoreReplace(int value)
    {
        score = value;
    }
}
