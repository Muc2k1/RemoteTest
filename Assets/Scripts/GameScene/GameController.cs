using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    const int MAX_BALLS_CAN_CLEAR = 33;
    public static GameController gamecontroller;
    const int NODES_IN_ROW = 9;
    public static int turn = 1; //0: waiting, 1: thinking
    private int maxNumberOfColor = 3;
    private GameObject selectingBall = null;
    private string gameMode = "Classic";
    private int score = 0; 

    private Board board;
    // Start is called before the first frame update
    void Awake()
    {
        gamecontroller = this;
    }
    void Start()
    {
        board = Board.mainBoard;
    }
    void CheckEndGame()
    {

    }
    void EndGame()
    {

    }
    public void CheckScore(Node justUpdateNode)
    {
        int x_dir = (int)justUpdateNode.GetMyPosition().x;
        int y_dir = (int)justUpdateNode.GetMyPosition().y;
        DiagonalCheck(x_dir, y_dir);
        HorizontalCheck(x_dir, y_dir);
        VerticleCheck(x_dir, y_dir);
    }
    private void DiagonalCheck(int x, int y)
    {

    }

    private void HorizontalCheck(int x, int y)
    {

    }

    private void VerticleCheck(int x, int y)
    {

    }

    private void ScoreCheck()
    {

    }
}
