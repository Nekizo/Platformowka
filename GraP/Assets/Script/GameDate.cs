using UnityEngine;

public class GameDate : MonoBehaviour
{
    public static GameDate instance;
    public bool[] instructions;
    private int score;
    public System.Action StopGame;
    public System.Action StartGame;
    public bool activeGame=true;
    

    private void Awake()
    {
        instance = this;
        StopGame += StopTime;
        StartGame += StartTime;
    }
    private void StartTime()
    {
        Time.timeScale = 1;
        activeGame = true;
    }
    private void StopTime()
    {
        Time.timeScale = 0;
        activeGame = false;
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
