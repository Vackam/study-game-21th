using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIR;
    private string SAVE_DATA_FILE = "/SaveFile.txt"; // JSON �������� ����� ����.
    // ������ ��� coinManager���� �� �����;� ��.


    // Start is called before the first frame update
    void Start()
    {
        SAVE_DATA_DIR = Application.dataPath + "/studyAsset/script/Save/SaveFile";

        if(!Directory.Exists(SAVE_DATA_DIR))
        {
            Directory.CreateDirectory(SAVE_DATA_DIR);
        }
        // �� ��ȯ �� ������ ���̺� 
        SceneManager.sceneLoaded += OnSceneLoaded;

        // ���۵� ��, LoadData()
        LoadData();
    }
    public void SaveData()
    {
        // saveData ������ CoinManager���� ������ �� ���� ��������.
        saveData.SavedShopPlusAttackSpeed = CoinManager.Instance.ShopPlusAttackSpeed;
        saveData.SavedShopPlusHp = CoinManager.Instance.ShopPlusHp;
        saveData.SavedShopPlusSpeed = CoinManager.Instance.ShopPlusSpeed;

        string json = JsonUtility.ToJson(saveData); // Json ȭ ��Ű��
        
        File.WriteAllText(SAVE_DATA_DIR+SAVE_DATA_FILE, json);

        Debug.Log(SAVE_DATA_DIR + SAVE_DATA_FILE);
        Debug.Log("���� ��");
        Debug.Log(json);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SaveData();
    }

    public void LoadData()
    {
        if(File.Exists(SAVE_DATA_DIR + SAVE_DATA_FILE))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIR + SAVE_DATA_FILE);
            SaveData saveData = JsonUtility.FromJson<SaveData>(loadJson); // �̰� ����

            CoinManager.Instance.SetShopHp(saveData.SavedShopPlusHp);
            CoinManager.Instance.SetShopSpeed(saveData.SavedShopPlusSpeed);
            CoinManager.Instance.SetShopAttackSpeed(saveData.SavedShopPlusAttackSpeed);

            Debug.Log("�ε� ��");
            Debug.Log(saveData.SavedShopPlusHp);
        }
        else
        {
            Debug.Log("No Such File");
        }
    }

}
