using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    // Start is called before the first frame update
    // Player ��ġ ��ƴ� ���� ����.
    static GameMgr _instance;
    GameObject player;

    public static GameMgr Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("GameMgr �ν��Ͻ��� �������� �ʽ��ϴ�.");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("���� �� �� �� �̻��̳�");
        }
    }
    public GameObject GetPlayer()
    {
        return player;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
        if (player == null)
        {
            Debug.LogError("Player �±װ� ������ GameObject�� ã�� �� �����ϴ�.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
