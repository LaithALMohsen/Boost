using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{

    private Rigidbody rb;
    private AudioSource audio; 

    [SerializeField] float Speed = 1f;
    [SerializeField] float RotatSpeed = 1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem boostPartical;
    [SerializeField] ParticleSystem leftPartical;
    [SerializeField] ParticleSystem rightPartical;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

       audio.Stop();
    }

   
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

 void ProcessThrust()
    {

       if(Input.GetKey(KeyCode.Space))
        {
            // «·ﬁÊ… «·‰”»Ì…   ” Œœ„ ··Õ—ﬂ… ⁄·Ï «·„Õ«Ê— «·À·«À… 
            rb.AddRelativeForce(Vector3.up * Speed * Time.deltaTime);
            StartThrusting();

        }

        else
            StopThrusting();

    }


    private void StartThrusting()
    {
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(mainEngine);
        }

        if (!boostPartical.isPlaying)
        {
            boostPartical.Play();
        }
    }
    private void StopThrusting()
    {
        audio.Stop();
        boostPartical.Stop();

    }

    void ProcessRotation() { 
        
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();

        }


    }
    private void RotateLeft()
    {
        RotatRocket(RotatSpeed);
        if (!rightPartical.isPlaying)
        {
            rightPartical.Play();
        }
    }
    private void RotateRight()
    {
        RotatRocket(-RotatSpeed);

        if (!leftPartical.isPlaying)
        {

            leftPartical.Play();

        }

        else
        {
            StopRoatate();

        }
    }

    private void StopRoatate()
    {
        rightPartical.Stop();
        leftPartical.Stop();
    }


    public void RotatRocket(float rotationThisFrame)
    {


        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);

        rb.freezeRotation = false; 
    }
}
