using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //color val: white, green, blue, yellow, red, orange, purple, brown, dark-green, dark-blue, nothing
    public enum STATUS {Idle, Holding, WillSpawn, SubSpawn};
    public STATUS status = STATUS.Idle;
    //status val: idle, holding, willspawn, subspawn

    //for route
    private Vector2 myPosition = new Vector2(-1f, -1f);
    private int crossCost = 1;
    public int costToSelectedBall = 999;
    public Node previousNode = null;

    private NormalBall myBall = null;
    private NormalBall nextSpawnBall = null;

    public bool isAcceptByRouter = false;

    public SpriteRenderer sign;
    private Color defaultSignColor; 
    private bool isClassic = true;

    public NormalBall ghost;

    // void Awake()
    // {
    //     // sign = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    // }    
    void Start()
    {
        isClassic = (PlayerPrefs.GetString("GameMode") == "Classic");
        defaultSignColor = sign.color;
        status = STATUS.Idle;
    }
    void OnMouseDown()
    {
        if(GameController.turn == 1 && !GameController.gamecontroller.paused)
        {
            if(status == STATUS.Holding)
            {
                SoundSource.PlaySound("click");
                Board.mainBoard.SetSelectingBall(myBall);
            }
            else if (Board.mainBoard.selectingBall)
            {
                Board.mainBoard.SetTarget(GetComponent<Node>());
            }
        }
    }
    public void SetNextSpawnBall(Color ballColor)
    {
        status = STATUS.WillSpawn;
        nextSpawnBall = BallPool.TakeMyBall();
        if(nextSpawnBall)
            nextSpawnBall.SetColor(ballColor);

        sign.color = ballColor;
    }
    public void SpawnBall()
    {
        myBall = nextSpawnBall;
        if(!isClassic)
        {
            int rand = (int)Random.Range(0f,20f);
            if(rand < 1)
            {
                SpawnGhostBall();
            }
            else
            {
                SpawnNormalBall();
            }
        }
        else if(myBall)
        {
            SpawnNormalBall();
        }
        SetToDefaultSign();
    }
    void SpawnNormalBall()
    {
        myBall.ToAwakeAnimation();
        status = STATUS.Holding;
        myBall.SetMyStand(GetComponent<Node>());
        nextSpawnBall = null;
        myBall.gameObject.SetActive(true);
        myBall.gameObject.transform.position = transform.position;
    }
    void SpawnGhostBall()
    {
        myBall = Instantiate(ghost, transform.position, transform.rotation);
        myBall.SetColor(nextSpawnBall.myColor);
        status = STATUS.Holding;
        myBall.SetMyStand(GetComponent<Node>());
        nextSpawnBall = null;
        myBall.gameObject.SetActive(true);
    }
    public void Score()
    {
        if(myBall)
            myBall.Score();
        status = STATUS.Idle;
        myBall = null;
        DataController.datacontroller.AddScore(1);
    }

    public void SetToDefaultSign()
    {
        sign.color = defaultSignColor;
    }

    public void SetMyPosition(int x, int y)
    {
        myPosition.x = (float)x;
        myPosition.y = (float)y;
        status = STATUS.Idle;
    }
    public void SetMyBall(NormalBall newBall)
    {
        myBall = newBall;
    }
    public NormalBall GetMyBall()
    {
        return myBall;
    }
    public Vector2 GetMyPosition()
    {
        return myPosition;
    }

    void UpdateCrossCost()
    {
        if(status == STATUS.Holding)
            crossCost = 999;
        else
            crossCost = 1;
    }
    public bool HasHolding()
    {
        return status == STATUS.Holding && myBall != null;
    }
}
