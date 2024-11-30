using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    // ������ ���� ���� 20���� ����.
    private static CoinManager _instance = null;
    public int ShopPlusHp;
    public float ShopPlusAttackSpeed;
    public float ShopPlusSpeed;
    public static CoinManager Instance
    {
        get
        {
            return _instance;
        }
    }
    public int Coin = 0;

    public void SetShopHp(int savedPlusHp)
    {
        ShopPlusHp = savedPlusHp;
        Debug.Log(savedPlusHp);
        Debug.Log("������� �Դ�? HP���� �˷��帳�ϴ�.");
    }

    public void SetShopAttackSpeed(float savedPlusAttackSpeed)
    {
        Debug.Log("���� ���� ������� ATTACKSPPED: " + savedPlusAttackSpeed);
        ShopPlusAttackSpeed = savedPlusAttackSpeed;
    }

    public void SetShopSpeed(float savedPlusSpeed)
    {
        Debug.Log("���� ���� ������� SPEED: " + savedPlusSpeed);
        ShopPlusSpeed = savedPlusSpeed;
    }
    void Awake()
    {
        //ShopPlusAttackSpeed = 0;
        //ShopPlusSpeed = 0;
        //ShopPlusHp = 0;
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject); 
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    public void Add20()
    {
        Coin += 20;
    }
}
