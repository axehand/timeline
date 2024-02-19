using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SignalControl : MonoBehaviour
{

    public PlayableDirector director;
    public void ReceiveSignal(){
        Debug.Log("next anim start");
        director.Pause();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
