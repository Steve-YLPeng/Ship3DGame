using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Timer{
    public SimpleTimer timer;
    public Image cooldown;
}

[System.Serializable]
public class Stats{
    public TMP_Text engine;
    public TMP_Text score_board;
    public TMP_Text hp_bar_text;
    public TMP_Text game_timer;

    public int score;
    public float hp; 
    public Image hp_bar;
}
public class UIctrl_player : MonoBehaviour
{
    [SerializeField] private HelicopterController _controller;

    [SerializeField] private PlayerState _playerState;
    [SerializeField] private Timer[] timers;

    [SerializeField] private Stats info;

    [SerializeField] private Button btnSetting;
    [SerializeField] private GameObject panelSetting,heli;

    // Start is called before the first frame update
    void Start()
    {
        //cooldown_bomb.gameObject.SetActive(false);
        foreach(Timer t in timers){
            //t.cooldown.gameObject.SetActive(false);
            t.cooldown.fillAmount = 0.0f;
        }
        info.score = 0;
        info.hp = 1.0f;

        btnSetting.onClick.AddListener(clickSetting);
        //panelSetting = FindObjectOfType<UIctrl_setting>().gameObject;
        //panelSetting = GameObject.Find("setting_menu");
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Timer t in timers){
            float clock =  t.timer.GetRemainingTime();
            float maxClock =  t.timer.GetTimer();
            if(clock > 0.0f){
                t.cooldown.gameObject.SetActive(true);
                t.cooldown.fillAmount = clock * (1.0f / maxClock);
            }
            else
            {
                t.cooldown.fillAmount = 0.0f;
            }
            //Debug.Log("timer:"+t.timer.GetRemainingTime());
        }
        info.engine.text = "Engine: " + _controller.EngineForce.ToString("F2");
        info.score_board.text = "Score: " + EnemyInfo.enemyPlayerGetScore.ToString();
        info.game_timer.text = "Timer: " + PlayerState._gameEndTimer.ToString("F2") + " s";
        info.hp_bar_text.text =  _playerState.currentHealth + " / " + _playerState.maxHealth;
        info.hp_bar.fillAmount = _playerState.currentHealth * (1.0f / _playerState.maxHealth);
    }
    private void clickSetting(){
        Debug.Log("[Log] setting");
        panelSetting.SetActive(true);
        heli.SetActive(false);
    }
}

