using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Attributes")]
    [Tooltip("The base speed attribute of the player, before modifiers and boosters")]
    public float speed = 300f;

    [Tooltip("The time it takes for the player to respawn after shooting")] 
    public float respawnTime = 2f;

    // Local Attributes
    bool initialShoot;
    bool canShoot;
    GameObject player;
    Vector3 originalPos;
    Vector3 shootDirection;

    // References
    Camera cam;
    Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    private void Start() {
        originalPos = transform.position;
        canShoot = true;
    }

    private void FixedUpdate() {
        HandleDirection();

        if (Input.GetMouseButtonDown(0) && canShoot) {
            Shoot(shootDirection, speed);
            StartCoroutine(ManageRespawn(respawnTime));
            initialShoot = false;
            canShoot = false;
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            transform.position = originalPos;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

    }

    void HandleDirection() {
        if (Input.GetMouseButton(1)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            shootDirection = ray.direction;
            shootDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(shootDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
        }

        else {
            if (canShoot) Focus();
        }

    }

    void Shoot(Vector3 direction, float force) {

        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    void Focus() {
        if (shootDirection != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(shootDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3 * Time.deltaTime);
        }
    }

    IEnumerator ManageRespawn(float timeInSeconds) {
        yield return new WaitForSeconds(timeInSeconds);
        transform.position = originalPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        canShoot = true;
        Debug.Log("Player Respawned");
        yield return null;
    }

}
