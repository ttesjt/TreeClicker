using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTextEditing : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void setValue(int value) {
        text.text = "+ $" + value.ToString();
    }
}
