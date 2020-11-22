using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SheepCounting
{
    public class SheepDestroyer : MonoBehaviour
    {
        //The target object (sheep or otherwise) to be flicked
        public GameObject clickTarget;

        //The target object's destruction script
        public PoofScript script;

        //The spawner object and script that track sheep counts
        public GameObject spawner;
        public Spawner spawnscript;

        public int realSheepYeeted;
        public int fakeSheepYeeted;
        

        // Start is called before the first frame update
        void Start()
        {
            //Assign spawner and spawner script
            spawner = GameObject.Find("Spawner");
            spawnscript = spawner.GetComponent<Spawner>();
        }

        // Update is called once per frame
        void Update()
        {
            



            //When you click while playing
            if (Input.GetMouseButtonDown(0) && Time.timeScale == 1)
            {
                //Throw a raycast from the cursor through the screen
                RaycastHit2D[] hit = Physics2D.LinecastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.ScreenToWorldPoint(Input.mousePosition));

                //If at least one collider was hit
                if (hit.Length > 0)
                {
                    //Sorting algorithm for top rendered sprite (may need to rework in future if it doesn't work properly)
                    int topHit = 0;
                    int preValue = hit[0].transform.GetComponent<SpriteRenderer>().sortingLayerID;

                    for (int arrayID = 1; arrayID < hit.Length; arrayID++)
                    {
                        int tempValue = hit[arrayID].transform.GetComponent<SpriteRenderer>().sortingLayerID;
                        if (tempValue > preValue)
                        {
                            preValue = tempValue;
                            topHit = arrayID;
                        }
                    }
                    //Debug prints
                    //print(topHit);
                    //print(Time.frameCount);

                    //Assign the selected object to the target variable
                    clickTarget = hit[topHit].collider.transform.parent.gameObject;
                    script = clickTarget.GetComponent<PoofScript>();

                    //Check the tag on the deleted object and change the appropriate score in the spawner script
                    //Also, initiate destruction method for target
                    if (script.isDead == false)
                    {
                        if (clickTarget.tag == "Sheep")
                        {
                            script.Poof();
                            spawnscript.realSheepCount -= 1;
                            realSheepYeeted += 1;
                        }

                        if (clickTarget.tag == "Rat" || clickTarget.tag == "Wolf" || clickTarget.tag == "Cat" || clickTarget.tag == "Rock")
                        {
                            script.Poof();
                            spawnscript.fakeSheepCount -= 1;
                            fakeSheepYeeted += 1;
                        }
                    }
                }
            }
        }
    }
}
