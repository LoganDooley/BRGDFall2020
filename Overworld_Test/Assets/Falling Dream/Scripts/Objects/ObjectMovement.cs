using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FallingDream.Object {
    public class ObjectMovement : MonoBehaviour
    {

        public AudioClip hitSound;

        private void OnTriggerEnter(Collider collision) {
            Debug.Log("Collision Ocurred!");
            GameObject obj = collision.gameObject;
            switch (obj.tag)
            {
                case "ObjectDestructor":
                    GameObject.FindWithTag("System").GetComponent<FallingDream.System.ScoreSystem>().IncrementScore();
                    Destroy(gameObject);
                    break;
                case "Player":
                    Debug.Log("Collided with player!");
                    obj.GetComponent<FallingDream.Player.PlayerHealth>().TakeDamage();
                    obj.GetComponent<FallingDream.Player.PlayObjectHit>().Play(hitSound);
                    Destroy(this.gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
