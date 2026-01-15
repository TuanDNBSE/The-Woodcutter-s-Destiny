using System.Collections;
using UnityEngine;

public class ChanTinhBoss : MonoBehaviour
{
    // Các trạng thái của Boss
    public enum BossState { Idle, Attack, Transform, Death }
    public BossState currentState;

    public int maxHealth = 100;
    public int currentHealth;
    public bool isTransformed = false;

    void Start()
    {
        currentHealth = maxHealth;
        currentState = BossState.Idle;
        StartCoroutine(BossLogicLoop()); // Bắt đầu vòng lặp suy nghĩ của Boss
    }

    // Vòng lặp xử lý logic (State Machine)
    IEnumerator BossLogicLoop()
    {
        while (currentState != BossState.Death)
        {
            switch (currentState)
            {
                case BossState.Idle:
                    // Boss nghỉ 2 giây rồi tấn công
                    Debug.Log("Boss đang lườm người chơi...");
                    yield return new WaitForSeconds(2f);

                    // Logic chuyển trạng thái: Máu < 50% thì biến hình, còn lại thì đánh
                    if (currentHealth < 50 && !isTransformed)
                    {
                        currentState = BossState.Transform;
                    }
                    else
                    {
                        currentState = BossState.Attack;
                    }
                    break;

                case BossState.Attack:
                    Debug.Log("Boss PHUN ĐỘC!");
                    // Code sinh ra đạn độc ở đây (Instantiate)
                    yield return new WaitForSeconds(1f); // Thời gian animation đánh
                    currentState = BossState.Idle; // Đánh xong quay về nghỉ
                    break;

                case BossState.Transform:
                    Debug.Log("Boss BIẾN HÌNH KHỔNG LỒ!");
                    transform.localScale = new Vector3(2, 2, 2); // Boss to lên gấp đôi
                    isTransformed = true;
                    yield return new WaitForSeconds(3f); // Chờ biến hình xong
                    currentState = BossState.Idle;
                    break;
            }
            yield return null;
        }
    }

    // Hàm nhận sát thương (để test)
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Boss bị đánh! Máu còn: " + currentHealth);
        if (currentHealth <= 0)
        {
            currentState = BossState.Death;
            Debug.Log("Boss đã bị tiêu diệt!");

            // Gọi Karma System: Cộng điểm vì diệt quái
            GameManager.Instance.AddKarma(50);
        }
    }
}
