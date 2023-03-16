using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_Text StopWatchDisplay;

    private Rigidbody rb;

    public float forwardForce;
    public float sideForce;
    public float jumpForce;

    private bool offGround = false;

    private bool timerActive = false;

    public int energyLevel;

    private bool gameOver = false;

    private TimeSpan stopWatchTime;
    private float timeTrack = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y <= -10 && !gameOver)
        {
            Debug.Log("Game Over!");
            gameOver = true;
            return;
        }
        if (timerActive){
            timeTrack = timeTrack + Time.deltaTime;
            stopWatchTime = TimeSpan.FromSeconds(timeTrack);
            StopWatchDisplay.text = stopWatchTime.ToString(@"mm\:ss\:fff");

        }

    }

    void FixedUpdate()
    {
        if(Input.GetKey("w")){
            rb.AddForce(0,0,forwardForce * Time.deltaTime);
        }
        if(Input.GetKey("z")){
            rb.AddForce(0,0,-forwardForce * Time.deltaTime);
        }
        if(Input.GetKey("d")){
            rb.AddForce(sideForce * Time.deltaTime,0,0);
        }
        if(Input.GetKey("a")){
            rb.AddForce(-sideForce * Time.deltaTime,0,0);
        }
        if (Input.GetKey("j") && offGround==false){
            offGround = true;
            rb.velocity = Vector3.up * jumpForce;

        }
    }

    void OnCollisionEnter(Collision collisionInfo){

        if (collisionInfo.collider.tag == "Obstacle"){
            energyLevel--;
            if(energyLevel <= 0 && !gameOver){
                Debug.Log("Game over!");
                gameOver = true;
                return;
            }
            else if (energyLevel > 0){
                Debug.Log(energyLevel);
            }

            
        }

    }

    void OnCollisionStay(Collision collisionInfo){
        
        if(collisionInfo.collider.name == "Ground"){
            offGround = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Start"){
            timerActive = true;
            Debug.Log("Stopwatch activated!");
        }
        if (collider.name == "End"){
            timerActive = false;
            Debug.Log("Stopwatch deactivated!");
        }

    }
}
