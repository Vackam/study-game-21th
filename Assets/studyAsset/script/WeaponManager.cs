using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    static WeaponManager _instance;


    public static WeaponManager Instance
    {
         get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<WeaponManager>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("WeaponManager");
                        _instance = obj.AddComponent<WeaponManager>();
                    }
                }
                return _instance;
            }
    }
    // Start is called before the first frame update
    [SerializeField]
    private List<Dictionary<bool, Weapon>> WeaponList = new List<Dictionary<bool, Weapon>>();

    public void AddWeapon(Weapon weapon)
    {
        Dictionary<bool, Weapon> weaponEntry = new Dictionary<bool, Weapon>();
        weaponEntry.Add(true, weapon);
    }

    /* �ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ� */
    GameObject player;
    /* �ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ� */
    // Basic Weapon ���� ����
    [SerializeField]
    private GameObject BasicWeapon;

    private float AttackRange = 5.0f; // �����Ÿ�
    private float AttackSpeed = 0.5f; // ���ʸ��� �߻� �� ����
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    [SerializeField]
    private Direction currentDir = Direction.LEFT;
    private float timer;

    private float waitingtime;
    
    // Basic Weapon ���� �Լ�
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
    void Attack()
    {
        if(timer > AttackSpeed)
            try
            {
                Vector3 MyPosition = transform.position;
                GameObject weaponCreate =  Instantiate(BasicWeapon, new Vector3(MyPosition.x, MyPosition.y, MyPosition.z), Quaternion.identity);
                weaponCreate.GetComponent<WeaponMovement>().SetDefault(AttackSpeed, AttackRange, currentDir.ToString());
                timer = 0.0f;
            }
            catch (UnassignedReferenceException)
            {
            }
        else
        {
            timer += Time.deltaTime;
        }
    }
    IEnumerator BasicAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackSpeed);
            Vector3 MyPosition = player.transform.position;
            GameObject weaponCreate = Instantiate(BasicWeapon, new Vector3(MyPosition.x, MyPosition.y, MyPosition.z), Quaternion.identity);
            weaponCreate.GetComponent<WeaponMovement>().SetDefault(AttackSpeed, AttackRange, currentDir.ToString());

        }  
    }

    /* �ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ� */
    // Circle Weapon ���� ����
    public GameObject[] CircleWeapon;

    [SerializeField]
    private GameObject CircleEntity;
    private int CircleCount = 3; // �⺻ ��� 3��.
    private float CircleRad = 1f; // ȸ�� ������
    private float CircleSpeed = 50f; // ȸ�� �ӵ�

    private float CircleCurrentDegree = 0f; // ���� ȸ�� ����

    // Circle Weapon ���� �Լ�
    private void CreateCircle(GameObject Player)
    {
        CircleWeapon = new GameObject[CircleCount];
        Vector3 newPosition = Player.transform.position;
        for (int i = 0; i < CircleCount; i++)
        {
            CircleWeapon[i] = Instantiate(CircleEntity, newPosition, Quaternion.identity);
        }
    }
    private void CircleUpdate(GameObject Player)
    {
        CircleCurrentDegree += Time.deltaTime * CircleSpeed;
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
            Vector3 newPosition = Player.transform.position + new Vector3(x, y, 0f);
            CircleWeapon[i].transform.position = newPosition;

            CircleWeapon[i].transform.rotation = Quaternion.Euler(0, 0, -angle);
        }
    }
    // Circle ��
    /* �ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ� */

    /* �ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ� */
    // Bomb Weapon ���� ����
    public GameObject Bomb;

    private float BombSpeed = 2.0f; // Bomb ���� �ֱ�
    private float BombTimer = 0f; // Bomb ������ ���� �ð� Ÿ�̸�

    // Bomb Weapon ���� �Լ�
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
            BombTimer += Time.deltaTime;
        }
    }
    // Bomb ��
    /* �ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ� */

    void Start()
    {
        // ��� ����� ����� �߽����� �����ϱ� �ϳ� �޾ƿ���
        player = GameMgr.Instance.GetPlayer();
        if (player == null)
        {
            Debug.LogError("�÷��̾� ������");
            return;
        }
        // Circle create
        CreateCircle(player);
        StartCoroutine(BasicAttack());
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            return;
        }
        CircleUpdate(player); 
        BombUpdate();
        AttackDirection();
        //Attack();
    }
}
