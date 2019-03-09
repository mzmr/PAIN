using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject TargetToFollow;
    public float MoveSpeed;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = new Vector3(
            TargetToFollow.transform.position.x, 
            TargetToFollow.transform.position.y, 
            transform.position.z
        );
        transform.position = Vector3.Lerp(transform.position, targetPosition, MoveSpeed * Time.deltaTime);
    }
}
