using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class DeathZone : MonoBehaviour
{
    public MainManager Manager;

    public int m_Points { get; private set; }
    public int highScore { get; private set; }
    public object highScoreName { get; private set; }
    public object playerName { get; private set; }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        if(m_Points >= highScore)
        {
            highScore = m_Points;
            highScoreName = playerName;
            MainManager.Instance.SaveHighScoreText();
        }
        Manager.GameOver();
    }
}
