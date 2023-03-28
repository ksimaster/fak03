using UnityEngine;


public class Lederboard : MonoBehaviour
{
    public string nameScene;
    void Start()
    {
        if(nameScene == "Menu") SetHighScoreOnLederboard();
        if (!PlayerPrefs.HasKey("HighScore")) PlayerPrefs.SetInt("HighScore", 0);
    }

    public void SetHighScoreOnLederboard()
    {
        
        int best = PlayerPrefs.GetInt("HighScore");
#if UNITY_WEBGL && !UNITY_EDITOR
    	WebGLPluginJS.SetLeder(best);
#endif
    }

    public void HighScore()
    {
        if(PlayerPrefs.GetInt("ScoreMonsters") > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("ScoreMonsters"));
            Debug.Log(PlayerPrefs.GetInt("HighScore"));
            SetHighScoreOnLederboard();
        }
    }

    private void Update()
    {
        HighScore();
    }



}
