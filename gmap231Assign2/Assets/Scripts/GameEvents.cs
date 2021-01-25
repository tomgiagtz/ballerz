using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    //singleton GameEvents object placed in scene
    public static GameEvents current;
    void Awake()
    {
        //set current to this instance
        current = this;
    }

    //create on coin pickup event
    public event Action<int> onCoinPickup;
    //function to be run when coin pickup event is triggered
    public void CoinPickup(int id)
    {   
        //if there are listeners to this event
        if (onCoinPickup != null)
        {
            //pass id value along to listeners
            onCoinPickup(id);
        }
    }


    //very similarly, create events for buttontrigger enter and exit
    public event Action<int> onButtonTriggerEnter;
    public void ButtonTriggerEnter(int id) {
        if (onButtonTriggerEnter != null) {
            onButtonTriggerEnter(id);
        }
    }

    public event Action<int> onButtonTriggerExit;
    public void ButtonTriggerExit(int id) {
        if (onButtonTriggerExit != null) {
            onButtonTriggerExit(id);
        }
    }
}
