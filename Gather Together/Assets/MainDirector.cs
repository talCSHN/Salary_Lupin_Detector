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

                // �� ��ȯ
                SceneManager.LoadScene("GameScene");
            }
            else
            {
                Debug.Log("�̸��� �Է��ϼ���.");
            }
        }
    }
}
