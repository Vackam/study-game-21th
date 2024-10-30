using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeaponMovement : MonoBehaviour
{
    public float BombTimer = 1.0f;
    public float BombRad = 3.0f;

    public float Counter = 0.0f;

    public Collider2D[] colliders = new Collider2D[50]; // 우선 50마리만 잡도록.

    public LayerMask EnemyLayer;

    void BombAttack()
    {
        // 1초가 안지난 경우
        if(BombTimer > Counter)
        {
            Counter += Time.deltaTime;
        }

        else
        {
            // 주변 적에게 피해
            // 주변 적 확인
            colliders = Physics2D.OverlapCircleAll(transform.position, BombRad, EnemyLayer);
            int count = 0;
            if(colliders.Length > 0)
            {
                while(colliders[count] && count <= 50)
                {
                    colliders[count].gameObject.GetComponent<EnemyMovement>().hp -= 100;
                    count++;
                } 
            }
            // 삭제한다.
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
        BombAttack(); 
    }
}
