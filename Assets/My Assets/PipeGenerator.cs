//using System.Numerics;
//using Unity.VisualScripting;
//using System.Collections;
//using System.Collections.Generic;
using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.X86;



public class PipeGenerator : MonoBehaviour
{




    // Variables
    public float c = 0f;                                    // Winkel
    public Vector3 b = Vector3.zero;                        // current Position
    public Quaternion a = new Quaternion(0f, 0f, 0f, 0f);   // current Rotation
    public int i = 0;                                       // current Iteration

    public Vector3 bt = Vector3.zero;                        // temp Position
    public Quaternion at = new Quaternion(0f, 0f, 0f, 0f);   // temp Rotation
    public int t = 0;                                       // temp Iteration



    // für die Farbeänderung nötig (HSV)
    [SerializeField] private Material matt;
    public float hh = 0f;
    public float ss = 0f;
    public float vv = 0f;



    // Hier werde ich Player Inputs speichern
    InputAction fireAction;
    InputAction jumpAction;

    // Die Pipes sind Game Objects, die der Eigenschaften von Röhren beinhalten
    public GameObject GreenPipe;
    public GameObject RedPipe;
    public GameObject BluePipe;
    public GameObject BrownPipe;
    public GameObject SoloPipe;

    // Außerdem initialisieren wir die Randomizers, die wir später für Position und Rotation nutzen werden
    public Vector3 PositionRandomizer = Vector3.zero;
    public Quaternion RotationRandomizer = new Quaternion(0f, 0f, 0f, 0f);



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        t = 1;

        // Wenn der Player eine dieser Aktionen ausführt, wird es hier gespeichert
        // Später werden wir der Ausführung dieser Aktionen überprüfen
        fireAction = InputSystem.actions.FindAction("Interact");
        jumpAction = InputSystem.actions.FindAction("Jump");


        // aktuelle Position und Rotation
        b = transform.position;
        a = transform.rotation;
        bt = transform.position;
        at = transform.rotation;



    }

    // Update is called once per frame
    void Update()
    {

        // BEISPIEL 1: Controlled Randomisation  (zufällige Instanzierung mit Interaktion)
        // Zuerst habe ich versucht, die Röhren je nach Eingabe in einer bestimmte Grenze zu instanzieren,
        // um zu sehen, wie es sich aussieht. (RGB Pipes) 

        {
            //// Wenn spacebar ist gedrückt und ...
            //if (jumpAction.IsPressed())
            //{
            //    // ... einer von dieser Bedingungen erfüllt ist, dann...

            //    // GREEN PIPE 
            //    // ( dies bleibt, bis man die Taste lässt ) -> unendliche Instanzierung mit Interaktion
            //    if (fireAction.IsPressed())
            //    {
            //        // ... werden die Positions / Rotations auf diesem Range geändert (damit die vor der Kamera sichtbar bleiben) 
            //        PositionRandomizer.Set(Random.Range(-20, 30), Random.Range(-15, 10), Random.Range(40, 60));
            //        RotationRandomizer.Set(Random.Range(1, 20), Random.Range(1, 20), Random.Range(1, 20), Random.Range(1, 20));
            //        // und dann eine Kopie erstellen, die diese randomized Positions / Rotations nimmt
            //        Instantiate(GreenPipe, PositionRandomizer, RotationRandomizer);
            //    }

            //    // RED PIPE 
            //    // ( hier wird nur ein Klick erlaubt ) -> endliche Instanzierung mit Interaktion
            //    if (Input.GetButtonDown("Fire1"))
            //    {
            //        // ... werden die Positions / Rotations auf diesem Range geändert (damit die vor der Kamera sichtbar bleiben) 
            //        PositionRandomizer.Set(Random.Range(-20, 30), Random.Range(-15, 10), Random.Range(40, 60));
            //        RotationRandomizer.Set(Random.Range(1, 20), Random.Range(1, 20), Random.Range(1, 20), Random.Range(1, 20));
            //        // und dann eine Kopie erstellen, die diese randomized Positions / Rotations nimmt
            //        Instantiate(RedPipe, PositionRandomizer, RotationRandomizer);
            //    }

            //    // BLUE PIPE 
            //    // ( hier wird jeder Sekunde ein neues Rohr instanziert ) -> unendliche Instanzierung ohne Interaktion
            //    if (Time.time > 0f)
            //    {
            //        // ... werden die Positions / Rotations auf diesem Range geändert (damit die vor der Kamera sichtbar bleiben) 
            //        PositionRandomizer.Set(Random.Range(-20, 30), Random.Range(-15, 10), Random.Range(40, 60));
            //        RotationRandomizer.Set(Random.Range(1, 20), Random.Range(1, 20), Random.Range(1, 20), Random.Range(1, 20));
            //        // und dann eine Kopie erstellen, die diese randomized Positions / Rotations nimmt
            //        Instantiate(BluePipe, PositionRandomizer, new Quaternion(0f, 0f, 90f, 0f));
            //    }

            //}
        }

        // BEISPIEL 2: Uncontrolled Generation (Zeit-bedingte Instanzierung ohne Interaktion) 
        // Danach habe ich versucht, die Röhren in eine bestimmte Richtung zu instanzieren
        // und in begrenzte Winkeln zu biegen. Dies ist ähnlich wie beim der Windows 95 Screensaver,
        // aber noch nicht genau das gleiche. (RGB+Brown Pipes) 

        {

        //    {

        //        // 2.1 unendliche Linie mit rotating pipes (0,90,180,360)
        //        {
        //            if (Time.time > 10f)
        //            {

        //                // Hier addieren wir jedes Mal ein Schritt nach vorne

        //                c += 90f;
        //                b += (Vector3.forward * 5f);
        //                //b += renderer.bounds.size; 
        //                a = Quaternion.AngleAxis(c, b);
        //                // a = Quaternion.FromToRotation(transform.position, b)

        //                //BrownPipe.GetComponent<Renderer>()
        //                //    .material.SetColor("_BaseColor",
        //                //    Color.HSVToRGB(Random.Range(0.0f,1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));
        //                //////  habe Random.ColorHSV() nicht benutzt, weil es hier nicht funktionierte


        //                // und dann instanzieren wir ein Rohr, der die neue Position enthält und
        //                // derer Rotation nach dieser Position sich orientiert
        //                Instantiate(BluePipe, b, a);

        //            }
        //        }

        //    {
        //        // 2.2 unendliche Treppen
        //        {
        //            if (Time.time > 5f)
        //            {
        //                if (i.Equals(3))
        //                {
        //                    a = Quaternion.Euler(Vector3.RotateTowards(transform.position, b + Vector3.up * 2f, 90f, 90f));
        //                    b += Vector3.up * 2f;
        //                    i = 0;
        //                    Instantiate(RedPipe, b, a);
        //                }
        //                else
        //                {
        //                    // Scale ist 2f für jedes Rohr, deswegen soll sich jedes neues Rohr 2f nach vorne bewegen
        //                    b += Vector3.forward * 2f;
        //                    i++;
        //                    Debug.Log(i);
        //                    Instantiate(GreenPipe, b, transform.rotation);

        //                }
        //            }
        //        }
        //    }

        //    {

        //        // 2.3 endless snake (no snapping) 

        //        {
        //            if (Time.time > 0f)
        //            {


        //                b += Vector3.forward * 2f * Random.Range(-1f, 1f);
        //                b += Vector3.up * 2f * Random.Range(-1f, 1f);
        //                b += Vector3.right * 2f * Random.Range(-1f, 1f);
        //                a = Quaternion.FromToRotation(transform.position, b);
        //                Instantiate(BrownPipe, b, a);



        //            }
        //        }


        //    }
        //}

        }

        // BEISPIEL 3: Controlled Simulation (Zeit-bedingte Instanzierung mit Parametern)
        // Zum Schluss habe ich versucht, die Röhre je nach bestimmte Parametern zu erstellen. 
        // Das erlaubt eine kontrollierte Wachstum von Röhren, die auf bestimmten Eigenschaften gesetzt werden kann. 
        // Das wäre hilfreich, um einen Prozess auf bestimmten Eigenschaften zu simulieren. (SoloPipe) 

        // 3.1 RandomColor + 2D Rectangle 
        {
            //if (Time.time > 0f)
            //{
            //    // Farbeänderung per Klick (nach x Frames) 
            //    if (Input.GetButton("Fire1") && i > 0)
            //    {

            //        RandomColor();

            //        if (b.x.Equals(6f) && b.y < 5f)
            //        {
            //            b += Vector3.up * 1f;
            //            at = Quaternion.Euler(0f, 0f, 0f);
            //            Instantiate(SoloPipe, b, at);
            //        }
            //        else
            //        {
            //            if (b.x > -8f && b.y.Equals(5f))
            //            {
            //                b += Vector3.right * -2f;
            //                at = Quaternion.Euler(0f, 0f, 90f);
            //                Instantiate(SoloPipe, b, at);
            //            }
            //            else
            //            {
            //                if (b.x.Equals(-8f) && -4f < b.y && b.y <= 5f)
            //                {
            //                    b += Vector3.up * -1f;
            //                    at = Quaternion.Euler(0f, 0f, 0f);
            //                    Instantiate(SoloPipe, b, at);

            //                }
            //                else
            //                {
            //                    if (b.x.Equals(-8f) && b.y.Equals(-5f))
            //                    {
            //                        b += Vector3.right * 2f;
            //                        at = Quaternion.Euler(0f, 0f, 90f);
            //                        Instantiate(SoloPipe, b, at);


            //                    }
            //                    else
            //                    {
            //                        if (b.x.Equals(6f) && b.y > -5f && b.z < 15)
            //                        {
            //                            b += Vector3.forward * 1f;
            //                            at = Quaternion.Euler(0f, 90f, 90f);
            //                            Instantiate(SoloPipe, b, at);

            //                            b += Vector3.up * 1f;
            //                            at = Quaternion.Euler(0f, 0f, 0f);
            //                            Instantiate(SoloPipe, b, at);

            //                            Debug.Log("pushed back");
            //                        }
            //                        else
            //                        {
            //                            b += Vector3.right * 2f; at = Quaternion.Euler(0f, 0f, 90f); Instantiate(SoloPipe, b, at);
            //                        }
            //                    }

            //                }
            //            }
            //        }


            //    }





            //    i = 0;



            //    i++;





            //}
            //// end of 3.1
        }


        // 3.2 
        {
            // 3.2 start

            if (Time.time > 0f)
            {
                if (i > 100)
                {
                    RandomColor();
                    i = 0;
                }

                Pipe3D(t,
                    Random.Range(-8f,8f), Random.Range(-5f, 5f), Random.Range(5f, 15f),
                    0f, 0f,90f);


            }


            // 3.2 end
        }


    }


    public void RandomColor()
    {
        hh = Random.Range(0.0f, 1.0f);
        ss = Random.Range(0.0f, 1.0f);
        vv = Random.Range(0.0f, 1.0f);
        matt.color = Color.HSVToRGB(hh, ss, vv);
        Debug.Log("RandomColor: " + hh + " " + ss + " " + vv);

    }

    public void Pipe3D(int room, float px, float py, float pz, float rx, float ry, float rz)
    {
        if ( 5 < room && room < 10)
        {
            // Instanzierung in einen 3D Raum
            bt = new Vector3(px, py, pz);
            at = Quaternion.Euler(rx, ry, rz);
            i++;
            Instantiate(SoloPipe, bt, at);
            b = bt;
            t++;
        } 
        else {
            // Instanzierung in Beziehung zu vorheriges Röhres

            b += Vector3.right * 2f * px;
            b += Vector3.up * 1f * py;
            b += Vector3.forward * 1f * pz;
            at = Quaternion.Euler(rx, ry, rz);
            i++;
            Instantiate(SoloPipe, b, at);
            bt = b;
            t++;
        }

        if (t.Equals(10)) { t = 0; }


    }

    // habe versucht, ein Collider zu nutzen, war aber nicht erfolgreich
    //private void OnTriggerEnter(Collider other)
    //{
    //    // wenn ein Rohr gegen ein anderes Rohr berüht, dann wird der nächste nach oben instanziert
    //    if (other.CompareTag("Pipe")) 
    //    {
    //        // hier können wir eine Bedingung hinzufügen, wenn nötig
    //        if (b.x.Equals(8))
    //        {
    //            b = new Vector3(b.x-2f, b.y+1f, b.z);
    //            Instantiate(SoloPipe, b, a);
    //        }
    //    }
    //}

}



