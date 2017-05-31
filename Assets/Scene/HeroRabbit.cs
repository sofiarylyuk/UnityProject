using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour
{


    public int MaxHealth = 2;
    public int health = 1;

    public float speed = 2;
    Rigidbody2D myBody = null;

    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;

    Animator myController = null;

    Vector3 targetScale = Vector3.one;

    Transform rabbitParent = null;
    public bool dead = false;


    void Start()
    {
        //Зберегти стандартний батьківський GameObject
        myBody = this.GetComponent<Rigidbody2D>();
        myController = this.GetComponent<Animator>();
        this.rabbitParent = this.transform.parent;
        LevelInfo.current.setRabbitsStartingPoint(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

    }

     static void SetNewParent(Transform obj, Transform new_parent) {
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

    static void MakeChild(Transform obj, Transform my_parent) {
        if (obj.transform.parent != my_parent)
        {
            Vector3 global_posistion = obj.transform.position;
            obj.transform.parent = my_parent;
            obj.transform.position = global_posistion;
        }
    } 

    public void addHealth(int numb)
    {
        this.health += numb;
        if (this.health > MaxHealth)
        {
            this.health = MaxHealth;
        }
        this.onHealthChange();
    }

    public void removeHealth (int numb)
    {
        this.health -= numb;
        if (this.health < 0)
        {
            this.health = 0;
        }
        this.onHealthChange();
    }

    public void onHealthChange()
    {
        if(this.health == 1)
        {
            this.transform.localScale = Vector3.one*2;
        }
        else if(this.health == 2)
        {
            this.transform.localScale = Vector3.one*4;
        }
        else if (this.health == 0)
        {
            LevelInfo.current.onRabbitDeath(this);
        }
    }

    void FixedUpdate()
    {
        //[-1, 1]
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;

        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);

        if (hit)
        {
            //Перевіряємо чи ми опинились на платформі
            if (hit.transform != null
            && hit.transform.GetComponent<MovingPlatform>() != null)
            {
                MakeChild(this.transform, hit.transform);
                //Приліпаємо до платформи
                // SetNewParent(this.transform, hit.transform);
            }
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            MakeChild(this.transform, this.rabbitParent);
            //Ми в повітрі відліпаємо під платформи
            //SetNewParent(this.transform, this.heroParent);
        }

        Debug.DrawLine(from, to, Color.red);

        if (this.isGrounded)
        {
            myController.SetBool("jump", false);
        }
        else
        {
            myController.SetBool("jump", true);
        }



        float value = Input.GetAxis("Horizontal");

        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
        }

        if (Mathf.Abs(value) > 0)
        {
            myController.SetBool("run", true);
        }
        else
        {
            myController.SetBool("run", false);
        }


        if (dead)
        {
            myController.SetBool("death", true);
        }
        else
        {

            myController.SetBool("death", false);
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

        

        
      


            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                this.JumpActive = true;
            }

            if (this.JumpActive)
            {
                //Якщо кнопку ще тримають 
                if (Input.GetButton("Jump"))
                {
                    this.JumpTime += Time.deltaTime;
                    if (this.JumpTime < this.MaxJumpTime)
                    {
                        Vector2 vel = myBody.velocity;

                        vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                        myBody.velocity = vel;
                    }
                }
                else
                {
                    this.JumpActive = false;
                    this.JumpTime = 0;
                }
            }

       // this.transform.localScale = Vector3.SmoothDamp(
       //     this.transform.localScale, this.targetScale, ref scale_speed, 1.0f);
        }

         
    }