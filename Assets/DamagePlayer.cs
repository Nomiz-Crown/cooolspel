using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DamagePlayer : MonoBehaviour
{
    public float damage;
    GooberBehaviour me;
    BoxCollider2D myCollider;
    mchp PlayerHealth;
    public Vector2 colliderSize;
    public LayerMask PlayerLayer;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Collider2D>() != null)
        {
            myCollider = GetComponent<BoxCollider2D>();
        }
        if (GetComponent<GooberBehaviour>() != null)
        {
            me = GetComponent<GooberBehaviour>();
        }
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<mchp>() != null)
        {
            PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<mchp>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckDamage();
    }
    void CheckDamage()
    {
        if(me.isLunging)
        {
            if (CheckOverlapp())
            {
                InflictDamage();
            }
        }
    }
    void InflictDamage()
    {
        PlayerHealth.SendMessage("TakeDamage", damage);
        ResetConditions();
    }
    void ResetConditions()
    {
        me.isLunging = false;
    }
    bool CheckOverlapp()
    {
        Vector2 offsetPos = new Vector2(transform.position.x, transform.position.y + 0.96875f);
        if (Physics2D.OverlapBox(offsetPos, myCollider.size, PlayerLayer).gameObject.CompareTag("Player"))
        {
            return true;
        }
        return false;
    }
}
