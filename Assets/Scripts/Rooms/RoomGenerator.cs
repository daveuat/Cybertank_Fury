using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [System.Serializable]
    public class Rule
    {
        public List<GameObject> roomPrefabs;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;
        public bool obligatory;
        public GameObject[] enemyPrefabs;
        public GameObject[] powerupPrefabs;

        public int ProbabilityOfSpawning(int x, int y)
        {
            return obligatory ? 2 : 0;
        }
    }

    public int totalEnemyCount = 0;

    public Vector2 size;
    public int startPos = 0;
    public Rule[] rules;
    public Vector2 offset;
    public GameObject playerPrefab;
    public bool spawnEnemies = true;
    public Vector2Int enemySpawnRange = new Vector2Int(1, 3);
    public float enemyRespawnTime = 5f;
    public GameObject[] spawnPrefabs;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    List<Cell> board;

    public GameObject tankPrefab;
    public Vector3 tankSpawnPosition;

    private Transform spawnedTank;
    private bool playerTankSpawned = false;

    [SerializeField] private bool useLevelOfTheDay = false;
    [SerializeField] private int seed = 0;

    void Start()
    {
        if (useLevelOfTheDay)
        {
            DateTime currentDate = DateTime.Now;
            seed = currentDate.Year + currentDate.Month + currentDate.Day;
        }

        Random.InitState(seed);

        GenerateMazeAndRooms();
    }

    void Update()
    {

    }

    void GenerateMazeAndRooms()
    {
        // Initialize the board
        board = new List<Cell>();
        for (int i = 0; i < size.x * size.y; i++)
        {
            board.Add(new Cell());
        }

        // Initialize the stack for the maze generation
        int currentCell = startPos;
        Stack<int> path = new Stack<int>();

        // Maze generation loop
        int k = 0;
        while (k < 1000)
        {
            k++;

            // Mark the current cell as visited
            board[currentCell].visited = true;

            // Get the unvisited neighbors of the current cell
            List<int> neighbors = CheckNeighbors(currentCell);

            // If there are no unvisited neighbors, backtrack to the last cell
            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            // If there are unvisited neighbors, choose one randomly, remove the wall to it and add it to the stack
            else
            {
                path.Push(currentCell);
                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                // Remove the wall between the current cell and the new cell
                if (newCell > currentCell)
                {
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[newCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[newCell].status[0] = true;
                    }
                }
                else
                {
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[newCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[newCell].status[1] = true;
                    }
                }
            }
        }

        // Generate rooms based on the maze
        GenerateRooms();
    }

    void GenerateRooms()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[i + j * (int)size.x];
                if (currentCell.visited)
                {
                    int randomRoom = -1;
                    List<int> availableRooms = new List<int>();

                    for (int k = 0; k < rules.Length; k++)
                    {
                        int p = rules[k].ProbabilityOfSpawning(i, j);
                        if (p == 2)
                        {
                            randomRoom = k;
                            break;
                        }
                        else if (p == 1)
                        {
                            availableRooms.Add(k);
                        }
                    }

                    if (randomRoom == -1)
                    {
                        if (availableRooms.Count > 0)
                        {
                            randomRoom = availableRooms[Random.Range(0, availableRooms.Count)];
                        }
                        else
                        {
                            randomRoom = 0;
                        }
                    }

                    int randomPrefabIndex = Random.Range(0, rules[randomRoom].roomPrefabs.Count);

                    var newRoom = Instantiate(rules[randomRoom].roomPrefabs[randomPrefabIndex], new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(currentCell.status);

                    string roomName = "Room " + i + "-" + j;
                    newRoom.name = roomName;

                    if (i == size.x / 2 && j == size.y / 2)
                    {
                        if (!playerTankSpawned)
                        {
                            GameObject playerTank = Instantiate(playerPrefab, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity);
                            AssignCameraToTank(playerTank.transform);
                            playerTankSpawned = true;
                        }
                    }

                    if (spawnEnemies)
                    {
                        int enemiesPerRoom = Random.Range(enemySpawnRange.x, enemySpawnRange.y + 1);

                        for (int enemyIndex = 0; enemyIndex < enemiesPerRoom; enemyIndex++)
                        {
                            GameObject enemyPrefab = rules[randomRoom].enemyPrefabs[Random.Range(0, rules[randomRoom].enemyPrefabs.Length)];
                            Vector3 spawnPosition = GetRandomSpawnPosition(newRoom.transform);
                            SpawnEnemy(enemyPrefab, spawnPosition);
                        }
                    }

                    int powerupsPerRoom = Random.Range(0, 2);

                    for (int powerupIndex = 0; powerupIndex < powerupsPerRoom; powerupIndex++)
                    {
                        GameObject powerupPrefab = rules[randomRoom].powerupPrefabs[Random.Range(0, rules[randomRoom].powerupPrefabs.Length)];
                        Vector3 spawnPosition = GetRandomSpawnPosition(newRoom.transform);
                        SpawnPowerup(powerupPrefab, spawnPosition);
                    }
                }
            }
        }
    }

    void AssignCameraToTank(Transform tankTransform)
    {
        spawnedTank = tankTransform;

        ThirdPersonCamera thirdPersonCamera = FindObjectOfType<ThirdPersonCamera>();

        if (thirdPersonCamera != null)
        {
            thirdPersonCamera.target = spawnedTank;
            thirdPersonCamera.distance = thirdPersonCamera.distance;
            thirdPersonCamera.height = thirdPersonCamera.height;
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab, Vector3 spawnPosition)
    {
        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnedEnemies.Add(spawnedEnemy);
        spawnedEnemy.SetActive(false);
        StartCoroutine(RespawnEnemy(spawnedEnemy));
    }

    private void SpawnPowerup(GameObject powerupPrefab, Vector3 spawnPosition)
    {
        Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
    }

    private IEnumerator RespawnEnemy(GameObject enemy)
    {
        yield return new WaitForSeconds(enemyRespawnTime);
        enemy.SetActive(true);
    }

    private Vector3 GetRandomSpawnPosition(Transform roomTransform)
    {
        float gridSize = 1f;

        int cellsX = Mathf.FloorToInt(roomTransform.localScale.x / gridSize);
        int cellsZ = Mathf.FloorToInt(roomTransform.localScale.z / gridSize);

        List<Vector3> possibleSpawnPositions = new List<Vector3>();

        for (int x = 0; x < cellsX; x++)
        {
            for (int z = 0; z < cellsZ; z++)
            {
                Vector3 cellPosition = roomTransform.position + new Vector3((x + 0.5f) * gridSize - roomTransform.localScale.x / 2f, 0f, (z + 0.5f) * gridSize - roomTransform.localScale.z / 2f);

                if (IsValidSpawnPosition(cellPosition))
                {
                    possibleSpawnPositions.Add(cellPosition);
                }
            }
        }

        if (possibleSpawnPositions.Count == 0)
        {
            return roomTransform.position;
        }

        return possibleSpawnPositions[Random.Range(0, possibleSpawnPositions.Count)];
    }

    private bool IsValidSpawnPosition(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 1f); // Adjust the radius based on your game's needs

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Wall") || collider.CompareTag("Obstacle"))
            {
                return false;
            }
        }

        return true;
    }

    List<int> CheckNeighbors(int index)
    {
        List<int> neighbors = new List<int>();

        int row = index / (int)size.y;
        int column = index % (int)size.y;

        // North
        if (row < size.x - 1 && !board[(row + 1) * (int)size.y + column].visited)
        {
            neighbors.Add((row + 1) * (int)size.y + column);
        }

        // South
        if (row > 0 && !board[(row - 1) * (int)size.y + column].visited)
        {
            neighbors.Add((row - 1) * (int)size.y + column);
        }

        // East
        if (column < size.y - 1 && !board[row * (int)size.y + column + 1].visited)
        {
            neighbors.Add(row * (int)size.y + column + 1);
        }

        // West
        if (column > 0 && !board[row * (int)size.y + column - 1].visited)
        {
            neighbors.Add(row * (int)size.y + column - 1);
        }

        return neighbors;
    }
}
