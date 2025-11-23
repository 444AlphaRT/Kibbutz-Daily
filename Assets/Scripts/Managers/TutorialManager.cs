using UnityEngine;
using UnityEngine.UI;

public enum TutorialStep
{
    Plant3Fields,
    Water3Fields,
    Done
}

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    public TutorialStep currentStep = TutorialStep.Plant3Fields;

    // UI text that shows the current tutorial instruction
    public Text tutorialText;

    private int plantedCount = 0;
    private int wateredCount = 0;

    private void Awake()
    {
        // Simple singleton so other scripts can call TutorialManager.Instance
        Instance = this;
    }

    private void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        switch (currentStep)
        {
            case TutorialStep.Plant3Fields:
                tutorialText.text = "Plant 3 fields";
                break;
            case TutorialStep.Water3Fields:
                tutorialText.text = "Water the 3 fields";
                break;
            case TutorialStep.Done:
                tutorialText.text = "Tutorial completed!";
                break;
        }
    }

    // Called when the player plants a field
    public void OnFieldPlanted()
    {
        if (currentStep != TutorialStep.Plant3Fields) return;

        plantedCount++;
        if (plantedCount >= 3)
        {
            currentStep = TutorialStep.Water3Fields;
            UpdateText();
        }
    }

    // Called when the player waters a field
    public void OnFieldWatered()
    {
        if (currentStep != TutorialStep.Water3Fields) return;

        wateredCount++;
        if (wateredCount >= 3)
        {
            currentStep = TutorialStep.Done;
            UpdateText();
        }
    }
}
