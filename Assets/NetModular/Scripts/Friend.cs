using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Pun;

public class Friend : MonoBehaviour
{
    string name = "";
    string onlineStatus = "";

    public void SetData(string friendName)
    {
        name = friendName;
    }

    public void SetFriendOnlineStatus(int status)
    {
        string friendStatus;

        switch (status)
        {
            case 1:
                friendStatus = "Online";
                break;
            default:
                friendStatus = "Offline";
                break;
        }

        onlineStatus = friendStatus;
    }
}
