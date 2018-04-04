using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour {

    private GameObject cam;
    public GameObject enemyPrefab;
    private GameObject enemy;
    private bool creatorOutsideOfCamera = false;

    void Start(){
        cam = GameObject.FindWithTag("MainCamera");
        GameObject newEnemy = Instantiate (enemyPrefab, transform.position, Quaternion.identity);
        enemy = newEnemy;
    }

    void Update(){
        Vector3 viewPosCreator = cam.GetComponent<Camera>().WorldToViewportPoint(transform.position);

        if (viewPosCreator.x > 1 || viewPosCreator.x < 0 || viewPosCreator.y > 1 || viewPosCreator.y < 0){
            creatorOutsideOfCamera = true;
        }else{
            creatorOutsideOfCamera = false;
        }

        if (enemy == null && creatorOutsideOfCamera){
            GameObject newEnemy = Instantiate (enemyPrefab, transform.position, Quaternion.identity);
            enemy = newEnemy;
        }
    }
}