using UnityEngine;

public class Exit : MonoBehaviour, IExit
{
    public Vector3 Position => gameObject.transform.position;
}
