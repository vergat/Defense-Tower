using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour {

    // Used with an array of boolean for know the next free move
    enum Direction
    {
        DownRight = 0,
        DownUp = 1,
        DownLeft = 2,
        UpRight = 3,
        UpDown = 4,
        UpLeft = 5,
        RightDown = 6,
        RightLeft = 7,
        RightUp = 8,
        LeftDown = 9,
        LeftRight = 10,
        LeftUp = 11
    };

    [SerializeField]
    private Controller control = null;

    // board and boardMatrix are used for initialize the game field
    private GameObject[] board;
    private GameObject[,] boardMatrix;
    // memorize the road for the enemies after the road construction
    private List<Vector3> road;
    // sprite for the head of the road
    [SerializeField]
    private Sprite spriteStart;
    // sprite for free field
    [SerializeField]
    private Sprite spriteBase;
    // row and colon are used for keep the position for the next move
    private int row;
    private int colon;
    // use like temporary sprite
    private Sprite tmpSprite;
    // sprites that indicate the road on the map
    [SerializeField]
    private Sprite FromDownToRight;
    [SerializeField]
    private Sprite FromLeftToUp;
    [SerializeField]
    private Sprite FromUpToRight;
    [SerializeField]
    private Sprite FromLeftToRight;
    [SerializeField]
    private Sprite FromDownToUp;
    [SerializeField]
    private Sprite FromLeftToDown;
    // array for indicate which move is possible
    private bool[] ArrayDirection;
    // the head of the road
    [SerializeField]
    private GameObject Castle;

    // the enemy
    [SerializeField]
    private GameObject Enemy;
    // index of the next position on the road to follow
    private int EnemyPosition;
    [SerializeField]
    // velocity on the road of the enemy
    private float EnemySpeed;
    // used for the path-finding
    private float lastWaypointSwitchTime;

    // number of the move possible
    [SerializeField]
    private int BlockNumber;

    private void Awake()
    {
        // Initialize the list and the array of bool
        road = new List<Vector3>();
        ArrayDirection = new bool[12];
        // Deactivate the GameObjects
        Castle.SetActive(false);
        Enemy.SetActive(false);
        // Call the functions for initialize the game
        InitializeBoardMatrix();
        SetStartEnd();
        // Set the ArrayDirection all true
        for (int i = 0; i < ArrayDirection.Length; i++)
        {
            ArrayDirection[i] = true;
        }
    }

    private void Update()
    {
        // Manage the movement of the enemy with the waypoints
        if (Enemy.activeInHierarchy)
        {
            float pathLenght = Vector3.Distance(road[EnemyPosition - 1], road[EnemyPosition]);
            float totalTimeForPath = pathLenght / EnemySpeed;
            float CurrentTimeOnPath = Time.time - lastWaypointSwitchTime;
            Enemy.transform.position = Vector3.Lerp(road[EnemyPosition - 1], road[EnemyPosition], CurrentTimeOnPath / totalTimeForPath);
            if (Enemy.transform.position.Equals(road[EnemyPosition]) && EnemyPosition < road.Count - 1)
            {
                EnemyPosition++;
                lastWaypointSwitchTime = Time.time;
            }
        }
    }

    private void OnEnable()
    {
        control.onRoadChange += OnRoadChangeCallback;
    }

    private void OnDisable()
    {
        control.onRoadChange -= OnRoadChangeCallback;
    }

    private void OnRoadChangeCallback(RoadData roadDataAsset)
    {
        // Do the move with the road selected
        if (roadDataAsset.Road.Equals(FromLeftToRight))
        {
            if (ArrayDirection[(int)Direction.LeftRight] && colon < 6 && NextPositionFree(row, colon+1))
            {
                road.Add(new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f));
                tmpSprite = FromLeftToRight;
                boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
                colon++;
                Castle.transform.position = new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f, -0.1f);
                ResetDirectionArray();
                ArrayDirection[(int)Direction.LeftDown] = true;
                ArrayDirection[(int)Direction.LeftRight] = true;
                ArrayDirection[(int)Direction.LeftUp] = true;
                BlockNumberCheck();
            } else if (ArrayDirection[(int)Direction.RightLeft] && colon > 0 && NextPositionFree(row, colon - 1))
            {
                road.Add(new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f));
                tmpSprite = FromLeftToRight;
                boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
                colon--;
                Castle.transform.position = new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f, -0.1f);
                ResetDirectionArray();
                ArrayDirection[(int)Direction.RightDown] = true;
                ArrayDirection[(int)Direction.RightLeft] = true;
                ArrayDirection[(int)Direction.RightUp] = true;
                BlockNumberCheck();
            }
        }

        if (roadDataAsset.Road.Equals(FromDownToRight))
        {
            if (ArrayDirection[(int)Direction.DownRight] && colon < 6 && NextPositionFree(row, colon + 1))
            {
                road.Add(new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f));
                tmpSprite = FromDownToRight;
                boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
                colon++;
                Castle.transform.position = new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f, -0.1f);
                ResetDirectionArray();
                ArrayDirection[(int)Direction.LeftDown] = true;
                ArrayDirection[(int)Direction.LeftRight] = true;
                ArrayDirection[(int)Direction.LeftUp] = true;
                BlockNumberCheck();
            } else if (ArrayDirection[(int)Direction.RightDown] && row > 0 && NextPositionFree(row - 1, colon))
            {
                road.Add(new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f));
                tmpSprite = FromDownToRight;
                boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
                row--;
                Castle.transform.position = new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f, -0.1f);
                ResetDirectionArray();
                ArrayDirection[(int)Direction.UpDown] = true;
                ArrayDirection[(int)Direction.UpLeft] = true;
                ArrayDirection[(int)Direction.UpRight] = true;
                BlockNumberCheck();
            }
        }

        if (roadDataAsset.Road.Equals(FromLeftToUp))
        {
            if (ArrayDirection[(int)Direction.LeftUp] && row < 6 && NextPositionFree(row + 1, colon))
            {
                road.Add(new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f));
                tmpSprite = FromLeftToUp;
                boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
                row++;
                Castle.transform.position = new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f, -0.1f);
                ResetDirectionArray();
                ArrayDirection[(int)Direction.DownLeft] = true;
                ArrayDirection[(int)Direction.DownUp] = true;
                ArrayDirection[(int)Direction.DownRight] = true;
                BlockNumberCheck();
            } else if (ArrayDirection[(int)Direction.UpLeft] && colon > 0 && NextPositionFree(row, colon - 1))
            {
                road.Add(new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f));
                tmpSprite = FromLeftToUp;
                boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
                colon--;
                Castle.transform.position = new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f, -0.1f);
                ResetDirectionArray();
                ArrayDirection[(int)Direction.RightDown] = true;
                ArrayDirection[(int)Direction.RightLeft] = true;
                ArrayDirection[(int)Direction.RightUp] = true;
                BlockNumberCheck();
            }
        }

        if (roadDataAsset.Road.Equals(FromUpToRight))
        {
            if (ArrayDirection[(int)Direction.UpRight] && colon < 6 && NextPositionFree(row, colon + 1))
            {
                road.Add(new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f));
                tmpSprite = FromUpToRight;
                boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
                colon++;
                Castle.transform.position = new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f, -0.1f);
                ResetDirectionArray();
                ArrayDirection[(int)Direction.LeftDown] = true;
                ArrayDirection[(int)Direction.LeftRight] = true;
                ArrayDirection[(int)Direction.LeftUp] = true;
                BlockNumberCheck();
            } else if (ArrayDirection[(int)Direction.RightUp] && row < 6 && NextPositionFree(row + 1, colon))
            {
                road.Add(new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f));
                tmpSprite = FromUpToRight;
                boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
                row++;
                Castle.transform.position = new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f, -0.1f);
                ResetDirectionArray();
                ArrayDirection[(int)Direction.DownLeft] = true;
                ArrayDirection[(int)Direction.DownUp] = true;
                ArrayDirection[(int)Direction.DownRight] = true;
                BlockNumberCheck();
            }
        }

        if (roadDataAsset.Road.Equals(FromDownToUp))
        {
            if (ArrayDirection[(int)Direction.DownUp] && row < 6 && NextPositionFree(row + 1, colon))
            {
                road.Add(new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f));
                tmpSprite = FromDownToUp;
                boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
                row++;
                Castle.transform.position = new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f, -0.1f);
                ResetDirectionArray();
                ArrayDirection[(int)Direction.DownLeft] = true;
                ArrayDirection[(int)Direction.DownUp] = true;
                ArrayDirection[(int)Direction.DownRight] = true;
                BlockNumberCheck();
            } else if (ArrayDirection[(int)Direction.UpDown] && row > 0 && NextPositionFree(row - 1, colon))
            {
                road.Add(new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f));
                tmpSprite = FromDownToUp;
                boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
                row--;
                Castle.transform.position = new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f, -0.1f);
                ResetDirectionArray();
                ArrayDirection[(int)Direction.UpDown] = true;
                ArrayDirection[(int)Direction.UpLeft] = true;
                ArrayDirection[(int)Direction.UpRight] = true;
                BlockNumberCheck();
            }
        }

        if (roadDataAsset.Road.Equals(FromLeftToDown))
        {
            if (ArrayDirection[(int)Direction.LeftDown] && row > 0 && NextPositionFree(row - 1, colon))
            {
                road.Add(new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f));
                tmpSprite = FromLeftToDown;
                boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
                row--;
                Castle.transform.position = new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f, -0.1f);
                ResetDirectionArray();
                ArrayDirection[(int)Direction.UpDown] = true;
                ArrayDirection[(int)Direction.UpLeft] = true;
                ArrayDirection[(int)Direction.UpRight] = true;
                BlockNumberCheck();
            } else if (ArrayDirection[(int)Direction.DownLeft] && colon > 0 && NextPositionFree(row, colon - 1))
            {
                road.Add(new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f));
                tmpSprite = FromLeftToDown;
                boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
                colon--;
                Castle.transform.position = new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f, -0.1f);
                ResetDirectionArray();
                ArrayDirection[(int)Direction.RightDown] = true;
                ArrayDirection[(int)Direction.RightLeft] = true;
                ArrayDirection[(int)Direction.RightUp] = true;
                BlockNumberCheck();
            }
        }

    }

    // Function for initialize the board to follow better the move in the map
    private void InitializeBoardMatrix()
    {
        GameObject root = GameObject.Find("rootMap");
        int boardNumber = root.transform.childCount;
        board = new GameObject[boardNumber];
        int matrixSize = (int)Mathf.Sqrt(boardNumber);
        boardMatrix = new GameObject[matrixSize, matrixSize];
        for (int i = 0; i < boardNumber; i++)
        {
            board[i] = root.transform.GetChild(i).gameObject;
        }

        boardMatrix[0, 0] = board[0]; boardMatrix[0, 1] = board[2]; boardMatrix[0, 2] = board[5]; boardMatrix[0, 3] = board[9];
        boardMatrix[0, 4] = board[14]; boardMatrix[0, 5] = board[20]; boardMatrix[0, 6] = board[27]; boardMatrix[1, 0] = board[1];
        boardMatrix[1, 1] = board[4]; boardMatrix[1, 2] = board[8]; boardMatrix[1, 3] = board[13]; boardMatrix[1, 4] = board[19];
        boardMatrix[1, 5] = board[26]; boardMatrix[1, 6] = board[33]; boardMatrix[2, 0] = board[3]; boardMatrix[2, 1] = board[7];
        boardMatrix[2, 2] = board[12]; boardMatrix[2, 3] = board[18]; boardMatrix[2, 4] = board[25]; boardMatrix[2, 5] = board[32];
        boardMatrix[2, 6] = board[38]; boardMatrix[3, 0] = board[6]; boardMatrix[3, 1] = board[11]; boardMatrix[3, 2] = board[17];
        boardMatrix[3, 3] = board[24]; boardMatrix[3, 4] = board[31]; boardMatrix[3, 5] = board[37]; boardMatrix[3, 6] = board[42];
        boardMatrix[4, 0] = board[10]; boardMatrix[4, 1] = board[16]; boardMatrix[4, 2] = board[23]; boardMatrix[4, 3] = board[30];
        boardMatrix[4, 4] = board[36]; boardMatrix[4, 5] = board[41]; boardMatrix[4, 6] = board[45]; boardMatrix[5, 0] = board[15];
        boardMatrix[5, 1] = board[22]; boardMatrix[5, 2] = board[29]; boardMatrix[5, 3] = board[35]; boardMatrix[5, 4] = board[40];
        boardMatrix[5, 5] = board[44]; boardMatrix[5, 6] = board[47]; boardMatrix[6, 0] = board[21]; boardMatrix[6, 1] = board[28];
        boardMatrix[6, 2] = board[34]; boardMatrix[6, 3] = board[39]; boardMatrix[6, 4] = board[43]; boardMatrix[6, 5] = board[46];
        boardMatrix[6, 6] = board[48];

    }

    // Set the start point at the beginning of the game
    private void SetStartEnd()
    {

        int startPointx = Random.Range(0, 7);
        int startPointy;
        if (startPointx == 0 && startPointx == 6)
        {
            startPointy = Random.Range(0, 7);
        }
        else
        {
            startPointy = Random.Range(0, 2);
            if (startPointy == 1)
            {
                startPointy = 6;
            }
        }

        row = startPointx;
        colon = startPointy;

        Castle.SetActive(true);
        Castle.transform.position = new Vector3(boardMatrix[row, colon].transform.position.x, boardMatrix[row, colon].transform.position.y + 0.4f, -0.1f);

    }

    // Set all ArrayDirection to false
    private void ResetDirectionArray()
    {
        for (int i = 0; i < ArrayDirection.Length; i++)
        {
            ArrayDirection[i] = false;
        }
    }

    // Check the number of moves and call the function for initialize the enemy
    private void BlockNumberCheck()
    {
        BlockNumber--;
        if (BlockNumber == 0)
        {
            ResetDirectionArray();
            road.Add(new Vector3(Castle.transform.position.x, Castle.transform.position.y));
            InitializeEnemy();
        }
    }

    // Check if the next position is free for the selected move
    private bool NextPositionFree(int pointy, int pointx)
    {
        if (boardMatrix[pointy, pointx].GetComponent<SpriteRenderer>().sprite == spriteBase)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Initialize the enemy on the map
    private void InitializeEnemy()
    {
        Enemy.SetActive(true);
        EnemyPosition = 0;
        Enemy.transform.position = road[EnemyPosition];
        EnemyPosition++;
        lastWaypointSwitchTime = Time.time;
    }
}
