using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    int numCrates;
    TextMeshProUGUI text;

    private void Start() {
        text = GetComponent<TextMeshProUGUI>();
        text.text = numCrates.ToString();
        Crate.cratePickedUp += CratePickedUp;
    }

    void CratePickedUp() {
        numCrates++;
        text.text = numCrates.ToString();
    }
}
