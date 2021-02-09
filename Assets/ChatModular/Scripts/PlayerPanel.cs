using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    public GameObject playerPrefab;

    public void PlayersInChannel(List<string> playerList)
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            GameObject player = Instantiate(playerPrefab, transform);
            player.GetComponentInChildren<Text>().text = playerList[i];
        }
    }
}
