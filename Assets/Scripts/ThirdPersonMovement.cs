using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public PhotonView pView;

    private Joystick moveStick;
    private Joystick camStick;
    private Vector3 velocity;

    private CinemachineFreeLook cineCam;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [SerializeField] private float gravity;
    
    // Update is called once per frame
    void Update()
    {
        if (pView.IsMine)
        {
            Move();
        }
    }
    
    private void Move()
    {       

        //Y-Axis
        float horizontal = moveStick.Horizontal;
        //Z-Axis
        float vertical = moveStick.Vertical;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cineCam.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        if (direction.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(moveDirection.normalized * speed * Time.deltaTime);

        }

        //Cinemachine cam movement
        cineCam.m_XAxis.Value = camStick.Horizontal * 100 * Time.deltaTime;
        cineCam.m_YAxis.Value += -camStick.Vertical * Time.deltaTime;


        //Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void SetJoysticks(GameObject camera)
    {
        Joystick[] tempJoystickList = camera.GetComponentsInChildren<Joystick>();
        foreach (Joystick temp in tempJoystickList)
        {
            if (temp.tag == "Joystick Movement")
                moveStick = temp;
            else if (temp.tag == "Joystick Camera")
                camStick = temp;
        }

        cineCam = camera.GetComponentInChildren<CinemachineFreeLook>();
        cineCam.LookAt = GameObject.Find("Head").transform;
        cineCam.Follow = transform;

    }
}

