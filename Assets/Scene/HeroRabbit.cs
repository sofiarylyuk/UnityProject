using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour
{

    public float speed = 1;
    Rigidbody2D myBody = null;

    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;

    Animator myController = null;



    // Use this for initialization
    Transform heroParent = null;
    void Start()
    {
        //Зберегти стандартний батьківський GameObject
        this.heroParent = this.transform.parent;
    myBody = this.GetComponent<Rigidbody2D>();
        myController = this.GetComponent<Animator>();
        LevelController.current.setStartPosition(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //[-1, 1]
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        Debug.DrawLine(from, to, Color.red);

        float value = Input.GetAxis("Horizontal");
        
        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
        }

         if (Mathf.Abs(value) > 0){
            myController.SetBool("run", true);
         }
         else{
            myController.SetBool("run", false);
         }

        if (this.isGrounded)
        {
            myController.SetBool("jump", false);
        }
        else
        {
            myController.SetBool("jump", true);
        }


        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }

        int layer_id = 1 << LayerMask.NameToLayer("Ground");

        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
       

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }

        if (this.JumpActive)
        {
            //Якщо кнопку ще тримають 
            if (Input.GetButton("Jump")) {
            this.JumpTime += Time.deltaTime;
            if (this.JumpTime < this.MaxJumpTime)
            {
                Vector2 vel = myBody.velocity;

                vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                myBody.velocity = vel;
            }
        }
        else {
            this.JumpActive = false; this.JumpTime = 0;
        }
    }

        // Згадуємо g r o u n d check
 //RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            //Перевіряємо чи ми опинились на платформі
            if (hit.transform != null
            && hit.transform.GetComponent<MovingPlatform>() != null)
            {
                //Приліпаємо до платформи
                SetNewParent(this.transform, hit.transform);
            }
        }
        else
        {
            //Ми в повітрі відліпаємо під платформи
            SetNewParent(this.transform, this.heroParent);
        }
    }

   static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
// Засікаємо позицію у Глобальних координатах
        Vector3 pos = obj.transform.position;
// Встановлюємо нового батька
        obj.transform.parent = new_parent;
// Після зміни батька координати кролика зміняться
 // Оскільки вони тепер відносно іншого об ’ єкта
  // повертаємо кролика в ті самі глобальні координати
        obj.transform.position = pos;
        }
    }
}