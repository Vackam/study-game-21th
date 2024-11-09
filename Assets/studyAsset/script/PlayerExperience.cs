using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    private float Exp;
    private float nextLevelTarget;
    // Start is called before the first frame update

    void SetNextLevelTarget(float nextLevelTarget)
    {
        this.nextLevelTarget = nextLevelTarget;
    } 

    public void AddExp(float exp)
    {
        this.Exp += exp;
    }

    void GetNextLevel()
    {
        if (nextLevelTarget < Exp)
        {
            // �� �ʱ�ȭ
            Exp = 0.0f;
            SetNextLevelTarget(nextLevelTarget + 10.0f);

            // ��ȣ ������
        }
    }
    void Start()
    {
        Exp = 0.0f;
        SetNextLevelTarget(100.0f);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
