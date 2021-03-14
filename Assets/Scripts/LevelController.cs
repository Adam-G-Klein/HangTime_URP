using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject curLevel;
    private GameObject curObject;
    private Vector3 spawnLocation;
    [SerializeField]
    private Vector3 spawnOffset = new Vector3(0, 2f, 0);
    private GameObject player;
    public float deathOffset = 10f;
    private Transform newStartingPlatform;
    void Start()
    {
        curLevel = GameObject.Find("Level1");
        newStartingPlatform = curLevel.gameObject.transform.Find("StartingPlatform");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void CheckLevelUpdate(GameObject GO){
        // if the object is under a new parent this means the player has gotten to a new level
        GameObject nextLevel = GO.transform.parent.gameObject;
        if(!nextLevel.name.Equals(curLevel.name))
        {
            curLevel.transform.Find("StartingPlatform").transform.gameObject.SetActive(false);
            curLevel = nextLevel;
            
            newStartingPlatform = nextLevel.transform.Find("StartingPlatform").transform;
            newStartingPlatform.gameObject.SetActive(true);
            newStartingPlatform.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            spawnLocation = newStartingPlatform.transform.position + spawnOffset;
            // This was simply here to test ResetCurrentLevel   
            // ResetCurrentLevel(curLevel);
        }
    }
    
    // When a player dies all spheres in that level should respawn to make the level possible
    // should loop through all sphers and make them active
    // should be called when the player dies to repawn them
    public void ResetCurrentLevel()
    {
        newStartingPlatform.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        foreach (Transform child in curLevel.transform)
        {
            if (child.gameObject.tag == "Sphere")
            {
                child.gameObject.SetActive(true);
            }
        }
        
        // Respawns the player at the current spawn location
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.transform.position = spawnLocation;

    }
    // current Measure of death for player is if they fall a certain distanec below the platform
    // for the level they are currently on. This call can later be made when they hit the water
    public void checkPlayerHeight() {
        if (player.transform.position.y < (spawnLocation.y /*- spawnOffset.y - deathOffset*/))
        {
            player.transform.GetComponent<Rigidbody>().freezeRotation = true;
            ResetCurrentLevel();
            //player.transform.GetComponent<Rigidbody>().freezeRotation = false;
        } 
    }
}
