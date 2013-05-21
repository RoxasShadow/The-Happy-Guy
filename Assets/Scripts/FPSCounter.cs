using UnityEngine;
using System;

public class FPSCounter : MonoBehaviour
{
    public static float updateInterval = 0.5F;
    private float timeleft = 0.5F;
    private float accum = 0;
    private int frames = 0;

    void OnGUI()
    {
        string counter;

        if (Time.timeScale == 0)
        {
            counter = "";
            GUI.Box(new Rect(Screen.width - (counter.Length * 12) - 1, 0, 60, 30), counter);
            return;
        }

        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        float fps = accum / frames;
        counter = String.Format("{0:F0} FPS", fps);

        GUI.Box(new Rect(Screen.width - (counter.Length * 12) - 1, 0, 60, 30), counter);

        timeleft = updateInterval;
        accum = 0.0F;
        frames = 0;
    }
}