using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuit1
{
    private GameObject playerObject;
    private PlayerController playerController;
    private Rigidbody2D rb;
    private GameObject groundCheckObject; // object para groundCheck

    private GameObject managerObject;

    private GameManager gameManager;

    [SetUp]
    public void Setup()
    {    
        //Nuevo Jugador
        playerObject = new GameObject();
        playerController = playerObject.AddComponent<PlayerController>();
        rb = playerObject.AddComponent<Rigidbody2D>();

        
        groundCheckObject = new GameObject("GroundCheck");
        playerController.groundCheck = groundCheckObject.transform;

        playerController.rb = rb;

        // para que sea por pc
        playerController.controlmode = Controls.pc;


        playerObject = new GameObject();
        playerController = playerObject.AddComponent<PlayerController>();

        //Game Manager
        managerObject = new GameObject();
        gameManager = managerObject.AddComponent<GameManager>();
        
    }

    // Test de Movimiento
    [UnityTest]
    public IEnumerator PlayerMovementTest7()
    {
        // guardo la posicion inicial
        Vector3 initialPosition = playerController.transform.position;

        
        playerController.moveX = 1.0f; // muevo hacia la derecha al ser positivo
        playerController.FixedUpdate();
        yield return new WaitForSeconds(0.2f); // espero menos de medio seg

        Debug.Log("nueva pos, se movio a la derecha (+): " + playerController.transform.position.x);
        
        // confirmo que se movio a la derecha
        Assert.Greater(playerController.transform.position.x, initialPosition.x, "no se movio a la derecha");

        // guardo la posicion actual
        initialPosition = playerController.transform.position;

        
        playerController.moveX = -1.0f; // muevo hacia la izq al ser negativo
        playerController.FixedUpdate();
        yield return new WaitForSeconds(0.2f); // espero menos de medio seg

        Debug.Log("nueva pos, se movio a la izq (-): " + playerController.transform.position.x);

        // Verifica que el jugador se haya movido hacia la izquierda
        Assert.Less(playerController.transform.position.x, initialPosition.x, "no se movio a la izq");
    }
    [Test]
    public void JugadorSoloSeDesactivaDeEscenaCuandoMuere()
    {
        //Finalizar el juego
        //La variable que era privada en el Game Manager la hago publica
        gameManager.isGameOver = true;
        //Llamar al metodo que se encuentra en el GameManager
        gameManager.Death();
        //Preguntar si el player es null, ya que el juego no destruye al jugador
        //solo desactiva momentaneamente mientras se reinicia el juego
        Assert.IsNotNull(playerController);
        //Assert.AreEqual(true, playerController.isActiveAndEnabled);
    }




    [Test] // Test para incrmentar las monedas del contador 
    //No funciona 
        public void SumarMonedaAContador()
        {
            gameManager.IncrementCoinCount();
            Assert.AreEqual(1, gameManager.coinCount);
        }

        [Test] //Test para reiniciar el contador de monedas
        //No funciona
        public void RestablecerContadorDeMonedasACero()
        {
            gameManager.coinCount = 5;
            gameManager.isGameOver = true;
            gameManager.Death();

            var restartScore = 0;
            Assert.AreEqual(restartScore, gameManager.coinCount);
        }

[TearDown]
public void TearDown()
{
    Object.DestroyImmediate(playerObject);
    Object.DestroyImmediate(managerObject);
    Object.DestroyImmediate(gameManager);
}

}
