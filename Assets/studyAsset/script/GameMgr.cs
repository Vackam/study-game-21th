using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    // Start is called before the first frame update
    // Player ��ġ ��ƴ� ���� ����.
    // GameManage�� �Ѱ�����.
    static GameMgr _instance;
    GameObject player;
    private bool isLevelUp = false;

    [SerializeField]
    private GameObject GameOverUI;

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
        player = GameObject.FindGameObjectWithTag("Player"); 
        if (player == null)
        {
            Debug.LogError("Player �±װ� ������ GameObject�� ã�� �� �����ϴ�.");
        }
    }

    public void ShowDeathUI()
    {
        Time.timeScale = 0.0f;
        GameOverUI.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
