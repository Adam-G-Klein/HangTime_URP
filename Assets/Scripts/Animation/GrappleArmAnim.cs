using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleArmAnim : MonoBehaviour
{
    private Transform playerFocus;
    private Transform cam;
    private Transform player;

    public float aimDist = 10f;
    public float armLength = 1f;
    public float idleXOffset = 1f;
    private Vector3 aimLoc = new Vector3(0,0,0);
    private Vector3 focusToAimLoc = new Vector3(0,0,0);
    private Vector3 playerToFocus = new Vector3(0,0,0);
    private Vector3 aimDir = new Vector3(0,0,0);
    private Vector3 handPos = new Vector3(0,0,0);
    private bool startHasRun = false;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        playerFocus = GameObject.FindGameObjectWithTag("PlayerFocus").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startHasRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        getAimLoc();
        transform.position = getOutstrechedHandPos();
    }
    void OnDrawGizmos(){
        if(startHasRun) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(aimLoc, 0.1f);
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(playerFocus.position, focusToAimLoc);
            Gizmos.DrawRay(player.position, playerToFocus);
            Gizmos.DrawRay(player.position, aimDir);
            Gizmos.DrawSphere(handPos, 0.1f);
        }

    }

    private void getAimLoc(){
        Vector3 retDir = playerFocus.position - cam.position;
        retDir.Normalize();
        Vector3 loc = (retDir * aimDist) + playerFocus.position;
        aimLoc = loc;
    }

    private Vector3 getOutstrechedHandPos(){
        focusToAimLoc = aimLoc - playerFocus.position;
        playerToFocus = playerFocus.position - player.position; 
        aimDir = (focusToAimLoc + playerToFocus).normalized;
        handPos = (aimDir * armLength) + player.position;
        handPos = new Vector3(handPos.x + idleXOffset, handPos.y, handPos.z);
        return handPos;
    }
}
