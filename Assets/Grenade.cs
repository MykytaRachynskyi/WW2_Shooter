using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {
    [SerializeField] float force = 5f;
    [SerializeField] GameObject exlosion;
    // Use this for initialization
    public void Throw (Vector3 direction) {
        GetComponent<Rigidbody> ().AddForce (direction * force, ForceMode.Impulse);
        Invoke ("Explode", 3f);
    }

    public void Explode () {
        GetComponent<MeshRenderer> ().enabled = false;
        GameObject go = Instantiate (exlosion, this.transform.position, Quaternion.identity);
        go.GetComponent<ParticleSystem> ().Play ();
        go.GetComponent<AudioSource> ().Play ();

        Collider[] colliders = Physics.OverlapSphere (this.transform.position, 5f);
        foreach (Collider col in colliders) {
            if (col.tag == "Enemy")
                col.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl> ().OnBeingShot ();
        }

        Destroy (go, 1f);
        Destroy (gameObject, 1.5f);
    }
}