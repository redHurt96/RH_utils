using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace RH.Prototype.Data.Editor
{
    public class LibraryChecker
    {
        [MenuItem("Prototype/Library/IsExist")]
        public static void IsLibraryExist()
        {
            var libraryLoader = SaveSystemCreator.CreateLibraryLoader();

            Debug.Log($"[LibraryChecker]: Is game library exist = {libraryLoader.CanLoad}.");
        }

        [MenuItem("Prototype/Library/TryCreate")]
        public static void TryCreateLibrary()
        {
            var libraryLoader = SaveSystemCreator.CreateLibraryLoader();

            if (libraryLoader.CanLoad)
                Debug.Log($"[LibraryChecker] The library cannot be created because it already exists.");
            else
            {
                if (!Directory.Exists(Application.streamingAssetsPath))
                    Directory.CreateDirectory(Application.streamingAssetsPath);

                libraryLoader.Save(new GameLibraryData());
                Debug.Log($"[LibraryChecker] The library has created.");
            }
        }

        [MenuItem("Prototype/Library/ForceCreate")]
        public static void ForceCreateLibrary()
        {
            var libraryLoader = SaveSystemCreator.CreateLibraryLoader();

            if (!Directory.Exists(Application.streamingAssetsPath))
                Directory.CreateDirectory(Application.streamingAssetsPath);

            libraryLoader.Save(new GameLibraryData());
        }
    }
}
#endif