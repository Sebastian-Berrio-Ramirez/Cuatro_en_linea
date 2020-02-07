using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CuatroEnRaya : MonoBehaviour             // Se crea una clase llamada "CuatroEnRaya"
{
    public int counter = 0;
    public int width;                                 // Se definen variables de tipo entero, flotante, boleano y GameObject
    public int height;
    public GameObject puzzlePiece;
    private GameObject[,] grid;
    public bool player1;
    public GameObject panel;
    public Text winnerText;
    

    void Start()                                      // Se inicializa
    {
        grid = new GameObject[width, height];         // La matriz sera llenada por un GameObject que sus dimensiones seran dadas por "width" y "height"
        for (int x = 0; x < width; x++)               // Se crea una variable y se le asigna unas condiciones y un operacion
        {
            for (int y = 0; y < height; y++)          // Se crea una variable y se le asigna unas condiciones y un operacion
            {
                GameObject go = GameObject.Instantiate(puzzlePiece) as GameObject;   // Se crea una variable de tipo GameObject y se le instancia un objeto prefab desde la escena de unity
                Vector3 position = new Vector3(x, y, 0);                             // Se crea una variable de tipo Vector3 y se le da la posicion con las variables "x" y "y"
                go.transform.position = position;                                    // A la variable go su ubicacion es dada por "position"                               
                grid[x, y] = go;                                                     // En la matriz estara en cada posicion, go
            }
        }
    }

    void Update()                                           // Se ejecuta cada frame
    {

        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);     // Se crea una variable de tipo Vector3 y esta contendra la posicion del mouse                                     
        UpdatePickedPiece(mPosition);                                                // Se llama al metodo UpdatePickedPiece
        

    }
    void UpdatePickedPiece(Vector3 mposition)                                        // Se crea un metodo llamado "UpdatePickedPiece" con un parametro de tipo Vector3

    {
        int x = (int)(mposition.x + 0.5f);                                           // Se establece un cambio en la posicion del mouse en los dos ejes para mayor presicion del mouse en la matriz
        int y = (int)(mposition.y + 0.5f);

        if (x >= 0 && y >= 0 && x < width && y < height && mposition.x > -0.5f && mposition.y > -0.5f)   // Se crea un condicional que verifique todo el tamaño de la matriz y si son verdaderos
        {                                                                                                // se pasa a verificar si se presiona el click derecho en algun objeto contenido en la matriz,
            GameObject go = grid[x, y];                                                                  // si se presiona se pasa a verificar si el objeto es de color blanco, si lo es, se llama a los metodos
            if (Input.GetButtonDown("Fire1"))                                                            // necesarios
            {                                                                                            
                if (go.GetComponent<Renderer>().material.color == Color.white)                           
                {
                    Cambiocolor(go, player1);
                    player1 = !player1;  // Se hace esta diferencia para el cambio de jugador
                    VerificacionHorizontal(go, go.GetComponent<Renderer>().material.color);
                    VerificacionVertical(go, go.GetComponent<Renderer>().material.color);
                    VerificacionDiagonalD(go, go.GetComponent<Renderer>().material.color);
                    VerificacionDiagonalI(go, go.GetComponent<Renderer>().material.color);
                }

            }
        }
    }

    void Cambiocolor(GameObject go, bool Player1)                                                // Se crea un metodo llamado "CambioColor" con parametros de tipo GameObject y bool
    {
       
        
            if (this.player1)                                                                    // Si "player1" es verdadero, la esfera sera azul, si player1 es falso entonces la esfera sera roja
            {
                go.GetComponent<Renderer>().material.color = Color.blue;
            }
            else
            {
                go.GetComponent<Renderer>().material.color = Color.red;
            }
        

    }

    void VerificacionHorizontal(GameObject go, Color colorJugador)      // Se crea un metodo llamado "VerificacionHorizontal" con parametros de tipo GameObject y Color
    {
        counter = 0;                                                    // Se define un contador en 0
        int _x = (int)go.transform.position.x;                          // Se le asigna la posicion de go de x y y a las variables "_x" y "_y" respectivamente
        int _y = (int)go.transform.position.y;

        for (int x = (_x + 1); x < (_x + 4); x++)                       // Se crea una variable y se le asigna unas condiciones y un operacion
        {
            if (x < width)                                              // Si x es menor a la anchura, pasa a verificar si hay elementos del mismo color en una linea horizontal por la derecha,
            {                                                           // si los hay, agrega uno al contador por cada elemento del mismo color seguido en esa direccion
                if (grid[x, _y].GetComponent<Renderer>().material.color == colorJugador)
                {
                    counter++;
                }
            }
        }

        for (int x = _x - 1; x > _x - 4; x--)                       // Se crea una variable y se le asigna unas condiciones y un operacion
        {
            if (x > -1)                                                // Si x es mayor a -1, pasa a verificar si hay elementos del mismo color en una linea horizontal por la izquierda,
            {                                                          // si los hay, agrega uno al contador por cada elemento del mismo color seguido en esa direccion
                if (grid[x, _y].GetComponent<Renderer>().material.color == colorJugador)
                {
                    counter++;
                }
            }
        }
        if (counter == 3)                                           // Si el contador llega a 3 se reinicia la escena y se empieza el juego de nuevo
        {
            panel.SetActive(true);
            if (colorJugador==Color.red)
            {
                winnerText.text = "WINNER PLAYER RED";
                winnerText.color = Color.red;
            }
            else
            {
                winnerText.text = "WINNER PLAYER BLUE";
                winnerText.color = Color.blue;
            }
        }
    }
    void VerificacionVertical(GameObject go, Color colorJugador)             // Se crea un metodo llamado "VerificacionVertical" con parametros de tipo GameObject y Color
    {
        counter = 0;                                                            // Se define un contador en 0
        int _x = (int)go.transform.position.x;                                  // Se le asigna la posicion de go de x y y a las variables "_x" y "_y" respectivamente
        int _y = (int)go.transform.position.y;

        for (int y = _y + 1; y < (_y + 4); y++)                                // Se crea una variable y se le asigna unas condiciones y un operacion
        {
            if (y < height)                                                    // Si "y" es menor a la altura, pasa a verificar si hay elementos del mismo color en una linea vertical por arriba,
            {                                                                  // si los hay, agrega uno al contador por cada elemto del mismo color seguido en esa direccion
                if (grid[_x, y].GetComponent<Renderer>().material.color == colorJugador)
                {
                    counter++;
                }
            }
        }
        for (int y = _y - 1; y > _y - 4; y--)                                 // Se crea una variable y se le asigna unas condiciones y un operacion
        {
            if (y > -1)                                                       // Si "y" es mayor a -1, pasa a verificar si hay elementos del mismo color en una linea vertical por abajo,
            {                                                                 // si los hay, agrega uno al contador por cada elemento del mismo color seguido en esa direccion
                if (grid[_x, y].GetComponent<Renderer>().material.color == colorJugador)
                {
                    counter++;
                }
            }
        }
        
        if (counter == 3)                                                  // Si el contador llega a 3 se reinicia la escena y se empieza el juego de nuevo
        {
            panel.SetActive(true);
            if (colorJugador == Color.red)
            {
                winnerText.text = "WINNER PLAYER RED";
                winnerText.color = Color.red;
            }
            else
            {
                winnerText.text = "WINNER PLAYER BLUE";
                winnerText.color = Color.blue;
            }
        }
    }
    void VerificacionDiagonalD(GameObject go, Color colorJugador)           // Se crea un metodo llamado "VerificacionDiagonalD" con parametros de tipo GameObject y Color
    {
        counter = 0;                                                        // Se define un contador en 0
        int _x = (int)go.transform.position.x;                              // Se le asigna la posicion de go de x y y a las variables "_x" y "_y" respectivamente
        int _y = (int)go.transform.position.y;

        
        for (int i = 1; i < 4; i++)                                        // Se crea una variable y se le asigna unas condiciones y un operacion
        {
            if (_x + i < width && _y + i < height)                         // Si "_x+i" es menor a la anchura y "_y+i" es menor a la altura, pasa a verificar si hay elementos del mismo color en una linea diagonal hacia abajo en direccion a la izquierda,
            {                                                              // si los hay, agrega uno al contador por cada elemento del mismo color seguido en esa direccion
                if (grid[_x + i, _y + i].GetComponent<Renderer>().material.color == colorJugador)
                {
                    counter++;
                }
                if (counter == 3)                                             // Si el contador llega a 3 se reinicia la escena y se empieza el juego de nuevo
                {
                    panel.SetActive(true);
                    if (colorJugador == Color.red)
                    {
                        winnerText.text = "WINNER PLAYER RED";
                        winnerText.color = Color.red;
                    }
                    else
                    {
                        winnerText.text = "WINNER PLAYER BLUE";
                        winnerText.color = Color.blue;
                    }
                }

            }
            if (_x - i > -1 && _y - i > -1)                                  // Si "_x-i" mayor a -1 y "_y-i" es mayor a -1, pasa a verificar si hay elementos del mismo color en una linea hacia arriba en direccion a la derecha,
            {                                                                // si los hay, agrega uno al contador por cada elemto del mismo color seguido en esa direccion
                if (grid[_x - i, _y - i].GetComponent<Renderer>().material.color == colorJugador)
                {
                    counter++;
                }
                if (counter == 3)                                            // Si el contador llega a 3 se reinicia la escena y se empieza el juego de nuevo
                {
                    panel.SetActive(true);
                    if (colorJugador == Color.red)
                    {
                        winnerText.text = "WINNER PLAYER RED";
                        winnerText.color = Color.red;
                    }
                    else
                    {
                        winnerText.text = "WINNER PLAYER BLUE";
                        winnerText.color = Color.blue;
                    }
                }

            }
        }
    }
    void VerificacionDiagonalI(GameObject go, Color colorJugador)                    // Se crea un metodo llamado "VerificacionDiagonalI" con parametros de tipo GameObject y Color
    {
        counter = 0;                                                                 // Se define un contador en 0
        int _x = (int)go.transform.position.x;                                       // Se le asigna la posicion de go de x y y a las variables "_x" y "_y" respectivamente
        int _y = (int)go.transform.position.y;
       
        for (int i = 1; i < 4; i++)
        {
            if (_x - i > -1 && _y + i < height)                                       // Si "_x-i"mayor a -1 y "_y+i" es menor a la altura, pasa a verificar si hay elementos del mismo color en una linea diagonal hacia abajo en direccion a la derecha,
            {                                                                         // si los hay, agrega uno al contador por cada elemento del mismo color seguido en en esa direccion
                if (grid[_x - i, _y + i].GetComponent<Renderer>().material.color == colorJugador)
                {
                    counter++;
                }
                if (counter == 3)                                                   // Si el contador llega a 3 se reinicia la escena y se empieza el juego de nuevo
                {
                    panel.SetActive(true);
                    if (colorJugador == Color.red)
                    {
                        winnerText.text = "WINNER PLAYER RED";
                        winnerText.color = Color.red;
                    }
                    else
                    {
                        winnerText.text = "WINNER PLAYER BLUE";
                        winnerText.color = Color.blue;
                    }
                }
            }
            if (_x + i < width && _y - i > -1)                                      // Si "_x+i" es menor a la anchura y "_y-i" es mayor a -1, pasa a verificar si hay elementos del mismo color en una linea diagonal hacia arriba en direccion a la izquierda,
            {                                                                       // si los hay, agrega uno al contador por cada elemento del mismo color seguido en esa direccion
                if (grid[_x + i, _y - i].GetComponent<Renderer>().material.color == colorJugador)
                {
                    counter++;
                }
                if (counter == 3)                                            // Si el contador llega a 3 se reinicia la escena y se empieza el juego de nuevo
                {
                    panel.SetActive(true);
                    if (colorJugador == Color.red)
                    {
                        winnerText.text = "WINNER PLAYER RED";
                        winnerText.color = Color.red;
                    }
                    else
                    {
                        winnerText.text = "WINNER PLAYER BLUE";
                        winnerText.color = Color.blue;
                    }
                }

            }
        }
        
    }
}
// fin del codigo
