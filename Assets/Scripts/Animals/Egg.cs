using UnityEngine;

public class Egg : MonoBehaviour
{
    public static int totalEggsCollected = 0;

    public int goldPerEgg = 3;

    public GoldSystem goldSystem;

    private void Start()
    {
        if (goldSystem == null)
        {
            goldSystem = FindFirstObjectByType<GoldSystem>();
        }
    }


    private void OnMouseDown()
    {
        CollectEgg();
    }

    private void CollectEgg()
    {
        totalEggsCollected++;
        Debug.Log("Egg collected! Total: " + totalEggsCollected);

        if (goldSystem != null)
        {
            goldSystem.AddGold(goldPerEgg);
        }
        else
        {
            Debug.LogWarning("Egg: No GoldSystem found, can't add gold.");
        }

        Destroy(gameObject);
    }
}
