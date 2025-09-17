using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketScript : MonoBehaviour
{
    public SpriteRenderer spriterenderer;
    CharacterController controller;
    float xspeed;
    float yspeed;
    public float accelerationx;
    public float accelerationy;
    Vector3 velocity = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xspeed = 0;
        yspeed = 0;
        accelerationx = 0;
        accelerationy = 0;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z != 0)
        {
            controller.enabled = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            controller.enabled = true;
        }

        float val = Input.GetAxis("Horizontal");
        float val2 = Input.GetAxis("Vertical");
        if (val == 0)
        {
            xspeed = 0;
        }
        else if (val > 0)
        {
            Console.WriteLine("fnkee");
            //adding Time.deltaTime so it updates in accordance to seconds rather than per frame
            xspeed += accelerationx * Time.deltaTime;
            if (xspeed >= 6)
                xspeed = 6;
        }
        else if (val < 0)
        {
            yspeed -= accelerationy * Time.deltaTime;
            if (yspeed <= -6)
                yspeed = -6;
        }
        if (val2 == 0)
        {
            yspeed = 0;
        }
        else if (val2 > 0)
        {
            //adding Time.deltaTime so it updates in accordance to seconds rather than per frame
            yspeed += accelerationy * Time.deltaTime;
            if (yspeed >= 6)
                yspeed = 6;
        }
        else if (val2 < 0)
        {
            yspeed -= accelerationy * Time.deltaTime;
            if (yspeed <= -6)
                yspeed = -6;
        }

        velocity = new Vector3(xspeed, yspeed, 0);

        controller.enabled = true;
        controller.Move(velocity * Time.deltaTime);  //move the controller	
    }
}
