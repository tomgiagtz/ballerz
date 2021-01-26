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
        //sub to oncoin pickup
        GameEvents.current.onCoinPickup += HandleCoinPickup;
        
    }
    
    private void OnDisable() {
        //unsub from oncoin pickup
        GameEvents.current.onCoinPickup -= HandleCoinPickup;
    }

    // Update is called once per frame
    void Update()
    {
        //update score value every frame, could be in handleCoinPickup tbh
        scoreCounter.SetText(score.ToString());
    }

    private void HandleCoinPickup(int id) {
        //increment score on coin pickup
        score++;
    }
}
