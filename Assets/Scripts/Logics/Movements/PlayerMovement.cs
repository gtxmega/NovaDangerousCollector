using Services.InputHandler;
using Services.locator;
using UnityEngine;

namespace Logics.Movements
{
    public class PlayerMovement : MonoBehaviour, InjectDependency
    {
        [SerializeField] private Transform _selfTransform;
        [SerializeField] private float _speed;
        [SerializeField] private CharacterController _characterController;

        private IInputHandler _playerInput;

        public void Inject(IServicesLocator locator)
        {
            _playerInput = locator.GetServices<IInputHandler>();
        }

        public void IncreaseMoveSpeed(float amount) { _speed += amount; }
        public void DecreaseMoveSpeed(float amount) { _speed -= amount; }


        private void FixedUpdate()
        {
            if (_playerInput != null)
            {
                Vector3 direction = new Vector3(_playerInput.GetHorizontal(), 0.0f, _playerInput.GetVertical());
                if (direction != Vector3.zero)
                {
                    direction *= _speed;
                    direction.y = Physics.gravity.y;

                    _characterController.Move(direction * Time.fixedDeltaTime);

                    direction.y = 0.0f;
                    _selfTransform.forward = direction;
                }
                else
                {
                    _characterController.Move(Physics.gravity * Time.fixedDeltaTime);
                }
            }
        }
    }
}