using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    static WeaponManager _instance;


    public enum Weapon
    {
        BOMB,
        CIRCLE
    }
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
    private Dictionary<Weapon, bool> WeaponList = new Dictionary<Weapon, bool>();

    public void AddWeapon(Weapon weapon)
    {
        WeaponList.Add(weapon, true);
    }

    public bool IsWeaponTrue(Weapon weapon)
    {
        if (WeaponList.ContainsKey(weapon))
        {
            return WeaponList[weapon];
        }
        return false;
    }

    /* �ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ� */
    GameObject player;
    /* �ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ� */
    // Basic Weapon ���� ����
    [SerializeField]
    private GameObject BasicWeapon;

    private float AttackRange = 3.0f; // �����Ÿ�
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
	public void AddAttackSpeed(float AttackSpeed)
	{
		// Add but real action is subtract
		this.AttackSpeed -= AttackSpeed;
	}
	public void AddAttackRange(float AttackRange)
	{
		this.AttackRange += AttackRange;
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
    public void CreateCircle(GameObject Player)
    {
        CircleWeapon = new GameObject[CircleCount];
        Vector3 newPosition = Player.transform.position;
        for (int i = 0; i < CircleCount; i++)
        {
            CircleWeapon[i] = Instantiate(CircleEntity, newPosition, Quaternion.identity);
        }
        StartCoroutine(CircleUpdate(Player));
    }
    public IEnumerator CircleUpdate(GameObject Player)
    {
        while (true)
        {
            CircleCurrentDegree += Time.deltaTime * CircleSpeed;
            if (CircleCurrentDegree >= 360f)
            {
                CircleCurrentDegree -= 360f;
            }

            for (int i = 0; i < CircleCount; i++)
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
            yield return null;
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
    public void BombCall()
    {
       StartCoroutine(BombUpdate()); 
    }
    public IEnumerator BombUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(BombSpeed);
            try
            {
                Vector3 MyPosition = transform.position;
                GameObject weaponCreate = Instantiate(Bomb, new Vector3(MyPosition.x, MyPosition.y, MyPosition.z), Quaternion.identity);
                BombTimer = 0.0f;
            }
            catch (UnassignedReferenceException)
            {
            }
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
        //CreateCircle(player);
        StartCoroutine(BasicAttack());
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            return;
        }
        //CircleUpdate(player); 
        //BombUpdate();
        AttackDirection();
    }
}
