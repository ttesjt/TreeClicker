using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeLabel : MonoBehaviour
{
    public TextMeshProUGUI priceTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPrice(int value) {
        priceTag.text = "$" + value.ToString();
    }
}
