using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public string name;
    public int max_hp;
    public int atk;
    public int def;
    public int hp;
    public float speed;
    public bool inBattle;
    RaycastHit2D target;

    // Start is called before the first frame update
    void Start()
    {
        name = "Enemy";
        max_hp = 100;
        atk = 30;
        def = 3;
        speed = 2.0f;
        
        hp = 100;
        inBattle = false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Debug.DrawRay(transform.position, Vector2.left * 1, new Color(1, 0, 0));
        target = Physics2D.Raycast(transform.position, Vector2.left * 1, 1, LayerMask.GetMask("Unit"));

        if (target.collider != null)
        {
            if (!inBattle)
                StartCoroutine(attack());
            inBattle = true;
        }
        else
        {
            if(inBattle)
                StopCoroutine(attack());
            inBattle = false;
            move();
        }
        //if ()

        if (hp < 0)
        {
            Destroy(gameObject);
        }
    }

    public void move()
    {
        Vector2 position = transform.position;
        position.x -= speed * Time.deltaTime;
        transform.position = position;
    }

    IEnumerator attack()
    {
        GameObject targetObject = target.collider.gameObject;
        Unit status = targetObject.GetComponent<Unit>();
        HpBar hpBar = targetObject.GetComponentInChildren<HpBar>();
        

        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            status.hp = status.hp - atk;
            hpBar.SetHealth(status.hp, status.max_hp);

        }
    }
}