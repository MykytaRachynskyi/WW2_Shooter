using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(AudioSource))]
    public class AICharacterControl : MonoBehaviour
    {
        public enum AIMode
        {
            Passive,
            Combat
        }

        [SerializeField] int availableGrenades = 2;
        [SerializeField] float timeBetweenShots = .3f;

        float lastShotTime { get; set; }

        public UnityEngine.AI.NavMeshAgent agent;
        public Transform player;
        [SerializeField] AIMode currentAIMode;

        Animator animator;
        CoverPointManager coverPointManger;
        public GameObject grenade;
        public Transform rightHand;
        public bool dead = false;
        public bool canThrowWhileRunning;

        bool inCombat = false;
        bool inCover = false;

        public Transform myCoverPoint;
        float defaultStoppingDistance;
        public CapsuleCollider collider { get; set; }
        List<Transform> targetsTransform = new List<Transform>();
        [SerializeField] Transform target = null;
        AudioSource audioSource;
        Vector3[] marchingPath = null;
        int currentPathIndex = 0;
        float initialColliderHeight;
        private void Start()
        {
            EventManager.instance.events[EVENT_TYPES.COMBAT_START].AddListener(StartCombat);
            currentAIMode = AIMode.Passive;
            audioSource = GetComponent<AudioSource>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
            animator = this.transform.GetChild(0).GetComponent<Animator>();
            coverPointManger = FindObjectOfType<CoverPointManager>();
            collider = this.transform.GetChild(0).GetComponent<CapsuleCollider>();
            animator.SetBool("IsShooting", false);
            dead = false;
            initialColliderHeight = collider.height;

            AllyControl[] targets = FindObjectsOfType<AllyControl>();
            foreach (AllyControl ac in targets)
                if (ac.Alive)
                    targetsTransform.Add(ac.transform);

            targetsTransform.Add(player);
        }

        private void Update()
        {
            if (currentAIMode == AIMode.Combat)
            {
                if (!agent.enabled)
                    return;
                if (agent.remainingDistance <= agent.stoppingDistance + 0.05f)
                {
                    animator.SetBool("IsShooting", true);
                    inCover = true;
                    //agent.ResetPath();
                    transform.LookAt(target);
                    if (Time.time > lastShotTime + timeBetweenShots && inCover)
                    {
                        //Shoot ();
                        lastShotTime = Time.time;
                    }
                }
                else
                {
                    animator.SetBool("IsShooting", false);
                }
            }
            else
            {
                if (animator.GetFloat("Speed") > 0 && agent.remainingDistance <= agent.stoppingDistance + 0.05f)
                {
                    if (marchingPath != null)
                    {
                        currentPathIndex++;
                        if (currentPathIndex < marchingPath.Length)
                            agent.SetDestination(marchingPath[currentPathIndex]);
                    }
                }
            }

            AllyControl[] targets = FindObjectsOfType<AllyControl>();
            targetsTransform.Clear();
            foreach (AllyControl ac in targets)
                if (ac.Alive)
                    targetsTransform.Add(ac.transform);

            targetsTransform.Add(player);
        }

        public void OnBeingShot()
        {
            Debug.Log("Enemy says: I've been shot!");
            animator.SetBool("Dead", true);
            agent.isStopped = true;
            this.gameObject.layer = 12;
        }

        public void Grenade()
        {
            GameObject go = Instantiate(grenade, rightHand.position, Quaternion.identity);
            go.GetComponent<Grenade>().Throw(Vector3.Lerp(this.transform.forward, this.transform.up, .2f).normalized);
        }

        public void Shoot()
        {
            audioSource.pitch = UnityEngine.Random.Range(.5f, 1.5f);
            audioSource.Play();
            agent.updateRotation = false;
            if (target == null)
            {
                target = targetsTransform[UnityEngine.Random.Range(0, targetsTransform.Count)];
                return;
            }
            if (target.GetComponent<Player>() == null)
            {
                if (!target.GetComponent<AllyControl>().Alive)
                    target = targetsTransform[UnityEngine.Random.Range(0, targetsTransform.Count)];
            }

            if (UnityEngine.Random.Range(0, 10) < 2 && inCover)
            {
                if (target == player)
                {
                    target.GetComponent<Player>().OnBeingShot();
                }
                else
                {
                    target.GetComponent<AllyControl>().OnBeingShot();
                }
            }
        }

        void TurnToTarget(Vector3 target)
        {
            StartCoroutine(TurnToTargetCR(target));
        }

        IEnumerator TurnToTargetCR(Vector3 target)
        {
            agent.velocity = Vector3.zero;
            agent.acceleration = 0f;
            agent.SetDestination(target);
            yield return new WaitForSeconds(1f);
            agent.ResetPath();
            agent.acceleration = 8f;
        }

        Vector3 FindNearestCover()
        {
            return Vector3.zero;
        }

        public void SetTarget(Transform target)
        {
            agent.SetDestination(target.position);
        }

        public void EnterMarchingState(Vector3[] path)
        {
            if (path != null)
                marchingPath = path;

            if (path.Length > 0)
                agent.SetDestination(marchingPath[0]);

            animator.SetFloat("Speed", 1f);
        }

        public void EnterDefaultState()
        {
            agent.speed = 5f;
            animator.SetFloat("Uprightness", 1);
            animator.SetFloat("Speed", 1);
            collider.height = initialColliderHeight;
            collider.center = new Vector3(0f, collider.height / 2f, 0f);
        }

        public void EnterCrouchState()
        {
            agent.speed = 3.5f;
            animator.SetFloat("Uprightness", .5f);
            animator.SetFloat("Speed", .5f);
            collider.height = initialColliderHeight * .75f;
            collider.center = new Vector3(0f, collider.height / 2f, 0f);
        }

        public void EnterProneState()
        {
            agent.speed = 1.5f;
            animator.SetFloat("Uprightness", 0f);
            animator.SetFloat("Speed", .2f);
            collider.height = initialColliderHeight * .2f;
            collider.center = new Vector3(0f, collider.height / 2f, 0f);
        }

        public void StartCombat()
        {
            if (!inCombat)
            {
                Debug.Log("Combat start!");
                myCoverPoint = coverPointManger.GetFreePoint(this.transform.position);
                agent.SetDestination(myCoverPoint.position);
                currentAIMode = AIMode.Combat;
                animator.SetTrigger("CombatStart");
                if (UnityEngine.Random.Range(0, 9) < 3)
                    EnterCrouchState();
                else
                    EnterDefaultState();

                inCombat = true;
            }
        }
    }
}