using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public float maxEnergy = 100f;
    public float currentEnergy;

    private UIManager uiManager;

    void Start()
    {
        currentEnergy = maxEnergy;
        uiManager = FindObjectOfType<UIManager>();
        UpdateUI();
    }

    public void UseEnergy(float amount)
    {
        currentEnergy -= amount;
        if (currentEnergy < 0) currentEnergy = 0;

        UpdateUI();
    }

    public void RestoreEnergy(float amount)
    {
        currentEnergy += amount;
        if (currentEnergy > maxEnergy) currentEnergy = maxEnergy;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (uiManager != null)
        {
            uiManager.UpdateEnergy(currentEnergy, maxEnergy);
        }
    }
}
