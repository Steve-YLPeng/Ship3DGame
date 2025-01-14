using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIctrl_selectmap : MonoBehaviour
{
    public GameObject panelCurrent;
    public Button map1, map2, map3, btnReturn;
    // Start is called before the first frame update
    
    void Start()
    {
        map1.onClick.AddListener(loadmap1);
        map2.onClick.AddListener(loadmap2);
        map3.onClick.AddListener(loadmap3);
        btnReturn.onClick.AddListener(clickReturn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void clickReturn(){
        Debug.Log("[Log] clickReturn");
        panelCurrent.SetActive(false);
    }
    private void loadmap1(){
        Debug.Log("[Log] map");
        SceneManager.LoadScene("MainGame");
    }
    private void loadmap2(){
        Debug.Log("[Log] map");
        SceneManager.LoadScene("map2");
    }
    private void loadmap3(){
        Debug.Log("[Log] map");
        SceneManager.LoadScene("map3");
    }
}
