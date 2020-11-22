using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using Random = UnityEngine.Random;

namespace SheepCounting
{
    public class SheepMovement : MonoBehaviour
    {
        // Start is called before the first frame update

        //float xPos;
        //float yPos;
        //float speed = 0.010F;
        //double dir = 0;
        //System.Random r = new System.Random();

        //Child sprite and sprite renderer for prefab
        public GameObject sprite;
        public SpriteRenderer spriteRenderer;

        //Birth/death script for prefab
        public PoofScript poofScript;

        //Wandering method determines where the sheep will go next
        public bool wandering;          //boolean for when the sheep is choosing a new destination
        float wanderlength;             //how far the sheep will go this time
        public float wanderlengthmin;   //the minimum wandering length
        public float wanderlengthmax;   //the maximum wandering length
        int wanderdirection;            //the sheep's direction for wandering (0 - 360 degrees)
        Vector3 target;                 //target xyz coordinates
        Vector3 start;                  //xyz coordinates of sheep when selecting a new destination
        public Tilemap tileMap;         //the level tilemap
        public Grid grid;               //the level grid

        //Moving method controls sheep motion towards new destination
        bool moving;                    //boolean for when sheep is in transit to new destination
        public float speed;             //how fast the sheep goes (folded into lerp function) 
        float t;                        //lerp speed modifier (affected by speed value)

        //Waiting method makes sheep spend time in current spot before choosing a new destination
        public bool waiting;            //boolean for sheep to wait between reaching a destination and selecting a new one
        float stopwatch;                //empty float for tracking time sheep waits in the waiting method
        float wandertimer;              //how long the sheep will wait before moving again, in seconds
        float wandertimermax;           //the upper limit for waiting

        void Start()
        {
            //xPos = transform.position.x;
            //yPos = transform.position.y;
            //newDir();

            //Assign tilemap and grid from level
            tileMap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
            grid = GameObject.Find("Grid").GetComponent<Grid>();

            //Start sheep in wandering mode to start movement loop
            //wandering = true;
            //print("script start");

            //Start entity with no mode active, to wait for birth animation to play
            wandering = false;
            moving = false;
            waiting = false;

            //Assign prefab's poof script
            poofScript = GetComponent<PoofScript>();

            //Initializing variables (adjust as necessary)
            wanderlengthmin = 0.5f;
            wanderlengthmax = 4f;
            speed = 0.2f;
            wandertimermax = 4;

            //Set initial stopwatch for first waiting phase
            stopwatch = 0;

            //Set the timer for the waiting method with altered values for first waiting
            wandertimer = Random.Range(0.5f, 1.25f);

            //Assign child object's sprite renderer
            sprite = transform.GetChild(0).gameObject;
            spriteRenderer = sprite.GetComponent<SpriteRenderer>();

        }

        // Update is called once per frame
        void Update()
        {
            //Wandering function
            if (wandering == true)
            {
                //print("wander");

                //Set start vector
                start = new Vector3(transform.position.x, transform.position.y, 0);

                //Randomly select a direction and distance to travel
                wanderdirection = Random.Range(0, 360);
                wanderlength = Random.Range(wanderlengthmin, wanderlengthmax);

                //Calculate the target vector using the above values
                target = new Vector3(start.x + (Mathf.Cos(wanderdirection) * wanderlength), start.y + (Mathf.Sin(wanderdirection) * wanderlength), 0);

                //print(start);
                //print(wanderdirection);
                //print(wanderlength);
                //print(target);

                //Convert target position to grid values
                Vector3Int gridpos = grid.WorldToCell(target);

                //print(gridpos);

                //Check if target is on the tilemap
                //If true, end wandering mode and being movement mode
                //If false, repeat wandering method on next frame
                if (tileMap.HasTile(gridpos))
                {
                    //print("has tile true");
                    wandering = false;
                    moving = true;
                    //print("begin movement");

                    //Also flip sprite if moving right, for R E A L I S M
                    if (target.x > transform.position.x)
                    {
                        spriteRenderer.flipX = true;
                    }

                    else
                    {
                        spriteRenderer.flipX = false;
                    }
                }
            }

            //Movement method
            if (moving == true && Time.timeScale == 1)
            {
                //print("move");
                
                //Lerp the transform to the destination using the speed modifier "t"
                transform.position = Vector3.Lerp(transform.position, target, t);
                t += speed * Time.deltaTime;

                //When the sheep has practically reached the destination, end movement and call the waiting method
                if (Vector3.Distance(target, transform.position) < (0.05 * Vector3.Distance(target, start)))
                {
                    //print("movement complete");
                    moving = false;
                    waiting = true;
                    //Set the timer for the waiting method
                    wandertimer = Random.Range(0, wandertimermax);
                    //Initialize the stopwatch variable
                    stopwatch = 0;
                }
            }

            //Waiting method to force sheep to wait between movements
            if (waiting == true)
            {
                //print("wait");

                //Increment stopwatch
                stopwatch += Time.deltaTime;

                //When stopwatch hits target, start the movement loop over again with the wandering method
                if (stopwatch >= wandertimer)
                {
                    waiting = false;
                    wandering = true;
                    //print("begin wander");
                }
            }
            //xPos = (float)(Math.Cos(dir * (Math.PI / 180.0))*speed);
            //yPos = (float)(Math.Sin(dir * (Math.PI / 180.0))*speed);
            //transform.position = new Vector3(transform.position.x + xPos, transform.position.y + yPos, (float)-0.10);
            //if (r.Next(100) > 98) {
            //    newDir();
            //}
        }

        //    void newDir()
        //    {
        //        dir = r.Next(360);
        //    }
    }
}
