using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;

    public float speed;

    [SerializeField]
    private bool isIdle;
    [SerializeField]
    private bool hasPlayerContact;
    [SerializeField]
    private bool isGoingAhead;
    public bool downToUp;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1.6f;
        isGoingAhead = true;
        isIdle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPlayerContact) isIdle = false;

        if (downToUp)
            GoDownToUp();
        else
            GoUpToDown();


    }

    void GoDownToUp() {
        if (transform.position.y <= pos1.position.y) {
            isGoingAhead = true;

            if (!hasPlayerContact) {
                isIdle = true;
            }
        }

        if (!isIdle) {
            if (isGoingAhead) {

                transform.Translate(0, Time.deltaTime * speed, 0);
               
            } else {

                transform.Translate(0, -Time.deltaTime * speed, 0);
               
            }
        }


        if (transform.position.y >= pos2.position.y) {
            isGoingAhead = false;
        }
    }
    void GoUpToDown() {
        if (transform.position.y >= pos1.position.y) {
            isGoingAhead = true;

            if (!hasPlayerContact) {
                isIdle = true;
            }
        }

        if (!isIdle) {
            if (isGoingAhead) {

                transform.Translate(0, -Time.deltaTime * speed, 0);
               
            } else {

                transform.Translate(0, Time.deltaTime * speed, 0);
               
            }
        }


        if (transform.position.y <= pos2.position.y) {
            isGoingAhead = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            hasPlayerContact = true;
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            hasPlayerContact = false;
            other.transform.parent = null;
        }
    }
}
