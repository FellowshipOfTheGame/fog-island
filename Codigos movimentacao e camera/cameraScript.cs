 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public GameObject player;
    public GameObject pontoCentro;//objeto pai da camera
    public Camera camera;

    public float minOffset = 1;
    public float maxOffset = 5;
    public float globalOffset = 1;
    private float yOffset;
    private float xOffset;
    private float zOffset;

    public float sens = 3;

    RaycastHit camHit;
    private Vector3 CamDist;

    // Start is called before the first frame update
    void Start()
    {
        CamDist = camera.transform.localPosition;
        Cursor.visible = false;
        yOffset = globalOffset/2;
    }

    // Update is called once per frame
    void Update()
    {
        //dar o zoom out e zoom in na camera
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (globalOffset < maxOffset - 0.1f)
            {
                globalOffset += 0.2f;
            }
               
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (globalOffset > minOffset + 0.1f)
            {
                globalOffset -= 0.2f;
            }
                
        }
        attOffset();
        pontoCentro.transform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, player.transform.position.z + zOffset);//segue o player

        //rotacao da camera
        pontoCentro.transform.rotation = Quaternion.Euler(pontoCentro.transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") * sens/ 2, pontoCentro.transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * sens, pontoCentro.transform.rotation.eulerAngles.z);

        camera.transform.localPosition = CamDist;
        GameObject obj = new GameObject();
        obj.transform.SetParent(camera.transform.parent);
        obj.transform.localPosition = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, camera.transform.localPosition.z - globalOffset);//coloca o objeto logo atras da camera, para verificar se tem algo entre eles(parede)
    
        if(Physics.Linecast(pontoCentro.transform.position, obj.transform.position,out camHit))// se tiver objeto, ele entra no if e o camHit eh o objeto que esta entre
        {
            camera.transform.position = camHit.point;
            camera.transform.localPosition = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, camera.transform.localPosition.z + 0.1f);
        }
        Destroy(obj);
    }

    private void attOffset()//calcula de acordo com o a posicao da camera e o angulo para o player sempre estar no centro
    {
        Transform cameraTransform = pontoCentro.transform;
        Vector3 cameraAtras = cameraTransform.forward * -1;
        zOffset = cameraAtras.z * globalOffset;
        xOffset = cameraAtras.x * globalOffset;
        yOffset = globalOffset/2;

    }
}
