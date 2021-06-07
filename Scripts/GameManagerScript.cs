using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    public GameObject player;
    public GameObject playerButtomPosition;
    public GameObject spiderMonster;
    public GameObject phoenix;
    public GameObject ghost;
    public GameObject smallBird;
    public GameObject gameManager;
    public GameObject hadesSymbol;
    public GameObject ninja;
    public GameObject ninjaStar;
    public GameObject ninjaShurikan;

    public GameObject damageMNG;
    public int enamyTypes = 4;
    private int waveNumber=0;
    private int enamyCount=0;
    private Vector3 spawnPoint;
    public int maxX = 14;
    public int minX = -7;
    public int maxY = 2;
    public int minY = -7;
    private int rn = 0;

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;




    // Start is called before the first frame update
    void Start()
    {
        waveText.text = "Wave: " + waveNumber;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (enamyCount == 0)
        {
            Debug.Log(waveNumber);
            waveNumber++;
            waveText.text = "Wave: " + waveNumber;
            for (int i = 0; i < waveNumber;i++)
                {
                spawnPoint = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
                
                GameObject hs= Instantiate(hadesSymbol, spawnPoint, Quaternion.identity);
                hs.GetComponent<HadesSymbolScript>().gameManager = gameManager;
                enamyCount++;

                }
            
        }
    }

    public void EnamyDead()
    {
        GetComponent<AudioSource>().Play();
        enamyCount -= 1;
    }

    private void CallSpiderMonster(Vector3 spawnPoint)
    {
        GameObject pf = Instantiate(spiderMonster, spawnPoint, Quaternion.identity);
        pf.GetComponent<SpiderMonsterScript>().player = player;
        pf.GetComponent<SpiderMonsterScript>().damageMNGObj = damageMNG;
        pf.GetComponent<SpiderMonsterScript>().spawnManager = gameManager;
    }

    private void CallGhost(Vector3 spawnPoint)
    {
        GameObject pf = Instantiate(ghost, spawnPoint, Quaternion.identity);
        pf.GetComponent<GhostScript>().player = player;
        pf.GetComponent<GhostScript>().damageMNGObj = damageMNG;
        pf.GetComponent<GhostScript>().spawnManager = gameManager;
    }

    private void CallPhoenix(Vector3 spawnPoint)
    {
        GameObject pf = Instantiate(phoenix, spawnPoint, Quaternion.identity);
        pf.GetComponent<PhoenixScript>().player = player;
        pf.GetComponent<PhoenixScript>().damageMNGObj = damageMNG;
        pf.GetComponent<PhoenixScript>().playerButtom = playerButtomPosition;
        pf.GetComponent<PhoenixScript>().smallBird = smallBird;
        pf.GetComponent<PhoenixScript>().spawnManager = gameManager;
    }

    private void CallNinja(Vector3 spawnPoint)
    {
        GameObject pf = Instantiate(ninja, spawnPoint, Quaternion.identity);
        pf.GetComponent<NInjaScript>().player = player;
        pf.GetComponent<NInjaScript>().damageMNGObj = damageMNG;
        pf.GetComponent<NInjaScript>().ninjaStar = ninjaStar;
        pf.GetComponent<NInjaScript>().Shurikan = ninjaShurikan;
        pf.GetComponent<NInjaScript>().spawnManager = gameManager;
    }

    public void CreateEnamy(Vector3 position)
    {
        Debug.Log("create enamy called");
        rn = Random.Range(1, enamyTypes + 1);

        if (rn == 1)
        {
            CallSpiderMonster(position);
        }
        else
        if (rn == 2)
        {
            CallGhost(position);
        }
        else
        if (rn == 3)
        {
            CallPhoenix(position);
        }
        else
        if (rn == 4)
        {
            CallNinja(position);


        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        if (GameObject.FindWithTag("Ghost") != null)
        {
            Destroy(GameObject.FindWithTag("Ghost"));
        }
        if (GameObject.FindWithTag("Spider Monster") != null)
        {
            Destroy(GameObject.FindWithTag("Spider Monster"));
        }
        if (GameObject.FindWithTag("Phoenix") != null)
        {
            Destroy(GameObject.FindWithTag("Phoenix"));
        }
        if (GameObject.FindWithTag("Ninja") != null)
        {
            Destroy(GameObject.FindWithTag("Ninja"));
        }

        


    }

    private IEnumerator HadesSymbolWait(Vector3 position)
    {
        yield return new WaitForSeconds(1f);
        CreateEnamy(position);

    }

    public void RestartGame()
    {
        Debug.Log("restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
