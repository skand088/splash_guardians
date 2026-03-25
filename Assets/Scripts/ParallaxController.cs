using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private float length, startpos_x, startpos_y;
    public GameObject cam;
    public float parallaxEffect;
    void Start()
    {
        startpos_x = transform.position.x;
        startpos_y = transform.position.y;

        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp_x = (cam.transform.position.x * (1 - parallaxEffect));
        float dist_x = (cam.transform.position.x * parallaxEffect);

        float temp_y = (cam.transform.position.y * (1 - parallaxEffect));
        float dist_y = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startpos_x + dist_x, startpos_y + dist_y, transform.position.z);

        if (temp_x > startpos_x + length) startpos_x += length;
        else if (temp_x < startpos_x - length) startpos_x -= length;

        if (temp_y > startpos_y + length) startpos_y += length;
        else if (temp_y < startpos_y - length) startpos_y -= length;
    }
}
