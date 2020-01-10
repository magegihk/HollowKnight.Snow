using Modding;
using System.Collections.Generic;
using UnityEngine;
using USceneManager = UnityEngine.SceneManagement.SceneManager;

namespace HollowKnight.Snow
{
    public class Snow : Mod, ITogglableMod
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

            //Some how preload objects will stop Randomizer2.0 from generating menu.
            //Something to do with API?
            //Load menu to trigger activescenechanged.
            USceneManager.LoadScene("Quit_To_Menu");
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
            return "v1.1";
        }

        public override bool IsCurrent()
        {
            return true;
        }

        public void Unload()
        {
            GameObject.DestroyImmediate(ParticleHandler.Clone);
            GameObject.DestroyImmediate(Snowobj);
        }
    }
}