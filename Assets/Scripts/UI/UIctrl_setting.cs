using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIctrl_setting : MonoBehaviour
{
    public GameObject panelCurrent;
    public Button btnReturn, btnApply;
    public Slider slider1,slider2;
    public Dropdown d_Dropdown;
    [SerializeField] private GameObject heli;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        btnReturn.onClick.AddListener(clickReturn);
        btnApply.onClick.AddListener(clickApply);
        slider1.onValueChanged.AddListener(delegate {BGMChange(); });
        slider2.onValueChanged.AddListener(delegate {effectChange(); });
        d_Dropdown.onValueChanged.AddListener(delegate {difficultyChanged(d_Dropdown);});
        // FindObjectOfType<setting_stat>().difficulty = .5f;
        setting_stat.difficulty = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void clickReturn(){
        Debug.Log("[Log] clickReturn");
        panelCurrent.SetActive(false);
        heli.SetActive(true);
    }
    private void clickApply(){
        Debug.Log("[Log] clickApply");
        panelCurrent.SetActive(false);
        heli.SetActive(true);
    }

    // Invoked when the value of the slider changes.
    public void BGMChange()
    {
    
        //Debug.Log(slider1.value);
        foreach(Sound s in FindObjectOfType<audio_manager>().sounds){
            if(s.tag == "bgm"){
                s.volume = slider1.value;
                s.source.volume = s.volume;
            }
        }
        setting_stat.bgmVolume = slider1.value;

    }
    public void effectChange(){
        foreach(Sound s in FindObjectOfType<audio_manager>().sounds){
            if(s.tag == "effect"){
                s.volume = slider2.value;
                s.source.volume = s.volume;
                Debug.Log(slider2.value);
            }
        }
    }
    public void difficultyChanged(Dropdown change){
        if(change.value ==0){
            // FindObjectOfType<setting_stat>().difficulty = 1.0f;
            setting_stat.difficulty = 0.5f;
        }
        else if(change.value ==1){
            // FindObjectOfType<setting_stat>().difficulty = 0.5f;
            setting_stat.difficulty = 0.0f;
        }
        else if(change.value ==2){
            // FindObjectOfType<setting_stat>().difficulty = 0.0f;
            setting_stat.difficulty = -0.5f;
        }
        Debug.Log(setting_stat.difficulty);

    }
}
