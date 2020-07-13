using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameController : Singleton<GameController>

{
    public GameObject locationStart;
    public GameObject locationCab;
    public Transform rigLocation;
    
    public void GotoCab()
    {
        /*rigLocation.position=new Vector3(locationCab);*/
        
    }

    public void GotoStart()
    {
        /*rigLocation.position=new Vector3(locationStart);*/
    }

}

