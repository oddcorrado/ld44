using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem particleSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InputRouter>() == null) return;

        Debug.Log("transform");
        collision.gameObject.SetActive(false);
        animator.SetBool("go", true);
        particleSystem.Stop();
    }
}
