
## 프로젝트 소개
# ClickerGame
내일 배움 캠프에서 진행한 팀 프로젝트 클리커 게임 입니다
## 프로젝트 명 : ClickMon

## 게임 설명
- 꼬북단들의 포켓몬 납치 모험! 마우스 하나로 포켓몬을 굴복시키기 대작전!!!
- 자동 클릭, 강화 시스템을 활용해 점점 더 빠르게 성장하며 강력한 능력을 해금하세요!

## 개발기간 : 2025.03.27 ~ 2025.04.02 (7일)

## 사용엔진 : UnityEngine 2022.3.17f1 (LTS)

## 언어 : C#

## 팀 소개
팀장 김태겸 

팀원 김규태, 나진영, 이재휴

## 플레이방법
클릭하나로 돈을 벌고 전투를 하며 무기와 능력치를 성장시켜 더욱 강한 포켓몬을 쓰러트리자!

## 순서도
![Frame 14](https://github.com/user-attachments/assets/08288113-9aea-470a-8fb7-a0fa67bbc26d)

## 기능

### 클릭 공격 / 자동 공격

- 몬스터를 클릭시 일반공격과 치명타 공격이 가능
- 자동 공격은 현재 공격력에 비례하여 플레이어의 피로도를 줄여주는 기능

### 무기 / 강화 시스템

- 다양한 무기의 능력치와 골드를 사용하여 강화 버튼을 클릭하면 능력치 강화 가능
- 현재 장착중인 무기와 무기의 능력치를 게임화면에 UI로 표시
- 보유하지 않은 무기는 실루엣으로 처리하여 능력치를 ???로 표시하고 구매 시 무기가 보여짐

### 플레이어 능력치 / 강화

- 세 가지 플레이어 능력치 레벨을 올려 다양한 방식의 성장 가능
- 레벨 업 버튼을 꾹 누르고 있으면 연속 업그레이드가 가능

### 적 / 스테이지
- 적 데이터와 스테이지 데이터 생성 (ScriptableObject 파일)
- 각 스테이지에 따른 몬스터 배치
- 적 처치 시 재화 획득
- 적 공격 받을 시 부드럽게 체력 감소

|---|---|
|![objectpool](https://github.com/user-attachments/assets/47c14e6e-69a9-4dee-9b24-d773a2f455ca)
|![MonsterDamage](https://github.com/user-attachments/assets/b12490e5-34eb-4d15-b0b5-f91ba2b8d170)
|

### 텍스트 데미지
- 큐를 이용한 오브젝트 풀링을 적용
- 텍스트 생성 후 재활용

  
### 사운드 / 씬 관리
- BGM 볼륨 조절
- 효과음 볼륨 조절
- 타이틀 화면으로 돌아가기

- 맵 배경을 스테이지에 따라 변경합니다.
- BGM을 스테이지에 맞게 변경하여 몰입감을 높입니다.

### 파티클

- 공격시 밋밋한 느낌보단 타격감을 주기위해 파티클 구현
- 일반공격과 치명타 공격에 따라 다른 이팩트 발생

## 에셋
### 몬스터볼 출처

- https://blog.naver.com/minarigirl/220924362054

### 포켓몬 출처

- https://pokemonkorea.co.kr/pokedex

### 백그라운 이미지 AI 생성

- https://www.genspark.ai/

### 코인 에셋

- https://assetstore.unity.com/packages/2d/gui/icons/storeiconui-124152

### 음악

- https://soundlibrary.pokemon.co.jp/asia-en/playlists/memories_of_151

## 🔧 설치 및 실행 방법  

1️⃣ **게임 다운로드**  
   - [GitHub Releases](https://github.com/BeautifulMaple/ClickerGame/releases) 페이지에서 최신 버전을 다운로드하세요.  
   - `ClickMon X.X.X.zip` 파일을 받습니다.  

2️⃣ **압축 해제**  
   - 다운로드한 ZIP 파일을 원하는 폴더에 압축 해제합니다.  

3️⃣ **게임 실행**  
   - `ATM Project.exe` 파일을 실행하면 게임이 시작됩니다. 

