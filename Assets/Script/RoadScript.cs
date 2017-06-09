using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour {
    
    private GameObject[] board;
    private GameObject[,] boardMatrix;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private Sprite spriteStart;
    [SerializeField]
    private Sprite spriteEnd;
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


    void Awake()
    {
        InitializeBoardMatrix();
        SetStartEnd();
    }

    // Use this for initialization
    void Start () {
        row = 0;
        colon = 0;
	}
	
	// Update is called once per frame
	void Update () {		

        if (Input.GetKeyDown(KeyCode.S))
        {
            tmpSprite = FromDownToRight;            
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
            colon++;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = spriteStart;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            tmpSprite = FromLeftToUp;            
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
            row++;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = spriteStart;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            tmpSprite = FromUpToRight;            
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
            colon++;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = spriteStart;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            tmpSprite = FromLeftToRight;            
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
            colon++;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = spriteStart;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            tmpSprite = FromDownToUp;            
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
            row++;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = spriteStart;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            tmpSprite = FromLeftToDown;            
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
            row--;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = spriteStart;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            tmpSprite = FromDownToRight;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
            row--;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = spriteStart;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            tmpSprite = FromLeftToUp;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
            colon--;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = spriteStart;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            tmpSprite = FromUpToRight;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
            row++;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = spriteStart;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            tmpSprite = FromLeftToRight;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
            colon--;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = spriteStart;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            tmpSprite = FromDownToUp;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
            row--;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = spriteStart;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            tmpSprite = FromLeftToDown;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = tmpSprite;
            colon--;
            boardMatrix[row, colon].GetComponent<SpriteRenderer>().sprite = spriteStart;
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

        int startPointx = Random.Range(0, 6);
        int startPointy = Random.Range(0, 6);
        int endPointx = Random.Range(0, 6);
        int endPointy = Random.Range(0, 6);

        boardMatrix[startPointx, startPointy].GetComponent<SpriteRenderer>().sprite = spriteStart;
        boardMatrix[endPointx, endPointy].GetComponent<SpriteRenderer>().sprite = spriteEnd;

        row = startPointx;
        colon = startPointy;

        NextPosition(startPointx, startPointy);

    }

    private void NextPosition(int pointX, int pointY)
    {



    }

}
