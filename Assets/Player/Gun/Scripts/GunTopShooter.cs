using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class GunTopShooter : MonoBehaviour
{

    [SerializeField] Transform muzzle;
    [SerializeField] float range = 100f;
    [SerializeField] bool windowsControls = false;
    [SerializeField] GameObject dustImpactVFX;
    [SerializeField] GameObject bloodImpactVFX;
    [SerializeField] float enemyAlertRadius = 5f;
    [SerializeField] float antiRecoilSpeed = 2f;
    [SerializeField] float recoilAmount = 1f;
    [SerializeField] Transform ironsightPoint;

    Dictionary<string, GameObject> effects = new Dictionary<string, GameObject>();

    public float shotsPerSecond = 1f;

    float previousShotTime = 0f;
    AudioSource audioSource;
    Animator animator;
    ParticleSystem VFX;
    Transform parent;
    Transform carrot;
    bool firstShot = true;

    Vector3 velocity = Vector3.zero;
    Vector3 initialLocalPosition;
    Vector3 currentVelocity = Vector3.zero;
    public bool canShoot = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        VFX = muzzle.GetComponentInChildren<ParticleSystem>();

        parent = this.transform.parent;

        animator = this.transform.parent.GetComponent<Animator>();
        animator.SetFloat("shootingSpeed", shotsPerSecond);

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            windowsControls = true;

        effects.Add("Terrain", dustImpactVFX);
        effects.Add("Enemy", bloodImpactVFX);

        carrot = this.GetComponent<GunTopMovement>().carrot;

        initialLocalPosition = this.transform.localPosition;

        StrategySettings strategySettings = GameObject.FindObjectOfType<StrategySettings>();
        if (strategySettings != null)
            strategySettings.PlaceObjects();
    }

    void Update()
    {
        if (windowsControls)
        {
            if (Input.GetKey(KeyCode.Space) && canShoot)
                Shoot();
        }

        if (transform.eulerAngles.x > 270f)
        {
            //parent.localEulerAngles = new Vector3(parent.localEulerAngles.x - Time.deltaTime * antiRecoilSpeed, 0f, 0f);
        }

        ApplyAntiRecoil();
    }

    void ApplyAntiRecoil()
    {
        this.transform.localPosition = Vector3.SmoothDamp(this.transform.localPosition,
            initialLocalPosition, ref currentVelocity, .2f);
    }

    void AddRecoil()
    {
        this.transform.localPosition = Vector3.SmoothDamp(this.transform.localPosition,
            this.transform.localPosition + transform.right * recoilAmount, ref currentVelocity, .05f);

        carrot.Rotate(Vector3.right, Time.deltaTime * -recoilAmount * 5f);
        this.transform.LookAt(carrot.GetChild(0), Vector3.up);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - 90f,
            transform.localEulerAngles.y,
            transform.localEulerAngles.z);
    }

    public void Shoot()
    {
        if (Time.time - previousShotTime > 1 / shotsPerSecond)
        {
            if (firstShot)
            {
                EventManager.instance.events[EVENT_TYPES.COMBAT_START].Invoke();
                firstShot = false;
            }
            //animator.SetTrigger("isShooting");
            AddRecoil();
            Ray ray = new Ray(Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, 0f)),
                (ironsightPoint.position - Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, 0f))));
            Debug.DrawRay(Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, 0f)),
                (ironsightPoint.position - Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, 0f))));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, range))
            {
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.transform.parent.GetComponent<AICharacterControl>().OnBeingShot();
                    GameObject bloodVFX = Instantiate(effects[hit.collider.tag]);
                    bloodVFX.transform.position = hit.point;
                    bloodVFX.transform.LookAt(this.transform);
                    bloodVFX.GetComponent<ParticleSystem>().Play();
                    Destroy(bloodVFX, 2f);
                }
                else if (hit.collider.tag == "Terrain")
                {
                    GameObject dustVFX = Instantiate(effects[hit.collider.tag]);
                    dustVFX.transform.position = hit.point;
                    dustVFX.GetComponent<ParticleSystem>().Play();
                    Destroy(dustVFX, 2f);
                }

                AlertNearbyEnemies(hit.point);
            }
            audioSource.Play();
            VFX.Play();
            previousShotTime = Time.time;
        }
    }

    void AlertNearbyEnemies(Vector3 point)
    {
        foreach (Collider col in Physics.OverlapSphere(point, enemyAlertRadius))
        {
            if (col is CapsuleCollider)
            {
                if (col == null)
                    continue;
                try
                {
                    //col.GetComponent<AICharacterControl>().Alert();
                }
                catch (System.Exception e)
                {
                    //Debug.Log(e.Message);
                }
            }
        }
    }
}