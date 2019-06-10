using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EZCameraShake;

public class Player : MonoBehaviour
{
    [SerializeField] int health = 2;
    [SerializeField] AnimationCurve curve;
    [SerializeField] AnimationCurve vfxcurve;
    [SerializeField] float fallingSpeed = 1f;
    [SerializeField] float vfxspeed = 1f;
    [SerializeField] Transform eyes;
    [SerializeField] Vector3 eyesFinalPos;
    [SerializeField] Vector3 eyesFinalRot;
    [SerializeField] CanvasGroup deathMenu;

    [SerializeField] GunTopShooter gts;
    [SerializeField] Image bloodVFX;

    [SerializeField] GameObject grenade;

    Vector3 eyesInitialPos;
    Vector3 eyesInitialRot;
    bool dead = false;

    private void Start()
    {
        eyesInitialPos = eyes.localPosition;
        eyesInitialRot = eyes.localEulerAngles;
    }

    public void OnBeingShot()
    {
        if (dead)
            return;


        health--;
        StartCoroutine(ShotEffect());
        if (health <= 0)
        {
            Die();
        }
    }

    public void DetonateAllBombs()
    {
        Grenade[] bombs = GameObject.FindObjectsOfType<Grenade>();
        foreach(Grenade bomb in bombs)
            bomb.Explode();

        EventManager.instance.events[EVENT_TYPES.COMBAT_START].Invoke ();
    }

    IEnumerator ShotEffect()
    {
        CameraShaker.Instance.ShakeOnce(15f, 4f, .1f, 1f);
        float step = 0f;
        while (step < 1f)
        {
            step += Time.deltaTime * vfxspeed;
            bloodVFX.color = new Color(bloodVFX.color.r, bloodVFX.color.g, bloodVFX.color.b, vfxcurve.Evaluate(step));
            yield return null;
        }
    }

    public void ThrowGrenade()
    {
        GameObject go = Instantiate(grenade, this.transform.position, Quaternion.identity);
        go.GetComponent<Grenade>().Throw(Vector3.Lerp(this.transform.GetChild(0).GetChild(2).transform.forward, this.transform.GetChild(0).GetChild(2).transform.up, .2f).normalized);
    }

    void Die()
    {
        deathMenu.alpha = 1f;
        deathMenu.blocksRaycasts = true;
        deathMenu.interactable = true;

        dead = true;
        gts.canShoot = false;
        StartCoroutine(FallDown());
    }

    IEnumerator FallDown()
    {
        float step = 0f;
        while (step < 1f)
        {
            step += Time.deltaTime * fallingSpeed;
            eyes.localPosition = Vector3.Lerp(eyesInitialPos, eyesFinalPos, curve.Evaluate(step));
            eyes.localEulerAngles = Vector3.Lerp(eyesInitialRot, eyesFinalRot, curve.Evaluate(step));
            yield return null;
        }
    }

    public void Revive()
    {
        SceneManager.LoadScene("Main");
        /*gts.canShoot = false;
        eyes.localPosition = eyesInitialPos;
        eyes.localEulerAngles = eyesInitialRot;*/
    }
}
