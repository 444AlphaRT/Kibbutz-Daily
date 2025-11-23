using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public int columnLength, rowLength;
    public float x_Space, z_Space;
    public GameObject grass;
    public GameObject currentGrind;
    public bool gotGrid;

    public GameObject hitted;
    public GameObject field;

    public bool creatingFields;

    public int maxFields = 4; 
    private RaycastHit _Hit;
    private List<Vector2Int> selectedCells = new List<Vector2Int>(); 

    public GoldSystem goldSystem;
    public int fieldsPrice = 5;   

    void Start()
    {
        for (int i = 0; i < columnLength * rowLength; i++)
        {
            int col = i % columnLength;
            int row = i / columnLength;

            Vector3 pos = new Vector3(
                x_Space + (x_Space * col),
                0f,
                z_Space + (z_Space * row)
            );

            Instantiate(grass, pos, Quaternion.identity);
        }
    }

    void Update()
    {
        if (!gotGrid)
        {
            currentGrind = GameObject.FindGameObjectWithTag("grid");
            if (currentGrind != null)
            {
                gotGrid = true;
            }
        }

        if (!creatingFields)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _Hit))
            {
                if (_Hit.transform.CompareTag("grid"))
                {
                    hitted = _Hit.transform.gameObject;

                    Vector2Int cell = WorldToGrid(hitted.transform.position);

                    if (selectedCells.Contains(cell))
                    {
                        Debug.Log("This cell is already selected");
                        return;
                    }

                    if (selectedCells.Count >= maxFields)
                    {
                        Debug.Log("More than " + maxFields + " fields – charging " + fieldsPrice + "$");

                        if (goldSystem != null)
                        {
                            goldSystem.SpendGold(fieldsPrice);
                        }
                        else
                        {
                            Debug.LogWarning("goldSystem reference is NULL on GsmeGrid!");
                        }
                    }

                    selectedCells.Add(cell);

                    if (selectedCells.Count == maxFields && !IsSquare2x2(selectedCells))
                    {
                        selectedCells.Remove(cell);
                        Debug.Log("All 4 cells must form a contiguous 2x2 square");
                        return;
                    }

                    Instantiate(field, hitted.transform.position, Quaternion.identity);
                    Destroy(hitted);
                }
            }
        }
    }

    private Vector2Int WorldToGrid(Vector3 pos)
    {
        int col = Mathf.RoundToInt((pos.x - x_Space) / x_Space);
        int row = Mathf.RoundToInt((pos.z - z_Space) / z_Space);
        return new Vector2Int(col, row);
    }

    private bool IsSquare2x2(List<Vector2Int> cells)
    {
        if (cells.Count != 4) return false;

        int minX = cells[0].x;
        int maxX = cells[0].x;
        int minY = cells[0].y;
        int maxY = cells[0].y;

        for (int i = 1; i < cells.Count; i++)
        {
            Vector2Int c = cells[i];
            if (c.x < minX) minX = c.x;
            if (c.x > maxX) maxX = c.x;
            if (c.y < minY) minY = c.y;
            if (c.y > maxY) maxY = c.y;
        }

        if (maxX - minX != 1) return false;
        if (maxY - minY != 1) return false;

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                if (!cells.Contains(new Vector2Int(x, y)))
                    return false;
            }
        }

        return true;
    }

    public void CreateFields()
    {
        creatingFields = true;
        selectedCells.Clear();
    }

    public void returnToNormality()
    {
        creatingFields = false;
    }
}
