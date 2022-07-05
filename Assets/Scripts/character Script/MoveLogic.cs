using System.Collections;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveLogic : MonoBehaviour
{
    [Header("---------KeyboardMovingParameters--------\n")]
    public static float playerMovementSpeed = 15f;
    public float jumpHeight = 7f;
    private float Hmovement;



    [Header("---------Scriptable Objects--------\n")]
    public BallSkinMaterialSO ballSkinsMaterialSO;


    [Header("---------Basic Data Type---------\n")]

    public float playerInvincibleTime;
    public static int tempControlsValue; //if this is 1 than touch controls will activate
                                         //and if its 2 then gyro controls will activate.
    private int whichSkin;
    [HideInInspector] public int temporaryDonutCount;

    public int playAgainTimerStartTime;

    public static int numberOfDonuts;
    public static int numberOfDiamonds;

    public bool isGrounded = true;
    private bool canBallMove = true;
    public static bool isGameOver = false;

    public float max_Y_PositionForGameOver;
    [HideInInspector]public int temporaryDiamondCount;


    [Header("---------GameObjects---------\n")]
    public GameObject Stars;
    public GameObject GameOverUI;
    public GameObject pauseMenu;
    public GameObject pauseButton;
    private GameObject PlayerSphere;
    private Rigidbody rigidBody;
    public Button jumpButton;
    public Button jumpDownButton;
    public GameObject PlayAgainTimerPanel;
    public GameObject duplicatePlayerCollider;

    [Header("---------TextMeshPro---------\n")]
    public TMP_Text donutText;
    public TMP_Text diamondText;
    public TMP_Text playAgainWithDiamondText;
    public TMP_Text TimerText;
    public TMP_Text timerTextAfterPlayAgainClicked;  

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

    private string encryptedDonutPrefs = "EncryptedDonut";
    private string encryptedDiamondPrefs = "EncryptedDiamond";
    private string Prefscontrols = "PrefsControls";
    private string indexOfMat = "indexOfMaterial1";

    [Header("---------Touch Move Parameters---------\n")]
    public float minClampX;
    public float maxClampX;
    private Touch touch;



    private bool isTimerActive = false;
    private int startTime = 4;
    private float currentTime;


    private void Awake()
    {
        playerMovementSpeed = 15f;
    }
    private void Start()
    {
        InitializeVariables();
        InitializePlayerPrefs();
    }

    private void InitializeVariables()
    {
        isGameOver = false;
        whichSkin = PlayerPrefs.GetInt(indexOfMat);
       
        PlayerSphere = transform.GetChild(1).gameObject;
        
        rigidBody = GetComponent<Rigidbody>();
        PlayerSphere.GetComponent<Renderer>().material = ballSkinsMaterialSO.materialOfObject[whichSkin];
        
        jumpButton.onClick.AddListener(Jump);
        jumpDownButton.onClick.AddListener(JumpDown);


        currentTime = startTime;
    }

    private void InitializePlayerPrefs()
    {
        if (PlayerPrefs.HasKey(Prefscontrols))
            tempControlsValue = PlayerPrefs.GetInt(Prefscontrols);
        else
            tempControlsValue = 0;
    }

    private void Update()
    {
        if (transform.position.y < max_Y_PositionForGameOver)
            Gameover();

        if (isTimerActive)
            Timer();
    }

    private void FixedUpdate()
    {
        if (canBallMove)
        {
            if (tempControlsValue == 0)
                TouchControls();
            if (tempControlsValue == 1)
                GyroControls();
        }
    }
    public void Jump()
    {
        if (isGrounded)
        {
            SoundManager.PlaySound(SoundManager.Sound.Jump);
            isGrounded = false;
            rigidBody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
    public void JumpDown()
    {
        if(!isGrounded)
        {
            rigidBody.AddForce(Vector3.down*jumpHeight*2,ForceMode.Impulse);
        }
    }
    private void KeyboardControls()
    {
        Hmovement = Input.GetAxis("Horizontal") * PlayerSettings.playerGyroControlSpeed;

        transform.Translate(new Vector3(Hmovement, 0, playerMovementSpeed) *Time.deltaTime);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minClampX, maxClampX),
                        transform.position.y,
                        transform.position.z);
    }
    private void GyroControls()
    {
        transform.Translate(new Vector3(Input.acceleration.x * PlayerSettings.playerGyroControlSpeed, 0, playerMovementSpeed) * Time.deltaTime);

        transform.position = new Vector3(
           Mathf.Clamp(transform.position.x, minClampX, maxClampX),
                        transform.position.y,
                        transform.position.z);
    }
    private void TouchControls()
    {
        transform.Translate(new Vector3(0, 0, playerMovementSpeed) * Time.deltaTime);
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                transform.position.x + touch.deltaPosition.x * PlayerSettings.playerTouchControlSpeed / 2,
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
        else
        {
            isGrounded = false;
        }

        if (collision.collider.CompareTag("Enemy") && this.gameObject.tag == "Player")
        {
            Gameover();
        }
        if (collision.collider.CompareTag("Tree") && this.gameObject.tag == "Player")
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
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        playerRotation.canRotate = false;
        pauseButton.SetActive(false);

        //SaveDonutAndDiamond();
        RefreshTimer();

        UpdateUIText();

        Invoke("SetGameOverPanelActive", 1.5f);
    }

    private void UpdateUIText()
    {
        int n = playerPrefsSaveSystem.ReturnDecryptedScore(encryptedDiamondPrefs);
        playAgainWithDiamondText.text = n.ToString();
    }

    private void RefreshTimer()
    {
        timer.Refresh();
    }

    public void SaveDonutAndDiamond()
    {
        SaveDonut(numberOfDonuts);
        SaveDiamond(numberOfDiamonds);
    }

    private void SetGameOverPanelActive()
    {
        Stars.SetActive(true);

        timer.DiamondPlayAgainTXT.text = timer.diamondsValueForPlayAgain.ToString();
        timer.Refresh();
        bGDisable.DisableAllBgComponents();
        GameOverUI.SetActive(true);
    }

    public void SaveDonut(int donutsAmount)
    {
        playerPrefsSaveSystem.EncryptPrefsPositive(donutsAmount, encryptedDonutPrefs);
    }
    private void SaveDiamond(int diamondAmount)
    {
        playerPrefsSaveSystem.EncryptPrefsPositive(diamondAmount, encryptedDiamondPrefs);
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
            target.gameObject.SetActive(false);
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
            target.gameObject.SetActive(false);
           
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
        EnableBGComponentAndDeactivateStars();
        ResumePlayerMovement();
        timer.ResetTimer();
        currentTime = startTime;
        
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
        timerTextAfterPlayAgainClicked.transform.parent.gameObject.SetActive(false);
        GameOverUI.SetActive(false);
        pauseButton.SetActive(true);   
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        playerRotation.canRotate = true;
        canBallMove = true;
    }

    public void StartTimerForNewLife()
    {
        Time.timeScale = 1f;
        timerTextAfterPlayAgainClicked.transform.parent.gameObject.SetActive(true);
        timerTextAfterPlayAgainClicked.text = "";
        GameOverUI.SetActive(false);
        isTimerActive = true;
        StartCoroutine(DestroyNearbyObjects());

    }
    private void Timer()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            isTimerActive = false;
            TimerText.gameObject.SetActive(false);
            PlayAgain();
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timerTextAfterPlayAgainClicked.text = time.Seconds.ToString();
    }
}