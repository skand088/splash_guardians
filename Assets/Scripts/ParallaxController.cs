using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private float length;
    private Vector2 startpos;
    public GameObject cam;
    public float parallaxEffect;
    void Start()
    {
        startpos = new
        (
            transform.position.x,
            transform.position.y
        );

        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        Vector2 temp = new
        (
            cam.transform.position.x * (1 - parallaxEffect),
            cam.transform.position.y * (1 - parallaxEffect)
        );

        Vector2 dist = new
        (
            cam.transform.position.x * parallaxEffect,
            cam.transform.position.y * parallaxEffect
        );

        transform.position = new Vector3(startpos.x + dist.x, startpos.y + dist.y, transform.position.z);

        if (temp.x > startpos.x + length) startpos.x += length;
        else if (temp.x < startpos.x - length) startpos.x -= length;

        if (temp.y > startpos.y + length) startpos.y += length;
        else if (temp.y < startpos.y - length) startpos.y -= length;
    }
}
