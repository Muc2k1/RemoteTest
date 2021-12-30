using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int turn = 0; //0: waiting, 1: thinking
    private int maxNumberOfColor = 3;
    private GameObject selectingBall = null;
    public static Board board = null;
    private string gameMode = "Classic";
    private int score = 0; 
    // Start is called before the first frame update

    void CheckEndGame()
    {

    }
    void EndGame()
    {

    }
    void CheckScore()
    {

    }
}
