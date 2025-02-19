using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine.UI;
using UnityEditor.UI;
using UnityEditor;

public class SpawnMenu : MonoBehaviour
{
    
    public GameObject menu;

    FirstPersonController controller;

    public GameObject vanObjekti;
    public SerializedDictionary<GameObject, GameObject> spawnGumbIObjekt = new SerializedDictionary<GameObject, GameObject>();

    public Transform spawnPoint;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        menu = transform.Find("SpawnMenu").gameObject;
        //postavi objekte i slikice na gumbe

        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();

        foreach (GameObject gumb in spawnGumbIObjekt.Keys) {

            GameObject trenutniObjekt = spawnGumbIObjekt[gumb];

            UnityEngine.UI.Button button = gumb.GetComponent<UnityEngine.UI.Button>();

            button.onClick.AddListener(delegate {spawnObject(gumb); });

            Image image = gumb.GetComponent<Image>();

            Texture2D tekstura = Resources.Load<Texture2D>(trenutniObjekt.name);

            if (tekstura != null)
            {
                image.sprite = Sprite.Create(tekstura, new Rect(0.0f, 0.0f, tekstura.width, tekstura.height), new Vector2(0.5f, 0.5f));
            }
            else 
            {
                Debug.Log("Problem sa teksturom za " + trenutniObjekt.name);
            }


        }
        
    }

    // Update is called once per fram
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            toggleMenu();
        }
    }

    public void spawnObject(GameObject gumb) {

        GameObject objektZaSpawnati = spawnGumbIObjekt[gumb];

        //ako objektu treba struja, spawnati sa uticnicom, ako objektu treba prekidac, spawnati sa prekidacem

        Debug.Log("Spawnujem " + objektZaSpawnati.name);

        Debug.Log(controller.transform.rotation.eulerAngles.y);

        Instantiate(objektZaSpawnati, position: spawnPoint.position, rotation: Quaternion.Euler(0, controller.transform.rotation.eulerAngles.y + 180, 0));

        toggleMenu();

    }

    private void toggleMenu()
    {

        if (menu.activeSelf)
        {
            menu.SetActive(false);
            controller.cameraCanMove = true;
            controller.playerCanMove = true;
            Cursor.visible = false;
        }
        else if (!menu.activeSelf)
        {
            menu.SetActive(true);
            controller.cameraCanMove = false;
            controller.playerCanMove = false;
            Cursor.visible = true;
        }
    }
    
}
