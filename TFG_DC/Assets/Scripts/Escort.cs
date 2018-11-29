using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Escort : MonoBehaviour
{
    #region Variables
    [Header("-Character stats-")]
    [Space]
    public float maxHealth;
    public float health; 
    public float attack;
    public float speed;
    public float persuasion;
    [Space]
    public float healthMultiplier;
    public float attackMultiplier;
    public float  speedMultiplier;
    public float persuasionMultiplier;

    [Header("-Battle-")]
    public GameObject BattleManager;
    GameObject[] Enemies;
    public GameObject target;

    [Header("-HUD and animations-")]
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

    [Header("-I.A-")]
    public State UnitState;
    #endregion

    void Start()
    {

        health = maxHealth = maxHealth * healthMultiplier;
        attack = attack * attackMultiplier;
        speed = speed * speedMultiplier;
        persuasion = persuasion * persuasionMultiplier;

        BattleManager = GameObject.FindGameObjectWithTag("GameController");

        Enemies = new GameObject[BattleManager.GetComponent<BattleManager>().enemies.Length];

        for (int i = 0; i < Enemies.Length; i++)
        {
            Enemies[i] = BattleManager.GetComponent<BattleManager>().allies[i];
        }
        healthbar.text = health.ToString() + " / " + maxHealth.ToString();
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        animations();
        switch (UnitState)
        {
            case State.Waiting:
                if (BattleManager.GetComponent<BattleManager>().turn[0] == this.gameObject)
                {
                    UnitState = State.Turn;
                }
                break;
            case State.Turn:
                //target = SelectTarget();
                if (target != null)
                    UnitState = State.Attack;

                break;
            case State.Attack:
                PerformAttack(target);
                BattleManager.GetComponent<BattleManager>().ChangeTurn();
                UnitState = State.Waiting;
                anim.SetBool("Attack", true);
                stop = false;
                target = null;

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

    public void Recieve_DMG(float dmg)
    {
        if (health - dmg <= 0)
        {
            UnitState = State.Dead;
        }
        else
        {
            health -= dmg;
        }
        anim.SetBool("Attacked", true);
        stop = false;
        healthbar.text = health.ToString() + " / " + maxHealth.ToString();
        if (health <= 0)
            UnitState = State.Dead;
    }


    void PerformAttack(GameObject Target)
    {

        Target.GetComponent<Enemy>().Recieve_DMG(attack);

    }

    public void Heal(int heal)
    {
        if(health + heal >= maxHealth){health = maxHealth;}
        else{ health += heal;}
        healthbar.text = health.ToString() + " / " + maxHealth.ToString();
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
