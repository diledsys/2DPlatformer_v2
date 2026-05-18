# Unity-Junior-Advanced-Enemy-Generation
## Описание
Учебный проект на Unity.
Базовый 2D platformer-проект на Unity.  
Проект построен по принципу разделения ответственности: движение, прыжок, анимация, спавн и сбор предметов вынесены в отдельные классы.

## Используемые технологии
- Unity 6.3LTS
- C#

## Структура
- Assets/_Project
- ├── Animations
- ├── Art
- ├── Prefab
- │   ├── Player
- │   ├── Enemies
- │   └── Collectibles
- ├── Scenes
- └── Scripts
-     ├── Core
-     ├── Player
-     ├── Enemy
-     ├── Collectibles
-     └── Spawn

# Core

## Mover2D
- Отвечает только за горизонтальное движение объекта.
- Используется:
- Player
- Enemy
- Основные задачи:
- хранит направление движения;
- задаёт скорость через Rigidbody2D;
- поддерживает обычное движение и бег;
- не занимается вводом, анимацией или логикой врага.
 
##  GroundChecker2D

- Проверяет, находится ли объект на земле.
- Основные задачи:
- использует Physics2D.OverlapCircle;
- хранит состояние IsGrounded;
- используется Jumper2D и CharacterAnimationPresenter.
 
##  CharacterAnimationPresenter
 
- Связывает физическое состояние объекта с Animator.
- Используется:
- Player
- Enemy
- Основные задачи:
- передаёт в Animator параметры:
- Speed
- Vertical
- IsGrounded
- разворачивает визуальную часть персонажа через VisualRoot;
- не двигает объект и не принимает игровые решения.
 
##  SpawnableObject
- Базовый компонент для объектов, которые можно создавать через спавнер.
- Используется:
- Player prefab
- Enemy prefab
- Cherry prefab
- Нужен для типизации prefab.

# Player

## PlayerController
- Отвечает за ввод игрока через PlayerInputSystem.
- Основные задачи:
- читает Move;
- читает Jump;
- читает Run;
- передаёт команды в Mover2D и Jumper2D.
- Не отвечает за физику напрямую.

## PlayerCameraTarget
- Хранит ссылку на точку, за которой должна следить камера.
- Обычно структура такая:
- Player
- └── CameraTarget
- Используется CameraBinder после спавна игрока.

## PlayerCollector
- Отвечает за сбор предметов игроком.
- Основные задачи:
- реагирует на OnTriggerEnter2D;
- проверяет, есть ли у объекта интерфейс ICollectable;
- вызывает Collect() у собранного объекта.

# Enemy

## EnemyPatrolController
- Простая логика движения врага.
- Основные задачи:
- задаёт направление движения через Mover2D;
- меняет направление через заданный промежуток времени;
- не управляет анимацией напрямую.

## Collectibles

## ICollectable

## CollectableItem
- Компонент собираемого предмета.
- Используется на Cherry.
- Основные задачи:
- хранит количество очков;
- включает Collider2D как Trigger;
- удаляет объект при сборе.

# Spawn

## ObjectSpawner
- Универсальный спавнер объектов.
- Основные задачи:
- создаёт объекты из prefab;
- поддерживает несколько точек спавна для одного prefab;
- хранит созданные объекты;
- удаляет созданные объекты при очистке;
- назначает камере нового игрока через CameraBinder.

# Prefab setup

## Player prefab
- Player
- ├── SpawnableObject
- ├── Rigidbody2D
- ├── Collider2D
- ├── Mover2D
- ├── GroundChecker2D
- ├── Jumper2D
- ├── PlayerController
- ├── PlayerCollector
- ├── PlayerCameraTarget
- ├── CharacterAnimationPresenter
- ├── CameraTarget
- ├── GroundCheck
- └── VisualPivot
-     └── Ninja_Cat_IK_Variant
-         └── Animator

## Enemy prefab
- Enemy
- ├── SpawnableObject
- ├── Rigidbody2D
- ├── Collider2D
- ├── Mover2D
- ├── EnemyPatrolController
- ├── CharacterAnimationPresenter
- └── VisualPivot
-     └── EnemyVisual
-         └── Animator

## Cherry prefab
- Cherry
- ├── SpawnableObject
- ├── CollectableItem
- ├── Collider2D
- ├── SpriteRenderer / Visual
- └── Animator

## Как запустить
1. Открыть проект в Unity Hub
2. Загрузить сцену Main
3. Нажать Play

## Автор
Dmitry Hromov