using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    public float health, attack, speed, persuasion, firmness;

    public GameObject BattleManager;

    GameObject[] Enemies;
    GameObject target;
    BattleManager BM;

    float wait = 1f;

    public Text healthbar;

    public Animator anim;
    public bool stop = false;

    public enum State
    {
        Waiting,
        Turn,
        Attack,
        Dead
    }

    public State UnitState;

    public void Attack(GameObject enemy)
    {

    }

    void Start()
    {

        BattleManager = GameObject.FindGameObjectWithTag("GameController");
        BM = BattleManager.GetComponent<BattleManager>();
        Enemies = new GameObject[BM.allies.Length];

        for (int i = 0; i < Enemies.Length; i++)
        {
            Enemies[i] = BM.allies[i];
        }
        //healthbar.text = health.ToString() + " / 5";
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {

        animations();
        switch (UnitState)
        {
            case State.Waiting:
                if (BM.turn[0] == this.gameObject)
                {
                    UnitState = State.Turn;
                }
                break;
            case State.Turn:

                wait -= Time.deltaTime;
                if (wait < 0)
                {
                    think();

                }

                break;
            case State.Attack:

                PerformAttack(target);
                anim.SetBool("Attack", true);
                stop = false;
                BM.ChangeTurn();
                UnitState = State.Waiting;
                break;
            case State.Dead:
                Destroy(this.gameObject);
                break;
        }

    }
    public GameObject SelectTarget()
    {
        int rnd = Random.Range(0, Enemies.Length - 1);
        Debug.Log(rnd);
        return Enemies[rnd];
    }

    public void Recieve_DMG(float Damage)
    {
        health -= Damage;
        anim.SetBool("Attacked", true);
        stop = false;
        healthbar.text = health.ToString() + " / 5";
        if (health <= 0)
            UnitState = State.Dead;
    }

    void think()
    {
        if (health < 2) { Heal(2); }
        else
        {
            int rnd = Random.Range(0, 10);
            if (rnd < 7)
            {
                target = SelectTarget();
                UnitState = State.Attack;
            }
            else { Buff(); }
        }
        wait = 1f;
    }

    void PerformAttack(GameObject Target)
    {
        int i;
        for (i = 0; i < Enemies.Length; i++)
        {
            if (Enemies[i] == target) break;
        }

        Enemies[i].GetComponent<Escort>().Recieve_DMG(attack);

    }

    public void Heal(int heal)
    {
        health += heal;
        healthbar.text = health.ToString() + " / 5";
        BattleManager.GetComponent<BattleManager>().ChangeTurn();
        UnitState = State.Waiting;
    }

    public void Buff()
    {
        attack++;
        BattleManager.GetComponent<BattleManager>().ChangeTurn();
        UnitState = State.Waiting;
    }

    void animations()
    {
        if (stop == false)
            stop = true;
        else
        {
            anim.SetBool("Attack", false);
            anim.SetBool("Attacked", false);
        }

    }

    void Death()
    {

    }
}
