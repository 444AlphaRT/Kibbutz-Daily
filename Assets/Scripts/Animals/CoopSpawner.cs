using UnityEngine;

public class CoopSpawner : MonoBehaviour
{
    // Reference to the coop in the scene
    public GameObject chickenCoop;

    // Chicken prefab to spawn
    public GameObject chickenPrefab;

    // Optional spawn point (can be a child of the coop)
    public Transform chickenSpawnPoint;

    private bool coopSpawned = false;
    private bool chickenSpawned = false;

    public int maxChickens = 3;
    private int currentChickens = 0;

    private void Start()
    {
        // Make sure coop starts hidden
        if (chickenCoop != null)
            chickenCoop.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleSpacePress();
        }
    }

    private void HandleSpacePress()
    {
        if (chickenCoop == null) return;
        // First space: show coop
        if (!coopSpawned)
        {
            chickenCoop.SetActive(true);
            coopSpawned = true;
            Debug.Log("Coop appeared");
            return;
        }

        // Second+ space: spawn chicken(s)
        if (chickenPrefab != null && currentChickens < maxChickens)
        {
            Vector3 basePos;
            if (chickenSpawnPoint != null)
                basePos = chickenSpawnPoint.position;
            else
                basePos = chickenCoop.transform.position + new Vector3(0.5f, 0f, 0.5f);
            //  NEW — different offset per chicken
            Vector3 offset = Vector3.zero;
            if (currentChickens == 0)
                offset = new Vector3(-0.3f, 0f, 0f);
            else if (currentChickens == 1)
                offset = new Vector3(0.6f, 0f, 0f);
            else if (currentChickens == 2)
                offset = new Vector3(0f, 0.3f, 0f);
            Vector3 pos = basePos + offset;
            Instantiate(chickenPrefab, pos, chickenCoop.transform.rotation);
            currentChickens++;
            chickenSpawned = currentChickens >= maxChickens;
            Debug.Log($"Chicken spawned ({currentChickens}/{maxChickens})");
        }
        else
        {
            Debug.Log("No more chickens can be spawned");
        }
    }
}
