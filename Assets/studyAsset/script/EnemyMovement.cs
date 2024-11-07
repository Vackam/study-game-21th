using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform playerTransform;
    private float speed = 2.5f;
    public float hp = 100;
    void Start()
    {
        GameObject player = GameMgr.Instance.GetPlayer();
        if(player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Not Player Tag");
        }
    }

    void DeleteEnemy()
    {
        if(this.hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DeleteEnemy();
        if(playerTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }
}
