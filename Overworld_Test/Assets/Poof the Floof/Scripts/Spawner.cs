using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using Random = UnityEngine.Random;


namespace SheepCounting
{
    public class Spawner : MonoBehaviour
    {


        //prefabs to instantiate
        public GameObject realSheepPrefab, fakeSheepPrefab;
        //number of real and nonreal sheep spawned
        public int realSheepCount = 0, fakeSheepCount = 0;

        //spawn prefabs once per 2 seconds
        public float spawnRate = 2f;
        //variable to adjust spawn rate increase
        //increase happens linearly, with every spawn
        public float spawnRateChange = 0.05f;
        //variable to set next spawn time
        private float nextSpawn = 0f;
        public float maxSpawnRate = 0.75f;

        //general count variables, applied to all regions
        //min/max range for randomly chosen spawn count
        //NOTE: can be tweaked to have region specific counters
        public int minCount = 1;
        public int maxCount = 4;
        //linearly increments spawn count (range)
        //NOTE: currently increases min and max Count 
        public int spawnCountChange = 1;

        //number of spawn cycles before spawn count increases;
        public int countChangeFreq = 10;
        //internal tracker, # rounds before count change
        private int countChangeTimer;
        

        
        //random var placeholder, determines which region to spawn entity in 
        private int whereToSpawn;

        //vars for the tileMap corodiantes, key points as ints
        private int xMin;
        private int xMax;
        private int yMin;
        private int yMax;

        //vars for the tileMap corners, key points as Vectors
        private Vector3Int botCorner;
        private Vector3Int topCorner;
        private Vector3Int leftCorner;
        private Vector3Int rightCorner;
        //tupils for defining each of the 5 spawn regions
        private (Vector3Int min, Vector3Int max) southEast;// = (botCorner, leftCorner);
        private (Vector3Int min, Vector3Int max) southWest;// = (botCorner, rightCorner);
        private (Vector3Int min, Vector3Int max) northEast;// = (leftCorner, topCorner);
        private (Vector3Int min, Vector3Int max) northWest;// = (rightCOrner, topCorner);
        private (Vector3Int min, Vector3Int max) entireField;// = (botCorner, topCorner);






       
       
        
        //var holder for world spawn coordinates
        private Vector3 spawnPoint;

        //creates a list of entities that can be spawned
        public List<GameObject> spawnPool;
        //variable for the level tilemap
        public Tilemap tileMap;

        
        private bool addSpawnPool;


        // Start is called before the first frame update
        void Start()
        {
            
            addSpawnPool = false;

            //Assign tilemap from level
            GameObject tileMapObj = GameObject.Find("Tilemap");
            tileMap = tileMapObj.GetComponent<Tilemap>();      
            
            //adjusts boundaries if needed
            tileMap.CompressBounds();

           
          
            
            //deconstructs tilemap borders creating coordinate frame
            xMin = tileMap.cellBounds.xMin;
            xMax = tileMap.cellBounds.xMax - 1; //side note: why was it necessary to subtract one?
            yMin = tileMap.cellBounds.yMin;
            yMax = tileMap.cellBounds.yMax - 1;
            //zMIn and zMax can also be defined
            

        

            //sets key points on Map
            botCorner.Set(xMin, yMin, 0);
            leftCorner.Set(xMin, yMax, 0);
            rightCorner.Set(xMax, yMin, 0);
            topCorner.Set(xMax, yMax, 0);


            //defines regions bound within two vectors
            //in the form of tupils
            //NOTE: can be rewritten as classes with independent properties
            //(e.g. 4 corners, counter, timer, spawnfrequency
            southEast = (botCorner, leftCorner);
            southWest = (botCorner, rightCorner);
            northEast = (leftCorner, topCorner);
            northWest = (rightCorner, topCorner);
            entireField = (botCorner, topCorner);

            //sets internal round tracker to preset value
            countChangeTimer = countChangeFreq;

           







            //starts game with realSheepCount number of sheep
            //spawns them randomly across the field
            for (int i = 0; i < realSheepCount; i++){
                Instantiate(realSheepPrefab, SpawnPoint(entireField), Quaternion.identity);
            }
            //starts game with fakeSheepCount number of fake sheep
            //spawns them randomly across the field
            for (int i = 0; i < fakeSheepCount; i++){
                Instantiate(fakeSheepPrefab, SpawnPoint(entireField), Quaternion.identity);
            }
        }





        // Update is called once per frame
        void Update()
        {

            //if each region spawns independently, can set 4 if statements with 4 independent timers
            if (Time.time > nextSpawn) //if time has come
            {
                
                //for popping array of spawner to include or get rid of real sheep odds to spawn
                if (realSheepCount == 8)
                {
                    spawnPool.Remove(realSheepPrefab);
                    addSpawnPool = true;
                }
                else if (realSheepCount != 8 && addSpawnPool == true)
                {
                    //1 out of 3 chance to pop the real sheep prefab back onto spawner list, last num exclusive
                    if (Random.Range(1, 4) == 3)
                    {
                        spawnPool.Add(realSheepPrefab);
                        addSpawnPool = false;
                    }
                }

                //triggers random region 
                //spawns on one region at a time
                //NOTE: can be modified to trigger a random number of regions simultaneously
                SpawnScript();
                //can call spawn methods directly if spawning from all sides
                /*
                SpawnSouthEast();
                SpawnSouthWest();
                SpawnNorthEast();
                SpawnNorthWest();
                */
               

                //increments spawn rate with every spawn, up to a maximum spawn rate
                if (spawnRate > maxSpawnRate)
                {
                    spawnRate -= spawnRateChange;
                }

                //comment out increasing min/max number of possible spawning sheep, stop it from getting out of control
                // if (countChangeTimer > 1)
                // {
                //     //clocks the spawn round
                //     countChangeTimer--;
                // }
                // else
                // {
                //     //increases max spawn count
                //     maxCount += spawnCountChange;
                //     //increases min spawn count
                //     minCount += spawnCountChange;
                //     //resets timer
                //     countChangeTimer = countChangeFreq;
                // }

                //set next spawn time
                nextSpawn = Time.time + spawnRate;
                print(spawnRate);
            }
        }

        //generates world coordinates for entity spawn
         public Vector3 SpawnPoint ((Vector3Int min, Vector3Int max) input) 
             {

                int x;
                int y;
                Vector3Int cell = new Vector3Int();

                x = Random.Range (input.min.x, input.max.x);
                y = Random.Range (input.min.y, input.max.y);
                cell.Set(x, y, 0);
                spawnPoint = tileMap.CellToWorld(cell);
                return spawnPoint;
            }
            
        //determines object to be spawned
        //updates ratio meter
         public GameObject SpawnObject() 
            {
                int randomItem = 0;
                GameObject toSpawn = realSheepPrefab;

                randomItem = Random.Range(0, spawnPool.Count);
                toSpawn = spawnPool[randomItem];

            if (toSpawn == realSheepPrefab) { realSheepCount++; }
            else { fakeSheepCount++; }
                return toSpawn;
            
            }
           

        //function for spawning entity on SE border
            public void SpawnSouthEast()
            {
            
            int count = Random.Range(minCount, maxCount);
                for (int i=0; i<count; i++)
                {
                Instantiate(SpawnObject(), SpawnPoint(southEast), Quaternion.identity);
                }
            }
        
        //function for spawning entity on SW border
            public void SpawnSouthWest()
            {
            
            int count = Random.Range(minCount, maxCount);
                for (int i=0; i<count; i++) {
                Instantiate(SpawnObject(), SpawnPoint(southWest), Quaternion.identity);
                }
            }

        //function for spawning entity on NE border
            public void SpawnNorthEast()
            {
           
            int count = Random.Range(minCount, maxCount);
                for (int i=0; i<count; i++) {
                Instantiate(SpawnObject(), SpawnPoint(northEast), Quaternion.identity);
                }
            }

        //function for spawning entity on NW border
            public void SpawnNorthWest()
            {
            
            int count = Random.Range(minCount, maxCount);
                for (int i=0; i<count; i++) {
                Instantiate(SpawnObject(), SpawnPoint(northWest), Quaternion.identity);
                }
            }


        public void SpawnScript()
        {
            int count = Random.Range(minCount, maxCount);
            for (int i=0; i<count; i++)
            {
                whereToSpawn = Random.Range(1, 5);

                switch (whereToSpawn)
                {
                    case 1:
                        SpawnSouthEast();
                        break;
                    case 2:
                        SpawnNorthEast();
                        break;
                    case 3:
                        SpawnSouthWest();
                        break;
                    case 4:
                        SpawnNorthWest();
                        break;
                }

            }
        }






        //==================================================================
        //Gregory's Notes and Drafts

        /* TO CHECK:
         
         * spawn rate
         * spawn rate change
         * spawn count
         * spawn count change
         * how many sides active at once? 
         * random or linear increments?
         * stages?
         * spawn ratio?
         * TODO: check if ratio meter works properly; if no entities initialized? 
         * add animations
         * add assets
         * add sound effects
         
         */


        //TODO: OUTLINE 

        //initialize regions? => define min point and max point as vector var - use cell coordinate or world coordinate? 
        //create different methods for each region, generating a spawn point
        //either run all methods in parallel (with an if statement if stages are being implemented)
        //each method running it's own random number generators and cases/controlling its spawning
        //OR if alternating, can put them under a switch
        //OR if simultaneous spawn put them under the same timer

        //is it possible to create objects with their own custom properties? 
        //I.e. have 4 different regions with their own xMin, xMax, yMin, and yMax
        //not going to need it here, though, as all entities will be jumping into the field

        //spawn function(s) will need to be wrapped in a timer+if check to see when to run
        //if functions are to run independently from each other, they need to be wrapped in 4 independent timers
        //if they are to run simultaneously then 1 common timer
        //if only one side is to be triggered at a time, use a switch
        //time variable will control spawn rate
        //initial spawn rate public var, spawn rate change public var? 

        //each function will have a spawnpoint variable, and perhaps a spawn count variable
        //spawn count variable can be determined independently for each function (within function, or outside of function?)
        //or can be a global variable inputted into the function
        //spawn count initial public variable, spawn count change rate public or private var?
        //for each spawned entity, coordinates need to be determined 
        //=> run for loop spawnCount times, run spawn script inside

        /*
         public var: counter paramters: initial values, increments 
                     spawn frequency
                     region parameters/triggers 
        */



        //----------------------------------------------------------


        //initial spawner:

        /*
               whatToSpawn = Random.Range(1, 4); //define random value between 1 and 3 (4 is exclusive) 
               // Debug.Log("Spawned Prefab: " + whatToSpawn); //display its value in console 

               switch (whatToSpawn)
               {
                   case 1:
                       Instantiate(realSheepPrefab, transform.position, Quaternion.identity);
                       realSheepCount++;
                       break;
                   case 2:
                       Instantiate(fakeSheepPrefab, transform.position, Quaternion.identity);
                       fakeSheepCount++;
                       break;
                   case 3:
                       Instantiate(fakeSheepPrefab , transform.position, Quaternion.identity);
                       fakeSheepCount++;
                       break;
               }
               */

        //-----------------------------------------------------
        /*
             Vector3 SpawnPoint (Vector3Int min, Vector3Int max) 
             {
                int x;
                int y;
                Vector3Int cell;
                x = Random.Range (min.x, max.x);
                y = Random.Range (min.y, max.y);
                cell.Set(x, y, 0);
                spawnPoint = tileMap.CellToWorld(cell);
                return spawnPoint;
            }
            
            public GameObject SpawnObject() 
            {
                int randomItem = 0;
                GameObject toSpawn;

                randomItem = Random.Range(0, spawnPool.Count);
                toSpawn = spawnPool[randomItem];
            }

            time - if (Time.time > nextSpawn) {SpawnSE}
            count - for (i=0, i<count, i++) {Instantiate(spawnPoint)}
            ^^^
            SpawnSouthEast()
            {
                count = Random.Range(minCount, maxCount)
                for (i=0, i<count, i++) {
                    Instantiate(SpawnObject(), spawnPoint(southEast.min, southEast.max), Quaternion.identity)
                }
            }

            SpawnSouthWest()
            {
                count = Random.Range(minCount, maxCount)
                for (i=0, i<count, i++) {
                    Instantiate(SpawnObject(), spawnPoint(southWest.min, southWest.max), Quaternion.identity)
                }
            }

            SpawnNorthEeast()
            {
                count = Random.Range(minCount, maxCount)
                for (i=0, i<count, i++) {
                    Instantiate(SpawnObject(), spawnPoint(northEast.min, northEast.max), Quaternion.identity)
                }
            }

            SpawnNorthWest()
            {
                count = Random.Range(minCount, maxCount)
                for (i=0, i<count, i++) {
                    Instantiate(SpawnObject(), spawnPoint(northWest.min, northWest.max), Quaternion.identity)
                }
            }

        */

        //-------------------------------------------------

        //Deleted vars:
        /*
            //determine random cell to spawn entity
            coordinateSouthEast.Set(xMin, Random.Range(yMin, yMax), 0);
            coordinateSouthWest.Set(Random.Range(xMin, xMax), yMin, 0);
            coordinateNorthEast.Set(Random.Range(xMin, xMax), yMax, 0);
            coordinateNorthWest.Set(xMax, Random.Range(yMin, yMax), 0);

            //convert cell coordinate to world coordinate
            //sets spawn point
            //spawnSouthEast = tileMap.CellToWorld(coordinateSouthEast);
            //spawnSouthWest = tileMap.CellToWorld(coordinateSouthWest);
            //spawnNorthEast = tileMap.CellToWorld(coordinateNorthEast);
            //spawnNorthWest = tileMap.CellToWorld(coordinateNorthWest);

        //grid = GameObject.Find("Grid").GetComponent<Grid>();

         //public Tilemap tileMap;          //the level tilemap
        // public Grid grid;               //the level grid
        private Vector3Int pos1;
        private Vector3Int pos2;
        private Vector3Int pos3;
        private Vector3Int pos4;
        private Vector3Int pos5;
        private Vector3Int pos6;
        private Vector3Int pos7;

        private int zMin;
        private int zMax;

        
        private int x;
        private int y;
        private int z;
             */

        /*
        private Vector3 spawnSouthEast;
        private Vector3 spawnSouthWest;
        private Vector3 spawnNorthEast;
        private Vector3 spawnNorthWest;
        private Vector3Int coordinateSouthEast;  //between (xMin, yMin, 0)[botCorner] and (xMin, yMax, 0)[leftCorner]
        private Vector3Int coordinateSouthWest;  //between (xMin, yMin, 0)[botCorner] and (xMax, yMin, 0)[rightCorner]
        private Vector3Int coordinateNorthEast;  //between (xMin, yMax, 0)[leftCorner] and (xMax, yMax, 0)[topCorner]
        private Vector3Int coordinateNorthWest;  //between (xMax, yMin, 0)[rightCorner] and (xMax, yMax, 0)[topCorner]
          //private int whatToSpawn;
        */

        //Deleted Code:


        /*
                //Assign tilemap and grid from level
                //GameObject tileMap = GameObject.Find("TileMap").GetComponent<Tilemap>();
                GameObject tilemapObj = GameObject.Find("Tilemap");
                Tilemap tileMap = tilemapObj.GetComponent<Tilemap>();
                //grid = GameObject.Find("Grid").GetComponent<Grid>();
                TileMap.CompressBounds();
                Debug.Log("TileMap cellBounds output: " + TileMap.cellBounds);
                Vector3Int posBottom = TileMap.cellBounds.position;
                Debug.Log("TileMap cellBounds position output: " + TileMap.cellBounds.position);
                Debug.Log("TileMap cellBounds position var: " + posBottom);
                Debug.Log("PosBottom has cell: " + TileMap.HasTile(posBottom));
                Debug.Log("TileMap cellBounds posBottom cell: " + TileMap.GetTile(posBottom).ToString());
                x = TileMap.cellBounds.position.x;
                Debug.Log("X pos: " + x);
                y = TileMap.cellBounds.position.y;
                Debug.Log("Y pos: " + y);
                z = TileMap.cellBounds.position.z;
                Debug.Log("Z pos: " + z);

                xMin = TileMap.cellBounds.xMin;
                Debug.Log("xMin pos: " + xMin);
                xMax = TileMap.cellBounds.xMax - 1;
                Debug.Log("xMax pos: " + xMax);
                yMin = TileMap.cellBounds.yMin;
                Debug.Log("yMin pos: " + yMin);
                yMax = TileMap.cellBounds.yMax - 1;
                Debug.Log("yMax pos: " + yMax);
                zMin = TileMap.cellBounds.zMin;
                Debug.Log("zMin pos: " + zMin);
                zMax = TileMap.cellBounds.zMax - 1;
                Debug.Log("zMax pos: " + zMax);

        */

        //from Start
        // x = tileMap.cellBounds.position.x;
        // y = tileMap.cellBounds.position.y;
        // z = tileMap.cellBounds.position.z;

        /*
         //code suggested by Unity
         public Tilemap TileMap { get => tileMap; set => tileMap = value; }
         */
    }
}
