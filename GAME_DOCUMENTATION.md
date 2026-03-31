# 🎮 2D Runner Game - Detailed Documentation

## Table of Contents
1. [Project Overview](#overview)
2. [Game Mechanics](#mechanics)
3. [Core Systems](#systems)
4. [Script Architecture](#architecture)
5. [Gameplay Features](#features)
6. [Scene Structure](#scenes)
7. [Prefab System](#prefabs)
8. [Game Flow](#flow)

## Project Overview

**Project Name:** 2D_RUNNER_GAME  
**Engine:** Unity (C#)  
**Game Type:** 2D Endless Runner  
**Language:** C#  
**Repository:** https://github.com/Alex-vincent04/2D_RUNNER_GAME

### Core Concept
This is a 3D perspective 2D runner game where the player controls a character running down an infinite road while avoiding obstacles and collecting coins to earn points. The game features power-ups that temporarily enhance gameplay abilities.

## Game Mechanics

### 1. Player Movement
- **Horizontal Movement:** Player can move left and right across the road
  - **Left Controls:** A key or Left Arrow
  - **Right Controls:** D key or Right Arrow
  - **Boundary Limits:** X-axis constrained between -4.75 and +4.75
  - **Movement Speed:** Configurable via speed parameter

### 2. Jumping System
- **Jump Mechanic:** Double jump capability
  - **Key:** Space bar
  - **Jump Limit:** 2 jumps per ground touch
  - **Jump Force:** 5 units of impulse (upward)
  - **Max Height Cap:** 3f position (prevents infinite upward movement)
  - Jump count resets when player collides with ground (planeCollision tag)

### 3. Collision Detection
- **Two Collision Types:**
  - **Hard Collisions:** Obstacles and walls (OnCollisionEnter)
  - **Soft Collisions:** Coins, power-ups (OnTriggerEnter)

## Core Systems

### 1. Player Controller (Player.cs)

**Main Responsibilities:**
- Player movement and jumping
- Power-up tracking
- Score management
- Collision handling
- Animation state management

**Key Variables:**
- speed (int) - Horizontal movement speed
- JumpSpeed (int) - Jump velocity (currently unused)
- Score (float) - Current game score
- rb (Rigidbody) - Physics component
- IsOnGround (bool) - Ground state check
- JumpCount (int) - Current jump count (max 2)
- PowerUp (bool) - Shield active state
- Point2x (bool) - Double points active
- Coin2x (bool) - Double coin spawn active
- Speed2x (bool) - Double movement speed active
- StartTimer (float) - Power-up duration timer (10 seconds)

**Key Methods:**
- Update() - Handles input, movement, power-up timers
- OnCollisionEnter() - Handles hard collisions (obstacles, ground)
- OnTriggerEnter() - Handles soft collisions (collectibles)

**Collision Logic:**
- Obstacle Hit: If PowerUp active → destroy obstacle; else → game over
- Ground Touch: Reset jump count to 0

**Collectible Handling:**
- Coins: +1 score (or +2 if Point2x active)
- PowerUp: 10-second shield (destroy obstacles)
- Point2x: 10-second double point multiplier
- Coin2x: 10-second double coin spawn rate
- Speed2x: 10-second movement speed boost

### 2. Road Movement (RoadMovement.cs)

**Purpose:** Create infinite road illusion

**Mechanism:**
- Continuously moves road backward (Vector3.back)
- Speed: Configurable RoadSpeed parameter
- **Recycling:** When Z position < -10f, resets to Z: 24.1f
- Creates seamless looping environment

### 3. Coin Movement (CoinMovement.cs)

**Purpose:** Move coins with potential speed multiplier

**Features:**
- Finds and references Player script at startup
- **Speed Responsive:** Moves at 2x speed when player.Speed2x is active
- **Auto-Destruction:** Destroys coin when Z < -30f (off-screen)
- **Graceful Fallback:** Safely handles missing player references

### 4. Obstacle Spawning (CreateCopy.cs)

**Purpose:** Procedurally spawn obstacles, coins, and power-ups

**Spawn Schedule:**
| Item | Initial Delay | Repeat Interval |
|------|---|---|
| Obstacles | 2s | 3s |
| Coins (Primary) | 2s | 2s |
| Coins (Secondary) | 1s | 4s |
| PowerUp (Shield) | 10s | 20s |
| Point2x | 5s | 20s |
| Coin2x | 5s | 20s |
| Speed2x | 10s | 20s |

**Spawn Positions:**
- **Obstacles:** 5 variants
  - X: Random between -3.35 and 3.35
  - Y: 0.5 (or 0 depending on obstacle type)
  - Z: -5.209f (spawn ahead of player)

- **Coins:**
  - X: Random between -3.35 and 3.35
  - Y: 2.2f (mid-air collectible)
  - Z: Random between -15 and -5f

- **Power-ups:**
  - X: Random between -3.35 and 3.35
  - Y: 1.0f
  - Z: Random between -15 and 5f

### 5. Obstacle & Collectible Movement (Destroy.cs & ObstacleRotation.cs)

**Movement Patterns:**
- All objects move backward (Vector3.back)
- Speed: Configurable per object
- **Speed2x Responsive:** Objects move at 2x speed when active
- **Auto-Cleanup:** Destroyed when Z < -30f

### 6. Animation Controller (Animation.cs)

**Current Implementation:**
- Listens to Space key input
- Sets Animator.Jump parameter

### 7. Button Controller (Button.cs)

**Scene Navigation:**
- PlayBtn() → Loads Scene 1 (Main Game)
- PlayBtn_0() → Loads Scene 0 (Menu/Start)

## Script Architecture

### Dependency Graph

Player (Core) - Referenced by CoinMovement, Destroy, CreateCopy  
RoadMovement (Environment) - Creates infinite road illusion  
CreateCopy (Spawner) - Spawns obstacles, coins, power-ups  
CoinMovement (Collectible) - References Player  
Destroy & ObstacleRotation (Moving Objects) - Use Player.Speed2x  
Animation (UI) - Controls Animator.Jump  
Button (Navigation) - Loads Scenes

## Gameplay Features

### Power-Up System

All power-ups last 10 seconds and auto-deactivate:

| Power-Up | Tag | Effect |
|----------|-----|--------|
| **Shield** | "Powerup" | Destroy obstacles on collision |
| **2x Points** | "Point2x" | Double coin point value |
| **2x Coins** | "Coin2x" | Spawn double coins |
| **2x Speed** | "Speed2x" | Everything moves 2x faster |

### Scoring System
- **Base Coin:** +1 point
- **With Point2x:** +2 points per coin
- **Score Display:** Real-time UI text update

### Difficulty Progression
- Obstacles spawn continuously every 3 seconds
- Coins spawn every 2-4 seconds
- No difficulty ramping (fixed spawn rates)

## Scene Structure

### Scene 0: UI Menu
- **Purpose:** Main menu / start screen
- **Script:** Button.cs (PlayBtn_0 navigation)

### Scene 1: SampleScene (Main Gameplay)
- **Purpose:** Primary gameplay experience
- **Contains:**
  - Player character
  - Road (looping environment)
  - Spawn manager (CreateCopy)
  - Camera
  - UI Canvas (score display)

### Scene 2: GameOver
- **Purpose:** Game over screen
- **Triggered:** When player dies (no shield active)
- **Features:** Reload/retry functionality

## Prefab System

### Obstacle Prefabs (5 Variants)
1. **Obstacle_0.prefab** - Generic obstacle
2. **Obstacle_1.prefab** - Generic obstacle
3. **Obstacle_2.prefab** - Generic obstacle
4. **Crate_01.prefab** - Wooden crate
5. **Barrel_02.prefab** - Metal barrel

### Collectible Prefabs
- **Coin Silver.prefab** - Base coin collectible
- **CoinParent.prefab** - Parent/group container for coins

### Power-Up Prefabs
- **Lightning Blue.prefab** - Shield/Powerup visual
- **Shield Metal.prefab** - Shield representation
- **Star Red.prefab** - Point multiplier visual

### Environment Prefabs
- **Prop_Barrier02 & 03.prefab** - Decorative barriers
- **Prop_Rock_Boulder_01.prefab** - Rock obstacle
- **Spool02.prefab** - Decorative element

## Game Flow

### Startup Sequence
1. Scene loads (SampleScene)
2. Player spawns at origin
3. CreateCopy.Start() initializes spawn timers
4. Road begins moving backward
5. Obstacles and coins spawn procedurally
6. Score UI initializes

### Main Loop (Per Frame)
Input Processing → Physics Update → Collision Detection → Power-Up Management → Animation → Score Update

### Game Over Sequence
1. Player collides with obstacle (no shield)
2. Player destroyed
3. Scene 2 (GameOver) loaded
4. Player can retry or return to menu

## Key GameObjects & Tags

**Required Tags:**
- Player - Main character
- obstacle - Destroyable obstacles
- coin - Collectible coins
- Powerup - Shield power-up
- Point2x - Point multiplier
- Coin2x - Coin doubler
- Speed2x - Speed multiplier
- planeCollision - Ground reference

## Performance Considerations

1. **Spawn Management:** Objects auto-destroyed at Z < -30f
2. **Player Finding:** Uses GameObject.FindGameObjectWithTag() with null checks
3. **Physics:** Uses AddForce for jump impulse
4. **Animation:** Animator.SetBool for state changes

## Potential Issues & Notes

1. **Animation.cs Logic:** Both if/else branches set Jump to true (duplicate)
2. **Uncommented Speed2x Road Logic:** Old code commented out in RoadMovement.cs
3. **JumpSpeed Variable:** Declared but never used
4. **No Difficulty Scaling:** Spawn rates don't increase over time
5. **Audio System:** Audio.cs is defined but mostly empty

## How to Play

1. **Launch:** Start from Scene 0 (Menu)
2. **Start Game:** Click Play button → Scene 1 loads
3. **Controls:**
   - A / Left Arrow - Move left
   - D / Right Arrow - Move right
   - Space - Jump (up to 2 times)
4. **Objective:** Collect coins, avoid obstacles, maximize score
5. **Power-Ups:** Grab power-ups for temporary advantages (10 seconds)
6. **Game Over:** Hit an obstacle without shield → Scene 2
7. **Retry:** Use buttons to restart or return to menu

---

Documentation created for 2D Runner Game by Alex-vincent04