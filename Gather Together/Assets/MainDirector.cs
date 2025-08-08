using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainDirector : MonoBehaviour
{
    public InputField nameInputField;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string playerName = nameInputField.text.Trim();

            if (!string.IsNullOrEmpty(playerName))
            {
                // 이름 저장 (선택사항)
                //GameManager.Instance.PlayerName = playerName;
                ClientSocket.Instance.MakeConnection("127.0.0.1", 9000, playerName);
                
                //DontDestroyOnLoad(ClientSocket.Instance);
                SceneManager.LoadScene("GameScene");
                
                Debug.Log($"[MainDirector] PlayerName = {ClientSocket.Instance.PlayerName}");
                // 씬 전환
            }
            else
            {
                Debug.Log("이름을 입력하세요.");
            }
        }
    }
}
