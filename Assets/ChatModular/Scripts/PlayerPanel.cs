using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    public GameObject playerPrefab;

    public void InstaniatePlayer(string name)
    {
        GameObject player = Instantiate(playerPrefab, transform);
        player.GetComponentInChildren<Text>().text = name;
    }

    public void OnRightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {

        }
    }
}
