//using UnityEngine;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTilePuzzleNoPrefab : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public int gridWidth = 3;
    public int gridHeight = 3;
    public float spacing = 1.1f;

    [Header("Reward")]
    public GameObject rewardObject; // Optional

    private List<Tile> tiles = new List<Tile>();

    void Start()
    {
        if (rewardObject) rewardObject.SetActive(false);
        GenerateTiles();
    }

    void GenerateTiles()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Vector3 pos = new Vector3(x * spacing, 0, y * spacing);
                GameObject tileObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tileObj.transform.position = pos;
                tileObj.transform.parent = transform;

                // Visual cue
                tileObj.transform.localScale = new Vector3(1, 0.2f, 1);

                // Add Tile component and logic
                Tile tile = tileObj.AddComponent<Tile>();
                tile.manager = this;
                tile.GenerateRandomConnections();
                tile.RandomizeRotation();

                tiles.Add(tile);
            }
        }
    }

    public void CheckPuzzle()
    {
        foreach (var tile in tiles)
        {
            if (!tile.IsCorrect()) return;
        }

        Debug.Log("✅ Puzzle Solved!");
        if (rewardObject) rewardObject.SetActive(true);
    }

    // Tile class (attached automatically)
    public class Tile : MonoBehaviour
    {
        public bool[] currentConnections = new bool[4]; // Up, Right, Down, Left
        public bool[] correctConnections = new bool[4];
        public int currentRotation = 0;

        public ProceduralTilePuzzleNoPrefab manager;

        public void GenerateRandomConnections()
        {
            for (int i = 0; i < 4; i++)
            {
                correctConnections[i] = Random.value > 0.5f;
                currentConnections[i] = correctConnections[i];
            }
        }

        public void RandomizeRotation()
        {
            int times = Random.Range(0, 4);
            for (int i = 0; i < times; i++) RotateTile();
        }

        void OnMouseDown()
        {
            RotateTile();
            manager.CheckPuzzle();
        }

        public void RotateTile()
        {
            transform.Rotate(0, 90, 0);
            currentRotation = (currentRotation + 1) % 4;

            // Rotate connections clockwise
            bool temp = currentConnections[3];
            currentConnections[3] = currentConnections[2];
            currentConnections[2] = currentConnections[1];
            currentConnections[1] = currentConnections[0];
            currentConnections[0] = temp;
        }

        public bool IsCorrect()
        {
            for (int i = 0; i < 4; i++)
            {
                if (currentConnections[i] != correctConnections[i])
                    return false;
            }
            return true;
        }
    }
}