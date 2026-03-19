# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

프로젝트 개요는 [README.md](README.md) 참고.

## Architecture

### FSM
`IState` (`Scripts/FSM/IState.cs`) — `Enter()` / `Tick()` / `Exit()` 구현. `StateMachine.ChangeState()`로 전환. 새 상태는 `IState` 클래스 추가만으로 확장 (OCP 준수).

### Player
`PlayerController` (MonoBehaviour)가 `StateMachine` + `InputActions`(`Controls`) 소유. 각 State는 `PlayerController` 참조를 통해 입력 및 컴포넌트에 접근.

- 이동: `CharacterController.SimpleMove`
- 회전: `Quaternion.Slerp` + `RotationSpeed`
- 상태: `PlayerIdleState` ↔ `PlayerMoveState` (`Move.sqrMagnitude > 0.01` 기준)

### Input
`Controls.cs`는 **자동 생성 파일** — 직접 수정 금지. 바인딩 변경은 Unity 에디터에서 `Controls.inputactions` 수정 후 재생성. 현재 액션: `Player.Move` (Vector2, WASD).

## 규칙
- 게임 코드: `Assets/_Project/Scripts/`
- 상태 클래스: `Characters/<Type>/States/`
- 그래픽: URP (`com.unity.render-pipelines.universal` 17.3.0)
- 입력: New Input System (`com.unity.inputsystem` 1.19.0), 레거시 사용 안 함
