using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Serialization;

public class DroneScript : MonoBehaviour
{
    /*
    // Drone characteristics
    public float speed;
    public float health;
    public float attackRange; // where it will attack instead of moving
    
    // Sound
    private AudioSource m_MyAudioSource;
    public AudioClip[] droneSounds;
    
    // Attack things
    public float attackDamage;
    public float attackCooldown;
    private float timeToNextFire = 0;
    public float engageDistance; // how far will it go to engage enemy
    public ParticleSystem flash;

    private bool wasTargetDestroyed = false;
    private float timeToFindTarget = 0;
    
    // Pathfinding part
    [FormerlySerializedAs("initial_position")] public Vector3 initialPosition;
    public GameObject player;
    public GameObject initialPositionObject; 

    public void takeDamage(float damage)
    {
        if (health - damage > 0)
        {
            health -= damage;
        }
        else
        {
            droneDie();
        }
    }
    public void droneDie()
    {
        // Play sounds
        Destroy(gameObject);
    }

    public void droneAttack()
    {
        if (player != null)
        {
            m_MyAudioSource.PlayOneShot(droneSounds[0]);
            flash.Play();
            player.GetComponent<cubeScipt>().DamagePlayer(attackDamage);
            if (player == null)
            {
                wasTargetDestroyed = true;
                timeToFindTarget = Time.time + 20f;
            }
        }
        
    }
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        m_MyAudioSource = GetComponent<AudioSource>();

        initialPosition = gameObject.transform.position;
        initialPositionObject.transform.parent = null;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null && Time.time > timeToFindTarget)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
        if (Vector3.Distance(gameObject.transform.position, initialPosition) < engageDistance)
        {
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) > attackRange)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.deltaTime);
                
                Vector3 targetDirection = player.transform.position - transform.position;
                
                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 3, 0.0f);

                // Draw a ray pointing at our target in
                Debug.DrawRay(transform.position, newDirection, Color.red);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                gameObject.transform.rotation = Quaternion.LookRotation(newDirection); 
            }
            else
            {
                Vector3 targetDirection = player.transform.position - transform.position;
                
                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 3, 0.0f);

                // Draw a ray pointing at our target in
                Debug.DrawRay(transform.position, newDirection, Color.red);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                gameObject.transform.rotation = Quaternion.LookRotation(newDirection); 
                
                if (Time.time > timeToNextFire)
                {
                    timeToNextFire = Time.time + attackCooldown;

                    droneAttack();
                }

            }
        }

        if (wasTargetDestroyed)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, initialPosition, speed * Time.deltaTime);
                
            Vector3 targetDirection = initialPosition - transform.position;
                
            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 3, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            gameObject.transform.rotation = Quaternion.LookRotation(newDirection); 
        }
    }*/
}
 