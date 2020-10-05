using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    readonly float minSpeed = 0.2f;

    public float MaxSpeed = 2f;
    public float Speed;
    public Vector2 Direction;

    // Start is called before the first frame update
    void Start()
    {
        // Set Initial Speed to mid-range from mid to max
        // Simplification of: min + (max-min)/2
        // Simplification of: (2min + max - min ) / 2
        Speed = (minSpeed + MaxSpeed) / 2;
    }

    // Update is called once per frame
    void Update()
    {

        //InputHandler();
        ProperInputHandler();



        // Direction.normalized will give us a vector in the direction
        // specified but always with magnitude 1
        Vector2 movement = Direction.normalized * Speed * Time.deltaTime;

        /*
        transform.position = new Vector2(
            transform.position.x + movement.x,
            transform.position.y + movement.y);
        */
        transform.Translate(movement);

        
    }


    void ProperInputHandler()
    {
        /*-------*
         * MOUSE *
         *-------*/
        // ScrollWheel Up   = Positive Value
        // ScrollWheel Down = Negative Value 
        float scrollWheelDelta = Input.GetAxis("Mouse ScrollWheel");
        if ( scrollWheelDelta != 0f)
        {
            MaxSpeed += scrollWheelDelta;
            if (MaxSpeed < minSpeed)   MaxSpeed = minSpeed;
        }


        /*----------*
         * KEYBOARD *
         *----------*/
        float horizontalDelta = Input.GetAxis("Horizontal");
        float verticalDelta = Input.GetAxis("Vertical");

        // Simulate acceleration taking advantage of axis value going incrementally from 0 to -1 and 0 to 1 
        Speed = math.abs(horizontalDelta) > math.abs(verticalDelta) ? math.abs(horizontalDelta) : math.abs(verticalDelta);
        Speed *= MaxSpeed;

        Direction = new Vector2(horizontalDelta, verticalDelta);
    }

    void InputHandler()
    {
        /*-------*
         * MOUSE *
         *-------*/
        // Right Click = Speed Up
        if (Input.GetMouseButtonDown(1))
        {
            Speed += 0.2f;
            Debug.Log("Speed Up!");
            if (Speed > MaxSpeed)
            {
                Speed = MaxSpeed;
                Debug.Log("Max Speed Reached!");
            }
        }

        // Left Click = Speed Down
        if (Input.GetMouseButtonDown(0))
        {
            if (Speed > minSpeed)
            {
                Speed -= 0.2f;
                Debug.Log("Speed Down!");
                if (Speed < minSpeed)
                {
                    Speed = minSpeed;
                }
            }
        }


        /*----------*
         * KEYBOARD *
         *----------*/
        
        // Keyboard Left Movement Detection
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Direction = new Vector2(-1, Direction.y);
        }

        // Keyboard Left Stop Detection
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Direction = new Vector2(0, Direction.y);
        }

        // Keyboard Right Movement Detection
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Direction = new Vector2(1, Direction.y);
        }

        // Keyboard Right Stop Detection
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Direction = new Vector2(0, Direction.y);
        }


        // Keyboard Up Movement Detection
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Direction = new Vector2(Direction.x, 1);
        }

        // Keyboard Up Stop Detection
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            Direction = new Vector2(Direction.x, 0);
        }

        // Keyboard Down Movement Detection
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Direction = new Vector2(Direction.x, -1);
        }

        // Keyboard Down Stop Detection
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Direction = new Vector2(Direction.x, 0);
        }
        
    }
}
