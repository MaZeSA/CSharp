using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject appplePrefab;
    public float speed = 1f;
    //
    public float leftAndRightAdge = 10f;
    //зміна напряму
    public float chanceToCangeDirection = 0.1f;
    //частота скидання
    public float secondBetweenAppleDrops = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DropApple", 2);
    }

    private void DropApple()
    {
        GameObject apple = Instantiate<GameObject>(appplePrefab);
        apple.transform.position = this.transform.position;
        Invoke("DropApple", secondBetweenAppleDrops);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed*Time.deltaTime;
        transform.position = pos;
        if(pos.x < -leftAndRightAdge)
        {
            speed = Mathf.Abs(speed);
        }
        else if(pos.x > leftAndRightAdge)
        {
            speed = -Mathf.Abs(speed);
        }
    }

    public void FixedUpdate()
    {
        if(Random.value < chanceToCangeDirection)
        {
            speed *= -1;
        }
    }
}
