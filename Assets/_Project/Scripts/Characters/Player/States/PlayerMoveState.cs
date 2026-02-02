using UnityEngine;
using DungeonCore.FSM;

namespace DungeonCore.Characters.Player
{
    public class PlayerMoveState : IState
    {
        private PlayerController _player;
        private Vector2 _input;

        public PlayerMoveState(PlayerController player)
        {
            _player = player;
        }

        public void Enter()
        {
            // TODO: Run 애니메이션 재생
        }

        public void Tick()
        {
            _input = _player.InputActions.Player.Move.ReadValue<Vector2>();

            if (_input.sqrMagnitude < 0.01f)
            {
                _player.SwitchState(new PlayerIdleState(_player));
                return;
            }

            Move();
        }

        public void Exit() { }

        private void Move()
        {

            Vector3 direction = new Vector3(_input.x, 0, _input.y).normalized;
            _player.Controller.SimpleMove(direction * _player.MoveSpeed);
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _player.transform.rotation = Quaternion.Slerp(
                    _player.transform.rotation, 
                    targetRotation, 
                    _player.RotationSpeed * Time.deltaTime
                );
            }
        }
    }
}