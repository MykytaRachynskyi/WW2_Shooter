using UnityEngine;

public struct AIAnimStateData {

    public float Uprightness {
        get;
        set;
    }
    public float AnimSpeed {
        get;
        set;
    }
    public float AgentSpeed {
        get;
        set;
    }
    public float ColliderHeight {
        get;
        set;
    }
    public Vector3 ColliderCenter {
        get {
            return new Vector3 (0f, ColliderHeight / 2f, 0f);
        }
    }
}