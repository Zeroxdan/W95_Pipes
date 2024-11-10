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

    public int exampleIndex = 0;


    // Variables
    public float currentAngle = 0f;                                         // Winkel
    public Vector3 currentPosition = Vector3.zero;                          // current Position
    public Quaternion currentRotation = new Quaternion(0f, 0f, 0f, 0f);     // current Rotation
    public int i = 0;                                                       // current Iteration

    public Vector3 tempPosition = Vector3.zero;                             // temp Position
    public Quaternion tempRotation = new Quaternion(0f, 0f, 0f, 0f);        // temp Rotation
    public int t = 0;                                                       // temp Iteration


    // für die Farbeänderung nötig (HSV)
    [SerializeField] private Material pipeColor;
    public float hValue = 0f;
    public float sValue = 0f;
    public float vValue = 0f;



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
        currentPosition = transform.position;
        currentRotation = transform.rotation;
        tempPosition = transform.position;
        tempRotation = transform.rotation;



    }

    // Update is called once per frame
    void Update()
    {

        // BEISPIEL 1: Randomisation  (zufällige Instanzierung mit Interaktion)
        // Zuerst habe ich versucht, die Röhren je nach Eingabe in einer bestimmte Grenze zu instanzieren,
        // um zu sehen, wie es sich aussieht. (RGB Pipes) (Space -> E / Klick)

        if (exampleIndex.Equals(1))
        {
            // Wenn spacebar ist gedrückt und ...
            if (jumpAction.IsPressed())
            {
                // ... einer von dieser Bedingungen erfüllt ist, dann...

                // GREEN PIPE 
                // ( dies bleibt, bis man die Taste lässt ) -> unendliche Instanzierung mit Interaktion
                if (fireAction.IsPressed())
                {
                    // ... werden die Positions / Rotations auf diesem Range geändert (damit die vor der Kamera sichtbar bleiben) 
                    PositionRandomizer.Set(Random.Range(-20, 30), Random.Range(-15, 10), Random.Range(40, 60));
                    RotationRandomizer.Set(Random.Range(1, 20), Random.Range(1, 20), Random.Range(1, 20), Random.Range(1, 20));
                    // und dann eine Kopie erstellen, die diese randomized Positions / Rotations nimmt
                    Instantiate(GreenPipe, PositionRandomizer, RotationRandomizer);
                }

                // RED PIPE 
                // ( hier wird nur ein Klick erlaubt ) -> endliche Instanzierung mit Interaktion
                if (Input.GetButtonDown("Fire1"))
                {
                    // ... werden die Positions / Rotations auf diesem Range geändert (damit die vor der Kamera sichtbar bleiben) 
                    PositionRandomizer.Set(Random.Range(-20, 30), Random.Range(-15, 10), Random.Range(40, 60));
                    RotationRandomizer.Set(Random.Range(1, 20), Random.Range(1, 20), Random.Range(1, 20), Random.Range(1, 20));
                    // und dann eine Kopie erstellen, die diese randomized Positions / Rotations nimmt
                    Instantiate(RedPipe, PositionRandomizer, RotationRandomizer);
                }

                // BLUE PIPE 
                // ( hier wird jeder Sekunde ein neues Rohr instanziert ) -> unendliche Instanzierung ohne Interaktion
                if (Time.time > 0f)
                {
                    // ... werden die Positions / Rotations auf diesem Range geändert (damit die vor der Kamera sichtbar bleiben) 
                    PositionRandomizer.Set(Random.Range(-20, 30), Random.Range(-15, 10), Random.Range(40, 60));
                    RotationRandomizer.Set(Random.Range(1, 20), Random.Range(1, 20), Random.Range(1, 20), Random.Range(1, 20));
                    // und dann eine Kopie erstellen, die diese randomized Positions / Rotations nimmt
                    Instantiate(BluePipe, PositionRandomizer, new Quaternion(0f, 0f, 90f, 0f));
                }

            }

        }

        // BEISPIEL 2: Generation (Zeit-bedingte Instanzierung ohne Interaktion) 
        // Danach habe ich versucht, die Röhren in eine bestimmte Richtung zu instanzieren
        // und in begrenzte Winkeln zu biegen. Dies ist ähnlich wie beim der Windows 95 Screensaver,
        // aber noch nicht genau das gleiche. (RGB+Brown Pipes) ( left shift + 15 sec)

        if (exampleIndex.Equals(2))
        {

            if (Input.GetKey("left shift"))
            {

                // 2.1 unendliche Linie mit rotating pipes (0,90,180,360)
                {
                    if (Time.time > 10f)
                    {

                        // Hier addieren wir jedes Mal ein Schritt nach vorne

                        currentAngle += 90f;
                        currentPosition += (Vector3.forward * 5f);
                        currentRotation = Quaternion.AngleAxis(currentAngle, currentPosition);
                        // und dann instanzieren wir ein Rohr, der die neue Position enthält und
                        // derer Rotation nach dieser Position sich orientiert
                        Instantiate(BluePipe, currentPosition, currentRotation);

                    }
                }

                {
                    // 2.2 unendliche Treppen
                    {
                        if (Time.time > 5f)
                        {
                            if (i.Equals(3))
                            {
                                currentRotation = Quaternion.Euler(Vector3.RotateTowards(transform.position, currentPosition + Vector3.up * 2f, 90f, 90f));
                                currentPosition += Vector3.up * 2f;
                                i = 0;
                                Instantiate(RedPipe, currentPosition, currentRotation);
                            }
                            else
                            {
                                // Scale ist 2f für jedes Rohr, deswegen soll sich jedes neues Rohr 2f nach vorne bewegen
                                currentPosition += Vector3.forward * 2f;
                                i++;
                                Debug.Log(i);
                                Instantiate(GreenPipe, currentPosition, transform.rotation);

                            }
                        }
                    }
                }

                {

                    // 2.3 endless snake (no snapping) 

                    {
                        if (Time.time > 0f)
                        {


                            currentPosition += Vector3.forward * 2f * Random.Range(-1f, 1f);
                            currentPosition += Vector3.up * 2f * Random.Range(-1f, 1f);
                            currentPosition += Vector3.right * 2f * Random.Range(-1f, 1f);
                            currentRotation = Quaternion.FromToRotation(transform.position, currentPosition);
                            Instantiate(BrownPipe, currentPosition, currentRotation);



                        }
                    }


                }
            }

        }

        // BEISPIEL 3: Simulation (Zeit-bedingte Instanzierung mit Parametern)
        // Hier habe ich versucht, die Röhren mathematisch in einen bestimmten 2D Pfad zu instanzieren.
        // Leider ist das begrenzt. Um eine zufällige Simulation durchzuführen, wurde ich nach 
        // diesem Beispiel jeder Möglichkeit beschreiben sollen; so habe ich bemerkt, dass die Lösung
        // wahrscheinlich mit Colliders und Physik zu tun hat. 
        // ( RandomColor() + 2D Rectangle + Instanzierung mit Klicks) 
        if (exampleIndex.Equals(3))
        {
            
            if (Time.time > 0f)
            {
                // Farbeänderung per Klick (nach x Frames) 
                if (Input.GetButton("Fire1") && i > 0)
                {

                    RandomColor();

                    if (currentPosition.x.Equals(6f) && currentPosition.y < 5f)
                    {
                        currentPosition += Vector3.up * 1f;
                        tempRotation = Quaternion.Euler(0f, 0f, 0f);
                        Instantiate(SoloPipe, currentPosition, tempRotation);
                    }
                    else
                    {
                        if (currentPosition.x > -8f && currentPosition.y.Equals(5f))
                        {
                            currentPosition += Vector3.right * -2f;
                            tempRotation = Quaternion.Euler(0f, 0f, 90f);
                            Instantiate(SoloPipe, currentPosition, tempRotation);
                        }
                        else
                        {
                            if (currentPosition.x.Equals(-8f) && -4f < currentPosition.y && currentPosition.y <= 5f)
                            {
                                currentPosition += Vector3.up * -1f;
                                tempRotation = Quaternion.Euler(0f, 0f, 0f);
                                Instantiate(SoloPipe, currentPosition, tempRotation);

                            }
                            else
                            {
                                if (currentPosition.x.Equals(-8f) && currentPosition.y.Equals(-5f))
                                {
                                    currentPosition += Vector3.right * 2f;
                                    tempRotation = Quaternion.Euler(0f, 0f, 90f);
                                    Instantiate(SoloPipe, currentPosition, tempRotation);


                                }
                                else
                                {
                                    if (currentPosition.x.Equals(6f) && currentPosition.y > -4f && currentPosition.z < 15)
                                    {
                                        currentPosition += Vector3.forward * 1f;
                                        tempRotation = Quaternion.Euler(0f, 90f, 90f);
                                        Instantiate(SoloPipe, currentPosition, tempRotation);

                                        currentPosition += Vector3.up * 1f;
                                        tempRotation = Quaternion.Euler(0f, 0f, 0f);
                                        Instantiate(SoloPipe, currentPosition, tempRotation);

                                        Debug.Log("pushed back");
                                    }
                                    else
                                    {
                                        currentPosition += Vector3.right * 2f; tempRotation = Quaternion.Euler(0f, 0f, 90f); Instantiate(SoloPipe, currentPosition, tempRotation);
                                    }
                                }

                            }
                        }
                    }
                }
                i = 0;
                i++;

            }
        }

        // BEISPIEL 4: Functions (Vereinfachung der Instanzierung) 
        // Hier habe ich einfach nur eine Funktion erstellt, dass die oben genannte Prozess vereinfacht. 
        // (RandomColor() + unendliche Instanzierung mit Pipe3D Funktion)
        if (exampleIndex.Equals(4))
        {

            if (Time.time > 0f)
            {
                if (i > 100)
                {
                    RandomColor();
                    i = 0;
                }

                Pipe3D(t,
                    Random.Range(-8f, 8f), Random.Range(-5f, 5f), Random.Range(5f, 15f),
                    0f, 0f, 90f);

            }

        }


        // BEISPIEL 5: Physics (Unity Muse + Colliders) 
        // Hier habe ich der Unity KI namens Muse genutzt, um ein Script zu generieren, die 
        // eine begrenzte Anzahl von Röhren in einem bestimmten 3D Raum instanziert. 
        // Ich habe auch selbst ein 3D Raum außerhalb dieses Box erstellt, um zu probieren
        // wie ich das in anderen Wegen erledigen kann. (BoundingBox oder Planes) 
        // Ich habe verschiedene Versuche mit Colliders und Scripts gemacht,
        // um die Unity-Physik besser zu verstehen.
        if (exampleIndex.Equals(5)) {
        // der Code steht unter PipeGeneratorAI.cs  (von Unity Muse erstellt) 
        }

        // BEISPIEL 6: Conclusion

        {


        }

    } 


    public void RandomColor()
    {
        hValue = Random.Range(0.0f, 1.0f);
        sValue = Random.Range(0.0f, 1.0f);
        vValue = Random.Range(0.0f, 1.0f);
        pipeColor.color = Color.HSVToRGB(hValue, sValue, vValue);
        Debug.Log("RandomColor: " + hValue + " " + sValue + " " + vValue);

    }

    public void Pipe3D(int room, float px, float py, float pz, float rx, float ry, float rz)
    {
        if ( 5 < room && room < 10)
        {
            // Instanzierung in einen 3D Raum
            tempPosition = new Vector3(px, py, pz);
            tempRotation = Quaternion.Euler(rx, ry, rz);
            i++;
            Instantiate(SoloPipe, tempPosition, tempRotation);
            currentPosition = tempPosition;
            t++;
        } 
        else {
            // Instanzierung in Beziehung zu vorheriges Röhres

            currentPosition += Vector3.right * 2f * px;
            currentPosition += Vector3.up * 1f * py;
            currentPosition += Vector3.forward * 1f * pz;
            tempRotation = Quaternion.Euler(rx, ry, rz);
            i++;
            Instantiate(SoloPipe, currentPosition, tempRotation);
            tempPosition = currentPosition;
            t++;
        }

        if (t.Equals(10)) { t = 0; }


    }

}



