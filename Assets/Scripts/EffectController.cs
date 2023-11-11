using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public GameObject player1ProfitSpark, player2ProfitSpark; // Assign the UI GameObject in the Unity Editor
    public GameObject profitSpendEffect;

    public GameObject woodPiecesEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 10;

            // Instantiate the particle effect at the mouse position
            GameObject instantiatedEffect = Instantiate(woodPiecesEffect, mousePosition, Quaternion.identity);
            Destroy(instantiatedEffect, 2.0f);
        } */
    }

    public void ChopWoodEffect(Vector2 chopPosition) {
        Vector3 mousePosition = chopPosition;
        mousePosition.z = 10;

        // Instantiate the particle effect at the mouse position
        GameObject instantiatedEffect = Instantiate(woodPiecesEffect, mousePosition, Quaternion.identity);
        Destroy(instantiatedEffect, 2.0f);
    }

    public void SpawnProfitSpark(string playerID) {
        GameObject profitSpark = playerID == "Player 1"? player1ProfitSpark : player2ProfitSpark;
        if (profitSpark != null && profitSpendEffect != null)
        {
            Vector3 worldPosition = profitSpark.transform.position;
            worldPosition.z = 10;

            GameObject instantiatedEffect = Instantiate(profitSpendEffect, worldPosition, Quaternion.identity);
            Destroy(instantiatedEffect, 2.0f);
        }
    }
}
