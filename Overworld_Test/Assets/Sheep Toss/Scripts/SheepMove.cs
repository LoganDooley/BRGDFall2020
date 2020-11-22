using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SheepMove : MonoBehaviour
{

    public Rigidbody2D rb;
    public float floor = -3.00001f;
    private bool tossed = false;
    public float gravityScale = 1.0f;
    public Vector2 launch = new Vector2(10.0f, 5.0f);
    public float launchScale = 1.0f;
    public UnityEngine.UI.Text txt;
    private float time = 0;
    public float launchDelay = 82f;
    private bool handle = false; 
    public OnBeat note;
    public AudioSource fling;
    public OnBeat onBeat;
    private Vector2 startPos;
    private Vector2 endPos;

    [SerializeField]
    PowerBar powerBar;

    // Start is called before the first frame update
    void Start()
    {
        this.rb.gravityScale = 0.0f;
        Invoke("GetTossed", launchDelay);

        powerBar.setMaxPower((143 * 5)/100);
        powerBar.setPower(this.launchScale);
       
    }

    // Update is called once per frame
    void Update()
    {
        this.txt.text = this.time.ToString();
        if (tossed)
        {
            if (this.rb.position.y <= this.floor)
            {
                this.rb.gravityScale = 0.0f;
                this.rb.velocity = new Vector2(0.0f, 0.0f);
                this.handle = true;
                endPos = this.GetComponent<Transform>().position;
                calculateDistance(startPos, endPos);
                this.transform.position = new Vector2(this.transform.position.x, this.floor - 0.001f);
            } else {
                this.time = this.time + Time.deltaTime;
             }
        } 

    }

    // This method adds to the launch amt (subtracts if amt is negative)
    public void AddLaunchAmt(float amt) {
        this.launchScale = this.launchScale + amt;
        if (this.launchScale < 0.0f) {
            this.launchScale = 0;
        }
        powerBar.setPower(launchScale);
    }

    public bool getHandle(){
        return this.handle;
    }
    public void setHandle(bool x){
        this.handle = x; 
    }
    public void GetTossed()
    {
        startPos = this.GetComponent<Transform>().position;
        onBeat.playThrow();
        this.tossed = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.launch.Scale(new Vector2(launchScale, launchScale));
        this.rb.velocity = launch;
        this.rb.gravityScale = this.gravityScale;
        this.fling.Play();
    }

    private void calculateDistance(Vector2 startPos, Vector2 endPos)
    {
        onBeat.tossDistance = Mathf.RoundToInt(endPos.x - startPos.x);
    }
}
