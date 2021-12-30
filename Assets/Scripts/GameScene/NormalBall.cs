using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NormalBall : MonoBehaviour
{
    public enum STATUS{ Idle, Using}
    public STATUS status = STATUS.Idle;
    private Color myColor = new Color(0,0,0,0);
    //color val: white, green, blue, yellow, red, orange, purple, brown, dark-green, dark-blue, nothing
    private Transform target = null;
    private Transform nextPos = null;
    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = myColor;
    }

    void SetNextPos()
    {

    }
    void SetTarget()
    {

    }
    public void SetColor(Color newColor)
    {
        myColor = newColor;
        gameObject.GetComponent<SpriteRenderer>().color = myColor;
        print(myColor);
    }
    void MoveToNextPos()
    {

    }
    void SetMeAsSelectedBall()
    {

    }
    void GetScore()
    {

    }
}
