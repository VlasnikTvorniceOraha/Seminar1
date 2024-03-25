using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    // Skripta za definiranje spajanja objekata

    public Transform player;

    public PlayerInfo playerInfo;

    public Transform ObjektiUticnice;
    public Transform ObjektiPrekidaci;

    public Transform prekidaci;

    public Transform uticnice;

    public HoldObjects holdObjects;

    public GameObject objektiVanSobe;
    void Start()
    {
        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        holdObjects = GameObject.FindGameObjectWithTag("Player").GetComponent<HoldObjects>();
        objektiVanSobe =  GameObject.FindGameObjectWithTag("VanSobe");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpojiObjekte() {
        //funkcija koja spaja objekte zajedno

        Debug.Log("Spajam stvari u sobi");
        //otkrij koje stvari su nespojene
        List<Transform> ObjektiUticniceNespojeni = new List<Transform>();
        List<Transform> ObjektiPrekidaciNespojeni = new List<Transform>();

        List<Transform> UticniceNespojene = new List<Transform>();
        List<Transform> PrekidaciNespojeni = new List<Transform>();

        foreach (Transform child in ObjektiUticnice) {
            //Debug.Log(child.name);
            StrujniObjekt skripta = child.GetComponent<StrujniObjekt>();
            if (skripta.uticnica == null) {
                ObjektiUticniceNespojeni.Add(child);
            }

        }

        foreach (Transform child in ObjektiPrekidaci) {
            //Debug.Log(child.name);
            LampaSkripta skripta = child.GetComponent<LampaSkripta>();
            if (skripta.prekidac == null) {
                ObjektiPrekidaciNespojeni.Add(child);
            }

        }

        foreach (Transform child in uticnice) {
            //Debug.Log(child.name);
            UticnicaSkripta skripta = child.GetComponent<UticnicaSkripta>();
            if (skripta.spojeniObjekt == null) {
                UticniceNespojene.Add(child);
            }

        }

        foreach (Transform child in prekidaci) {
            //Debug.Log(child.name);
            PrekidacSkripta skripta = child.GetChild(1).GetComponent<PrekidacSkripta>();
            if (skripta.spojeniObjekt == null) {
                ObjektiPrekidaciNespojeni.Add(child);
            }

        }

        Debug.Log(ObjektiUticniceNespojeni.Count);
        Debug.Log(UticniceNespojene.Count);

        //sada kada znamo sve nespojene objekte, spojimo ih

        //prvo uticnice i strujne objekte


        foreach (Transform nespojeniObjekt in ObjektiUticniceNespojeni) {

            //pronadi najblizu uticnicu i spoji je sa objektom
            StrujniObjekt skripta = nespojeniObjekt.GetComponent<StrujniObjekt>();

            if (UticniceNespojene.Count > 0) {

                float najmanjaUdaljenost = Vector3.Distance(nespojeniObjekt.position, UticniceNespojene[0].position);
                Transform odabranaUticnica = UticniceNespojene[0];

                foreach (Transform uticnica in UticniceNespojene) {
                    float udaljenost = Vector3.Distance(nespojeniObjekt.position, uticnica.position);
                    if (udaljenost < najmanjaUdaljenost) {
                        najmanjaUdaljenost = udaljenost;
                        odabranaUticnica = uticnica;
                    }
                }

                //spoji uticnicu sa objektom i makni uticnicu iz nespojenih i objekt iz nespojenih
                skripta.uticnica = odabranaUticnica.gameObject;
                odabranaUticnica.GetComponent<UticnicaSkripta>().spojeniObjekt = nespojeniObjekt.gameObject;

                UticniceNespojene.Remove(odabranaUticnica);
                //ObjektiUticniceNespojeni.Remove(nespojeniObjekt);

            }

        }

    }

    public void OdspojiSve() {
        //za odspajanje svih objekata
        Debug.Log("Spajam stvari u sobi");


    }

    private void OnTriggerEnter(Collider other) {
        
        //provjeri je li item usao u trigger

        if (other.gameObject.layer == 12) {
            Debug.Log("Item usao, pokusavam spojit");
            //stavi roditelja ovisno o tipu objekta
            if (!holdObjects.holding || (holdObjects.holding && holdObjects.heldObject != other.gameObject)) {
                //igrac ne drzi nista, samo spoji ovisno o tagu ili drzi objekt i zgurao je neki drugi objekt u sobu
                switch(other.gameObject.tag) {
                    
                    case "StrujniObjekt":
                    other.transform.SetParent(ObjektiUticnice, true);
                    break;

                    case "Uticnina":
                    other.transform.SetParent(uticnice, true);
                    break;

                    case "Prekidac":
                    other.transform.SetParent(prekidaci, true);
                    break;

                    case "Lampa":
                    other.transform.SetParent(ObjektiPrekidaci, true);
                    break;

                    default:
                    break;

                }

            } else if (holdObjects.holding && holdObjects.heldObject == other.gameObject) {
                //igrac drzi isti objekt koji je usao, stavi prijasnji roditelj u holding skripti
                switch(other.gameObject.tag) {
                    
                    case "StrujniObjekt":
                    holdObjects.prijasnjiParent = ObjektiUticnice.gameObject;
                    break;

                    case "Uticnina":
                    holdObjects.prijasnjiParent = uticnice.gameObject;
                    break;

                    case "Prekidac":
                    holdObjects.prijasnjiParent = prekidaci.gameObject;
                    break;

                    case "Lampa":
                    holdObjects.prijasnjiParent = ObjektiPrekidaci.gameObject;
                    break;

                    default:
                    break;

                }

            }

        }

        //ili igrac
        if (other.tag == "Player") {
            //assignaj ovu sobu kao trenutnu
            playerInfo.currentRoom = this.GetComponent<RoomController>();
            Debug.Log("Usao u novu sobu");
        }
    }

    private void OnTriggerExit(Collider other) {
        
        if (other.gameObject.layer == 12) {
            if (!holdObjects.holding || (holdObjects.holding && holdObjects.heldObject != other.gameObject)) {
                //igrac ne drzi nista, samo odspoji ili drzi objekt i zgurao je neki drugi objekt iz sobe
                other.transform.SetParent(objektiVanSobe.transform, true);

            

            } else if (holdObjects.holding && holdObjects.heldObject == other.gameObject) {
                //igrac drzi isti objekt koji je usao, stavi prijasnji roditelj u holding skripti
                holdObjects.prijasnjiParent = objektiVanSobe;
                

            }
        }

    }
}
