using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIctrl_startmenu : MonoBehaviour
{
    public GameObject panelSetting, panelSelectMap;
    public Button btnStart, btnSetting;

    // Start is called before the first frame update
    void Start()
    {
        btnStart.onClick.AddListener(clickStart);
        btnSetting.onClick.AddListener(clickSetting);
        FindObjectOfType<audio_manager>().play("BGM");
        if(panelSetting == null){
            panelSetting = FindObjectOfType<UIctrl_setting>().gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void clickSetting(){
        Debug.Log("[Log] setting");
        
        
        panelSetting.SetActive(true);
    }
    private void clickStart(){
        Debug.Log("[Log] start");
        panelSelectMap.SetActive(true);
    }
}
