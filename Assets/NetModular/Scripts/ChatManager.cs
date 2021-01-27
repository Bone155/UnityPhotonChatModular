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

    public string playerName = "PlayerName";

    public GameObject friendPrefab;
    public string[] friendsList;

    public GameObject channelPrefab;
    public string[] channelList;
    string channelName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Connect()
    {
        chatClient = new ChatClient(this)
        {
            UseBackgroundWorkerForSending = true,
            AuthValues = new AuthenticationValues(playerName)
        };
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(PlayerPrefs.GetString(playerName)));
    }

    public void ToSendMessage()
    {

    }

    public void SendChatMessage(string inputLine)
    {
        if (string.IsNullOrEmpty(inputLine))
        {
            return;
        }

        bool isPrivate = false;
        if (isPrivate)
        {
            chatClient.TryGetChannel(channelName, out ChatChannel selectedChannel);
        }
    }

    public void AddMessageToSelectedChannel(string msg)
    {

    }

    void SpawnChannel(string channelName)
    {

    }

    void SpawnFriend(string friendName)
    {
        GameObject friend = Instantiate(friendPrefab);
        friend.gameObject.SetActive(true);
        Friend friendItem = friend.GetComponent<Friend>();
        friendItem.Name = friendName;
        friend.transform.SetParent(friendPrefab.transform.parent, false);
    }

    public void ShowChannnel(string channelName)
    {
        if (string.IsNullOrEmpty(channelName))
        {
            return;
        }
    }

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

        if (friendsList != null && friendsList.Length > 0)
        {
            chatClient.AddFriends(friendsList);

            foreach(string friend in friendsList)
            {

            }
        }

        chatClient.SetOnlineStatus(ChatUserStatus.Online);
    }

    public void OnDisconnected()
    {
        
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        
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
