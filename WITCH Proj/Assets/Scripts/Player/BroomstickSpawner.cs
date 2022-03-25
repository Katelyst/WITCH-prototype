using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using UnityEngine.Assertions;
using ScriptableData;

public class BroomstickSpawner : MonoBehaviour
{
    //spawns broomstick
    //changes parent of player following cam
    //changes control scheme?
    public bool playerFlying;

    private StarterAssetsInputs _GroundInput;
    private StarterAssetsInputs _FlyInput;
    private PlayerInput _GroundPlayerInput;
    private PlayerInput _FlyPlayerInput;
    private bool mounted = false;

    [SerializeField]
    private GameObject broomstick;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera virtualCam;
    [SerializeField]
    private GameObject cinemachineTarget0, cinemachineTarget1;  //ground and air player targets respectively

    [SerializeField]
    private ParticleSystem mountUpParticleSystem;


    [SerializeField][Range(0.0f, 10.0f)]
    private float takeoffOffset = 5.0f; 

    //[SerializeField]
    //private AudioClip MountBroomAudio; FOR WHEN WE WANT TO IMPLEMENTAUDIO :)
    private Transform followTrans;

    [Header("Audio Events")]
    [SerializeField]
    private SDBool WitchMountEvent;
    private SDGameObject gameObj;
    //if not using ScriptableData at top of file, add it before type
    //private ScriptableData.SDBool WitchMountEvent;

    void Start()
    {
        virtualCam = GameObject.FindWithTag(Tags.followCameraTag).GetComponent<Cinemachine.CinemachineVirtualCamera>();
        Assert.IsTrue(virtualCam);  //If this asserts it means you have no PlayerFollowCamera-like obj in your scene. Reference playground scene.



        player = GameObject.FindWithTag(Tags.playerTag);
        Assert.IsTrue(player);
        broomstick = GameObject.FindWithTag(Tags.broomstickTag);
        Assert.IsTrue(broomstick);

        _GroundInput = player.GetComponent<StarterAssetsInputs>();
        _GroundPlayerInput = player.GetComponent<PlayerInput>();

        _FlyInput = broomstick.GetComponent<StarterAssetsInputs>();
        _FlyPlayerInput = broomstick.GetComponent<PlayerInput>();

        if (!mounted)
        {
            ActivateGroundPlayer();
        }
        else
        {
            ActivateFlyPlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_GroundInput.mount || _FlyInput.dismount)
        {
            //Debug.Log("Mounting broomstick");
            _GroundInput.mount = false;   //reset input value
            _FlyInput.dismount = false;

            // determine if broomstick should be spawned or destroyed, and if player should be spawned or destroyed. 
            mounted = !mounted;

            if(mounted)
            {
                ActivateFlyPlayer();
                WitchMountEvent.Invoke(mounted);
            }
            else
            {
                ActivateGroundPlayer();
                WitchMountEvent.Invoke(mounted);
            }
        }
    }

    private void ActivateGroundPlayer()
    {
        //turn OFF broomstick player input, turn ON ground player input
        _FlyPlayerInput.enabled = false;
        _GroundPlayerInput.enabled = true;

        //match rotation of broomstick, except on Y axis
        Quaternion playerRot = new Quaternion();
        playerRot.eulerAngles = new Vector3(
            0,
            broomstick.transform.rotation.eulerAngles.y,
            0
        );

        player.transform.rotation = playerRot;
        player.transform.position = broomstick.transform.position;

        playerFlying = false;
        virtualCam.Follow = cinemachineTarget0.transform;

        broomstick.SetActive(false);
        player.SetActive(true);

        mountUpParticleSystem.Stop();
    }

    private void ActivateFlyPlayer()
    {
        //turn ON broomstick player input, turn OFF ground player input
        _FlyPlayerInput.enabled = true;
        _GroundPlayerInput.enabled = false;
        
        //match rotation of player, except on Y axis
        Quaternion broomstickRot = new Quaternion();
        broomstickRot.eulerAngles = new Vector3(
            player.transform.rotation.eulerAngles.x,
            player.transform.rotation.eulerAngles.y,
            player.transform.rotation.eulerAngles.z
        );

        broomstick.transform.rotation = broomstickRot;
        broomstick.transform.position = player.transform.position + new Vector3(0.0f, takeoffOffset, 0.0f);

        playerFlying = true;
        virtualCam.Follow = cinemachineTarget1.transform;

        broomstick.SetActive(true);
        player.SetActive(false);

        mountUpParticleSystem.Play();
    }
}
