using UnityEngine;

public enum FieldState
{
    Empty,
    Seeded,
    Watered,
    ReadyToHarvest
}

public class Field : MonoBehaviour
{
    // current state of this field
    public FieldState state = FieldState.Empty;

    // time (in seconds) from watering until crop is ready
    public float growTime = 5f;
    private float growTimer;

    // visuals on top of the base tile
    public GameObject seedVisual;   // small yellow seeds
    public GameObject waterVisual;  // blue water drops
    public GameObject cropVisual;   // final crop

    private void Start()
    {
        // make sure all overlays are hidden at the beginning
        if (seedVisual != null) seedVisual.SetActive(false);
        if (waterVisual != null) waterVisual.SetActive(false);
        if (cropVisual != null) cropVisual.SetActive(false);
    }

    private void Update()
    {
        // handle growth after watering
        if (state == FieldState.Watered)
        {
            growTimer -= Time.deltaTime;
            if (growTimer <= 0f)
            {
                state = FieldState.ReadyToHarvest;

                // hide water, show crop
                if (waterVisual != null) waterVisual.SetActive(false);
                if (cropVisual != null) cropVisual.SetActive(true);
            }
        }
    }

    /// First click – plant seeds.
    public void Plant()
    {
        if (state != FieldState.Empty)
            return;

        state = FieldState.Seeded;

        // show seeds, hide others
        if (seedVisual != null) seedVisual.SetActive(true);
        if (waterVisual != null) waterVisual.SetActive(false);
        if (cropVisual != null) cropVisual.SetActive(false);

        // notify tutorial
        if (TutorialManager.Instance != null)
            TutorialManager.Instance.OnFieldPlanted();
    }

    /// Second click – water the field.
    public void Water()
    {
        if (state != FieldState.Seeded)
            return;

        state = FieldState.Watered;
        growTimer = growTime;

        // hide seeds, show water
        if (seedVisual != null) seedVisual.SetActive(false);
        if (waterVisual != null) waterVisual.SetActive(true);
        if (cropVisual != null) cropVisual.SetActive(false);

        // notify tutorial
        if (TutorialManager.Instance != null)
            TutorialManager.Instance.OnFieldWatered();
    }

    /// Third click (after ready) – harvest.
    public void Harvest()
    {
        if (state != FieldState.ReadyToHarvest)
            return;

        state = FieldState.Empty;

        // hide all overlays
        if (seedVisual != null) seedVisual.SetActive(false);
        if (waterVisual != null) waterVisual.SetActive(false);
        if (cropVisual != null) cropVisual.SetActive(false);

    }
}
