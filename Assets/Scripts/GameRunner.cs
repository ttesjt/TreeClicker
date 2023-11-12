using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameRunner : MonoBehaviour
{
    public static GameRunner currentInstance;
    public static int profitToWin = 500;
    public static int earthHealth = 800; // a random huge number.
    public static int maxEarthHealth = 800;

    public static string winner = "";

    public EffectController effectController;
    public EarthHealthEvents earthHealthEvents;

    public GameObject finalStage;
    public TextMeshProUGUI finalStageDisplay;
    public Image earthFill;
    private bool earthShowUp = false;
    public bool EarthShowUp {get{return earthShowUp;} set{earthShowUp = value;}}


    // Start is called before the first frame update
    void Start()
    {
        currentInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        earthHealthEvents.CheckForEvents((float)earthHealth/(float)maxEarthHealth);
    }

    public void DamageTheEarth(int amount) {
        earthHealth -= amount;
        float healthBarFillAmount = (float)earthHealth / (float)maxEarthHealth;
        earthFill.fillAmount = healthBarFillAmount;
        if (earthHealth <= 0) {
            LoseGame();
        }
    }

    public void HealTheEarth(int amount) {
        earthHealth += amount;
        float healthBarFillAmount = (float)earthHealth / (float)maxEarthHealth;
        earthFill.fillAmount = healthBarFillAmount;
    }

    public void WinGame(string playerID) {
        if (winner == "") {
            winner = playerID;
            finalStage.SetActive(true);
            finalStageDisplay.text = playerID + " has won!!";
        }
    }

    public void LoseGame() {
        if (winner == "") {
            finalStage.SetActive(true);
            finalStageDisplay.text = " You all dead ";
        }
    }
}
