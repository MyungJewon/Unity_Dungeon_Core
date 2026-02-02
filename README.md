# ⚔️ Dungeon Core: Action RPG Prototype
> **"OOP 기반의 견고한 전투 시스템과 DOTS 최적화를 결합한 하이브리드 액션 RPG"**

![Unity](https://img.shields.io/badge/Unity-6000.0.3f1-000000?style=flat-square&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-OOP_%26_Patterns-512BD4?style=flat-square&logo=c-sharp&logoColor=white)
![DOTS](https://img.shields.io/badge/Optimization-DOTS_%26_Jobs-E34F26?style=flat-square)

---

## 📖 Project Overview
**Dungeon Core**는 탑뷰(Top-down) 시점의 핵앤슬래시 액션 RPG 프로젝트입니다.
화려한 스킬과 타격감을 구현하는 **게임플레이 로직(Game Loop)**은 객체지향(OOP)으로 유연하게 설계하고, 성능 부하가 심한 **투사체 및 환경 오브젝트**는 데이터 지향(DOTS)으로 처리하여 **생산성과 성능의 균형(Hybrid Architecture)**을 맞추는 데 중점을 두었습니다.

**[핵심 구현 목표]**
1.  **OOP & FSM:** `State Pattern`을 활용한 확장 가능한 캐릭터 제어 시스템 구축.
2.  **Combat Polish:** 애니메이션 이벤트와 물리 엔진을 활용한 정교한 피격 판정 및 타격감(Hit Stop, Camera Shake) 구현.
3.  **Hybrid Optimization:** 대량의 투사체(Bullet Hell) 처리를 위한 ECS(Entity Component System) 도입.

---
<!--
## 🎮 Gameplay Demo
| Combo Action | Skill & Hit Feedback |
| :---: | :---: |
| ![Combo](https://via.placeholder.com/350x200?text=Insert+Combo+GIF) | ![Skill](https://via.placeholder.com/350x200?text=Insert+Skill+GIF) |
| *FSM 기반의 콤보 시스템* | *역경직 및 카메라 쉐이크* |

*(이미지/GIF가 준비되면 경로를 수정해주세요)*

---
-->
## 🛠 Technical Architecture

### 1. FSM (Finite State Machine) 기반 캐릭터 제어
복잡한 `boolean` 플래그 관리(`isAttacking`, `isMoving` 등)를 방지하기 위해 **State Pattern**을 도입하여 캐릭터의 상태를 모듈화했습니다.

- **IState 인터페이스:** `Enter`, `Tick`, `Exit` 메서드를 정의하여 상태 간 전이 로직을 명확히 분리.
- **확장성:** 새로운 스킬이나 상태(예: `StunState`, `DashState`) 추가 시 기존 코드를 수정하지 않고 클래스만 추가하면 되는 **OCP(개방-폐쇄 원칙)** 준수.
