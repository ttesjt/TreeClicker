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

    public AudioSource audio;
    public AudioClip chopWoodClip;

    public RectTransform canvasMainRectTransform;
    public GameObject floatingTextPrefab;
    public Image cursorImage;

    private int profitValue = 0;


    public GameObject finalStage;
    public TextMeshProUGUI winText;

    public OfferBoard offerBoard;
    public List<Upgrades> allUpgrades;

    private int chainsawLevel = 0, chainsawCost = 10; // todo. a class.
    private int multiplierCost = 10; // todo. a class.
    private float deltaTimeSecond = 0;

    public TextMeshProUGUI clickMultiplierText;
    public TextMeshProUGUI autoClickRateText;
    private int deltaChangePerClick = 1;
    private int autoClickRate = 0;

    public int ProfitValue {get{return profitValue;} set{profitValue = value;}}
    public int DeltaChangePerClick {get{return deltaChangePerClick;} set{deltaChangePerClick = value;}}

    void Start() {
        deltaTimeSecond = 0;
    }

    void Update() {
        deltaTimeSecond+=Time.deltaTime;
        if (deltaTimeSecond >= 1f) {
            int currentAutoRate = 0;
            // CutTrees(chainsawLevel);
            foreach (Upgrades upgrade in allUpgrades) {
                currentAutoRate += upgrade.getCurrentAutoClickRate();
                CutTrees(upgrade.getCurrentAutoClickRate());
                HealTheEarth(upgrade.healEarthAmount);
            }
            deltaTimeSecond = 0;
            autoClickRate = currentAutoRate;
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

    private void HealTheEarth(int amount) {
        GameRunner.currentInstance.HealTheEarth(amount);
    }

    // should cost.
    public bool SpendProfits(int amount) {
        if (profitValue >= amount) {
            profitValue -= amount;
            GameRunner.currentInstance.effectController.SpawnProfitSpark(playerID);
            return true;
        }
        return false;
    }

    public void PlayerClicked() {
        if (chopWoodClip) {audio.PlayOneShot(chopWoodClip);}
        GameObject floatingTextInstance = Instantiate(floatingTextPrefab, cursorImage.rectTransform);
        floatingTextInstance.transform.localPosition = Vector3.zero; // Set position to center of container
        // floatingTextInstance.transform.localScale = Vector3.one;
        floatingTextInstance.transform.parent = canvasMainRectTransform;
        floatingTextInstance.GetComponent<FloatingTextEditing>().setValue(deltaChangePerClick);

        Destroy(floatingTextInstance.gameObject, 0.5f);
        CutTrees(deltaChangePerClick);
        UpdateValues();
    }

    private void UpdateUpgradeStatus()
    {
        if (clickMultiplierText)
        {
            clickMultiplierText.text = deltaChangePerClick.ToString() + "/click";
        }
        if(autoClickRateText)
        {
            autoClickRateText.text = autoClickRate.ToString() + "/s";
        }
    }

    private void UpdateProfitText()
    {
        if (profitText != null)
        {
            profitText.text = profitValue.ToString();
        }
    }

    /* public void BuyChainSaw() {
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
    } */

    private void UpdateValues() {
        if (fillBar != null)
        {
            fillBar.value = (float)profitValue / (float)GameRunner.profitToWin; // Increase the slider's value by 0.1
        }
        UpdateProfitText();
        UpdateUpgradeStatus();
    }
}
