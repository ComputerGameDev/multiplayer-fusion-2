# Multiplayer Fusion Game

## Overview
This is a multiplayer game built with Fusion that allows players to compete in real-time. Players can move, shoot, and collect power-ups while aiming to outscore their opponents. The game features intuitive controls, a scoring system, and strategic elements such as the newly added shield power-up.

## Important Scripts

### 1. **[Player.cs](Assets/Scripts/Player.cs)**
- Manages player movement and shooting mechanics.
- Handles player input and synchronizes it across the network.
- Spawns projectiles (balls) that interact with other players and the environment.

### 2. **[Ball.cs](Assets/Scripts/Ball.cs)**
- Represents the projectiles shot by players.
- Detects collisions with other players and applies damage.
- Awards scores to the opposing player when their projectile hits.

### 3. **[Health.cs](Assets/Scripts/Health.cs)**
- Tracks each player's health and handles damage application.
- Implements the new shield functionality to prevent damage while active.

### 4. **[Shield.cs](Assets/Scripts/Shield.cs)**
- Defines the shield power-up.
- Detects when a player collects the shield and activates temporary invulnerability.
- Despawns the shield object once collected.

### 5. **[Score.cs](Assets/Scripts/Score.cs)**
- Manages the player's score, updating it when they hit an opponent.
- Synchronizes the score across the network.

## New Features

### Shield Power-Up
- **What it Does**: The shield provides temporary invulnerability for players.
- **How it Works**:
  - Players can collect the shield power-up by colliding with it.
  - Once collected, the shield prevents damage for 10 seconds.
  - The power-up disappears immediately after being collected.
- **Script**: The `Shield.cs` script handles activation and despawning, while the `Health.cs` script implements the shield effect.

### Scoring System
- **What it Does**: Awards points to players for hitting their opponents.
- **How it Works**:
  - When a player hits another with a projectile, their score increases.
  - If the target has an active shield, no damage or score is awarded.
- **Script**: The `Score.cs` script tracks and updates scores, while `Ball.cs` handles the collision logic.

## How to Play
1. Move around the map using the movement controls.
2. Shoot projectiles to hit opponents and gain points.
3. Collect shield power-ups for temporary invulnerability.
4. Avoid taking damage to maintain your score advantage.

## Play Online
You can play the game online on [Itch.io](https://example.itch.io/game).

