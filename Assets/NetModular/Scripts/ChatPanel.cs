using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;

public class ChatPanel : MonoBehaviour
{
    [SerializeField] InputField chatInputField;
    [SerializeField] GameObject messagePrefab;
    [SerializeField] ScrollRect scrollChatRect;

    RectTransform rectTransform;
    bool isMine;
    const int minChildCanBeOnScreen = 13;

    void Start()
    {
        rectTransform = scrollChatRect.content.GetComponent<RectTransform>();
        LoadMessage();
    }

    #region Private Methods

    void ShowMessage(string message, bool isMine)
    {
        GameObject msg = Instantiate(messagePrefab, scrollChatRect.content);
        msg.GetComponent<Message>().ShowMessage(message, isMine);
    }

    void LoadMessage()
    {
        

    }

    IEnumerator SetContentPosition(int childCount = 1, float duration = 1)
    {
        float time = 0;

        Vector2 newPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + childCount * 120);

        while (time < duration)
        {
            time += Time.deltaTime;

            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, newPosition, time * 5);

            yield return null;
        }
    }

    #endregion

    #region Public Methods

    public void SendMessageToFriend()
    {
        string msg = chatInputField.text;
        if (!string.IsNullOrEmpty(msg))
        {
            //ChatManager.chatClient.SendPrivateMessage(ChatManager.privateChatTarget, msg);
        }
        chatInputField.text = "";
    }

    public void GetMessageFromFriend(string sender, object message, string channelName)
    {
        isMine = sender == PlayerPrefs.GetString(ChatManager.playerNameKey);
        ShowMessage(message.ToString(), isMine);
        
    }


    #endregion
}
