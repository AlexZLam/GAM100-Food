using UnityEngine;
using UnityEngine.UI;

public class baking : MonoBehaviour
{
    [SerializeField]
    private float time, countdown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 2.0f;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void Timer()
    {
        countdown -= Time.deltaTime;
    }
}
