using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    /* ------FromTheTop-------*/
    public float xMinFromTheTop, xMaxFromTheTop,yFromTheTop,xScaleFromTheTop, yScaleFromTheTop; //Coord Correspondantes aux bords de la salle et taille de l'objets qui tombe
    public GameObject FallingGO; //Gameobject qui sert à la chute
    /* ------Poison-----------*/
    public List<GameObject> PlatformList; //Liste des plateformes existante pour le choix de celle qui vont se transformer
    public List<GameObject> SelectedPlatform;//Liste des platesformes qui vont se transformer
    public int nbPlatformPoisoned; //Nb de plateformes à transformer
    /* ------Direct Shoot-----*/
    public float yMaxDirectShoot,yMinDirectShoot, xTarget, xShoot; //Coord correspondant aux bords de la salle
    public GameObject Target, ShootingGO, Shoot; //Differents objets servant de cible, objet qui avance et origine
    /* ------FromTheCam-------*/
    public float xMinFromTheCam,xMaxFromTheCam,yMinFromTheCam,yMaxFromTheCam; //Coord correspondant aux bords de map (la ou le cercle peut arriver)
    public GameObject CircleArea; //Object qui tombe de la cam
    /* ------Dangerous Area---*/
    public float PosXDAFix,ScaleYDAFix;  //Coord correspondant a l'ancrage de la zone vis a vis du boss
    public float PosYDAVarMin,PosYDAVarMax, ScaleXDAVarMin, ScaleXDAVarMax; //Variable servant la taille et la hauteur de la zone
    public Vector3 PosArea,ScaleArea; //Position et scale de la zone
    public GameObject TriangleArea; //Go qui sert a pop le triangle
    /* -----------------------*/

    public float TimeBtwAttack, StartTimeBtwAttack; //Timer d'attaque (varier le start pour augmenter ou diminuer le cd d'attaque du boss)

    // Start is called before the first frame update
    void Start()
    {  
        TimeBtwAttack = StartTimeBtwAttack; //Premier timer qui oblige le boss a attaquer apres seulement un tour de time
    }

    // Update is called once per frame
    void Update()
    {
        PlatformList = GameObject.Find("Generateur").GetComponent<Generateur_Platforme>().FilePlatform; //Mise a jour de la liste des platformes deja existante 
        //Choix de l'attaque avec chacune 1 chance sur 6 de tomber donc 1/6 de ne rien faire pour laisser du repit au joueur
        if (TimeBtwAttack <= 0)
        {
            float Action = Random.value;
            print(Action); //Affichage de la selection pour correction dans la console
            if (Action <=(float)1/6)
            {
                DirectShoot();
            }
            else {
                if (Action <=(float)2/6)
                {
                    DangerousArea();
                }
                else
                {
                    if (Action <=(float)3/6)
                    {
                        PoisonousPlatform();
                    }
                    else
                    {
                        if (Action <=(float)4/6)
                        {
                            FromTheCam();
                        }
                        else
                        {
                            if (Action <=(float)5/6)
                            {
                                FromTheTop();
                            }
                            else
                            {
                                print("Ne fais rien");
                            }
                        }
                    }
                }
            }
            TimeBtwAttack = StartTimeBtwAttack; //debut du timer de cooldown
        }
        else 
        {
            TimeBtwAttack -= Time.deltaTime; //reduction du timer
        }
    }
    public void DirectShoot()
    {
        print("Shoot");
        //Generate target
        float TargetYF = yMinDirectShoot + (Random.value * (yMaxDirectShoot - yMinDirectShoot)); //Choix de la hauteur de la cible entre deux bornes
        Vector3 TargetPos = new Vector3(xTarget,TargetYF,0f);
        Target.GetComponent<Transform>().localPosition = TargetPos;
        Instantiate(Target);
        //Generate Shooter
        float ShooterYF = yMinDirectShoot + (Random.value * (yMaxDirectShoot - yMinDirectShoot)); //Choix de la hauteur de l'origine du tir entre deux bornes
        Vector3 ShooterPos = new Vector3(xShoot,ShooterYF,0f);
        Shoot.GetComponent<Transform>().localPosition = ShooterPos;
        Instantiate(Shoot);
        //Generate shootingGO
        ShootingGO.GetComponent<Transform>().localPosition = ShooterPos; //Positionnement du projectile sur son origine 
        Instantiate(ShootingGO); //script de deplacement dans ShootingGO
    }
    public void DangerousArea()
    {
        print("Dangereux");
        //Random Pos -> Y
        float randomYF = PosYDAVarMin + (Random.value * (PosYDAVarMax - PosYDAVarMin)); 
        PosArea = new Vector3(PosXDAFix, randomYF, 0f);
        //Random Scale -> X
        float randomXF = ScaleXDAVarMin + (Random.value * (ScaleXDAVarMax - ScaleXDAVarMin));
        ScaleArea = new Vector3(randomXF, ScaleYDAFix, 0f);
        //Instant
        TriangleArea.GetComponent<Transform>().localPosition = PosArea; // Affectation dans les variable pour reutilisation à termes
        TriangleArea.GetComponent<Transform>().localScale = ScaleArea; // Affectation dans les variable pour reutilisation à termes
        Instantiate(TriangleArea); //Scirpt d'activation dans le prefab
        
    }
    public void PoisonousPlatform()
    {
        print("Le poison !");
        int Count = 0; //Compteur pour reset nbPlatformPoisoned en fin d'attaque
        while (nbPlatformPoisoned > 0){
            List<int> AlreadySelectioned = new List<int>(); //Tentative de selection unique mais fonctionne pas :'(
            int SelectionI = -1;
            while ((SelectionI == -1)&&(!AlreadySelectioned.Contains(SelectionI))){
                float SelectionF = Random.value * PlatformList.Count; //Selections des platformes parmis celle deja existante
                SelectionI = (int) SelectionF;
                if (SelectionI == PlatformList.Count){
                    SelectionI-=1;
                }
            }
            SelectedPlatform.Add(PlatformList[SelectionI]);
            AlreadySelectioned.Add(SelectionI); //ajout dans la liste des futurs modifié
            nbPlatformPoisoned -= 1;
            Count += 1;
        }
        for(int i = 0; i < SelectedPlatform.Count; i++){ //parmis toute les plateformes a transformer
            Vector3 newShapeOfPoisonousPlatform = new Vector3(1f,1f,0f); 
            SelectedPlatform[i].GetComponent<Transform>().localScale = newShapeOfPoisonousPlatform; //changement du visuel des platformes pour affichage
        }
        nbPlatformPoisoned += Count; //reset nbPlatformPoisoned
        SelectedPlatform.Clear(); //clear list pour next attaque
    }
    public void FromTheCam()
    {
        print("Camerattaque");
        //RandomCoord
        float CircleAreaXF = xMinFromTheCam + (Random.value * (xMaxFromTheCam - xMinFromTheCam));
        float CircleAreaYF = yMinFromTheCam + (Random.value * (yMaxFromTheCam - yMinFromTheCam));

        Vector3 CoordArea = new Vector3(CircleAreaXF,CircleAreaYF,0f); 
        CircleArea.GetComponent<Transform>().localPosition = CoordArea;//affectation des coordonnées au prefab
        Instantiate(CircleArea); //Script de retrecissement dans le prefab
    }
    public void FromTheTop()
    {
        print("Par toutatis");
        //Randomisation Falling
        float PosXFallingF = xMinFromTheTop + (Random.value * (xMaxFromTheTop - xMinFromTheTop)); //choix position en x de l'objet qui va tomber
        int PosXFallingI = (int)PosXFallingF; //ajustement en entier pour que ca soit plus joli
        //Affectation GO
        Vector3 PosFalling = new Vector3(PosXFallingI,yFromTheTop,0f); 
        Vector3 ScaleFalling = new Vector3(xScaleFromTheTop, yScaleFromTheTop, 0f);
        FallingGO.transform.localScale = ScaleFalling; //affectation de la taille souhaité
        FallingGO.transform.localPosition = PosFalling; //affectation de la position
        //Instantiate GO
        Instantiate (FallingGO); //Scirpt de chute dans le prefab
    }
}