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
    // Circle Weapon ���� ����
    public GameObject[] CircleWeapon;

    [SerializeField]
    private GameObject CircleEntity;
    private int CircleCount = 3; // �⺻ ��� 3��.
    private float CircleRad = 1f; // ȸ�� ������
    private float CircleSpeed = 50f; // ȸ�� �ӵ�

    private float CircleCurrentDegree = 0f; // ���� ȸ�� ����
    // �������� ����
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
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            return;
        }
        CircleUpdate(player); 
    }
}
