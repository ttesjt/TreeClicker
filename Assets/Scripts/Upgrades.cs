using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public PlayerClick player;

    public int baseAutoClickRate = 0;
    public int baseCost = 10;
    public double costMultiplierPerLevel = 1.5;
    public int currentPurchaseLevel = 0;
    public int additionalClick = 0;
    public int healEarthAmount = 0;

    // Start is called before the first frame update
    void Start() {
    }

    // no check, but should advanced to the next level.
    public void PurchasedUpgrade() {
        if (player.SpendProfits(getCurrentCost())) {
            currentPurchaseLevel++;
            player.DeltaChangePerClick += additionalClick;
        }
    }

    public int getCurrentCost() {
        return (int) ((double)baseCost * costMultiplierPerLevel);
    }

    public int getCurrentAutoClickRate()
    {
        return baseAutoClickRate * currentPurchaseLevel;
    }
}
