using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour {

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

    private GameObject[] board;
    private GameObject[,] boardMatrix;
    private List<Vector3> road;
    [SerializeField]
    private Sprite spriteStart;
    [SerializeField]
    private Sprite spriteBase;
    private int row;
    private int colon;
    private Sprite tmpSprite;
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
    private bool[] ArrayDirection;
    [SerializeField]
    private GameObject Castle;

    [SerializeField]
    private GameObject Enemy;
    private int EnemyPosition;
    [SerializeField]
    private float EnemySpeed;
    private float lastWaypointSwitchTime;

    [SerializeField]
    private int BlockNumber;

    void Awake()
    {
        road = new List<Vector3>();
        ArrayDirection = new bool[12];
        Castle.SetActive(false);
        Enemy.SetActive(false);
        InitializeBoardMatrix();
        SetStartEnd();
        for (int i = 0; i < ArrayDirection.Length; i++)
        {
            ArrayDirection[i] = true;
        }        
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {		

        if (Input.GetKeyDown(KeyCode.S))
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
            }            
        }
        if (Input.GetKeyDown(KeyCode.D))
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
            }            
        }
        if (Input.GetKeyDown(KeyCode.F))
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
            }            
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (ArrayDirection[(int)Direction.LeftRight] && colon < 6 && NextPositionFree(row, colon + 1))
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
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if(ArrayDirection[(int)Direction.DownUp] && row < 6 && NextPositionFree(row + 1, colon))
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
            }            
        }
        if (Input.GetKeyDown(KeyCode.J))
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
            }            
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (ArrayDirection[(int)Direction.RightDown] && row > 0 && NextPositionFree(row - 1, colon))
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (ArrayDirection[(int)Direction.UpLeft] && colon > 0 && NextPositionFree(row, colon - 1))
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
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (ArrayDirection[(int)Direction.RightUp] && row < 6 && NextPositionFree(row + 1, colon))
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
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (ArrayDirection[(int)Direction.RightLeft] && colon > 0 && NextPositionFree(row, colon - 1))
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
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (ArrayDirection[(int)Direction.UpDown] && row > 0 && NextPositionFree(row - 1, colon))
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
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (ArrayDirection[(int)Direction.DownLeft] && colon > 0 && NextPositionFree(row, colon - 1))
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

        if (Enemy.activeInHierarchy)
        {
            float pathLenght = Vector3.Distance(road[EnemyPosition - 1], road[EnemyPosition]);
            float totalTimeForPath = pathLenght / EnemySpeed;
            float CurrentTimeOnPath = Time.time - lastWaypointSwitchTime;
            Enemy.transform.position = Vector3.Lerp(road[EnemyPosition - 1], road[EnemyPosition], CurrentTimeOnPath / totalTimeForPath);
            if (Enemy.transform.position.Equals(road[EnemyPosition]) && EnemyPosition < road.Count-1)
            {
                EnemyPosition++;
                lastWaypointSwitchTime = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < road.Count; i++)
            {
                Debug.Log(road[i].x.ToString() + road[i].y.ToString());
            }
        }

    }

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

    private void ResetDirectionArray()
    {
        for (int i = 0; i < ArrayDirection.Length; i++)
        {
            ArrayDirection[i] = false;
        }
    }

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

    private bool NextPositionFree(int pointy, int pointx)
    {
        if (boardMatrix[pointy, pointx].GetComponent<SpriteRenderer>().sprite == spriteBase)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void InitializeEnemy()
    {
        Enemy.SetActive(true);
        EnemyPosition = 0;
        Enemy.transform.position = road[EnemyPosition];
        EnemyPosition++;
        lastWaypointSwitchTime = Time.time;
    }

}
