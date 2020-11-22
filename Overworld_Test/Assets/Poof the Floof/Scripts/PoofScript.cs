using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SheepCounting
{
    public class PoofScript : MonoBehaviour
    {
        //Boolean for when to begin death animation
        public bool isDead;

        //Boolean for when prefab is born
        public bool isBorn;

        //Bool for highlighting sprite
        public bool isHighlight;

        //The child object's sprite renderer for animating
        public GameObject sprite;
        public SpriteRenderer spriteRenderer;

        //Object's movement script to assign initial movement booleans
        public SheepMovement sheepMovement;

        //Iterator variable to track progress of animations
        int oldindex;

        //Frame array to hold frames of death animation
        public Sprite[] frames;

        //Main sprite for entity to use during play
        public Sprite mainSprite;

        //FPS (should be changeable in editor)
        public double framesPerSecond = 10.0;

        //Timer to track progress of animation
        float timer;

        //script for SFX
        //public SfxDestroy soundscript;

        //Audio source and clips for death sound effects
        AudioSource source;
        public AudioClip poofclip;
        public AudioClip animalclip;
        public Color newColor;
        public Color original; 
      

        // Start is called before the first frame update
        void Start()
        {
            //Set isDead to false
            isDead = false;

            //Set isBorn to true, since prefab has just been created
            isBorn = true;

            //Set timer to 0 for birth animation
            timer = 0;

            //Assign child object's sprite renderer
            sprite = transform.GetChild(0).gameObject;
            spriteRenderer = sprite.GetComponent<SpriteRenderer>();

            newColor = spriteRenderer.color; 
            //Assign sheep movement script
            sheepMovement = GetComponent<SheepMovement>();

            //Assign sound script
            //soundscript = GetComponent<SfxDestroy>();
            //animal = GetComponent<AudioSource>()[0];
            //poof = GetComponent<AudioSource>()[1];
            source = GetComponent<AudioSource>();

            //Set oldindex to 2 for birthing animation
            oldindex = 2;
        }

        // Update is called once per frame
        void Update()
        {
            //Only do death animation if death was triggered
            if (isDead == true)
            {
                //Iterate the timer
                timer += Time.deltaTime;

                //Calculate the index of the sprite array for the animation
                int index = (int)(timer * framesPerSecond) % frames.Length;

                //print(index);

                //If the current index is 0 or the next integer up from 0
                if (index >= oldindex)
                {
                    //Render the current frame of the animation
                    spriteRenderer.sprite = frames[index];

                    //Remember the current index value for the next frame
                    oldindex = index;
                }

                //If the animation would loop back to the beginning on this frame, destroy the prefab
                if (index < oldindex && source.isPlaying)
                {

                    sprite.SetActive(false);
                }
            }

            //Do birth animation when prefab is first created
            if (isBorn == true)
            {
                //Iterate the timer
                timer += Time.deltaTime;

                //Calculate the index of the sprite array for the animation
                int index = (((int)(timer * framesPerSecond) % frames.Length) - 2) * (-1);

                //print(index);

                //If the current index is 0 or the next integer up from 0
                if (index <= oldindex)
                {
                    //Render the current frame of the animation
                    spriteRenderer.sprite = frames[index];

                    //Remember the current index value for the next frame
                    oldindex = index;
                }

                //If the animation would loop back to the beginning on this frame, set "real" sprite and end animation
                //Also set entity to start movement script by starting entity's initial waiting phase
                if (index > oldindex)
                {
                    spriteRenderer.sprite = mainSprite;
                    isBorn = false;
                    sheepMovement.waiting = true;
                    //print("begin waiting");
                }


            }
        }

        public void Poof()
        {
            //print("Poof!");

            //Set timer and oldindex for playing animation through
            timer = 0;
            oldindex = 0;

            //plays SFX
            //soundscript.Sound();
            StartCoroutine(Sound());

            //Activate isDead to begin death animation
            isDead = true;

            //Cancel isBorn if prefab is still doing birth animation
            isBorn = false;
        }

        private IEnumerator Sound()
        {
            source.PlayOneShot(poofclip);
            source.PlayOneShot(animalclip);
            yield return new WaitForSeconds(animalclip.length);
            Destroy(this.gameObject);
            Debug.Log("Destroy is called");

        }

        void OnMouseEnter()
        {
            spriteRenderer.color = original;
            print("mouse entered");
        }

        void OnMouseExit()
        {
            spriteRenderer.color = newColor;
        }
    }

}
