using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    const int NODES_IN_ROW = 9;
    private Node[,] nodes = new Node[NODES_IN_ROW,NODES_IN_ROW];
    // Start is called before the first frame update
    void Awake()
    {
        GameController.board = this.GetComponent<Board>();
        initNodes();

        //Debug side
        nodes[1, 1].SetNextSpawnBall(ColorDefine.Blue);
        nodes[1, 1].SpawnBall();
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
        UpdateMap();
    }
    void RouteForBall()
    {
        //lấy node đang được chọn
        //khởi tạo (lấy dữ liệu các cung a.k.a lấy các node có chứa ball hoặc không)
        //chọn node kế có độ dài nhỏ nhất
        //lấy node đó xét tiếp
        //xét đủ 81 node thì ngưng
    }

    void initNodes(){
        for (int i = 0; i < NODES_IN_ROW; i++)
        {
            for (int j = 0; j < NODES_IN_ROW; j++)
            {
                nodes[i,j] = transform.GetChild(NODES_IN_ROW * i + j).gameObject.GetComponent<Node>();
                nodes[i,j].SetMyPosition(i, j);
            }
        }
    }
}
