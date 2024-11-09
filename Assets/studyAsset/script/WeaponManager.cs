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

    /* ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ */
    GameObject player;
    /* ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ */
    // Circle Weapon 관련 변수
    public GameObject[] CircleWeapon;

    [SerializeField]
    private GameObject CircleEntity;
    private int CircleCount = 3; // 기본 원운동 3개.
    private float CircleRad = 1f; // 회전 반지름
    private float CircleSpeed = 50f; // 회전 속도

    private float CircleCurrentDegree = 0f; // 현재 회전 각도
    // 에러나서 변경
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
        // 모든 무기는 사용자 중심으로 나가니까 하나 받아오기
        player = GameMgr.Instance.GetPlayer();
        if (player == null)
        {
            Debug.LogError("플레이어 못잡음");
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
