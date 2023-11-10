using UnityEngine;

public class recoil : MonoBehaviour
{
    private Vector3 currentRotation;
    private Vector3 targetRotation;
    public Transform holder;

    public Transform camera;

    [SerializeField] private float recoilX, recoilY, recoilZ, snappiness, returnSpeed;

     
    

    void Start()
    {
        
    }
    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire() {
        Debug.Log("solah music");
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), recoilZ);
    }
}
