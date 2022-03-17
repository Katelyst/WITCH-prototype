using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public Vector2 steering;
		public bool jump;
		public bool sprint;
		public bool mount;
		public bool dismount;
		public bool accelerate;
		public bool decelerate;
		public bool cursorPersist = false;


		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnMount(InputValue value)
        {
			MountInput(value.isPressed);
		}

		public void OnDismount(InputValue value)
        {
			DismountInput(value.isPressed);

		}

		//also add steering
		public void OnSteer(InputValue value)
        {
			//Debug.Log("Steering inputs: " + value.Get<Vector2>());
			SteeringInput(value.Get<Vector2>());
        }

		//also add accellerating
		public void OnAccelerate(InputValue value)
        {
			AccelerateInput(value.isPressed);
        }

		//also add deccellerating
		public void OnDecelerate(InputValue value)
        {
			DecelerateInput(value.isPressed);
        }

		public void OnClick(InputValue value)
        {
			//for clicking
        }

		public void OnPoint(InputValue value)
        {
			//for hovering
        }

#else
	// old input sys if we do decide to have it (most likely wont)...
#endif

        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void MountInput(bool newMountState)
        {
			mount = newMountState;
        }

		public void DismountInput(bool newDismountState)
        {
			dismount = newDismountState;
        }
		public void SteeringInput(Vector2 newSteeringDirection)
		{
			steering = newSteeringDirection;
		}

		public void AccelerateInput(bool newAccelerateState)
        {
			accelerate = newAccelerateState;
        }

		public void DecelerateInput(bool newDecelerateState)
        {
			decelerate = newDecelerateState;
        }



#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			//prevent cursor lock state from changing if "cursorPersist" is true. useful for preventing cursor from dissapearing during NPC conversations
			if (cursorPersist)
            {
				return;
            }
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}