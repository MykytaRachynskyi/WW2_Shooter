using UnityEngine;

public interface IAIUnit {

    Transform target {
        get;
        set;
    }

    void OnBeingShot ();
    Transform AcquireTarget ();
    void Shoot ();
    void EnterAnimState (AIAnimStateData data);
    

}