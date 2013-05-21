using UnityEngine;

public class Directionallight : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            light.enabled = !light.enabled;
    }
}
