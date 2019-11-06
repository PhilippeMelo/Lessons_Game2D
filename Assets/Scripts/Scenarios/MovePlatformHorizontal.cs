using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformHorizontal : MonoBehaviour
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
    public bool leftToRight;

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

        if (leftToRight)
            GoLeftToRight();
        else
            GoRightToLeft();
    }

    void GoLeftToRight() {
        if (transform.position.x <= pos1.position.x) {
            isGoingAhead = true;

            if (!hasPlayerContact) {
                isIdle = true;
            }
        }

        if (!isIdle) {
            if (isGoingAhead) {
                transform.Translate(Time.deltaTime * speed, 0, 0);
            } else {
                transform.Translate(-Time.deltaTime * speed, 0, 0);
            }
        }

        if (transform.position.x >= pos2.position.x) {
            isGoingAhead = false;
        }
    }
    void GoRightToLeft() {
        if (transform.position.x >= pos1.position.x) {
            isGoingAhead = true;

            if (!hasPlayerContact) {
                isIdle = true;
            }
        }

        if (!isIdle) {
            if (isGoingAhead) {
                transform.Translate(-Time.deltaTime * speed, 0, 0);
            } else {
                transform.Translate( Time.deltaTime * speed, 0, 0);
            }
        }

        if (transform.position.x <= pos2.position.x) {
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
