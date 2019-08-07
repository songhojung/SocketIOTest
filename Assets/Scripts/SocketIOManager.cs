using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using socket.io;

public class User_UserInfo
{
    public string UserName;
    public int UserId;

    public User_UserInfo(string name, int id)
    {
        UserName = name;
        UserId = id;
    }
}


public class SocketIOManager : MonoBehaviour
{

    #region sigleton
    private static SocketIOManager _instance;
    public static SocketIOManager Getsingleton
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(SocketIOManager)) as SocketIOManager;

                if (_instance == null)
                {
                    GameObject instanceObj = new GameObject("SocketIOManager");
                    _instance = instanceObj.AddComponent<SocketIOManager>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
            }

            return _instance;
        }
    }


    void Awake()
    {
        _instance = this;


    }
    #endregion



    public string userName;
    public int userid;
    public Player otherPlayer;

    private Socket socket;


    string serverUrl = "http://192.168.0.119:1337";

  

    private void Start()
    {
        socket = Socket.Connect(serverUrl);
        socket.On("connect", Callback_Connect);
        socket.On("login", Callback_Login);
        socket.On("moveUnit", Callback_MoveUnit);

        
    }


    void Callback_Connect()
    {
        var data = JsonUtility.ToJson(new User_UserInfo(userName, userid));
        
        socket.EmitJson("login", data);
    }

    void Callback_Login(string data )
    {
        Debug.Log(data);
    }


    void Callback_MoveUnit(string data)
    {
        Debug.Log(data);
        Vector3 pos = JsonUtility.FromJson<Vector3>(data);

        otherPlayer.Move(pos);
    }






    //==================Send======================


    public void Send_Move(Vector3 pos)
    {
        var data = JsonUtility.ToJson(pos);
        socket.EmitJson("moveUnit",data);
    }






    private void OnApplicationQuit()
    {
        socket.Emit("disconnect");
        
    }
}
