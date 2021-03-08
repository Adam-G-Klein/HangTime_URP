 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpHeight = 7f;
    public bool isGrounded = true;
    
    private Rigidbody rb;
    public ParticleSystem sphereParticles;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Sphere")
        {
            GameObject GM = GameObject.Find("GrappleManager");
            GM.GetComponent<DrawLines>().stopGrappling();
            other.gameObject.SetActive(false);
            ParticleSystem ps = GameObject.Instantiate(sphereParticles, other.gameObject.transform.position, other.gameObject.transform.rotation);
            Destroy(ps.gameObject, 1f);
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