using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Tạo Singleton để các script khác dễ dàng gọi (VD: GameManager.Instance.AddKarma(5))
    public static GameManager Instance;

    public int karmaScore = 0; // Biến lưu điểm đạo đức

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject); // Giữ nguyên khi chuyển màn chơi
    }

    // Hàm cộng/trừ điểm Karma
    public void AddKarma(int amount)
    {
        karmaScore += amount;
        Debug.Log("Điểm Karma hiện tại: " + karmaScore);

        // Logic kiểm tra Ending (ví dụ)
        if (karmaScore < -50) Debug.Log("Cảnh báo: Sắp có Bad Ending!");
    }
}
