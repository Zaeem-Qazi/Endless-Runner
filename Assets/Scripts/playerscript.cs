using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerscript : MonoBehaviour
{
  [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpforce = 10f;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private Transform Feetpos;
    [SerializeField] private float JumpTime = 0.25f;
    [SerializeField] private float Grounddistance = 0.3f;
    private float jumpTimer;
    [SerializeField] private Transform Player;
    private float CrouchHeight;
    private float OrignalHeight;
    private bool isgrounded=false;
    private bool isjumping=false;
    private bool iscrouching=false;

    private void Start()
    {
        OrignalHeight = Player.localScale.y;
        CrouchHeight = OrignalHeight / 2;
    }

    void Update()
    {
        #region Jumping
        isgrounded = Physics2D.OverlapCircle(Feetpos.position, Grounddistance, GroundLayer);
        if(isgrounded == true && Input.GetButtonDown("Jump")){
            isjumping = true;
            rb.velocity=Vector2.up * jumpforce;
            //isgrounded=false;
        }
        if(isjumping ==true && Input.GetButton("Jump"))
        {
            if (jumpTimer < JumpTime)
            {
                rb.velocity= Vector2.up * jumpforce;
                jumpTimer += Time.deltaTime;
            }
            else
            {
                isjumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isjumping = false;
            jumpTimer = 0;
        }
        #endregion


        #region crouching

        if (isgrounded)
        {
            if (Input.GetButton("Crouch"))
            {
                if (!iscrouching)
                {
                    Player.localScale = new Vector3(Player.localScale.x, CrouchHeight, Player.localScale.z);
                    iscrouching = true;
                }
            }
            else if (iscrouching)
            {
                Player.localScale = new Vector3(Player.localScale.x, OrignalHeight, Player.localScale.z);
                iscrouching = false;
            }
        }
        else if(iscrouching){

                Player.localScale = new Vector3(Player.localScale.x, OrignalHeight, Player.localScale.z);

                iscrouching = false;
            }



        #endregion
    }
}
