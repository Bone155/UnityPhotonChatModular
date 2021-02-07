using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Pun;
using ExitGames.Client.Photon;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    public ChatClient chatClient;
    public int HistoryChatLength = 0;

    public string[] friendsList;
    public string[] channelList;

    public string playerName = "PlayerName";
    public Text playerId;

    public GameObject chatPanel;
    public GameObject signInPanel;

    public Transform channelPanel; // to set channelPrefab parent
    public Transform friendPanel; // to set friendPrefab parent

    public InputField chatInput;

    public GameObject friendPrefab;

    public GameObject channelPrefab;
    public Text channelDisplayText;
    string selectedChannelName;

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

    public void Connect()
    {
        signInPanel.SetActive(false);
        chatClient = new ChatClient(this)
        {
            UseBackgroundWorkerForSending = true,
            AuthValues = new AuthenticationValues(playerName)
        };
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(PlayerPrefs.GetString(playerName)));
    }

    public void EnterSend()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            channelDisplayText.color = myColor;
            SendChatMessage(chatInput.text);
            chatInput.text = "";
        }
    }

    public void ClickSend()
    {
        if (chatInput != null)
        {
            channelDisplayText.color = myColor;
            SendChatMessage(chatInput.text);
            chatInput.text = "";
        }
    }

    public void SendChatMessage(string inputLine)
    {
        if (!string.IsNullOrEmpty(inputLine))
        {
            return;
        }

        bool isPrivate = chatClient.PrivateChannels.ContainsKey(selectedChannelName);
        string privateChatTarget = string.Empty;
        if (isPrivate)
        {
            string[] splitNames = selectedChannelName.Split(new char[] { ':' });
            privateChatTarget = splitNames[1];
        }

        if (inputLine[0].Equals('@'))
        {
            //string[] line = inputLine.Split(new char[] { ' ' }, 2);
            //string target = string.Empty;
            //if (line[0].Equals('@' + privateChatTarget) && !string.IsNullOrEmpty(line[1]))
            //{
            //    target = privateChatTarget;
            //    string message = line[1];
            //    chatClient.SendPrivateMessage(target, message);
            //}

        }
        else
        {
            if (isPrivate)
            {
                chatClient.SendPrivateMessage(privateChatTarget, inputLine);
            }
            else
            {
                chatClient.PublishMessage(selectedChannelName, inputLine);
            }
        }

    }

    public void ShowChannnel(string channelName)
    {
        if (string.IsNullOrEmpty(channelName))
        {
            return;
        }

        bool foundChannel = chatClient.TryGetChannel(channelName, out ChatChannel channel);
        if (!foundChannel) { return; }
        selectedChannelName = channelName;
        channelDisplayText.color = otherColor;
        channelDisplayText.text = channel.ToStringMessages();

    }

    #endregion

    #region IChatClientListener implementation

    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnChatStateChange(ChatState state)
    {
        
    }

    public void OnConnected()
    {
        if (channelList != null && channelList.Length > 0)
        {
            chatClient.Subscribe(channelList, HistoryChatLength);
        }
        playerId.text = playerName;
        chatPanel.SetActive(true);
        if (friendsList != null && friendsList.Length > 0)
        {
            chatClient.AddFriends(friendsList);

            foreach(string friend in friendsList)
            {
                if (friendPrefab != null && friend != playerName)
                {
                    SpawnFriend(friend);
                }
            }
        }

        if (friendPrefab != null)
        {
            friendPrefab.SetActive(false);
        }

        chatClient.SetOnlineStatus(ChatUserStatus.Online);

        myColor = new Color(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f));
    }

    public void OnDisconnected()
    {
        
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        otherColor = new Color(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f));
        if (channelName.Equals(selectedChannelName))
        {
            ShowChannnel(selectedChannelName);
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        otherColor = new Color(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f));

        SpawnChannel(channelName);

        if (selectedChannelName.Equals(channelName))
        {
            ShowChannnel(channelName);
        }
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        //if (friendItemList.ContainsKey(playerName))
        //{
        //    Friend friend = friendItemList[playerName];
        //    if (friend != null)
        //    {
        //        friend.SetFriendOnlineStatus(status);
        //    }
        //}
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        foreach (string channel in channels)
        {
            chatClient.PublishMessage(channel, "HELLO WORLD!!!!!!!!");

            if (channelPrefab != null)
            {
                SpawnChannel(channel);
            }
        }

        ShowChannnel(channels[0]);
    }

    public void OnUnsubscribed(string[] channels)
    {
        foreach (string channelName in channels)
        {
            //if (channelButtons.ContainsKey(channelName))
            //{
            //    Button button = channelButtons[channelName];
            //    Destroy(button.gameObject);

            //    channelButtons.Remove(channelName);

            //    if (channelName == selectedChannelName && channelButtons.Count > 0)
            //    {
            //        IEnumerator<KeyValuePair<string, Button>> channelEntry = channelButtons.GetEnumerator();
            //        channelEntry.MoveNext();

            //        ShowChannnel(channelEntry.Current.Key);
            //    }
            //}
        }
    }

    public void OnUserSubscribed(string channel, string user)
    {
        
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        
    }

    #endregion

    void SpawnChannel(string channelName)
    {
        //if (channelButtons.ContainsKey(channelName))
        //{
        //    return;
        //}

        GameObject channel = Instantiate(channelPrefab);
        channel.SetActive(true);
        channel.transform.SetParent(channelPanel, false);
        channel.GetComponentInChildren<SelectChannel>().SetChannel(channelName);
        //channelButtons.Add(channelName, channel.GetComponent<Button>());
    }

    void SpawnFriend(string friendName)
    {
        GameObject friend = Instantiate(friendPrefab);
        friend.gameObject.SetActive(true);
        Friend friendItem = friend.GetComponent<Friend>();
        friendItem.Name = friendName;
        friend.transform.SetParent(friendPanel, false);
        //friendItemList[friendName] = friendItem;
    }

}
