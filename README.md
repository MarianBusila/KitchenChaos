# KitchenChaos

## Overview
A game based on the following tutorials from CodeMonkey.
- [Learn Unity Beginner/Intermediate 2023](https://www.youtube.com/watch?v=AmGSEH7QcDg)
- [Learn Unity Multiplayer (FREE Complete Course, Netcode for Game Objects Unity Tutorial 2023)](https://www.youtube.com/watch?v=7glCsF9fv3s)

## Netcode for GameObjects Notes

- scripts should inherit from __NetworkBehaviour__ and use __IsOwner__ property to decide if code should execute or not

- when inheriting from __NetworkBehaviour__, you need to a add a __NetworkObject__ component to the game object in the Editor.

- Use client authoritative  for co-op type of games, so there is a level of trust between players. Use [ClientNetworkTransform](https://docs-multiplayer.unity3d.com/netcode/current/components/networktransform#clientnetworktransform) component to enable client authoritative 

- use server authoritative  for competitive P2P games, where there is a chance a player could modify the build to cheat. Use [NetworkTransfrom](https://docs-multiplayer.unity3d.com/netcode/current/components/networktransform) to sync data between players by annotating the code with __ServerRpc__ attribute. 

- server authoritative is more complex to implement and requires extra validation on the data the client sends. You will also have to implement __client side prediction__.

- use [OwnerNetworkAnimator](https://docs-multiplayer.unity3d.com/netcode/current/components/networkanimator#owner-authoritative-mode) component to sync animations using client authoritative

- use __[ClientRpc]__ attribute to mark code to be executed on the client, though it is the server that will invoke this call to be executed by the clients. Note that the method must end with __ClientRpc__ suffix.

- use the __[ServerRpc]__ attribute to mark the code to be executed on the server. Note that the method must end with __ServerRpc__ suffix. 

- __[ServerRpc(RequireOwnerShip=false)]__ means that all the clients can call that code to run on the server

- objects that need to be spawned on the network, must be added in the __NetworkManager__ component, under the NetworkPrefabs. 

- object spawning must be done on the server

- destroying a network object spawned on the server, must be done also on the server

- the player prefab must be added in the __NetworkManager__ component, under the PlayerPrefabs.

- when spawning objects with __[ClientRpc]__ or __[ServerRpc]__, it is a good practice to use indexes as parameters to the function and keep a list of prefabs. This way, you do not have to serialize a complex object over the network.

- use __NetworkObjectReference__ to reference an existing object over the network

- only server can modify a __NetworkVariable__

- __NetworkVariable__ has an __OnValueChanged__ event we can subscribe to

