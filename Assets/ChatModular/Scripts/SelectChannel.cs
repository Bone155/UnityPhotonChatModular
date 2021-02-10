using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectChannel : MonoBehaviour
{
    public string Channel;
    
    public void SetChannel(string channel)
    {
        Channel = channel;
        Text text = GetComponentInChildren<Text>();
        text.text = Channel;
    }

    public void ChannelSelect()
    {
        ChatManager handler = FindObjectOfType<ChatManager>();
        handler.ShowChannnel(Channel);
    }
}
