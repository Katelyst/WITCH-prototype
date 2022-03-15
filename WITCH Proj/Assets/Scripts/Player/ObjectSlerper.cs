using UnityEngine;
using System.Collections;

public class ObjectSlerper : MonoBehaviour
{
    [SerializeField]
    private Transform rotationRefrence;

    void FixedUpdate()
    {
        Quaternion current = transform.rotation;
        Quaternion rotation = new Quaternion(rotationRefrence.rotation.x, rotationRefrence.rotation.y, rotationRefrence.rotation.z, rotationRefrence.rotation.w);

        transform.rotation = Quaternion.Slerp(current, rotation, 5 * Time.fixedDeltaTime);
    }
}