using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerController : MonoBehaviour
{
    private Rigidbody PlayerRb;
    public float speed = 5.0f;
    private GameObject focalPoint;
    public bool hasPowerup;
    private float powerupStrength = 15f;
    public GameObject powerupIndicator;

    public PowerUpType currentPowerUp = PowerUpType.None; 
    public GameObject rocketPrefab;
    private GameObject tmpRocket; 
    private Coroutine powerupCountdown;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float SidewardInput = Input.GetAxis("Horizontal");

        PlayerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        PlayerRb.AddForce(focalPoint.transform.right * speed * SidewardInput);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5F, 0);

        if (transform.position.y < -20)
        { SceneManager.LoadScene(0); }

        if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F)) { LaunchRockets(); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);

            if (powerupCountdown != null) 
            {
                StopCoroutine(powerupCountdown); 
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")  && currentPowerUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

            Debug.Log("Player collided with: " + collision.gameObject.name + " with powerup set to " + currentPowerUp.ToString());
        }
    }

    void LaunchRockets() { foreach (var enemy in FindObjectsOfType<Enemy>()) { tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity); tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform); } }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        currentPowerUp = PowerUpType.None;
        powerupIndicator.gameObject.SetActive(false);
    }

    //SceneManager.LoadScene(0);
}
