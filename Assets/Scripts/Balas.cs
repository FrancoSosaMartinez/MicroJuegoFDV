using UnityEngine;
using UnityEngine.UI;

public class Balas : MonoBehaviour
{
    public float speed = 10f;
    public float maxLifeTime = 3f;
    public Vector3 targetVector;
    public GameObject miniAsteroidePrefab; 

    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }
  
    void Update()
    {
        transform.Translate(speed*targetVector * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
            return;

        Asteroide enemy = collision.gameObject.GetComponent<Asteroide>();
 
        if ((enemy!=null) && enemy.esGrande)
        {
            FragmentarAsteroide(collision.gameObject);
            Player.PUNTOS += 10;
        }
        else {
            Player.PUNTOS += 5;
        }

        UpdateScoreTotal();
        Destroy(collision.gameObject); 
        gameObject.SetActive(false);
    }


private void FragmentarAsteroide(GameObject asteroide)
{
    Vector3 pos = asteroide.transform.position;
    Vector3 dirDisparo = targetVector; 
    
    Quaternion rotDer = Quaternion.AngleAxis(30f, Vector3.forward);
    Quaternion rotIzq= Quaternion.AngleAxis(-30f, Vector3.forward);
    
    float fuerza = 0.5f;
    GameObject mini1 = Instantiate(miniAsteroidePrefab, pos, Quaternion.identity);
    GameObject mini2 = Instantiate(miniAsteroidePrefab, pos, Quaternion.identity);

    Rigidbody rb1 = mini1.GetComponent<Rigidbody>();
    Rigidbody rb2 = mini2.GetComponent<Rigidbody>();

    if (rb1 != null)
    {
        Vector3 impulso1 = rotDer*dirDisparo*fuerza;
        rb1.AddForce(impulso1, ForceMode.Impulse);
    }
    
    if (rb2 != null)
    {
        Vector3 impulso2 = rotIzq*dirDisparo*fuerza;
        rb2.AddForce(impulso2, ForceMode.Impulse);
    }

    Destroy(asteroide);
}

    private void UpdateScoreTotal()
    {
        GameObject gsi = GameObject.FindGameObjectWithTag("UI");
        gsi.GetComponent<Text>().text = "Puntos: " + Player.PUNTOS;
    }
}
