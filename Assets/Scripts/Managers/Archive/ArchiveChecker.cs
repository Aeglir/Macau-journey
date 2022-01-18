using System.Net.Mime;
using System.IO;
namespace Managers.Archive
{
    public class ArchiveChecker
    {
        private readonly static string floderName = "Saves";
        public readonly static string fileSuffix = "sav";
        /// <summary>
        /// 检查存档路径是否存在，若不存在则创建。返回存档路径
        /// </summary>
        /// <returns>存档路径</returns>
        public static string checkedAndgetPath()
        {
            string path = UnityEngine.Application.persistentDataPath + "/" + floderName;
            DirectoryInfo di = new DirectoryInfo(path);
            if (!di.Exists)
            {
                di.Create();
            }
            return path;
        }
        /// <summary>
        /// 检查存档路径是否存在
        /// </summary>
        /// <returns>存档路径是否存在</returns>
        public static bool checkPath()
        {
            string path = UnityEngine.Application.persistentDataPath + "/" + floderName;
            DirectoryInfo di = new DirectoryInfo(path);
            if (!di.Exists)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 检查存档信息文件是否存在
        /// </summary>
        /// <returns>存档信息文件是否存在</returns>
        public static bool checkInfoFile()
        {
            string path = checkedAndgetPath();
            string fullName = path + "/" + ArchiveManager.InfoFileName;
            FileInfo file = new FileInfo(fullName);
            if (!file.Exists)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 检查存档文件是否存在
        /// </summary>
        /// <param name="fileName">存档文件名带后缀不包括文件路径</param>
        /// <returns>存档文件是否存在</returns>
        public static bool checkDataFile(string fileName)
        {
            string path = checkedAndgetPath();
            string fullName = path + "/" + fileName;
            FileInfo file = new FileInfo(fullName);
            if (!file.Exists)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取存档信息文件完整路径，包括文件路径、文件名及其后缀
        /// </summary>
        /// <returns>存档信息文件完整路径，包括文件路径、文件名及其后缀</returns>
        public static string getInfoFileName() => UnityEngine.Application.persistentDataPath + "/" + floderName + "/" + ArchiveManager.InfoFileName;
        /// <summary>
        /// 获取存档信息文件完整路径，包括文件路径、文件名及其后缀
        /// </summary>
        /// <param name="fileName">存档文件名带后缀不包括文件路径</param>
        /// <returns>存档信息文件完整路径，包括文件路径、文件名及其后缀</returns>
        public static string getFileName(string fileName) => UnityEngine.Application.persistentDataPath + "/" + floderName + "/" + fileName;
    }
}