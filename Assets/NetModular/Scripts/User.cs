using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct User
{
    [Tooltip("The User's sign in username")]  string username;
    [Tooltip("The User's sign in password")] string password;
    [Tooltip("The list of User's friends")] List<string> friends;
    [Tooltip("The list of User's subscibed channels")] List<string> subChannels;
}
