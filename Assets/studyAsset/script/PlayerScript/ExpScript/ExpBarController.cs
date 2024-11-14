using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    public float MaxExp;
    protected float CurExp;

    public Slider ExpSlider;
    void Start()
    {
        player = GameMgr.Instance.GetPlayer();
        MaxExp = player.GetComponent<PlayerExperience>().GetnextLevelTarget();
        CurExp = player.GetComponent<PlayerExperience>().Exp;
    }

    // Update is called once per frame
    void Update()
    {
        // ������Ʈ ��ȣ�ø� �ٲ�� ����
        if (ExpSlider != null)
        {
            if (CurExp != player.GetComponent<PlayerExperience>().Exp)
            {
                MaxExp = player.GetComponent<PlayerExperience>().GetnextLevelTarget();
                CurExp = player.GetComponent<PlayerExperience>().Exp;
                ExpSlider.value = CurExp / MaxExp;
            }
        }
    }
}
