 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpHeight = 7f;
    public bool isGrounded = true;
    
    private Rigidbody rb;
    public ParticleSystem sphereParticles;
    private LevelController levelController;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
    }
    
    void Update()
    {
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if(other.gameObject.tag == "Finish")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().restartGame();
        }
        if(other.gameObject.tag == "Water")
        {
            levelController.ResetCurrentLevel();
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Sphere")
        {
            GameObject GM = GameObject.Find("GrappleManager");

            // This if statement ensures that you actually make it to the sphere you grapple to
            // otherwise you would be brought out of your grapple by any spheres in the way which
            // feels misleading.
            if(other.gameObject.name == GM.GetComponent<DrawLines>().getCurrentGrapplingTarget())
            {
                GM.GetComponent<DrawLines>().stopGrappling();
                other.gameObject.SetActive(false);
                ParticleSystem ps = GameObject.Instantiate(sphereParticles, other.gameObject.transform.position, other.gameObject.transform.rotation);
                Destroy(ps.gameObject, 1f);
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}