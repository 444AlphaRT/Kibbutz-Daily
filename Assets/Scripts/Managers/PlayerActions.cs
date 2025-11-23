using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit))
            return;

        // Did we hit a Field?
        Field field = hit.transform.GetComponent<Field>();
        if (field != null)
        {
            Debug.Log("Clicked on field, state = " + field.state);

            switch (field.state)
            {
                case FieldState.Empty:
                    field.Plant();
                    break;

                case FieldState.Seeded:
                    field.Water();
                    break;

                case FieldState.ReadyToHarvest:
                    field.Harvest();
                    break;
            }

            return;
        }
    }

}
