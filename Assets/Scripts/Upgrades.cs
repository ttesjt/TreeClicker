using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public PlayerClick player;

    public int autoClickRate = 0;
    public int autoHealRate = 0;
    public int additionalClick = 0;
    private CostAndEffects currentPurchaseLevel = null;

    private List<CostAndEffects> costs = new List<CostAndEffects>();

    // Start is called before the first frame update
    void Start() {
        LoadCosts();
        currentPurchaseLevel = GetNextUpgrade();
    }

    // no check, but should advanced to the next level.
    public void PurchasedUpgrade() {
        if (currentPurchaseLevel) {
            if (player.SpendProfits(currentPurchaseLevel.cost)) {
                // made the purchase...
                autoClickRate += currentPurchaseLevel.autoClickAddition;
                autoHealRate += currentPurchaseLevel.healEarthAddition;
                player.DeltaChangePerClick += currentPurchaseLevel.additionalClick;
            }
        }
        currentPurchaseLevel = GetNextUpgrade();
    }

    public CostAndEffects GetCurrentPurchaseLevel() {
        return currentPurchaseLevel;
    }

    private void LoadCosts() {
        CostAndEffects[] foundCosts = GetComponentsInChildren<CostAndEffects>(true);
        foreach (var cost in foundCosts)
        {
            if (cost != null) {
                costs.Add(cost);
            }
        }
    }

    private CostAndEffects GetNextUpgrade() {
        if (costs.Count > 0) {
            CostAndEffects cost = costs[0];
            costs.RemoveAt(0);
            return cost;
        }
        return null;
    }
}
