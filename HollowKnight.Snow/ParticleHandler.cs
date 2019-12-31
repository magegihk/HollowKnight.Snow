using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HollowKnight.Snow
{
    class ParticleHandler : MonoBehaviour
    {
        public int snowLevel = 0;  //random 5~500
        public float snowLast = 0.0f;  //random 10~60s
        private float Timer = 0.0f;
        private ParticleSystem[] particles;
        
        private static GameObject _snow1;
        private static GameObject _snow2;
        private static GameObject _snow3;
        public static GameObject Snow0;

        public static void GetPrefabs(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            if (preloadedObjects == null) return;
            Snow0 = new GameObject();
            GameObject.DontDestroyOnLoad(Snow0);
            _snow1 = preloadedObjects["Deepnest_East_13"]["outskirts_particles/wispy smoke FG (1)"];
            _snow2 = preloadedObjects["Deepnest_East_13"]["outskirts_particles/wispy smoke BG"];
            _snow3 = preloadedObjects["Deepnest_East_13"]["outskirts_particles/wispy smoke FG"];
            _snow1.transform.SetParent(Snow0.transform);
            _snow2.transform.SetParent(Snow0.transform);
            _snow3.transform.SetParent(Snow0.transform);
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
        private void RandomSnow(GameObject gameObject,float time)
        {
            var em = gameObject.GetComponent<ParticleSystem>().emission;
            em.rateOverTime = time;
        }
        public static void StopSnow()
        {
            if (Snow0 != null)
            {
                Snow0.SetActive(false);
            }
        }
        void Start()
        {
            if (Snow0 != null)
            {
                Snow0.SetActive(true);
            }
        }
        void Update()
        {
#if DEBUG
            if (Input.GetKeyDown(KeyCode.Q))
            {
                particles = GameObject.FindObjectsOfType<ParticleSystem>();
                Snow.instance.GlobalSettings.Particles = String.Join("|", particles.Select(p => p.name).ToArray());
                Snow.instance.SaveGlobalSettings();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Snow.instance.LoadGlobalSettings();
                string[] names = Snow.instance.GlobalSettings.Particles.Split('|');
                for (int j = 0; j < particles.Length; j++)
                {
                    for (int i = 0; i < names.Length; i++)
                    {
                        if (names[i] == particles[j].name)
                        {
                            Snow.instance.Log(particles[j].gameObject.name);
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (HeroController.instance.gameObject != null)
                {
                    GameObject mySnow = Snow0;

                    mySnow.transform.SetPosition2D(HeroController.instance.transform.position + new Vector3(0,10,0));
                    mySnow.transform.SetParent(HeroController.instance.transform);
                    mySnow.SetActive(true);
                    foreach (Transform child in mySnow.transform)
                    {
                        child.gameObject.SetActive(true);
                        child.gameObject.GetComponent<ParticleSystem>().Play();
                        Snow.instance.Log(child.position);
                    }
                    Snow.instance.Log(mySnow.transform.position);
                }
            }
#endif
            Timer += Time.deltaTime;
            if (Timer >= snowLast)
            {
                snowLevel = Random.Range(5, 500);
                snowLast = Random.Range(10, 60);
                Timer = 0;
                RandomSnow(_snow1,snowLevel);
                RandomSnow(_snow2,snowLevel);
                RandomSnow(_snow3,snowLevel);
                Snow.instance.Log("Change snow scale to " + snowLevel);
            }
        }

 
    }
}
