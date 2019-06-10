using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Artillery : MonoBehaviour
{

    float lastShotTime = 0f;
    float shotCooldown = 10f;

    AudioSource audioSource;

    [SerializeField] AudioClip shotSound;
    [SerializeField] AudioClip exlosionSound;
    [SerializeField] Transform rayOriginBox;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float damageRadius = 5f;

    BoxCollider rayOriginBoxCol;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        explosionEffect.SetActive(false);

        rayOriginBoxCol = rayOriginBox.GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (Time.time - lastShotTime > shotCooldown)
        {
            Shoot();
            lastShotTime = Time.time;
            shotCooldown = Random.Range(7f, 12f);
        }
    }

    void Shoot()
    {
        audioSource.clip = shotSound;
        audioSource.Play();

        StartCoroutine(ChooseLandingSpot());
    }

    IEnumerator ChooseLandingSpot()
    {
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        Vector3 rayOrigin = new Vector3(Random.Range(rayOriginBox.position.x - rayOriginBox.localScale.x / 2f,
            rayOriginBox.position.x + rayOriginBox.localScale.x / 2f),
            Random.Range(rayOriginBox.position.y - rayOriginBox.localScale.y / 2f,
            rayOriginBox.position.y + rayOriginBox.localScale.y / 2f),
            Random.Range(rayOriginBox.position.z - rayOriginBox.localScale.z / 2f,
            rayOriginBox.position.z + rayOriginBox.localScale.z / 2f));
        RaycastHit hit;
        if (Physics.Raycast(new Ray(rayOrigin, Vector3.down), out hit, 50f))
        {
            explosionEffect.transform.position = hit.point;
            explosionEffect.SetActive(true);
            explosionEffect.GetComponent<AudioSource>().Play();
            ParticleSystem[] explosions = explosionEffect.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem explosion in explosions)
                explosion.Play();

            Collider[] affectedTargets = Physics.OverlapSphere(hit.point, damageRadius);
            foreach (Collider target in affectedTargets)
            {
                if (target is CapsuleCollider &&
                    target.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>() != null)
                {
                    target.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().OnBeingShot();
                }
            }

            CameraShaker.Instance.ShakeOnce(6f, 4f, 1f, 1f);
        }

        yield return new WaitForSeconds(3f);
        explosionEffect.SetActive(false);
    }
}
