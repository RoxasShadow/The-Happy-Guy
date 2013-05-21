using UnityEngine;

public class Sun : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F2))
            light.enabled = !light.enabled;
    }
}
