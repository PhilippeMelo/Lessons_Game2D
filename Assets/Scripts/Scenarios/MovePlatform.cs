using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;

    public bool isVertical = true;

    public float speed;

    [SerializeField]
    private bool isIdle;
    [SerializeField]
    private bool hasPlayerContact;
    [SerializeField]
    private bool isGoingUp;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1.6f;
        isGoingUp = true;
        isIdle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPlayerContact) isIdle = false;

        if (isVertical) {
            if (transform.position.y <= pos1.position.y) {
                isGoingUp = true;

                if (!hasPlayerContact) {
                    isIdle = true;
                }
            }
        } else {
            if (transform.position.x >= pos1.position.x) {
                isGoingUp = true;

                if (!hasPlayerContact) {
                    isIdle = true;
                }
            }
        }

        if (!isIdle) {
            if (isGoingUp) {
                if (isVertical) {
                    transform.Translate(0, Time.deltaTime * speed, 0);
                } else {
                    transform.Translate(-Time.deltaTime * speed, 0, 0);
                }
                
            } else {
                if (isVertical) {
                    transform.Translate(0, -1 * Time.deltaTime * speed, 0);
                } else {
                    transform.Translate( Time.deltaTime * speed, 0, 0);
                }
                
            }
        }

        if (isVertical) {
            if (transform.position.y >= pos2.position.y) {
                isGoingUp = false;
            }
        } else {
            if (transform.position.x <= pos2.position.x) {
                isGoingUp = false;
            }
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
