using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerName : MonoBehaviour
{
    public GameObject TMP_InputField_Playername;

    public void StoreName()
    {
        string playerName = TMP_InputField_Playername.GetComponent<TMP_InputField>().text;
        Debug.Log("Player Name: " + playerName);
    }
}
