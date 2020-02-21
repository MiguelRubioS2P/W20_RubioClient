using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string _httpServerAddress = "http://localhost:58357/";
    public string HttpServerAddress
    {
        get
        {
            return _httpServerAddress;
        } 
    }

    public string _token;
    public string Token
    {
        get { return _token; }
        set { _token = value; }
    }

    public string _id;
    public string Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public DateTime _birthday;
    public DateTime BirthDay
    {
        get { return _birthday; }
        set { _birthday = value; }
    }

    public string _email;
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    void Awake()
    {
        //int count = FindObjectOfType<Player>();
        int count = FindObjectsOfType<Player>().Length;
        if (count > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

}
