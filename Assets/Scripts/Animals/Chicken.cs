using System.Collections;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [Header("Hunger")]
    public bool isHungry = true;
    public float eatingTime = 1.5f;

    [Header("Eggs")]
    public GameObject eggPrefab;
    public float timeToLayEgg = 3f;
    public Vector3 eggOffset = new Vector3(0f, -0.35f, 0f);


    private bool isEating = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    private void OnMouseDown()
    {
        if (!isHungry || isEating)
            return;

        StartCoroutine(FeedRoutine());
    }

    private IEnumerator FeedRoutine()
    {
        isEating = true;
        Debug.Log("Chicken started eating");

        if (spriteRenderer != null)
            spriteRenderer.color = Color.green;

        yield return new WaitForSeconds(eatingTime);

        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;

        isHungry = false;
        isEating = false;

        Debug.Log("Chicken finished eating!");


        StartCoroutine(LayEggAfterDelay());
    }

    private IEnumerator LayEggAfterDelay()
    {

        yield return new WaitForSeconds(timeToLayEgg);

        if (eggPrefab != null)
        {
            Vector3 spawnPos = transform.position + eggOffset;
            Instantiate(eggPrefab, spawnPos, Quaternion.identity);
            Debug.Log("Egg laid!");
        }
        else
        {
            Debug.LogWarning("Chicken has no eggPrefab assigned!");
        }
    }

    public void MakeHungryAgain()
    {
        isHungry = true;
    }

    public bool IsFed()
    {
        return !isHungry;
    }
}
