using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Entity

{
    //дистанция от которой он начинает видеть игрока
    public float seeDistance = 2f;
    //дистанция до атаки
    public float attackDistance = 2f;
    //скорость енеми
    public float speed = 6;
    //игрок
    public Transform target;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < seeDistance)
        {
            if (Vector3.Distance(transform.position, target.transform.position) > attackDistance)
            {
                //walk
                transform.LookAt(target.transform);
                transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            }
        }
        else
        {
            //idle
        }
    }

}
