using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWeaponMovement : MonoBehaviour
{
    // Start is called before the first frame update
    float circleR; //반지름
    float deg; //각도
    float objSpeed; //원운동 스피드
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyMovement>().hp -= 50;
        } 
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
