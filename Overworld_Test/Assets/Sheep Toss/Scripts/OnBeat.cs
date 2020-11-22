using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnBeat : MonoBehaviour
{
    public GameObject note;
    public float bpm = 100.0f;
    private float spawnTime;
    public GameObject player;
    public GameObject powerIcon;
    public Sprite Dance1;
    public Sprite Dance2;
    public Sprite Jojo;
    public Sprite Smoosh;
    public Sprite Windup;
    public Sprite Throw;
    private int spriteType = 0;
    private bool spriteToggle = false;
    public Vector2 notePos;
    public int numNotes;
    public int notesOnScreen = 0;
    public bool reset = false;
    private bool handled = false;
    public SheepMove sm;
    public CollideNote cn;
    public SongManage songManager;
    private bool isWindup = false;
    private int beatCount = 1;
    public float startDelay;
    public int tossDistance;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("startGame", startDelay);

    }

    // Update is called once per frame
    void Update()
    {   
        this.reset = this.sm.getHandle();
        if (this.reset) {
            StartCoroutine(Handle());
        }

    }

    void startGame()
    {
        this.spawnTime = 60.0f / bpm;
        InvokeRepeating("everyBeat", this.spawnTime, this.spawnTime);
        songManager.PlaySong();
    }

    void everyBeat()
    {   
        if(beatCount == 1)
        {
            beatCount = 2;
        }
        else if(beatCount == 2)
        {
            beatCount = 1;
        }

        Instantiate(note, notePos, Quaternion.identity);
        notesOnScreen++;

        Invoke("returnToIdle", 1 / 6f);
        this.player.gameObject.GetComponent<Animator>().Play("PlayerBounce");
        this.powerIcon.gameObject.GetComponent<Animator>().Play("PowerIconPulse");

        if (!this.cn.getFail()){
            if (spriteType == 0 && beatCount == 2) {
                this.player.gameObject.GetComponent<SpriteRenderer>().sprite = Dance1;
                spriteType ++;
            } 
              else if (spriteType == 1 && beatCount == 2) {
                this.player.gameObject.GetComponent<SpriteRenderer>().sprite = Dance2;
                spriteType ++;
            } else if (spriteType == 2 && beatCount == 2) {
                this.player.gameObject.GetComponent<SpriteRenderer>().sprite = Jojo;
                spriteType = 0;
            }
        } else {
            this.player.gameObject.GetComponent<SpriteRenderer>().sprite = Smoosh;
            this.cn.setFail(false);
        }

        this.spriteToggle = !this.spriteToggle;
        this.numNotes--;

        if (this.numNotes <= 0) {
            CancelInvoke();
        }
        
    }

    public void returnToIdle()
    {
        this.player.gameObject.GetComponent<Animator>().Play("PlayerIdle");
        this.powerIcon.gameObject.GetComponent<Animator>().Play("PowerIconIdle");
    }

    public void playThrow()
    {
        this.player.gameObject.GetComponent<SpriteRenderer>().sprite = Throw;
    }

    public void enterWindup()
    {
        isWindup = true;
        this.player.gameObject.GetComponent<SpriteRenderer>().sprite = Windup;
    }

    public IEnumerator Handle()
    {
        this.handled = true; 
        yield return new WaitForSeconds(3.0f);
        this.reset = false;
        this.handled = false;
        this.destroy();
        //Application.LoadLevel(Application.loadedLevel);
        PlayerPrefs.SetInt("score", tossDistance);
        SceneManager.LoadScene(9);

    }
    void destroy(){
        Destroy(note);
        
    }

  
    
}
