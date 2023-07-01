using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject model;
    public Vector3 offset;
    public int gridHeight;
    public int gridWidth;
    public float modelScale;
    public float gap = 0.1f;
    public float columnPhase = 10.0f;
    public float rowPhase = 5.0f;
    public float amplitude = 0.005f;
    public float speed = 4.0f;

    private GameObject[,] grid;
    private bool isEnabled;
    private void Start()
    {
        isEnabled = false;
        grid = new GameObject[gridWidth, gridHeight];
        for (int row = 0; row < gridHeight; row++)
        {
            for (int col = 0; col < gridWidth; col++)
            {
                Vector3 loc = transform.position;
                loc += offset + new Vector3(col * (modelScale + gap), 0.0f, row * (modelScale + gap));
                grid[col, row] = Instantiate(model, loc, Quaternion.identity);
                grid[col, row].transform.localScale = Vector3.one * modelScale;
                grid[col, row].transform.parent = transform;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isEnabled) return;
        for (int row = 0; row < gridHeight; row++)
        {
            for (int col = 0; col < gridWidth; col++)
            {
                grid[col, row].transform.Translate(
                    Vector3.up
                    * Random.Range(0.9f, 1.0f)
                    * amplitude
                    * Mathf.Sin(speed * Time.time + (rowPhase * Mathf.Max(row, col)) + columnPhase * (row + col))//row + columnPhase * col))
                    , Space.World
                );
            }
        }
    }

    public void Enable()
    {
        isEnabled = true;
    }

    public void Disable()
    {
        isEnabled = false;
        for (int row = 0; row < gridHeight; row++)
        {
            for (int col = 0; col < gridWidth; col++)
            {
                Vector3 loc = grid[col, row].transform.position;
                loc.y = transform.position.y + offset.y;
                grid[col, row].transform.position = loc;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 topLeft = transform.position 
            + new Vector3(offset.x, offset.y, -offset.z)
            - new Vector3( (modelScale + gap), 0.0f, -(modelScale + gap));

        Gizmos.DrawLine(topLeft, topLeft + Vector3.back * gridHeight * modelScale);
        Gizmos.DrawLine(topLeft, topLeft + Vector3.right * gridWidth * modelScale);
        Gizmos.DrawLine(
            topLeft + Vector3.right * gridWidth * modelScale,
            topLeft + new Vector3(gridWidth * modelScale, 0, -gridHeight * modelScale)
        );
        Gizmos.DrawLine(
                    topLeft + Vector3.back * gridHeight * modelScale,
                    topLeft + new Vector3(gridWidth * modelScale, 0, -gridHeight * modelScale)
                );
    }
}
