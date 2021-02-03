using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerSignIn : MonoBehaviour
{
    const string playerNamePrefKey = "PlayerName";

    public InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        string prefsName = PlayerPrefs.GetString(playerNamePrefKey);
        if (!string.IsNullOrEmpty(prefsName))
        {
            inputField.text = prefsName;
        }
        PhotonNetwork.NickName = prefsName;
    }

    public void OnEnter()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            StartChat();
        }
    }

    public void StartChat()
    {
        ChatManager chatManager = FindObjectOfType<ChatManager>();
        chatManager.playerName = inputField.text.Trim();
        chatManager.Connect();
        enabled = false;

        PlayerPrefs.SetString(playerNamePrefKey, chatManager.playerName);
    }

}
