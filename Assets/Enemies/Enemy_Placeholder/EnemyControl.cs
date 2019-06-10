using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class EnemyControl : MonoBehaviour
    {

        Animator anim;
        AICharacterControl ai;
        // Use this for initialization
        void Start()
        {
            this.tag = "Enemy";
            anim = GetComponent<Animator>();
            ai = GetComponentInParent<AICharacterControl>();
        }

        public void Shoot()
        {
            ai.Shoot();
        }
    }
}