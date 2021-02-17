using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Pun;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    public ChatClient chatClient;
    public int HistoryChatLength = 0;

    ChatChannel chatChannel;

    public GameObject channelPrefab;
    public Transform channelPanel;
    public List<string> channelList;

    public PlayerPanel playerPanel;

    public InputField signInField;

    string playerName = "PlayerName";
    public Text playerId;

    public GameObject chatPanel;
    public GameObject signInPanel;

    public InputField chatInput;
    public Text channelDisplayText;

    Color myColor;
    Color otherColor;

    // Start is called before the first frame update
    void Start()
    {
        signInPanel.SetActive(true);
        chatPanel.SetActive(false);

        playerId.text = "";
        playerId.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (chatClient != null)
        {
            chatClient.Service();
        }
    }

    public void OnDestroy()
    {
        if (chatClient != null)
        {
            chatClient.Disconnect();
        }
    }

    #region Public Methods

    public void EnterName()
    {
        if ((Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)))
        {
            Connect();
        }
    }

    public void SendMsg()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            channelDisplayText.color = myColor;
            SendChatMessage(chatInput.text);
            chatInput.text = "";
        }
    }

    public void ClickSendMsg()
    {
        if (!string.IsNullOrEmpty(chatInput.text))
        {
            channelDisplayText.color = myColor;
            SendChatMessage(chatInput.text);
            chatInput.text = "";
        }
    }

    public void ShowChannnel(string channelName)
    {
        if (string.IsNullOrEmpty(channelName))
        {
            return;
        }

        bool foundChannel = chatClient.TryGetChannel(channelName, out chatChannel);
        if (!foundChannel) { return; }
        channelDisplayText.color = otherColor;
        channelDisplayText.text = chatChannel.ToStringMessages();
        Refresh();
    }

    #endregion

    #region Private Methods
    
    void Connect()
    {
        signInPanel.SetActive(false);

        if (!string.IsNullOrEmpty(signInField.text))
        {
            playerName = signInField.text;
            PhotonNetwork.NickName = playerName;
        }

        chatClient = new ChatClient(this);

        chatChannel = new ChatChannel(channelList[0]);

        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(playerName));
    }

    void InstantiateChannel(string channelName)
    {
        GameObject channelObject = Instantiate(channelPrefab, channelPanel);
        channelObject.gameObject.SetActive(true);
        channelObject.GetComponentInChildren<SelectChannel>().SetChannel(channelName);
        channelList.Add(channelName);
    }

    void SendChatMessage(string inputLine)
    {
        if (!string.IsNullOrEmpty(inputLine))
        {
            return;
        }

        bool isPrivate = chatClient.PrivateChannels.ContainsKey(chatChannel.Name);
        string privateChatTarget = string.Empty;
        if (isPrivate)
        {
            
        }

        if (inputLine.Equals("@" + privateChatTarget))
        {
            chatClient.SendPrivateMessage(privateChatTarget, inputLine);
        }
        else if (inputLine.Equals("clear_messages"))
        {
            if (isPrivate)
            {
                chatClient.PrivateChannels.Remove(chatChannel.Name);
            }
            else
            {
                if (chatClient.TryGetChannel(chatChannel.Name, isPrivate, out chatChannel))
                {
                    chatChannel.ClearMessages();
                }
            }
        }
        else if (inputLine.Equals("leave_channel"))
        {
            chatClient.Unsubscribe(new string[] { chatChannel.Name });
        }
        else
        {
            if (isPrivate)
            {
                chatClient.SendPrivateMessage(privateChatTarget, inputLine);
            }
            else
            {
                chatClient.PublishMessage(chatChannel.Name, inputLine);
            }
        }

    }

    void Refresh()
    {
        string[] players = new string[chatChannel.MaxSubscribers];
        chatChannel.Subscribers.CopyTo(players);
        for (int i = 0; i < players.Length; i++)
        {
            playerPanel.InstaniatePlayer(players[i]);
        }
    }

    #endregion

    #region IChatClientListener implementation

    public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
    {
        
    }

    public void OnChatStateChange(ChatState state)
    {
        
    }

    public void OnConnected()
    {
        if (channelList != null && channelList.Count > 0)
        {
            chatClient.Subscribe(channelList.ToArray(), HistoryChatLength);
        }
        playerId.text = playerName;
        chatPanel.SetActive(true);

        chatClient.SetOnlineStatus(ChatUserStatus.Online);

        myColor = new Color(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f));
    }

    public void OnDisconnected()
    {
        chatClient.SetOnlineStatus(ChatUserStatus.Offline);
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        otherColor = new Color(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f));
        if (channelName.Equals(chatChannel.Name))
        {
            ShowChannnel(channelName);
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        otherColor = new Color(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f));

        if (chatChannel.Name.Equals(channelName))
        {
            ShowChannnel(channelName);
        }
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        foreach (string channel in channels)
        {
            chatClient.PublishMessage(channel, "HELLO " + channel);
            channelList.Add(channel);
            InstantiateChannel(channel);
        }

        ShowChannnel(channels[0]);
    }

    public void OnUnsubscribed(string[] channels)
    {
        foreach (string channelName in channels)
        {
            if (channelList.Contains(channelName))
            {
                channelList.Remove(channelName);
            }
        }
    }

    public void OnUserSubscribed(string channel, string user)
    {
        bool isPrivate = chatClient.PrivateChannels.ContainsKey(channel);
        if (isPrivate)
        {
            chatClient.SendPrivateMessage(channel, "Who's ready to chat");
        }
        else
        {
            chatClient.PublishMessage(channel, "Who's ready to chat");
        }
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        bool isPrivate = chatClient.PrivateChannels.ContainsKey(channel);
        if (isPrivate)
        {
            chatClient.SendPrivateMessage(channel, "See ya");
        }
        else
        {
            chatClient.PublishMessage(channel, "See ya");
        }
    }

    #endregion

}
