# KitchenChaos

## Overview
A game based on the following tutorials from CodeMonkey.
- [Learn Unity Beginner/Intermediate 2023](https://www.youtube.com/watch?v=AmGSEH7QcDg)
- [Learn Unity Multiplayer (FREE Complete Course, Netcode for Game Objects Unity Tutorial 2023)](https://www.youtube.com/watch?v=7glCsF9fv3s)

## Netcode for GameObjects Notes
- Use client authoritative  for co-op type of games, so there is a level of trust between players. Use [ClientNetworkTransform](https://docs-multiplayer.unity3d.com/netcode/current/components/networktransform#clientnetworktransform) component to enable client authoritative 
- Use server authoritative  for competitive P2P games, where there is a chance a player could modify the build to cheat. Use [NetworkTransfrom](https://docs-multiplayer.unity3d.com/netcode/current/components/networktransform) to sync data between players by annotating the code with __ServerRpc__ attribute. 
- Server authoritative is more complex to implement and requires extra validation on the data the client sends. You will also have to implement __client side prediction__.
