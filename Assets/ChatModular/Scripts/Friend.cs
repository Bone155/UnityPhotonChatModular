using UnityEngine;
using UnityEngine.UI;

public class Friend : MonoBehaviour
{
    public Text nameText;
    public Image image;

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

    public void SetFriendOnlineStatus(int status)
    {
        switch (status)
        {
            case 1:
                // "Invisible";
                break;
            case 2:
                // "Online";
                image.color = Color.green;
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
                image.color = Color.red;
                break;
        }
    }
}
