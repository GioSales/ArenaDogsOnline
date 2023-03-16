using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Unity.Netcode;
using SmartConsole;
using UnityEngine.Serialization;

public class PlayerMovement : NetworkCmndBehaviour
{
    [FormerlySerializedAs("speed")]
    [SerializeField] private float movSpeed;
    [SerializeField] private float rotateSpeed = 2;
    private PlayerManager playerManager;
    private Player player;
    private CharacterController characterController;
    private NetworkVariable<Vector3> validPos = new NetworkVariable<Vector3>(
        Vector3.zero, NetworkVariableReadPermission.Owner,
        NetworkVariableWritePermission.Server
        );

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        characterController = GetComponent<CharacterController>();
    }

    protected override void Start()
    {
        base.Start();
        player = ReInput.players.GetPlayer(0);
    }

    //public override void OnNetworkSpawn()
    //{
        
    //}


    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer)
            return;

        float hAxis = player.GetAxis(RewiredConsts.Action.MoveHorizontal);
        float vAxis = player.GetAxis(RewiredConsts.Action.MoveVertical);

        if (HasMoved(hAxis, vAxis))
        {
            Move(hAxis, vAxis);
            RotateToInput(hAxis, vAxis);
        }
    }

    private bool HasMoved(float hAxis, float vAxis)
    {
        return (hAxis > 0.1 || hAxis < -0.1) || (vAxis > 0.1 || vAxis < -0.1);
    }

    private void Move(float hAxis, float vAxis)
    {
        playerManager.Animator.SetInteger("Speed", GetSpeedAnimParam(hAxis, vAxis));
        characterController.Move(new Vector3(hAxis, 0, vAxis) * movSpeed * Time.deltaTime);
        RequestMoveServerRpc(hAxis, vAxis, new ServerRpcParams());
    }

    private static int GetSpeedAnimParam(float hAxis, float vAxis)
    {
        return Mathf.RoundToInt(Mathf.Max(Mathf.Abs(hAxis), Mathf.Abs(vAxis)));
    }

    [ServerRpc]
    private void RequestMoveServerRpc(float hAxis, float vAxis, ServerRpcParams serverRpcParams)
    {
        SetClientValidPos(hAxis, vAxis, Time.deltaTime);
    }

    private void SetClientValidPos(float hAxis, float vAxis, float deltaTime)
    {
        //Debug.Log($"Move client rpc: {clientRpcParams.Send.TargetClientIds}, hAxis: {hAxis}; vAxis: {vAxis}");
        validPos.Value = new Vector3(hAxis, 0, vAxis) * movSpeed * deltaTime;
    }

    private void RotateToInput(float hAxis, float vAxis)
    {
        float inputDirectionAngle = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.Euler(0, inputDirectionAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
    }
}
