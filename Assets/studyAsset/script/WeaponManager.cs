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
            if(_instance == null){}
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

    void Start()
    {
        // ��� ����� ����� �߽����� �����ϱ� �ϳ� �޾ƿ���
        GameObject player = GameMgr.Instance.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
