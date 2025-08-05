using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float walkForce = 30.0f;
    float maxWalkSpeed = 5.0f;
    void Start()
    {
        Application.targetFrameRate = 60;
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ĳ���� �̵�
        int keyX = 0;
        int keyY = 0;
        if (Input.GetKey(KeyCode.RightArrow)) keyX = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) keyX = -1;
        if (Input.GetKey(KeyCode.UpArrow)) keyY = 1;
        if (Input.GetKey(KeyCode.DownArrow)) keyY = -1;
        // X�� �÷��̾� �ӵ�
        float speedX = Mathf.Abs(this.rigid2D.linearVelocity.x);
        // y�� �÷��̾� �ӵ�
        float speedY = Mathf.Abs(this.rigid2D.linearVelocity.y);
        // ���� �¿����
        if (keyX != 0)
        {
            transform.localScale = new Vector3(keyX, 1, 1);
        }

        if (speedX < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(Vector2.right * keyX * walkForce);
        }
        if (speedY < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(Vector2.up * keyY * walkForce);
        }
    }
}
