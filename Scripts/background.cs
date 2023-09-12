using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
      private float backGroundMoveSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * backGroundMoveSpeed * Time.deltaTime;

        if(transform.position.y < -15 )
        {
            transform.position += new Vector3(0, 40,0);
        }
    }
}
