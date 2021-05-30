using UnityEngine;
using TMPro;

public class DragRigidbodySword : MonoBehaviour
{
    public AudioSource pickUpSwordSource;
    public AudioClip[] pickUpSounds;
    public string[] notPickUpQuotes;
    public GameObject pickUpTextPrefab;
    public Transform pickUpTextPosition;

    public float forceAmount = 500;

    public Rigidbody selectedRigidbody;
    public Vector3 desiredPosition;
    public Vector2 screenSize;

    Camera targetcamera;
    Vector3 originalScreenTargetPosition;
    Vector3 originalRigidbodyPos;

    float selectionDistance;
    bool isRigidbodySelected;
    public bool getPositionFromCamera;

    public Vector3 trackerPosition = Vector3.zero;

    private void Start()
    {
        targetcamera = GetComponent<Camera>();
        isRigidbodySelected = false;
        getPositionFromCamera = false;
        trackerPosition = Vector3.zero;
    }

    private void Update()
    {
        trackerPosition.x = UdpManager.instance.CurrentData.XPosition;
        trackerPosition.y = UdpManager.instance.CurrentData.YPosition;

        if (Input.GetKeyDown(KeyCode.C))
        {
            getPositionFromCamera = !getPositionFromCamera;
        }

        screenSize.y = Screen.height;
        screenSize.x = Screen.width;

        desiredPosition = !getPositionFromCamera ? Input.mousePosition : trackerPosition;

        if (!targetcamera)
            return;

        if (Input.GetMouseButtonDown(0) && !isRigidbodySelected)
        {
            // Grab
            isRigidbodySelected = true;
            selectedRigidbody = GetRigidbodyFromMouseClick();

            if (selectedRigidbody == null)
            {
                isRigidbodySelected = false;
                Debug.Log("This GameObject has no Rigidbody");

                int randomQuote = Random.Range(0, notPickUpQuotes.Length);

                TextMeshPro text = Instantiate(pickUpTextPrefab, pickUpTextPosition.position, Quaternion.identity).GetComponent<TextMeshPro>();
                text.text = notPickUpQuotes[randomQuote];

                pickUpSwordSource.clip = pickUpSounds[1];
                pickUpSwordSource.volume = 0.6f;
                pickUpSwordSource.Play();
                StartCoroutine(FindObjectOfType<CameraShake>().Shake(.1f, .1f));
            }
            else
            {
                if (selectedRigidbody.GetComponent<SwordManager>() != null)
                {
                    TextMeshPro text = Instantiate(pickUpTextPrefab, pickUpTextPosition.position, Quaternion.identity).GetComponent<TextMeshPro>();
                    text.text = "SWORD!";

                    pickUpSwordSource.clip = pickUpSounds[0];
                    pickUpSwordSource.volume = 1f;
                    pickUpSwordSource.Play();
                    StartCoroutine(FindObjectOfType<CameraShake>().Shake(.5f, .2f));
                    selectedRigidbody.GetComponent<SwordManager>().SwordSelected();

                    StartCoroutine(FindObjectOfType<GameManager>().StartLevel());

                    Cursor.lockState = CursorLockMode.Confined;
                }
                else
                {
                    Debug.Log("This is not a Sword");
                    selectedRigidbody = null;
                    isRigidbodySelected = false;

                    int randomQuote = Random.Range(0, notPickUpQuotes.Length);

                    TextMeshPro text = Instantiate(pickUpTextPrefab, pickUpTextPosition.position, Quaternion.identity).GetComponent<TextMeshPro>();
                    text.text = notPickUpQuotes[randomQuote];

                    pickUpSwordSource.clip = pickUpSounds[1];
                    pickUpSwordSource.volume = 0.6f;
                    pickUpSwordSource.Play();
                    StartCoroutine(FindObjectOfType<CameraShake>().Shake(.1f, .1f));
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (selectedRigidbody)
        {
            Vector3 mousePositionOffset = targetcamera.ScreenToWorldPoint(new Vector3(desiredPosition.x, desiredPosition.y, selectionDistance)) - originalScreenTargetPosition;
            selectedRigidbody.velocity = (originalRigidbodyPos + mousePositionOffset - selectedRigidbody.transform.position) * forceAmount * Time.deltaTime;
        }
    }

    Rigidbody GetRigidbodyFromMouseClick()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = targetcamera.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out hitInfo);
        if (hit)
        {
            if (hitInfo.collider.gameObject.GetComponent<Rigidbody>())
            {
                selectionDistance = Vector3.Distance(ray.origin, hitInfo.point);
                originalScreenTargetPosition = targetcamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance));
                originalRigidbodyPos = hitInfo.collider.transform.position;

                return hitInfo.collider.gameObject.GetComponent<Rigidbody>();
            }
        }

        return null;
    }
}
