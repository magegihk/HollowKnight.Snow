using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HollowKnight.Snow
{
    internal class ParticleHandler : MonoBehaviour
    {
        public int snowLevel = 0;  //random 5~500
        public float snowLast = 0.0f;  //random 10~60s
        private float Timer = 0.0f;

        private static GameObject _snow1;
        private static GameObject _snow2;
        private static GameObject _snow3;
        private static GameObject _snow0;
        public static GameObject Clone;
        public static void GetPrefabs(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            if (preloadedObjects == null) return;
            _snow0 = new GameObject();
            GameObject.DontDestroyOnLoad(_snow0);
            _snow1 = preloadedObjects["Deepnest_East_13"]["outskirts_particles/wispy smoke FG (1)"];
            _snow2 = preloadedObjects["Deepnest_East_13"]["outskirts_particles/wispy smoke BG"];
            _snow3 = preloadedObjects["Deepnest_East_13"]["outskirts_particles/wispy smoke FG"];
            _snow1.transform.SetParent(_snow0.transform);
            _snow2.transform.SetParent(_snow0.transform);
            _snow3.transform.SetParent(_snow0.transform);
            SetSnow(_snow1);
            SetSnow(_snow2);
            SetSnow(_snow3);
        }

        private static void SetSnow(GameObject gameObject)
        {
            gameObject.transform.SetScaleX(60);
            gameObject.transform.SetScaleY(30);
            var ps = gameObject.GetComponent<ParticleSystem>();
            var main = ps.main;
            main.maxParticles = 2000;
            var em = ps.emission;
            em.rateOverTime = 50;
        }
        private void CloneSnow()
        {
            Clone = GameObject.Instantiate(_snow0);
            Clone.transform.SetPosition2D(HeroController.instance.transform.position + new Vector3(0, 10, 0));
            Clone.transform.SetParent(HeroController.instance.transform);
            Clone.SetActive(true);
            foreach (Transform child in Clone.transform)
            {
                child.gameObject.SetActive(true);
                child.gameObject.GetComponent<ParticleSystem>().Play();
            }
        }
        private void RandomSnow(GameObject gameObject, float time)
        {
            var em = gameObject.GetComponent<ParticleSystem>().emission;
            em.rateOverTime = time;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                
            }
            if (GameManager.instance?.gameState == GlobalEnums.GameState.PLAYING)
            {
                if (Clone == null)
                {
                    CloneSnow();
                }
                Timer += Time.deltaTime;
                if (Timer >= snowLast)
                {
                    snowLevel = Random.Range(5, 500);
                    snowLast = Random.Range(10, 60);
                    Timer = 0;
                    RandomSnow(Clone.transform.GetChild(0).gameObject, snowLevel);
                    RandomSnow(Clone.transform.GetChild(1).gameObject, snowLevel);
                    RandomSnow(Clone.transform.GetChild(2).gameObject, snowLevel);
                    Snow.instance.Log("Change snow scale to " + snowLevel);
                }
            }
        }
    }
}