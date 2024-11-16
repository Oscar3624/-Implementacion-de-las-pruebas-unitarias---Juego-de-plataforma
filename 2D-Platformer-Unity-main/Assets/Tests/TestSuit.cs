using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class TestSuit
{    private GameObject playerObject;
    private PlayerController playerController;
    private Rigidbody2D rb;

    // Configuración de la prueba: crear un objeto de prueba con el PlayerController
    [SetUp]
    public void Setup()
    {
        playerObject = new GameObject();
        playerController = playerObject.AddComponent<PlayerController>();
        rb = playerObject.AddComponent<Rigidbody2D>();
        playerController.rb = rb;

        // Se configura para un modo de controles PC para las pruebas
        playerController.controlmode = Controls.pc;
    }

    public IEnumerator PlayerMovementTest()
{
    // Guarda la posición inicial
    Vector3 initialPosition = playerController.transform.position;

    // Simula una entrada para mover al jugador (por ejemplo, hacia la derecha)
    playerController.horizontalInput = 1; // Suponiendo que este valor controla el movimiento horizontal
    yield return null;  // Espera un cuadro para que el movimiento se aplique

    // Verifica si la posición X del jugador ha cambiado (el jugador debe haberse movido)
    Assert.AreNotEqual(playerController.transform.position.x, initialPosition.x, "Error: El jugador no se movió correctamente.");
}

}