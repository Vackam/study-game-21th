using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Transform transform;
    private float speed = 5f;
    public GameObject abc;
    public GameObject Weapon;
    public GameObject[] CircleWeapon;
    public GameObject Bomb;


    // Weapon 관련 변수
    public float AttackRange = 5.0f; // 얼마나 갈 건지 시간으로 사정거리를 늘리는 느낌.
    public float AttackSpeed = 0.5f; // 몇초마다 발사 할 건지
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    public Direction currentDir = Direction.LEFT;
    public float timer;

    public float waitingtime;

    // Circle Weapon 관련 변수
    public int CircleCount = 3; // 기본 원운동 3개.
    public float CircleRad = 1f; // 회전 반지름
    public float CircleSpeed = 50f; // 회전 속도

    private float CircleCurrentDegree = 0f; // 현재 회전 각도

    // Bomb Weapon 관련 변수
    public float BombSpeed = 2.0f;
    public float BombTimer = 0f;

    public bool theWorld = false;

    void CircleUpdate()
    {
        CircleCurrentDegree += Time.unscaledDeltaTime * CircleSpeed;
        if(CircleCurrentDegree >= 360f)
        {
            CircleCurrentDegree -= 360f;
        }

        for(int i=0; i<CircleCount; i++)
        {
            if (CircleWeapon[i] == null) continue;

            float angle = CircleCurrentDegree + i * (360f / CircleCount);
            float rad = angle * Mathf.Deg2Rad;

            var x = CircleRad * Mathf.Sin(rad);
            var y = CircleRad * Mathf.Cos(rad);
            Vector3 newPosition = transform.position + new Vector3(x, y, 0f);
            CircleWeapon[i].transform.position = newPosition;

            CircleWeapon[i].transform.rotation = Quaternion.Euler(0, 0, -angle);
        }
    }
    void getLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.unscaledDeltaTime;
        }
    }

    void getRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.unscaledDeltaTime;
        }

    }

    void getDown()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.unscaledDeltaTime;
        }
    }

    void getUp()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.unscaledDeltaTime;
        }
    }
    
    void Attack()
    {
        if(timer > AttackSpeed)
            try
            {
                Vector3 MyPosition = transform.position;
                GameObject weaponCreate =  Instantiate(Weapon, new Vector3(MyPosition.x, MyPosition.y, MyPosition.z), Quaternion.identity);
                weaponCreate.GetComponent<WeaponMovement>().SetDefault(AttackSpeed, AttackRange, currentDir.ToString());
                timer = 0.0f;
            }
            catch (UnassignedReferenceException)
            {
            }
        else
        {
            timer += Time.unscaledDeltaTime;
        }

    }

    void AttackDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentDir = Direction.UP;
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            currentDir = Direction.DOWN;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentDir = Direction.LEFT;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            currentDir = Direction.RIGHT;
        }

    }

    void BombUpdate()
    {
        if(BombSpeed < BombTimer)
        {
            try
            {
                Vector3 MyPosition = transform.position;
                GameObject weaponCreate =  Instantiate(Bomb, new Vector3(MyPosition.x, MyPosition.y, MyPosition.z), Quaternion.identity);
                BombTimer = 0.0f;
            }
            catch (UnassignedReferenceException)
            {
            }
        }
        else
        {
            BombTimer += Time.unscaledDeltaTime;
        }
    }

    void TheWorld()
    {
        if (Input.GetButtonDown("Fire1") && !theWorld)
        {
            Time.timeScale = 0;
            theWorld = true;
        }
        else if(Input.GetButtonDown("Fire1") && theWorld)
        {
            Time.timeScale = 1;
            theWorld = false;
        }
    }

    void Start()
    {
        transform = GetComponent<Transform>();
        transform.position = new Vector3(0, 0, 0);
        timer = 0.0f;
        waitingtime = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        getUp();
        getRight();
        getDown();
        getLeft();
        Attack();
        AttackDirection();
        CircleUpdate();
        BombUpdate();
        TheWorld();
    }
}
