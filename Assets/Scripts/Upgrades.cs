using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public PlayerClick player;
    public GameObject UIPrefab;

    public string upgradeId = "";
    public int baseAutoClickRate = 0;
    public int baseCost = 10;
    public float costMultiplierPerLevel = 1.5f;
    public float minRate = 1.0f, maxRate = 1.0f; // I'm removing random for now
    public int currentPurchaseLevel = 0;
    public int additionalClick = 0;
    public int healEarthAmount = 0;

    public bool healIsReady = false;


    public int availableAfterProfitLevel = -1; // no.
    public bool availableAfterEarthShowUp = false; // no.

    private bool upgradeAvailable = false;
    private int currentPrice = 0;

    // Start is called before the first frame update
    void Start() {
    }

    void Update() {
        if (availableAfterProfitLevel >= 0 && HaveMoreMoneyThan(availableAfterProfitLevel)) {
            player.offerBoard.upgrades.Add(this);
            upgradeAvailable = true;
        }
        if (availableAfterEarthShowUp && GameRunner.currentInstance.EarthShowUp) {
            player.offerBoard.upgrades.Add(this);
            upgradeAvailable = true;
        }
    }

    // no check, but should advanced to the next level.
    // Maybe remove from offerboard after the purchase, also set upgradeAvailable to false
    public bool PurchasedUpgrade() {
        if (player.SpendProfits(GetPriceAfterInflation())) {
            currentPurchaseLevel++;
            if(healEarthAmount > 0)
            {
                healIsReady = true;
            }
            player.DeltaChangePerClick += additionalClick;
            return true;
        }
        return false;
    }

    // just for more fun, I added a RNG factor to the cost
    public void SetCurrentCost(GameObject labalInstance) {
        // currentPrice = (int) ((float)baseCost * costMultiplierPerLevel * Random.Range(minRate, maxRate)); // remove it if needed.
        currentPrice = (int)((float)baseCost * costMultiplierPerLevel);
        labalInstance.GetComponent<UpgradeLabel>().SetPrice(currentPrice);
    }

    public int GetPriceAfterInflation() {
        return currentPrice;
    }

    public int getCurrentAutoClickRate()
    {
        return baseAutoClickRate * currentPurchaseLevel;
    }




    // conditions
    public bool HaveMoreMoneyThan(int money) {
        if (player.ProfitValue >= money) {
            return true;
        }
        return false;
    }
}
