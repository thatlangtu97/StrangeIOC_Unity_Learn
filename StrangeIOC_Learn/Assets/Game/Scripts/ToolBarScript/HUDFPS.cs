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
		//timeleft -= Time.deltaTime;
		//accum += Time.timeScale/Time.deltaTime;
		//++frames;

		//// Interval ended - update GUI text and start new interval
		//if( timeleft <= 0.0 )
		//{
		//	// Calculate frame rate
		//	fps = accum/frames;

		//	timeleft = updateInterval;
		//	accum = 0.0F;
		//	frames = 0;
		//}
		fps = Time.timeScale / Time.deltaTime;
        fps_text.text = System.String.Format("{0:F2}", fps);
    }
}

