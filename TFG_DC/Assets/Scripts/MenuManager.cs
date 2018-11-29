using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void changeScene(int Scene)
    {
        SceneManager.LoadScene(Scene);
    }


    public void TargetAttack()
    {

    }

    public void GlovalAttack()
    {



        GameObject unit;

        if (this.GetComponent<BattleManager>().turn[0].tag == "EscortA")
        {
            unit = GameObject.FindGameObjectWithTag("EscortA");
            unit.GetComponent<Escort>().UnitState = Escort.State.Waiting;
            unit.GetComponent<Escort>().anim.SetBool("Attack", true);
            unit.GetComponent<Escort>().stop = false;
        }
        else if (this.GetComponent<BattleManager>().turn[0].tag == "EscortB")
        {
            unit = GameObject.FindGameObjectWithTag("EscortB");
            unit.GetComponent<Escort>().UnitState = Escort.State.Waiting;
            unit.GetComponent<Escort>().anim.SetBool("Attack", true);
            unit.GetComponent<Escort>().stop = false;
        }
        else if (this.GetComponent<BattleManager>().turn[0].tag == "EscortC")
        {
            unit = GameObject.FindGameObjectWithTag("EscortC");
            unit.GetComponent<Escort>().UnitState = Escort.State.Waiting;
            unit.GetComponent<Escort>().anim.SetBool("Attack", true);
            unit.GetComponent<Escort>().stop = false;
        }

        for (int i = 0; i < this.GetComponent<BattleManager>().enemies.Length; i++)
        {
            this.GetComponent<BattleManager>().enemies[i].GetComponent<Enemy>().Recieve_DMG(1);
        }

        this.GetComponent<BattleManager>().ChangeTurn();

    }

    public void Heal()
    {
        GameObject unit;


        if (this.GetComponent<BattleManager>().turn[0].tag == "EscortA")
        {
            unit = GameObject.FindGameObjectWithTag("EscortA");
            unit.GetComponent<Escort>().Heal(2);
        }
        else if (this.GetComponent<BattleManager>().turn[0].tag == "EscortB")
        {
            unit = GameObject.FindGameObjectWithTag("EscortB");
            unit.GetComponent<Escort>().Heal(2);
        }
        else if (this.GetComponent<BattleManager>().turn[0].tag == "EscortC")
        {
            unit = GameObject.FindGameObjectWithTag("EscortC");
            unit.GetComponent<Escort>().Heal(2);
        }
    }

    public void Buff()
    {
        GameObject unit;


        if (this.GetComponent<BattleManager>().turn[0].tag == "EscortA")
        {
            unit = GameObject.FindGameObjectWithTag("EscortA");
            unit.GetComponent<Escort>().Buff();
        }
        else if (this.GetComponent<BattleManager>().turn[0].tag == "EscortB")
        {
            unit = GameObject.FindGameObjectWithTag("EscortB");
            unit.GetComponent<Escort>().Buff();
        }
        else if (this.GetComponent<BattleManager>().turn[0].tag == "EscortC")
        {
            unit = GameObject.FindGameObjectWithTag("EscortC");
            unit.GetComponent<Escort>().Buff();
        }
    }


    public void SetTarget(GameObject target)
    {

        GameObject unit;


        if (this.GetComponent<BattleManager>().turn[0].tag == "EscortA")
        {
            unit = GameObject.FindGameObjectWithTag("EscortA");
            unit.GetComponent<Escort>().target = target;
        }
        else if (this.GetComponent<BattleManager>().turn[0].tag == "EscortB")
        {
            unit = GameObject.FindGameObjectWithTag("EscortB");
            unit.GetComponent<Escort>().target = target;
        }
        else if (this.GetComponent<BattleManager>().turn[0].tag == "EscortC")
        {
            unit = GameObject.FindGameObjectWithTag("EscortC");
            unit.GetComponent<Escort>().target = target;
        }

    }
}
