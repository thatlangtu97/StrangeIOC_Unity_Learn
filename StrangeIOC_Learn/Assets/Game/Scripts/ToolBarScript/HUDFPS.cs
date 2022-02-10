using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HUDFPS : MonoBehaviour 
{

    // Attach this to a GUIText to make a frames/second indicator.
    //
    // It calculates frames/second over each updateInterval,
    // so the display does not keep changing wildly.
    //
    // It is also fairly accurate at very low FPS counts (<10).
    // We do this not by simply counting frames per interval, but
    // by accumulating FPS for each frame. This way we end up with
    // correct overall FPS even if the interval renders something like
    // 5.5 frames.
	public  float updateInterval = 0.5F;
    public Text fps_text;
	private float accum   = 0; // FPS accumulated over the interval
	private int   frames  = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval4

	private float fps = 0.0f;
	private int CountFps = 10;
	
    private void Awake()
    {
        Application.targetFrameRate = 62;
    }
    void Start()
	{
        timeleft = updateInterval;  
	}
	
	void Update()
	{
		CountFps -= 1;
		if (CountFps <= 0)
		{
			fps = Time.timeScale / Time.deltaTime;
			fps_text.text = System.String.Format("{0:F2}", fps);
			CountFps = 10;
			if (fps < 20)
			{
				fps_text.color = Color.red;
				return;
			}

			if (fps < 30)
			{
				fps_text.color = Color.magenta;
				return;
			}
			if (fps < 50)
			{
				fps_text.color = Color.yellow;
				return;
			}
			if (fps < 100)
			{
				fps_text.color = Color.green;
				return;
			}
		}
	}
}

