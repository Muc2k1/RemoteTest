using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //color val: white, green, blue, yellow, red, orange, purple, brown, dark-green, dark-blue, nothing
    public enum STATUS {Idle, Holding, WillSpawn, SubSpawn};
    public STATUS status;
    //status val: idle, holding, willspawn, subspawn

    //for route
    private Vector2 myPosition = new Vector2(-1f, -1f);
    private int crossCost = 1;
    public int costToSelectedBall = 999;
    public Node previousNode = null;

    private NormalBall myBall = null;
    private NormalBall nextSpawnBall = null;

    public bool isAcceptByRouter = false;
    void OnMouseDown()
    {
        if(GameController.turn == 1)
        {
            if(status == STATUS.Holding)
            {
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
        nextSpawnBall.SetColor(ballColor);
    }
    public void SpawnBall()
    {
        status = STATUS.Holding;
        myBall = nextSpawnBall;
        myBall.SetMyStand(GetComponent<Node>());
        nextSpawnBall = null;
        myBall.gameObject.SetActive(true);
        myBall.gameObject.transform.position = transform.position;
    }
    public void Score()
    {
        status = STATUS.Idle;
        BallPool.GiveBackBall(myBall);
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
}
