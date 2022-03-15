using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using UnityEngine.Assertions;

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

    [SerializeField][Range(0.0f, 10.0f)]
    private float takeoffOffset = 5.0f; 

    private Transform followTrans;

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
            Debug.Log("Mounting broomstick");
            _GroundInput.mount = false;   //reset input value
            _FlyInput.dismount = false;

            if(_FlyInput.dismount)
            {
                Debug.Log("Dismounting broomstick");
            }

            // determine if broomstick should be spawned or destroyed, and if player should be spawned or destroyed. 
            mounted = !mounted;

            if(mounted)
            {
                ActivateFlyPlayer();
            }
            else
            {
                ActivateGroundPlayer();
            }
        }
    }

    private void ActivateGroundPlayer()
    {
        //turn OFF broomstick player input, turn ON ground player input
        _FlyPlayerInput.enabled = false;
        _GroundPlayerInput.enabled = true;

        broomstick.SetActive(false);
        player.SetActive(true);

        player.transform.position = broomstick.transform.position;

        playerFlying = false;
    }

    private void ActivateFlyPlayer()
    {
        //turn ON broomstick player input, turn OFF ground player input
        _FlyPlayerInput.enabled = true;
        _GroundPlayerInput.enabled = false;
        
        broomstick.SetActive(true);
        player.SetActive(false);

        broomstick.transform.position = player.transform.position + new Vector3(0.0f, takeoffOffset, 0.0f);

        playerFlying = true;
    }
}
