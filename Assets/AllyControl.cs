using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class AllyControl : MonoBehaviour
    {
        [SerializeField] int health = 2;
        [SerializeField] float range = 30f;

        BatallionControl batallion;
        Coroutine lookAtBattailion;
        Animator anim;

        public bool Alive
        {
            get;
            private set;
        }
        // Use this for initialization
        void Start()
        {
            batallion = GameObject.FindObjectOfType<BatallionControl>();
            anim = GetComponent<Animator>();
            anim.SetTrigger("CombatStart");
            anim.SetFloat("Uprightness", 0f);

            if (EventManager.instance != null)
                EventManager.instance.events[EVENT_TYPES.COMBAT_START].AddListener(StartCombat);

            Alive = true;

            lookAtBattailion = StartCoroutine(LookAtBattailion());
        }
        IEnumerator LookAtBattailion()
        {
            while (true)
            {
                transform.LookAt(batallion.centerPoint);
                yield return null;
            }
        }

        public void OnBeingShot()
        {
            if (health > 1)
            {
                health--;
            }
            else
            {
                anim.SetBool("Dead", true);
                Destroy(gameObject, 15f);
                Alive = false;
                CancelInvoke();
            }
        }
        public void StartShooting()
        {
            anim.SetBool("IsShooting", true);
            InvokeRepeating("Shoot", 0f, .3f);
        }

        public void StartCombat()
        {
            StopCoroutine(lookAtBattailion);
            anim.SetTrigger("CombatStart");
            anim.SetBool("IsShooting", true);
            InvokeRepeating("Shoot", 1f, 5f);
        }

        public void Shoot()
        {
            AICharacterControl[] targets = GameObject.FindObjectsOfType<AICharacterControl>();
            AICharacterControl target = null;
            foreach (AICharacterControl t in targets)
            {
                if (!t.GetComponent<AICharacterControl>().dead)
                    target = t;
            }

            if (target == null)
                return;

            if (Vector3.Distance(this.transform.position, target.transform.position) < range)
            {
                int random = Random.Range(0, 9);
                if (random < 4)
                {
                    Debug.Log("Ally killed an Enemy");
                    target.OnBeingShot();
                }
            }
        }
    }
}