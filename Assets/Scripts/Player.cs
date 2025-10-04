using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
   public float thrustForce = 10f;
   public float rotationSpeed = 120f;
   public static int  PUNTOS = 0;
   public static float xBordeLimit, yBordeLimit; 
   public GameObject gun, bulletPrefab;
   private Rigidbody _rigidbody;
  
   void Start()
   {
      _rigidbody = GetComponent<Rigidbody>();
      yBordeLimit = Camera.main.orthographicSize;
      xBordeLimit = (Camera.main.aspect * yBordeLimit) ;

      //Debug.Log("xLimit = " + xBordeLimit + ", yLimit = " + yBordeLimit);
   }
   void FixedUpdate()
   {
      float rotacion = Input.GetAxis("Rotate") * Time.deltaTime;
      float thrust = Input.GetAxis("Thrust") * Time.deltaTime * thrustForce;

      Vector3 thrustDirection = transform.right;
      _rigidbody.AddForce(thrustDirection * thrust);

      transform.Rotate(Vector3.forward, -rotacion * rotationSpeed);
   }

   void Update()
   {
      // Teletransporte de la nave, vídeo último
      var newPos = transform.position;

      if (newPos.x > xBordeLimit)
         newPos.x = -xBordeLimit;
      else if (newPos.x < -xBordeLimit)
         newPos.x = xBordeLimit;

      if (newPos.y > yBordeLimit)
         newPos.y = -yBordeLimit;
      else if (newPos.y < -yBordeLimit)
         newPos.y = yBordeLimit;

      transform.position = newPos;
      // Teletransporte FIN

      if (Input.GetKeyDown(KeyCode.Space))
      {
         GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
        
         Collider bulletCollider = bullet.GetComponent<Collider>();
         Collider shipCollider = GetComponent<Collider>();
         
         // CÓDIGO PARA EVITAR CHOQUE BALA CON NAVE
         if ((bulletCollider != null) && (shipCollider != null))
            Physics.IgnoreCollision(bulletCollider, shipCollider);
         // FIN CHOQUE BALA NAVE

         Balas balaScript = bullet.GetComponent<Balas>();
         balaScript.targetVector = transform.right;
      }
      
   }
   private void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.tag == "Enemy")
      {
         Debug.Log("HAS COLISIONADO CON UN METEOORITO, VUELVES A EMPEZAR. HAS CONSEGUIDO: "+ Player.PUNTOS +" PUNTOS");
         PUNTOS = 0;
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }
      else
      {
         Debug.Log("HAS COLISIONADO CON OTRA COSA");
      }
   }

   // ONTRIGGERENTER ES PARA ATRAVESAMIENTO, SE GUARDA LA COLISION PERO NO SE MUEVE 
}