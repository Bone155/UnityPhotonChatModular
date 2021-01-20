using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Pun;
using ExitGames.Client.Photon;
using System.Collections.Generic;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    [SerializeField] GameObject friendPanel;

    public ChatClient chatClient;

    public InputField playerNameInputField;
    public InputField createChannelInput;
    public InputField chatInput;
    string worldChat = "world";
    
    public GameObject chatPanel;
    public const string playerNameKey = "PlayerName";
    public static string privateChatTarget;
    ChatChannel chatChannel;

    List<string> friendList = new List<string> { "Nitil", "Raj" };
    bool isGamePaused;

    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat))
        {
            return;
        }
        if (PlayerPrefs.HasKey(playerNameKey))
        {
            StartChatClient();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (chatClient != null)
        {
            chatClient.Service();
        }
    }

    private void OnApplicationQuit()
    {
        //chatPanel.GetComponent<ChatPanel>().OnDisconnection();
    }

    public void StartChatClient()
    {
        if (!PlayerPrefs.HasKey(playerNameKey))
        {
            PlayerPrefs.SetString(playerNameKey, playerNameInputField.text);
            PhotonNetwork.NickName = playerNameInputField.text;
        }

        chatClient = new ChatClient(this);
        chatChannel = new ChatChannel(worldChat);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(PlayerPrefs.GetString(playerNameKey)));
    }

    public void OnSendMessage()
    {
        if (!string.IsNullOrEmpty(chatInput.text) && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)))
        {
            chatClient.PublishMessage(chatChannel.Name, chatInput.text);
        }
    }

    public void OnCreateChannel()
    {
        chatChannel = new ChatChannel(createChannelInput.name);
        chatClient.Subscribe(new string[] { chatChannel.Name });
    }

    #region IChatClientListner implementation

    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnChatStateChange(ChatState state)
    {
        
    }

    public void OnConnected()
    {
        chatClient.AddFriends(friendList.ToArray());
        chatClient.Subscribe(new string[] { worldChat });
        chatClient.SetOnlineStatus(ChatUserStatus.Online);

    }

    public void OnDisconnected()
    {
        
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            //chatDisplay.text += senders[i] + ": " + messages[i] + "\n";
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        
    }

    public void OnUnsubscribed(string[] channels)
    {
        
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }
    
    public void OnUserSubscribed(string channel, string user)
    {
        
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        
    }

    #endregion
}
