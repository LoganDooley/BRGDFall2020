using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollideNote : MonoBehaviour
{

    public Collider2D cldr;
    private OnHit currentNote;
    public SheepMove sm;
    public float failAmt = -10;
    public Sprite Smoosh;
    public GameObject player;
    public bool fail = false;
    public int soundType;
    public AudioSource hit1;
    public AudioSource hit2;
    public AudioSource miss;
    public GameObject textBox;
    public Vector2 textPos;
 

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {   

        if (Input.GetKeyDown("up"))
        {
            if(currentNote != null && currentNote.GetHittable() && currentNote.spriteType == 0)
            {
                OnHitSuccess();
            }
            else
            {
                OnHitFail();
            }
        }

        if (Input.GetKeyDown("down"))
        {
            if (currentNote != null && currentNote.GetHittable() && currentNote.spriteType == 1)
            {
                OnHitSuccess();
            }
            else
            {
                OnHitFail();
            }
        }

        if (Input.GetKeyDown("left"))
        {
            if (currentNote != null && currentNote.GetHittable() && currentNote.spriteType == 2)
            {
                OnHitSuccess();
            }
            else
            {
                OnHitFail();
            }
        }

        if (Input.GetKeyDown("right"))
        {
            if (currentNote != null && currentNote.GetHittable() && currentNote.spriteType == 3)
            {
                OnHitSuccess();
            }
            else
            {
                OnHitFail();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        this.currentNote = other.GetComponentInParent<OnHit>();
        this.currentNote.SetHittable(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponentInParent<OnHit>().SetHittable(false);
    }

    private void OnHitSuccess()
    {
        GameObject textInstance;
        this.sm.AddLaunchAmt(currentNote.GetSuccess() / 100.0f);
        this.currentNote.SetHittable(false);
        this.currentNote.GetHit();
        this.hit2.Play();
        textInstance = Instantiate(textBox, textPos, Quaternion.identity);
        textInstance.GetComponent<TextKill>().setType(1);
    }

    private void OnHitFail()
    {
        GameObject textInstance;
        fail = true;
        this.sm.AddLaunchAmt(this.failAmt / 100.0f);
        this.miss.Play();
        textInstance = Instantiate(textBox, textPos, Quaternion.identity);
        textInstance.GetComponent<TextKill>().setType(0);
    }

    public bool getFail(){
        return fail;
    }
    public void setFail(bool failure){
        fail = failure;
    }
}   
