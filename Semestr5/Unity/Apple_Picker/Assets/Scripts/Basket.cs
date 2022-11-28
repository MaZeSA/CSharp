using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    [Header("Set Dynamically")]
    public Text scoreGT;

    public int level = 1;
    public Text textLevel; 
    public Text textNextLevel;
    AppleTree appleTree;
    int nextLevel = 1000;

    private void Start()
    {
        GameObject scoreGo = GameObject.Find("ScoreCounter");
        scoreGT = scoreGo.GetComponent<Text>();
        scoreGT.text = "0";

        GameObject textLevelGo = GameObject.Find("TextLevel");
        textLevel = textLevelGo.GetComponent<Text>();
        GameObject textNextLevelGo = GameObject.Find("TextNextLevel");
        textNextLevel = textNextLevelGo.GetComponent<Text>();

        GameObject appleTreeGo = GameObject.Find("AppleTree");
        appleTree = appleTreeGo.GetComponent<AppleTree>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedWith = collision.gameObject;
        if (collidedWith.tag == "Apple")
        {
            Destroy(collidedWith);
          
            int score = int.Parse(scoreGT.text);
            score += 100;
            scoreGT.text = score.ToString();

            if(score > HighScore.score)
            {
                HighScore.score = score;
            }

            if(score >= nextLevel )
            {
                LevelUP();
            }
        }
    }

    private void LevelUP()
    {
        level++;
        textLevel.text = "Level: " + level.ToString();
        nextLevel *= 2;
        textNextLevel.text = "Next Level: " + nextLevel;

        appleTree.speed = Mathf.Abs(appleTree.speed) + 10;
        Debug.Log(appleTree.speed);
        if (appleTree.speed % 2 == 0)
        {
            appleTree.chanceToCangeDirection += 0.01f;
            Debug.Log(appleTree.chanceToCangeDirection);
            appleTree.secondBetweenAppleDrops -= 0.1f;
        }
    }
}
