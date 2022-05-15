using System.Collections;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveLogic : MonoBehaviour
{
    [Header("---------KeyboardMovingParameters--------\n")]
    public float playerMovementSpeed = 15f;
    public float SideScrollSpeed = 7f;
    public float jumpHeight = 7f;
    private float Hmovement;



    [Header("---------Scriptable Objects--------\n")]
    public BallSkinMaterialSO ballSkinsMaterialSO;


    [Header("---------Basic Data Type---------\n")]

    public float playerInvincibleTime;
    [Range(0, 1)] public static int tempControlsValue; //if this is 1 than touch controls will activate
                                                       //and if its 2 then gyro controls will activate.
    private int whichSkin;
    [HideInInspector] public int temporaryDonutCount;

    public int playAgainTimerStartTime;
    private float currentTime;

    public static int numberOfDonuts;
    public static int numberOfDiamonds;
    public bool isGrounded = true;
    private bool canBallMove = true;
    public static bool isGameOver = false;
    public float max_Y_PositionForGameOver;
    [HideInInspector] public int temporaryDiamondCount;


    [Header("---------GameObjects---------\n")]
    public GameObject Stars;
    public GameObject Player, GameOverUI;
    public GameObject pauseMenu;
    public GameObject PlayerSphere;
    public Rigidbody rigidBody;
    public Button jumpButton;
    public GameObject PlayAgainTimerPanel;
    public GameObject duplicatePlayerCollider;

    [Header("---------TextMeshPro---------\n")]
    public TMP_Text donutText;
    public TMP_Text diamondText;
    public TMP_Text TimerText;

    [Header("---------Scripts---------\n")]
    public BGDisable bGDisable;
    public Timer timer;
    public RotateGameObject playerRotation;
    [SerializeField]private InterstialAds interstialAds;
    PlayerPrefsSaveSystem playerPrefsSaveSystem = new PlayerPrefsSaveSystem();


    [Header("---------Particle System---------\n")]

    public ParticleSystem DonutConsumeEffect;
    public ParticleSystem DiamondConsumeEffect;
    public ParticleSystem PlayAgainEffect;

    [Header("---------Strings---------\n")]

    public string encryptedDonutPrefs = "EncryptedDonut";
    public string encryptedDiamondPrefs = "EncryptedDiamond";
    private string Prefscontrols = "PrefsControls";
    private string indexOfMat = "indexOfMaterial1";

    [Header("---------Touch Move Parameters---------\n")]
    public float controllingSpeedForAndroidTouch;
    private Touch touch;
    public float minClampX;
    public float maxClampX;

    private void Start()
    {
        isGameOver = false;
        whichSkin = PlayerPrefs.GetInt(indexOfMat);
        PlayerSphere.GetComponent<Renderer>().material = ballSkinsMaterialSO.materialOfObject[whichSkin];
        jumpButton.onClick.AddListener(Jump);

        currentTime = playAgainTimerStartTime * 1;

        if (PlayerPrefs.HasKey(Prefscontrols))
            tempControlsValue = PlayerPrefs.GetInt(Prefscontrols);
        else
            tempControlsValue = 0;

    }


    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
#endif

        if (transform.position.y < max_Y_PositionForGameOver)
            Gameover();
    }

    //FixedUpdate Function run once per frame
    private void FixedUpdate()
    {
        if (canBallMove)
        {
#if UNITY_EDITOR
            playerMovementSpeed = 8f;
            KeyboardControls();
#endif
#if UNITY_ANDROID

            if (tempControlsValue == 0)
                TouchControls();
            if (tempControlsValue == 1)
                TiltControls();
#endif
        }


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

        transform.Translate(new Vector3(Hmovement, 0, playerMovementSpeed) *Time.deltaTime);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minClampX, maxClampX),
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
        this.gameObject.tag = "Finish";
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        playerRotation.canRotate = false;
        SaveDonut();
        SaveDiamond();
        Invoke("SetGameOverPanelActive", 1.5f);
    }

    private void SetGameOverPanelActive()
    {
        Stars.SetActive(true);

        timer.DiamondPlayAgainTXT.text = timer.diamondsValueForPlayAgain.ToString();
        timer.Refresh();
        bGDisable.DisableAllBgComponents();
        GameOverUI.SetActive(true);
    }

    public void SaveDonut()
    {
        int tempDonut = numberOfDonuts;
        playerPrefsSaveSystem.EncryptPrefsPositive(tempDonut, encryptedDonutPrefs);
    }
    private void SaveDiamond()
    {
        int tempDiamond = numberOfDiamonds;
        playerPrefsSaveSystem.EncryptPrefsPositive(tempDiamond, encryptedDiamondPrefs);
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Coin") && (this.gameObject.tag == "Player" 
                                                      || this.gameObject.tag =="Finish"))
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
        if (target.gameObject.CompareTag("Diamond") && (this.gameObject.tag == "Player"
                                                      || this.gameObject.tag == "Finish"))
        {
            SoundManager.PlaySound(SoundManager.Sound.DiamondCollect);
            temporaryDiamondCount++;
            numberOfDiamonds++;
            diamondText.text = temporaryDiamondCount.ToString();

            if (DiamondConsumeEffect.isPlaying)
                DiamondConsumeEffect.Stop();

            DiamondConsumeEffect.Play();
            Destroy(target.gameObject);

        }
    }
    public void Continue()
    {
        SceneManager.LoadScene(1);
    }
    private void PauseTheTimer()
    {
        Time.timeScale = 0f;
    }
    private void ResumeTime()
    {
        Time.timeScale = 1f;
        PlayAgainEffect.Play();
    }
    public void PlayAgain()
    {
        ResumeTime();
        StartCoroutine(DestroyNearbyObjects());
        EnableBGComponentAndDeactivateStars();
        ResumePlayerMovement();
    }

    private void EnableBGComponentAndDeactivateStars()
    {
        Stars.SetActive(false);
        bGDisable.EnableAllBgComponents();
    }

    IEnumerator DestroyNearbyObjects()
    {
        duplicatePlayerCollider.transform.position=transform.position;
        duplicatePlayerCollider.GetComponent<SphereCollider>().enabled = true;
        yield return new WaitForSeconds(1f);
        duplicatePlayerCollider.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        this.gameObject.tag = "Player";

    }

    private void ResumePlayerMovement()
    {
        isGameOver = false;
        GameOverUI.SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        playerRotation.canRotate = true;
        canBallMove = true;
    }
}