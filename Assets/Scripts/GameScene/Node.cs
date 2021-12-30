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
    private Vector2 previousNode = new Vector2(-1f, -1f);

    private NormalBall myBall = null;
    private NormalBall nextSpawnBall = null;

    public void SetNextSpawnBall(Color ballColor)
    {
        nextSpawnBall = BallPool.TakeMyBall();
        nextSpawnBall.SetColor(ballColor);
    }
    public void SpawnBall()
    {
        myBall = nextSpawnBall;
        nextSpawnBall = null;
        myBall.gameObject.SetActive(true);
        myBall.gameObject.transform.position = transform.position;
    }
    void SetMeAsTarget()
    {

    }
    void SetMeAsSelectedBall()
    {

    }

    public void SetMyPosition(int x, int y)
    {
        myPosition.x = (float)x;
        myPosition.y = (float)y;
        status = STATUS.Idle;
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
