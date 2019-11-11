using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private GameObject end;
    [SerializeField]
    private float speed = 3f;

    void Start()
    {
        end = GameObject.Find("CloudEnd");
        speed  = Random.Range(0.2f, 0.6f);
        Invoke("NoParent", 0.1f);
    }


    void Update()
    {
        transform.Translate(Time.deltaTime * speed * -1, 0, 0);

        if (transform.position.x < end.transform.position.x)
        {
            Destroy(gameObject);
        }
    }

    void NoParent()
    {
        transform.parent = null;
    }
}
