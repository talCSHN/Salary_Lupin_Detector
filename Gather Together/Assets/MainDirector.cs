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
                // �̸� ���� (���û���)
                //GameManager.Instance.PlayerName = playerName;
                ClientSocket.Instance.MakeConnection("127.0.0.1", 9000, playerName);
                
                //DontDestroyOnLoad(ClientSocket.Instance);
                SceneManager.LoadScene("GameScene");
                
                Debug.Log($"[MainDirector] PlayerName = {ClientSocket.Instance.PlayerName}");
                // �� ��ȯ
            }
            else
            {
                Debug.Log("�̸��� �Է��ϼ���.");
            }
        }
    }
}
