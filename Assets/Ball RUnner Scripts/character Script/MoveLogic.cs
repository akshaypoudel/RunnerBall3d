using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MoveLogic : MonoBehaviour
{
    [Header("KeyboardMovingParameters")]
    public float playerMovementSpeed = 15f;
    
    public static int numberOfDonuts;
    public static int numberOfDiamonds;
    private int whichSkin;

    public float SideScrollSpeed = 7f;
    public float jumpHeight = 7f;
    private float Hmovement;

    [HideInInspector] public int temporaryDonutCount;
    [HideInInspector] public int temporaryDiamondCount;

    public TMP_Text donutText;
    public TMP_Text diamondText;

    public ParticleSystem DonutConsumeEffect;
    public ParticleSystem DiamondConsumeEffect;

    public Rigidbody rigidBody;
    public GameObject Player, GameOverUI;
    public GameObject pauseMenu;
    public GameObject PlayerSphere;
    public Material[] skinsMaterial;


    public bool isGrounded = true;
    private bool canBallMove = true;
    public static bool isGameOver = false;
    public Button jumpButton;
    public float max_Y_PositionForGameOver;

    private string Prefscontrols = "PrefsControls";
    private string indexOfMat = "indexOfMaterial1";

    public string encryptedDonutPrefs = "EncryptedDonut";
    public string encryptedDiamondPrefs = "EncryptedDiamond";

    [HideInInspector]public int tempControlsValue; //if this is 1 than touch controls will activate
    //and if its 2 then gyro controls will activate.
   
    
    PlayerPrefsSaveSystem playerPrefsSaveSystem = new PlayerPrefsSaveSystem();
    

    [Header("TouchMoveParameters")]
    public float controllingSpeedForAndroidTouch;
    private Touch touch;
    public float minClampX;
    public float maxClampX;

    private void Start()
    {
        isGameOver = false;
        whichSkin = PlayerPrefs.GetInt(indexOfMat);
        PlayerSphere.GetComponent<Renderer>().material=skinsMaterial[whichSkin];
        jumpButton.onClick.AddListener(Jump);


        if(PlayerPrefs.HasKey(Prefscontrols))
            tempControlsValue = PlayerPrefs.GetInt(Prefscontrols);
        else
            tempControlsValue = 0;

    }


    //FixedUpdate Function run once per frame
    private void FixedUpdate()
    {
        if (canBallMove)
        { 
            if(tempControlsValue==0)
                TouchControls();
            if(tempControlsValue==1)
                TiltControls();
        }
        
        if (transform.position.y < max_Y_PositionForGameOver)
            Gameover();
    }
    public void Jump()
    {
        if (isGrounded)
        {
            SoundManager.PlaySound(SoundManager.Sound.Jump);
            isGrounded = false;
            rigidBody.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }
    }
    private void KeyboardControls()
    {
        Hmovement = Input.GetAxis("Horizontal") * SideScrollSpeed;

        transform.Translate(new Vector3(Hmovement, 0, playerMovementSpeed) * Time.deltaTime);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x,minClampX, maxClampX),
                        transform.position.y,
                        transform.position.z);
    }
    private void TiltControls()
    {
        transform.Translate(new Vector3(Input.acceleration.x * SideScrollSpeed, 0, playerMovementSpeed) * Time.deltaTime);
        
        transform.position = new Vector3(
           Mathf.Clamp(transform.position.x, minClampX, maxClampX),
                        transform.position.y,
                        transform.position.z);
    }
    public void TouchControls()
    {
        transform.Translate(new Vector3(0, 0, playerMovementSpeed) * Time.deltaTime);
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                transform.position.x + touch.deltaPosition.x * controllingSpeedForAndroidTouch / 2,
                transform.position.y,
                transform.position.z);

                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, minClampX, maxClampX),
                                transform.position.y,
                                transform.position.z);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.collider.CompareTag("Enemy") && this.gameObject.tag == "Player")
        {
            Gameover();
        }
        if (collision.collider.CompareTag("Tree") && isGrounded && this.gameObject.tag == "Player")
        {
            Gameover();
        }

    }
    private void Gameover()
    {
        SoundManager.PlaySound(SoundManager.Sound.GameOver);
        isGameOver = true;
        canBallMove = false;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GameOverUI.SetActive(true);
        SaveDonut();
        SaveDiamond();
    }

    public void SaveDonut()
    {
        int tempDonut = numberOfDonuts;
        numberOfDonuts = 0;
        playerPrefsSaveSystem.EncryptPrefsPositive(tempDonut,encryptedDonutPrefs);
    }
    private void SaveDiamond()
    {
        int tempDiamond = numberOfDiamonds;
        numberOfDiamonds = 0;
        playerPrefsSaveSystem.EncryptPrefsPositive(tempDiamond,encryptedDiamondPrefs);
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Coin"))
        {
            SoundManager.PlaySound(SoundManager.Sound.DonutCollect);
            temporaryDonutCount++;
            numberOfDonuts++;
            donutText.text = temporaryDonutCount.ToString();

            if (DonutConsumeEffect.isPlaying)
                DonutConsumeEffect.Stop();

            DonutConsumeEffect.Play();
            Destroy(target.gameObject);
        }
        if(target.gameObject.CompareTag("Diamond"))
        {
            SoundManager.PlaySound(SoundManager.Sound.DiamondCollect);
            temporaryDiamondCount++;
            numberOfDiamonds++;
            diamondText.text = temporaryDiamondCount.ToString();

            if(DiamondConsumeEffect.isPlaying)
                DiamondConsumeEffect.Stop();

            DiamondConsumeEffect.Play();
            Destroy (target.gameObject);

        }
    }

    public void Continue()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        numberOfDonuts = 0; //by setting numberOfDonuts = 0, we are resetting the value of coin
        //after we hit play again button
        this.gameObject.tag = "Hello";
        canBallMove = true;
        GameOverUI.SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        canBallMove = true;
        StartCoroutine(Invincible());

    }
    IEnumerator Invincible()
    {
        yield return new WaitForSeconds(10f);
        this.gameObject.tag = "Player";

    }


}