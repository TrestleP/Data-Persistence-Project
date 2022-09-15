using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    public string userName { get; internal set; }
    public string playerName { get; internal set; }
    public string highScoreName { get; internal set; }
    public int highScore;


    private void Awake()
    {
        playerName = MenuUIHandler.scene1.userName;
        LoadHighScore();
    }

    // Start is called before the first frame update
    void Start()
    {
        HighScoreText.text = "Best Score: " + highScoreName + " " + highScore;
        ScoreText.text = $"{playerName} Score : 0";

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            //I put this here to exit the game using the Unity Editor application exit
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Exit();
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{playerName} Score : {m_Points}";
        if (m_Points >= highScore)
        {
            highScore = m_Points;
            highScoreName = playerName;
            HighScoreText.text = "Best Score: " + highScoreName + " " + highScore;

        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        SaveHighScore();
    }

    public void Exit()
    {
        SaveHighScore();
#if UNITY_EDITOR

        EditorApplication.ExitPlaymode();

#else
        Application.Quit();

#endif
    }

    [System.Serializable]
    public class PlayerData
    {
        public string highScoreName;
        public int highScore;
    }

    public void SaveHighScore()
    {
        if (m_Points >= highScore)
        {
            PlayerData data = new PlayerData();
            data.highScore = m_Points;
            data.highScoreName = playerName;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            highScore = data.highScore;
            highScoreName = data.highScoreName;
        }
    }
}
