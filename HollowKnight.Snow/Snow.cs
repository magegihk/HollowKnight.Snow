using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modding;
using UnityEngine;

namespace HollowKnight.Snow
{
    public class Snow : Mod<SaveSettings, GlobalSettings>,ITogglableMod
    {
        internal static Snow instance;
        internal GameObject Snowobj;
        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            instance = this;
            instance.Log("Initializing");

            ParticleHandler.GetPrefabs(preloadedObjects);

            Snowobj = new GameObject();
            Snowobj.AddComponent<ParticleHandler>();
            GameObject.DontDestroyOnLoad(Snowobj);

            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

        private void SceneManager_activeSceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
        {
            if (HeroController.instance != null && arg1.name != "Menu_Title" && arg1.name != "Quit_To_Menu")
            {
                GameObject mySnow = ParticleHandler.Snow0;

                mySnow.transform.SetPosition2D(HeroController.instance.transform.position + new Vector3(0, 10, 0));
                mySnow.transform.SetParent(HeroController.instance.transform);
                mySnow.SetActive(true);
                foreach (Transform child in mySnow.transform)
                {
                    child.gameObject.SetActive(true);
                    child.gameObject.GetComponent<ParticleSystem>().Play();
                }
            }

        }

        

        public override List<(string, string)> GetPreloadNames()
        {
            return new List<(string, string)>
            {
                ("Deepnest_East_13","outskirts_particles/wispy smoke FG (1)"),
                ("Deepnest_East_13","outskirts_particles/wispy smoke BG"),
                ("Deepnest_East_13","outskirts_particles/wispy smoke FG")
            };
        }
        public override string GetVersion()
        {
            return "v1.0";
        }

        public override bool IsCurrent()
        {
            return true;
        }

        public void Unload()
        {
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
            ParticleHandler.StopSnow();
            GameObject.DestroyImmediate(Snowobj);
        }
    }
}
