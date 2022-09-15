using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    public static MenuUIHandler scene1;
    public string userName;
    public TMP_InputField userInputField;

    public void Awake()
    {
        if (scene1 == null)
        {
            scene1 = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartNew()
    {
        userName = userInputField.text;
        SceneManager.LoadScene(1);
    }
}
