using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    public int numberOfLegs;
    public GameObject[] legTargets;
    public GameObject[] legRacycastOrigins;
    public GameObject[] legRacycastDirections;
    public bool[] moveLeg;
    public bool[] isGrounded;
    public Vector3[] legTargetOrigins;   // used for animation
    public Vector3[] legTargetTargets;
    public float legMovementSpeed;
    public LayerMask layerMask;
    public float maxDistance;
    public float minDistance;
    public float speed;
    private int i=0;
    private float averageY = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numberOfLegs; i++)
        {
            legTargets[i].transform.parent = gameObject.transform.parent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Rotate(0, -90 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Rotate(0, 90 * Time.deltaTime, 0);
        }
        averageY = 0;
        foreach (GameObject leg in legTargets)
        {
            averageY += leg.transform.position.y;
        }
        averageY = averageY / numberOfLegs;
        transform.position = new Vector3(transform.position.x,averageY, transform.position.z);
    }/*
    int elapsedFrames =0;
    public int interpolationFramesCount = 45;
    private float startTime;
    private float journeyLength;
    public float distCovered;
    void legVars(int x)
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(legTargetOrigins[x], legTargetTargets[x]);
    }*/
    void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        i = 0;
        foreach (GameObject leg in legTargets)
        {
            
            if (Physics.Raycast(legRacycastOrigins[i].transform.position, (legRacycastDirections[i].transform.position - legRacycastOrigins[i].transform.position).normalized, out hit, Mathf.Infinity, layerMask))
            {
                if (Vector3.Distance(legTargets[i].transform.position , hit.point) >= maxDistance)
                {
                    //Debug.Log(maxDistance);
                    //Debug.Log(Vector3.Distance(legTargets[i].transform.position, hit.point));
                    //  legTargets[i].transform.position = hit.point;
                    if (i < (numberOfLegs / 2) && isGrounded[i] && isGrounded[i+ (numberOfLegs / 2)])
                    {
                        moveLeg[i] = true;
                        isGrounded[i] = false;
                    }
                    else if (i >= (numberOfLegs / 2) && isGrounded[i] && isGrounded[i - (numberOfLegs / 2)])
                    {
                        moveLeg[i] = true;
                        isGrounded[i] = false;
                    }

                    legTargetOrigins[i] = legTargets[i].transform.position;
                    legTargetTargets[i] = hit.point;
                    
                    //legVars(i);
                }
                if (moveLeg[i])
                {
                    legTargets[i].transform.position = Vector3.MoveTowards(legTargets[i].transform.position, legTargetTargets[i], legMovementSpeed);
                    if(Vector3.Distance(legTargets[i].transform.position, legTargetTargets[i]) <= minDistance)
                    {
                        moveLeg[i] = false;
                        isGrounded[i] = true;
                    }
                }
                
                Debug.DrawRay(legRacycastOrigins[i].transform.position, (legRacycastDirections[i].transform.position - legRacycastOrigins[i].transform.position).normalized * hit.distance, Color.yellow);
                Debug.DrawRay(legTargetOrigins[i], (legTargetTargets[i] - legTargetOrigins[i]).normalized * hit.distance, Color.blue);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(legRacycastOrigins[i].transform.position, (legRacycastDirections[i].transform.position - legRacycastOrigins[i].transform.position).normalized * 1000, Color.white);
                Debug.Log("Did not Hit");
            }

            /*

            float av1=0,av2=0;
            if (i < (numberOfLegs / 2) )
            {
                av1+=legTargets[i].transform.position.y;
            }
            else if (i >= (numberOfLegs / 2))
            {
                av2 += legTargets[i].transform.position.y;
            }
            av1 = av1 / 4;
            av2 = av2 / 4;
            transform.rotation = Quaternion.LookRotation( new Vector3(av1, 0,0 ) - new Vector3(av2, 0,0), Vector3.right);
            // transform.rotation = Quaternion.LookRotation(new Vector3(0, av1/4, 0) - new Vector3(0, av2/4, 0), Vector3.right);
            */


            i++;
        }
    }
}
