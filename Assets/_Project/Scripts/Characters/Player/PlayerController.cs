using UnityEngine;
using UnityEngine.InputSystem;
using DungeonCore.FSM;

namespace DungeonCore.Characters.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("이동 설정")]
        public float MoveSpeed = 6f;
        public float RotationSpeed = 15f;

        [Header("전투 설정")]
        public float AttackRange = 1.5f;
        public int AttackDamage = 10;

        public CharacterController Controller { get; private set; }
        public Controls InputActions { get; private set; }

        // 공격 입력 버퍼 — 상태가 소비(ConsumeAttack)해서 중복 처리 방지
        public bool IsAttackBuffered { get; private set; }
        public void ConsumeAttack() => IsAttackBuffered = false;

        private StateMachine _stateMachine;

        private void Awake()
        {
            Controller = GetComponent<CharacterController>();
            InputActions = new Controls();
            _stateMachine = new StateMachine();
        }

        private void OnEnable()
        {
            InputActions.Enable();
            InputActions.Player.Attack.performed += OnAttackPerformed;
        }

        private void OnDisable()
        {
            InputActions.Player.Attack.performed -= OnAttackPerformed;
            InputActions.Disable();
        }

        private void Start()
        {
            _stateMachine.Initialize(new PlayerIdleState(this));
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        public void SwitchState(IState newState)
        {
            _stateMachine.ChangeState(newState);
        }

        private void OnAttackPerformed(InputAction.CallbackContext _)
        {
            IsAttackBuffered = true;
        }
    }
}