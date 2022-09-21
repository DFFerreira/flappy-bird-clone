using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 1;
    [SerializeField] private float gravity = 1;
    [SerializeField] private float rotateSpeed = 1;
    [SerializeField] private float flapPower = 1;
    [SerializeField] private float flapAngleUp = 60;

    private float zRot;
    private Vector3 velocity;

    public Vector3 Velocity => velocity;

    private PlayerInput playerInput;

    private bool isDead = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        ProcessInput();
        ModifyVelocity();
        RotateDown();
                
        transform.position += velocity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, zRot);

        
    }

    private void ProcessInput()
    {
        if (playerInput.Tap())
        {
            velocity.y = flapPower;
            zRot = flapAngleUp;
        }        
    }

    private void ModifyVelocity()
    {
        velocity.x = forwardSpeed;
        velocity.y -= gravity * Time.deltaTime;
    }

    private void RotateDown()
    {
        if(velocity.y < 0)
        {
            zRot -= rotateSpeed * Time.deltaTime;
            zRot = Mathf.Clamp(zRot, -90, 60);
        }
    }

    public void Die()
    {
        if(!isDead)
        {
            isDead = true;
            forwardSpeed = 0;
            playerInput.enabled = false;
            flapPower = 0;
            velocity = Vector3.zero;
            PlayerAnimController playerAnim = GetComponent<PlayerAnimController>();
            if (playerAnim != null)
            {
                playerAnim.Die();
            }

            StartCoroutine(WaitAndReloadGame());
        }
    }

    private IEnumerator WaitAndReloadGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Main");
    }
}
