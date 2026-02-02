using UnityEngine;
using DungeonCore.FSM;

namespace DungeonCore.Characters.Player
{
    public class PlayerIdleState : IState
    {
        private PlayerController _player;
        private Vector2 _input;

        public PlayerIdleState(PlayerController player)
        {
            _player = player;
        }

        public void Enter()
        {
            // TODO: Idle 애니메이션 재생
        }

        public void Tick()
        {
            _input = _player.InputActions.Player.Move.ReadValue<Vector2>();
            if (_input.sqrMagnitude > 0.01f)
            {
                _player.SwitchState(new PlayerMoveState(_player));
            }
        }

        public void Exit() { }
    }
}