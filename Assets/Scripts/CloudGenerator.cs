using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    public GameObject[] cloudPrefabs;
    private bool canCreate = true;
    private float howOften = 3;

    void Start()
    {

    }


    void Update()
    {
        if (canCreate)
        {
            CreateCloud();
        }
    }

    void CreateCloud()
    {
        canCreate = false;

        int index = Random.Range(0,2);
        Instantiate(cloudPrefabs[index], transform.position, Quaternion.identity);
        Invoke("ResetCanCreate", Random.Range(30f, 50f));
    }

    void ResetCanCreate()
    {
        canCreate = true;
    }
}
