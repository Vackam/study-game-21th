using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponMovement : MonoBehaviour
{
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    public float speed = 0;
    public float range = 0;
    public float timer = 0;
    public Direction dir;


    // 얘는 Player에서 알아서 해줄 거임.
    public void SetDefault(float speed, float range, string dir)
    {
        this.speed = speed;
        this.range = range;
        this.dir = (Direction)Enum.Parse(typeof(Direction), dir);
    }

    public void Movement()
    {
        if (dir == Direction.UP)
            transform.position += Vector3.up * speed * Time.unscaledDeltaTime;
        else if(dir == Direction.DOWN)
            transform.position += Vector3.down * speed * Time.unscaledDeltaTime;
        else if (dir == Direction.LEFT)
            transform.position += Vector3.left * speed * Time.unscaledDeltaTime;
        else if (dir == Direction.RIGHT)
            transform.position += Vector3.right * speed * Time.unscaledDeltaTime;
    }
    void Delete()
    {
       if (timer > range)
        {
            Destroy(this.gameObject);
            timer = 0;
        }

        timer += Time.unscaledDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyMovement>().hp -= 50;
            Destroy(this.gameObject);
        } 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Delete();
    }
}
