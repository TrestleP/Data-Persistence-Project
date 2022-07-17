using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public Text HighScoreText;
    public static int highScore = 0;
    public static Text highScoreName;

    public void StartNew()
    {
        // MainManager.Instance.LoadHighScoreText();
        // HighScoreText.text = $"Best Score: {highScoreName}: {highScore}";
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        MainManager.Instance.SaveHighScoreText();

#if UNITY_EDITOR
EditorApplication.ExitPlaymode();
#else
        Application.Quit();

#endif
    }

    
}
