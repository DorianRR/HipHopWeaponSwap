using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int doorsPerSide;
    public Vector3[] leftSpawnPositions;
    public Vector3[] leftMovePositions;
    public Vector3[] rightSpawnPositions;
    public Vector3[] rightMovePositions;
    public int[] enemiesPerWaveletPerSide;
    public int[] totalEnemiesPerWave;
    public GameObject player1;
    public GameObject player2;

    public GameObject enemy;

    private int currWave;
    private int enemiesKilled;

    // Use this for initialization
    void Start() {
        Vector3[] leftSpawnPositions = new Vector3[doorsPerSide];
        Vector3[] rightSpawnPositions = new Vector3[doorsPerSide];
        Vector3[] leftMovePositions = new Vector3[doorsPerSide];
        Vector3[] rightMovePositions = new Vector3[doorsPerSide];
        currWave = 0;
        startWave();
    }

    // Update is called once per frame
    void Update() {

    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < doorsPerSide; i++)
        {
            Gizmos.DrawRay(leftSpawnPositions[i], leftMovePositions[i] - leftSpawnPositions[i]);
            Gizmos.DrawRay(rightSpawnPositions[i], rightMovePositions[i] - rightSpawnPositions[i]);

        }
    }

    private void startWave()
    {
        enemiesKilled = 0;
        spawnWavelet();
    }

    private void spawnWavelet()
    {
        for (int i=0; i<enemiesPerWaveletPerSide[currWave]; i++)
        {
            Enemy newEnemy = Instantiate(enemy, leftSpawnPositions[i], Quaternion.identity).GetComponent<Enemy>();
            newEnemy.setInitialMovement(leftMovePositions[i]);
            newEnemy.setEnemyInfo(this, player1);

            newEnemy = Instantiate(enemy, rightSpawnPositions[i], Quaternion.identity).GetComponent<Enemy>();
            newEnemy.setInitialMovement(rightMovePositions[i]);
            newEnemy.setEnemyInfo(this, player2);
        }
    }

    public void registerEnemyDead( GameObject side )
    {
        if ( enemiesKilled < totalEnemiesPerWave[currWave] )
        {
            spawnNewEnemy(side);
        }
        else
        {
            Debug.Log("Wave over!");
        }
    }

    private void spawnNewEnemy ( GameObject side )
    {
        int door = Random.Range(0, doorsPerSide);
        Enemy newEnemy;
        if ( side == player1)
        {
            newEnemy = Instantiate(enemy, leftSpawnPositions[door], Quaternion.identity).GetComponent<Enemy>();
            newEnemy.setInitialMovement(leftMovePositions[door]);
        }
        else
        {
            newEnemy = Instantiate(enemy, rightSpawnPositions[door], Quaternion.identity).GetComponent<Enemy>();
            newEnemy.setInitialMovement(rightMovePositions[door]);
        }
        newEnemy.setEnemyInfo(this, side);
    }

}
