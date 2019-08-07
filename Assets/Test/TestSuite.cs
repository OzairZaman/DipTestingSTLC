using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.TestTools;
using NUnit.Framework;

public class TestSuite
{
    private GameObject game;
    private GameManager gameManager;
    private Player player;

    [SetUp] public  void Setup()
    {
        //load and spawn prefab
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Game");
        game = Object.Instantiate(prefab);

        //get game manager
        gameManager = game.GetComponent<GameManager>();
        // use the singleton
        //gameManager = GameManager.Instance;

        //get player 2 ways
        //searches entire game for a type
        //player = Object.FindObjectOfType<Player>();

        // searches locally 
        player = game.GetComponentInChildren<Player>(); //
    }


    [UnityTest] public IEnumerator GamePrefabLoaded()
    {
        //at the end of the frame is when things get spawned
        yield return new WaitForEndOfFrame();
        

        //lets make an assetion
        Assert.NotNull(game);
    }


    [UnityTest] public IEnumerator PlayerDoesNotExist()
    {
        yield return new WaitForEndOfFrame();

        Assert.NotNull(player, "Player done the Harry!");
    }



    [UnityTest]
    public IEnumerator ItemCollidesWithPlayer()
    {
        

        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Entities/Item");
        Vector3 pos = player.transform.position;
        GameObject item = Object.Instantiate(itemPrefab, pos, Quaternion.identity);

        //yield return new WaitForSeconds(.1f);
        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();

        Assert.IsTrue(item == null);
    }


    [UnityTest] public IEnumerator ItemCollectedAndScoreAdded()
    {
        int oldScore = gameManager.score;

        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Entities/Item");
        Vector3 pos = player.transform.position;
        GameObject item = Object.Instantiate(itemPrefab, pos, Quaternion.identity);

        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();

        int newScore = gameManager.score;

        Assert.IsTrue(newScore > oldScore);
    }


    [TearDown] public void Teaddown()
    {
        Object.Destroy(game);
    }
}
