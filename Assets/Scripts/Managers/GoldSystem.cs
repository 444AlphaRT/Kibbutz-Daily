using UnityEngine;
using UnityEngine.UI;

public class GoldSystem : MonoBehaviour
{
    public int gold = 125;
    public Text goldText;

    void Start()
    {
        UpdateUI();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateUI();
    }

    public void SpendGold(int amount)
    {
        gold -= amount;
        if (gold < 0) gold = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        goldText.text = gold + "$";
    }
}
