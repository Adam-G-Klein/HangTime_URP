using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour
{
    [SerializeField]
    private GameObject LineGeneratorPrefab;
    private AudioManager audioManager;
    public Transform PlayerTransform;
    public Transform RightHand;
    public Transform CameraTransform;
    public Transform reticalPosition;
    public int rayLength;
    private GameObject newLineGen;
    private LineRenderer lRend;
    public float range = 30f;
    private GameObject lastObjectHit = null;
    private Vector3 grapplePoint;
    private Vector3 grappleDir;
    public float aimAssist = 2f;
    private bool grappling = false;
    private Rigidbody PlayerRB;
    public Material[] SelectedMaterial;
    public Material[] UnselectedMaterial;
    private LevelController levelController;
    private bool gameStarted = false;
    private GameObject grappleTarget;
    // Start is called before the first frame update
    void Start()
    {
        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        //SpawnLineGenerator();
        gameStarted = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(gameStarted)
        {
            Vector3 retDir = reticalPosition.position - CameraTransform.position;
            retDir.Normalize();
            retDir = retDir * rayLength;

            DetectPossibleGrapple(retDir);
            if(Input.GetButtonDown("Fire1") && lastObjectHit)
            {
                audioManager.grappleSounds();
                Shoot();
                UseGrappleForce();
                grappling = true;
            }
            if(grappling)
            {
                lRend.SetPosition(0, RightHand.position);
            }
        }
    }
    private void DetectPossibleGrapple(Vector3 retDir)
    {
        RaycastHit[] hit;
        // Does a Raycast first and then if no objects are hit it attempst to do a 
        // spherecast instead. This allows for precise aim when a player is point directly
        // at an object as well as aim assit provided by the sphere cast.
        bool somethingHit = false;
        hit = Physics.RaycastAll(CameraTransform.position, retDir, range);
        if(hit.Length == 0){
            hit = Physics.SphereCastAll(CameraTransform.position, aimAssist, retDir, range);
        }
        if(hit != null)
        //if(Physics.SphereCast(CameraTransform.position, .5f, retDir, out hit, range))
        {
            foreach (RaycastHit rHit in hit)
            {
                if (!somethingHit)
                {
                    Transform hitTransform = rHit.transform;
                    if (hitTransform.tag == "Sphere")
                    {
                        somethingHit = true;
                        //Debug.Log(hit.transform.name);
                        GameObject curObjectHit = hitTransform.gameObject;
                        if (lastObjectHit)
                        {
                            if (lastObjectHit.name != curObjectHit.name)
                            {
                                lastObjectHit.GetComponent<MeshRenderer>().materials = UnselectedMaterial;
                            }
                        }
                        lastObjectHit = curObjectHit;
                        grapplePoint = rHit.point;
                        hitTransform.GetComponent<MeshRenderer>().materials = SelectedMaterial;
                    }
                }
            }

        }
        // will be run if none of the rays hit
        if(!somethingHit){
            if(lastObjectHit)
            {
                lastObjectHit.GetComponent<MeshRenderer>().materials = UnselectedMaterial;
                lastObjectHit = null;
            }
        }
    }
    private void Shoot()
    {
        grappleTarget = lastObjectHit;
        levelController.CheckLevelUpdate(lastObjectHit);
        if(newLineGen)
        {
            Destroy(newLineGen);
        }
        newLineGen = Instantiate(LineGeneratorPrefab);
        lRend = newLineGen.GetComponent<LineRenderer>();

        lRend.SetPosition(0, RightHand.position);
        lRend.SetPosition(1, grapplePoint);
        grappleDir = grapplePoint - PlayerTransform.position;
        Debug.Log(grapplePoint);
    }
    private void UseGrapple()
    {
        //float grappleDistance = grapplePoint.magnitude;
        float step = 1f * Time.deltaTime;
        //Debug.Log(step);
        PlayerTransform.position += grappleDir * step;
        lRend.SetPosition(0, PlayerTransform.position);
    }
    private void UseGrappleForce()
    {
        PlayerRB = PlayerTransform.GetComponent<Rigidbody>();
        PlayerRB.useGravity = false;
        PlayerRB.velocity = Vector3.zero;
        PlayerRB.AddForce(grappleDir.normalized * 10, ForceMode.Impulse);
    }
    public void stopGrappling()
    {
        PlayerRB.useGravity = true;
        PlayerRB.AddForce(grappleDir.normalized * 4, ForceMode.Impulse);
        //Debug.Log("Stopping Grappling");
        grappling = false;
        Destroy(newLineGen);
    }
    public bool getGameStarted()
    {
        return gameStarted;
    }
    public void setGameStarted(bool status)
    {
        gameStarted = status;
    }
    public void setAimAssist(float value)
    {
        aimAssist = value;
    }
    public string getCurrentGrapplingTarget()
    {
        return grappleTarget.name;
    }
}