using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SheepCounting
{
    public class CursorScript : MonoBehaviour
    {
        //Textures for the idle cursor and click cursor
        public Texture2D idlecursor;
        public Texture2D clickcursor;

        //Audio source for button click sound effects
        public AudioSource sound;
        public AudioClip clickdown;
        public AudioClip clickup;

        //Boolean for when a button is being clicked
        bool buttonclick;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.SetCursor(idlecursor, Vector2.zero, CursorMode.Auto);
            sound = this.gameObject.GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            //When left click is held down, switch to click cursor
            if (Input.GetMouseButton(0))
            {
                Cursor.SetCursor(clickcursor, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(idlecursor, Vector2.zero, CursorMode.Auto);
            }

            //
            if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.tag == "Button")
            {
                sound.PlayOneShot(clickdown);
                buttonclick = true;
            }

            if (Input.GetMouseButtonUp(0) && buttonclick == true)
            {
                sound.PlayOneShot(clickup);
                buttonclick = false;
            }
        }
    }
}
