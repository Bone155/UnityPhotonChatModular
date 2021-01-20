using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendPanel : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject friendPrefab;
    [SerializeField] Transform friendParent;
    public List<Friend> friends = new List<Friend>();

    public void SetData(string userName, List<string> friendsList)
    {
        for (int i = 0; i < friendsList.Count; i++)
        {
            GameObject go_Friend = Instantiate(friendPrefab, friendParent);
            go_Friend.name = friendsList[i];
            Friend friend = go_Friend.GetComponent<Friend>();
            //friend.SetData(go_Friend.name, transform);
            friends.Add(friend);
        }

    }

}
