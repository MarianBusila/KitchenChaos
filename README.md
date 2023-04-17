# KitchenChaos

## Overview
A game based on the following tutorials from CodeMonkey.
- [Learn Unity Beginner/Intermediate 2023](https://www.youtube.com/watch?v=AmGSEH7QcDg)
- [Learn Unity Multiplayer (FREE Complete Course, Netcode for Game Objects Unity Tutorial 2023)](https://www.youtube.com/watch?v=7glCsF9fv3s)

## Netcode for GameObjects Notes
- scripts should inherit from __NetworkBehaviour__ and use __IsOwner__ property to decide if code should execute or not
- Use client authoritative  for co-op type of games, so there is a level of trust between players. Use [ClientNetworkTransform](https://docs-multiplayer.unity3d.com/netcode/current/components/networktransform#clientnetworktransform) component to enable client authoritative 
- Use server authoritative  for competitive P2P games, where there is a chance a player could modify the build to cheat. Use [NetworkTransfrom](https://docs-multiplayer.unity3d.com/netcode/current/components/networktransform) to sync data between players by annotating the code with __ServerRpc__ attribute. 
- Server authoritative is more complex to implement and requires extra validation on the data the client sends. You will also have to implement __client side prediction__.
- use [OwnerNetworkAnimator](https://docs-multiplayer.unity3d.com/netcode/current/components/networkanimator#owner-authoritative-mode) component to sync animations using client authoritative
- use __[ClientRpc]__ attribute to mark code to be executed on the client, though it is the server that will invoke this call to be executed by the clients. Note that the method must end with __ClientRpc__ suffix.
- use the __[ServerRpc]__ attribute to mark the code to be executed on the server. Note that the method must end with __ServerRpc__ suffix. 
