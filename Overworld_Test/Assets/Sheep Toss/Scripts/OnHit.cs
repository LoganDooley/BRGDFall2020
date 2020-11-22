using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{
    // Start is called before the first frame update
    private bool hittable = false;
    private Vector2 origin;
    public float speed = 3.0f;
    public Rigidbody2D rb;
    public SheepMove sm;
    public float successAmt = 5;
    public float failAmt = -10;

    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;
    public int spriteType;

    public OnBeat onBeat;
    public GameObject shootingStar;

    void Start()
    {
        spriteType = Random.Range(0, 4);

        if (spriteType == 0)
        {
            this.GetComponent<SpriteRenderer>().sprite = up;
        }
        else if (spriteType == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = down;
        }
        else if (spriteType == 2)
        {
            this.GetComponent<SpriteRenderer>().sprite = left;
        }
        else if (spriteType == 3)
        {
            this.GetComponent<SpriteRenderer>().sprite = right;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetHit() {
        onBeat.notesOnScreen--;
        Instantiate(shootingStar, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        if (onBeat.notesOnScreen <= 0)
        {
            onBeat.enterWindup();
        }
    }

    public void GetKilled() {
        onBeat.notesOnScreen--;
        this.sm.AddLaunchAmt(this.failAmt / 100.0f);
        Destroy(this.gameObject);
        if (onBeat.notesOnScreen <= 0)
        {
            onBeat.enterWindup();
        }
    }

    public void SetHittable(bool hittable) {
        this.hittable = hittable;
    }
    public bool GetHittable()
    {
        return this.hittable;
    }

    public float GetSuccess()
    {
        return this.successAmt;
    }
}
