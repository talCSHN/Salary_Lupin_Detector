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
            Debug.Log($"[GameManager] Init() 호출 완료 - name: {myName}");
        }
        else
        {
            Debug.LogError("[GameManager] PlayerController 컴포넌트가 없습니다!");
        }
    }

    private void OnMessageReceived(string msg)
    {
        Debug.Log("[GameManager] MessageReceived Callback");
        string[] msgFromServer = msg.Split(' '); // LOGIN Username 좌표
        if (msgFromServer[0] == "LOGIN")
        {
            string name = msgFromServer[1];
            int seat = int.Parse(msgFromServer[2]);

            // 나는 제외
            if (name == ClientSocket.Instance.PlayerName) return;

            // 다른 유저 캐릭터 생성
            GameObject player = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
            var controller = player.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.Init(name);
                Debug.Log($"[GameManager] 다른 플레이어 입장: {name}, 좌석: {seat}");
            }
            else
            {
                Debug.LogError("[GameManager] PlayerController 컴포넌트가 없습니다!");
            }
        }
    }


    void Update()
    {
        
    }
}
