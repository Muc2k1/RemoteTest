using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    const int NODES_IN_ROW = 9;
    private GameObject[,] nodes = new GameObject[NODES_IN_ROW,NODES_IN_ROW];
    // Start is called before the first frame update
    void Awake()
    {
        GameController.board = this.GetComponent<Board>();
        for (int i = 0; i < NODES_IN_ROW; i++)
        {
            for (int j = 0; j < NODES_IN_ROW; j++)
            {
                nodes[i,j] = transform.GetChild(NODES_IN_ROW * i + j).gameObject;
            }
        }
    }

    // Update is called once per frame
    void UpdateMap()
    {
        
    }
    void SetSpawnBall()
    {

    }
    void SetSpawnQueue()
    {

    }
    void RouteForBall()
    {
        
    }
}
