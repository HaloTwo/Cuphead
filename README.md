# 🥊 Cuphead (Unity 2D)

Studio MDHR의 2D 런앤건 액션 게임 **Cuphead** 모작 프로젝트입니다.  
보스전 중심 전투 구조와 패턴 기반 AI, 비동기 로딩 및 오브젝트 풀링 시스템 구현을 목표로 개발했습니다.

---

## 📌 Project Info
- 개발 인원 : 1인  
- 개발 기간 : 2023.04.11 ~ 2023.04.25 (2주)  
- 개발 환경 : C#, Unity2D, GitHub  

---

## 🎥 Gameplay Video
[![Watch the Gameplay](https://img.youtube.com/vi/5bR0k2nC6nk/0.jpg)](https://www.youtube.com/watch?v=5bR0k2nC6nk)

---

## 🧠 핵심 구현 요소

- 3종 보스 패턴 기반 전투 시스템 구현  
- 전투 맵 / 이동 맵 분리 구조 설계  
- 필살기 연출 및 공격 패턴 구현  
- Object Pool 시스템 구현 (투사체 및 이펙트 관리)  
- 비동기 로딩(Scene Async Loading) 구조 구현  
- Json + ScriptableObject 기반 데이터 저장 구조  
- UGUI 기반 UI 시스템 구성  
- 싱글톤 패턴 기반 매니저 구조 설계  
- Coroutine 기반 비동기 처리  
- Lerp를 활용한 카메라 이동 구현  
- Platform Effector 2D 기반 물리 상호작용 구현  

---

## 🔧 Core Systems (Code Reference)

- Boss Pattern & Attack Logic  
  → (보스 관련 스크립트 링크 추가)

- Object Pool System  
  → (ObjectPool.cs 링크 추가)

- Scene Async Loading  
  → (AsyncLoadManager.cs 링크 추가)

- Data Save Structure (Json + ScriptableObject)  
  → (DataManager.cs 링크 추가)

---

## 📷 Screenshots

![Gameplay1](https://github.com/HaloTwo/Cuphead/assets/94442043/946490cc-c2f2-4da2-b380-553a3b73bdc3)
![Gameplay2](https://github.com/HaloTwo/Cuphead/assets/94442043/d46e1c5b-9fe7-4605-b9af-fe5acdc2b63e)
![Gameplay3](https://github.com/HaloTwo/Cuphead/assets/94442043/ec015439-dad5-4299-845b-110f35edcf74)
