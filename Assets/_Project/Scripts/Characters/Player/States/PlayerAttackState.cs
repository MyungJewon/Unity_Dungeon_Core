using UnityEngine;
using DungeonCore.FSM;
using DungeonCore.Characters.Enemy;

namespace DungeonCore.Characters.Player
{
    /// <summary>
    /// 3콤보 공격 상태.
    /// 각 공격은 지속 시간(attackDurations) 동안 유지되며,
    /// 그 안에 공격 입력이 들어오면 다음 콤보로 이어진다.
    /// </summary>
    public class PlayerAttackState : IState
    {
        private readonly PlayerController _player;
        private readonly int _comboIndex; // 0 = 1타, 1 = 2타, 2 = 3타

        // 각 콤보별 지속 시간 (애니메이션 연동 전 임시값)
        private static readonly float[] AttackDurations = { 0.3f, 0.3f, 0.5f };
        private static readonly int MaxCombo = AttackDurations.Length - 1;

        private float _timer;
        private bool _nextComboQueued;

        public PlayerAttackState(PlayerController player, int comboIndex = 0)
        {
            _player = player;
            _comboIndex = Mathf.Clamp(comboIndex, 0, MaxCombo);
        }

        public void Enter()
        {
            _timer = AttackDurations[_comboIndex];
            _nextComboQueued = false;

            // TODO: 콤보별 애니메이션 재생
            Debug.Log($"[공격] {_comboIndex + 1}타");

            PerformHit();
        }

        public void Tick()
        {
            // 공격 지속 시간 중 입력 버퍼 체크
            if (_player.IsAttackBuffered && _comboIndex < MaxCombo)
            {
                _player.ConsumeAttack();
                _nextComboQueued = true;
            }

            _timer -= Time.deltaTime;
            if (_timer > 0f) return;

            // 다음 콤보로 전환하거나 종료
            if (_nextComboQueued)
            {
                _player.SwitchState(new PlayerAttackState(_player, _comboIndex + 1));
            }
            else
            {
                // 이동 입력이 있으면 MoveState, 없으면 IdleState
                var moveInput = _player.InputActions.Player.Move.ReadValue<Vector2>();
                if (moveInput.sqrMagnitude > 0.01f)
                    _player.SwitchState(new PlayerMoveState(_player));
                else
                    _player.SwitchState(new PlayerIdleState(_player));
            }
        }

        public void Exit() { }

        /// <summary>
        /// 플레이어 전방 반구 범위 내 Enemy에 피해를 적용한다.
        /// </summary>
        private void PerformHit()
        {
            Vector3 hitCenter = _player.transform.position
                                + _player.transform.forward * (_player.AttackRange * 0.5f);

            Collider[] hits = Physics.OverlapSphere(hitCenter, _player.AttackRange);
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<EnemyBase>(out var enemy))
                    enemy.TakeDamage(_player.AttackDamage);
            }
        }
    }
}
