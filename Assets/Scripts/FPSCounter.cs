using UnityEngine;

// https://sharpcoderblog.com/blog/unity-fps-counter

public class FPSCounter : MonoBehaviour
{
    public float updateInterval = 0.5f;

    private float accum = 0f;
    private int frames = 0;
    private float timeLeft;
    private float fps;
    private GUIStyle textStyle = new GUIStyle();
    
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = updateInterval;
        textStyle.fontStyle = FontStyle.Bold;
        textStyle.normal.textColor = Color.green;

    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeLeft <= 0.0)
        {
            fps = accum / frames;
            timeLeft = updateInterval;
            accum = 0f;
            frames = 0;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(5, 5, 100, 25), fps.ToString("F2") + "FPS", textStyle);
    }
}
