using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    // Start is called before the first frame update
    // Player 위치 잡아다 끄는 역할.
    // GameManage를 총괄하자.
    static GameMgr _instance;
    GameObject player;
    private bool isLevelUp = false;

    public static GameMgr Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("GameMgr 인스턴스가 존재하지 않습니다.");
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
            Debug.LogError("쉬발 왜 두 개 이상이노");
        }
        player = GameObject.FindGameObjectWithTag("Player"); 
        if (player == null)
        {
            Debug.LogError("Player 태그가 지정된 GameObject를 찾을 수 없습니다.");
        }
    }
    public GameObject GetPlayer()
    {
        return player;
    }

    public bool SetIsLevelUp(bool a)
    {
        return isLevelUp = a;
    }

    public void ManageLevel()
    {
        if (isLevelUp)
        {
            SelectionWindowManager.Instance.ShowLevelUpPopup();        
        }
    }
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
