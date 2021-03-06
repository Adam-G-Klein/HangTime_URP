using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCameraController : MonoBehaviour
{

    public Transform PlayerTransform;

    private Vector3 cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = .05f;
    public bool RotateAroundPlayer = true;

    public float MaxRotationSpeed = 4f;
    //public float RotationSpeedMulitplier = 2f;
    public float playerSensitivity = 2f;
    private float angleOfRotation;
    public GameObject grappleManager;
    private bool yAxisInversion = false;
    void Start()
    {
        cameraOffset = transform.position - PlayerTransform.position;
        angleOfRotation = transform.eulerAngles.x;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if(RotateAroundPlayer && getGameStarted())
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            if(!yAxisInversion){
                mouseY *= -1;
            }
            Vector2 mouseMove = new Vector2(mouseX, mouseY);
            //Debug.Log(mouseMove);
            mouseMove = mouseMove.normalized;
            //Debug.Log(mouseMove);
            mouseMove = mouseMove * playerSensitivity;
            
            //Debug.Log(mouseMove);

            Quaternion camTurnAngleX = 
                Quaternion.AngleAxis(mouseMove.x, Vector3.up);

            Quaternion camTurnAngleY =  
                Quaternion.AngleAxis(mouseMove.y, transform.right);
            
            //angleOfRotation += mouseY;
            float angle = transform.eulerAngles.x;
            //Debug.Log("camera eulers: " + transform.eulerAngles.x);
            if((mouseY > 0 && ((angle + mouseY) >= 80) && ((angle + mouseY) < 160)) ||
                (mouseY < 0 && ((angle - mouseY) <= 280) && ((angle - mouseY) > 160)))
            {
                cameraOffset = camTurnAngleX * cameraOffset;

                Vector3 newPos = PlayerTransform.position + cameraOffset;
                
                //newPos = new Vector3(newPos.x, Mathf.Min(newPos.y, 12.5f - PlayerTransform.position.y), newPos.z);
                //transform.position = newPos;

                transform.position = Vector3.Slerp(transform.position, newPos, 1f);
                
                transform.LookAt(PlayerTransform);
            }
            else
            {
                cameraOffset = camTurnAngleX * camTurnAngleY * cameraOffset;

                Vector3 newPos = PlayerTransform.position + cameraOffset;
                
                //newPos = new Vector3(newPos.x, Mathf.Min(newPos.y, 12.5f - PlayerTransform.position.y), newPos.z);
                //transform.position = newPos;                
                transform.position = Vector3.Slerp(transform.position, newPos, 1f);
                transform.LookAt(PlayerTransform);

                //transform.LookAt(PlayerTransform);
            }

        }
    }
    public bool getGameStarted()
    {
        return grappleManager.GetComponent<DrawLines>().getGameStarted();
    }

    public void setSensitivity(float value)
    {
        playerSensitivity = value;
        Debug.Log(playerSensitivity);
    }
    public void setYAxisInversion(bool value){
        yAxisInversion = value;
    }
}
