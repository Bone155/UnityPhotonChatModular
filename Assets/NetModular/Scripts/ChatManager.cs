using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Pun;
using ExitGames.Client.Photon;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    ChatClient chatClient;

    public InputField playerName;
    public InputField chatInput;
    public Text chatDisplay;

    public GameObject joinChatPanel;
    public GameObject worldChatPanel;

    string worldChat;

    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat))
        {
            return;
        }

        worldChat = "world";
    }

    // Update is called once per frame
    void Update()
    {
        if (chatClient != null)
        {
            chatClient.Service();
            if (Input.GetKey(KeyCode.Return))
            {
                SendMessage();
            }
        }
    }

    #region Public Methods

    public void GetConnected()
    {
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(playerName.text));
    }

    public void SendMessage()
    {
        chatClient.PublishMessage(worldChat, chatInput.text);
    }

    #endregion

    #region IChatClientListner implementation

    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnChatStateChange(ChatState state)
    {
        
    }

    public void OnConnected()
    {
        joinChatPanel.SetActive(false);
        worldChatPanel.SetActive(true);
        chatClient.Subscribe(new string[] { worldChat });
        chatClient.SetOnlineStatus(ChatUserStatus.Online);
    }

    public void OnDisconnected()
    {
        chatClient.SetOnlineStatus(ChatUserStatus.Offline);
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            chatDisplay.text += senders[i] + ": " + messages[i] + "\n";
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        foreach(var channel in channels)
        {
            chatClient.PublishMessage(channel, "joined");
        }
    }

    public void OnUnsubscribed(string[] channels)
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
