using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private GrappleNotes grappleNotes;
    // Start is called before the first frame update
    void Start()
    {
        grappleNotes = transform.GetComponentInChildren<GrappleNotes>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void grappleSounds(){
        grappleNotes.grappleSound();
    }

    public void nextChord(){
        grappleNotes.nextChord();
    }
}
