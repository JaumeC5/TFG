using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {
    public GameObject[] turn;

    [Header("Allies")]
    [Space]
    public GameObject[] allies;
    [Header("Enemies")]
    [Space]
    public GameObject[] enemies;

    [Header("Prefabs")]
    [Space]
    public GameObject[] EnemiesPrefabs;
    public GameObject enemyParent;
    [Space]

    public GameObject cursor;

    // Use this for initialization
    void Start () {
        GenerateEnemies();

        SetTurn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void SetTurn()
    {
        float[] speed;
        turn = new GameObject[allies.Length + enemies.Length];
        speed = new float[turn.Length];

        #region Omplir taula
        int i = 0;
        for (i = 0; i < allies.Length; i++){
            turn[i] = allies[i];
            speed[i] = allies[i].GetComponent<Escort>().speed;
        }
        for (int j = i; j < turn.Length; j++)
        {
            turn[j] = enemies[j-allies.Length];
            speed[j] = enemies[j - allies.Length].GetComponent<Enemy>().speed;
        }
        #endregion
        #region Ordenar taula
        GameObject aux1;
        float aux2;
        for (int j = 0; j < turn.Length; j++)
        {
            for (i = 1; i < turn.Length; i++)
            {
                if (speed[i] > speed[i - 1])
                {
                    aux1 = turn[i]; aux2 = speed[i];
                    turn[i] = turn[i - 1]; speed[i] = speed[i - 1];
                    turn[i - 1] = aux1; speed[i - 1] = aux2;
                }
            }
        }
        cursor.transform.position = turn[0].transform.position + new Vector3(0, 1, 0);
        #endregion
    }

    public void ChangeTurn()
    {
        GameObject aux = turn[0];
        for(int i = 0; i < turn.Length-1; i++) 
            turn[i] = turn[i + 1];
        turn[turn.Length - 1] = aux;
        cursor.transform.position = turn[0].transform.position + new Vector3(0, 1, 0);
    }

    void GenerateEnemies()
    {
        int numEnemies = Random.Range(1, 4);
        enemies = new GameObject[numEnemies];
        for (int i = 0; i < numEnemies; i++)
        {
            int typeEnemies = Random.Range(0, EnemiesPrefabs.Length);
            GameObject newEnemy;
            newEnemy = Instantiate(EnemiesPrefabs[typeEnemies], new Vector3(7, 3 - i * 2, 0), Quaternion.identity);
            newEnemy.transform.parent = enemyParent.transform;
            enemies[i] = newEnemy;
        }
    }

}
