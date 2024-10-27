using UnityEngine;

namespace InClass
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerCameraManager cameraManager;

        private Character controlledCharacter;

        public void StartControllingCharacter(Character character)
        {
            controlledCharacter = character;
            controlledCharacter.Player = this;
        }
        
        public PlayerCameraManager CameraManager => cameraManager;
    }
}
