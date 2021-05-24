using UnityEngine;

public class DragRigidbodySword : MonoBehaviour
{
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
            }
            else
            {
                if (selectedRigidbody.GetComponent<SwordManager>() != null)
                {
                    Debug.Log("SWORD");
                    Debug.Log(selectedRigidbody.name);
                    selectedRigidbody.GetComponent<SwordManager>().SwordSelected();
                }
                else
                {
                    Debug.Log("This is not a Sword");
                    selectedRigidbody = null;
                    isRigidbodySelected = false;
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
