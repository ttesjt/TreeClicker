using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameRunner : MonoBehaviour
{
    public static GameRunner currentInstance;
    public int profitToWin = 500;
    public int earthHealth = 800; // a random huge number.
    public int maxEarthHealth = 800;

    public AudioSource audio;
    public AudioClip winClip, loseClip;

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
            if (winClip) {audio.PlayOneShot(winClip);}
            winner = playerID;
            finalStage.SetActive(true);
            finalStageDisplay.text = playerID + ", thanks to your genius business acumen, you were able to annihilate the competition and monopolize the entire industry. Woohoo!!";
        }
    }

    private bool lost = false;
    public void LoseGame() {
        if (!lost) {
            lost = true;
            if (loseClip) {audio.PlayOneShot(loseClip);}
            finalStage.SetActive(true);
            finalStageDisplay.text = "In your reckless pursuits for profit, you both managed to ruin the entire earth, killing everyone in the process. Great going genius.";
        }
    }
}
