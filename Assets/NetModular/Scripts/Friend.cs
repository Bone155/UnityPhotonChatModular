using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Pun;

public class Friend : MonoBehaviour
{
    public Text nameText;
    public Text statusText;

    public string friendName
    {
        set
        {
            nameText.text = value;
        }
        get
        {
            return nameText.text;
        }
    }

    public void SetFriendOnlineStatus(int status, bool gotMessage, object message)
    {
        string friendStatus;

        switch (status)
        {
            case 1:
                friendStatus = "Invisible";
                break;
            case 2:
                friendStatus = "Online";
                statusText.color = Color.green;
                break;
            case 3:
                friendStatus = "Away";
                break;
            case 4:
                friendStatus = "Do not distrub";
                break;
            case 5:
                friendStatus = "Looking For Game/Group";
                break;
            case 6:
                friendStatus = "Playing";
                break;
            default:
                friendStatus = "Offline";
                statusText.color = Color.red;
                break;
        }

        statusText.text = friendStatus;

    }
}
