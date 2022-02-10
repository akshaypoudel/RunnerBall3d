using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveLogic : MonoBehaviour
{
    public float MovementSpeed = 15f;
    public float SideScrollSpeed = 7f;
    public float jumpDistance = 7f;
    public Rigidbody rb;
    public float playerMaxDownPosition=1f;
    public GameObject Player,GameOverUI;
    public bool isGrounded = true;
    public AudioClip GameOver;
    // public ParticleSystem stars;
    public Text CoinText;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // groundTile=FindObjectOfType<GroundTile>();
    }
    private void FixedUpdate()
    {
        float Hmovement = Input.GetAxis("Horizontal")*SideScrollSpeed;
        transform.Translate(
            new Vector3(Hmovement, 0, MovementSpeed)*Time.deltaTime
            );
        if (Player.transform.position.y < playerMaxDownPosition)
        {
            AudioSource.PlayClipAtPoint(GameOver, transform.position, 0.3f);
            GameOverUI.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("Player is out of the track: ");
        }
        CoinText.text=CoinScript.CoinValue.ToString();

    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpDistance, 0), ForceMode.Impulse);
            isGrounded = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            // AudioSource.PlayClipAtPoint(GameOver, transform.position , 0.3f);        
            GameOverUI.SetActive(true);
            Time.timeScale=0f;
            Debug.Log("Collided with Enemy: ");
        }
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
 
    public void Continue()
    {
        SceneManager.LoadScene(1);
    }
    
}
