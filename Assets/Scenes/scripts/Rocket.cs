using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsthrust = 100f;
    [SerializeField] float mainthrust = 10f;
    [SerializeField] AudioClip mainengine;
    [SerializeField] AudioClip joy;
    [SerializeField] AudioClip death;
    [SerializeField] ParticleSystem mainengineparticles;
    [SerializeField] ParticleSystem joyparticles;
    [SerializeField] ParticleSystem deathparticles;
    [SerializeField] int nextlevel;
    [SerializeField] int currentlevel;
    Rigidbody rigidBody;
    AudioSource audioSource;
    enum State { Dying, transcending, alive }
    State state = State.alive;
    
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {    if(state!=State.alive)
        {
            return;
        }
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence()
    {
        print("Dead");
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathparticles.Play();
          
       Invoke("LoadFirstLevel", 1f);
    }

    private void StartSuccessSequence()
    {
        print("Hit finish");
        state = State.transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(joy);
        joyparticles.Play();
        
        Invoke("LoadNextScene", 1f);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(currentlevel);
    }

    private void LoadNextScene()
    {
       
        SceneManager.LoadScene(nextlevel);
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();

        }
        else
        {
            audioSource.Stop();
            mainengineparticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainthrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainengine);

        }
        mainengineparticles.Play();
    }

    void Update()
    {
        if (state == State.alive)
        {
            Thrust();
            Rotate();
        }
      
       
    }

    private void Rotate()
    {
        
        float Rotationthisframe = rcsthrust * Time.deltaTime;
        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward*Rotationthisframe);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * Rotationthisframe);

        }
        rigidBody.freezeRotation = false;
    }

   
}
