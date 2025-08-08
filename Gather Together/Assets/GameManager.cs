using System.Xml.Linq;
using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.TextCore.Text;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    private readonly ClientSocket client = ClientSocket.Instance;
    string myName = ClientSocket.Instance.PlayerName;
    
    void Start()
    {
        client.MessageReceived += OnMessageReceived;
        //playerPrefab = GameObject.Find("PlayerPrefab");
        
        Debug.Log($"[GameManager] Creating character for {myName}");

        GameObject player = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        var controller = player.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.Init(myName);
            Debug.Log($"[GameManager] Init() ȣ�� �Ϸ� - name: {myName}");
        }
        else
        {
            Debug.LogError("[GameManager] PlayerController ������Ʈ�� �����ϴ�!");
        }
    }

    private void OnMessageReceived(string msg)
    {
        Debug.Log("[GameManager] MessageReceived Callback");
        string[] msgFromServer = msg.Split(' '); // LOGIN Username ��ǥ
        if (msgFromServer[0] == "LOGIN")
        {
            string name = msgFromServer[1];
            int seat = int.Parse(msgFromServer[2]);

            // ���� ����
            if (name == ClientSocket.Instance.PlayerName) return;

            // �ٸ� ���� ĳ���� ����
            GameObject player = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
            var controller = player.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.Init(name);
                Debug.Log($"[GameManager] �ٸ� �÷��̾� ����: {name}, �¼�: {seat}");
            }
            else
            {
                Debug.LogError("[GameManager] PlayerController ������Ʈ�� �����ϴ�!");
            }
        }
    }


    void Update()
    {
        
    }
}
