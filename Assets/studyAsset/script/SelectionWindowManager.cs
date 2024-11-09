using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionWindowManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SelectionWindowManager Instance {  get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        HideLevelUpPopup();
    }
    public void ShowLevelUpPopup()
    {
        this.gameObject.SetActive(true);
    }

    public void HideLevelUpPopup()
    {
        this .gameObject.SetActive(false);  
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
