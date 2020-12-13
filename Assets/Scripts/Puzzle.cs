using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Puzzle : MonoBehaviour
{

    public Texture2D image;
    public int blocksPerLine = 4;
    public int shuffleLength = 20;
    public float defaultMoveDuration = .2f;
    public float shuffleMoveDuration = .1f;
    public int clickcount = 0;
    public Text textTimer;
    public string textTimercos;
    public float startTime;
    private bool finnished = false;
    public float totalPoints;
    private int poztrud;
    public float tdalej;

    enum PuzzleState { Solved, Shuffling, InPlay };
    PuzzleState state;

    Block emptyBlock;
    Block[,] blocks;
    Queue<Block> inputs;
    bool blockIsMoving;
    int shuffleMovesRemaining;
    Vector2Int prevShuffleOffset;


    public void Awake()
    {
        image = (Texture2D) Resources.Load(PlayerPrefs.GetString("image"));
        blocksPerLine = PlayerPrefs.GetInt("blocksPerLine");
        shuffleLength = PlayerPrefs.GetInt("shuffleLength");
        PlayerPrefs.SetInt("clickcount", 0);
        PlayerPrefs.SetString("textTimer", " ");

    }

    public void Start()
    {
        CreatePuzzle();

    }

    void Update()
    {
        if (state == PuzzleState.Solved && Input.touchCount>0)
        {
            StartShuffle();
        }
        UpdateTimer();
    }

    void CreatePuzzle()
    {
        blocks = new Block[blocksPerLine, blocksPerLine];
        Texture2D[,] imageSlices = ImageSlicer.GetSlices(image, blocksPerLine);
        for (int y = 0; y < blocksPerLine; y++)
        {
            for (int x = 0; x < blocksPerLine; x++)
            {
                GameObject blockObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                blockObject.transform.position = -Vector2.one * (blocksPerLine - 1) * .5f + new Vector2(x, y);
                blockObject.transform.parent = transform;

                Block block = blockObject.AddComponent<Block>();
                block.OnBlockPressed += PlayerMoveBlockInput;
                block.OnFinishedMoving += OnBlockFinishedMoving;
                block.Init(new Vector2Int(x, y), imageSlices[x, y]);
                blocks[x, y] = block;
                
                if (y == 0 && x == blocksPerLine - 1)
                {
                    emptyBlock = block;
                }
            }
        }

        Camera.main.orthographicSize = blocksPerLine * .55f;
        inputs = new Queue<Block>();
    }

    void PlayerMoveBlockInput(Block blockToMove)
    {      
        if (state == PuzzleState.InPlay)
        {
            inputs.Enqueue(blockToMove);
            MakeNextPlayerMove();
           
            clickcount++;
        }
        if (clickcount == 1)
        {
            StartTimer();
            textTimer.transform.gameObject.SetActive(true);
        }
    }

    void MakeNextPlayerMove()
    {
        while (inputs.Count > 0 && !blockIsMoving)
        {
            MoveBlock(inputs.Dequeue(), defaultMoveDuration);
        }
    }

    void MoveBlock(Block blockToMove, float duration)
    {
        if ((blockToMove.coord - emptyBlock.coord).sqrMagnitude == 1)
        {
            blocks[blockToMove.coord.x, blockToMove.coord.y] = emptyBlock;
            blocks[emptyBlock.coord.x, emptyBlock.coord.y] = blockToMove;

            Vector2Int targetCoord = emptyBlock.coord;
            emptyBlock.coord = blockToMove.coord;
            blockToMove.coord = targetCoord;

            Vector2 targetPosition = emptyBlock.transform.position;
            emptyBlock.transform.position = blockToMove.transform.position;
            blockToMove.MoveToPosition(targetPosition, duration);
            blockIsMoving = true;
        }
    }

    void OnBlockFinishedMoving()
    {
        blockIsMoving = false;
        CheckIfSolved();

        if (state == PuzzleState.InPlay)
        {
            MakeNextPlayerMove();
        }
        else if (state == PuzzleState.Shuffling)
        {
            if (shuffleMovesRemaining > 0)
            {
                MakeNextShuffleMove();
            }
            else
            {
                state = PuzzleState.InPlay;
            }
        }
    }

    void StartShuffle()
    {
        state = PuzzleState.Shuffling;
        shuffleMovesRemaining = shuffleLength;
        emptyBlock.gameObject.SetActive(false);
        MakeNextShuffleMove();
    }

    void MakeNextShuffleMove()
    {
        Vector2Int[] offsets = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };
        int randomIndex = Random.Range(0, offsets.Length);

        for (int i = 0; i < offsets.Length; i++)
        {
            Vector2Int offset = offsets[(randomIndex + i) % offsets.Length];
            if (offset != prevShuffleOffset * -1)
            {
                Vector2Int moveBlockCoord = emptyBlock.coord + offset;

                if (moveBlockCoord.x >= 0 && moveBlockCoord.x < blocksPerLine && moveBlockCoord.y >= 0 && moveBlockCoord.y < blocksPerLine)
                {
                    MoveBlock(blocks[moveBlockCoord.x, moveBlockCoord.y], shuffleMoveDuration);
                    shuffleMovesRemaining--;
                    prevShuffleOffset = offset;
                    break;
                }
            }
        }

    }

    void CheckIfSolved()
    {
        foreach (Block block in blocks)
        {
            if (!block.IsAtStartingCoord())
            {
                return;
            }
        }
        Zliczpunkt(PlayerPrefs.GetString("poziom", "Easy"),tdalej, clickcount);
        PlayerPrefs.SetFloat("totalPoints", totalPoints);
        PlayerPrefs.SetFloat(PlayerPrefs.GetString("image"), totalPoints);
        PlayerPrefs.SetString("textTimer",textTimercos);
        PlayerPrefs.SetInt("clickcount", clickcount);
        state = PuzzleState.Solved;
        emptyBlock.gameObject.SetActive(true);
        SceneManager.LoadScene(3);
    }
    //Timer
    void StartTimer()
    {
        startTime = Time.time;
    }
    public void UpdateTimer()
    {
        if (finnished)
            return;

        float t = Time.time - startTime;
        tdalej = ((int)t);
        string minuts = ((int)t / 60).ToString();
        string secends = (t % 60).ToString("f1");
        textTimer.text = minuts + ":" + secends;
        textTimercos = textTimer.text;

    }
    void FinnishTimer()
    {
        finnished = true;
    }
    public void Zliczpunkt(string poziom ,float czas,int klikniecia)
    {

        if (poziom == "Easy")
            poztrud = 1;
        else if (poziom == "Hard")
            poztrud = 10;
        else
            poztrud = 5;

        totalPoints = (czas/klikniecia) * poztrud;

    }
}