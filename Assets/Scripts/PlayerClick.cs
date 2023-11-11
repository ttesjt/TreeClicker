using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerClick : MonoBehaviour
{
    public string playerID = "";
    public Slider fillBar;
    public TextMeshProUGUI profitText;
    private int profitValue = 0;

    public GameObject finalStage;
    public TextMeshProUGUI winText;

    private int chainsawLevel = 0, chainsawCost = 10; // todo. a class.
    private int multiplierCost = 10; // todo. a class.
    private float deltaTimeSecond = 0;


    private int deltaChangePerClick = 1;

    void Start() {
        deltaTimeSecond = 0;
    }

    void Update() {

        deltaTimeSecond+=Time.deltaTime;
        if (deltaTimeSecond >= 1f) {
            CutTrees(chainsawLevel);
            deltaTimeSecond = 0;
        }
        UpdateValues();

        if (profitValue >= GameRunner.profitToWin) {
            if (!finalStage.activeSelf) {
                GameRunner.currentInstance.WinGame(playerID);
            }
            // GameRunner.currentInstance.WinGame(playerID);
        }
    }

    private void CutTrees(int amount) {
        profitValue = profitValue + amount;
        GameRunner.currentInstance.DamageTheEarth(amount);
    }

    // should cost.
    private bool SpendProfits(int amount) {
        if (profitValue >= amount) {
            profitValue -= amount;
            GameRunner.currentInstance.effectController.SpawnProfitSpark(playerID);
            return true;
        }
        return false;
    }

    public void PlayerClicked() {
        CutTrees(deltaChangePerClick);
        UpdateValues();
    }

    private void UpdateProfitText()
    {
        if (profitText != null)
        {
            profitText.text = profitValue.ToString();
        }
    }

    public void BuyChainSaw() {
        if (SpendProfits(chainsawCost)) {
            chainsawLevel += 1;
            UpdateValues();
        }
    }

    public void BuyMultiplier() {
        if (SpendProfits(multiplierCost)) {
            deltaChangePerClick += 1;
            UpdateValues();
        }
    }

    private void UpdateValues() {
        if (fillBar != null)
        {
            fillBar.value = (float)profitValue / (float)GameRunner.profitToWin; // Increase the slider's value by 0.1
        }
        UpdateProfitText();
    }
}
