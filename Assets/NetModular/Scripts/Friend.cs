using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;

public class Friend : MonoBehaviour
{
    public Text nameText;

    public string Name
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
        switch (status)
        {
            case 1:
                // "Invisible";
                break;
            case 2:
                // "Online";
                nameText.color = Color.green;
                break;
            case 3:
                // "Away";
                break;
            case 4:
                // "Do not disturb";
                break;
            case 5:
                // "Looking for Game/Group";
                break;
            case 6:
                // "Playing";
                break;
            default:
                // "Offline";
                nameText.color = Color.red;
                break;
        }
    }
}
