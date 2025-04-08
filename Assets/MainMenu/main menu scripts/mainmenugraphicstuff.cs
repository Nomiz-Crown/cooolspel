using UnityEngine;

public class mainmenugraphicstuff : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    public float followStrength = 0.05f;
    public float maxOffset = 30f;

    private Vector3 initialPosition;

    void Start()
    {
        if (targetObject != null)
        {
            initialPosition = targetObject.transform.localPosition;
        }
    }

    void Update()
    {
        if (targetObject == null) return;

        Vector3 mousePos = Input.mousePosition;

        float screenX = (mousePos.x / Screen.width) * 2 - 1;
        float screenY = (mousePos.y / Screen.height) * 2 - 1;

        Vector3 targetOffset = new Vector3(screenX, screenY, 0) * maxOffset;

        targetObject.transform.localPosition = Vector3.Lerp(
            targetObject.transform.localPosition,
            initialPosition + targetOffset,
            followStrength
        );
    }

    // Method to update max offset (to be called by the mainmenuhandler script)
    public void SetMaxOffset(float newOffset)
    {
        maxOffset = newOffset;
    }
}
