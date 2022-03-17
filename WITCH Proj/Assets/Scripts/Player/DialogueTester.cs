using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueTester : MonoBehaviour
{
    private Conversation conversation;
    private bool lockConversation = false;

    private StarterAssets.StarterAssetsInputs inputs;
    private PlayerInput _input;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        conversation = GetComponent<Conversation>();
        inputs = GetComponent<StarterAssets.StarterAssetsInputs>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.GetComponent<Conversation>() != null && lockConversation == false)
        {
            conversation = hit.gameObject.GetComponent<Conversation>();
            LockConversation();
        }
    }

    private void LockConversation()
    {
        conversation.ConvoLocker += AllowConversation;

        lockConversation = true;

        conversation.ConversationStart();
        inputs.cursorLocked = false;
        inputs.cursorInputForLook = false;

        _input.actions.FindActionMap("Conversation").Enable();
        _input.actions.FindActionMap("Player").Disable();
        //_input.currentActionMap = _input.actions.FindActionMap("Conversation");

        //prevent cursor from locking
        inputs.cursorPersist = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void AllowConversation(bool blockConvo) //this should be on the player itself, but due to difficulties i temporarily put it here
    {
        lockConversation = blockConvo;

        if (blockConvo == false)
        {
            conversation.ConvoLocker -= AllowConversation;
            inputs.cursorLocked = true;
            inputs.cursorInputForLook = true;

            _input.actions.FindActionMap("Player").Enable();
            _input.actions.FindActionMap("Conversation").Disable();

            inputs.cursorPersist = false;
            Cursor.visible = false;
        }
    }
}
