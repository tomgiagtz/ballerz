using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    // Start is called before the first frame update
    private float score = 0f;
    public TextMeshProUGUI scoreCounter;
    
    void OnEnable()
    {
        GameEvents.current.onCoinPickup += HandleCoinPickup;
        // scoreCounter = GetComponent<TextMeshProUGUI>();
    }
    
    private void OnDisable() {
        GameEvents.current.onCoinPickup -= HandleCoinPickup;
    }

    // Update is called once per frame
    void Update()
    {
        scoreCounter.SetText(score.ToString());
    }

    private void HandleCoinPickup(int id) {
        score++;
    }
}
